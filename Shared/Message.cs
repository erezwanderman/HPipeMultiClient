using MessagePack;

namespace Shared
{
    [MessagePackObject]
    [Serializable]
    public class Message
    {
        [Key(0)]
        public string? Request { get; set; }

        [Key(1)]
        public string? Response { get; set; }

        public override string ToString()
        {
            return (Request ?? string.Empty) + " ---- " + (Response ?? string.Empty);
        }
    }
}