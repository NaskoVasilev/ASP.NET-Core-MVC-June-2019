using Panda.ViewModels.Receipt;

namespace Panda.Services
{
	public interface IReceiptService
	{
		ReceiptViewModel[] GetRecepitsById(string id);

		ReceiptInfoViewModel GetReceiptById(int id);
	}
}
