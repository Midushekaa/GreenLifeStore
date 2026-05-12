CREATE DATABASE GreenLifeStore;
GO

USE GreenLifeStore;
GO

IF OBJECT_ID('AdminLogin', 'P') IS NOT NULL DROP PROCEDURE AdminLogin;
IF OBJECT_ID('CustomerLogin', 'P') IS NOT NULL DROP PROCEDURE CustomerLogin;
GO


IF OBJECT_ID('cart', 'U') IS NOT NULL DROP TABLE cart;
IF OBJECT_ID('low_stock_notification', 'U') IS NOT NULL DROP TABLE low_stock_notification;
IF OBJECT_ID('review', 'U') IS NOT NULL DROP TABLE review;
IF OBJECT_ID('discount', 'U') IS NOT NULL DROP TABLE discount;
IF OBJECT_ID('payment', 'U') IS NOT NULL DROP TABLE payment;
IF OBJECT_ID('payment_status', 'U') IS NOT NULL DROP TABLE payment_status;
IF OBJECT_ID('payment_type', 'U') IS NOT NULL DROP TABLE payment_type;
IF OBJECT_ID('orders', 'U') IS NOT NULL DROP TABLE orders;
IF OBJECT_ID('order_status', 'U') IS NOT NULL DROP TABLE order_status;
IF OBJECT_ID('inventory', 'U') IS NOT NULL DROP TABLE inventory;
IF OBJECT_ID('product', 'U') IS NOT NULL DROP TABLE product;
IF OBJECT_ID('product_type', 'U') IS NOT NULL DROP TABLE product_type;
IF OBJECT_ID('supplier', 'U') IS NOT NULL DROP TABLE supplier;
IF OBJECT_ID('[user]', 'U') IS NOT NULL DROP TABLE [user];
GO

CREATE TABLE [user] (
    id INT IDENTITY(1,1) PRIMARY KEY,
    full_name VARCHAR(255) NOT NULL,
    user_name VARCHAR(255),
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(50) NOT NULL, -- ADMIN or CUSTOMER
    address VARCHAR(255),
    phone VARCHAR(50),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE supplier (
    id INT IDENTITY(1,1) PRIMARY KEY,
    supplier_name VARCHAR(255) NOT NULL,
    contact_person VARCHAR(255),
    phone VARCHAR(50),
    email VARCHAR(255),
    address VARCHAR(255),
    distance_type VARCHAR(20), -- LONG or SHORT
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE product_type (
    id INT IDENTITY(1,1) PRIMARY KEY,
    type_name VARCHAR(255) NOT NULL,
    description TEXT,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE product (
    id INT IDENTITY(1,1) PRIMARY KEY,
    supplier_id INT NOT NULL,
    product_type_id INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    category VARCHAR(100),
    price DECIMAL(10,2) NOT NULL,
    discount DECIMAL(5,2) DEFAULT 0,
    rating FLOAT DEFAULT 0,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (supplier_id) REFERENCES supplier(id),
    FOREIGN KEY (product_type_id) REFERENCES product_type(id)
);
GO

CREATE TABLE inventory (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    quantity_in_stock INT NOT NULL DEFAULT 0,
    reserved_quantity INT DEFAULT 0,
    damaged_quantity INT DEFAULT 0,
    reorder_level INT NOT NULL,
    reorder_quantity INT,
    batch_number VARCHAR(100),
    expiry_date DATE,
    stock_status VARCHAR(50),
    last_stock_in_date DATETIME,
    last_stock_out_date DATETIME,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES product(id)
);
GO

CREATE TABLE order_status (
    id INT IDENTITY(1,1) PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL
);
GO

CREATE TABLE orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    order_status_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),
    total_amount DECIMAL(10,2) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (customer_id) REFERENCES [user](id),
    FOREIGN KEY (order_status_id) REFERENCES order_status(id),
    FOREIGN KEY (product_id) REFERENCES product(id)
);



CREATE TABLE payment_type (
    id INT IDENTITY(1,1) PRIMARY KEY,
    payment_name VARCHAR(50) NOT NULL
);
GO

CREATE TABLE payment_status (
    id INT IDENTITY(1,1) PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL
);
GO

CREATE TABLE payment (
    id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT UNIQUE NOT NULL,
    payment_type_id INT NOT NULL,
    payment_status_id INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    transaction_ref VARCHAR(100),
    payment_date DATETIME DEFAULT GETDATE(),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES orders(id),
    FOREIGN KEY (payment_type_id) REFERENCES payment_type(id),
    FOREIGN KEY (payment_status_id) REFERENCES payment_status(id)
);
GO

CREATE TABLE discount (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    discount_percentage DECIMAL(5,2) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES product(id)
);
GO

CREATE TABLE review (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    customer_id INT NOT NULL,
    rating INT NOT NULL,
    comment TEXT,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES product(id),
    FOREIGN KEY (customer_id) REFERENCES [user](id)
);
GO

CREATE TABLE low_stock_notification (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    message VARCHAR(255),
    notified_at DATETIME,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES product(id)
);
GO


CREATE TABLE cart (
    id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (customer_id) REFERENCES [user](id),
    FOREIGN KEY (product_id) REFERENCES product(id)
);
GO

-- Create a default admin user if not exists
IF NOT EXISTS (SELECT 1 FROM [user] WHERE role='ADMIN')
BEGIN
    DECLARE @HashPassword NVARCHAR(64);
    SET @HashPassword = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Admin@123'), 2);

    INSERT INTO [user] (full_name, user_name, email, password, role)
    VALUES ('Default Admin', 'admin', 'admin@greenlife.com', @HashPassword, 'ADMIN');
END
GO

-- Admin Login Procedure
CREATE PROCEDURE AdminLogin
    @UserInput NVARCHAR(255),
    @PasswordInput NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PasswordHash NVARCHAR(64) = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', @PasswordInput), 2);

    IF EXISTS (
        SELECT 1 FROM [user]
        WHERE (UPPER(user_name) = UPPER(@UserInput) OR UPPER(email) = UPPER(@UserInput))
          AND password = @PasswordHash
          AND role = 'ADMIN'
    )
    BEGIN
        SELECT 'Login Successful' AS Message;
        SELECT id, full_name, user_name, email
        FROM [user]
        WHERE (UPPER(user_name) = UPPER(@UserInput) OR UPPER(email) = UPPER(@UserInput))
          AND password = @PasswordHash
          AND role = 'ADMIN';
    END
    ELSE
    BEGIN
        SELECT 'Login Failed: Invalid credentials or not an admin' AS Message;
    END
END;
GO


-- Customer Login Procedure
CREATE PROCEDURE CustomerLogin
    @UserInput NVARCHAR(255),
    @PasswordInput NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PasswordHash NVARCHAR(64) = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', @PasswordInput), 2);

    IF EXISTS (
        SELECT 1 FROM [user]
        WHERE (UPPER(user_name) = UPPER(@UserInput) OR UPPER(email) = UPPER(@UserInput))
          AND password = @PasswordHash
          AND role = 'CUSTOMER'
    )
    BEGIN
        SELECT 'Login Successful' AS Message;
        SELECT id, full_name, user_name, email
        FROM [user]
        WHERE (UPPER(user_name) = UPPER(@UserInput) OR UPPER(email) = UPPER(@UserInput))
          AND password = @PasswordHash
          AND role = 'CUSTOMER';
    END
    ELSE
    BEGIN
        SELECT 'Login Failed: Invalid credentials or not a customer' AS Message;
    END
END;
GO


SELECT * FROM [user];
SELECT * FROM supplier;
SELECT * FROM product_type;
SELECT * FROM product;
SELECT * FROM payment_type;
SELECT * FROM payment_status;
SELECT * FROM Order_Status;
SELECT * FROM payment;
SELECT * FROM inventory;
SELECT o.id, u.full_name AS Customer, p.name AS Product, o.quantity, o.price, os.status_name AS OrderStatus, o.total_amount, o.order_date
FROM orders o
JOIN [user] u ON o.customer_id = u.id
JOIN product p ON o.product_id = p.id
JOIN order_status os ON o.order_status_id = os.id;

SELECT pay.id, o.id AS OrderID, u.full_name AS Customer, pt.payment_name, ps.status_name AS PaymentStatus, pay.amount, pay.transaction_ref
FROM payment pay
JOIN orders o ON pay.order_id = o.id
JOIN [user] u ON o.customer_id = u.id
JOIN payment_type pt ON pay.payment_type_id = pt.id
JOIN payment_status ps ON pay.payment_status_id = ps.id;

SELECT d.id, p.name AS Product, d.discount_percentage, d.start_date, d.end_date
FROM discount d
JOIN product p ON d.product_id = p.id;

SELECT r.id, u.full_name AS Customer, p.name AS Product, r.rating, r.comment
FROM review r
JOIN [user] u ON r.customer_id = u.id
JOIN product p ON r.product_id = p.id;

SELECT lsn.id, p.name AS Product, lsn.message, lsn.notified_at
FROM low_stock_notification lsn
JOIN product p ON lsn.product_id = p.id;


SELECT c.id, u.full_name AS Customer, p.name AS Product, c.quantity
FROM cart c
JOIN [user] u ON c.customer_id = u.id
JOIN product p ON c.product_id = p.id;


ALTER TABLE product
ADD image VARBINARY(MAX) NULL;  