using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class DeleteContactByIdAndSupplierIdUseCase(IContactService contactService)
{
    public async Task Execute(string contactId)
    {
        await contactService.DeleteContactByIdAsync(contactId);
    }
}