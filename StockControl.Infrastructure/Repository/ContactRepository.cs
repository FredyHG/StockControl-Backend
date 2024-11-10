using StockControl.Domain.Entities;
using StockControl.Domain.Repository;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repository;

public class ContactRepository(AppDbContext context) : BaseRepository<Contact>(context), IContactRepository
{
}