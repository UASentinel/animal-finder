using AnimalFinderApp.Models;
using System.Net.Http.Json;

namespace AnimalFinderApp.Services;

public class AnimalService
{
    private readonly HttpClient _httpClient;
    private readonly IConnectivity _connectivity;

    public AnimalService(IConnectivity connectivity)
    {
        _httpClient = new HttpClient();
        _connectivity = connectivity;
    }

    public async Task<List<Animal>> GetAnimals()
    {

        var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

        var response = await _httpClient.GetAsync(url);
        if(response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<Animal>>();

        return new List<Animal>();
    }
}
