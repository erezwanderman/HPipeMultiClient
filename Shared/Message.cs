namespace Shared
{
    [Serializable]
    public class Message
    {
        public string? Request { get; set; }
        public string? Response { get; set; }

        public override string ToString()
        {
            return (Request ?? string.Empty) + " ---- " + (Response ?? string.Empty);
        }
    }
}