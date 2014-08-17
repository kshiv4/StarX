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
    public class GenerateRecaptcha2 : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            Bitmap objBMP = new Bitmap(200, 60);
            Graphics objGraphics = Graphics.FromImage(objBMP);
            objGraphics.Clear(Color.White);
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.CompositingQuality = CompositingQuality.HighQuality;
            objGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //' Configure font to use for text
            Font objFont = new Font("Georgia", 26, FontStyle.Italic);
            //generate a random string for captcha
            string randomStr = GenerateRandomString(8);
            //This is to add the string to session cookie, to be compared later
            HttpContext.Current.Session.Add("captcha2", randomStr);
            //' Write out the text
            objGraphics.DrawString(randomStr, objFont, Brushes.Black, 3, 3);
            //' Set the content type and return the image
            context.Response.ContentType = "image/JPEG";
            //render image 
            objBMP.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            //dispose everything, we do not need them any more.
            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();
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