using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PresentationSnapshot
{
    public partial class SnapshotOverlay : Form
    {
        // Win32 API Declarations
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int GWL_EXSTYLE = -20;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private Bitmap snapshotImage;
        private Timer keepOnTopTimer;

        public SnapshotOverlay(Bitmap snapshot)
        {
            snapshotImage = snapshot;
            InitializeComponent();
            ConfigureOverlay();
            InitializeKeepOnTopTimer();
        }

        private void ConfigureOverlay()
        {
            // Cấu hình cửa sổ overlay
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true; // Luôn nằm trên cùng
            this.ShowInTaskbar = false;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;
            this.Opacity = 1.0; // Fully opaque

            // Thiết lập WS_EX_TRANSPARENT - cho phép click-through
            int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle | WS_EX_TRANSPARENT);
        }

        private void InitializeKeepOnTopTimer()
        {
            keepOnTopTimer = new Timer();
            keepOnTopTimer.Interval = 50; // Chạy mỗi 50ms
            keepOnTopTimer.Tick += (sender, e) =>
            {
                // Đảm bảo cửa sổ luôn ở trên cùng
                if (!this.IsDisposed)
                {
                    this.BringToFront();
                    this.TopMost = true;
                }
            };
            keepOnTopTimer.Start();
        }

        public void UpdateSnapshot(Bitmap newSnapshot)
        {
            if (snapshotImage != null)
            {
                snapshotImage.Dispose();
            }
            snapshotImage = newSnapshot;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            if (snapshotImage != null)
            {
                // Vẽ ảnh chụp vào toàn bộ form - không có hướng dẫn
                e.Graphics.DrawImage(snapshotImage, 0, 0, this.Width, this.Height);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dừng timer
            if (keepOnTopTimer != null)
            {
                keepOnTopTimer.Stop();
                keepOnTopTimer.Dispose();
            }

            // Giải phóng ảnh
            if (snapshotImage != null)
            {
                snapshotImage.Dispose();
            }
            
            base.OnFormClosing(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Không vẽ background mặc định để tránh flicker
        }
    }
}
