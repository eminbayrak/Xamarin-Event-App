using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.Services;
using ParPorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JoinTeamPage : ContentPage
    {
        private List<AccountGroups> _accountgroups;
        private readonly ApiServices _apiServices = new ApiServices();
        public List<AccountGroups> AccountGroups
        {
            get => _accountgroups;
            set
            {
                _accountgroups = value;
                OnPropertyChanged();
            }
        }
        public JoinTeamPage()
        {
            InitializeComponent();
            joinButton.IsVisible = false;
            emptyErrorMsg.IsVisible = false;
            tryAgainBtn.IsVisible = false;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntCode.Text)){
                emptyErrorMsg.IsVisible = true;
            }
            else
            {
                MyMethod();
            }
        }
        public async Task LoadData()
        {
            var accessToken = Settings.AccessToken;
            AccountGroups = await GetAccountGroupNameAsync(accessToken);
            var rtCode = AccountGroups.Where(x => x.TeamCode == EntCode.Text).ToList();
            if (rtCode.Count == 0)
            {
                await UserDialogs.Instance.AlertAsync("Check your code", "No team found", "Try again");
                var vUpdatedPage = new JoinTeamPage();
                Navigation.InsertPageBefore(vUpdatedPage, this);
                await Navigation.PopAsync();
            }
            else
            {
                var ex = string.Format("{0}", rtCode[0].TeamName);
                tmName.Text = "Team name: \n" + ex;
            }
        }

        void MyMethod()
        {
            UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
            LoadData().ContinueWith((task) => {
                UserDialogs.Instance.HideLoading();
            });
            joinButton.IsVisible = true;
            tmCodeButton.IsVisible = false;
            tryAgainBtn.IsVisible = true;
        }
        public async Task<List<AccountGroups>> GetAccountGroupNameAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/AccountGroups/");
            var group = JsonConvert.DeserializeObject<List<AccountGroups>>(json);
            return group;
        }

        private async void tryAgainBtn_ClickedAsync(object sender, EventArgs e)
        {
            var vUpdatedPage = new JoinTeamPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            await Navigation.PopAsync();
        }
    }
}