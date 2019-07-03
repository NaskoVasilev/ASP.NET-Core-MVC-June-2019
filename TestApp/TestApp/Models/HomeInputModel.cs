using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestApp.ModelBinders;

namespace TestApp.Models
{
	public class HomeInputModel : IValidatableObject
	{
		public string Name { get; set; }

		[ModelBinder(BinderType = typeof(DateToYearModelBinder))]
		[DataType(DataType.Date)]
		public int Year { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(string.IsNullOrEmpty(this.Name))
			{
				yield return new ValidationResult("Name is required");
			}
			if(this.Year < 2000 && Year > 2019)
			{
				yield return new ValidationResult("Year must be between 2000 nad 2019");
			}
		}
	}
}
