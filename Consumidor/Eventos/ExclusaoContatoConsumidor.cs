using Core.Entity;
using Core.Repository;
using MassTransit;

namespace Consumidor.Eventos
{
    public class ExclusaoContatoConsumidor : IConsumer<IdMessage>
    {
        private readonly IContatoRepository _contatoRepository;

        public ExclusaoContatoConsumidor(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }
        public Task Consume(ConsumeContext<IdMessage> context)
        {
            Console.WriteLine("Exclusão: "+context.Message);
            _contatoRepository.Deletar(context.Message.Id);
            return Task.CompletedTask;
        }
    }
}
