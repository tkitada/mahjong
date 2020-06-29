using Newtonsoft.Json;
using Simple.Common.Models;

namespace Simple.Player.Domain
{
    internal class Switcher
    {
        public Switcher(IMessageReceiver receiver)
        {
            receiver.MessageReceivedEvent += Receiver_MessageReceivedEvent;
        }

        private void Receiver_MessageReceivedEvent(object sender, MessageReceivedEventArgs e)
        {
            var message = JsonConvert.DeserializeObject<MessageModel>(e.Message);
            switch (message.Header)
            {
                case "Tsumo":
                    var body = JsonConvert.DeserializeObject<TsumoModel>(message.Body);
                    break;

                default:
                    break;
            }
        }
    }
}