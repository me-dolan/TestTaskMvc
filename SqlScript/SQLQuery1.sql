CREATE DATABASE TestTaskDb

USE TestTaskDb

CREATE TABLE [Provider](
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(MAX)
)
  CREATE TABLE [Order](
  	[Id] INT PRIMARY KEY IDENTITY,
  	[Number] NVARCHAR(MAX),
  	[Date] DATETIME2(7),
  	[ProviderId] INT,
  
  	CONSTRAINT FK_Order_Provider_ProviderId FOREIGN KEY (ProviderId)  REFERENCES [Provider] (Id)
  )
CREATE TABLE [OrderItem](
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(MAX),
	[Quantity] DECIMAL(18,3),
	[Unit] NVARCHAR(MAX),
	[OrderId] INT,

	CONSTRAINT FK_OrderItem_Order_OrderId FOREIGN KEY (OrderId)  REFERENCES [Order] (Id) ON DELETE CASCADE
)