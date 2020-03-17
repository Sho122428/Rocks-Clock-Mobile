﻿using RockClockMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PincodePage : ContentPage
    {
        PincodeViewModel pincodeViewModel = new PincodeViewModel();
        public PincodePage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });

            NavigationPage.SetHasNavigationBar(this,false);
        }

        private async void BtnSignInEvent(object sender, EventArgs e)
        {
            string pin = EntryPin.Text;

            if (pin == "")
            {
                await DisplayAlert("Error", "Pincode is required.", "OK");
            }
            else {
                if (!pincodeViewModel.ValidatePin(pin))
                {
                    await DisplayAlert("Error", "Pincode is not registered.", "OK");
                }
                else
                {
                    await Navigation.PushModalAsync(new NavigationPage(new TimeClockPage()));
                }
            }                      
        }
    }
}