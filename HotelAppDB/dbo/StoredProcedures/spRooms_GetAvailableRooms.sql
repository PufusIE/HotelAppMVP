CREATE PROCEDURE [dbo].[spRooms_GetAvailableRooms]
	@startDate date,
	@endDate date,
	@roomTypeId int
as
	begin
	set nocount on;

	select r.*
	from dbo.Rooms r 
	inner join dbo.RoomType t on t.Id = r.RoomTypeId
	where @roomTypeId = r.RoomTypeId and 
	r.Id not in (
	select b.RoomId
	from dbo.Bookings b
	where (b.StartDate < @startDate and b.EndDate > @endDate)
	   or (b.StartDate <= @endDate and @endDate < b.EndDate)
	   or (b.StartDate <= @startDate and @startDate < b.EndDate)
	  );
	end