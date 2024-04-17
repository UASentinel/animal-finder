using AnimalFinderApp.ViewModels;

namespace AnimalFinderApp.Views;

public partial class MainPage : ContentPage
{
	public MainPage(AnimalsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}