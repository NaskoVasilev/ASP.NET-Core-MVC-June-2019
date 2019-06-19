using Panda.InputModels.Package;
using Panda.Models.Enums;
using Panda.ViewModels.Package;
using Panda.ViewModels.Receipt;
using System;

namespace Panda.Services
{
	public interface IPackageService
	{
		RecipientViewModel[] GetAllRecipients();

		void Create(PackageCreateInputModel model);

		void SetDeliveyDate(int id);

		PackageIndexViewModel[] GetPackagesByStatus(string statusName, string userId);

		PackageInfoViewModel[] PackagesByStatus(PackageStatus status);

		PackageInfoViewModel[] GetDeliveredPackages();

		void GenerateReceipt(int id, int recipientId);

		PackageViewModel GetById(int id);

		void UpdateStatus(int id, PackageStatus newStatus);

		string NornalizeDeliveryDate(DateTime deliveryDate, PackageStatus status);
	}
}
