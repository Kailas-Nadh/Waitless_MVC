using System.Diagnostics.CodeAnalysis;
using Waitless.Models.Entities;

namespace Waitless.Models.DTOs
{
    public class CreateParticipantViewModel
    {
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string EmailId { get; set; }
        [NotNull]
        public string PhoneNumber { get; set; }
        [NotNull]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
