using System.ComponentModel.DataAnnotations;

namespace CarsApi.Data.Models
{
	public class Car
	{
		public int Id { get; set; }

		[Required]
		[MinLength(3), MaxLength(30)]
		public string Model { get; set; }

		[Range(typeof(decimal), "0.01", "100000000")]
		public decimal Price { get; set; }

		[Range(2000, 2019)]
		public int Year { get; set; }
	}
}
