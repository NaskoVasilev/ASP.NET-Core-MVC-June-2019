using AutoMapper;
using Eventures.InputModels.Event;
using Eventures.Models;
using Eventures.ViewModels.Event;

namespace Eventures.Services.Mappings
{
	public class EventuresProfile : Profile
	{
		public EventuresProfile()
		{
			CreateMap<EventCreateInputModel, Event>();
			CreateMap<Event, EventViewModel>();
		}
	}
}
