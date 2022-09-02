using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Web.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly IDatabaseData _db;

        //First date of booking        
        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date )]
        public DateTime StartDate { get; set; }

        //Last date of booking
        [BindProperty(SupportsGet = true)]     
        [DataType(DataType.Date )]
        public DateTime EndDate { get; set; }

        //Id of the room and the Room Model instace 
        [BindProperty(SupportsGet = true)]        
        public int RoomTypeId { get; set; }
        public RoomTypeModel RoomType { get; set; }

        // Clients First name
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]       

        //CLients Last name
        public string LastName { get; set; }
        

        public BookRoomModel(IDatabaseData db)
        {
            _db = db;
        }
        
        public void OnGet()
        {
            if (RoomTypeId > 0)
            {
                //Carry on the information of choosen room type
                RoomType = _db.GetRoomTypeById(RoomTypeId);
            }
        }
        public IActionResult OnPost()
        {
            //Isert record into DB
            _db.BookGuest(FirstName, LastName, StartDate = StartDate, EndDate, RoomTypeId);
            return RedirectToPage("/Index");
        }
    }
}
