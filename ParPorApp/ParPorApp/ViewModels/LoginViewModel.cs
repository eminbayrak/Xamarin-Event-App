using System.Windows.Input;
using Acr.UserDialogs;
using Microsoft.Build.Framework;
using ParPorApp.Helpers;
using ParPorApp.Services;
using ParPorApp.Views;
using Xamarin.Forms;

namespace ParPorApp.ViewModels
{
    public class LoginViewModel : Page
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                    var accesstoken = await ApiServices.LoginAsync(Email, Password);                              
                    
                        if (!string.IsNullOrEmpty(accesstoken))
                        {
                            Settings.Email = Email;
                            Settings.Password = Password;
                            Settings.AccessToken = accesstoken;
                            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(), true);
                        }
                        else
                        {
                            await UserDialogs.Instance.AlertAsync(string.Format("Uh Oh! Please check your login info...", 3000)); //Use ShowImage instead
                        }
                        UserDialogs.Instance.HideLoading();
                });
            }
        }
        private ApiServices ApiServices { get; set; } = new ApiServices();

        public LoginViewModel()
        {
	        if (string.IsNullOrEmpty(Settings.Email)) return;
	        Email = Settings.Email;
	        Settings.AccessToken = string.Empty;

        }
    }
}

