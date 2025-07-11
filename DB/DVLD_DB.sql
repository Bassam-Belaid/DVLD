USE [master]
GO

/****** Object:  Database [DVLD_DB]    Script Date: 7/11/2025 5:12:58 PM ******/
CREATE DATABASE [DVLD_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DVLD_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DVLD_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DVLD_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DVLD_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DVLD_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [DVLD_DB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [DVLD_DB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [DVLD_DB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [DVLD_DB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [DVLD_DB] SET ARITHABORT OFF 
GO

ALTER DATABASE [DVLD_DB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [DVLD_DB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [DVLD_DB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [DVLD_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [DVLD_DB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [DVLD_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [DVLD_DB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [DVLD_DB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [DVLD_DB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [DVLD_DB] SET  ENABLE_BROKER 
GO

ALTER DATABASE [DVLD_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [DVLD_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [DVLD_DB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [DVLD_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [DVLD_DB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [DVLD_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [DVLD_DB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [DVLD_DB] SET RECOVERY FULL 
GO

ALTER DATABASE [DVLD_DB] SET  MULTI_USER 
GO

ALTER DATABASE [DVLD_DB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [DVLD_DB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [DVLD_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [DVLD_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [DVLD_DB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [DVLD_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [DVLD_DB] SET QUERY_STORE = ON
GO

ALTER DATABASE [DVLD_DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [DVLD_DB] SET  READ_WRITE 
GO

USE [DVLD_DB]
GO
/****** Object:  Table [dbo].[LicenseClasses]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseClasses](
	[LicenseClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[ClassDescription] [nvarchar](500) NOT NULL,
	[MinimumAllowedAge] [tinyint] NOT NULL,
	[DefaultValidityLength] [tinyint] NOT NULL,
	[ClassFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_LicenseClasses] PRIMARY KEY CLUSTERED 
(
	[LicenseClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicantPersonID] [int] NOT NULL,
	[ApplicationDate] [datetime] NOT NULL,
	[ApplicationTypeID] [int] NOT NULL,
	[ApplicationStatus] [tinyint] NOT NULL,
	[LastStatusDate] [datetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocalDrivingLicenseApplications]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalDrivingLicenseApplications](
	[LocalDrivingLicenseApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[LicenseClassID] [int] NOT NULL,
 CONSTRAINT [PK_DrivingLicsenseApplications] PRIMARY KEY CLUSTERED 
(
	[LocalDrivingLicenseApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestAppointments]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestAppointments](
	[TestAppointmentID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeID] [int] NOT NULL,
	[LocalDrivingLicenseApplicationID] [int] NOT NULL,
	[AppointmentDate] [smalldatetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsLocked] [bit] NOT NULL,
 CONSTRAINT [PK_TestAppointments] PRIMARY KEY CLUSTERED 
(
	[TestAppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[TestID] [int] IDENTITY(1,1) NOT NULL,
	[TestAppointmentID] [int] NOT NULL,
	[TestResult] [bit] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[TestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[NationalityCountryID] [int] NOT NULL,
	[NationalNo] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[SecondName] [nvarchar](20) NOT NULL,
	[ThirdName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [bit] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[ImagePath] [nvarchar](250) NULL,
 CONSTRAINT [PK__People__PersonID] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__People__National_Number] UNIQUE NONCLUSTERED 
(
	[NationalNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_People_Phone] UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vLocalDrivingLicenseApplicationsView]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vLocalDrivingLicenseApplicationsView]
AS
SELECT        dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, dbo.LicenseClasses.ClassName, dbo.People.NationalNo, 
                         dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + CASE WHEN People.ThirdName IS NOT NULL THEN People.ThirdName + ' ' ELSE '' END + dbo.People.LastName AS FullName, dbo.Applications.ApplicationDate,
                             (SELECT        COUNT(dbo.TestAppointments.TestTypeID) AS PassedTests
                               FROM            dbo.TestAppointments INNER JOIN
                                                         dbo.Tests ON dbo.TestAppointments.TestAppointmentID = dbo.Tests.TestAppointmentID
                               WHERE        (dbo.TestAppointments.LocalDrivingLicenseApplicationID = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) AND (dbo.Tests.TestResult = 1)) AS PassedTests, 
                         CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled' WHEN Applications.ApplicationStatus = 3 THEN 'Completed' END AS Status
FROM            dbo.Applications INNER JOIN
                         dbo.LocalDrivingLicenseApplications ON dbo.Applications.ApplicationID = dbo.LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                         dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID INNER JOIN
                         dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
	[CountryNationality] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Countrie__10D160BFCEB4655F] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vPeopleView]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vPeopleView] AS
SELECT 
    People.PersonID,
    People.NationalNo,
    People.FirstName,
    People.SecondName,
    People.ThirdName,
    People.LastName,
    CASE 
        WHEN People.Gender = 0 THEN 'Female' 
        WHEN People.Gender = 1 THEN 'Male' 
    END AS Gender,
    People.DateOfBirth,
    Countries.CountryNationality,
    People.Phone,
    People.Email
FROM 
    Countries 
INNER JOIN 
    People ON Countries.CountryID = People.NationalityCountryID;
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](64) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Permissions] [int] NOT NULL,
 CONSTRAINT [PK__Users__1788CCAC2F42B83A] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Users__AA2FFB84BB378D51] UNIQUE NONCLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Users__C9F28456164F1488] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vUsersView]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vUsersView] AS
SELECT 
    Users.UserID, 
    Users.PersonID, 
    (People.FirstName + ' ' + 
     People.SecondName + ' ' + 
     CASE WHEN People.ThirdName IS NOT NULL THEN People.ThirdName + ' ' ELSE '' END + 
     People.LastName) AS FullName, 
    Users.UserName, 
    Users.IsActive
FROM 
    Users 
INNER JOIN 
    People ON Users.PersonID = People.PersonID;
GO
/****** Object:  Table [dbo].[ApplicationTypes]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationTypes](
	[ApplicationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeTitle] [nvarchar](150) NOT NULL,
	[ApplicationFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK__Applicat__2821511A01EDB065] PRIMARY KEY CLUSTERED 
(
	[ApplicationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Applicat__0FAB6816C8600DAC] UNIQUE NONCLUSTERED 
(
	[ApplicationTypeTitle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestTypes]    Script Date: 7/11/2025 4:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestTypes](
	[TestTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeTitle] [nvarchar](100) NOT NULL,
	[TestTypeDescription] [nvarchar](500) NOT NULL,
	[TestTypeFees] [smallmoney] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TestTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TestTypeTitle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_ApplicationDate]  DEFAULT (getdate()) FOR [ApplicationDate]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_ApplicationStatus]  DEFAULT ((1)) FOR [ApplicationStatus]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_LastStatusDate]  DEFAULT (getdate()) FOR [LastStatusDate]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_Age]  DEFAULT ((18)) FOR [MinimumAllowedAge]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_DefaultPeriodLength]  DEFAULT ((1)) FOR [DefaultValidityLength]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_ClassFees]  DEFAULT ((0)) FOR [ClassFees]
GO
ALTER TABLE [dbo].[TestAppointments] ADD  CONSTRAINT [DF_TestAppointments_AppointmentLocked]  DEFAULT ((0)) FOR [IsLocked]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__IsActive__71D1E811]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_ApplicationTypes] FOREIGN KEY([ApplicationTypeID])
REFERENCES [dbo].[ApplicationTypes] ([ApplicationTypeID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_ApplicationTypes]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_People] FOREIGN KEY([ApplicantPersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_People]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Users]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_DrivingLicsenseApplications_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_DrivingLicsenseApplications_Applications]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses] FOREIGN KEY([LicenseClassID])
REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK__People__National__3B75D760] FOREIGN KEY([NationalityCountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK__People__National__3B75D760]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications] FOREIGN KEY([LocalDrivingLicenseApplicationID])
REFERENCES [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_TestTypes] FOREIGN KEY([TestTypeID])
REFERENCES [dbo].[TestTypes] ([TestTypeID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_TestTypes]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_Users]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_TestAppointments] FOREIGN KEY([TestAppointmentID])
REFERENCES [dbo].[TestAppointments] ([TestAppointmentID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_TestAppointments]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__PersonID__72C60C4A] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__PersonID__72C60C4A]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-New 2-Cancelled 3-Completed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'ApplicationStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minmum age allowed to apply for this license' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LicenseClasses', @level2type=N'COLUMN',@level2name=N'MinimumAllowedAge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How many years the licesnse will be valid.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LicenseClasses', @level2type=N'COLUMN',@level2name=N'DefaultValidityLength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
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
         Begin Table = "Applications"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LocalDrivingLicenseApplications"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 304
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LicenseClasses"
            Begin Extent = 
               Top = 252
               Left = 38
               Bottom = 382
               Right = 244
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "People"
            Begin Extent = 
               Top = 6
               Left = 263
               Bottom = 136
               Right = 464
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vLocalDrivingLicenseApplicationsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vLocalDrivingLicenseApplicationsView'
GO
