CREATE PROCEDURE [dbo].[spRoomTypes_GetAllAvailableRoomTypes]
	@startDate date,
	@endDate date
	AS
	begin
	set nocount on;

	select t.Id, t.Title, t.[Description], t.Price
	from dbo.Rooms r 
	inner join dbo.RoomType t on t.Id = r.RoomTypeId
	where r.Id not in 
	(
	select b.RoomId
	from dbo.Bookings b
	where (b.StartDate < @startDate and b.EndDate > @endDate)
	   or (b.StartDate <= @endDate and @endDate < b.EndDate)
	   or (b.StartDate <= @startDate and @startDate < b.EndDate)
	)
	group by t.Id, t.Title, t.[Description], t.Price	
	end
