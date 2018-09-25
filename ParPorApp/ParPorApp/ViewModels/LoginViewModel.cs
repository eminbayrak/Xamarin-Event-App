using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FormsToolkit;
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
                    var accesstoken = await ApiServices.LoginAsync(Email, Password);

                    if (string.IsNullOrWhiteSpace(Email))
                    {
                        MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                        {
                            Title = "Sign in Information",
                            Message = "Email cannot be empty!",
                            Cancel = "OK"
                        });
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                        {
                            Title = "Sign in Information",
                            Message = "Password cannot be empty!",
                            Cancel = "OK"
                        });
                        return;
                    }
                    try
                    {
                        IsBusy = true;
                        
                        #if DEBUG
                        await Task.Delay(1000);
                        #endif
                        if (!string.IsNullOrEmpty(accesstoken))
                        {
                            
                                Settings.Email = Email;
                                Settings.Password = Password;
                                Settings.AccessToken = accesstoken;
                                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(), true);
                            
                            //IsBusy = true;

                        }
                        else
                        {
                            await UserDialogs.Instance.AlertAsync(string.Format("Uh Oh! Please check your login info...", 3000)); //Use ShowImage instead

                            //IsBusy = false;
                            //await UserDialogs.Instance.AlertAsync(string.Format("Wrong email or password :("));
                            //await Application.Current.MainPage.DisplayAlert("Error", "Wrong username or password", "Dismiss");

                        }
                    }
                    catch (Exception ex)
                    {
                        MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                        {
                            Title = "Unable to Sign in",
                            Message = "The email or password provided is incorrect.",
                            Cancel = "OK"
                        });
                    }


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

