CREATE PROCEDURE [dbo].[spBookings_Isert]		
	@roomId int,
	@guestId int,
	@startDate date,
	@endDate date,
	@totalCost money
AS
	begin
	set nocount on;

	insert into dbo.Bookings (RoomId, GuestId, StartDate, EndDate, TotalCost)
	values (@roomId, @guestId, @startDate, @endDate, @totalCost);

	end
