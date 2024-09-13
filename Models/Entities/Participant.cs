using System;
using System.Diagnostics.CodeAnalysis;

namespace Waitless.Models.Entities
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        [NotNull]

        public string Name { get; set; }
        [NotNull]

        public string EmailId { get; set; }
        [NotNull]

        public string PhoneNumber { get; set; }
        [NotNull]

        public bool IsComplete { get; set; }
        [NotNull]

        public bool IsCurrent { get; set; }
        [NotNull]

        public DateTime RegistrationDate { get; set; }
        [NotNull]

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
