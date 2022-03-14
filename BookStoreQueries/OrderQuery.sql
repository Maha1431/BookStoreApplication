----------------------OrderTable-------------------
create table Orders
(
         OrderId int  primary key identity (1,1),
		 userId INT NOT NULL,
		 FOREIGN KEY (userId) REFERENCES RegUser(userId),
		 AddressId int
		 FOREIGN KEY (AddressId) REFERENCES Address(AddressId),
	     BookId INT NOT NULL
		 FOREIGN KEY (BookId) REFERENCES Books(BookId),
		 TotalPrice int,
		 BookQuantity int,
		 OrderDate Date
);
select * from Orders
------------------------------------------------------------------------------------------------------------
Alter PROC sp_AddingOrders
	@userId INT,
	@AddressId int,
	@BookId INT ,
	@BookQuantity int,
	@TotalPrice int
AS
BEGIN
		Begin try
			Begin transaction			
				INSERT INTO Orders(userId,AddressId,BookId,TotalPrice,BookQuantity,OrderDate)
				VALUES ( @userId,@AddressId,@BookId,@BookQuantity*@TotalPrice,@BookQuantity,GETDATE())
				
			commit Transaction
		End try
		Begin catch
			Rollback transaction
		End catch
END
-------------------------------------------------------------------------------------------------------------------
-------------------get orders-----------------------
Alter PROC sp_GetAllOrdersbyuserId
	@userId INT
AS
BEGIN
	select 
		Books.BookId,
		Books.BookName,
		Books.AuthorName,
		Books.DiscountPrice,
		Books.OriginalPrice,
		Books.BookDescription,
		Books.Rating,
		Books.Reviewer,
		Books.Image,
		Books.BookCount,
		Orders.OrderId,
		Orders.userId,
		Orders.AddressId,
		Orders.BookId,
		Orders.TotalPrice,
		Orders.BookQuantity,
		Orders.OrderDate
		FROM Books
		inner join Orders
		on Orders.BookId=Books.BookId where Orders.userId=@userId
END
-----------------------------------------------------------------------------------------------------------
select * from RegUser
select * from Books
select * from Cart
select * from Address
select * from Wishlist
select * from Orders

delete Orders where OrderId=1