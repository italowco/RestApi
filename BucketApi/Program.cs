using BucketApi.Consumer;
using GreenPipes;
using MassTransit;
using RestApi.Domain.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(mt =>
{
    mt.AddConsumer<ProductConsumer>();

    mt.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => {
        
        cfg.Host(new Uri("amqp://localhost:15672"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("productQueue", ep =>
        {
            ep.PrefetchCount = 10;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<ProductConsumer>(provider);
        });

    }));


});

builder.Services.AddMassTransitHostedService();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
