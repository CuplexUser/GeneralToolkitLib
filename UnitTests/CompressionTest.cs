using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class CompressionTest
    {
        private readonly RandomNumberGenerator _rndGenerator;
        public CompressionTest()
        {
            _rndGenerator = RandomNumberGenerator.Create();
        }

        [TestMethod]
        public void TestLZMACompress()
        {
            const int DATA_SIZE = 1048576;
            byte[] plaintextBytes = CreateTestdata(DATA_SIZE, 0.01);
            var coder = new GeneralToolkitLib.Compression.SevenZip.Compress.LZMA.Encoder();

            MemoryStream msInput = new MemoryStream(plaintextBytes);
            MemoryStream msOutput = new MemoryStream();

            msInput.Position = 0;

            // Write the encoder properties
            coder.WriteCoderProperties(msOutput);

            // Write the decompressed file size.
            msOutput.Write(BitConverter.GetBytes(msInput.Length), 0, 8);

            //Encode data
            coder.Code(msInput, msOutput, msInput.Length, -1, null);

            msOutput.Flush();

            long compressedSize = msOutput.Length;
            double compressionFactor = (double)compressedSize / DATA_SIZE;


            //Decode
            var decoder = new GeneralToolkitLib.Compression.SevenZip.Compress.LZMA.Decoder();
            msInput = msOutput;
            msOutput = new MemoryStream();
            msInput.Position = 0;

            // Read the decoder properties
            byte[] properties = new byte[5];
            msInput.Read(properties, 0, 5);

            // Read in the decompress file size.
            byte[] fileLengthBytes = new byte[8];
            msInput.Read(fileLengthBytes, 0, 8);
            long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            decoder.SetDecoderProperties(properties);
            decoder.Code(msInput, msOutput, msInput.Length, fileLength, null);

            Assert.IsTrue(CompareByteArrays(plaintextBytes, msOutput.ToArray()), "Decompressed data was not equal to original data!");
        }

        private string GetTestdataString(int length)
        {
            const string testData1 = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in rhoncus eros, at pulvinar lacus. Donec id libero malesuada, congue lacus a, convallis nisi. Duis vitae nulla sed turpis fermentum molestie in ut nisl. Fusce magna erat, faucibus ut neque id, scelerisque ultricies elit. Praesent turpis sapien, dignissim a elit sed, accumsan dictum neque. Suspendisse vitae tincidunt ligula. Cras et fringilla arcu. Vivamus tortor lorem, sodales et nunc nec, imperdiet rhoncus felis. Integer tortor orci, placerat quis nisl id, facilisis eleifend elit. Morbi volutpat odio quis urna tristique, in finibus nulla vulputate. Ut id fermentum urna. Duis aliquam vulputate dui, sit amet rhoncus diam consectetur ut. Morbi ac ex sed dolor convallis blandit at sit amet turpis. Nullam ultrices sed turpis ac ultricies.
Morbi gravida elit eget ipsum dapibus, eu molestie nulla consequat. Suspendisse fringilla luctus egestas. Curabitur vitae nisi lacinia lectus convallis pellentesque auctor non purus. Suspendisse molestie maximus lacus sit amet venenatis. Vestibulum quis porta odio. Mauris dui sapien, molestie sit amet lacus ut, molestie auctor augue. Morbi dictum imperdiet lacus ut imperdiet. Aliquam maximus, nibh porttitor porttitor elementum, nulla est dictum lorem, a tempus ante nunc a felis. Maecenas efficitur mi vitae dui venenatis consectetur. Nunc vitae nunc enim. Sed vitae est eu odio dictum blandit. Cras dignissim tortor in elit aliquam maximus. Suspendisse rutrum erat quis commodo dignissim. Nulla suscipit eros sit amet nunc commodo dignissim. Nulla facilisi. Fusce vestibulum dictum lacus, quis ultricies sapien congue vel.
Sed ligula libero, lobortis suscipit laoreet at, rhoncus non massa. Aenean vel odio interdum, elementum eros accumsan, accumsan neque. Praesent vestibulum tellus a pharetra scelerisque. Aenean at varius ex. Cras maximus vel ante eget mattis. Sed lorem purus, vulputate egestas ligula ac, semper euismod elit. Nunc scelerisque sapien nunc, sed fermentum nibh facilisis in. Donec tincidunt ipsum non justo dictum, nec finibus arcu efficitur. Ut id dignissim ante, non posuere lectus. Donec elementum dui tortor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc consectetur nunc at diam placerat porta. Cras condimentum est orci, sed tincidunt erat lacinia at.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla pulvinar ex ante, ut congue est ullamcorper semper. Morbi eget lorem eleifend, facilisis nisl ut, sodales arcu. Nunc gravida tellus orci, quis fringilla tortor lacinia sit amet. Phasellus nec arcu viverra, posuere neque ut, mollis ex. Mauris et efficitur erat. Sed scelerisque odio libero, eget convallis arcu mollis quis.
Cras id pharetra quam, finibus tempus turpis. Cras malesuada venenatis molestie. Cras porta eros velit, ac blandit quam ornare vel. Duis ac accumsan orci. Donec iaculis eleifend dui at viverra. Fusce condimentum malesuada diam, convallis scelerisque tortor iaculis a. Suspendisse dignissim interdum orci et iaculis.";

            const string testData2 = @"Bacon ipsum dolor amet shoulder ground round meatball fatback. Frankfurter brisket corned beef, picanha alcatra tail meatball swine. Porchetta frankfurter tail salami tenderloin rump ground round venison, bresaola jerky. Tail shoulder frankfurter ground round, sirloin boudin turkey shankle fatback jerky sausage t-bone spare ribs tri-tip. Biltong swine shank prosciutto bacon chicken.
Pig pancetta strip steak sirloin beef ribs capicola leberkas venison tail pork loin. Filet mignon jerky bacon cow shank doner. Tail landjaeger sausage hamburger prosciutto. Ground round ham shank corned beef doner porchetta turducken shoulder brisket bresaola beef ribs alcatra ball tip pig. Sausage salami beef t-bone, hamburger landjaeger jowl filet mignon leberkas capicola pig cupim prosciutto ball tip. Pork belly spare ribs porchetta corned beef cupim turkey capicola swine ham hock. Ham hock landjaeger chuck andouille tri-tip pork loin pastrami pork belly kielbasa bresaola salami jerky short ribs drumstick doner.
Cow pastrami strip steak flank, kielbasa swine spare ribs ham hock. Kevin hamburger cupim, corned beef beef ribs kielbasa biltong picanha. Pork loin turkey bresaola kevin turducken. Flank leberkas ribeye shank. Ham tenderloin ribeye kielbasa salami brisket jerky biltong filet mignon pork belly. Prosciutto fatback kielbasa strip steak swine turducken jowl, venison porchetta filet mignon brisket ground round bresaola ham.
Pork chop turkey shankle, porchetta hamburger meatball jerky pork andouille swine filet mignon boudin pork loin leberkas. Leberkas rump brisket pork loin jerky hamburger andouille chicken beef, ham biltong. Drumstick filet mignon ham sausage short ribs, jowl alcatra. Pork belly ham short ribs alcatra shank short loin shoulder tail kevin filet mignon.
Drumstick sirloin ball tip salami strip steak jerky shank. Beef ribs tri-tip ball tip, jowl biltong corned beef prosciutto spare ribs hamburger flank shankle shank salami. Drumstick hamburger meatball shank pork turducken t-bone salami. Chuck chicken flank, shankle meatloaf turkey landjaeger jerky prosciutto beef ribs.";

            StringBuilder sb = new StringBuilder();

            while (sb.Length < length)
            {
                string testData;
                if (this.GetRandomInt(0, 9) < 5)
                    testData = testData1;
                else
                    testData = testData2;

                int startIndex = this.GetRandomInt(0, testData.Length - 25);
                int subStrlength = testData.Length - startIndex;

                if (subStrlength > length - sb.Length)
                    subStrlength = length - sb.Length;

                if (subStrlength > 0)
                    sb.Append(testData.Substring(startIndex, subStrlength));
            }

            return sb.ToString();
        }

        private byte[] CreateTestdata(int size, double randomDataAmount)
        {
            if (randomDataAmount > 1)
                randomDataAmount = 1;

            if (randomDataAmount < 0)
                randomDataAmount = 0;

            const int MIN_TEXT_LENGT = 10;
            const int MAX_TEXT_LENGT = 10000;

            MemoryStream ms = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(ms);

            int dataWritten = 0;
            int randomDataBytes = (int)(size * randomDataAmount);
            int textData = size - randomDataBytes;

            while (dataWritten < textData)
            {
                int testDataLength = GetRandomInt(MIN_TEXT_LENGT, MAX_TEXT_LENGT);
                string testData = GetTestdataString(testDataLength);

                if (testData.Length > (textData - dataWritten))
                {
                    int subStrLength = textData - dataWritten;
                    if (subStrLength == 0)
                        break;

                    streamWriter.Write(testData.Substring(0, subStrLength));
                    dataWritten += subStrLength;
                }
                else
                {
                    streamWriter.Write(testData);
                    dataWritten += testData.Length;
                }
            }
            streamWriter.Flush();
            if (randomDataBytes > 0)
            {
                var rndGen = RandomNumberGenerator.Create();
                byte[] buffer = new byte[randomDataBytes];
                rndGen.GetBytes(buffer);
                ms.Write(buffer, 0, buffer.Length);
            }


            return ms.ToArray();
        }

        private int GetRandomInt(int min, int max)
        {
            byte[] buffer = new byte[4];
            _rndGenerator.GetBytes(buffer);

            int randomInt = min + (BitConverter.ToInt32(buffer, 0) % max);

            if (randomInt < min)
                randomInt = Math.Abs(randomInt);

            return randomInt;
        }

        private bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            return !array1.Where((t, i) => t != array2[i]).Any();
        }
    }
}
