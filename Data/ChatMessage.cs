namespace SKWebChatBot.Data
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
