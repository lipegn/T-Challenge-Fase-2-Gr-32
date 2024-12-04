using Core.Input;
using MassTransit;

namespace Consumidor.Eventos
{
    public class CadastroContatoConsumidor : IConsumer<ContatoInput>
    {
        public Task Consume(ConsumeContext<ContatoInput> context)
        {
            Console.WriteLine("Inclusão: "+context.Message);
            return Task.CompletedTask;
        }
    }
}
