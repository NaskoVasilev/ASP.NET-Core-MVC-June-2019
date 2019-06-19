using Panda.Models.Enums;

namespace Panda.ViewModels.Package
{
	public class PackageViewModel
	{
		public int Id { get; set; }

		public string Description { get; set; }

		public decimal Weight { get; set; }

		public string ShippingAddress { get; set; }

		public PackageStatus Status { get; set; }

		public string EstimatedDeliveryDate { get; set; }

		public string Recipient { get; set; }
	}
}
