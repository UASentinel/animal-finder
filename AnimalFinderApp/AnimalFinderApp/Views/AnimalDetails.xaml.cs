using AnimalFinderApp.Models;
using AnimalFinderApp.ViewModels;

namespace AnimalFinderApp.Views;

public partial class AnimalDetails : ContentPage
{
	public AnimalDetails(AnimalDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}