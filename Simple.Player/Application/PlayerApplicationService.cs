using Newtonsoft.Json;
using Simple.Common.Models;
using Simple.Player.Domain;
using Simple.Player.Infrastructure;
using System;

namespace Simple.Player.Application
{
    public class PlayerApplicationService
    {
        public event EventHandler<JoinEventArgs> JoinEvent;

        public event EventHandler<HandEventArgs> HandEvent;

        public event EventHandler<TsumoEventArgs> TsumoEvent;

        public event EventHandler<DahaiEventArgs> DahaiEvent;

        public event EventHandler<AgariEventArgs> AgariEvent;

        private readonly IMessageClient client_;

        private readonly string name_;

        public PlayerApplicationService(string name)
        {
            client_ = new MessageClient();

            name_ = name;
            client_.MessageReceivedEvent += OnMessageReceived;
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var message = JsonConvert.DeserializeObject<Message>(e.Message);
            switch (message.Header)
            {
                case "Join":
                    var joinRes = JsonConvert.DeserializeObject<JoinRes>(message.Body);
                    JoinEvent?.Invoke(this, new JoinEventArgs
                    {
                        JoinRes = joinRes
                    });
                    break;

                case "Hand":
                    var handRes = JsonConvert.DeserializeObject<HandRes>(message.Body);
                    HandEvent?.Invoke(this, new HandEventArgs
                    {
                        HandRes = handRes
                    });
                    break;

                case "Tsumo":
                    var tsumoRes = JsonConvert.DeserializeObject<TsumoRes>(message.Body);
                    TsumoEvent?.Invoke(this, new TsumoEventArgs
                    {
                        TsumoRes = tsumoRes
                    });
                    break;

                case "Dahai":
                    var dahaiRes = JsonConvert.DeserializeObject<DahaiRes>(message.Body);
                    DahaiEvent?.Invoke(this, new DahaiEventArgs
                    {
                        DahaiRes = dahaiRes
                    });
                    break;

                case "Agari":
                    var agariRes = JsonConvert.DeserializeObject<AgariRes>(message.Body);
                    AgariEvent?.Invoke(this, new AgariEventArgs
                    {
                        AgariRes = agariRes
                    });
                    break;

                default:
                    break;
            }
        }

        public void RequestJoin()
        {
            var joinReq = new JoinReq
            {
                Name = name_
            };
            var message = new Message
            {
                Header = "Join",
                Body = JsonConvert.SerializeObject(joinReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }

        public void RequestHand()
        {
            var handReq = new HandReq { };
            var message = new Message
            {
                Header = "Hand",
                Body = JsonConvert.SerializeObject(handReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }

        public void RequestTsumo()
        {
            var tsumoReq = new TsumoReq { };
            var message = new Message
            {
                Header = "Tsumo",
                Body = JsonConvert.SerializeObject(tsumoReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }

        public void RequestDahai(int index)
        {
            var dahaiReq = new DahaiReq
            {
                Index = index
            };
            var message = new Message
            {
                Header = "Dahai",
                Body = JsonConvert.SerializeObject(dahaiReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }

        public void RequestAgari()
        {
            var agariReq = new AgariReq { };
            var message = new Message
            {
                Header = "Agari",
                Body = JsonConvert.SerializeObject(agariReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }
    }
}