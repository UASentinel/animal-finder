using AnimalFinderApp.Views;

namespace AnimalFinderApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AnimalDetails), typeof(AnimalDetails));
        }
    }
}
