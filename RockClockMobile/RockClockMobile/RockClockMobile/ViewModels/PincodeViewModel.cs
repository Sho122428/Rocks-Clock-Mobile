using Xamarin.Forms;

namespace RockClockMobile.ViewModels
{
    public class PincodeViewModel
    {
        public bool ValidatePin(string pin)
        {
            if (pin != "123")
            {
                return false;
            }

            return true;
        }
    }
}
