using System;
using ParPorApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        UserViewModel usersViewModel;

        public UserProfilePage()
        {
            InitializeComponent();
            BindingContext = usersViewModel = new UserViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
            //var isvis = adminLbl.Text;
            //if (isvis == "False")
            //{
            //    addEventBtn.IsVisible = false;
            //}
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            usersViewModel.GetUserCommand.Execute(null);      
        }

        private async void addEventBtn_clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddEventPage());
        }

        private async void LogoutMenuItem_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new LoginPage());
        }
        private async void Profile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProfilePage());
        }
    }
}