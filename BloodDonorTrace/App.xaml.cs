using BloodDonorTrace.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonorTrace
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new SignInPage());
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                MainPage = new NavigationPage(new HomePage());
            }
            else if (string.IsNullOrEmpty(Settings.UserName)&&string.IsNullOrEmpty(Settings.Password))  
            {
                MainPage = new NavigationPage(new SignInPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
