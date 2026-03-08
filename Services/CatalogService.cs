using DataClient.Models;
using DataClient.Services.Interfaces;

namespace DataClient.Services;

public class CatalogService : ICatalogService
{
    private HttpClient _client;

    public CatalogService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("RemoteCatalogService");
    }

    public async Task<List<Product>> GetProducts()
    {
        try
        {
            return await _client.GetFromJsonAsync<List<Product>>("catalog");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return new List<Product>();
        }
    }
}