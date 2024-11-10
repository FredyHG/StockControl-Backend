using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using StockControl.Application.UseCases;
using StockControl.Domain.Interfaces;
using StockControl.Domain.Repository;
using StockControl.Domain.Services;
using StockControl.Infrastructure.Data;
using StockControl.Infrastructure.Repository;
using StockControl.Infrastructure.Services;
using StockControl.Presentation.Controllers.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

//Interfaces
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IStockHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStockHistoryService, StockHistoryService>();

//Dependency Inject
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<ProductService>();

//UseCases
builder.Services.AddScoped<RegisterSupplierUseCase>();
builder.Services.AddScoped<ListAllSuppliersUseCase>();
builder.Services.AddScoped<ListAllProductsUseCase>();
builder.Services.AddScoped<DeleteContactByIdAndSupplierIdUseCase>();
builder.Services.AddScoped<FindContactByIdUseCase>();
builder.Services.AddScoped<FindAllProductsBySupplierCnpjUseCase>();
builder.Services.AddScoped<DeleteProductBySkuCodeUseCase>();
builder.Services.AddScoped<DeleteProductsByListOfSkuCodesUseCase>();
builder.Services.AddScoped<RegisterProductUseCase>();
builder.Services.AddScoped<FindSupplierByCnpjUseCase>();
builder.Services.AddScoped<UpdateProductInfosUseCase>();
builder.Services.AddScoped<CheckProductStockUseCase>();
builder.Services.AddScoped<DeleteAddressByIdUseCase>();
builder.Services.AddScoped<AddAddressInSupplierUseCase>();
builder.Services.AddScoped<RegisterProductSellUseCase>();
builder.Services.AddScoped<RestockProductUseCase>();
builder.Services.AddScoped<SetStockLimitUseCase>();
builder.Services.AddScoped<ProductForecastUseCase>();
builder.Services.AddScoped<GetAllSalesHistoryCurrentYearUseCase>();
builder.Services.AddScoped<GetTopCategorySalesUseCase>();
builder.Services.AddScoped<ListAllCategoriesUseCase>();
builder.Services.AddScoped<ListAllStockHistoryUseCase>();
builder.Services.AddScoped<AddContactInSupplierUseCase>();
builder.Services.AddScoped<DeleteSuppliersByListOfCnpjUseCase>();


//Database Config
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    if (builder.Environment.IsDevelopment()) options.EnableSensitiveDataLogging();
});


// Load RabbitMQ settings from appsettings.json
var mqSettings = builder.Configuration.GetSection("RabbitMQ");
var factory = new ConnectionFactory
{
    HostName = mqSettings["HostName"],
    Port = int.Parse(mqSettings["Port"]),
    UserName = mqSettings["UserName"],
    Password = mqSettings["Password"]
};

var exchangeName = mqSettings["ExchangeName"];
var queueName = mqSettings["QueueName"];

// Register services
builder.Services.AddSingleton(factory);
builder.Services.AddSingleton<IMqService>(new MqService(factory, exchangeName, queueName));


//Validators
builder.Services.AddValidatorsFromAssemblyContaining<SupplierRequestValidation>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();