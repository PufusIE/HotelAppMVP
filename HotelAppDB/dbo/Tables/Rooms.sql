CREATE TABLE [dbo].[Rooms]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [RoomTypeId] INT NOT NULL, 
    [Number] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Rooms_RoomType] FOREIGN KEY (RoomTypeId) REFERENCES RoomType(Id)

)
