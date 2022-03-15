create Table Admin
 (
 AdminId int primary key identity(1,1),
  userId INT NOT NULL,
  FOREIGN KEY (userId) REFERENCES RegUser(userId),
  AdminName varchar(50),
  Email varchar(100),
  password varchar(100)
  )
  select * from Admin
  select * from RegUser
---------------------------------------------------------------------------------------------------------
create proc sp_AddAdmin
(
@userId int,
@AdminName varchar(100),
@Email varchar(50),
@password varchar(100)
)
as 
begin try
 Insert into Admin (userId, AdminName, Email, password)
  values(@userId,@AdminName, @Email, @password)
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
create proc sp_UpdateAdminbyAdminId
(
@AdminId int,
@AdminName varchar(50),
@Email varchar(50),
@password varchar(50))
as
begin try
Update Admin set AdminName=@AdminName, Email=@Email, password=@password
 where AdminId=@AdminId
end try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch

Exec sp_UpdateAdminbyAdminId
1
--------------------------------------------------------------------------------------------
create proc sp_GetAllAdminbyAdminId
(
@AdminId int)
as
begin
 IF(EXISTS(SELECT * FROM Admin WHERE AdminId=@AdminId))
	 begin
	   SELECT * FROM Admin WHERE AdminId=@AdminId;
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