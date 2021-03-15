using BloodDonorTrace.Models;
using BloodDonorTrace.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonorTrace.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LatestDonorsPage : ContentPage
    {
        public ObservableCollection<BloodUser> BloodUsers;
        public LatestDonorsPage()
        {
            InitializeComponent();
            BloodUsers = new ObservableCollection<BloodUser>();
            FindBloodDonors();
        }

        private async void FindBloodDonors()
        {
            ApiServices apiServices = new ApiServices();
            var bloodUsers = await apiServices.LatestBloodUser();
            foreach (var blooduser in bloodUsers)
            {
                BloodUsers.Add(blooduser);
            }
            LvBloodDonors.ItemsSource = BloodUsers;
        }

        private void LvBloodDonors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedDonor = e.SelectedItem as BloodUser;
            
            if (selectedDonor != null)
            {
                Navigation.PushAsync(new DonorProfilePage(selectedDonor));
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}