using StockControl.Application.Requests.Contact;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappers;

public class ContactMapper
{
    public static Contact ToEntity(ContactPostRequest request, Supplier supplier)
    {
        return new Contact(request.ContactType, request.ContactName, request.Ddd, request.Number, null,
            supplier.Id);
    }

    public static ContactGetRequest ToGetRequest(Contact contact, string supplierBusinessName)
    {
        return new ContactGetRequest(contact.Id, contact.ContactType, contact.ContactName, contact.Ddd, contact.Number,
            supplierBusinessName);
    }
}