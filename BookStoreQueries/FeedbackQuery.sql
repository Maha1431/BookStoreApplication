create table Feedback
(
         FeedbackId int primary key   identity (1,1),
		 userId INT NOT NULL
		 FOREIGN KEY (userId) REFERENCES Reguser(userId),
	     BookId INT NULL
		 FOREIGN KEY (BookId) REFERENCES Books(BookId),
		 FeedBackUserName varchar(50),
		 Comments Varchar(250),
		 Ratings float default 2		 
);
select * from Feedback
-----------------------------------------------------------------------
-------------------------CreateFeedbacktSP---------------------------------
Alter procedure Sp_AddFeedback(
	@userId INT,
	@BookId INT,
	@FeedbackUserName varchar(50),
	@Comments Varchar(200),
	@Ratings float)		
As 
Begin
				Begin try
				Begin transaction
					Insert into Feedback (userId,BookId,FeedBackUserName,Comments,Ratings )
						values (@userId,@BookId,@FeedbackUserName,@Comments,@Ratings);
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		
End
----------------------------------------------------------------------------------------------------
create PROC sp_GetFeedbacksbyBookId
	@BookId INT
AS
BEGIN try
  select * from Feedback where BookId = @BookId;
End try
Begin Catch
	SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage
END catch