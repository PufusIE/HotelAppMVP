using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Data
{
    public class SqlData : IDatabaseData
    {
        private readonly ISQLDataAccess _db;
        private readonly string connectionString = "SqlDb"; 
        public SqlData(ISQLDataAccess db)
        {
            _db = db;
        }

        // Get available rooms from DB based on selected date
        public List<RoomTypeModel> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            string sql = "dbo.spRoomTypes_GetAllAvailableRoomTypes";
            var output = _db.LoadData<RoomTypeModel, dynamic>(sql, new { startDate, endDate }, connectionString, true);
            return output;
        }

       //  Add new record to DB
        public void BookGuest(string firstName,
                              string lastName,
                              DateTime startDate,
                              DateTime endDate,
                              int roomTypeId)
        {

            GuestModel guest = _db.LoadData<GuestModel, dynamic>("dbo.spGuest_Isert",
                                                                 new { firstName, lastName },
                                                                 connectionString,
                                                                 true).First();

            RoomTypeModel roomType = _db.LoadData<RoomTypeModel, dynamic>("select * from dbo.RoomType where Id = @Id",
                                                                          new { Id = roomTypeId },
                                                                          connectionString,
                                                                          false).First();

            TimeSpan stayTime = endDate.Date.Subtract(startDate.Date);

            List<RoomsModel> availableRooms = _db.LoadData<RoomsModel, dynamic>("dbo.spRooms_GetAvailableRooms",
                                                                                new { startDate, endDate, roomTypeId },
                                                                                connectionString,
                                                                                true);

            _db.SaveData("dbo.spBookings_Isert",
                         new
                         {
                             roomId = availableRooms.First().Id,
                             guestId = guest.Id,
                             startDate,
                             endDate,
                             totalCost = stayTime.Days * roomType.Price
                         },
                         connectionString,
                         true);
        }

        // Search a guest from DB by his Last Name
        public List<BookingFullModel> SearchBookings(string lastName)
        {
            var output = _db.LoadData<BookingFullModel, dynamic>("dbo.spBookings_GetRecordsByLastName",
                                                           new { lastName, startDate = DateTime.Now.Date },
                                                           connectionString,
                                                           true);
            return output;
        }

        //Change the Checked In status of the customer
        public void CheckInGuest(int id)
        {
            string sql = "update dbo.Bookings set CheckedIn = 1 where Id = @id";
            _db.SaveData(sql, new { Id = id }, connectionString, false);
        }

        //Get the Room Type from DB by id
        public RoomTypeModel GetRoomTypeById(int id)
        {
            string sql = "select Id,Title,[Description],Price from dbo.RoomType where Id = @id";
            return _db.LoadData<RoomTypeModel, dynamic>(sql, new { id }, connectionString, false).First();
        }
    }
}
