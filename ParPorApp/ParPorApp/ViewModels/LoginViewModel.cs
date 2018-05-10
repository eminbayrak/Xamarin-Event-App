using System;
using System.Threading.Tasks;
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
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accesstoken = await ApiServices.LoginAsync(Username, Password);
	                
                    if (!string.IsNullOrEmpty(accesstoken))
                    {
	                    //using (UserDialogs.Instance.Loading("Hang on...", null, null, true, MaskType.Clear))
	                    //{
		                    await Task.Delay(2000);
		                    Settings.Username = Username;
		                    Settings.Password = Password;
		                    Settings.AccessToken = accesstoken;
		                    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(), true);
						//}
						//IsBusy = true;
                        
                    }
                    else
                    {
	                    await UserDialogs.Instance.AlertAsync(string.Format("Uh Oh! Please check your login info...", 3000)); //Use ShowImage instead

						//IsBusy = false;
						//await UserDialogs.Instance.AlertAsync(string.Format("Wrong email or password :("));
						//await Application.Current.MainPage.DisplayAlert("Error", "Wrong username or password", "Dismiss");

					}
                    
                });
            }
        }


        private ApiServices ApiServices { get; set; } = new ApiServices();

        public LoginViewModel()
        {
	        if (string.IsNullOrEmpty(Settings.Username)) return;
	        Username = Settings.Username;
	        Settings.AccessToken = string.Empty;

        }
    }
}

