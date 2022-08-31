/*
 Pre-Deployment Script Template							
That populates RoomTypes with initial records
*/
if not exists (select 1 from dbo.RoomType where Title = 'King' or price = 100)
begin
insert into dbo.RoomType (Title, [Description], Price)
values ('King Size Bed', 'A rom with a king-size bed', 125)
,('Double King', 'A rom with a two king-size beds', 170)
,('Queen Size Bed', 'A rom with a queen-size bed', 100)
,('Double Queen', 'A rom with a two queen-size beds', 150)
end

if not exists (select 1 from dbo.Rooms where Number = 101 or RoomTypeId = 1)
begin

declare @roomId1 int;
declare @roomId2 int;
declare @roomId3 int;
declare @roomId4 int;
select @roomId1 = Id from dbo.RoomType where Title = 'King Size Bed';
select @roomId2 = Id from dbo.RoomType where Title = 'Double King';
select @roomId3 = Id from dbo.RoomType where Title = 'Queen Size Bed';
select @roomId4 = Id from dbo.RoomType where Title = 'Double Queen';

insert into dbo.Rooms (Number, RoomTypeId)
values (101, @roomId1),(102, @roomId2), (103, @roomId3), (104, @roomId4),(201, @roomId1),(202, @roomId2), (203, @roomId3), (204, @roomId4)

end
