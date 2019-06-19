using System.ComponentModel.DataAnnotations;

namespace Panda.InputModels.Package
{
	public class PackageCreateInputModel
	{
		public int Id { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }

		[Range(typeof(decimal), "0.01", " 79228162514264337593543950335")]
		public decimal Weight { get; set; }

		[Required]
		[MinLength(3), MaxLength(200)]
		public string ShippingAddress { get; set; }


		[Required]
		public string RecipientId { get; set; }
	}
}
