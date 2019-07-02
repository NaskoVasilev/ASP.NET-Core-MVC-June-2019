using Eventures.InputModels.Event;
using Eventures.ViewModels.Event;
using System.Collections.Generic;

namespace Eventures.Services
{
	public interface IEventService
	{
		void Add(EventCreateInputModel eventCreateInput);

		IEnumerable<EventViewModel> All();
	}
}
