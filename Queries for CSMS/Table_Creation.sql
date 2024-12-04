-- Create the Database
-- CSMS : Coffee Shop Management System

CREATE DATABASE CSMS;
GO
USE CSMS;
GO



-- Create Tables

-- Table: Product

CREATE TABLE Product (
    ProductID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    ProductPrice DECIMAL(10,2) NOT NULL,
    ProductCode VARCHAR(100) NOT NULL,
    Description VARCHAR(100) NOT NULL,
    UserID INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);



-- Table: User

CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    MobileNo VARCHAR(15) NOT NULL,
    Address VARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL
);



-- Table: Order

CREATE TABLE [Order] (
    OrderID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    OrderDate DATETIME NOT NULL,
    CustomerID INT NOT NULL FOREIGN KEY REFERENCES Customer(CustomerID),
    PaymentMode VARCHAR(100) NULL,
    TotalAmount DECIMAL(10,2) NULL,
    ShippingAddress VARCHAR(100) NOT NULL,
    UserID INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);



-- Table: OrderDetail

CREATE TABLE OrderDetail (
    OrderDetailID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    OrderID INT NOT NULL FOREIGN KEY REFERENCES [Order](OrderID),
    ProductID INT NOT NULL FOREIGN KEY REFERENCES Product(ProductID),
    Quantity INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    UserID INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);



-- Table: Bills

CREATE TABLE Bills (
    BillID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    BillNumber VARCHAR(100) NOT NULL,
    BillDate DATETIME NOT NULL,
    OrderID INT NOT NULL FOREIGN KEY REFERENCES [Order](OrderID),
    TotalAmount DECIMAL(10,2) NOT NULL,
    Discount DECIMAL(10,2) NULL,
    NetAmount DECIMAL(10,2) NOT NULL,
    UserID INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);



-- Table: Customer

CREATE TABLE Customer (
    CustomerID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CustomerName VARCHAR(100) NOT NULL,
    HomeAddress VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    MobileNo VARCHAR(15) NOT NULL,
    GST_NO VARCHAR(15) NOT NULL,
    CityName VARCHAR(100) NOT NULL,
    PinCode VARCHAR(15) NOT NULL,
    NetAmount DECIMAL(10,2) NOT NULL,
    UserID INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);



