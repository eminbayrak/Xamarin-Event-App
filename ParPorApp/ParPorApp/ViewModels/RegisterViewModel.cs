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
        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isRegistered = await _apiServices.RegisterUserAsync
                        (Email, Password, ConfirmPassword, FirstName, LastName);

                    Settings.Email = Email;
                    Settings.Password = Password;
                    Settings.FirstName = FirstName;
                    Settings.LastName = LastName;
                    await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage(), true);
                });
            }
        }
    }
}
