using System;

namespace Panda.ViewModels.Package
{
	public class PackageInfoViewModel
	{
		public int Id { get; set; }

		public string Description { get; set; }

		public decimal Weight { get; set; }

		public string ShippingAddress { get; set; }

		public DateTime EstimatedDeliveryDate { get; set; }

		public string Recipient { get; set; }
	}
}
