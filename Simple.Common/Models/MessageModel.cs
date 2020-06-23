namespace Simple.Common.Models
{
    public class MessageModel
    {
        public string Header { get; }
        public string Body { get; }

        public MessageModel(string header, string body)
        {
            Header = header;
            Body = body;
        }
    }
}