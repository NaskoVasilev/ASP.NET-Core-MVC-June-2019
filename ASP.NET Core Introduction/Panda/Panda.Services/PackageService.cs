using Microsoft.EntityFrameworkCore;
using Panda.InputModels.Package;
using Panda.Models;
using Panda.Models.Enums;
using Panda.ViewModels.Package;
using Panda.ViewModels.Receipt;
using Panda.Web.Data;
using System;
using System.Globalization;
using System.Linq;

namespace Panda.Services
{
	public class PackageService : IPackageService
	{
		private const string DateFormat = "dd/MM/yyyy";
		private readonly PandaDbContext context;

		public PackageService(PandaDbContext context)
		{
			this.context = context;
		}

		public RecipientViewModel[] GetAllRecipients()
		{
			return context.Users
			   .Select(u => new RecipientViewModel
			   {
				   Id = u.Id,
				   Username = u.UserName
			   })
			   .ToArray();
		}

		public void Create(PackageCreateInputModel model)
		{
			Package package = new Package()
			{
				Description = model.Description,
				RecipientId = model.RecipientId,
				Weight = model.Weight,
				ShippingAddress = model.ShippingAddress
			};

			context.Packages.Add(package);
			context.SaveChanges();
		}

		public void SetDeliveyDate(int id)
		{
			Random random = new Random();
			int days = random.Next(20, 40);
			Package package = context.Packages.Find(id);
			package.EstimatedDeliveryDate = DateTime.Now.AddDays(days);
		}

		public PackageIndexViewModel[] GetPackagesByStatus(string statusName, string userId)
		{

			bool isValid = Enum.TryParse(statusName, out PackageStatus status);

			if(!isValid)
			{
				return new PackageIndexViewModel[0];
			}

			return context.Packages
				.Where(p => p.Status == status && p.RecipientId == userId)
				.Select(p => new PackageIndexViewModel()
				{
					Id = p.Id,
					Name = p.Description
				})
				.ToArray();
		}

		public PackageInfoViewModel[] PackagesByStatus(PackageStatus status)
		{
			return context.Packages
				.Include(x => x.Recipient)
				.Where(p => p.Status == status)
				.Select(p => new PackageInfoViewModel()
				{
					Id = p.Id,
					Description = p.Description,
					EstimatedDeliveryDate = p.EstimatedDeliveryDate,
					Weight = p.Weight,
					Recipient = p.Recipient.UserName,
					ShippingAddress = p.ShippingAddress
				})
				.ToArray();
		}

		public void GenerateReceipt(int id, int recipientId)
		{
			const decimal feeRate = 2.67M;
			Package package = context.Packages.Find(id);
			Receipt receipt = new Receipt()
			{
				RecipientId = package.RecipientId,
				PackageId = id,
				Fee = package.Weight * feeRate
			};

			context.Receipts.Add(receipt);
			context.SaveChanges();
		}

		public PackageViewModel GetById(int id)
		{
			return context.Packages
				.Where(x => x.Id == id)
				.Select(p => new PackageViewModel
				{
					EstimatedDeliveryDate = NornalizeDeliveryDate(p.EstimatedDeliveryDate, p.Status),
					Status = p.Status,
					Description = p.Description,
					Id = p.Id,
					Recipient = p.Recipient.UserName,
					ShippingAddress = p.ShippingAddress,
					Weight = p.Weight
				})
				.FirstOrDefault();
		}

		public void UpdateStatus(int id, PackageStatus newStatus)
		{
			Package package = context.Packages.Find(id);
			package.Status = newStatus;
			context.SaveChanges();
		}

		public string NornalizeDeliveryDate(DateTime deliveryDate, PackageStatus status)
		{
			if (status == PackageStatus.Pending)
			{
				return "N\\A";
			}
			else if (status == PackageStatus.Shipped)
			{
				return deliveryDate.ToString(DateFormat, CultureInfo.InvariantCulture);
			}
			else
			{
				return "Delivered";
			}
		}

		public PackageInfoViewModel[] GetDeliveredPackages()
		{
			return context.Packages
				.Where(p => p.Status == PackageStatus.Delivered || p.Status == PackageStatus.Acquired)
				.Select(p => new PackageInfoViewModel()
				{
					Id = p.Id,
					Description = p.Description,
					EstimatedDeliveryDate = p.EstimatedDeliveryDate,
					Weight = p.Weight,
					Recipient = p.Recipient.UserName,
					ShippingAddress = p.ShippingAddress,
				})
				.ToArray();
		}
	}
}
