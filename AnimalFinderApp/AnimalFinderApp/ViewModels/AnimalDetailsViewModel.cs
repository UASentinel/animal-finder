using AnimalFinderApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kotlin.Text;
using System.Diagnostics;

namespace AnimalFinderApp.ViewModels;

[QueryProperty("Animal", "Animal")]
public partial class AnimalDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Animal animal;
    private readonly IMap _map;

    public AnimalDetailsViewModel(IMap map)
    {
        Title = "Animal Details";
        _map = map;
    }

    [RelayCommand]
    async Task OpenMapAsync()
    {
        try
        {
            await _map.OpenAsync(Animal.Latitude, Animal.Longitude,
                new MapLaunchOptions
                {
                    Name = Animal.Name,
                    NavigationMode = NavigationMode.None
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to open map.", "OK");
        }
        finally
        {

        }
    }
}
