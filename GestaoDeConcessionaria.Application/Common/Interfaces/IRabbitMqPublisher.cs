namespace GestaoDeConcessionaria.Application.Common.Interfaces
{
    public interface IRabbitMqPublisher
    {
        void Publish<T>(T message, string routingKey);
    }
}
