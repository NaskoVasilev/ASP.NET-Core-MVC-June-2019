using System.Collections.Generic;
using System.Linq;
using Eventures.Data;
using Eventures.InputModels.Event;
using Eventures.Models;
using Eventures.Services.Mappings.Extensions;
using Eventures.ViewModels.Event;

namespace Eventures.Services
{
	public class EventService : IEventService
	{
		private readonly ApplicationDbContext context;

		public EventService(ApplicationDbContext context)
		{
			this.context = context;
		}

		public void Add(EventCreateInputModel eventCreateInput)
		{
			Event validEvent = eventCreateInput.To<Event>();
			context.Events.Add(validEvent);
			context.SaveChanges();
		}

		public IEnumerable<EventViewModel> All()
		{
			return context.Events.To<EventViewModel>().ToList();
		}
	}
}
