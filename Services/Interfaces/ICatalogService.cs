using DataClient.Models;

namespace DataClient.Services.Interfaces;

public interface ICatalogService
{
    Task<List<Product>> GetProducts();
}