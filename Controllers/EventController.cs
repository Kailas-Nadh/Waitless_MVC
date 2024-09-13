using Microsoft.AspNetCore.Mvc;
using Waitless.Models.DTOs;
using Waitless.Models.Entities;
using Waitless.Areas.Identity.Data;
using Waitless.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Waitless.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly AuthDbContext _dbContext;
        public EventController(AuthDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateEventViewModel viewModel)
        {
            var newEvent = new Event
            {
                EventName = viewModel.EventName,
                EventDescription = viewModel.EventDescription,
                Initiator = viewModel.Initiator,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                CreatedDate = DateTime.Now
            };
            await _dbContext.Events.AddAsync(newEvent);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Event");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var events = await _dbContext.Events.ToListAsync();
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var eventToDelete = await _dbContext.Events.AsNoTracking().FirstOrDefaultAsync(x => x.EventId == id);
            if (eventToDelete is not null)
            {
                _dbContext.Events.Remove(eventToDelete);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Event");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eventToEdit = await _dbContext.Events.FindAsync(id);
            return View(eventToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Event viewModel)
        {
            var existingEvent = await _dbContext.Events.FindAsync(viewModel.EventId);
            if (existingEvent is not null)
            {
                existingEvent.EventName = viewModel.EventName;
                existingEvent.EventDescription = viewModel.EventDescription;
                existingEvent.Initiator = viewModel.Initiator;
                existingEvent.StartTime = viewModel.StartTime;
                existingEvent.EndTime = viewModel.EndTime;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Event");
        }
    }
}
