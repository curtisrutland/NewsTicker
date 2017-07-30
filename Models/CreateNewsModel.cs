using NewsTicker.Entities;

namespace NewsTicker.Models
{
    public class CreateNewsModel
    {
        public string Message { get; set; }
        public int Group { get; set; }
        public Severity Severity { get; set; }

        public NewsEvent ToNewsEvent() => new NewsEvent
        {
            Group = Group,
            Message = Message,
            Severity = Severity
        };
    }
}