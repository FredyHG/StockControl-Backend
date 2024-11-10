using StockControl.Domain.Entities;
using StockControl.Domain.Repository;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repository;

public class AddressRepository(AppDbContext context) : BaseRepository<Address>(context), IAddressRepository
{
}