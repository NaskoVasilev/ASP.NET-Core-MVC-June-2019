using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models
{
	public class Event
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		[Required]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 3)]
		public string Place { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		[Range(1, int.MaxValue)]
		public int TotalTickets { get; set; }

		[Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
		public decimal PricePerTicket { get; set; }
	}
}
