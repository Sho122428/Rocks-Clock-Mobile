using RockClockMobile.Custom;
using RockClockMobile.ViewModels.ResetPassword;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views.ResetPassword
{
    /// <summary>
    /// Page to reset old password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordPage" /> class.
        /// </summary>
        public ResetPasswordPage(int userId)
        {
            InitializeComponent();

            BindingContext = new ResetPasswordViewModel();
            var ResetPasswordVM = (ResetPasswordViewModel)this.BindingContext;

            ResetPasswordVM.UserId = userId;
            ImgConfirmPassword.Source = ImageSource.FromFile("passwordicon");
            ImgCurrentPassword.Source = ImageSource.FromFile("passwordicon");
            ImgNewPassword.Source = ImageSource.FromFile("passwordicon");

            CurrentPasswordEntry.Keyboard = Keyboard.Numeric;
            NewPasswordEntry.Keyboard = Keyboard.Numeric;
            ConfirmNewPasswordEntry.Keyboard = Keyboard.Numeric;
        }

        private void ConfirmNewPasswordTextLengthEvent(object sender, TextChangedEventArgs e)
        {           
        }

        private void NewPasswordTextLengthEvent(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            int entryLength = entry.Text.Length;

            if (entryLength > 4)
            {
                ToastPopup.ToastMessage("Pincode length should be 4 digit numbers.", false);
                BtnSubmit.IsEnabled = false;
            }
            else
            {
                BtnSubmit.IsEnabled = true;
            }
        }
    }
}