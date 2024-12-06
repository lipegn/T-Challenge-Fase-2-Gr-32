using Core.Entity;
using Core.Input;
using Core.Repository;
using MassTransit;

namespace Consumidor.Eventos
{
    public class CadastroContatoConsumidor : IConsumer<ContatoInput>
    {
        private readonly IContatoRepository _contatoRepository;

        public CadastroContatoConsumidor(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }
        public Task Consume(ConsumeContext<ContatoInput> context)
        {
            Console.WriteLine("Inclusão: "+context.Message);
            var contato = new Contato()
            {
                Nome = context.Message.Nome,
                DDD = context.Message.DDD,
                Telefone = Convert.ToInt32(context.Message.Telefone),
                Email = context.Message.Email,
            };
            _contatoRepository.Cadastrar(contato);

            return Task.CompletedTask;
        }
    }
}
