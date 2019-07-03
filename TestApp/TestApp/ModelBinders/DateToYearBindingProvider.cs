using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TestApp.ModelBinders
{
	public class DateToYearBindingProvider : IModelBinderProvider
	{
		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if(context.Metadata.ModelType == typeof(int) && context.Metadata.Name == "Year")
			{
				return new DateToYearModelBinder();
			}

			return null;
		}
	}
}
