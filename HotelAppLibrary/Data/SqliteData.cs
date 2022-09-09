using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;

namespace HotelAppLibrary.Data
{
    // Functionality is exact same except Stored Procedures has to be removed because of SQLite limitations 
    public class SqliteData : IDatabaseData
    {
        private readonly ISQLiteDataAccess _db;
        private readonly string connectionString = "SqliteDb";

        public SqliteData(ISQLiteDataAccess db)
        {
            _db = db;
        }

        // Get available rooms from DB based on selected date
        public List<RoomTypeModel> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            string sql = @"select t.Id, t.Title, t.[Description], t.Price
	                        from Rooms r 
	                        inner join RoomType t on t.Id = r.RoomTypeId
	                        where r.Id not in 
	                        (
	                        select b.RoomId
	                        from Bookings b
	                        where (b.StartDate < @startDate and b.EndDate > @endDate)
	                           or (b.StartDate <= @endDate and @endDate < b.EndDate)
	                           or (b.StartDate <= @startDate and @startDate < b.EndDate)
	                        )
	                        group by t.Id, t.Title, t.[Description], t.Price";
            var output = _db.LoadData<RoomTypeModel, dynamic>(sql, new { startDate, endDate }, connectionString);
            output.ForEach(x => x.Price = x.Price / 100);
            return output;
        }
       
        //  Add new record to DB
        public void BookGuest(string firstName,
                              string lastName,
                              DateTime startDate,
                              DateTime endDate,
                              int roomTypeId)
        {
            string  sql = @"select 1 from Guests where FirstName = @firstName and LastName = @lastName";
            int results = _db.LoadData<dynamic,dynamic>(sql, new { firstName, lastName }, connectionString).Count;

            if (results == 0)
            {
                sql = @"insert into Guests (FirstName, LastName) values (@firstName, @lastName)";
                _db.SaveData(sql, new { firstName, lastName }, connectionString);
            }

            sql = @"select *
	                        from Guests 
	                        where FirstName = @firstName and LastName = @lastName";
            GuestModel guest = _db.LoadData<GuestModel, dynamic>(sql,
                                                                 new { firstName, lastName },
                                                                 connectionString).First();

            RoomTypeModel roomType = _db.LoadData<RoomTypeModel, dynamic>("select * from RoomType where Id = @Id",
                                                                          new { Id = roomTypeId },
                                                                          connectionString).First();

            TimeSpan stayTime = endDate.Date.Subtract(startDate.Date);
            sql = @"select r.*
	                                from Rooms r 
	                                inner join RoomType t on t.Id = r.RoomTypeId
	                                where @roomTypeId = r.RoomTypeId and 
	                                r.Id not in (
	                                select b.RoomId
	                                from Bookings b
	                                where (b.StartDate < @startDate and b.EndDate > @endDate)
	                                   or (b.StartDate <= @endDate and @endDate < b.EndDate)
	                                   or (b.StartDate <= @startDate and @startDate < b.EndDate)
	                                  );";
            List<RoomsModel> availableRooms = _db.LoadData<RoomsModel, dynamic>(sql,
                                                                                new { startDate, endDate, roomTypeId },
                                                                                connectionString);
            sql = @"insert into Bookings (RoomId, GuestId, StartDate, EndDate, TotalCost)
	                values (@roomId, @guestId, @startDate, @endDate, @totalCost);";

            _db.SaveData(sql,
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
            string sql = @"	select b.Id, b.RoomId, b.GuestId, b.StartDate, b.EndDate, 
                            b.CheckedIn,b.TotalCost, g.FirstName, g.LastName, 
                            r.Number as RoomNumber, r.RoomTypeId, rt.Title, rt.[Description], rt.Price
	                        from Bookings b
	                        inner join Guests g on b.GuestId = g.Id
	                        inner join Rooms r on b.RoomId = r.Id
	                        inner join RoomType rt on r.RoomTypeId = rt.Id
	                        where g.LastName = @lastName and b.StartDate = @startDate and b.CheckedIn = 0";
            var output = _db.LoadData<BookingFullModel, dynamic>(sql,
                                                           new { lastName, startDate = DateTime.Now.Date },
                                                           connectionString);
            output.ForEach(x => { x.TotalCost = x.TotalCost / 100; x.Price = x.Price / 100; });
            return output;
        }

        //Change the Checked In status of the customer
        public void CheckInGuest(int id)
        {
            string sql = "update Bookings set CheckedIn = 1 where Id = @id";
            _db.SaveData(sql, new { Id = id }, connectionString, false);
        }

        //Get the Room Type from DB by id
        public RoomTypeModel GetRoomTypeById(int id)
        {
            string sql = "select Id,Title,[Description],Price from RoomType where Id = @id";
            return _db.LoadData<RoomTypeModel, dynamic>(sql, new { id }, connectionString).First();
        }
    }
} 