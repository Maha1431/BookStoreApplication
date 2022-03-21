create Table Admin
(
 AdminId int primary key identity(1,1),
 AdminName varchar(100),
  password varchar(50),
  Email varchar(100),
  )
------------------------------------------------------------------------------------------------
  create Table AdminAddBook
  (
  AdminId int 
  FOREIGN KEY(AdminId) References Admin (AdminId),
   BookId INT NOT NULL,
  FOREIGN KEY (BookId) REFERENCES Books(BookId),
  BookName varchar(200) not null,
	AuthorName varchar(200) not null,
    DiscountPrice money not null,   
	OriginalPrice  money not null,            
    BookDescription varchar(250),
    Rating float default 0,
    Reviewer int default 0 ,
	Image varchar(250),
	BookCount int not null
	)
-----------------------------------------------------------------------------------------------------
  select * from Admin
  select * from AdminAddBook
  select * from RegUser
---------------------------------------------------------------------------------------------------------

create proc sp_AddAdmin
(
@AdminName varchar(100),
@Email varchar(50),
@password varchar(100)
)
as 
begin try
 Insert into Admin (AdminName, Email, password)
  values(@AdminName, @Email, @password)
end try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch
------------------------------------------------------------------------------------
create proc sp_AddAdminforBook
(
    @AdminId int,
    @BookId int,
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
 Insert into AdminAddBook(AdminId,BookId,BookName,AuthorName,DiscountPrice,OriginalPrice,BookDescription,Rating,Reviewer,Image,BookCount)    
		Values (@AdminId,@BookId,@BookName,@AuthorName,@DiscountPrice,@OriginalPrice,@BookDescription,@Rating,@Reviewer,@Image,@BookCount) 
end try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch
-----------------------------------------------------------------------------------------
Alter proc sp_AdminUpdateBook
(
@AdminId int,
@BookId int,
@BookName varchar(20),
@BookCount int
)
as
begin try
Update AdminAddBook set BookName=@BookName, BookCount=@BookCount where BookId=@BookId and AdminId=@AdminId
end try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch

Exec sp_AdminUpdateBook
1, 1
--------------------------------------------------------------------------------------------
create proc sp_AdminDeleteBookbyBookId
(
@BookId int
)
as
begin 
delete AdminAddBook  where BookId=@BookId
end
----------------------------------------------------------------------------------------------------------------------
Create proc sp_AdminLogin
(@Email varchar(50) ,
@Password varchar (50))
as
begin
SELECT Email, Password FROM Admin WHERE @Email=Email AND @Password=Password
end
-----------------------------------------------------------------------------------------------------------------
create proc sp_AdminGetAllBooks
as
begin
 IF(EXISTS(SELECT * FROM AdminAddBook))
	 begin
	   SELECT * FROM AdminAddBook;
   	 end
	 else
	 begin
		print 'Not Available'
	end
End
---------------------------------------------------------------------------------------------
select * from RegUser;
select * from Books;
select * from Cart;
select * from Address;
select * from Admin
select * from AdminAddBook