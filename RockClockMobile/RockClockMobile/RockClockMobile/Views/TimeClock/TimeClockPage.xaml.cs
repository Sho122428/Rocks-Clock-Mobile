using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System;
using Syncfusion.SfPicker.XForms;
using System.Collections.ObjectModel;
using RockClockMobile.ViewModels.Onboarding;
using RockClockMobile.Custom;

namespace RockClockMobile.Views.Onboarding
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnBoardingAnimationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnBoardingAnimationPage" /> class.
        /// </summary>
        /// 

        public OnBoardingAnimationPage()
        {
            InitializeComponent();
            
            
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );

                return true;
                
            });
         }

        

        private void ProjectButton_Clicked(object sender, EventArgs e)
        {
            picker.IsOpen = true;
        }

        private void picker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            
            //Picker p = sender as Picker;
            //var selectedProject = picker.SelectedItem;
            var project = "Clock in to " + picker.SelectedItem;
            if (project.Length > 25)
            {
                //NextButtonText.FontSize = 10;
                //NextButtonText.VerticalOptions = LayoutOptions.StartAndExpand;
                //project = "Clock in to " + System.Environment.NewLine + picker.SelectedItem;
                project = "Clock In";
            }
            ClockInBtn.Text = project;
        }

        
    }
}