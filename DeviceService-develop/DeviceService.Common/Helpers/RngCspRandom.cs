using System;
using System.Security.Cryptography;

namespace DeviceService.Common.Helpers
{
    /// <summary>
    /// RngCspRandom.
    /// </summary>
    public class RngCspRandom
    {
        private const uint BufferSize = 1024;  // must be a multiple of 4
        private byte[] randomBuffer;
        private int bufferOffset;
        private readonly RNGCryptoServiceProvider rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="RngCspRandom"/> class.
        /// </summary>
        public RngCspRandom()
        {
            randomBuffer = new byte[BufferSize];
            rng = new RNGCryptoServiceProvider();
            bufferOffset = randomBuffer.Length;
        }

        public uint Next()
        {
            if (bufferOffset >= randomBuffer.Length)
            {
                FillBuffer();
            }

            uint val = BitConverter.ToUInt32(randomBuffer, bufferOffset) & 0x7fffffff;
            bufferOffset += sizeof(ulong);

            return val;
        }

        public uint Next(uint maxValue)
        {
            return Next() % maxValue;
        }

        public uint Next(uint minValue, uint maxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentException("maxValue must be greater than or equal to minValue");
            }

            uint range = maxValue - minValue;

            return minValue + Next(range);
        }

        public double NextDouble()
        {
            ulong val = Next();

            return (double)val / ulong.MaxValue;
        }

        public void GetBytes(byte[] buff)
        {
            rng.GetBytes(buff);
        }

        private void FillBuffer()
        {
            rng.GetBytes(randomBuffer);
            bufferOffset = 0;
        }
    }
}