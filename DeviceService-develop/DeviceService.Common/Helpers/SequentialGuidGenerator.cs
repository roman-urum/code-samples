using System;

namespace DeviceService.Common.Helpers
{
    /// <summary>
    /// Sequential guid generator.
    /// </summary>
    public static class SequentialGuidGenerator
    {
        /// <summary>
        /// Generate next sequential guid.
        /// </summary>
        /// <returns> next sequential guid </returns>
        public static Guid Generate()
        {
            var guidArray = Guid.NewGuid().ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            var now = DateTime.Now;

            var daysArray = BitConverter.GetBytes((now - baseDate).Days);
            var msecsArray = BitConverter.GetBytes((long)(now.TimeOfDay.TotalMilliseconds / 3.333333));

            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }
    }
}