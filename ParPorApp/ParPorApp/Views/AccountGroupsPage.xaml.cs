using ParPorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountGroupsPage : ContentPage
	{
        AccountGroupsViewModel accountGroups;
		public AccountGroupsPage ()
		{
			InitializeComponent ();
            BindingContext = accountGroups = new AccountGroupsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            accountGroups.GetGroupsCommand.Execute(null);
        }
    }
}