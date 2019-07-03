using Eventures.Filters;
using Eventures.InputModels.Event;
using Eventures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
	public class EventController : Controller
	{
		private readonly IEventService eventService;

		public EventController(IEventService eventService)
		{
			this.eventService = eventService;
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			return View();
		}

		[TypeFilter(typeof(AdminActivityFilter))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Create(EventCreateInputModel model)
		{
			if (!ModelState.IsValid && model.Start < model.End)
			{
				return View(model);
			}

			eventService.Add(model);
			return RedirectToAction("All");
		}

		[Authorize]
		public IActionResult All()
		{
			var events = eventService.All();
			return View(events);
		}
	}
}
