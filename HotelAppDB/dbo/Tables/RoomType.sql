CREATE TABLE [dbo].[RoomType]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Title] NVARCHAR(20) NOT NULL, 
    [Description] NVARCHAR(2000) NOT NULL, 
    [Price] MONEY NOT NULL 

)
