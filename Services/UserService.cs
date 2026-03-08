using DataClient.Models;
using DataClient.Services.Interfaces;

namespace DataClient.Services;

public class UserService : IUserService
{
    private HttpClient _client;
    
    public UserService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("RemoteUserService");
    }
    
    public async Task<List<User>> GetUsers()
    {
        Console.WriteLine($"Writing base address: {_client.BaseAddress}");
        try
        {
            return await _client.GetFromJsonAsync<List<User>>("/user");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return new List<User>();
        }
    }

    public async Task<User?> GetUserById(Guid guid)
    {
        try
        {
            return await _client.GetFromJsonAsync<User>($"/user/{guid}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> CreateUser(UserDTO user)
    {
        try
        {
            var res = await _client.PostAsJsonAsync("/user", user);
            if (res.IsSuccessStatusCode)
                return true;
            return false;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> UpdateUser(UserDTO user, Guid guid)
    {
        try
        {
            var res = await _client.PutAsJsonAsync($"/user/{guid}", user);
            if (res.IsSuccessStatusCode)
                return true;
            return false;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> DeleteUser(Guid guid)
    {
        try
        {
            var res = await _client.DeleteAsync($"/user/{guid}");
            if (res.IsSuccessStatusCode)
                return  true;
            return false;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}