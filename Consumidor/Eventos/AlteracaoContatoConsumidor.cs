using Core.Input;
using MassTransit;

namespace Consumidor.Eventos
{
    public class AlteracaoContatoConsumidor : IConsumer<ContatoUpdateInput>
    {
        public Task Consume(ConsumeContext<ContatoUpdateInput> context)
        {
            Console.WriteLine("Alteração : "+context.Message);
            return Task.CompletedTask;
        }
    }
}
