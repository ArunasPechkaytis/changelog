--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.311.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 22.11.2017 23:32:09
-- Версия сервера: 10.50.1600
--



USE changelog
GO

IF DB_NAME() <> N'changelog' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[RW_Projects]
--
PRINT (N'Создать таблицу [dbo].[RW_Projects]')
GO
CREATE TABLE dbo.RW_Projects (
  id_Projects int IDENTITY,
  ProjectName nvarchar(100) NULL,
  ProjectLink nvarchar(1000) NULL,
  PullMax int NULL,
  DateCreate datetime NULL DEFAULT (getdate()),
  deleted bit NULL DEFAULT (0),
  DateChanged datetime NULL DEFAULT (getdate()),
  CONSTRAINT PK_tbl_projects PRIMARY KEY CLUSTERED (id_Projects)
)
ON [PRIMARY]
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[ProjectName] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[ProjectName] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Название проекта', 'SCHEMA', N'dbo', 'TABLE', N'RW_Projects', 'COLUMN', N'ProjectName'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[ProjectLink] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[ProjectLink] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Ссылка на загрузку проекта', 'SCHEMA', N'dbo', 'TABLE', N'RW_Projects', 'COLUMN', N'ProjectLink'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[DateCreate] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[DateCreate] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'дата создания объекта', 'SCHEMA', N'dbo', 'TABLE', N'RW_Projects', 'COLUMN', N'DateCreate'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[deleted] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[deleted] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'флаг пометки на удаление', 'SCHEMA', N'dbo', 'TABLE', N'RW_Projects', 'COLUMN', N'deleted'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[DateChanged] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_Projects].[DateChanged] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'дата изменения', 'SCHEMA', N'dbo', 'TABLE', N'RW_Projects', 'COLUMN', N'DateChanged'
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать процедуру [dbo].[RW_ProjectPullUpdate]
--
GO
PRINT (N'Создать процедуру [dbo].[RW_ProjectPullUpdate]')
GO
-- =============================================
-- author:		pechkaytis arunas
-- create date: 17.11.2017
-- description:	Настройки проекта 
-- =============================================
CREATE PROCEDURE dbo.RW_ProjectPullUpdate 
  @id_Projects int,
  @PullMax int
AS
BEGIN

 update dbo.RW_Projects
 set PullMax = @PullMax
 where id_Projects = @id_Projects

END;
GO

--
-- Создать процедуру [dbo].[RW_ProjectGet]
--
GO
PRINT (N'Создать процедуру [dbo].[RW_ProjectGet]')
GO

-- =============================================
-- author:		pechkaytis arunas
-- create date: 16.11.2017
-- description:	Настройки проекта 
-- =============================================
create PROCEDURE dbo.RW_ProjectGet @ProjectName NVARCHAR(1000)
AS
BEGIN

  SELECT
    *
  FROM dbo.RW_Projects
  WHERE ProjectName LIKE '%' + @ProjectName + '%'

END;
GO

--
-- Создать функцию [dbo].[RW_ProjectDataGetDate]
--
GO
PRINT (N'Создать функцию [dbo].[RW_ProjectDataGetDate]')
GO

-- =============================================
-- author:		pechkaytis arunas
-- create date: 17.11.2017
-- description:	преобразование юникс даты в 'нормальную' дату 
-- =============================================
CREATE FUNCTION dbo.RW_ProjectDataGetDate
(	
	@DateUnix nvarchar(100)
)
RETURNS datetime 
AS
begin

declare @unixtimestamp int
declare @oneday int
declare @remainingseconds decimal(20,4)
declare @date datetime
set @unixtimestamp = convert(bigint, @DateUnix) / 1000
set @oneday= 86400 --60 min *60 sec * 24 hrs
set @date=dateadd(day,@unixtimestamp /@oneday,'01/01/1970') 
set @remainingseconds =(convert(decimal(20,4),@unixtimestamp) /convert(decimal(20,4),@oneday)) -(@unixtimestamp /@oneday)
  
return dateadd(ss, @remainingseconds * @oneday, @date)
 
end
GO

--
-- Создать таблицу [dbo].[RW_ProjectsData]
--
PRINT (N'Создать таблицу [dbo].[RW_ProjectsData]')
GO
CREATE TABLE dbo.RW_ProjectsData (
  id_Data int IDENTITY,
  id_Projects int NULL,
  DateCreate datetime NULL DEFAULT (getdate()),
  deleted bit NULL DEFAULT (0),
  DateChanged datetime NULL DEFAULT (getdate()),
  ProjectData xml NULL,
  CONSTRAINT PK_RW_ProjectsData_id_Data PRIMARY KEY CLUSTERED (id_Data)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData] (таблица)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData] (таблица)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Таблица для хранения xml (json) ответа', 'SCHEMA', N'dbo', 'TABLE', N'RW_ProjectsData'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData].[id_Data] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData].[id_Data] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Уникальный код записи', 'SCHEMA', N'dbo', 'TABLE', N'RW_ProjectsData', 'COLUMN', N'id_Data'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData].[DateCreate] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData].[DateCreate] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'дата создания объекта', 'SCHEMA', N'dbo', 'TABLE', N'RW_ProjectsData', 'COLUMN', N'DateCreate'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData].[deleted] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData].[deleted] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'флаг пометки на удаление', 'SCHEMA', N'dbo', 'TABLE', N'RW_ProjectsData', 'COLUMN', N'deleted'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData].[DateChanged] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[RW_ProjectsData].[DateChanged] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'дата изменения', 'SCHEMA', N'dbo', 'TABLE', N'RW_ProjectsData', 'COLUMN', N'DateChanged'
GO

--
-- Создать процедуру [dbo].[RW_ProjectDataAdd]
--
GO
PRINT (N'Создать процедуру [dbo].[RW_ProjectDataAdd]')
GO

-- =============================================
-- author:		pechkaytis arunas
-- create date: 16.11.2017
-- description:	сохраняем xml данные 
-- =============================================
CREATE procedure dbo.RW_ProjectDataAdd 
  @id_Projects int,
  @ProjectData xml
as
begin 
   
insert into dbo.RW_ProjectsData ( id_Projects, ProjectData )
values ( @id_Projects, @ProjectData )

end;
GO

--
-- Создать представление [dbo].[RW_ProjectsViewDataReviewers]
--
GO
PRINT (N'Создать представление [dbo].[RW_ProjectsViewDataReviewers]')
GO
CREATE VIEW dbo.RW_ProjectsViewDataReviewers
AS
select 
    pd.id_Projects
  ,	[ProjectData].value('(/root/changelog//id/node())[1]', 'nvarchar(max)') as id
  , xml.xmldata.value('(lastReviewedCommit)[1]', 'varchar(1000)') as ReviewersLastReviewedCommit 
  , xml.xmldata.value('(role)[1]', 'varchar(1000)') as ReviewersRole   
  , xml.xmldata.value('(approved)[1]', 'bit') as ReviewersApproved     
  , xml.xmldata.value('(status)[1]', 'varchar(1000)') as ReviewersStatus    
  , xml.xmldata.value('(user/name)[1]', 'varchar(1000)') as ReviewersUserName 
  , xml.xmldata.value('(user/emailAddress)[1]', 'varchar(1000)') as ReviewersUserEmailAddress
  , xml.xmldata.value('(user/id)[1]', 'varchar(1000)') as ReviewersUserId
  , xml.xmldata.value('(user/displayName)[1]', 'varchar(1000)') as ReviewersUserDisplayName
  , xml.xmldata.value('(user/active)[1]', 'varchar(1000)') as ReviewersUserActive
  , xml.xmldata.value('(user/slug)[1]', 'varchar(1000)') as ReviewersUserSlug  
  , xml.xmldata.value('(user/type)[1]', 'varchar(1000)') as ReviewersUserType  
  , xml.xmldata.value('(user/links)[1]', 'varchar(1000)') as ReviewersUserLinks    
  , 'http://sh31.corteos.ru:7990/' + xml.xmldata.value('(user/avatarUrl)[1]', 'varchar(1000)') as ReviewersUserAvatarUrl   
   from RW_ProjectsData pd
   cross apply pd.[ProjectData].nodes('/root/changelog/reviewers') xml (xmldata)  
GO

--
-- Добавить расширенное свойство [MS_DiagramPane1] для [dbo].[RW_ProjectsViewDataReviewers] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPane1] для [dbo].[RW_ProjectsViewDataReviewers] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', 'SCHEMA', N'dbo', 'VIEW', N'RW_ProjectsViewDataReviewers'
GO

--
-- Добавить расширенное свойство [MS_DiagramPaneCount] для [dbo].[RW_ProjectsViewDataReviewers] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPaneCount] для [dbo].[RW_ProjectsViewDataReviewers] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPaneCount', 1, 'SCHEMA', N'dbo', 'VIEW', N'RW_ProjectsViewDataReviewers'
GO

--
-- Создать процедуру [dbo].[RW_ProjectsViewReviewers]
--
GO
PRINT (N'Создать процедуру [dbo].[RW_ProjectsViewReviewers]')
GO
-- =============================================
-- author:		pechkaytis arunas
-- create date: 19.11.2017
-- description:	Отображение аудиторов
-- =============================================
CREATE PROCEDURE dbo.RW_ProjectsViewReviewers @id int
AS
BEGIN


select
    *
from RW_ProjectsViewDataReviewers
where id = @id  or @id = 0
order by id desc
  

END;
GO

--
-- Создать представление [dbo].[RW_ProjectsViewData]
--
GO
PRINT (N'Создать представление [dbo].[RW_ProjectsViewData]')
GO
CREATE VIEW dbo.RW_ProjectsViewData
AS
SELECT     p.id_Projects, p.ProjectName, pd.ProjectData.value('(/root/changelog//id/node())[1]', 'int') AS id, pd.ProjectData.value('(/root/changelog//version/node())[1]', 'nvarchar(max)') AS version, 
                      pd.ProjectData.value('(/root/changelog//title/node())[1]', 'nvarchar(max)') AS title, pd.ProjectData.value('(/root/changelog//description/node())[1]', 'nvarchar(max)') AS description, 
                      pd.ProjectData.value('(/root/changelog//state/node())[1]', 'nvarchar(max)') AS state, pd.ProjectData.value('(/root/changelog//open/node())[1]', 'nvarchar(max)') AS [open], 
                      pd.ProjectData.value('(/root/changelog//closed/node())[1]', 'nvarchar(max)') AS closed, dbo.RW_ProjectDataGetDate(pd.ProjectData.value('(/root/changelog//createdDate/node())[1]', 'nvarchar(max)')) 
                      AS createdDate, dbo.RW_ProjectDataGetDate(pd.ProjectData.value('(/root/changelog//updatedDate/node())[1]', 'nvarchar(max)')) AS updatedDate, 
                      dbo.RW_ProjectDataGetDate(pd.ProjectData.value('(/root/changelog//closedDate/node())[1]', 'nvarchar(max)')) AS closedDate, pd.ProjectData.value('(/root/changelog//locked/node())[1]', 
                      'nvarchar(max)') AS locked, pd.ProjectData.value('(/root/changelog//links/node())[1]', 'nvarchar(max)') AS links, pd.ProjectData.value('(/root/changelog//descriptionAsHtml/node())[1]', 'nvarchar(max)') 
                      AS descriptionAsHtml, pd.ProjectData.value('(/root/changelog/author//role/node())[1]', 'nvarchar(max)') AS AuthorRole, pd.ProjectData.value('(/root/changelog/author//approved/node())[1]', 
                      'nvarchar(max)') AS AuthorApproved, pd.ProjectData.value('(/root/changelog/author//status/node())[1]', 'nvarchar(max)') AS AuthorStatus, 
                      pd.ProjectData.value('(/root/changelog/author/user//name/node())[1]', 'nvarchar(max)') AS AuthorUserName, pd.ProjectData.value('(/root/changelog/author/user//emailAddress/node())[1]', 
                      'nvarchar(max)') AS AuthorUserEmailAddress, pd.ProjectData.value('(/root/changelog/author/user//id/node())[1]', 'nvarchar(max)') AS AuthorUserId, 
                      pd.ProjectData.value('(/root/changelog/author/user//displayName/node())[1]', 'nvarchar(max)') AS AuthorUserDisplayName, pd.ProjectData.value('(/root/changelog/author/user//active/node())[1]', 
                      'nvarchar(max)') AS AuthorUserActive, pd.ProjectData.value('(/root/changelog/author/user//slug/node())[1]', 'nvarchar(max)') AS AuthorUserSlug, 
                      pd.ProjectData.value('(/root/changelog/author/user//type/node())[1]', 'nvarchar(max)') AS AuthorUserType, pd.ProjectData.value('(/root/changelog/author/user//links/node())[1]', 'nvarchar(max)') 
                      AS AuthorUserLinks, 'http://sh31.corteos.ru:7990/' + pd.ProjectData.value('(/root/changelog/author/user//avatarUrl/node())[1]', 'nvarchar(max)') AS AuthorUserAvatarUrl
FROM         dbo.RW_ProjectsData AS pd INNER JOIN
                      dbo.RW_Projects AS p ON pd.id_Projects = p.id_Projects
GO

--
-- Добавить расширенное свойство [MS_DiagramPane1] для [dbo].[RW_ProjectsViewData] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPane1] для [dbo].[RW_ProjectsViewData] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pd"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 126
               Right = 414
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', 'SCHEMA', N'dbo', 'VIEW', N'RW_ProjectsViewData'
GO

--
-- Добавить расширенное свойство [MS_DiagramPaneCount] для [dbo].[RW_ProjectsViewData] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPaneCount] для [dbo].[RW_ProjectsViewData] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPaneCount', 1, 'SCHEMA', N'dbo', 'VIEW', N'RW_ProjectsViewData'
GO

--
-- Создать процедуру [dbo].[RW_ProjectsViewAuthor]
--
GO
PRINT (N'Создать процедуру [dbo].[RW_ProjectsViewAuthor]')
GO
-- =============================================
-- author:		pechkaytis arunas
-- create date: 19.11.2017
-- description:	Отображение авторов логов 
-- =============================================
CREATE PROCEDURE dbo.RW_ProjectsViewAuthor  
AS
BEGIN


select 
  distinct
  author.AuthorRole
, author.AuthorApproved
, author.AuthorUserName
, author.AuthorStatus
, author.AuthorUserEmailAddress
, author.AuthorUserId
, author.AuthorUserDisplayName
, author.AuthorUserActive
, author.AuthorUserSlug
, author.AuthorUserType
, author.AuthorUserLinks
, author.AuthorUserAvatarUrl
from
(
	select *
	from RW_ProjectsViewData
) author
order by author.AuthorUserName desc
  

END;
GO

--
-- Создать процедуру [dbo].[RW_ProjectsView]
--
GO
PRINT (N'Создать процедуру [dbo].[RW_ProjectsView]')
GO
-- =============================================
-- author:		pechkaytis arunas
-- create date: 19.11.2017
-- description:	Отображение лога всего или только по проекту 
-- =============================================
CREATE PROCEDURE dbo.RW_ProjectsView @id int
AS
BEGIN


select
    *
from RW_ProjectsViewData
where id_Projects = @id  or @id = 0
order by id_Projects desc
  

END;
GO

--
-- Создать пользователя [tester]
--
PRINT (N'Создать пользователя [tester]')
GO
CREATE USER tester
  FOR LOGIN tester
GO
-- 
-- Вывод данных для таблицы RW_Projects
--
SET IDENTITY_INSERT dbo.RW_Projects ON
GO
INSERT dbo.RW_Projects(id_Projects, ProjectName, ProjectLink, PullMax, DateCreate, deleted, DateChanged) VALUES (1, N'aviav3', N'http://sh31.corteos.ru:7990/projects/AVIAV3/repos/aviav3/pull-requests/{0}/overview', 163, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT dbo.RW_Projects OFF
GO
 