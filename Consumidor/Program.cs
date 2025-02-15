using Consumidor;
using Consumidor.Eventos;
using Core.Repository;
using Infrastructure.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var filaCadastro = configuration.GetSection("MassTransit")["FilaCadastro"] ?? string.Empty;
var filaAlteracao = configuration.GetSection("MassTransit")["FilaAlteracao"] ?? string.Empty;
var filaExclusao = configuration.GetSection("MassTransit")["FilaExclusao"] ?? string.Empty;
var servidor = configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
var usuario = configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
var senha = configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", h =>
        {
            h.Username(usuario);
            h.Password(senha);
        });
        cfg.ReceiveEndpoint(filaCadastro, e=>
        {
            e.Consumer<CadastroContatoConsumidor>(context);
        });
        cfg.ReceiveEndpoint(filaAlteracao, e =>
        {
            e.Consumer<AlteracaoContatoConsumidor>(context);
        });
        cfg.ReceiveEndpoint(filaExclusao, e =>
        {
            e.Consumer<ExclusaoContatoConsumidor>(context);
        });

        cfg.ConfigureEndpoints(context);
    });

    x.AddConsumer<CadastroContatoConsumidor>();
    x.AddConsumer<AlteracaoContatoConsumidor>();
    x.AddConsumer<ExclusaoContatoConsumidor>();
});



var host = builder.Build();
host.Run();
