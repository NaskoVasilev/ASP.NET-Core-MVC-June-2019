using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace TestApp.ModelBinders
{
	public class DateToYearModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			var dateAsString = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if(DateTime.TryParse(dateAsString.FirstValue, out DateTime date))
			{
				bindingContext.Result = ModelBindingResult.Success(date.Year);
			}
			else
			{
				bindingContext.Result = ModelBindingResult.Failed();
			}

			return Task.CompletedTask;
		}
	}
}
