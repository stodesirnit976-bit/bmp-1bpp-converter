using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string inputPath;
        string outputPath = @"D:\img";


        public Form1()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }



        private void btn_ImgImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "IMG Files (*.png;*.bmp;*.jpg)|*.png;*.bmp;*.jpg";
                dialog.Title = "选择 需要转换的图片文件";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                dialog.AutoUpgradeEnabled = false; 

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string IMGPath = dialog.FileName;

                    try
                    {
                        inputPath = IMGPath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            int targetWidth = int.Parse(tbx_wight.Text);
            int targetHeight = int.Parse(tbx_height.Text);

            if(pictureBox1.Image != null)
                pictureBox1.Image.Dispose();

            string dir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            Image originalImage = null;

            try
            {
                byte[] imageBytes = File.ReadAllBytes(inputPath);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    originalImage = Image.FromStream(ms);

                    using (Bitmap resizedImage = new Bitmap(targetWidth, targetHeight, PixelFormat.Format24bppRgb))
                    {
                        resizedImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);

                        using (Graphics g = Graphics.FromImage(resizedImage))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;

                            g.DrawImage(originalImage, new Rectangle(0, 0, targetWidth, targetHeight),
                                        new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                        GraphicsUnit.Pixel);
                        }

                        using (MemoryStream outStream = new MemoryStream())
                        {
                            Bitmap b = Convert24bppTo1bpp(resizedImage);
                            
                            string fullPath = Path.Combine(outputPath, tbx_name.Text.Trim() + ".bmp");
                   
                            b.Save(fullPath, ImageFormat.Png);
                           
                            pictureBox1.Image = b;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理失败: {ex.Message}");
                throw;
            }
            
        
        }


        public  Bitmap Convert24bppTo1bpp(Bitmap source)
        {
            int w = source.Width;
            int h = source.Height;

            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format1bppIndexed);
            BitmapData srcData = source.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            int srcStride = srcData.Stride;
            byte[] srcBuffer = new byte[srcStride * h];
            System.Runtime.InteropServices.Marshal.Copy(srcData.Scan0, srcBuffer, 0, srcBuffer.Length);

            int dstStride = dstData.Stride;
            byte[] dstBuffer = new byte[dstStride * h];

            for (int y = 0; y < h; y++)
            {
                int srcRow = y * srcStride;
                int dstRow = y * dstStride;

                for (int x = 0; x < w; x++)
                {
                    int pixelIndex = srcRow + x * 3;
                    byte b = srcBuffer[pixelIndex];
                    byte g = srcBuffer[pixelIndex + 1];
                    byte r = srcBuffer[pixelIndex + 2];

                    if ((r + g + b) / 3 > 128) dstBuffer[dstRow + (x / 8)] |= (byte)(0x80 >> (x % 8));
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(dstBuffer, 0, dstData.Scan0, dstBuffer.Length);
            bmp.UnlockBits(dstData);
            source.UnlockBits(srcData);

            return bmp;
        }



    }
}
