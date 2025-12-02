ProjectForumAPI – ASP.NET Core 6 Forum API

Tóm tắt
- API diễn đàn (bài viết, người dùng, ảnh) xây dựng bằng ASP.NET Core 6 + Entity Framework Core (SQL Server), ASP.NET Identity, JWT, Swagger.
- Chức năng chính: đăng ký/đăng nhập, xác thực JWT, quản lý bài viết theo danh mục, duyệt bài (Admin), tải ảnh, xem thông tin và cập nhật hồ sơ người dùng.

Kiến trúc & Công nghệ
- Runtime: .NET 6 (ASP.NET Core Web API + Razor Pages cho một số trang).
- ORM: EF Core `SqlServer`, DbContext: `DatabaseContext` với bảng `Articles` và Identity.
- Identity & Auth: ASP.NET Identity + JWT Bearer; Roles: `User`, `Admin`.
- API Docs: Swagger (`/swagger`), bảo vệ bằng Bearer token.
- Khác: AutoMapper, FluentValidation, Serilog, MailKit/SendGrid (gửi email), CORS.

Cấu trúc thư mục (rút gọn)
```
ProjectForumAPI/
	Articles.csproj
	Program.cs
	Startup.cs
	appsettings.json
	Controllers/
		AccountController.cs
		ArticleController.cs
	Models/
		Data/
			DbContext/DatabaseContext.cs
			AggregateArticles/Article.cs
		DTOs/ ...
	Services/
		ServiceExtensions/ServiceExtension.cs  # cấu hình DI, JWT, Swagger, CORS, Email
		ArticleRepositories/
		UserRepositories/
		ImageRepositories/
		StorageServices/
	Migrations/
	wwwroot/
	Pages/
```

Thiết lập môi trường
Yêu cầu
- .NET SDK 6.0+
- SQL Server (cục bộ hoặc từ xa)

1) Cấu hình `appsettings.json`
- Cập nhật chuỗi kết nối:
```json
"ConnectionStrings": {
	"DatabaseContext": "Server=.;Database=ForumProject;Trusted_Connection=True;"
}
```
- Cấu hình JWT và Mail (đổi giá trị thực tế, không commit bí mật):
```json
"JWT": {
	"Issuer": "Article",
	"lifetime": 360,
	"Key": "<your-long-random-secret>"
},
"MailSettings": {
	"Mail": "<no-reply@example.com>",
	"DisplayName": "AdminPage",
	"Password": "<app-password>",
	"Host": "smtp.gmail.com",
	"Port": 587
},
"AppUrl": "https://localhost:5002"
```

2) Khởi tạo database
- Tạo/migrate schema (nếu dùng EF migrations sẵn):
```powershell
dotnet tool install --global dotnet-ef
dotnet ef database update
```

3) Chạy ứng dụng
```powershell
dotnet restore
dotnet build
dotnet run
```
- Swagger: mở `https://localhost:<port>/swagger` để thử API.

Các endpoint chính
Xác thực & Người dùng (`AccountController` – một số route là tuyệt đối, không theo tiền tố `forum/Account`):
- POST `/register`: đăng ký tài khoản mới (body: `UserDTO`).
- POST `/login`: đăng nhập, trả về JWT token.
- GET `/logout`: đăng xuất.
- GET `/confirmemail?userId=&token=`: xác nhận email, redirect về `AppUrl/confirmemail.html`.
- POST `/ForgetPassword`: yêu cầu quên mật khẩu (input: email).
- POST `/ResetPassword`: đặt lại mật khẩu (form `ResetPasswordDTO`).
- GET `/UserInfo` (Bearer): trả về thông tin người dùng hiện tại.
- POST `/UpdateUser` (Bearer, form): cập nhật tên; (gợi ý) có thể mở rộng cập nhật avatar.
- POST `/UpdateAvatarUser` (Bearer, form): cập nhật avatar bằng upload file.

Bài viết (`ArticleController`, route gốc `forum/Article`):
- GET `forum/Article` (anonymous): danh sách bài viết đã duyệt (`IsActive=true`).
- GET `forum/Article/getArticlesOfMe` (User/Admin): bài viết của người dùng hiện tại.
- GET `forum/Article/getArticlesByAdmin` (Admin): danh sách bài viết chờ duyệt (`IsActive=false`).
- GET `forum/Article/{id}` (anonymous): chi tiết bài viết, tự tăng `ViewCount`.
- GET `forum/Article/category/{categoryId}` (anonymous): lọc theo danh mục.
- GET `forum/Article/filterByKey?key=...` (anonymous): tìm theo tiêu đề/nội dung (có bỏ dấu tiếng Việt).
- POST `forum/Article` (User/Admin, form): tạo bài viết. Trường cần thiết: `Title`, `Content`, `Category` (1..5), `Thumbnails` (tùy chọn). Tự gán `UserId`, `AuthorName`, `CreatedDate`.
- PUT `forum/Article/admin/{id}` (Admin, form): duyệt bài – cập nhật `IsActive` theo `ArticleUpdateByAdminRequest`.
- PUT `forum/Article/{id}` (User/Admin, form): cập nhật tiêu đề/nội dung/ảnh.
- DELETE `forum/Article/{id}` (User/Admin): xóa bài.

Bảo mật & CORS
- JWT Bearer trong header `Authorization: Bearer <token>`.
- Roles: `User`, `Admin` (một số API yêu cầu `Admin`).
- CORS policy `AllowAll` bật sẵn cho phát triển; khuyến nghị thắt chặt trong production.

Swagger
- Bật sẵn tại `/swagger` với security scheme Bearer. Nhấn “Authorize” để nhập token JWT trước khi gọi API bảo vệ.

Gửi email
- `MailSettings` dùng cho các luồng xác nhận email/quên mật khẩu. Yêu cầu SMTP hợp lệ (ví dụ Gmail App Password).

Lưu trữ ảnh
- Upload ảnh qua các endpoint dạng form (`IFormFile Thumbnails`). Ảnh được lưu qua `IStorageService` và đường dẫn ảnh nằm ở trường `ImagePath`.

Phát triển & Gỡ lỗi
- Logging: Serilog + console.
- Sửa lỗi nhanh: kiểm tra response body (API trả `Response{ message, data }`), xem Exception Handler, theo dõi console logs.

Ghi chú
- `Category` của bài viết hiện được kiểm tra cứng 1..5 trong `CreateArticle` – cân nhắc chuyển sang bảng danh mục.
- Một số endpoint sử dụng `[FromForm]` cho cả text và file; khi gọi từ Swagger/clients cần gửi `multipart/form-data`.
- Không commit khóa JWT/SMTP thật vào VCS. Dùng Secret Manager/User Secrets trong môi trường dev.

