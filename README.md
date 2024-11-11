CREATE DATABASE Magazine;
GO

USE Magazine;
GO

-- Bảng Roles: Lưu thông tin về các loại quyền
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- Bảng Accounts: Lưu thông tin tài khoản đăng nhập
CREATE TABLE Accounts (
    UserID INT PRIMARY KEY IDENTITY NOT NULL,
    Username NVARCHAR(MAX) NOT NULL,
	RoleID INT NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Avatar NVARCHAR(MAX) NULL,
	FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
GO

-- Bảng Users: Lưu thông tin người dùng
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY NOT NULL,
    RoleID INT NOT NULL,
    Username NVARCHAR(256) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Email NVARCHAR(256) NULL,
    Fullname NVARCHAR(MAX) NULL,
    Avatar NVARCHAR(MAX) NULL,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
GO

-- Bảng Categories: Lưu thông tin chuyên mục cha
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY NOT NULL,
    CategoryName NVARCHAR(MAX) NOT NULL,
);
GO

-- Bảng Categories: Lưu thông tin chuyên mục con
CREATE TABLE SubCategories (
    SubCategoryID INT PRIMARY KEY IDENTITY NOT NULL,
    CategoryID INT NOT NULL,
    SubCategoryName NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) 
);
GO

-- Bảng Articles: Lưu thông tin bài viết
CREATE TABLE Articles (
    ArticleID INT PRIMARY KEY IDENTITY NOT NULL,
    Title NVARCHAR(MAX) NOT NULL,
    Contents NVARCHAR(MAX),
    PostTime DATETIME DEFAULT GETDATE(),
    ArticleImage NVARCHAR(MAX),
    Link NVARCHAR(256),
    UserID INT,
    CategoryID INT NOT NULL,
	ApprovalStatus NVARCHAR(256),
	Feedback NVARCHAR (MAX),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) ON DELETE CASCADE
);
GO

-- Bảng Comments: Lưu các phản hồi từ người đọc
CREATE TABLE Comments (
    CmtID INT PRIMARY KEY IDENTITY NOT NULL,
    UserID INT NULL,
    ArticleID INT NOT NULL,
    CmtContent NVARCHAR(MAX) NOT NULL,
    DateComment DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ArticleID) REFERENCES Articles(ArticleID) ON DELETE CASCADE
);
GO

-- Bảng NewsletterSubscription: Đăng ký nhận bản tin
CREATE TABLE NewsletterSubscription (
    SubID INT PRIMARY KEY IDENTITY,
    Email NVARCHAR(256) NOT NULL UNIQUE,
    SubDate DATE DEFAULT GETDATE(),
    UserID INT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE  
);
GO

INSERT INTO Roles (RoleName)
VALUES ('Admin'), ('Editor'), ('Viewer');
GO

INSERT INTO Accounts (Username, RoleID, PasswordHash, Avatar)
VALUES 
    ('Admin', 1 ,'lsrjXOipsCRBeL8o5JZsLOG4OFcjqWprg4hYzdbKCh4=', '~\Admin\Content\img\avatar\admin.jpg'),
    ('Editor', 2 , 'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', '~\Admin\Content\img\avatar\editor.jpg');
GO

INSERT INTO Users (RoleID, Username, PasswordHash, Email, Fullname, Avatar)
VALUES 
    (3, 'DiuTran', 'lsrjXOipsCRBeL8o5JZsLOG4OFcjqWprg4hYzdbKCh4=', 'tranthidiu@gmail.com', N'Trần Thị Dịu', '~\Content\img\avatar\Diu.jpg'),
    (3, 'DucLe', 'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', 'lephiduc@gmail.com', N'Lê Phỉ Đức', '~\Content\img\avatar\Duc.jpg'),
	(3, 'DuongHuynh', 'lsrjXOipsCRBeL8o5JZsLOG4OFcjqWprg4hYzdbKCh4=', 'huynhphucduong@gmail.com', N'Huỳnh Phúc Dương', '~\Content\img\avatar\Duong.jpg'),
	(3, 'TuNguyen', 'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', 'nguyenvantu@gmail.com', N'Nguyễn Văn Tư', '~\Content\img\avatar\Tu.jpg'),
	(3, 'NhatHoang', 'lsrjXOipsCRBeL8o5JZsLOG4OFcjqWprg4hYzdbKCh4=', 'hoangvannhat@gmail.com', N'Hoàng Văn Nhật', '~\Content\img\avatar\Nhat.jpg');
GO

INSERT INTO Categories (CategoryName)
VALUES 
    (N'Chính trị'),
    (N'Thời sự'),
    (N'Thể thao'),
    (N'Thế giới'),
	(N'Giáo dục'),
	(N'Văn hóa'),
	(N'Sức khỏe'),
	(N'Pháp luật'),
	(N'Thời tiết'),
    (N'Giải trí');
GO

INSERT INTO SubCategories (CategoryID, SubCategoryName)
VALUES 
    (1,N'Đối ngoại'),
    (2,N'Quốc hội'),
    (3,N'Bóng đá'),
    (4,N'Quân sự'),
	(5,N'Du học'),
	(6,N'Di sản'),
	(7,N'Tư vấn sức khỏe'),
	(8,N'Tư vấn pháp luật'),
	(9,N'Dự báo thời tiết'),
    (10,N'Nhạc');
GO

INSERT INTO Articles (Title, Contents, PostTime, ArticleImage, Link, UserID, CategoryID, ApprovalStatus, Feedback)
VALUES 
    (N'Introduction to ASP.NET MVC', N'Content of the article about ASP.NET MVC...', GETDATE(), 'image1.jpg', 'https://example.com/article1', 1, 1, N'Đã Duyệt', NULL),
    (N'Health Benefits of Daily Exercise', N'Content of the article about health benefits...', GETDATE(), 'image2.jpg', 'https://example.com/article2', 2, 2, N'Yêu cầu chỉnh sửa', N'Thêm nhiều tài liệu tham khảo hơn.'),
    (N'Top 10 Lifestyle Trends in 2023', N'Content of the article about lifestyle trends...', GETDATE(), 'image3.jpg', 'https://example.com/article3', 1, 3, N'Chưa xử lý', NULL),
    (N'The Future of Business Automation', N'Content of the article about business automation...', GETDATE(), 'image4.jpg', 'https://example.com/article4', 2, 4, N'Yêu cầu chỉnh sửa', N'Cần hình ảnh tốt hơn.'),
    (N'Upcoming Movies and Shows', N'Content of the article about entertainment...', GETDATE(), 'image5.jpg', 'https://example.com/article5', 1, 5, N'Đã Duyệt', NULL);
GO

INSERT INTO Comments (UserID, ArticleID, CmtContent, DateComment)
VALUES 
    (1, 1, N'Bài viết thật tuyệt', GETDATE()),
    (2, 2, N'Rất bổ ích. Cảm ơn đã chia sẻ!', GETDATE()),
    (1, 3, N'Thích những hiểu biết sâu sắc về xu hướng lối sống.', GETDATE()),
    (2, 4, N'Mong đợi nhiều bài viết hơn về chính trị.', GETDATE()),
    (1, 5, N'Rất háo hức với những bộ phim sắp tới!', GETDATE());
GO

INSERT INTO NewsletterSubscription (Email, SubDate, UserID)
VALUES 
    ('tranthidiu@gmail.com', GETDATE(), 1),
    ('nguyenvantu@gmail.com', GETDATE(), 4),
    ('lephiduc@gmail.com', GETDATE(), 2);
GO
