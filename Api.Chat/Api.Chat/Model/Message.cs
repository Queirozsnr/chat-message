namespace Api.Chat.Model
{
    public class Message
    {
        public string Name { get; private set; }
        public string Body { get; private set; }
        public string Date { get; private set; }

        public Message(string name, string body)
        {
            Name = name;
            Body = body;
            Date = DateTime.Now.ToString("G");
        }
    }
}
