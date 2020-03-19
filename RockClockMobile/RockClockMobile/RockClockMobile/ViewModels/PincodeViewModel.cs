using Xamarin.Forms;

namespace RockClockMobile.ViewModels
{
    public class PincodeViewModel
    {
        public bool ValidatePin(int pin)
        {
            if (pin != 123)
            {
                return false;
            }

            return true;
        }
    }
}
