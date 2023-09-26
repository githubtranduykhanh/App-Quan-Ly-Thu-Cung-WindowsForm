
namespace QuanLyTHUCUNG.CuttomControl
{
    partial class ChuongControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChuongControl));
            this.lblLoaiChuong = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblTenChuong = new System.Windows.Forms.Label();
            this.pb_icon = new System.Windows.Forms.PictureBox();
            this.lblTThai = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLoaiChuong
            // 
            this.lblLoaiChuong.AutoSize = true;
            this.lblLoaiChuong.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiChuong.ForeColor = System.Drawing.Color.White;
            this.lblLoaiChuong.Location = new System.Drawing.Point(3, 17);
            this.lblLoaiChuong.Name = "lblLoaiChuong";
            this.lblLoaiChuong.Size = new System.Drawing.Size(88, 19);
            this.lblLoaiChuong.TabIndex = 100;
            this.lblLoaiChuong.Text = "Chuồng 1";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            this.guna2Elipse1.TargetControl = this;
            // 
            // lblTenChuong
            // 
            this.lblTenChuong.AutoSize = true;
            this.lblTenChuong.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenChuong.ForeColor = System.Drawing.Color.White;
            this.lblTenChuong.Location = new System.Drawing.Point(3, 47);
            this.lblTenChuong.Name = "lblTenChuong";
            this.lblTenChuong.Size = new System.Drawing.Size(114, 20);
            this.lblTenChuong.TabIndex = 101;
            this.lblTenChuong.Text = "Chuồng Chó 1";
            // 
            // pb_icon
            // 
            this.pb_icon.Image = ((System.Drawing.Image)(resources.GetObject("pb_icon.Image")));
            this.pb_icon.Location = new System.Drawing.Point(97, 3);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(34, 33);
            this.pb_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_icon.TabIndex = 102;
            this.pb_icon.TabStop = false;
            // 
            // lblTThai
            // 
            this.lblTThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTThai.AutoSize = true;
            this.lblTThai.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTThai.ForeColor = System.Drawing.Color.White;
            this.lblTThai.Location = new System.Drawing.Point(83, 76);
            this.lblTThai.Name = "lblTThai";
            this.lblTThai.Size = new System.Drawing.Size(60, 20);
            this.lblTThai.TabIndex = 103;
            this.lblTThai.Text = "Đã Đặt";
            this.lblTThai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChuongControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(212)))));
            this.Controls.Add(this.lblTThai);
            this.Controls.Add(this.pb_icon);
            this.Controls.Add(this.lblTenChuong);
            this.Controls.Add(this.lblLoaiChuong);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ChuongControl";
            this.Size = new System.Drawing.Size(136, 100);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoaiChuong;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label lblTenChuong;
        private System.Windows.Forms.PictureBox pb_icon;
        private System.Windows.Forms.Label lblTThai;
    }
}
