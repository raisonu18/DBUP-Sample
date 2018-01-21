 


IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
						 AND  TABLE_NAME = 'TableName'))
BEGIN
  Create Table SampleTable
   (
	 Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	 DateCreated DATETIME  DEFAULT GetDATE() NOT NULL,
	 EmailAddress varchar (50)	 
   )
END  
