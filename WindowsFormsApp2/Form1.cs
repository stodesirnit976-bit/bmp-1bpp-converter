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

        string IMGPath;

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
                    IMGPath = dialog.FileName;
                }
            }
        }

        private void btn_transparent_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(IMGPath, FileMode.Open, FileAccess.Read))
            using (Bitmap srcBitmap = new Bitmap(fs))
            {

                var hasAlpha = Image.IsAlphaPixelFormat(srcBitmap.PixelFormat);
                Console.WriteLine(srcBitmap.PixelFormat + " alpha=" + hasAlpha);
                if (hasAlpha == true)
                    MessageBox.Show("图片是透明底色");
            }
            
           
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            inputPath = IMGPath;
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
                   
                            b.Save(fullPath, ImageFormat.Bmp);
                           
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



        public static Bitmap ConvertToTransparent(string filePath)
        {
            // 1. 安全加载图片 (使用 FileStream 防止文件被锁定)
            // 直接把结果覆盖保存回原路径
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (Bitmap srcBitmap = new Bitmap(fs))
            {
                // 2. 创建 32位 ARGB 目标图
                Bitmap destBitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height, PixelFormat.Format32bppArgb);
                destBitmap.SetResolution(srcBitmap.HorizontalResolution, srcBitmap.VerticalResolution);

                // 3. 定义颜色映射表 (ColorMap)
                // 逻辑：
                //   黑字 (0,0,0) -> 白字 (255,255,255)
                //   白底 (255,255,255) -> 透明 (0,0,0,0)
                ColorMap[] colorMap = new ColorMap[2];

                // 映射 1: 黑色 -> 白色
                colorMap[0] = new ColorMap();
                colorMap[0].OldColor = Color.Black;
                colorMap[0].NewColor = Color.White;

                // 映射 2: 白色 -> 透明
                colorMap[1] = new ColorMap();
                colorMap[1].OldColor = Color.White;
                colorMap[1].NewColor = Color.Transparent;

                // 4. 设置 ImageAttributes
                using (ImageAttributes attr = new ImageAttributes())
                {
                    attr.SetRemapTable(colorMap);

                    // 5. 绘制
                    using (Graphics g = Graphics.FromImage(destBitmap))
                    {
                        // 清空画布
                        g.Clear(Color.Transparent);

                        // 以最高质量绘制
                        g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

                        g.DrawImage(
                            srcBitmap,
                            new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height),
                            0, 0, srcBitmap.Width, srcBitmap.Height,
                            GraphicsUnit.Pixel,
                            attr);
                    }
                }

                return destBitmap;
            }
        }

        
    }
}
