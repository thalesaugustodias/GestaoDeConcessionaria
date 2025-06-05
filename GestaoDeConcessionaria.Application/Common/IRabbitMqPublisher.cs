namespace GestaoDeConcessionaria.Application.Common
{
    public interface IRabbitMqPublisher
    {
        void Publish<T>(T message, string routingKey);
    }
}
