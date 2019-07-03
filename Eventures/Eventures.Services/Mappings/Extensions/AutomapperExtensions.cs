using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace Eventures.Services.Mappings.Extensions
{
	public static class AutomapperExtensions
	{
		public static TDestination To<TDestination>(this object source)
		{
			return (TDestination)Mapper.Map(source, source.GetType(), typeof(TDestination));
		}

		public static IQueryable<TDestination> To<TDestination>(this IQueryable<object> sources)
		{
			return sources.ProjectTo<TDestination>();
		}
	}
}
