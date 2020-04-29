using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views.LoginForm
{
    /// <summary>
    /// View used to show the email entry with validation status.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailEntry" /> class.
        /// </summary>
        /// 
       
        public EmailEntry()
        {
            InitializeComponent();
            ImgEmail.Source = ImageSource.FromFile("usericon.png");
        }


    }
}