using System.Diagnostics.CodeAnalysis;

namespace Waitless.Models.DTOs
{
    public class CreateEventViewModel
    {
        [NotNull]
        public string EventName { get; set; }
        [MaybeNull]
        public string EventDescription { get; set; }
        [NotNull]
        public string Initiator { get; set; }
        [NotNull]
        public DateTime StartTime { get; set; }
        [NotNull]
        public DateTime EndTime { get; set; }
    }
}
