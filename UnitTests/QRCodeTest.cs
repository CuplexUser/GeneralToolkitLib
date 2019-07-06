using System;
using System.Drawing;
using GeneralToolkitLib.Barcode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class QRCodeTest
    {
        [TestMethod]
        public void GenerateTestQRCode()
        {
            const string textToQR = "This is a simple test code";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(textToQR, QRCodeGenerator.ECCLevel.M);
            Bitmap qrCodeImage = qrCode.GetGraphic(40);
            qrCodeImage.Save("testQRCode.bmp");
        }
    }
}
