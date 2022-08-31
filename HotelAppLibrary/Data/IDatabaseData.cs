using HotelAppLibrary.Models;

namespace HotelAppLibrary.Data
{
    // DB Modification Interface for Dependency Injection
    public interface IDatabaseData
    {
        void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId);
        void CheckInGuest(int id);
        List<RoomTypeModel> GetAvailableRooms(DateTime startDate, DateTime endDate);       
        RoomTypeModel GetRoomTypeById(int id);
        List<BookingFullModel> SearchBookings(string lastName);
    }
}