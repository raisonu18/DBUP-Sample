 

IF  EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('SpName'))
BEGIN
	DROP Procedure SpName
END
GO



CREATE Procedure SpName
as
Select * from Article
GO