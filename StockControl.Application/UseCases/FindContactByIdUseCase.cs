using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class FindContactByIdUseCase(IContactService contactService)
{
    public async Task<Contact> Execute(string contactId)
    {
        return await contactService.FindContactByIdAsync(contactId);
    }
}