using Core.Entity;
using Core.Input;
using Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace TechChallangeCadastroContatosAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        /// <summary>
        /// Necessita de autenticação via token para retorno de todos os contatos
        /// </summary>
        /// <returns>Retorna uma lista de contato</returns>
        /// <response code="200">Sucesso na execução ao retornar os contatos</response>
        /// <response code="500">Não foi possivel retornar as informações dos contatos</response>
        /// <response code="401">Token inválido</response>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                return Ok(_contatoRepository.ObterTodos());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Necessita de autenticação via token para retorno o contato por Id
        /// </summary>
        /// <param name="id">Id do contato que será retornado</param>
        /// <returns>Retorna um Contato filtrado pelo Id</returns>
        /// <response code="200">Sucesso na execução ao retornar do contato</response>
        /// <response code="500">Não foi possivel retornar as informações do contato</response>
        /// <response code="401">Token inválido</response>
        [Authorize]
        [HttpGet("PorId/{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {

            try
            {
                return Ok(_contatoRepository.ObterPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        /// Necessita de autenticação via token para retorno o contato por DDD
        /// </summary>
        /// <param name="ddd">DDD do contato que será retornado</param>
        /// <returns></returns>
        /// <response code="200">Sucesso na execução ao retornar do contato</response>
        /// <response code="500">Não foi possivel retornar as informações do contato</response>
        /// <response code="401">Token inválido</response>
        [Authorize]
        [HttpGet("GetPorDDD/{ddd:int}")]
        public IActionResult GetPorDDD([FromRoute] int ddd)
        {

            try
            {
                return Ok(_contatoRepository.ObterPorDDD(ddd));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        /// Necessita de autenticação via token para cadastrar um novo contato
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// {
        ///     "Nome": "Nome do Contato",
        ///     "DDD": "DDD (Região) do telefone do contato",
        ///     "Telefone": "Telefone do Contato",
        ///     "Email": "Email do Contato",
        /// }
        /// 
        /// Observação: Não é necessario informar o Id
        /// </remarks>
        /// <param name="input">Objeto do ContatoInput</param>
        /// <returns>Retorna Contato cadastrado</returns>
        /// <response code="200">Sucesso na execução da inclusão do contato na fila-cadastro</response>
        /// <response code="500">Não foi possivel incluir um novo contato</response>
        /// <response code="401">Token inválido</response>
        [Authorize]
        [HttpPost]
        public async  Task<IActionResult> Post([FromBody] ContatoInput input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = "localhost",
                        UserName = "guest",
                        Password = "guest"
                    };

                    using var connection = await factory.CreateConnectionAsync();
                    using var channel = await connection.CreateChannelAsync();
                    {
                        //está declarando a fila
                        await channel.QueueDeclareAsync(
                            queue: "fila-cadastro",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        //serializa o dado
                        var message = JsonSerializer.Serialize(
                            new Contato()
                            {
                                Nome = input.Nome,
                                DDD = input.DDD,
                                Telefone = Convert.ToInt32(input.Telefone),
                                Email = input.Email,
                            });

                        //faz encoding para não perder acentuação
                        var body = Encoding.UTF8.GetBytes(message);

                        await channel.BasicPublishAsync(
                            exchange: "",
                            routingKey: "fila-cadastro",
                            body: body);
                    }

                    //_contatoRepository.Cadastrar(contato);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        /// Necessita de autenticação via token para alterar um contato
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// {
        ///     "Id": "Id do Contato"
        ///     "Nome": "Nome do Contato",
        ///     "DDD": "DDD (Região) do telefone do contato",
        ///     "Telefone": "Telefone do Contato",
        ///     "Email": "Email do Contato",
        /// }
        /// 
        /// </remarks>
        /// <param name="input">Objeto de ContatoUpdate</param>
        /// <returns>Contato alterado</returns>
        /// <response code="200">Sucesso na inclusão da alteração do contato na fila-alteracao</response>
        /// <response code="500">Não foi possivel alterar o contato</response>
        /// <response code="401">Token inválido</response>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ContatoUpdateInput input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = "localhost",
                        UserName = "guest",
                        Password = "guest"
                    };

                    using var connection = await factory.CreateConnectionAsync();
                    using var channel = await connection.CreateChannelAsync();
                    {
                        //está declarando a fila
                        await channel.QueueDeclareAsync(
                            queue: "fila-alteracao",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        var contato = _contatoRepository.ObterPorId(input.Id);
                        contato.Nome = input.Nome;
                        contato.DDD = input.DDD;
                        contato.Telefone = Convert.ToInt32(input.Telefone);
                        contato.Email = input.Email;
                        //_contatoRepository.Alterar(contato);

                        //serializa o dado
                        var message = JsonSerializer.Serialize(contato);

                        //faz encoding para não perder acentuação
                        var body = Encoding.UTF8.GetBytes(message);

                        await channel.BasicPublishAsync(
                            exchange: "",
                            routingKey: "fila-alteracao",
                            body: body);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Necessita de autenticação via token para excluir um contato da base de dados
        /// </summary>
        /// <param name="id">Id do contato</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao inclir o contato para exclusão na fila-exclusao</response>
        /// <response code="500">Não foi possivel excluir o contato</response>
        /// <response code="401">Token inválido</response>
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };

                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();
                {
                    //está declarando a fila
                    await channel.QueueDeclareAsync(
                        queue: "fila-exclusao",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    //_contatoRepository.Deletar(id);
                    //serializa o dado
                    var message = JsonSerializer.Serialize(id);

                    //faz encoding para não perder acentuação
                    var body = Encoding.UTF8.GetBytes(message);

                    await channel.BasicPublishAsync(
                        exchange: "",
                        routingKey: "fila-exclusao",
                        body: body);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
