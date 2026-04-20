using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PresentationSnapshot
{
    public partial class Form1 : Form
    {
        // Win32 API Declarations
        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_CAPTURE = 1;
        private const int HOTKEY_RELEASE = 2;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private SnapshotOverlay overlayForm;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Ẩn cửa sổ chính - chỉ hoạt động qua phím tắt
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;

            // Đăng ký phím tắt F9 (chụp snapshot)
            RegisterHotKey(this.Handle, HOTKEY_CAPTURE, 0, (uint)Keys.F9);
            
            // Đăng ký phím tắt F10 (tắt snapshot)
            RegisterHotKey(this.Handle, HOTKEY_RELEASE, 0, (uint)Keys.F10);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int hotkeyId = (int)m.WParam;

                if (hotkeyId == HOTKEY_CAPTURE)
                {
                    CaptureSnapshot();
                }
                else if (hotkeyId == HOTKEY_RELEASE)
                {
                    ReleaseSnapshot();
                }
            }

            base.WndProc(ref m);
        }

        private void CaptureSnapshot()
        {
            try
            {
                // Chụp toàn bộ màn hình
                Bitmap screenshot = CaptureScreen();
                
                // Tạo hoặc cập nhật overlay
                if (overlayForm == null || overlayForm.IsDisposed)
                {
                    overlayForm = new SnapshotOverlay(screenshot);
                    overlayForm.Show();
                }
                else
                {
                    overlayForm.UpdateSnapshot(screenshot);
                    overlayForm.BringToFront();
                }
            }
            catch (Exception ex)
            {
                // Im lặng - không hiển thị lỗi
                System.Diagnostics.Debug.WriteLine($"Error capturing snapshot: {ex.Message}");
            }
        }

        private void ReleaseSnapshot()
        {
            if (overlayForm != null && !overlayForm.IsDisposed)
            {
                overlayForm.Close();
                overlayForm.Dispose();
                overlayForm = null;
            }
        }

        private Bitmap CaptureScreen()
        {
            // Lấy kích thước toàn màn hình
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            Bitmap bitmap = new Bitmap(screenBounds.Width, screenBounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size);
            graphics.Dispose();
            return bitmap;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hủy đăng ký phím tắt
            UnregisterHotKey(this.Handle, HOTKEY_CAPTURE);
            UnregisterHotKey(this.Handle, HOTKEY_RELEASE);

            // Đóng overlay nếu đang mở
            if (overlayForm != null && !overlayForm.IsDisposed)
            {
                overlayForm.Close();
            }
        }
    }
}
