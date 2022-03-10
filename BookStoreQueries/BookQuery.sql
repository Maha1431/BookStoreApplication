Use BookStore;
Create Table Books(
    BookId int Identity(1,1) PRIMARY KEY,
	BookName varchar(200) not null,
	AuthorName varchar(200) not null,
    DiscountPrice money not null,   
	OriginalPrice  money not null,            
    BookDescription varchar(250),
    Rating float default 0,
    Reviewer int default 0 ,
	Image varchar(250),
	BookCount int not null
	);
	select * from Books
-----------------------------------------------------------------------------------------------------
Alter proc sp_AddBook
(   @BookName VARCHAR(100),    
    @AuthorName varchar(100),
    @DiscountPrice money ,   
	@OriginalPrice  money ,            
    @BookDescription varchar(200),
    @Rating float ,
    @Reviewer int  ,
	@Image varchar(250),
    @BookCount int 
)
as
begin try
Insert into Books (BookName,AuthorName,DiscountPrice,OriginalPrice,BookDescription,Rating,Reviewer,Image,BookCount)    
		Values (@BookName,@AuthorName,@DiscountPrice,@OriginalPrice,@BookDescription,@Rating,@Reviewer,@Image,@BookCount) 
end try
begin Catch
SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH  
Exec sp_AddBook
'Success Stories', 'William Shakespeare', 500, 900, 'Stories', 3.1, 0,'stories/url', 3
-------------------------------------------------------------------------------------------------------------------
Alter proc sp_BookUpdate
(   @BookId int,
    @BookName VARCHAR(100),    
    @AuthorName varchar(100),
    @DiscountPrice money ,   
	@OriginalPrice  money ,            
    @BookDescription varchar(200),
    @Rating float ,
    @Reviewer int  ,
	@Image varchar(250),
	@BookCount int    
)
as
begin try
update Books set 
			BookName= @BookName ,
			AuthorName=@AuthorName,
			DiscountPrice=@DiscountPrice,
			OriginalPrice=@OriginalPrice,
			BookDescription=@BookDescription,
			Rating=@Rating,
			Reviewer=@Reviewer,
			Image = @Image,
			BookCount=@BookCount			
			where BookId = @BookId;
end try
begin Catch
SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH
Exec sp_BookUpdate
'Success Stories', 'William Shakespeare', 500, 900, 'Stories', 3.1, 0, 3
--------------------------------------------------------------------------------------------------------------------
create proc sp_DeleteBook
(@BookId int)
as
begin try
delete Books where BookId=@BookId
end try
begin Catch
SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH
------------------------------------------------------------------------------------------------------------------------
Create Proc sp_GetAllBooks
as
Begin try
select * from Books
end try
begin Catch
SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH
----------------------------------------------------------------------------------------------------------------------------
create proc sp_GetAllBookByBookId
(@BookId int)
as
Begin try
select * from Books where BookId=@BookId
end try
begin Catch
SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH
