using System.Diagnostics.CodeAnalysis;

namespace Waitless.Models.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        [NotNull]
        public string EventName { get; set; }
        [MaybeNull]
        public string EventDescription { get; set; }
        [NotNull]
        public string Initiator { get; set; }
        [NotNull]
        public DateTime CreatedDate { get; set; }
        [NotNull]
        public DateTime StartTime { get; set; }
        [NotNull]
        public DateTime EndTime { get; set; }
        [MaybeNull]
        public ICollection<Participant> Participants { get; set; }


    }
}
