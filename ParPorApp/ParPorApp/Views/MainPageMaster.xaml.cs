using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPageMaster : ContentPage
	{
		public ListView ListView;

		public MainPageMaster()
		{
			InitializeComponent();
            BindingContext = new MainPageMasterViewModel();
			ListView = MenuItemsListView;
		}

	    public class MainPageMasterViewModel : INotifyPropertyChanged
		{
			public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

			public MainPageMasterViewModel() => MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
			{
                new MainPageMenuItem { Id = 0, Title = "Profile", IconSource="ic_group.png", TargetType=typeof(UserProfilePage)},
				new MainPageMenuItem { Id = 1, Title = "Social", IconSource="ic_message.png", TargetType=typeof(SocialPage)},
				new MainPageMenuItem { Id = 2, Title = "Group", IconSource="ic_groups.png", TargetType=typeof(GroupPage) },
			    new MainPageMenuItem { Id = 2, Title = "Latest Events", IconSource="ic_notifications.png", TargetType=typeof(EventsPage) }
            });

			#region INotifyPropertyChanged Implementation
			public event PropertyChangedEventHandler PropertyChanged;
			void OnPropertyChanged([CallerMemberName] string propertyName = "")
			{
				if (PropertyChanged == null)
					return;

				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
			#endregion
		}

		private async void LogoutMenuItem_Clicked(object sender, EventArgs e)
		{

			await Navigation.PushAsync(new LoginPage());
		}

	    private async void Profile_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new ProfilePage());
	    }
	}
}