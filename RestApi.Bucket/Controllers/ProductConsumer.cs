using MassTransit;
using RestApi.Domain.Model;

namespace BucketApi.Consumer
{
    public class ProductConsumer : IConsumer<Product>
    {
        private readonly ILogger<ProductConsumer> logger;

        public ProductConsumer(ILogger<ProductConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<Product> context)
        {
            await Console.Out.WriteAsync(context.Message.Title);

            logger.LogInformation("Consumed product: {0}", context.Message.Title);
        }
    }
}
