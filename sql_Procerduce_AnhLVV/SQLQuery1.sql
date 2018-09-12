use ShoesProject
go

create proc selectAllAdmins
as
begin
select * from Admins
end
go

create proc selectAllBrands
as
begin
select * from Brands
end
go

create proc selectAllBusinesses
as
begin
select * from Businesses
end
go

create proc selectAllCategories
as
begin
select * from Categories
end
go

create proc selectAllColors
as
begin
select * from Colors
end
go

create proc selectAllOrders
as
begin
select * from Orders
end
go

create proc selectAllOrdersDetail
as
begin
select * from OrdersDetail
end
go

create proc selectAllPermissons
as
begin
select * from Permissons
end
go

create proc selectAllProductImages
as
begin
select * from ProductImages
end
go

create proc selectAllProducts
as
begin
select * from Products
end
go

create proc selectAllSizes
as
begin
select * from Sizes
end
go

create proc selectAllUsers
as
begin
select * from Users
end
go

create proc selectAllVouchers
as
begin
select * from Vouchers
end
go

/* delete procerduce */

create proc deleteAdmins @id int
as
begin
delete Admins where AdminId = @id
end
go

create proc deleteBrands @id int
as
begin
delete Brands where BrandId = @id
end
go

create proc deleteBusinesses @id int
as
begin
delete Businesses where BusinessId = @id
end
go

create proc deleteCategories @id int
as
begin
delete Categories where CategoryId = @id
end
go

create proc deleteColors @id int
as
begin
delete Colors where ColorId = @id
end
go

create proc deleteOrders @id int
as
begin
delete Orders where OrderId = @id
end
go

/*create proc deleteOrdersDetail @id int
as
begin
select * from OrdersDetail
delete OrdersDetail where Order = @id
end
go*/

create proc deletePermissons @id int
as
begin
delete Permissons where PermissonId = @id
end
go

/* create proc deleteProductImages @id int
as
begin
select * from ProductImages
delete ProductImages where Product = @id
end
go */

create proc deleteProducts @id int
as
begin
delete Products where ProductId = @id
end
go

create proc deleteSizes @id int
as
begin
delete Sizes where SizeId = @id
end
go

create proc deleteUsers @id int
as
begin
delete Users where UserId = @id
end
go

create proc deleteVouchers @id int
as
begin
delete Vouchers where VoucherId = @id
end
go

/* deleteAllProcerduce */

create proc deleteAllAdmins
as
begin
truncate table Admins
end
go

create proc deleteAllBrands
as
begin
truncate table Brands
end
go

create proc deleteAllBusinesses
as
begin
truncate table Businesses
end
go

create proc deleteAllCategories
as
begin
truncate table Categories
end
go

create proc deleteAllColors
as
begin
truncate table Colors
end
go

create proc deleteAllOrders
as
begin
truncate table Orders
end
go

create proc deleteAllPermissons
as
begin
truncate table Permissons
end
go

create proc deleteAllProducts
as
begin
truncate table Products
end
go

create proc deleteAllSizes
as
begin
truncate table Sizes
end
go

create proc deleteAllUsers
as
begin
truncate table Users
end
go

create proc deleteAllVouchers
as
begin
truncate table Vouchers
end
go



/* InsertProc */

create proc insertAdmins
	@password text,
	@username varchar(14),
	@isDeleted bit,
	@Disabled bit,
	@isSuper bit
as
begin
insert into Admins values (
	@password,
	@username,
	@isDeleted,
	@Disabled,
	@isSuper
)
end
go

create proc insertBrands
@name nvarchar(100),
@image text
as
begin
insert into Brands values (
	@name,
	@image
)
end
go

create proc insertBusinesses 
@name nvarchar(256)
as
begin
insert into Businesses(BusinessName) values (
	@name
)
end
go

create proc insertCategories
@status bit,
@parenId int,
@name nvarchar(64)
as
begin
insert into Categories values (
@status,
@parenId,
@name
)
end
go

create proc insertColors 
@value varchar(10),
@code varchar(10),
@status bit
as
begin
insert into Colors values (
@value,
@code,
@status
)
end
go

create proc insertOrders
@userId int,
@voucherId int,
@address text,
@phone varchar(20),
@progressStatus int,
@orderStatus bit
as
begin
insert into Orders values (
@userId,
@voucherId,
@address,
@phone,
@progressStatus,
@orderStatus
)
end
go

create proc insertPermissons
@name varchar(256),
@description ntext,
@businessId varchar(64)
as
begin
insert into Permissons values (
@name,
@description,
@businessId
)
end
go

create proc insertProducts
@name nvarchar(200),
@categoryId int,
@instock int,
@brandId int,
@price int,
@description ntext,
@discount int
as
begin
insert into Products values (
	@name,
	@categoryId,
	@instock,
	@brandId,
	@price,
	@description,
	@discount
)
end
go

create proc insertSizes
	@value varchar(10),
	@status bit
as
begin
insert into Sizes values (
	@value,
	@status
	)
end
go

create proc insertUsers
	@username nvarchar(100),
	@password text,
	@email varchar(100),
	@status bit
as
begin
insert into Users values (
	@username,
	@password,
	@email,
	@status
)
end
go

create proc insertVouchers
@name nvarchar(100),
@code varchar(20),
@discountPercent int,
@remain int,
@expiredAt date
as
begin
insert into Vouchers values (
	@name,
	@code,
	@discountPercent,
	@remain,
	@expiredAt
)
end
go

/* updateProc */

create proc updateAdmins
	@id int,
	@password text,
	@username varchar(14),
	@isDeleted bit,
	@Disabled bit,
	@isSuper bit
as
begin
update Admins set
	AdminPassword = @password,
	AdminUsername = @username,
	AdminIsDeleted = @isDeleted,
	AdminIsDisabled = @Disabled,
	IsSuper = @isSuper
	where AdminId = @id
end
go

create proc updateBrands @id int,
@name nvarchar(100),
@image text
as
begin
update Brands set 
BrandName =	@name,
BrandImage =	@image
where BrandId = @id
end
go

create proc updateBusinesses @id int, @name nvarchar(256)
as
begin
update Businesses set 
BusinessId =	@name where BusinessId = @id
end
go

create proc updateCategories @id int,
@status bit,
@parenId int,
@name nvarchar(64)
as
begin
update Categories set
CategoryStatus = @status,
CategoryParentId = @parenId,
CategoryName = @name
where CategoryId = @id
end
go

create proc updateColors  @id int,
@value varchar(10),
@code varchar(10),
@status bit
as
begin
update Colors set
ColorValue = @value,
ColorCode = @code,
ColorStatus = @status
where ColorId = @id
end
go

create proc updateOrders @id int,
@userId int,
@voucherId int,
@address text,
@phone varchar(20),
@progressStatus int,
@orderStatus bit
as
begin
update Orders set
UserId = @userId,
VoucherId = @voucherId,
Address = @address,
PhoneNumber = @phone,
ProgressStatus = @progressStatus,
OrderStatus = @orderStatus
where OrderId = @id
end
go

create proc updatePermissons @id int,
@name varchar(256),
@description ntext,
@businessId varchar(64)
as
begin
update Permissons set
PermissonName =	@name,
PermissonDescription =	@description,
BusinessId =	@businessId
where PermissonId = @id
end
go

create proc updateProducts @id int,
@name nvarchar(200),
@categoryId int,
@instock int,
@brandId int,
@price int,
@description ntext,
@discount int
as
begin
update Products set 
ProductName =	@name,
CategoryId =	@categoryId,
Instock =	@instock,
BrandId =	@brandId,
ProductPrice =	@price,
ProductDescription =	@description,
ProductDiscount = @discount
where ProductId = @id

end
go

create proc updateSizes @id int,
	@value varchar(10),
	@status bit
as
begin
update Sizes set
SizeValue =	@value,
SizeStatus =	@status
	where SizeId = @id
end
go

create proc updateUsers @id int,
	@username nvarchar(100),
	@password text,
	@email varchar(100),
	@status bit
as
begin
update Users set
UserName =	@username,
Password =	@password,
Email =	@email,
UserStatus =	@status
where UserId = @id
end
go

create proc updateVouchers @id int,
@name nvarchar(100),
@code varchar(20),
@discountPercent int,
@remain int,
@expiredAt date
as
begin
update Vouchers set
VoucherName =	@name,
Code =	@code,
DiscountPercent =	@discountPercent,
Remain =	@remain,
ExpiredAt =	@expiredAt
where VoucherId = @id
end
go

/* findByNameProc */

create proc findByNameAdmins
	@username varchar(14)
as
begin
select * from Admins where 
AdminUsername like '%' + @username + '%'
end
go

create proc findByNameBrands
@name nvarchar(100)
as
begin
select * from Brands where 
BrandName like '%' +	@name + '%'
end
go

create proc findByNameBusinesses 
@name nvarchar(256)
as
begin
select * from Businesses where 
BusinessName like '%' +	@name + '%'
end
go

create proc findByNameCategories
@name nvarchar(64)
as
begin
select * from Categories where
CategoryName like '%' +	@name + '%'
end
go

create proc findByNameColors
@value varchar(10),
@code varchar(10)
as
begin
select * from Colors where
ColorValue  like '%' +	@value + '%' or
ColorCode  like '%' +	@code + '%'
end
go

/* create proc findByNameOrders
@address text,
@phone varchar(20),
@username varchar(100)
as
begin
select * from Orders , Users where 
Address  like '%' +	@address + '%' or
PhoneNumber  like '%' +	@phone + '%' or
UserName  like '%' +	@username + '%'
end
go */

create proc findByNamePermissons
@name varchar(256)
as
begin
select * from Permissons where
PermissonName like '%' +	@name + '%'
end
go

create proc findByNameProducts
@name nvarchar(200)
as
begin
select * from Products where 
ProductName like '%' +	@name + '%'

end
go

create proc findByNameSizes
	@value varchar(10)
as
begin
select * from  Sizes where
SizeValue like '%' +	@value + '%'
end
go

create proc findByNameUsers
	@username nvarchar(100)
as
begin
select * from Users where
UserName like '%' +	@username + '%'
end
go

create proc findByNameVouchers
@name nvarchar(100)
as
begin
select * from  Vouchers where
VoucherName like '%' +	@name + '%'
end
go

create proc checkLoginUsers
	@username nvarchar(100),
	@password text
as
begin
select * from Users where
UserName like	@username and
Password like @password
end
go