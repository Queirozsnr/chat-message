using Api.Chat.Model;

namespace Api.Chat.Hubs
{
    public interface IHubProvider
    {
        Task ReceivedMessage(Message message);
    }
}
