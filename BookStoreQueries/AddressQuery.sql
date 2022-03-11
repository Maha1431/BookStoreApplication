create table AddressType
(
	TypeId int  PRIMARY KEY  IDENTITY(1,1),
	Type varchar(20)
);
INSERT INTO AddressType (Type) VALUES ('Home')
INSERT INTO AddressType (Type) VALUES ('Work')
INSERT INTO AddressType (Type) VALUES ('Other')

select * from AddressType;

create table Address
(
    AddressId int  PRIMARY KEY  IDENTITY(1,1),
	userId INT 
	FOREIGN KEY (userId) REFERENCES Reguser(userId),
	Address varchar(200) not null,
	City varchar(100),
	State varchar(100),
	TypeId int
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId)
);

select * from Address
------------------------------------------------------------------------------------------------------------
create procedure Sp_AddAddress(
		@userId int,
        @Address varchar(600),
		@City varchar(50),
		@State varchar(50),
		@TypeId int	)		
As 
Begin
	IF (EXISTS(SELECT * FROM RegUser WHERE @userId = @userId))
	Begin
	Insert into Address (userId,Address,City,State,TypeId )
		values (@UserId,@Address,@City,@State,@TypeId);
	End
	Else
	Begin
		Select 1
	End
End
Exec Sp_AddAddress
1,'kkNagar','Chennai','TamilNadu',2
------------------------------------------------------------------------------------------------------------
create PROCEDURE sp_UpdateAddress
(
@AddressId int,
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int	)

AS
BEGIN try
UPDATE Address SET 
			Address= @Address, 
			City = @City,
			State=@State,
			TypeId=@TypeId 
		   WHERE AddressId=@AddressID;
end try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch
Exec sp_UpdateAddress
1,'kkNagar','Chennai','TamilNadu',1
-----------------------------------------------------------------------------------
create Proc sp_DeleteAddress
@AddressId int
as
begin try
delete Address where AddressId=@AddressId
end try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch
Exec sp_DeleteAddress
3
select * from Address
-------------------------------------------------------------------------------------------
create PROCEDURE sp_GetAllAddresses
AS
BEGIN
	 begin
	   SELECT * FROM Address ;
   	 end
End

Exec sp_GetAllAddresses
-------------------------------------------------------------------------------------------------------------
alter PROCEDURE sp_GetAddressbyuserId
  @userId int
AS
BEGIN

     IF(EXISTS(SELECT * FROM Address WHERE userId=@userId))
	 begin
	   SELECT * FROM Address WHERE userId=@userId;
   	 end
	 else
	 begin
		select 1
	end
End

Exec sp_GetAddressbyUserid 1
