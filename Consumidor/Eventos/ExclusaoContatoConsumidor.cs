using Core.Entity;
using MassTransit;

namespace Consumidor.Eventos
{
    public class ExclusaoContatoConsumidor : IConsumer<IdMessage>
    {
        public Task Consume(ConsumeContext<IdMessage> context)
        {
            Console.WriteLine("Exclusão: "+context.Message);
            return Task.CompletedTask;
        }
    }
}
