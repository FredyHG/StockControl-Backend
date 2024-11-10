using Microsoft.Extensions.Logging;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using StockControl.Domain.Repository;

namespace StockControl.Domain.Services;

public class ContactService(IContactRepository contactRepository, ILogger<ContactService> logger) : IContactService
{
    public async Task<Contact> FindContactByIdAsync(string contactId)
    {
        var contact = await contactRepository.FindByAsync(c => c.Id == contactId);

        if (contact is null)
            throw new KeyNotFoundException();

        return contact;
    }

    public async Task DeleteContactByIdAsync(string contactId)
    {
        var contact = await FindContactByIdAsync(contactId);

        await contactRepository.DeleteAsync(contact);
    }

    public async Task AddContact(Supplier supplier, Contact contact)
    {
        logger.LogInformation("Starting AddContact method for supplier ID {SupplierId}", supplier.Id);

        if (supplier.Addresses.Count >= 3)
        {
            logger.LogWarning("Supplier ID {SupplierId} already has three contacts. Throwing exception.", supplier.Id);
            throw new ArgumentException("Supplier already contains three contacts");
        }

        await contactRepository.AddAsync(contact);

        logger.LogInformation("Contact successfully added to supplier ID {SupplierId}", supplier.Id);
    }
}