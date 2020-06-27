namespace Simple.Game.Domain
{
    internal interface IMessageSender
    {
        void Send(string message);
    }
}