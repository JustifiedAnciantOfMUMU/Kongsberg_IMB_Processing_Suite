using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace IMB_Data_Processing
{
    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                int PixelCount = Width * Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat);

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                System.Runtime.InteropServices.Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                System.Runtime.InteropServices.Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B; //Convert.ToByte(255 - color.B)
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;

                //System.Diagnostics.Debug.WriteLine("Pixel {0} = {1},{2},{3},{4}", i, Pixels[i], Pixels[i+1], Pixels[i+2], color.A);

            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }
    }

    public class IMBtoBitmap
    {
        //3 variables used to check whether the beamlist or ranges have changed in the 
        private static float[] beamlistsaved = null;
        private static int LowRsaved = 0;
        private static int HighRsaved = 0;
        //An array to hold the sample and angle index of each pixel in the bitmap.
        private static int[,,] RangeAngleBitmap = null;

        private static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }

        // A binary search that returns the closest possible index out of an array of floats from a target
        private static int BinarySearch(float[] inputArr, float Target, int start, int end)
        {
            int mid = (start + end) / 2;
            //if (inputArr[start] <= Target && Target <= inputArr[end])
            //{
            if (start == mid)
            {
                float difference = Math.Abs(inputArr[start] - Target);
                if (difference < Math.Abs(inputArr[end] - Target))
                {
                    return start;
                }
                else
                    return end;
            }
            else if (Target < inputArr[mid])
                return BinarySearch(inputArr, Target, start, mid);
            else if (Target > inputArr[mid])
                return BinarySearch(inputArr, Target, mid, end);
            else if (inputArr[mid] == Target)
                return mid;
            else
                return -1;
        }

        public static Bitmap IMBpacketToBitmap(IMBPacket packet, int width, int height, int gain)
        {
            if (packet == null)
            {
                return null;
            }
            int LowRange = (int)((packet.fNearRange * 2.0 / packet.fVelocitySound + packet.fTXWST - packet.fSWST) / packet.fImageSampleInterval);
            int HighRange = (int)((packet.fFarRange * 2.0 / packet.fVelocitySound + packet.fTXWST - packet.fSWST) / packet.fImageSampleInterval);
            int nSamples = packet.fPacketBody.GetLength(1);
            int nBeams = packet.fPacketBody.GetLength(0);
            int[,] intensityPixels = new int[nSamples / 2, nBeams];
            double dMax_mag = 0; //maximum magnitude of the raw data , it is used for normalization
            double dMin_mag = 10000000000;
            int intensity;
            for (int y = 0; y < nBeams; y++)
            {
                for (int x = 0; x < nSamples - 1; x += 2)
                {
                    float firstop = packet.fPacketBody[y, x];
                    float secondop = packet.fPacketBody[y, x + 1];
                    float sqrt = (float)Math.Sqrt((double)(firstop * firstop + secondop * secondop));
                    if (sqrt > 100)
                    {
                        continue;
                    }
                    double Normalization = Math.Log10(1.0 + (double)sqrt);
                    if (Normalization > dMax_mag)
                    {
                        dMax_mag = Normalization;
                    }
                    if (Normalization < dMin_mag)
                    {
                        dMin_mag = Normalization;
                    }
                    intensity = (int)((Normalization - dMin_mag) * 255 / (dMax_mag - dMin_mag) / 10.0 * gain);
                    if (intensity < 0 || intensity > 255)
                        intensityPixels[x / 2, y] = 255;
                    else
                        intensityPixels[x / 2, y] = intensity;
                }
            }
            Bitmap completedmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            LockBitmap lockbitmap = new LockBitmap(completedmap);
            lockbitmap.LockBits();
            if (ArraysEqual(packet.BeamList, beamlistsaved) && LowRsaved == LowRange && HighRsaved == HighRange)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (RangeAngleBitmap[j, i, 0] == -1)
                            continue;
                        else
                            lockbitmap.SetPixel(j, i, Color.FromArgb(255, intensityPixels[RangeAngleBitmap[j, i, 0], RangeAngleBitmap[j, i, 1]], intensityPixels[RangeAngleBitmap[j, i, 0], RangeAngleBitmap[j, i, 1]] / 2, 0));
                    }
                }
            }
            else
            {
                LowRsaved = LowRange;
                HighRsaved = HighRange;
                beamlistsaved = new float[packet.BeamList.Length];
                Array.Copy(packet.BeamList, beamlistsaved, packet.BeamList.Length);
                RangeAngleBitmap = new int[width, height, 2];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int y = height - 1 - i;
                        int x = j - width / 2;
                        float rawAngle = -1 * (float)(Math.Atan((double)x / (double)y) * 180 / Math.PI);
                        if (rawAngle < packet.BeamList[0] || rawAngle > packet.BeamList[nBeams - 1] || float.IsNaN(rawAngle))
                        {
                            RangeAngleBitmap[j, i, 0] = -1;
                            continue;
                        }
                        double rawDistance = Math.Sqrt((double)(x * x) + (double)(y * y));
                        double range = rawDistance / (height - 1.0) * packet.fFarRange;
                        if (range < packet.fNearRange || range > packet.fFarRange)
                        {
                            RangeAngleBitmap[j, i, 0] = -1;
                            continue;
                        }
                        int SampleInd = (int)Math.Round((range - packet.fNearRange) * (HighRange - LowRange - 1) / (packet.fFarRange - packet.fNearRange)) + LowRange;
                        if (SampleInd > (HighRange - LowRange - 1) || SampleInd >= nSamples / 2)
                        {
                            RangeAngleBitmap[j, i, 0] = -1;
                            continue;
                        }
                        int beamInd = BinarySearch(packet.BeamList, rawAngle, 0, nBeams - 1);
                        lockbitmap.SetPixel(j, i, Color.FromArgb(255, intensityPixels[SampleInd, beamInd], intensityPixels[SampleInd, beamInd] / 2, 0));
                        RangeAngleBitmap[j, i, 0] = SampleInd;
                        RangeAngleBitmap[j, i, 1] = beamInd;
                    }
                }
            }
            lockbitmap.UnlockBits();
            return completedmap;
        }
    }

}
