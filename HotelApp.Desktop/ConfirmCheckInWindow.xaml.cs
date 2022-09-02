using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using System.Windows;

namespace HotelApp.Desktop
{   
    //Chech In Confirmation Window
    public partial class ConfirmCheckInWindow : Window
    {
        private readonly IDatabaseData _db;
        private BookingFullModel _guestInfo = null;        
        public ConfirmCheckInWindow(IDatabaseData db)
        {
            InitializeComponent();
            _db = db;

        }

        // Filling the information about the Guest from DB 
        public void PopulateChechInInfo(BookingFullModel guestInfo)
        {
            _guestInfo = guestInfo;

            firstNameText.Text = _guestInfo!.FirstName;
            lastNameText.Text = _guestInfo.LastName;
            titleText.Text = _guestInfo.Title;
            roomNumberText.Text = _guestInfo.RoomNumber;
            totalCost.Text = string.Format("€{0:N2}", _guestInfo.TotalCost);
        }      

        //Close Check in choosen Guest and close the form
        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            _db.CheckInGuest(_guestInfo.Id);
            this.Close();
        }
    }
}
