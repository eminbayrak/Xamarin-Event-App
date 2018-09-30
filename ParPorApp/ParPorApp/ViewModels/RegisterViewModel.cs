using System.Windows.Input;
using ParPorApp.Services;
using Xamarin.Forms;
using ParPorApp.Helpers;
using ParPorApp.Views;

namespace ParPorApp.ViewModels
{
    public class RegisterViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public string TeamName { get; set; }
        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isRegistered = await _apiServices.RegisterUserAsync
                        (Email, Password, ConfirmPassword, FirstName, LastName, TeamName);

                    Settings.Email = Email;
                    Settings.Password = Password;
                    Settings.FirstName = FirstName;
                    Settings.LastName = LastName;
                    Settings.TeamName = TeamName;

                    if (isRegistered)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage(), true);
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Cool, your account is created!");
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Something went wrong :(");
                    }
                });
            }
        }
    }
}
