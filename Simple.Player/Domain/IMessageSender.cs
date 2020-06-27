namespace Simple.Player.Domain
{
    internal interface IMessageSender
    {
        void Send(string message);
    }
}