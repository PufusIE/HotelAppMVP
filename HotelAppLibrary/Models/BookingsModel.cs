namespace HotelAppLibrary.Models
{
    public class BookingsModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int PersonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckedIn { get; set; }
        public decimal TotalCost { get; set; }
    }
}
