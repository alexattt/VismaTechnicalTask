USE [AppRecDB]
GO
/****** Object:  StoredProcedure [dbo].[AppRecErrorTopGlobal]    Script Date: 04/07/2022 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AppRecErrorTopGlobal] 
	@StartDate varchar(MAX),
	@EndDate varchar(MAX)
AS   

    SET NOCOUNT ON;  
	SELECT Errors.Err_DN, COUNT(Errors.Err_DN) AS ErrorCount FROM
	(SELECT dbo.AppRecs.ID as AppRecID, dbo.AppRecs.GenDate, dbo.AppRecs.Status, dbo.ErrorReasons.Err_S, dbo.ErrorReasons.Err_V, dbo.ErrorReasons.Err_DN, dbo.ErrorReasons.Err_OT
		FROM dbo.AppRecs
			JOIN dbo.ErrorReasons ON dbo.AppRecs.Id = dbo.ErrorReasons.AppRecID
			WHERE GenDate BETWEEN @StartDate AND @EndDate
		) AS Errors
	GROUP BY Errors.Err_DN
	ORDER BY ErrorCount DESC
GO
/****** Object:  StoredProcedure [dbo].[AppRecErrorTopSender]    Script Date: 04/07/2022 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AppRecErrorTopSender]   
	@SenderIdentifier varchar(MAX),
	@StartDate varchar(MAX),
	@EndDate varchar(MAX)
AS   
    SET NOCOUNT ON;  
	IF 
	(
		(SELECT COUNT(*)
		FROM dbo.AppRecs
			JOIN dbo.Senders ON dbo.AppRecs.SenderID = dbo.Senders.Id
			WHERE GenDate BETWEEN @StartDate AND @EndDate
			AND (dbo.Senders.Name = @SenderIdentifier OR dbo.Senders.SenderId = @SenderIdentifier)) > 3
	)
		SELECT TOP(5) Errors.Err_DN, COUNT(Errors.Err_DN) AS ErrorCount FROM
		(SELECT dbo.AppRecs.ID as AppRecID, dbo.AppRecs.GenDate, dbo.AppRecs.Status, 
				dbo.ErrorReasons.Err_S, dbo.ErrorReasons.Err_V, dbo.ErrorReasons.Err_DN, dbo.ErrorReasons.Err_OT
			FROM dbo.AppRecs
				JOIN dbo.ErrorReasons ON dbo.AppRecs.Id = dbo.ErrorReasons.AppRecID
				JOIN dbo.Senders ON dbo.AppRecs.SenderID = dbo.Senders.Id
				WHERE GenDate BETWEEN @StartDate AND @EndDate
				AND (dbo.Senders.Name = @SenderIdentifier OR dbo.Senders.SenderId = @SenderIdentifier)
			) AS Errors
		GROUP BY Errors.Err_DN
		ORDER BY ErrorCount DESC
	ELSE (
		SELECT 'NOT ENOUGH MESSAGES' AS Err_DN, 0 AS ErrorCount
	)

GO
/****** Object:  StoredProcedure [dbo].[GetFailedApprecPeriodsGlobal]    Script Date: 04/07/2022 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFailedApprecPeriodsGlobal]    
AS   
    SET NOCOUNT ON;  
	select min(GenDate) as PeriodStart, max(GenDate) as PeriodEnd, Status, count(*) as PeriodLength
	from (select *,
				 row_number() over (partition by MsgType order by GenDate) as seqnum,
				 row_number() over (partition by MsgType, Status order by GenDate) as seqnum_uc
		  from 
		   (
				SELECT GenDate,
					   MsgType, 
						case Status
							when 1 then 'Success'
							when 2 then 'Failed'
							when 3 then 'Failed'
						end as Status
				FROM dbo.AppRecs
				ORDER BY GenDate OFFSET 0 ROWS
		   ) b
		 ) t
	WHERE Status='Failed'
	group by MsgType, Status, (seqnum - seqnum_uc)
	HAVING COUNT(*) > 1
GO
/****** Object:  StoredProcedure [dbo].[GetFailedApprecPeriodsSender]    Script Date: 04/07/2022 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFailedApprecPeriodsSender]
	@SenderIdentifier varchar(MAX)  
AS   

    SET NOCOUNT ON;  
	select min(GenDate) as PeriodStart, max(GenDate) as PeriodEnd, Status, count(*) as PeriodLength
	from (select *,
				 row_number() over (partition by MsgType order by GenDate) as seqnum,
				 row_number() over (partition by MsgType, Status order by GenDate) as seqnum_uc
		  from 
		   (
			   SELECT GenDate, MsgType, dbo.Senders.SenderId, dbo.Senders.Name, 
						case Status
							when 1 then 'Success'
							when 2 then 'Failed'
							when 3 then 'Failed'
						end as Status
					FROM dbo.AppRecs
						JOIN dbo.Senders ON dbo.AppRecs.SenderID = dbo.Senders.Id
						WHERE (dbo.Senders.SenderId = @SenderIdentifier OR Name = @SenderIdentifier)
						ORDER BY GenDate OFFSET 0 ROWS
		   ) b
		 ) t
	WHERE Status='Failed'
	group by MsgType, Status, (seqnum - seqnum_uc)
	HAVING COUNT(*) > 1
GO
/****** Object:  StoredProcedure [dbo].[GetMonthlySenderRatio]    Script Date: 04/07/2022 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMonthlySenderRatio]   
	@SenderIdentifier varchar(MAX),
	@StartDate varchar(MAX),
	@EndDate varchar(MAX)   
AS   
    SET NOCOUNT ON;  
	SELECT TOP(20) Ratios.Status, COUNT(Ratios.Status) AS StatusCount FROM
		(SELECT dbo.Apprecs.Id AS ApprecID, dbo.Apprecs.GenDate, dbo.Apprecs.SenderID, dbo.Apprecs.Status, dbo.Senders.SenderId AS UnqSenderId, dbo.Senders.Name
			FROM dbo.AppRecs
				JOIN dbo.Senders ON dbo.Apprecs.SenderID = dbo.Senders.Id
				--ORDER BY GenDate
			WHERE (dbo.Senders.SenderId = @SenderIdentifier OR dbo.Senders.Name = @SenderIdentifier)
				AND GenDate BETWEEN @StartDate AND @EndDate
		) AS Ratios
	GROUP BY Ratios.Status
GO
