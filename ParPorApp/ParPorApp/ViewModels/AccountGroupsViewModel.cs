using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ParPorApp.ViewModels
{
    class AccountGroupsViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiServices = new ApiServices();
        private List<AccountGroups> _accountgroups;
        public List<AccountGroups> AccountGroups
        {
            get => _accountgroups;
            set
            {
                _accountgroups = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetGroupsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    AccountGroups = await _apiServices.GetAccountGroupsAsync(accessToken);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
