using StockControl.Domain.Dto;

namespace StockControl.Application.Requests.Product;

public class RegisterSellPostRequest
{
    public List<ProductSellDto> Products { get; set; }
}