Create database BookStore;

create table RegUser
(
userId int Primary key  identity(1,1),
FullName varchar(50) Not null,
Email varchar(50) Not Null,
Password varchar (50)Not Null,
MobileNo int Not Null );
--------------------------------------------------------------------------------------
create proc sp_RegUser
( @FullName varchar(50),
@Email varchar(50) ,
@Password varchar (50),
@MobileNo int )
as
begin
Insert into RegUser values (@FullName,@Email, @Password,@MobileNo)
end
Exec sp_RegUser
'varunKumar', 'varun11@gmail.com', 'mHsjh45#', 7565452

select * from RegUser
-----------------------------------------------------------------------------------------
Create proc sp_UserLogin
(@Email varchar(50) ,
@Password varchar (50))
as
begin
SELECT Email, Password FROM RegUser WHERE @Email=Email AND @Password=Password
end
exec sp_UserLogin
'maha122@gmail.com','ahahs34$'
----------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE sp_ForgetPassword
(
	@Email varchar(50)
)
AS
BEGIN
	SELECT Email FROM RegUser WHERE @Email=Email 
END
exec sp_ForgetPassword
'maha122@gmail.com'
------------------------------------------------------------------------------------------
create procedure sp_ResetPassword
 (
    @Email varchar(50),
	@Password varchar(50)
)
 as
 begin
	 Update RegUser SET Password=@Password where Email=@Email
	 Select * from RegUser where Email=@Email; 
 End;
 Exec sp_ResetPassword
 'maha122@gmail.com','mm11'

 select * from RegUser

 