using System.Windows.Input;
using ParPorApp.Services;
using Xamarin.Forms;
using ParPorApp.Helpers;
using ParPorApp.Views;
using System;
using ParPorApp.Models;
using System.Collections.Generic;

namespace ParPorApp.ViewModels
{
    public class RegisterViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();
        public User User { get; set; }
        public string Id => User.Id;
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public string TeamName { get; set; }
        public string EnteredCode { get; set; }
        public DateTime AccountDate => DateTime.Now;
        public string TeamCode => Math.Abs(TeamName.Replace(" ", "").GetHashCode()).ToString();
        public AccountGroups Account { get; set; }
        
        public ICommand JoinTeamCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isRegistered = await _apiServices.JoinTeamAsync(TeamName, EnteredCode);
                    Settings.TeamCode = EnteredCode;
                    Settings.TeamName = TeamName;
                    //Settings.Id = Id;
                    //accountGroups.EnteredCode = TeamCode;
                });
            }
        }

        public ICommand RegisterTeamCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isRegistered = await _apiServices.RegisterUserAsync
                        (Email, Password, ConfirmPassword, FirstName, LastName, TeamName, TeamCode, AccountDate);

                    Settings.Email = Email;
                    Settings.Password = Password;
                    Settings.FirstName = FirstName;
                    Settings.LastName = LastName;
                    Settings.TeamName = TeamName;
                    Settings.TeamCode = TeamCode;
                    Settings.AccountDate = AccountDate;

                    if (isRegistered)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage(), true);
                    }
                });
            }
        }
    }
}
