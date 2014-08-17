using System;
using System.Web;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.SessionState;

namespace ReCaptchaDemo
{
    /// <summary>
    /// Summary description for GenerateRecaptcha
    /// </summary>
    public class GenerateRecaptcha : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int captchaStr = new Random().Next(6, 12);
            context.Session["captcha"] = GenerateRandomString(captchaStr);
            const int iHeight = 70;
            const int iWidth = 250;
            var oRandom = new Random();
            int[] aFontEmSizes = { 15, 20, 25, 30 };
            string[] aFontNames = { "Comic Sans MS", "Arial", "Times New Roman", "Georgia", "Verdana", "Geneva" };

            FontStyle[] aFontStyles =
            {  
                FontStyle.Bold,
                FontStyle.Italic,
                FontStyle.Regular,
                FontStyle.Strikeout,
                FontStyle.Underline
            };

            HatchStyle[] aHatchStyles =
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
                HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical, HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
                HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
                HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal, HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
                HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal, HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
                HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard, HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
                HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis, HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
                HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
            };

            //Get Captcha in Session
            string sCaptchaText = context.Session["captcha"].ToString();

            //Creates an output Bitmap
            var oOutputBitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
            var oGraphics = Graphics.FromImage(oOutputBitmap);
            oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //Create a Drawing area
            var oRectangleF = new RectangleF(0, 0, iWidth, iHeight);

            //Draw background (Lighter colors RGB 100 to 255)
            Brush oBrush = new HatchBrush(aHatchStyles[oRandom.Next(aHatchStyles.Length - 1)], Color.Gainsboro, Color.White);//HatchBrush(aHatchStyles[oRandom.Next(aHatchStyles.Length - 1)], Color.FromArgb((oRandom.Next(100, 255)), (oRandom.Next(100, 255)), (oRandom.Next(100, 255))), Color.White);
            oGraphics.FillRectangle(oBrush, oRectangleF);

            var oMatrix = new Matrix();
            int i;
            for (i = 0; i <= sCaptchaText.Length - 1; i++)
            {
                oMatrix.Reset();
                int iChars = sCaptchaText.Length;
                int x = iWidth / (iChars + 1) * i;
                const int y = iHeight / 2;

                //Rotate text Random
                oMatrix.RotateAt(oRandom.Next(-40, 40), new PointF(x, y));
                oGraphics.Transform = oMatrix;

                //Draw the letters with Randon Font Type, Size and Color
                oGraphics.DrawString
                (
                    //Text
                sCaptchaText.Substring(i, 1),
                    //Random Font Name and Style
                new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)], aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)], aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
                    //Random Color (Darker colors RGB 0 to 100)
                new SolidBrush(Color.FromArgb(oRandom.Next(0, 100), oRandom.Next(0, 100), oRandom.Next(0, 100))),
                x,
                oRandom.Next(10, 40)
                );
                oGraphics.ResetTransform();
            }

            context.Response.ContentType = "image/JPEG";
            //render image 
            oOutputBitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            //dispose everything, we do not need them any more.
            oOutputBitmap.Dispose();
            oGraphics.Dispose();
            Console.WriteLine();
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
            string result = "";
            var rand = new Random();
            for (int i = 0; i < length; i++)
            {
                result += chars[rand.Next(chars.Length)];
            }
            return result;
        }

    }
}