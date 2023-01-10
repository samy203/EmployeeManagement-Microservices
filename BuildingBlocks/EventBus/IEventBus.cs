namespace EventBus
{
    public interface IEventBus
    {
        void Publish<T>(T message);

        void Subscribe<T>(string sub_id, Action<T> action);
    }
}