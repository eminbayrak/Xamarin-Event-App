using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.Services;
using Xamarin.Forms;

namespace ParPorApp.ViewModels
{
	public sealed class UserViewModel : INotifyPropertyChanged
	{
		private readonly ApiServices _apiServices = new ApiServices();
	    private List<User> _users;

	    public List<User> Users
		{
			get => _users;
			set
			{
				_users = value;
				OnPropertyChanged();
			}
		}
        public ICommand GetUserCommand
		{
			get
			{
				return new Command(async () =>
				{
					var accessToken = Settings.AccessToken;
					Users = await _apiServices.GetUsersAsync(accessToken);
				});
			}
		}
        
        public event PropertyChangedEventHandler PropertyChanged;

	    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
