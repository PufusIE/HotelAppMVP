using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Web.Pages
{
    public class SearchRoomModel : PageModel
    {
        private readonly IDatabaseData _db;
       
        //Id of the Room Type that has been selected
        [BindProperty(SupportsGet = true)]
        public RoomTypeModel RoomTypeId { get; set; }

        //First Date of booking
        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        //Last date of booking
        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        //Chech if user started the search
        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; } = false;

        //List of Avaliable Room Types on the specific date
        public static List<RoomTypeModel> AvaliableRoomTypes { get; set; }
        public SearchRoomModel(IDatabaseData db)
        {
            _db = db;
        }
        // Getting Avaliable Rooms using choosen dates
        public void OnGet()
        {
            if (SearchEnabled == true)
            {
                AvaliableRoomTypes = _db.GetAvailableRooms(StartDate, EndDate);
            }
        }

        //Sending the choosen information
        public IActionResult OnPost()
        {
            return RedirectToPage(new { SearchEnabled = true, StartDate = StartDate.ToString("yyyy-MM-dd"), EndDate = EndDate.ToString("yyyy-MM-dd") }) ;
        }
    }
}
