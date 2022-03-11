Use BookStore;

CREATE TABLE Cart
(
	CartId INT PRIMARY KEY IDENTITY(1,1) ,
	userId INT NOT NULL
	FOREIGN KEY (userId) REFERENCES RegUser(userId),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES Books(BookId),	
	OrderQuantity INT default 1
);
select * from Cart;
-------------------------------------------------------------------------------------------------------
Alter PROCEDURE sp_AddCart(
	@userId INT,
	@BookId INT,
	@OrderQuantity INT)
AS
BEGIN try
	
		INSERT INTO Cart( UserId,BookId,OrderQuantity)
		VALUES (@UserId,@BookId,@OrderQuantity)
end try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch

Exec sp_AddCart
2, 3, 4
---------------------------------------------------------------------------------------------------------------------
create Proc sp_UpdateCart(
@CartId INT,
@OrderQuantity INT
)
as
begin try
Update Cart set OrderQuantity=@OrderQuantity where CartId=@CartId
end try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch
-------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_DeleteCartDetails
	@CartID INT
AS
BEGIN
	IF EXISTS(SELECT * FROM Cart WHERE CartID = @CartID)
	BEGIN
		DELETE FROM Cart WHERE CartID = @CartID
	END
	ELSE
	BEGIN
		select 1
	END
END
Exec sp_DeleteCartDetails
1
-------------------------------------------------------------------------------------------------
Alter PROCEDURE sp_GetCartDetails
	@userId INT
AS
BEGIN
	SELECT
		Cart.CartId,
		Cart.userId,
		Cart.BookId,
		Cart.OrderQuantity,	
		Books.BookName,
		Books.AuthorName,
		Books.DiscountPrice,
		Books.OriginalPrice,
		Books.BookDescription,
		Books.Rating,
		Books.Reviewer,
		Books.Image,
		Books.BookCount
    FROM Cart
	Inner JOIN Books ON Cart.BookId = Books.BookId
	WHERE Cart.userId = @userId
END
Exec sp_GetCartDetails
1