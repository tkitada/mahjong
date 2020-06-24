using Newtonsoft.Json;
using Simple.Common.Models;
using Simple.Player.Domain;
using System;

namespace Simple.Player.Application
{
    public class PlayerApplicationService
    {
        public event EventHandler<TsumoEventArgs> TsumoEvent;

        private readonly IMessageReceiver receiver_;

        public PlayerApplicationService()
        {
            receiver_.MessageReceivedEvent += Receiver__MessageReceivedEvent;
        }

        private void Receiver__MessageReceivedEvent(object sender, MessageReceivedEventArgs e)
        {
            var message = JsonConvert.DeserializeObject<MessageModel>(e.Message);
            switch (message.Header)
            {
                case "Tsumo":
                    TsumoEvent(this, new TsumoEventArgs(JsonConvert.DeserializeObject<TsumoModel>(message.Body)));
                    break;

                default:
                    break;
            }
        }

        public void Dahai(int index, bool IsRiichi)
        {

        }
    }
}