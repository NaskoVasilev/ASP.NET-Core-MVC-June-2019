using Microsoft.AspNetCore.Identity;
using Panda.Models.Enums;
using System;

namespace Panda.Models
{
	public class Package
	{
		public Package()
		{
			Status = PackageStatus.Pending;
		}

		public int Id { get; set; }

		public string Description { get; set; }

		public decimal Weight { get; set; }

		public string ShippingAddress { get; set; }

		public PackageStatus Status { get; set; }

		public DateTime EstimatedDeliveryDate { get; set; }

		public Receipt Receipt { get; set; }

		public string RecipientId { get; set; }
		public IdentityUser Recipient { get; set; }
	}
}
