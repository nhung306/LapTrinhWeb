create database QLBANHOA1
go

use QLBANHOA1
go
 
DROP DATABASE QLBANHOA
GO


create table KHACHHANG
(
	MAKH INT IDENTITY(1,1) PRIMARY KEY,
	HOTEN nVarchar(50) NOT NULL,
	TAIKHOAN varchar(50) UNIQUE,
	MATKHAU varchar(50) NOT NULL,
	EMAIL Varchar(100) UNIQUE,  
	DIACHIKH nVarchar(200),  
	DIENTHOAIKH Varchar(50),   
	NGAYSINH DATETIME 
)

INSERT INTO KHACHHANG  VALUES (N'Việt Trạch','vt122', 'trach5643',N'Trach254@gmail.com', N'2Bis, q1,tp.hcm','0376158462','1994/05/14')
INSERT INTO KHACHHANG  VALUES (N'Thẩm Tịch Nguyệt','anglela','a23165',N'thuantan@gmail.com',N'79A Hàm Nghi, phường Nguyễn Thái Bình, Quận 1, Thành Phố Hồ Chí Minh','0677735880','1990/03/25')
INSERT INTO KHACHHANG  VALUES (N'Tiêu Quân Nhã','nhanha','nha2367',N'nhannha@gmail.com',N' KCN Hiệp Phước, xã Long Thới, Huyện Nhà Bè, Thành Phố Hồ Chí Minh','0885647125','2000/12/24')
INSERT INTO KHACHHANG  VALUES (N'Từ Vi Vũ','tuvivuu','vuvu123',N'vune@gmail.com',N'Cao ốc Estar Building số 147 -149 Võ Văn Tần, phường 6, quận 3, Thành Phố Hồ Chí Minh','0376958412','2001/06/05')
INSERT INTO KHACHHANG  VALUES (N'Nguyễn Minh Lan','mlan23','abc235',N'lannhi@gmail.com',N'44 Nguyễn Duy Trinh, phường Bình Trưng Tây, Quận 2, Thành Phố Hồ Chí Minh','0765569512','1993/11/8')


CREATE TABLE LOAIHOA
(
	MALOAI  INT IDENTITY(1,1) PRIMARY KEY,
	TENLOAI NVARCHAR(100) NOT NULL,
	MAKIEU INT	
)
insert into LOAIHOA values(N'Hoa sinh nhật',1)
insert into LOAIHOA values (N'Hoa tình yêu',2)
insert into LOAIHOA values (N'Hoa tặng mẹ',1)
insert into LOAIHOA values (N'Hoa chúc sức khỏe',2)
insert into LOAIHOA values (N'Hoa giáng sinh',3)
insert into LOAIHOA values (N'Hoa chia buồn',3)

CREATE TABLE KIEUHOA
(
	MAKIEU INT IDENTITY(1,1) PRIMARY KEY,
	KIEU NVARCHAR(50) NOT NULL
)


insert into KIEUHOA values(N'Bó hoa')
insert into KIEUHOA values(N'Giỏ hoa')
insert into KIEUHOA values(N'Lẵng hoa')
insert into KIEUHOA values(N'Hộp hoa')

CREATE TABLE NCC
(
	MANCC INT IDENTITY(1,1) PRIMARY KEY,
	TENNCC NVARCHAR(50) NOT NULL,
	DIENTHOAI VARCHAR(50),
	DIACHI NVARCHAR(100)	
)
INSERT INTO NCC VALUES ( N'Hoa tươi giá vườn','0944432959',N'Lê Hồng Phong, P. 1, Q. 10, Tp. Hồ Chí Minh')
INSERT INTO NCC VALUES ( N'Đà Lạt Hasfarm','0944432959',N'450 Nguyên Tử Lực, Đà Lạt, Lâm Đồng')
INSERT INTO NCC VALUES (N'Đà Lạt Flower','0974113068',N'Nhà vườn Hải Hạnh, Ngã Ba Dốc Trời, Làng hoa Vạn Thành,Đà Lạt')
insert into NCC values ( N'Hoa yêu thương','588323098', N'12/7 Xô Viết Nghệ Tĩnh, Bình Thạnh, TPHCM')
insert into NCC values ( N'Hoa gió','768451325', N'112 Kha Vạn Cân, Thủ Đức, TPHCM')

CREATE TABLE DONDAT
(
	MADONDAT INT IDENTITY(1,1) PRIMARY KEY,  
	THANHTOAN BIT,  
	TINHTRANGGIAOHANG BIT,  
	NGAYDAT DATETIME,  
	NGAYGIAO Datetime,   
	MAKH INT
)
ALTER TABLE DONDAT ADD FOREIGN KEY(MAKH) REFERENCES KHACHHANG(MAKH)

set dateformat mdy
insert into DONDAT values (1,1,'07/03/2019','08/03/2019',2)
insert into DONDAT values (0,1,'03/03/2019','08/03/2019',5)
insert into DONDAT values (1,0,'06/03/2019','08/03/2019',3)
insert into DONDAT values (0,0,'05/03/2019','11/03/2019',1)

CREATE TABLE HOATUOI
(
	MAHOA INT IDENTITY(1,1) PRIMARY KEY,  
	TENHOA NVARCHAR(100) NOT NULL,  	
	MANCC INT	
)


insert into HOATUOI values (N'Cẩm chướng',1)
insert into HOATUOI values (N'Hoa hồng',3)
insert into HOATUOI values (N'Lily',1)
insert into HOATUOI values (N'Rossi',2)
insert into HOATUOI values (N'Tulip',4)
insert into HOATUOI values (N'Cát tường',5)
insert into HOATUOI values (N'Hoa cúc',3)
insert into HOATUOI values (N'Hoa gấu',4)
CREATE TABLE SANPHAM
(
	MASP INT IDENTITY(1,1) PRIMARY KEY,
	MAHOA INT,
	MAKIEU INT,
	MALOAI INT,
	TENSP NVARCHAR(100) NOT NULL,
	GIABAN Decimal(15,0) CHECK (GIABAN>=0),  
	ANH VARCHAR(50),  
	NGAYNHAP DATETIME,  
	SLTON INT,
	MANCC INT	   
)
ALTER TABLE SANPHAM ADD FOREIGN KEY(MAHOA) REFERENCES HOATUOI(MAHOA)
ALTER TABLE SANPHAM ADD FOREIGN KEY(MANCC) REFERENCES NCC(MANCC)
ALTER TABLE SANPHAM ADD FOREIGN KEY(MAKIEU) REFERENCES KIEUHOA(MAKIEU)
ALTER TABLE SANPHAM ADD FOREIGN KEY(MALOAI) REFERENCES LOAIHOA(MALOAI)

insert into SANPHAM values (1,1,2,N'Lan châu', 250000,N'c1.jpg','10/2/2019',2,1)
insert into SANPHAM values (2,2,3,N'Ngọc cầm', 428000,N'h1.jpg','9/2/2019',5,2)
insert into SANPHAM values (3,3,4,N'Băng băng', 379000,N'l1.jpg','4/2/2019',20,1)
INSERT INTO SANPHAM VALUES (4,4,5,N'Như bảo',150000,N'hr.jpg','2019/04/03',10,4)
INSERT INTO SANPHAM VALUES (5,1,6,N'Phồn vinh',120000,N'ht.jpg','2019/04/03',50,5)
INSERT INTO SANPHAM VALUES (6,2,1,N'Tươi sáng',240000,N'hct.jpg','2019/07/03',20,3)
INSERT INTO SANPHAM VALUES (7,3,1,N'Vạn sự',230000,N'hc.jpg','2019/07/03',20,3)
INSERT INTO SANPHAM VALUES (8,2,2,N'Ngàn lời yêu',400000,N'hg.jpg','10/07/2019',10,3)
insert into SANPHAM values (1,3,3,N'Đam mê', 250000,N'c2.jpg','10/2/2019',2,1)
insert into SANPHAM values (2,4,5,N'Chẳng thể nói lời yêu', 428000,N'h2.jpg','9/2/2019',5,2)
insert into SANPHAM values (3,1,4,N'Bao xa', 379000,N'l2.jpg','4/2/2019',20,1)
INSERT INTO SANPHAM VALUES (4,2,6,N'Chân thành',150000,N'hr2.jpg','2019/04/03',10,4)
INSERT INTO SANPHAM VALUES (5,3,1,N'Biệt ly',120000,N'ht2.jpg','2019/04/03',50,5)
INSERT INTO SANPHAM VALUES (6,4,2,N'Thành công',240000,N'hct2.jpg','2019/07/03',20,3)
INSERT INTO SANPHAM VALUES (8,1,5,N'Lời yêu',400000,N'hg2.jpg','10/07/2019',10,3)

CREATE TABLE MEANS
(
	MAHOA INT PRIMARY KEY,
	TENHOA NVARCHAR(100) NOT NULL,
	MEAN NVARCHAR(300) NOT NULL
)
ALTER TABLE MEANS ADD FOREIGN KEY(MAHOA) REFERENCES HOATUOI(MAHOA)
INSERT INTO MEANS VALUES(1,N'Cẩm chướng',N'Hoa cẩm chướng là một trong những loài hoa đẹp được nhiều người yêu thích và đã xuất hiện từ lâu đời. Tuy nhiên, với ý nghĩa tượng trưng của mình, hoa cẩm chướng phần lớn được dành tặng cho bà , cho mẹ hoặc cho bạn bè. Rất ít người dùng hoa cẩm chướng để dành tặng người yêu.')
INSERT INTO MEANS VALUES(2,N'Hoa hồng',N'Trong ngày lễ tình nhân hay mỗi dịp kỉ niệm tình yêu, hoa hồng là một món quà không thể thiếu. Bởi lẽ đó là loại hoa tượng trưng cho tình yêu được nhiều người biết đến nhất trên thế giới, là loại hoa ngọt ngào, nồng thắm mà tình yêu đã len lỏi vào từng cánh hoa...')
INSERT INTO MEANS VALUES(3,N'Lily',N'Hoa Ly từ lâu đã được mệnh danh là một loài hoa thanh cao và quý phái, nó không những tượng trưng cho sắc đẹp, đức hạnh mà còn là sự kiêu hãnh và cả tình yêu cao thượng, chung thủy. Chính bởi vậy, hoa Ly không chỉ thích hợp để dành tặng mẹ, người yêu...')
INSERT INTO MEANS VALUES(4,N'Rossi',N'Hoa Rossi được xem là loài hoa của sự vương giả, phú quý và giàu sang. Chính bởi vậy, mẫu đơn không chỉ được người ta ưa chuộng bởi vẻ đẹp, khí chất đặc biệt mà còn bởi những ý nghĩa tốt lành mà nó mang lại.')
INSERT INTO MEANS VALUES(5,N'Tulip',N' Hoa Tulip tượng trưng cho sự nổi tiếng, giàu có và tình yêu hoàn hảo. Có lẽ vì nó nở vào mùa xuân, khi bóng tối của những ngày đông đã bị xóa nhòa, Tulip còn trở thành biểu tượng cho cuộc sống vĩnh hằng.')
INSERT INTO MEANS VALUES(6,N'Cát tường',N'Cát Tường là loài hoa được đa số người Á Đông rất yêu thích vì tên của nó có nghĩa là "May mắn". Ở hoa Cát Tường người ta dễ dàng cảm nhận được một vẻ đẹp nhẹ nhàng, ngọt ngào và quý phái.')
INSERT INTO MEANS VALUES(7,N'Hoa cúc',N'Hoa cúc - loài hoa được người ta yêu mến bởi vẻ mộc mạc, giản đơn và biết bao gần gũi. Không chỉ là nét ngây thơ trong sáng của những đóa cúc nhỏ nhắn, trắng xinh; mà còn là niềm vui tươi, rạng rỡ của những đóa cúc đa sắc màu...')
INSERT INTO MEANS VALUES(8,N'Hoa gấu',N'Hoa gấu một loài hoa tình yêu luôn hướng về phía mặt trời nên thường là biểu tượng của lòng trung thành, chung thủy sâu sắc, sự kiên định đó cũng biểu thị cho sức mạnh, uy quyền, sự ấm áp, nuôi dưỡng (tất cả những thuộc tính của mặt trời)...')

CREATE TABLE CTDONDAT
(
	MADONDAT INT PRIMARY KEY,  
	MASP INT ,	
	SL Int Check(SL>0),  
	DONGIA DECIMAL(15,0) Check(Dongia>=0)
)
ALTER TABLE CTDONDAT ADD FOREIGN KEY(MASP) REFERENCES SANPHAM(MASP)
ALTER TABLE CTDONDAT ADD FOREIGN KEY(MADONDAT) REFERENCES DONDAT(MADONDAT)

insert into CTDONDAT values (1,4,1,'400000')
insert into CTDONDAT values (2,3,3,'1137000')

create table ADMIN
( 
	USERADMIN VARCHAR(30) PRIMARY KEY,
	PASSADMIN VARCHAR(30) NOT NULL,
	HOTEN NVARCHAR(50)
)
INSERT INTO ADMIN VALUES ('user','123456',N'Thanh Nhung')
INSERT INTO ADMIN VALUES ('admin','230698',N'Trịnh Thị Thanh Nhung')