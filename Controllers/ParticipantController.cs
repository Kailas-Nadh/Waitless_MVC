using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waitless.Data;
using Waitless.Models.DTOs;
using Waitless.Models.Entities;

namespace Waitless.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly AuthDbContext _dbContext;
        public ParticipantController(AuthDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var participants = await _dbContext.Participants.ToListAsync();
            return View(participants);
        }

        [HttpGet]
        public async Task<IActionResult> ListEvents()
        {
            var eventNameList = await _dbContext.Events.Select(e => new EventCardListViewModel
            {
                EventId = e.EventId,
                EventName = e.EventName,
                EventDescription = e.EventDescription
            }).ToListAsync();
            return View(eventNameList);
        }

        [Route("Participant/Add/{EventId}")]
        [HttpGet]
        public async Task<IActionResult> Add(int EventId)
        {
            var eventToAdd = await _dbContext.Events.FindAsync(EventId);

            if (eventToAdd == null)
            {
                return NotFound();
            }

            var viewModel = new CreateParticipantViewModel
            {
                EventId = EventId
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Add(CreateParticipantViewModel viewModel)
        {
            var participant = new Participant
            {
                Name = viewModel.Name,
                EmailId = viewModel.EmailId,
                PhoneNumber = viewModel.PhoneNumber,
                IsComplete = false,
                IsCurrent = false,
                RegistrationDate = DateTime.Now,
                EventId = viewModel.EventId,
            };
            await _dbContext.Participants.AddAsync(participant);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("ListParticipantsByEventId", "Participant", new { EventId = viewModel.EventId });
        }

        [HttpGet]
        public async Task<IActionResult> ListAllEventsBeforeTable()
        {
            var eventNameList = await _dbContext.Events.Select(e => new EventCardListViewModel
            {
                EventId = e.EventId,
                EventName = e.EventName,
                EventDescription = e.EventDescription
            }).ToListAsync();
            return View(eventNameList);
        }

        [Route("Participant/ListParticipantsByEventId/{EventId}")]
        [HttpGet]
        public async Task<IActionResult> ListParticipantsByEventId(int EventId)
        {

            var participants = await _dbContext.Participants
                                        .Where(p => p.EventId == EventId)
                                        .ToListAsync();

            var eventName = await _dbContext.Events
                                    .Where(e => e.EventId == EventId)
                                    .Select(e => e.EventName)
                                    .FirstOrDefaultAsync();


            var viewModel = new ParticipantsWithEventNameViewModel
            {
                EventName = eventName,
                Participants = participants
            };

            // Pass the view model to the view
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ListParticipantsByEventId(int ParticipantId, int EventId, string action)
        {
            var participant = await _dbContext.Participants
                                         .Where(p => p.ParticipantId == ParticipantId && p.EventId == EventId)
                                         .FirstOrDefaultAsync();
            if (participant == null)
            {
                return NotFound();
            }

            if(action == "complete")
            {
                participant.IsComplete = true;
                await _dbContext.SaveChangesAsync();
            }
            else if (action == "skip")
            {
                participant.IsCurrent = true;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListParticipantsByEventId", "Participant", new { EventId = EventId });
        }
    }
}