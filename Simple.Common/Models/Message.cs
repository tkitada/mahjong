﻿namespace Simple.Common.Models
{
    public class Message
    {
        public string Header { get; }
        public string Body { get; }

        public Message(string header, string body)
        {
            Header = header;
            Body = body;
        }
    }
}