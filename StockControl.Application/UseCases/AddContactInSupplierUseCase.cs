using StockControl.Application.Mappers;
using StockControl.Application.Requests.Contact;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class AddContactInSupplierUseCase(IContactService contactService, ISupplierService supplierService)
{
    public async Task Execute(ContactPostRequest request)
    {
        var supplier = await supplierService.FindSupplierByCnpj(request.SupplierCnpj);

        var contact = ContactMapper.ToEntity(request, supplier);

        await contactService.AddContact(supplier, contact);
    }
}