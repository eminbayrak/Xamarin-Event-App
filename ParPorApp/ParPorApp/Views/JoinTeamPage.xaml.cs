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
        }
        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            var accessToken = Settings.AccessToken;
            AccountGroups = await GetAccountGroupNameAsync(accessToken);
            var rtCode = AccountGroups.Where(x => x.TeamCode == EntCode.Text).ToList();
            var ex = string.Format("{0}", rtCode[0].TeamName);
            tmName.Text = ex;
            
        }
        public async Task<List<AccountGroups>> GetAccountGroupNameAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/AccountGroups/");
            var group = JsonConvert.DeserializeObject<List<AccountGroups>>(json);
            return group;
        }
    }
}