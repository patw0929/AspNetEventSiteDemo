using System;
using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System.IO;
using System.Drawing;

namespace tw.patw
{
    public partial class PatwCommon
    {
        public class Convert_type
        {
            public static Image Base64ToImage(string base64String)
            {
                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
		
	}
}
