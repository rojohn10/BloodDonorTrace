using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonorTrace.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void TblLogout_Clicked(object sender, EventArgs e)
        {
            Settings.UserName = "";
            Settings.Password = "";
            Settings.AccessToken = "";
            Navigation.InsertPageBefore(new SignInPage(), this);
            Navigation.PopAsync();
        }

        private void TapFindBlood_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FindBloodPage());
        }

        private void TapRegisterBlood_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterBloodPage());
        }

        private void TapLatestDonors_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LatestDonorsPage());
        }

        private void TapHelp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }
    }
}