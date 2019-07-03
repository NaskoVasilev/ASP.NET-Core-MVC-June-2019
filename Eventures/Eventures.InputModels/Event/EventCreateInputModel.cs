using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventures.InputModels.Event
{
	public class EventCreateInputModel : IValidatableObject
	{
		[Required]
		[StringLength(50, MinimumLength = 10)]
		public string Name { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 1)]
		public string Place { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		[Range(1, int.MaxValue)]
		public int TotalTickets { get; set; }

		[Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
		public decimal PricePerTicket { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(this.Start < DateTime.Now)
			{
				yield return new ValidationResult("Start date must be in the future.");
			}
			if (this.End < DateTime.Now)
			{
				yield return new ValidationResult("End date must be in the future.");
			}
			if (this.End <= this.Start)
			{
				yield return new ValidationResult("End date must after start date");
			}
		}
	}
}
