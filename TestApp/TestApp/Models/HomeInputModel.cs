using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestApp.ModelBinders;

namespace TestApp.Models
{
	public class HomeInputModel
	{
		public string Name { get; set; }

		[ModelBinder(BinderType = typeof(DateToYearModelBinder))]
		[DataType(DataType.Date)]
		public int Year { get; set; }
	}
}
