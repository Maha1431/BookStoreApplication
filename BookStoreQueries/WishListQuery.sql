use BookStore;
create table WishList
(
	WishlistId INT  PRIMARY KEY IDENTITY(1,1),
	userId INT NOT NULL
	FOREIGN KEY (userId) REFERENCES RegUser(userId),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES Books(BookId)	
);
select * from WishList
-----------------------------------------------------------------------------------------
Alter PROCEDURE sp_AddWishlist
	@userId INT,
	@BookId INT
AS
BEGIN 
	IF EXISTS(SELECT * FROM Books WHERE BookId = @BookId)
	BEGIN
			INSERT INTO Wishlist(userId,BookId)
			VALUES ( @userId,@BookId)
	END
		ELSE
			print 'No Records';
	
END
Exec sp_AddWishlist
1, 2
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_RemoveWishlist
	@WishlistId INT
AS
BEGIN try
		DELETE FROM Wishlist WHERE WishlistId = @WishlistId
END try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch
---------------------------------------------------------------------------------------------
Alter PROCEDURE sp_GetWishlistbyuserId
  @userId int
AS
BEGIN
	   select 
		Books.BookId,
		Books.BookName,
		Books.AuthorName,
		Books.DiscountPrice,
		Books.OriginalPrice,
		Books.Rating,
		Books.Reviewer,
		Books.Image,
		Books.BookCount,
		Wishlist.WishlistId,
		Wishlist.userId,
		Wishlist.BookId
		FROM Books
		Inner join Wishlist
		on Wishlist.BookId=Books.BookId where Wishlist.userId=@userId
End
-----------------------------------------------------------------------------------------------------------
Create Proc sp_GetAllWishlist
as
begin Transaction

    Select * from Wishlist

SELECT @@TRANCOUNT AS OpenTransactions

commit Transaction

Exec sp_GetAllWishlist;

-------------------------------------------------------------------------------------------------------------
select * from RegUser
select * from Books
select * from Cart
select * from Address
select * from Wishlist