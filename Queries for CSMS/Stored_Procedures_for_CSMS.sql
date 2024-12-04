-- Create Stored Procedures

--=================================================================(INSERT)==============================================================--


-- USER
alter PROCEDURE [dbo].[PR_User_Insert]
    @UserName VARCHAR(100),
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @MobileNo VARCHAR(15),
    @Address VARCHAR(100),
    @IsActive BIT
AS
BEGIN
    INSERT INTO [User] (UserName, Email, Password, MobileNo, Address, IsActive)
    VALUES (@UserName, @Email, @Password, @MobileNo, @Address, @IsActive);
END;
GO

-----------------------------------------------------------	------------------------------
-- Insert into User Table

INSERT INTO dbo.[User] (UserName, Email, Password, MobileNo, Address, IsActive)
VALUES 
('John Doe', 'john.doe@example.com', 'password123', '1234567890', '123 Main St', 1),
('Jane Smith', 'jane.smith@example.com', 'password456', '0987654321', '456 Elm St', 1),
('Alice Johnson', 'alice.johnson@example.com', 'password789', '1122334455', '789 Pine St', 0),
('Bob Brown', 'bob.brown@example.com', 'password321', '2233445566', '321 Oak St', 1),
('Charlie Davis', 'charlie.davis@example.com', 'password654', '3344556677', '654 Cedar St', 0),
('David Evans', 'david.evans@example.com', 'password111', '4455667788', '111 Maple St', 1),
('Eva Green', 'eva.green@example.com', 'password222', '5566778899', '222 Birch St', 0),
('Frank Harris', 'frank.harris@example.com', 'password333', '6677889900', '333 Willow St', 1),
('Grace Lee', 'grace.lee@example.com', 'password444', '7788990011', '444 Spruce St', 1),
('Henry King', 'henry.king@example.com', 'password555', '8899001122', '555 Cherry St', 0);
select * from [User]

EXEC PR_User_Insert 
    @UserName = 'Alice Johnson', 
    @Email = 'alice.johnson@example.com', 
    @Password = 'password123', 
    @MobileNo = '1234567890', 
    @Address = '123 Main St, Cityville', 
    @IsActive = 1;
	select * from [User]

EXEC PR_User_Insert 
    @UserName = 'Bob Smith', 
    @Email = 'bob.smith@example.com', 
    @Password = 'password456', 
    @MobileNo = '0987654321', 
    @Address = '456 Elm St, Townsville', 
    @IsActive = 1;
	select * from [User]

EXEC PR_User_Insert 
    @UserName = 'Carol White', 
    @Email = 'carol.white@example.com', 
    @Password = 'password789', 
    @MobileNo = '1122334455', 
    @Address = '789 Pine St, Villageville', 
    @IsActive = 1;

	select * from [User]

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

-- PRODUCT
Alter PROCEDURE [dbo].[PR_Product_Insert]
    @ProductName VARCHAR(100),
    @ProductPrice DECIMAL(10,2),
    @ProductCode VARCHAR(100),
    @Description VARCHAR(100),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[Product] 
	(
		[dbo].[Product].ProductName,
		[dbo].[Product].ProductPrice,
		[dbo].[Product].ProductCode,
		[dbo].[Product].Description,
		[dbo].[Product].UserID
	)
    VALUES
	(
		@ProductName,
		@ProductPrice,
		@ProductCode,
		@Description,
		@UserID
	);
END;
GO
-------------------------------------------------------------------------------------------------
-- Insert into Product Table

INSERT INTO dbo.Product (ProductName, ProductPrice, ProductCode, Description, UserID)
VALUES 
('Product A', 10.00, 'PRA100', 'Description of Product A', 16),
('Product B', 20.00, 'PRB200', 'Description of Product B', 17),
('Product C', 30.00, 'PRC300', 'Description of Product C', 18),
('Product D', 40.00, 'PRD400', 'Description of Product D', 19),
('Product E', 50.00, 'PRE500', 'Description of Product E', 20),
('Product F', 60.00, 'PRF600', 'Description of Product F', 21),
('Product G', 70.00, 'PRG700', 'Description of Product G', 22),
('Product H', 80.00, 'PRH800', 'Description of Product H', 23),
('Product I', 90.00, 'PRI900', 'Description of Product I', 24),
('Product J', 100.00, 'PRJ1000', 'Description of Product J', 25);
select * from Product

EXEC PR_Product_Insert 
    @ProductName = 'Espresso', 
    @ProductPrice = 2.50, 
    @ProductCode = 'ESP001', 
    @Description = 'Strong coffee', 
    @UserID = 1;
select * from Product

EXEC PR_Product_Insert 
    @ProductName = 'Latte', 
    @ProductPrice = 3.50, 
    @ProductCode = 'LAT001', 
    @Description = 'Coffee with milk', 
    @UserID = 2;
select * from Product

EXEC PR_Product_Insert 
    @ProductName = 'Cappuccino', 
    @ProductPrice = 3.00, 
    @ProductCode = 'CAP001', 
    @Description = 'Coffee with foam', 
    @UserID = 3;
select * from Product


---------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------


-- CUSTOMER
CREATE PROCEDURE [dbo].[PR_Customer_Insert]
    @CustomerName VARCHAR(100),
    @HomeAddress VARCHAR(100),
    @Email VARCHAR(100),
    @MobileNo VARCHAR(15),
    @GST_NO VARCHAR(15),
    @CityName VARCHAR(100),
    @PinCode VARCHAR(15),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    INSERT INTO [Customer] (CustomerName, HomeAddress, Email, MobileNo, GST_NO, CityName, PinCode, NetAmount, UserID)
    VALUES (@CustomerName, @HomeAddress, @Email, @MobileNo, @GST_NO, @CityName, @PinCode, @NetAmount, @UserID);
END;
GO

---------------------------------------------------------------------------------------------------------------------------------------------

-- Insert into Customer Table
INSERT INTO dbo.Customer (CustomerName, HomeAddress, Email, MobileNo, GST_NO, CityName, PinCode, NetAmount, UserID)
VALUES 
('Alice Green', '789 Pine St', 'alice.green@example.com', '1234567890', 'GST1234567890', 'Pine City', '123456', 1000.00, 16),
('Bob White', '321 Oak St', 'bob.white@example.com', '0987654321', 'GST0987654321', 'Oak Town', '654321', 2000.00, 17),
('Charlie Black', '456 Elm St', 'charlie.black@example.com', '1122334455', 'GST1122334455', 'Elm Village', '789012', 1500.00, 18),
('David Blue', '654 Cedar St', 'david.blue@example.com', '2233445566', 'GST2233445566', 'Cedar Grove', '345678', 2500.00, 19),
('Emma Yellow', '123 Main St', 'emma.yellow@example.com', '3344556677', 'GST3344556677', 'Main City', '567890', 3000.00, 20),
('Frank Orange', '789 Birch St', 'frank.orange@example.com', '4455667788', 'GST4455667788', 'Birch Town', '678901', 1750.00, 21),
('Grace Purple', '321 Willow St', 'grace.purple@example.com', '5566778899', 'GST5566778899', 'Willow Grove', '890123', 2250.00, 22),
('Henry Brown', '456 Maple St', 'henry.brown@example.com', '6677889900', 'GST6677889900', 'Maple Village', '901234', 2750.00, 23),
('Isabel Silver', '654 Spruce St', 'isabel.silver@example.com', '7788990011', 'GST7788990011', 'Spruce Town', '123789', 3250.00, 24),
('Jack Gold', '123 Cedar St', 'jack.gold@example.com', '8899001122', 'GST8899001122', 'Cedar City', '345012', 3500.00, 25);
select * from Customer

EXEC PR_Customer_Insert 
    @CustomerName = 'David Brown', 
    @HomeAddress = '321 Oak St, Hamletville', 
    @Email = 'david.brown@example.com', 
    @MobileNo = '2233445566', 
    @GST_NO = 'GST001', 
    @CityName = 'Hamletville', 
    @PinCode = '123456', 
    @NetAmount = 100.00, 
    @UserID = 1; 
select * from Customer

EXEC PR_Customer_Insert 
    @CustomerName = 'Eva Green', 
    @HomeAddress = '654 Birch St, Metropolis', 
    @Email = 'eva.green@example.com', 
    @MobileNo = '3344556677', 
    @GST_NO = 'GST002', 
    @CityName = 'Metropolis', 
    @PinCode = '654321', 
    @NetAmount = 200.00, 
    @UserID = 2; 
select * from Customer

EXEC PR_Customer_Insert 
    @CustomerName = 'Frank Blue', 
    @HomeAddress = '987 Cedar St, Urbania', 
    @Email = 'frank.blue@example.com', 
    @MobileNo = '4455667788', 
    @GST_NO = 'GST003', 
    @CityName = 'Urbania', 
    @PinCode = '987654', 
    @NetAmount = 150.00, 
    @UserID = 3;
select * from Customer

------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------

-- ORDER
alter PROCEDURE [dbo].[PR_Order_Insert]
    @OrderDate Date,
    @CustomerID INT,
    @PaymentMode VARCHAR(100),
    @TotalAmount DECIMAL(10,2),
    @ShippingAddress VARCHAR(100),
    @UserID INT
AS
BEGIN
    INSERT INTO [Order] (OrderDate, CustomerID, PaymentMode, TotalAmount, ShippingAddress, UserID)
    VALUES (@OrderDate, @CustomerID, @PaymentMode, @TotalAmount, @ShippingAddress, @UserID);
END;
GO

--------------------------------------------------------------------------------------------------------------------

-- Insert into Order Table

INSERT INTO dbo.[Order] (OrderDate, CustomerID, PaymentMode, TotalAmount, ShippingAddress, UserID)
VALUES 
('2023-07-01 10:30:00', 4, 'Credit Card', 150.75, '123 Main St', 16),
('2023-07-02 14:00:00', 5, 'PayPal', 200.00, '456 Elm St', 17),
('2023-07-03 09:15:00', 6, 'Credit Card', 120.00, '789 Pine St', 18),
('2023-07-04 11:45:00', 7, 'Cash', 99.99, '321 Oak St', 19),
('2023-07-05 16:20:00', 8, 'Debit Card', 175.50, '654 Cedar St', 20),
('2023-07-06 12:00:00', 9, 'Credit Card', 220.75, '123 Main St', 21),
('2023-07-07 08:45:00', 10, 'PayPal', 300.00, '456 Elm St', 22),
('2023-07-08 17:30:00', 11, 'Cash', 180.25, '789 Pine St', 23),
('2023-07-09 13:10:00', 12, 'Credit Card', 210.00, '321 Oak St', 24),
('2023-07-10 10:50:00', 13, 'Debit Card', 250.00, '654 Cedar St', 25);
select * from [Order]

EXEC PR_Order_Insert 
    @OrderDate = '2024-07-31', 
    @CustomerID = 1, 
    @PaymentMode = 'Credit Card', 
    @TotalAmount = 50.00, 
    @ShippingAddress = '321 Oak St, Hamletville', 
    @UserID = 1; 
select * from [Order]

EXEC PR_Order_Insert 
    @OrderDate = '2024-07-31', 
    @CustomerID = 2,
    @PaymentMode = 'Cash', 
    @TotalAmount = 75.00, 
    @ShippingAddress = '654 Birch St, Metropolis', 
    @UserID = 2; 
select * from [Order]

EXEC PR_Order_Insert 
    @OrderDate = '2024-07-31', 
    @CustomerID = 3, 
    @PaymentMode = 'Debit Card', 
    @TotalAmount = 60.00, 
    @ShippingAddress = '987 Cedar St, Urbania', 
    @UserID = 3; 
select * from [Order]

----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

-- ORDERDETAILS
CREATE PROCEDURE [dbo].[PR_OrderDetail_Insert]
    @OrderID INT,
    @ProductID INT,
    @Quantity INT,
    @Amount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    INSERT INTO [OrderDetail] (OrderID, ProductID, Quantity, Amount, TotalAmount, UserID)
    VALUES (@OrderID, @ProductID, @Quantity, @Amount, @TotalAmount, @UserID);
END;
GO

-----------------------------------------------------------------------------------------------------------------

-- Insert into OrderDetail Table
INSERT INTO dbo.OrderDetail (OrderID, ProductID, Quantity, Amount, TotalAmount, UserID)
VALUES 
(9, 13, 1, 10.00, 10.00, 16),
(10, 14, 2, 20.00, 40.00, 17),
(11, 15, 1, 30.00, 30.00, 18),
(12, 16, 2, 40.00, 80.00, 19),
(13, 17, 1, 50.00, 50.00, 20),
(14, 18, 3, 10.00, 30.00, 21),
(15, 19, 2, 20.00, 40.00, 22),
(16, 20, 1, 30.00, 30.00, 23),
(17, 21, 2, 40.00, 80.00, 24),
(18, 22, 1, 50.00, 50.00, 25);
select * from OrderDetail

EXEC PR_OrderDetail_Insert 
    @OrderID = 1, 
    @ProductID = 1,
    @Quantity = 2, 
    @Amount = 2.50, 
    @TotalAmount = 5.00, 
    @UserID = 1; 
select * from OrderDetail

EXEC PR_OrderDetail_Insert 
    @OrderID = 2,
    @ProductID = 2, 
    @Quantity = 3, 
    @Amount = 3.50, 
    @TotalAmount = 10.50, 
    @UserID = 2; 
select * from OrderDetail

EXEC PR_OrderDetail_Insert 
    @OrderID = 3,
    @ProductID = 3, 
    @Quantity = 4, 
    @Amount = 3.00, 
    @TotalAmount = 12.00, 
    @UserID = 3; 
select * from OrderDetail

-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------

-- BILLS
CREATE PROCEDURE [dbo].[PR_Bills_Insert]
    @BillNumber VARCHAR(100),
    @BillDate DATETIME,
    @OrderID INT,
    @TotalAmount DECIMAL(10,2),
    @Discount DECIMAL(10,2),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    INSERT INTO [Bills] (BillNumber, BillDate, OrderID, TotalAmount, Discount, NetAmount, UserID)
    VALUES (@BillNumber, @BillDate, @OrderID, @TotalAmount, @Discount, @NetAmount, @UserID);
END;
GO

-------------------------------------------------------------------------------------------------------------------------

-- Insert into Bills Table

INSERT INTO dbo.Bills (BillNumber, BillDate, OrderID, TotalAmount, Discount, NetAmount, UserID)
VALUES 

('BILL009', '2024-07-09', 15, 180.00, 9.00, 171.00, 22),
('BILL010', '2024-07-10', 14, 270.00, 13.50, 256.50, 21);
select * from Bills

EXEC PR_Bills_Insert 
    @BillNumber = 'B001', 
    @BillDate = '2024-07-31', 
    @OrderID = 13, 
    @TotalAmount = 50.00, 
    @Discount = 5.00, 
    @NetAmount = 45.00, 
    @UserID = 20; 
select * from Bills

EXEC PR_Bills_Insert 
    @BillNumber = 'B002', 
    @BillDate = '2024-07-31', 
    @OrderID = 12, 
    @TotalAmount = 75.00, 
    @Discount = 7.50, 
    @NetAmount = 67.50, 
    @UserID = 19; 
select * from Bills

EXEC PR_Bills_Insert 
    @BillNumber = 'B003', 
    @BillDate = '2024-07-31', 
    @OrderID = 11, 
    @TotalAmount = 60.00, 
    @Discount = 6.00, 
    @NetAmount = 54.00, 
    @UserID = 18; 
select * from Bills



--=======================================================================(SELECT ALL)===========================================================--


-- USER
CREATE PROCEDURE [dbo].[PR_User_SelectAll]
AS
BEGIN
    SELECT [User].[UserID], 
           [User].[UserName], 
           [User].[Email], 
           [User].[Password], 
           [User].[MobileNo], 
           [User].[Address], 
           [User].[IsActive]
    FROM [User]
    ORDER BY [User].[UserName];
END;
GO

EXEC PR_User_SelectAll;

--------------------------------------------------------------------------------------------

-- PRODUCT
alter PROCEDURE [dbo].[PR_Product_SelectAll]
AS
BEGIN
    SELECT 
			[dbo].[Product].[ProductID], 
			[dbo].[Product].[ProductName], 
			[dbo].[Product].[ProductPrice], 
			[dbo].[Product].[ProductCode], 
			[dbo].[Product].[Description],
			[dbo].[User].[UserID], 
			[dbo].[User].[UserName]
    FROM [Product]
    INNER JOIN [User] ON [Product].[UserID] = [User].[UserID]
    ORDER BY [Product].[ProductName];
END;
GO

EXEC PR_Product_SelectAll;

------------------------------------------------------------------------------------------------

-- CUSTOMERS
alter PROCEDURE [dbo].[PR_Customer_SelectAll]		
AS
BEGIN
    SELECT [Customer].[CustomerID], 
           [Customer].[CustomerName], 
           [Customer].[HomeAddress], 
           [Customer].[Email], 
           [Customer].[MobileNo], 
           [Customer].[GST_NO], 
           [Customer].[CityName], 
           [Customer].[PinCode], 
           [Customer].[NetAmount],
		   [dbo].[User].[UserID],  
           [dbo].[User].[UserName]
    FROM [Customer]
    INNER JOIN [User] ON [Customer].[UserID] = [User].[UserID]
    ORDER BY [Customer].[CustomerName];
END;
GO

EXEC PR_Customer_SelectAll;

-------------------------------------------------------------------------------------------------------

-- ORDER
alter PROCEDURE [dbo].[PR_Order_SelectAll]
AS
BEGIN
    SELECT [Order].[OrderID], 
           [Order].[OrderDate], 
           [Order].[CustomerID], 
           [Customer].[CustomerName], 
           [Order].[PaymentMode], 
           [Order].[TotalAmount], 
           [Order].[ShippingAddress],
		   [dbo].[User].[UserID], 
           [User].[UserName]
    FROM [Order]
    INNER JOIN [Customer] ON [Order].[CustomerID] = [Customer].[CustomerID]
    INNER JOIN [User] ON [Order].[UserID] = [User].[UserID]
    ORDER BY [Order].[OrderDate];
END;
GO

EXEC PR_Order_SelectAll;

------------------------------------------------------------------------------------------------------------

-- ORDERDETAILS
alter PROCEDURE [dbo].[PR_OrderDetail_SelectAll]
AS
BEGIN
    SELECT [OrderDetail].[OrderDetailID], 
           [OrderDetail].[OrderID], 
           [Order].[OrderDate], 
           [OrderDetail].[ProductID], 
           [Product].[ProductName], 
           [OrderDetail].[Quantity], 
           [OrderDetail].[Amount], 
           [OrderDetail].[TotalAmount],
		   [User].[UserID], 
           [User].[UserName]
    FROM [OrderDetail]
    INNER JOIN [Order] ON [OrderDetail].[OrderID] = [Order].[OrderID]
    INNER JOIN [Product] ON [OrderDetail].[ProductID] = [Product].[ProductID]
    INNER JOIN [User] ON [OrderDetail].[UserID] = [User].[UserID]
    ORDER BY [OrderDetail].[OrderDetailID];
END;
GO

EXEC PR_OrderDetail_SelectAll;

-------------------------------------------------------------------------------------------------------------------

-- BILLS
alter PROCEDURE [dbo].[PR_Bills_SelectAll]
AS
BEGIN
    SELECT [Bills].[BillID], 
           [Bills].[BillNumber], 
           [Bills].[BillDate], 
           [Bills].[OrderID], 
           [Order].[OrderDate], 
           [Bills].[TotalAmount], 
           [Bills].[Discount], 
           [Bills].[NetAmount],
		   [User].[UserID], 
           [User].[UserName]
    FROM [Bills]
    INNER JOIN [Order] ON [Bills].[OrderID] = [Order].[OrderID]
    INNER JOIN [User] ON [Bills].[UserID] = [User].[UserID]
    ORDER BY [Bills].[BillID];
END;
GO

EXEC PR_Bills_SelectAll;



--===============================================================================(SELECT BY ID)==================================================--


-- USER
CREATE PROCEDURE [dbo].[PR_User_SelectByPK]
    @UserID INT	
AS
BEGIN
    SELECT [User].[UserID], 
           [User].[UserName], 
           [User].[Email], 
           [User].[Password], 
           [User].[MobileNo], 
           [User].[Address], 
           [User].[IsActive]
    FROM [User]
    WHERE [User].[UserID] = @UserID
    ORDER BY [User].[UserName];
END;
GO

EXEC PR_User_SelectByPK @UserID = 1;
select * from [User]
--------------------------------------------------------------------------------------

-- PRODUCT
alter PROCEDURE [dbo].[PR_Product_SelectByPK]
    @ProductID INT
AS
BEGIN
    SELECT 
		[dbo].[Product].[ProductID], 
        [dbo].[Product].[ProductName], 
		[dbo].[Product].[ProductPrice], 
		[dbo].[Product].[ProductCode], 
		[dbo].[Product].[Description], 
		[dbo].[Product].[UserID],
		[dbo].[User].[UserName]
    FROM [dbo].[Product]
    INNER JOIN [User] ON [Product].[UserID] = [User].[UserID]
    WHERE [Product].[ProductID] = @ProductID
    ORDER BY [Product].[ProductName];
END;
GO

EXEC PR_Product_SelectByPK @ProductID = 16;


-------------------------------	--------------------------------------------------------------

-- CUSTOMER
alter PROCEDURE [dbo].[PR_Customer_SelectByPK]
    @CustomerID INT
AS
BEGIN
    SELECT [Customer].[CustomerID], 
           [Customer].[CustomerName], 
           [Customer].[HomeAddress], 
           [Customer].[Email], 
           [Customer].[MobileNo], 
           [Customer].[GST_NO], 
           [Customer].[CityName], 
           [Customer].[PinCode], 
           [Customer].[NetAmount],
		   [User].[UserID], 
           [User].[UserName]
    FROM [Customer]
    INNER JOIN [User] ON [Customer].[UserID] = [User].[UserID]
    WHERE [Customer].[CustomerID] = @CustomerID
    ORDER BY [Customer].[CustomerName];
END;
GO

EXEC PR_Customer_SelectByPK @CustomerID = 1;

----------------------------------------------------------------------------------------------------

-- ORDER
alter PROCEDURE [dbo].[PR_Order_SelectByPK]
    @OrderID INT
AS
BEGIN
    SELECT [Order].[OrderID], 
           [Order].[OrderDate], 
           [Order].[CustomerID], 
           [Customer].[CustomerName], 
           [Order].[PaymentMode], 
           [Order].[TotalAmount], 
           [Order].[ShippingAddress],
		   [User].[UserID], 
           [User].[UserName]
    FROM [Order]
    INNER JOIN [Customer] ON [Order].[CustomerID] = [Customer].[CustomerID]
    INNER JOIN [User] ON [Order].[UserID] = [User].[UserID]
    WHERE [Order].[OrderID] = @OrderID
    ORDER BY [Order].[OrderDate];
END;
GO

EXEC PR_Order_SelectByPK @OrderID = 1;

--------------------------------------------------------------------------------------------------------

-- ORDERDETAIL
alter PROCEDURE [dbo].[PR_OrderDetail_SelectByPK]
    @OrderDetailID INT
AS
BEGIN
    SELECT [OrderDetail].[OrderDetailID], 
           [OrderDetail].[OrderID], 
           [Order].[OrderDate], 
           [OrderDetail].[ProductID], 
           [Product].[ProductName], 
           [OrderDetail].[Quantity], 
           [OrderDetail].[Amount], 
           [OrderDetail].[TotalAmount],
		   [User].[UserID],
           [User].[UserName]
    FROM [OrderDetail]
    INNER JOIN [Order] ON [OrderDetail].[OrderID] = [Order].[OrderID]
    INNER JOIN [Product] ON [OrderDetail].[ProductID] = [Product].[ProductID]
    INNER JOIN [User] ON [OrderDetail].[UserID] = [User].[UserID]
    WHERE [OrderDetail].[OrderDetailID] = @OrderDetailID
    ORDER BY [OrderDetail].[OrderDetailID];
END;
GO

EXEC PR_OrderDetail_SelectByPK @OrderDetailID = 1;

-----------------------------------------------------------------------------------------------------------------

-- BILLS
alter PROCEDURE [dbo].[PR_Bills_SelectByPK]
    @BillID INT
AS
BEGIN
    SELECT [Bills].[BillID], 
           [Bills].[BillNumber], 
           [Bills].[BillDate], 
           [Bills].[OrderID], 
           [Order].[OrderDate], 
           [Bills].[TotalAmount], 
           [Bills].[Discount], 
           [Bills].[NetAmount],
		   [User].[UserID], 
           [User].[UserName]
    FROM [Bills]
    INNER JOIN [Order] ON [Bills].[OrderID] = [Order].[OrderID]
    INNER JOIN [User] ON [Bills].[UserID] = [User].[UserID]
    WHERE [Bills].[BillID] = @BillID
    ORDER BY [Bills].[BillID];
END;
GO

EXEC PR_Bills_SelectByPK @BillID = 1;


--========================================================================(UPDATE)==============================================================---


-- USER
CREATE PROCEDURE [dbo].[PR_User_Update]
    @UserID INT,
    @UserName VARCHAR(100),
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @MobileNo VARCHAR(15),
    @Address VARCHAR(100),
    @IsActive BIT
AS
BEGIN
    UPDATE [User]
    SET UserName = @UserName, 
        Email = @Email, 
        Password = @Password, 
        MobileNo = @MobileNo, 
        Address = @Address, 
        IsActive = @IsActive
    WHERE UserID = @UserID;
END;
GO

-- Update User

EXEC PR_User_UpdateByPK 
    @UserID = 1, 
    @UserName = 'Alice Johnson Updated', 
    @Email = 'alice.johnson.updated@example.com', 
    @Password = 'newpassword123', 
    @MobileNo = '1234567890', 
    @Address = '123 Main St, Cityville', 
    @IsActive = 1;

-----------------------------------------------------------------------------------------------------------

-- PRODUCT
create PROCEDURE [dbo].[PR_Product_Update]
    @ProductID INT,
    @ProductName VARCHAR(100),
    @ProductPrice DECIMAL(10,2),
    @ProductCode VARCHAR(100),
    @Description VARCHAR(100),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[Product]
    SET 
		[dbo].[Product].[ProductName] = @ProductName, 
        [dbo].[Product].[ProductPrice] = @ProductPrice, 
        [dbo].[Product].[ProductCode] = @ProductCode, 
        [dbo].[Product].[Description] = @Description, 
        [dbo].[Product].[UserID] = @UserID
    WHERE 
		[dbo].[Product].[ProductID] = @ProductID;
END;
GO

-- Update Product

EXEC PR_Product_Update
    @ProductID = 1, 
    @ProductName = 'Espresso Updated', 
    @ProductPrice = 2.75, 
    @ProductCode = 'ESP001', 
    @Description = 'Strong coffee with extra foam', 
    @UserID = 1;

----------------------------------------------------------------------------------------------------------------

-- CUSTOMER
CREATE PROCEDURE [dbo].[PR_Customer_Update]
    @CustomerID INT,
    @CustomerName VARCHAR(100),
    @HomeAddress VARCHAR(100),
    @Email VARCHAR(100),
    @MobileNo VARCHAR(15),
    @GST_NO VARCHAR(15),
    @CityName VARCHAR(100),
    @PinCode VARCHAR(15),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    UPDATE [Customer]
    SET CustomerName = @CustomerName, 
        HomeAddress = @HomeAddress, 
        Email = @Email, 
        MobileNo = @MobileNo, 
        GST_NO = @GST_NO, 
        CityName = @CityName, 
        PinCode = @PinCode, 
        NetAmount = @NetAmount, 
        UserID = @UserID
    WHERE CustomerID = @CustomerID;
END;
GO

-- Update Customer

EXEC PR_Customer_UpdateByPK 
    @CustomerID = 1, 
    @CustomerName = 'David Brown Updated', 
    @HomeAddress = '321 Oak St, Hamletville Updated', 
    @Email = 'david.brown.updated@example.com', 
    @MobileNo = '2233445566', 
    @GST_NO = 'GST001', 
    @CityName = 'Hamletville Updated', 
    @PinCode = '123456', 
    @NetAmount = 110.00, 
    @UserID = 1;

--------------------------------------------------------------------------------------------------------------------------

-- ORDER
CREATE PROCEDURE [dbo].[PR_Order_Update]
    @OrderID INT,
    @OrderDate DATETIME,
    @CustomerID INT,
    @PaymentMode VARCHAR(100),
    @TotalAmount DECIMAL(10,2),
    @ShippingAddress VARCHAR(100),
    @UserID INT
AS
BEGIN
    UPDATE [Order]
    SET OrderDate = @OrderDate, 
        CustomerID = @CustomerID, 
        PaymentMode = @PaymentMode, 
        TotalAmount = @TotalAmount, 
        ShippingAddress = @ShippingAddress, 
        UserID = @UserID
    WHERE OrderID = @OrderID;
END;
GO

-- Update Order

EXEC PR_Order_Update 
    @OrderID = 1, 
    @OrderDate = '2024-08-01', 
    @CustomerID = 1, 
    @PaymentMode = 'Credit Card Updated', 
    @TotalAmount = 55.00, 
    @ShippingAddress = '321 Oak St, Hamletville Updated', 
    @UserID = 1;

---------------------------------------------------------------------------------------------------------------------------------

-- ORDERDETAIL
CREATE PROCEDURE [dbo].[PR_OrderDetail_Update]
    @OrderDetailID INT,
    @OrderID INT,
    @ProductID INT,
    @Quantity INT,
    @Amount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    UPDATE [OrderDetail]
    SET OrderID = @OrderID, 
        ProductID = @ProductID, 
        Quantity = @Quantity, 
        Amount = @Amount, 
        TotalAmount = @TotalAmount, 
        UserID = @UserID
    WHERE OrderDetailID = @OrderDetailID;
END;
GO

-- Update OrderDetail

EXEC PR_OrderDetail_Update 
    @OrderDetailID = 1, 
    @OrderID = 1, 
    @ProductID = 1, 
    @Quantity = 3, 
    @Amount = 2.75, 
    @TotalAmount = 8.25, 
    @UserID = 1;

----------------------------------------------------------------------------------------------------------------------------

-- BILLS
CREATE PROCEDURE [dbo].[PR_Bills_Update]
    @BillID INT,
    @BillNumber VARCHAR(100),
    @BillDate DATETIME,
    @OrderID INT,
    @TotalAmount DECIMAL(10,2),
    @Discount DECIMAL(10,2),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    UPDATE [Bills]
    SET BillNumber = @BillNumber, 
        BillDate = @BillDate, 
        OrderID = @OrderID, 
        TotalAmount = @TotalAmount, 
        Discount = @Discount, 
        NetAmount = @NetAmount, 
        UserID = @UserID
    WHERE BillID = @BillID;
END;
GO

-- Update Bills

EXEC PR_Bills_UpdateByPK 
    @BillID = 1, 
    @BillNumber = 'B001 Updated', 
    @BillDate = '2024-08-01', 
    @OrderID = 1, 
    @TotalAmount = 55.00, 
    @Discount = 5.50, 
    @NetAmount = 49.50, 
    @UserID = 1;



--=======================================================================(DELETE)================================================================--


-- (6) USER
create PROCEDURE [dbo].[PR_User_Delete]
    @UserID INT
AS
BEGIN
    DELETE FROM [User]
    WHERE UserID = @UserID;
END;
GO

EXEC PR_User_Delete @UserID= 16;
select * from [User]

-----------------------------------------------------------------------------------

-- (5) PRODUCT
CREATE PROCEDURE [dbo].[PR_Product_Delete]
    @ProductID INT
AS
BEGIN
    DELETE FROM [dbo].[Product]
    WHERE [dbo].[Product].[ProductID] = @ProductID
END

EXEC PR_Product_Delete @ProductID = 16;
select * from Product

--------------------------------------------------------------------------------------

-- (4) CUSTOMER
create PROCEDURE [dbo].[PR_Customer_Delete]
    @CustomerID INT
AS
BEGIN
    DELETE FROM [Customer]
    WHERE CustomerID = @CustomerID;
END
GO

EXEC PR_Customer_Delete @CustomerID = 4;
select * from Customer

-----------------------------------------------------------------------------------------

-- (3) ORDER
create PROCEDURE [dbo].[PR_Order_Delete]
    @OrderID INT
AS
BEGIN
    DELETE FROM [Order]
    WHERE OrderID = @OrderID;
END;
GO

EXEC PR_Order_Delete @OrderID = 15;
select * from [Order]

-----------------------------------------------------------------------------------------------

-- (2) ORDERDETAIL
create PROCEDURE [dbo].[PR_OrderDetail_Delete]
    @OrderDetailID INT
AS
BEGIN
    DELETE FROM [OrderDetail]
	WHERE OrderDetailID = @OrderDetailID;

END;
GO

EXEC PR_OrderDetail_Delete @OrderDetailID = 7;
select * from OrderDetail

-------------------------------------------------------------------------------------------------

-- (1) BILLS
create PROCEDURE [dbo].[PR_Bills_Delete]
    @BillID INT
AS
BEGIN
    DELETE FROM [Bills]
	WHERE BillID = @BillID;;	
END;
GO

-- Delete Bills

EXEC PR_Bills_Delete @BillID = 25;
select * from Bills

--===========================================================(DropDown)=============================================================================

alter PROCEDURE [dbo].[PR_Customer_DropDown]
AS
BEGIN
    SELECT
		[dbo].[Customer].[CustomerID],
        [dbo].[Customer].[CustomerName]
    FROM
        [dbo].[Customer]
END

exec [dbo].[PR_Customer_DropDown]
--------------------------------------------------------------

create proc [dbo].[PR_User_DropDown]
as
begin
	select 
		[dbo].[User].[UserID],
		[dbo].[User].[UserName]
	from
		[dbo].[User]
end

exec PR_User_DropDown

------------------------------------------------------------

alter proc [dbo].[PR_Order_Dropdown]
as
begin
	select
		[dbo].[Order].[OrderID],
		[dbo].[Order].[OrderDate]
	from [dbo].[Order]
end

exec [PR_Order_Dropdown]

----------------------------------------------------------------

create procedure [dbo].[PR_Product_Dropdown]
as
begin
	select
		[dbo].[Product].[ProductID],
		[dbo].[Product].[ProductName]
	from [dbo].[Product]
end

exec [PR_Product_Dropdown]

--------------------------------------------------------------------(Register)---------------------------------------------------------------------

CREATE PROCEDURE [dbo].[PR_User_Register]
    @UserName NVARCHAR(50),
    @Password NVARCHAR(50),
    @Email NVARCHAR(500),
    @MobileNo VARCHAR(50),
    @Address VARCHAR(50)
AS
BEGIN
    INSERT INTO [dbo].[User]
    (
        [UserName],
        [Password],
        [Email],
        [MobileNo],
        [Address]
    )
    VALUES
    (
        @UserName,
        @Password,
        @Email,
        @MobileNo,
        @Address
    );
END

select * from [User]

------------------------------------------------------------(User Login)---------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[PR_User_Login]
    @UserName NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT 
        [dbo].[User].[UserID], 
        [dbo].[User].[UserName], 
        [dbo].[User].[MobileNo], 
        [dbo].[User].[Email], 
        [dbo].[User].[Password],
        [dbo].[User].[Address]
    FROM 
        [dbo].[User] 
    WHERE 
        [dbo].[User].[UserName] = @UserName 
        AND [dbo].[User].[Password] = @Password;
END