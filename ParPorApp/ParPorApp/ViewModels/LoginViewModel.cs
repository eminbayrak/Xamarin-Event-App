using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Microsoft.Build.Framework;
using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.Services;
using ParPorApp.Views;
using Xamarin.Forms;


namespace ParPorApp.ViewModels
{
    public class LoginViewModel : Page
    {
        private readonly IFacebookManager _facebookManager;
        private readonly IGoogleManager _googleManager;
        public ICommand GoogleLoginCommand { get; set; }

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
	                    using (UserDialogs.Instance.Loading("You are in...", null, null, true, MaskType.Clear))
	                    {
		                    Settings.Username = Username;
		                    Settings.Password = Password;
		                    Settings.AccessToken = accesstoken;
		                    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(), true);
						}
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

        public ICommand LoginGoogleCommand
        {
            get
            {
                return new Command(async () => { await GoogleLogin(); });
            }
        }

        public ICommand LoginFacebookCommand
        {
            get
            {
                return new Command(async () => { await FacebookLogin(); });
            }
        }


        private ApiServices ApiServices { get; set; } = new ApiServices();

        public LoginViewModel()
        {
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();
            if (string.IsNullOrEmpty(Settings.Username)) return;
	        Username = Settings.Username;
	        Settings.AccessToken = string.Empty;
        }

        #region facebook

        private async Task FacebookLogin()
        {
            _facebookManager.Login(OnLoginCompleteFace);
        }

        private async void OnLoginCompleteFace(FacebookUser facebookUser, string message)
        {
            if (facebookUser != null)
            {
                using (UserDialogs.Instance.Loading("You are in...", null, null, true, MaskType.Clear))
                {
                    Settings.Username = facebookUser.FullName;
                    Settings.Password = "";
                    Settings.AccessToken = facebookUser.Token;
                    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(), true);
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync($"Error: {message}");
            }
        }

        #endregion


        #region google
        private async Task GoogleLogin()
        {
            _googleManager.Login(OnLoginComplete);
        }

        private async void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                using (UserDialogs.Instance.Loading("You are in...", null, null, true, MaskType.Clear))
                {
                    Settings.Username = googleUser.Name;
                    Settings.Password = "";
                    Settings.AccessToken = googleUser.Token;
                    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(), true);
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync($"Error: {message}"); 
            }
        }
        #endregion
    }
}

