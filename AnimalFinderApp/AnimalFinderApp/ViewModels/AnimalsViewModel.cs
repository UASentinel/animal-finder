using AnimalFinderApp.Models;
using AnimalFinderApp.Services;
using AnimalFinderApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AnimalFinderApp.ViewModels;

public partial class AnimalsViewModel : BaseViewModel
{
    private readonly AnimalService _animalService;
    private readonly IConnectivity _connectivity;
    private readonly IGeolocation _geolocation;
    private readonly IMap _map;

    public ObservableCollection<Animal> Animals { get; } = new();

    public Command GetAnimalsCommand { get; }

    public AnimalsViewModel(AnimalService animalService, IConnectivity connectivity, IGeolocation geolocation, IMap map)
    {
        Title = "Animal Finder";
        _animalService = animalService;
        GetAnimalsCommand = new Command(async () => await GetAnimalsAsync());
        _connectivity = connectivity;
        _geolocation = geolocation;
        _map = map;
    }

    [ObservableProperty]
    private bool isRefreshing;

    [RelayCommand]
    async Task GetClosestAnymalAsync()
    {
        if (IsBusy || Animals.Count == 0)
            return;

        try
        {
            var location = await _geolocation.GetLastKnownLocationAsync();
            if(location is null)
            {
                location = await _geolocation.GetLocationAsync(
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
            }

            if (location is null)
                return;

            var closestAnimal = Animals.OrderBy(a => location.CalculateDistance(a.Latitude, a.Longitude, DistanceUnits.Kilometers)).First();

            if (closestAnimal is null)
                return;

            var distance = Math.Round(location.CalculateDistance(closestAnimal.Latitude, closestAnimal.Longitude, DistanceUnits.Kilometers));

            await Shell.Current.DisplayAlert("Closest animal",
                $"{closestAnimal.Name} is {distance} km away from you.",
                "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Unable to get closest animal. {ex.Message}", "OK");
        }
        finally
        {

        }
    }

    [RelayCommand]
    async Task GoToAnimalDetailsAsync(Animal animal)
    {
        if (animal is null) return;

        await Shell.Current.GoToAsync($"{nameof(AnimalDetails)}", true,
            new Dictionary<string, object>() { { "Animal", animal } });
    }

    async Task GetAnimalsAsync()
    {
        if(IsBusy) return;

        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Connection issue", $"Check your internet connection and try again.", "OK");
                return;
            }

            IsBusy = true;
            var animals = await _animalService.GetAnimals();

            if (Animals.Count != 0)
                Animals.Clear();

            foreach(var animal in animals)
                Animals.Add(animal);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to get animals.", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
}
