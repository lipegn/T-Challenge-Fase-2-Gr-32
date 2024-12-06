using Core.Input;
using Core.Repository;
using MassTransit;

namespace Consumidor.Eventos
{
    public class AlteracaoContatoConsumidor : IConsumer<ContatoUpdateInput>
    {
        private readonly IContatoRepository _contatoRepository;

        public AlteracaoContatoConsumidor(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public Task Consume(ConsumeContext<ContatoUpdateInput> context)
        {
            Console.WriteLine("Alteração : " + context.Message);

            var contato = _contatoRepository.ObterPorId(context.Message.Id);
            contato.Nome = context.Message.Nome;
            contato.DDD = context.Message.DDD;
            contato.Telefone = Convert.ToInt32(context.Message.Telefone);
            contato.Email = context.Message.Email;


            _contatoRepository.Alterar(contato);

            return Task.CompletedTask;
        }
    }
}
