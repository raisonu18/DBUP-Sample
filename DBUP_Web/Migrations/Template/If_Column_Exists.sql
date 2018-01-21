 

  IF COL_LENGTH('TableName','ColumnName') IS NULL
 BEGIN	 
	  ALTER TABLE Article
	  ADD HeadSectionScripts ntext
 END
 