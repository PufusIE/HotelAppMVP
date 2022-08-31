using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
