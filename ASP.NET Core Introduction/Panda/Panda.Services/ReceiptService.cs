using Microsoft.EntityFrameworkCore;
using Panda.ViewModels.Receipt;
using Panda.Web.Data;
using System.Linq;

namespace Panda.Services
{
	public class ReceiptService : IReceiptService
	{
		private readonly PandaDbContext context;

		public ReceiptService(PandaDbContext context)
		{
			this.context = context;
		}

		public ReceiptViewModel[] GetRecepitsById(string id)
		{
			return context.Receipts
				.Include(r => r.Recipient)
				.Where(r => r.RecipientId == id)
				.Select(r => new ReceiptViewModel
				{
					Id = r.Id,
					Fee = r.Fee,
					IssuedOn = r.IssuedOn,
					Recipient = r.Recipient.UserName
				})
				.ToArray();
		}

		public ReceiptInfoViewModel GetReceiptById(int id)
		{
			var receipt = context
				.Receipts
				.Include(r => r.Recipient)
				.Include(r => r.Package)
				.FirstOrDefault(r => r.Id == id);

			var model = new ReceiptInfoViewModel()
			{
				Number = receipt.Id,
				Total = receipt.Fee,
				DeliveryAddress = receipt.Package.ShippingAddress,
				PackageWeight = receipt.Package.Weight,
				PackageDescription = receipt.Package.Description,
				Recipient = receipt.Recipient.UserName,
				IssuedOn = receipt.IssuedOn
			};

			return model;
		}
	}
}
