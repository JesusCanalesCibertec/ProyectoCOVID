using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace net.royal.spring.framework.web
{

    public static class Captcha
    {
        const string Letters = "23456789ABCDEFGHJKMNPQRSTUVWXYZ";

        public static string GenerateCaptchaCode()
        {
            Random rand = new Random();
            int maxRand = Letters.Length - 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                int index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }

        public static CaptchaResult GenerateCaptchaImage(int width, int height, string captchaCode)
        {
           
            using (System.Drawing.Bitmap baseMap = new System.Drawing.Bitmap(width, height))
            using (System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(baseMap))
            {
                Random rand = new Random();

                graph.Clear(Color.FromArgb(255, 123, 0)/*GetRandomLightColor()*/);

                DrawCaptchaCode();

                //DrawDisorderLine();

                AdjustRippleEffect();

                MemoryStream ms = new MemoryStream();

                baseMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return new CaptchaResult { CaptchaCode = captchaCode, CaptchaByteData = ms.ToArray(), Timestamp = DateTime.Now };

                int GetFontSize(int imageWidth, int captchCodeCount)
                {
                    var averageSize = imageWidth / captchCodeCount;

                    return Convert.ToInt32(averageSize);
                }

                Color GetRandomDeepColor()
                {
                    int redlow = 160, greenLow = 100, blueLow = 160;

                    return Color.FromArgb(rand.Next(redlow), rand.Next(greenLow), rand.Next(blueLow));
                }

                Color GetRandomLightColor()
                {
                    int low = 180, high = 255;

                    int nRend = rand.Next(high) % (high - low) + low;
                    int nGreen = rand.Next(high) % (high - low) + low;
                    int nBlue = rand.Next(high) % (high - low) + low;

                    return Color.FromArgb(nRend, nGreen, nBlue);
                }

                void DrawCaptchaCode()
                {
                    System.Drawing.SolidBrush fontBrush = new System.Drawing.SolidBrush(Color.Black);
                    int fontSize = GetFontSize(width, captchaCode.Length);
                    System.Drawing.Font font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
                    for (int i = 0; i < captchaCode.Length; i++)
                    {
                        fontBrush.Color = Color.White;//GetRandomDeepColor();

                        int shiftPx = fontSize / 6;

                        float x = i * fontSize + rand.Next(-shiftPx, shiftPx) + rand.Next(-shiftPx, shiftPx);
                        int maxY = height - fontSize;
                        if (maxY < 0) maxY = 0;
                        float y = rand.Next(0, maxY);

                        graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
                    }
                }

                void DrawDisorderLine()
                {
                    System.Drawing.Pen linePen = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 3);
                    for (int i = 0; i < rand.Next(3, 5); i++)
                    {
                        linePen.Color = Color.Black;

                        System.Drawing.Point startPoint = new System.Drawing.Point(rand.Next(0, width), rand.Next(0, height));
                        System.Drawing.Point endPoint = new System.Drawing.Point(rand.Next(0, width), rand.Next(0, height));
                        graph.DrawLine(linePen, startPoint, endPoint);

                        System.Drawing.Point bezierPoint1 = new System.Drawing.Point(rand.Next(0, width), rand.Next(0, height));
                        System.Drawing.Point bezierPoint2 = new System.Drawing.Point(rand.Next(0, width), rand.Next(0, height));

                        graph.DrawBezier(linePen, startPoint, bezierPoint1, bezierPoint2, endPoint);
                    }
                }

                void AdjustRippleEffect()
                {
                    short nWave = 6;
                    int nWidth = baseMap.Width;
                    int nHeight = baseMap.Height;

                    System.Drawing.Point[,] pt = new System.Drawing.Point[nWidth, nHeight];

                    double newX, newY;
                    double xo, yo;

                    for (int x = 0; x < nWidth; ++x)
                    {
                        for (int y = 0; y < nHeight; ++y)
                        {
                            xo = ((double)nWave * Math.Sin(2.0 * 3.1415 * (float)y / 128.0));
                            yo = ((double)nWave * Math.Cos(2.0 * 3.1415 * (float)x / 128.0));

                            newX = (x + xo);
                            newY = (y + yo);

                            if (newX > 0 && newX < nWidth)
                            {
                                pt[x, y].X = (int)newX;
                            }
                            else
                            {
                                pt[x, y].X = 0;
                            }


                            if (newY > 0 && newY < nHeight)
                            {
                                pt[x, y].Y = (int)newY;
                            }
                            else
                            {
                                pt[x, y].Y = 0;
                            }
                        }
                    }

                    System.Drawing.Bitmap bSrc = (System.Drawing.Bitmap)baseMap.Clone();

                    System.Drawing.Imaging.BitmapData bitmapData = baseMap.LockBits(new System.Drawing.Rectangle(0, 0, baseMap.Width, baseMap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    System.Drawing.Imaging.BitmapData bmSrc = bSrc.LockBits(new System.Drawing.Rectangle(0, 0, bSrc.Width, bSrc.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                    int scanline = bitmapData.Stride;

                    IntPtr Scan0 = bitmapData.Scan0;
                    IntPtr SrcScan0 = bmSrc.Scan0;

                    unsafe
                    {
                        byte* p = (byte*)(void*)Scan0;
                        byte* pSrc = (byte*)(void*)SrcScan0;

                        int nOffset = bitmapData.Stride - baseMap.Width * 3;

                        int xOffset, yOffset;

                        for (int y = 0; y < nHeight; ++y)
                        {
                            for (int x = 0; x < nWidth; ++x)
                            {
                                xOffset = pt[x, y].X;
                                yOffset = pt[x, y].Y;

                                if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                                {
                                    p[0] = pSrc[(yOffset * scanline) + (xOffset * 3)];
                                    p[1] = pSrc[(yOffset * scanline) + (xOffset * 3) + 1];
                                    p[2] = pSrc[(yOffset * scanline) + (xOffset * 3) + 2];
                                }

                                p += 3;
                            }
                            p += nOffset;
                        }
                    }

                    baseMap.UnlockBits(bitmapData);
                    bSrc.UnlockBits(bmSrc);
                    bSrc.Dispose();
                }
            }
        }
    }

    public class CaptchaResult
    {
        public string CaptchaCode { get; set; }

        public byte[] CaptchaByteData { get; set; }

        public string CaptchBase64Data
        {
            get
            {
                return Convert.ToBase64String(CaptchaByteData);
            }
        }

        public DateTime Timestamp { get; set; }
    }
}