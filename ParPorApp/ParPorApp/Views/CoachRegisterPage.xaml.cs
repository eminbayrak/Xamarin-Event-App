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
    public partial class CoachRegisterPage : ContentPage
    {
        public CoachRegisterPage()
        {
            InitializeComponent();
            GenerateGroupId();
            //Init();
        }

        //void Init()
        //{
        //    LogoIcon.HeightRequest = Constants.LogoIconHeight;
        //}

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());

        }
        //Generating random GroupId for the group, so group members can join the group by using this GroupId
        public string GenerateGroupId()
        {
            var id = Guid.NewGuid().ToString("N");
            return id;            
        }
        private async void ReturnWelcome_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
    }
}