using System;

namespace NewsTicker.Entities
{
    public enum Severity { Success, Info, Warning, Critical };
    public class NewsEvent
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public Severity Severity { get; set; }
        public int Group { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}