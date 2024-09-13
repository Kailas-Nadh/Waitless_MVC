using Waitless.Models.Entities;

namespace Waitless.Models.DTOs
{
    public class ParticipantsWithEventNameViewModel
    {
        public string EventName { get; set; }
        public List<Participant> Participants { get; set; }
    }
}
