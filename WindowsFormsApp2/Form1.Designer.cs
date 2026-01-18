namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbx_wight = new System.Windows.Forms.TextBox();
            this.tbx_height = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_work = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "选择文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_ImgImport_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(221, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 257);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tbx_wight
            // 
            this.tbx_wight.Location = new System.Drawing.Point(31, 213);
            this.tbx_wight.Multiline = true;
            this.tbx_wight.Name = "tbx_wight";
            this.tbx_wight.Size = new System.Drawing.Size(121, 41);
            this.tbx_wight.TabIndex = 2;
            this.tbx_wight.Text = "1234";
            // 
            // tbx_height
            // 
            this.tbx_height.Location = new System.Drawing.Point(31, 295);
            this.tbx_height.Multiline = true;
            this.tbx_height.Name = "tbx_height";
            this.tbx_height.Size = new System.Drawing.Size(121, 41);
            this.tbx_height.TabIndex = 2;
            this.tbx_height.Text = "1234";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(10, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "W";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(10, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "H";
            // 
            // tbx_name
            // 
            this.tbx_name.Location = new System.Drawing.Point(31, 118);
            this.tbx_name.Multiline = true;
            this.tbx_name.Name = "tbx_name";
            this.tbx_name.Size = new System.Drawing.Size(121, 28);
            this.tbx_name.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(28, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "保存为文件名";
            // 
            // btn_work
            // 
            this.btn_work.Location = new System.Drawing.Point(231, 294);
            this.btn_work.Margin = new System.Windows.Forms.Padding(2);
            this.btn_work.Name = "btn_work";
            this.btn_work.Size = new System.Drawing.Size(128, 55);
            this.btn_work.TabIndex = 0;
            this.btn_work.Text = "转换";
            this.btn_work.UseVisualStyleBackColor = true;
            this.btn_work.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_height);
            this.Controls.Add(this.tbx_name);
            this.Controls.Add(this.tbx_wight);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_work);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbx_wight;
        private System.Windows.Forms.TextBox tbx_height;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_work;
    }
}

