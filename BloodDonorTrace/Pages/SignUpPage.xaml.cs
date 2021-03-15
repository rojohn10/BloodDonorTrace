using BloodDonorTrace.Services;
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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.RegisterUser(EntEmail.Text, EntPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                await DisplayAlert("Alert!", "Something wrong...", "Cancel");
            }
            else
            {
                await DisplayAlert("Hi!", "Your account has been created", "Ok");
                await Navigation.PopToRootAsync(); //will take the user to the root of navigation
            }
        }
    }
}