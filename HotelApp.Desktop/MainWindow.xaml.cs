using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HotelApp.Desktop
{
    //Main window of desktop aplication 
    public partial class MainWindow : Window
    {
        private readonly IDatabaseData _db;  
        public MainWindow(IDatabaseData db)
        {            
            InitializeComponent();
            _db = db;
        }

        //Search for a Guest using his Last name
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            List<BookingFullModel> bookings = new List<BookingFullModel>();            
            bookings = _db.SearchBookings(lastName.Text);            
            
            guestList.ItemsSource = bookings;             
        }

        //Open the form to chech in the choosen Guest 
        //and send across the information about choosen Guest
        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {

           var model = (BookingFullModel)((Button)e.Source).DataContext;

           var checkInForm = App.AppHost!.Services.GetService<ConfirmCheckInWindow>();
           checkInForm!.PopulateChechInInfo(model);
           checkInForm!.Show();
            
        }
    }
}
