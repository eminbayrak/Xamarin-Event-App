using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParPorApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();

            Children.Add(new EventsPage() { Title = "All" });
            Children.Add(new GamesPage() { Title = "Games"});
            Children.Add(new TrainingPage() { Title = "Trainings" });
            //NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}