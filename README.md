# 🖥️ Presentation Snapshot Tool (PST)

**Presentation Snapshot Tool** là một ứng dụng mã nguồn mở được phát triển bằng **C#** và **.NET Framework**. Ứng dụng này giúp các diễn giả và giáo viên giữ một hình ảnh tĩnh (Snapshot) trên màn hình chính để khán giả theo dõi, trong khi họ có thể chuẩn bị tài liệu hoặc thực hiện các thao tác kỹ thuật ở chế độ nền mà không gây xao nhãng.

## 🚀 Tính năng chính
* **Instant Snapshot:** Chụp ảnh màn hình ngay lập tức chỉ với một phím tắt.
* **Smart Overlay:** Sử dụng công nghệ **Click-through** (Win32 API) cho phép tương tác với các ứng dụng bên dưới lớp ảnh.
* **Top-level Persistence:** Đảm bảo lớp phủ luôn nằm trên cùng để tránh bị gián đoạn bởi thông báo hệ thống.
* **Global Hotkeys:** Điều khiển nhanh chóng bằng phím tắt toàn hệ thống.

## ⌨️ Cách sử dụng
1. Chạy file `PresentationSnapshot.exe`.
2. Mở tài liệu/slide bạn muốn trình chiếu cho khán giả.
3. Nhấn **F9** để kích hoạt chế độ "Đóng băng" màn hình (Snapshot).
4. Nhấn **F10** để quay lại chế độ hiển thị thông thường.

## 🛠️ Công nghệ sử dụng
* Ngôn ngữ: **C#**
* Framework: **.NET Framework 4.8**
* Thư viện hệ thống: `user32.dll` (Win32 API) cho xử lý Hotkeys và Window Styles.

## 📄 Giấy phép
Dự án này được phát hành dưới giấy phép MIT - hoàn toàn miễn phí cho mục đích học tập và nghiên cứu.
