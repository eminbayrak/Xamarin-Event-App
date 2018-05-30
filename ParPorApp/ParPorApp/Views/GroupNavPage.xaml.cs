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
    public partial class GroupNavPage : TabbedPage
    {
        public GroupNavPage ()
        {
            InitializeComponent();
            Children.Add(new GroupDetailPage() { Title="Detail"});
            Children.Add(new GroupMembersPage() { Title = "Members" });
        }
    }
}