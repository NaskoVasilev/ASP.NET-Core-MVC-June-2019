using System;

namespace Panda.ViewModels.Receipt
{
	public class ReceiptInfoViewModel
	{
		public int Number { get; set; }

		public decimal Total { get; set; }

		public DateTime IssuedOn { get; set; }

		public string DeliveryAddress { get; set; }

		public decimal PackageWeight { get; set; }

		public string PackageDescription { get; set; }

		public string Recipient { get; set; }
	}
}
