namespace PresentationSnapshot
{
    partial class SnapshotOverlay
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Name = "SnapshotOverlay";
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SnapshotOverlay_FormClosing);
            
            this.ResumeLayout(false);
        }

        private void SnapshotOverlay_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
        }
    }
}
