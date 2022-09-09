CREATE PROCEDURE [dbo].[spBookings_GetRecordsByLastName]
	@lastName nvarchar(50),
	@startDate date
	
AS
	begin
	set nocount on;
	select b.Id, b.RoomId, b.GuestId, b.StartDate, b.EndDate, b.CheckedIn,b.TotalCost, g.FirstName, g.LastName, r.Number as RoomNumber, r.RoomTypeId, rt.Title, rt.[Description], rt.Price
	from dbo.Bookings b
	inner join dbo.Guests g on b.GuestId = g.Id
	inner join dbo.Rooms r on b.RoomId = r.Id
	inner join dbo.RoomType rt on r.RoomTypeId = rt.Id
	where g.LastName = @lastName and b.StartDate = @startDate and b.CheckedIn = 0
	end
