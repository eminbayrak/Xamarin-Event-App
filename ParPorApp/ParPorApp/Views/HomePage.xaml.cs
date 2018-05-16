using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParPorApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();

            Children.Add(new FABPage() { Title = "Mon" });
            Children.Add(new FABPage() { Title = "Tue"});
            Children.Add(new FABPage() { Title = "Wed" });
            Children.Add(new FABPage() { Title = "Thu" });
            Children.Add(new FABPage() { Title = "Fri"});
            Children.Add(new FABPage() { Title = "Sat"});
            Children.Add(new FABPage() { Title = "Sun"});
            //NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}