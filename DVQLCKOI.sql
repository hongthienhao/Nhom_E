CREATE DATABASE HTQLKoi;
USE HTQLKoi;

-- Xóa bảng nếu đã tồn tại
DROP TABLE IF EXISTS EmployeePermissions;
DROP TABLE IF EXISTS Transactions;
DROP TABLE IF EXISTS Invoice;
DROP TABLE IF EXISTS Feedback;
DROP TABLE IF EXISTS Delivery;
DROP TABLE IF EXISTS OrderDetails;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Services;
DROP TABLE IF EXISTS Users;

-- Bảng Users
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY,
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20),
    address NVARCHAR(255),
    role NVARCHAR(50) CHECK (role IN ('Guest', 'Customer', 'Sales Staff', 'Delivering Staff', 'Manager')) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Chèn dữ liệu vào bảng Users
INSERT INTO Users (name, email, password, phone, address, role) 
VALUES 
('Nguyen Van A', 'nguyenvana@example.com', 'password123', '0901234567', 'Hanoi, Vietnam', 'Customer'),
('Tran Thi B', 'tranthib@example.com', 'password456', '0912345678', 'Ho Chi Minh, Vietnam', 'Sales Staff'),
('Le Van C', 'levanc@example.com', 'password789', '0987654321', 'Da Nang, Vietnam', 'Delivering Staff'),
('Nguyen Thi D', 'nguyenthid@example.com', 'password101', '0923456789', 'Hue, Vietnam', 'Manager');

-- Bảng Services
CREATE TABLE Services (
    service_id INT PRIMARY KEY IDENTITY,
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    price DECIMAL(18, 2) NOT NULL,
    category NVARCHAR(50) CHECK (category IN ('Nội địa', 'Quốc tế')) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Chèn dữ liệu vào bảng Services
INSERT INTO Services (name, description, price, category)
VALUES 
('Vận chuyển nội địa', 'Dịch vụ vận chuyển cá Koi trong nước', 1000000, 'Nội địa'),
('Vận chuyển quốc tế', 'Dịch vụ vận chuyển cá Koi ra nước ngoài', 5000000, 'Quốc tế'),
('Bảo hiểm', 'Dịch vụ bảo hiểm cho cá Koi', 200000, 'Nội địa'),
('Kiểm dịch', 'Dịch vụ kiểm dịch cá Koi trước khi xuất khẩu', 300000, 'Quốc tế');

-- Bảng Orders
CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY,
    customer_id INT NOT NULL, -- Liên kết với bảng Users
    pickup_location NVARCHAR(255), -- Địa điểm xuất phát
    delivery_location NVARCHAR(255), -- Địa điểm giao
    shipping_method NVARCHAR(50) CHECK (shipping_method IN ('Hàng không', 'Đường biển', 'Đường bộ')) NOT NULL,
    total_weight FLOAT, -- Tổng khối lượng
    total_quantity INT, -- Tổng số lượng cá
    additional_services NVARCHAR(255), -- Các dịch vụ gia tăng (ví dụ: bảo hiểm, kiểm dịch)
    order_date DATETIME DEFAULT GETDATE(),
    status NVARCHAR(50) CHECK (status IN ('Pending', 'Shipped', 'Delivered')) NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES Users(user_id)
);

-- Chèn dữ liệu vào bảng Orders
INSERT INTO Orders (customer_id, pickup_location, delivery_location, shipping_method, total_weight, total_quantity, additional_services, status)
VALUES 
(1, 'Hanoi', 'Ho Chi Minh', 'Hàng không', 10.5, 5, 'Bảo hiểm', 'Pending'),
(2, 'Da Nang', 'Hue', 'Đường bộ', 15.0, 10, 'Kiểm dịch', 'Shipped');

-- Bảng OrderDetails
CREATE TABLE OrderDetails (
    order_detail_id INT PRIMARY KEY IDENTITY,
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    koi_fish_type NVARCHAR(50), -- Loại cá Koi (ví dụ: Kohaku, Sanke, Showa)
    quantity INT CHECK (quantity > 0), -- Số lượng cá
    weight FLOAT CHECK (weight > 0), -- Khối lượng
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

-- Chèn dữ liệu vào bảng OrderDetails
INSERT INTO OrderDetails (order_id, koi_fish_type, quantity, weight)
VALUES 
(1, 'Kohaku', 3, 6.0),
(1, 'Sanke', 2, 4.5),
(2, 'Showa', 5, 10.0),
(2, 'Kohaku', 5, 5.0);

-- Bảng Delivery
CREATE TABLE Delivery (
    delivery_id INT PRIMARY KEY IDENTITY,
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    delivery_staff_id INT NOT NULL, -- Liên kết với bảng Users
    delivery_status NVARCHAR(50) CHECK (delivery_status IN ('Đang chờ', 'Đang giao', 'Đã giao')) NOT NULL,
    estimated_time DATETIME, -- Thời gian dự kiến
    actual_time DATETIME, -- Thời gian giao hàng thực tế
    delivery_address NVARCHAR(255),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (delivery_staff_id) REFERENCES Users(user_id)
);

-- Chèn dữ liệu vào bảng Delivery
INSERT INTO Delivery (order_id, delivery_staff_id, delivery_status, estimated_time, actual_time, delivery_address)
VALUES 
(1, 3, 'Đang giao', '2024-10-30 15:00:00', NULL, 'Ho Chi Minh'),
(2, 3, 'Đã giao', '2024-10-28 14:00:00', '2024-10-28 14:30:00', 'Hue');

-- Bảng Feedback
CREATE TABLE Feedback (
    feedback_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL, -- Khách hàng đưa ra phản hồi
    service_id INT NOT NULL, -- Dịch vụ mà khách hàng phản hồi
    rating INT CHECK (rating BETWEEN 1 AND 5), -- Đánh giá từ 1 đến 5
    comment NVARCHAR(MAX), -- Bình luận của khách hàng
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (service_id) REFERENCES Services(service_id)
);

-- Chèn dữ liệu vào bảng Feedback
INSERT INTO Feedback (user_id, service_id, rating, comment)
VALUES 
(1, 1, 5, 'Dịch vụ vận chuyển nhanh và an toàn'),
(2, 2, 4, 'Vận chuyển quốc tế tốt nhưng cần giảm giá');

-- Bảng Invoice
CREATE TABLE Invoice (
    invoice_id INT PRIMARY KEY IDENTITY,
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    user_id INT NOT NULL, -- Liên kết với bảng Users
    status NVARCHAR(50) CHECK (status IN ('Đã thanh toán', 'Chưa thanh toán')) NOT NULL,
    amount_due DECIMAL(18, 2),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

-- Chèn dữ liệu vào bảng Invoice
INSERT INTO Invoice (order_id, user_id, status, amount_due)
VALUES 
(1, 1, 'Chưa thanh toán', 1200000),
(2, 2, 'Đã thanh toán', 7500000);

-- Bảng Transactions
CREATE TABLE Transactions (
    transaction_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL, -- Liên kết với bảng Users
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    amount DECIMAL(18, 2) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

-- Chèn dữ liệu vào bảng Transactions
INSERT INTO Transactions (user_id, order_id, amount)
VALUES 
(1, 1, 1200000),
(2, 2, 7500000);

-- Bảng EmployeePermissions
CREATE TABLE EmployeePermissions (
    employee_permission_id INT PRIMARY KEY IDENTITY,
    employee_id INT NOT NULL, -- Liên kết với bảng Users
    permission NVARCHAR(255) NOT NULL, -- Quyền của nhân viên
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (employee_id) REFERENCES Users(user_id)
);

-- Chèn dữ liệu vào bảng EmployeePermissions
INSERT INTO EmployeePermissions (employee_id, permission)
VALUES 
(3, 'Giao hàng nội địa'),
(4, 'Quản lý hệ thống');
