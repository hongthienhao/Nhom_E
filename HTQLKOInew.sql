CREATE DATABASE HTQLKoi;
USE HTQLKoi;

-- Xóa bảng nếu đã tồn tại để tránh lỗi khi tạo lại
DROP TABLE IF EXISTS RolePermissions;
DROP TABLE IF EXISTS Permissions;
DROP TABLE IF EXISTS EmployeePermissions;
DROP TABLE IF EXISTS Transactions;
DROP TABLE IF EXISTS Invoice;
DROP TABLE IF EXISTS Feedback;
DROP TABLE IF EXISTS DeliveryStatusHistory;
DROP TABLE IF EXISTS Schedule;
DROP TABLE IF EXISTS ServicePricing;
DROP TABLE IF EXISTS Delivery;
DROP TABLE IF EXISTS OrderDetails;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Services;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Roles;

-- Bảng Roles (Vai trò của người dùng)
CREATE TABLE Roles (
    role_id INT PRIMARY KEY IDENTITY,
    role_name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(255)
);

-- Bảng Permissions (Quyền của từng vai trò)
CREATE TABLE Permissions (
    permission_id INT PRIMARY KEY IDENTITY,
    permission_name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(255)
);

-- Bảng RolePermissions (Liên kết quyền với vai trò)
CREATE TABLE RolePermissions (
    role_permission_id INT PRIMARY KEY IDENTITY,
    role_id INT NOT NULL,
    permission_id INT NOT NULL,
    FOREIGN KEY (role_id) REFERENCES Roles(role_id),
    FOREIGN KEY (permission_id) REFERENCES Permissions(permission_id)
);

-- Bảng Users (Thông tin người dùng)
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY,
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20),
    address NVARCHAR(255),
    role_id INT, -- Liên kết với bảng Roles
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    last_login DATETIME, -- Lần đăng nhập gần nhất
    is_active BIT DEFAULT 1, -- Đánh dấu người dùng đang hoạt động hoặc không
    FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);

-- Bảng Services (Thông tin về các dịch vụ vận chuyển và bổ sung)
CREATE TABLE Services (
    service_id INT PRIMARY KEY IDENTITY,
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    price DECIMAL(18, 2) NOT NULL, -- Giá của dịch vụ
    category NVARCHAR(50) CHECK (category IN ('Vận chuyển', 'Dịch vụ bổ sung')) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Bảng ServicePricing (Lịch sử giá dịch vụ)
CREATE TABLE ServicePricing (
    pricing_id INT PRIMARY KEY IDENTITY,
    service_id INT NOT NULL, -- Liên kết với bảng Services
    effective_date DATETIME DEFAULT GETDATE(), -- Ngày bắt đầu áp dụng giá này
    price DECIMAL(18, 2) NOT NULL, -- Giá tại thời điểm này
    conditions NVARCHAR(MAX), -- Điều kiện áp dụng, nếu có
    FOREIGN KEY (service_id) REFERENCES Services(service_id)
);

-- Bảng Orders (Thông tin đơn hàng)
CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY,
    customer_id INT NOT NULL, -- Liên kết với bảng Users
    pickup_location NVARCHAR(255), -- Địa điểm xuất phát
    delivery_location NVARCHAR(255), -- Địa điểm giao
    shipping_method NVARCHAR(50) CHECK (shipping_method IN ('Hàng không', 'Đường biển', 'Đường bộ')) NOT NULL,
    total_weight FLOAT, -- Tổng khối lượng
    total_quantity INT, -- Tổng số lượng cá
    additional_services NVARCHAR(255), -- Các dịch vụ bổ sung
    order_date DATETIME DEFAULT GETDATE(),
    estimated_delivery_date DATETIME, -- Ngày dự kiến giao hàng
    status NVARCHAR(50) CHECK (status IN ('Pending', 'Shipped', 'Delivered')) NOT NULL,
    payment_method NVARCHAR(50), -- Phương thức thanh toán
    total_price DECIMAL(18, 2) DEFAULT 0, -- Tổng giá của đơn hàng
    FOREIGN KEY (customer_id) REFERENCES Users(user_id)
);

-- Bảng OrderDetails (Chi tiết từng loại cá Koi trong đơn hàng)
CREATE TABLE OrderDetails (
    order_detail_id INT PRIMARY KEY IDENTITY,
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    koi_fish_type NVARCHAR(50), -- Loại cá Koi (ví dụ: Kohaku, Sanke, Showa)
    quantity INT CHECK (quantity > 0), -- Số lượng cá
    weight FLOAT CHECK (weight > 0), -- Khối lượng cá
    price_per_unit DECIMAL(18, 2), -- Giá của từng loại cá
    total_price DECIMAL(18, 2), -- Tổng giá của từng loại cá trong đơn hàng
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

-- Bảng Delivery (Thông tin giao hàng)
CREATE TABLE Delivery (
    delivery_id INT PRIMARY KEY IDENTITY,
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    delivery_staff_id INT NOT NULL, -- Liên kết với bảng Users
    tracking_code NVARCHAR(50), -- Mã theo dõi giao hàng
    delivery_status NVARCHAR(50) CHECK (delivery_status IN ('Đang chờ', 'Đang giao', 'Đã giao')) NOT NULL,
    estimated_time DATETIME, -- Thời gian dự kiến
    actual_time DATETIME, -- Thời gian giao hàng thực tế
    delivery_address NVARCHAR(255),
    delivery_notes NVARCHAR(MAX), -- Ghi chú bổ sung cho việc giao hàng
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (delivery_staff_id) REFERENCES Users(user_id)
);

-- Bảng DeliveryStatusHistory (Lưu lịch sử trạng thái vận chuyển)
CREATE TABLE DeliveryStatusHistory (
    status_id INT PRIMARY KEY IDENTITY,
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    status NVARCHAR(255), -- Trạng thái như "Kiểm tra sức khỏe", "Đóng gói", v.v.
    status_date DATETIME DEFAULT GETDATE(),
    notes NVARCHAR(MAX), -- Ghi chú thêm nếu cần
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

-- Bảng Feedback (Phản hồi của khách hàng về dịch vụ)
CREATE TABLE Feedback (
    feedback_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL, -- Khách hàng đưa ra phản hồi
    service_id INT NOT NULL, -- Dịch vụ mà khách hàng phản hồi
    rating INT CHECK (rating BETWEEN 1 AND 5), -- Đánh giá từ 1 đến 5
    comment NVARCHAR(MAX), -- Bình luận của khách hàng
    resolved BIT DEFAULT 0, -- Đánh dấu phản hồi đã được xử lý hay chưa
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (service_id) REFERENCES Services(service_id)
);

-- Bảng Invoice (Hóa đơn cho đơn hàng)
CREATE TABLE Invoice (
    invoice_id INT PRIMARY KEY IDENTITY,
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    user_id INT NOT NULL, -- Liên kết với bảng Users
    status NVARCHAR(50) CHECK (status IN ('Đã thanh toán', 'Chưa thanh toán')) NOT NULL,
    amount_due DECIMAL(18, 2), -- Số tiền cần thanh toán
    payment_date DATETIME, -- Ngày thanh toán của hóa đơn
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

-- Bảng Transactions (Giao dịch thanh toán)
CREATE TABLE Transactions (
    transaction_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL, -- Liên kết với bảng Users
    order_id INT NOT NULL, -- Liên kết với bảng Orders
    amount DECIMAL(18, 2) NOT NULL, -- Số tiền thanh toán
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);
USE HTQLKoi;

-- Dữ liệu cho bảng Roles
INSERT INTO Roles (role_name, description) 
VALUES 
('Customer', 'Khách hàng sử dụng dịch vụ'),
('Sales Staff', 'Nhân viên bán hàng'),
('Delivery Staff', 'Nhân viên giao hàng'),
('Manager', 'Quản lý hệ thống');

-- Dữ liệu cho bảng Permissions
INSERT INTO Permissions (permission_name, description)
VALUES 
('View Orders', 'Xem đơn hàng'),
('Create Orders', 'Tạo đơn hàng'),
('Manage Users', 'Quản lý người dùng'),
('Update Delivery Status', 'Cập nhật trạng thái giao hàng');

-- Dữ liệu cho bảng RolePermissions
INSERT INTO RolePermissions (role_id, permission_id)
VALUES 
(1, 1), (1, 2), -- Khách hàng có quyền xem và tạo đơn hàng
(2, 1), (2, 2), (2, 3), -- Nhân viên bán hàng có quyền xem, tạo đơn hàng và quản lý người dùng
(3, 1), (3, 4), -- Nhân viên giao hàng có quyền xem đơn hàng và cập nhật trạng thái giao hàng
(4, 1), (4, 2), (4, 3), (4, 4); -- Quản lý có đầy đủ quyền

-- Dữ liệu cho bảng Users
INSERT INTO Users (name, email, password, phone, address, role_id, created_at, updated_at, last_login, is_active)
VALUES 
('Nguyen Van A', 'nguyenvana@example.com', 'password123', '0901234567', 'Hanoi, Vietnam', 1, GETDATE(), GETDATE(), GETDATE(), 1),
('Tran Thi B', 'tranthib@example.com', 'password456', '0912345678', 'Ho Chi Minh, Vietnam', 2, GETDATE(), GETDATE(), GETDATE(), 1),
('Le Van C', 'levanc@example.com', 'password789', '0987654321', 'Da Nang, Vietnam', 3, GETDATE(), GETDATE(), GETDATE(), 1),
('Nguyen Thi D', 'nguyenthid@example.com', 'password101', '0923456789', 'Hue, Vietnam', 4, GETDATE(), GETDATE(), GETDATE(), 1);

-- Dữ liệu cho bảng Services
INSERT INTO Services (name, description, price, category, created_at, updated_at)
VALUES 
('Vận chuyển nội địa', 'Dịch vụ vận chuyển cá Koi trong nước', 1000000, 'Vận chuyển', GETDATE(), GETDATE()),
('Vận chuyển quốc tế', 'Dịch vụ vận chuyển cá Koi ra nước ngoài', 5000000, 'Vận chuyển', GETDATE(), GETDATE()),
('Bảo hiểm', 'Dịch vụ bảo hiểm cho cá Koi', 200000, 'Dịch vụ bổ sung', GETDATE(), GETDATE()),
('Kiểm dịch', 'Dịch vụ kiểm dịch cá Koi trước khi xuất khẩu', 300000, 'Dịch vụ bổ sung', GETDATE(), GETDATE());

-- Dữ liệu cho bảng ServicePricing
INSERT INTO ServicePricing (service_id, effective_date, price, conditions)
VALUES 
(1, GETDATE(), 1000000, 'Áp dụng cho tất cả các tỉnh thành'),
(2, GETDATE(), 5000000, 'Áp dụng cho các nước Đông Nam Á'),
(3, GETDATE(), 200000, 'Bao gồm bảo hiểm cơ bản cho cá Koi nhỏ'),
(4, GETDATE(), 300000, 'Kiểm dịch tại địa phương');

-- Dữ liệu cho bảng Orders
INSERT INTO Orders (customer_id, pickup_location, delivery_location, shipping_method, total_weight, total_quantity, additional_services, order_date, estimated_delivery_date, status, payment_method, total_price)
VALUES 
(1, 'Hanoi', 'Ho Chi Minh', 'Hàng không', 10.5, 5, 'Bảo hiểm', GETDATE(), DATEADD(day, 3, GETDATE()), 'Pending', 'Credit Card', 1200000),
(2, 'Da Nang', 'Hue', 'Đường bộ', 15.0, 10, 'Kiểm dịch', GETDATE(), DATEADD(day, 5, GETDATE()), 'Shipped', 'Bank Transfer', 7500000);

-- Dữ liệu cho bảng OrderDetails
INSERT INTO OrderDetails (order_id, koi_fish_type, quantity, weight, price_per_unit, total_price)
VALUES 
(1, 'Kohaku', 3, 6.0, 200000, 600000),
(1, 'Sanke', 2, 4.5, 300000, 600000),
(2, 'Showa', 5, 10.0, 150000, 1500000),
(2, 'Kohaku', 5, 5.0, 120000, 600000);

-- Dữ liệu cho bảng Delivery
INSERT INTO Delivery (order_id, delivery_staff_id, tracking_code, delivery_status, estimated_time, actual_time, delivery_address, delivery_notes, updated_at)
VALUES 
(1, 3, 'TRACK001', 'Đang giao', DATEADD(day, 2, GETDATE()), NULL, 'Ho Chi Minh', 'Giao cẩn thận', GETDATE()),
(2, 3, 'TRACK002', 'Đã giao', DATEADD(day, 4, GETDATE()), GETDATE(), 'Hue', 'Kiểm tra kỹ trước khi giao', GETDATE());

-- Dữ liệu cho bảng DeliveryStatusHistory
INSERT INTO DeliveryStatusHistory (order_id, status, status_date, notes)
VALUES 
(1, 'Kiểm tra sức khỏe', GETDATE(), 'Cá Koi khỏe mạnh'),
(1, 'Đóng gói', DATEADD(hour, 2, GETDATE()), 'Đóng gói với túi oxy'),
(2, 'Kiểm tra sức khỏe', GETDATE(), 'Cá Koi khỏe mạnh'),
(2, 'Đóng gói', DATEADD(hour, 3, GETDATE()), 'Đóng gói trong thùng nước có điều chỉnh nhiệt độ');

-- Dữ liệu cho bảng Feedback
INSERT INTO Feedback (user_id, service_id, rating, comment, resolved, created_at)
VALUES 
(1, 1, 5, 'Dịch vụ vận chuyển nhanh và an toàn', 1, GETDATE()),
(2, 2, 4, 'Vận chuyển quốc tế tốt nhưng cần giảm giá', 0, GETDATE());

-- Dữ liệu cho bảng Invoice
INSERT INTO Invoice (order_id, user_id, status, amount_due, payment_date, created_at, updated_at)
VALUES 
(1, 1, 'Chưa thanh toán', 1200000, NULL, GETDATE(), GETDATE()),
(2, 2, 'Đã thanh toán', 7500000, GETDATE(), GETDATE(), GETDATE());

-- Dữ liệu cho bảng Transactions
INSERT INTO Transactions (user_id, order_id, amount, created_at)
VALUES 
(1, 1, 1200000, GETDATE()),
(2, 2, 7500000, GETDATE());
