using StockControl.Domain.Entities;

namespace StockControl.Domain.Interfaces;

public interface IContactService
{
    Task<Contact> FindContactByIdAsync(string contactId);
    Task DeleteContactByIdAsync(string contactId);
    Task AddContact(Supplier supplier, Contact contact);
}