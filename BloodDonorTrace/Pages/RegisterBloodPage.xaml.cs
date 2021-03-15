using BloodDonorTrace.Helpers;
using BloodDonorTrace.Models;
using BloodDonorTrace.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class RegisterBloodPage : ContentPage
    {
        public MediaFile file;

        public RegisterBloodPage()
        {
            InitializeComponent();
        }

        private async void TapOpenCamera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");
 
            ImgDonor.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            if (ImgDonor.ToString().Length <= 0)
            {
                await DisplayAlert("Alert!", "Please select your image", "Ok");
                return;
            }
            var imageArray = FilesHelper.ReadFully(file.GetStream());
            file.Dispose(); //dispose to prevent excessive memory flows
            var country = PickerCountry.Items[PickerCountry.SelectedIndex];
            var bloodGroup = PickerBloodGroup.Items[PickerBloodGroup.SelectedIndex];

            DateTime dateTime = DateTime.Now;
            int date = Convert.ToInt32(dateTime.ToOADate());

            var bloodUser = new BloodUser()
            {
                UserName = EntName.Text,
                Email = EntEmail.Text,
                Phone = EntPhone.Text,
                BloodGroup = bloodGroup,
                Country = country,
                ImageArray = imageArray,
                Date = date
            };


            ApiServices apiServices = new ApiServices();
            var response = await apiServices.RegisterDonor(bloodUser);
            if (!response)
            {
                await DisplayAlert("Alert!", "Something wrong..", "Cancel");
            }
            else
            {
                await DisplayAlert("Hi", "Your record has been added successfully!", "Alright");
                await Navigation.PushAsync(new HomePage());
                
            }
            
        }

        private void ValidateRegister()
        {
            if (string.IsNullOrWhiteSpace(EntEmail.Text) ||
                string.IsNullOrWhiteSpace(EntName.Text) ||
                string.IsNullOrWhiteSpace(EntPhone.Text) ||
                PickerBloodGroup.SelectedIndex <= 0 ||
                PickerCountry.SelectedIndex <= 0)             
            {
                BtnSubmit.IsEnabled = false;
                          
            }
            else
            {
                BtnSubmit.IsEnabled = true;
            }
        }

        private void EntName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateRegister();
        }

        private void EntEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateRegister();
        }

        private void EntPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateRegister();
        }

        private void PickerBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateRegister();
        }

        private void PickerCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateRegister();
        }
    }
}