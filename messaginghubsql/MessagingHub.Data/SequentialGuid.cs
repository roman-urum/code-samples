using System;

namespace MessagingHub.Data
{
    public class SequentialGuid
    {
        public static Guid GenerateComb()
        {
            DateTime time = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan span = new TimeSpan(now.Ticks - time.Ticks);
            TimeSpan timeOfDay = now.TimeOfDay;

            byte[] destinationArray = Guid.NewGuid().ToByteArray();
            byte[] bytes = BitConverter.GetBytes(span.Days);
            byte[] array = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));

            Array.Reverse(bytes);
            Array.Reverse(array);

            Array.Copy(bytes, bytes.Length - 2, destinationArray, destinationArray.Length - 6, 2);
            Array.Copy(array, array.Length - 4, destinationArray, destinationArray.Length - 4, 4);

            return new Guid(destinationArray);
        }

        Guid _CurrentGuid;
        public Guid CurrentGuid
        {
            get { return _CurrentGuid; }
        }

        public SequentialGuid()
        {
            _CurrentGuid = GenerateComb();
        }

        public SequentialGuid(Guid previousGuid)
        {
            _CurrentGuid = previousGuid;
        }

        public static SequentialGuid operator ++(SequentialGuid sequentialGuid)
        {
            byte[] bytes = sequentialGuid._CurrentGuid.ToByteArray();
            for (int mapIndex = 0; mapIndex < 16; mapIndex++)
            {
                int bytesIndex = SqlOrderMap[mapIndex];
                bytes[bytesIndex]++;
                if (bytes[bytesIndex] != 0)
                {
                    break; // No need to increment more significant bytes
                }
            }
            sequentialGuid._CurrentGuid = new Guid(bytes);
            return sequentialGuid;
        }

        private static int[] _SqlOrderMap = null;
        private static int[] SqlOrderMap
        {
            get
            {
                if (_SqlOrderMap == null)
                {
                    _SqlOrderMap = new int[16] { 3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10 };
                    // 3 - the least significant byte in Guid ByteArray [for SQL Server ORDER BY clause]
                    // 10 - the most significant byte in Guid ByteArray [for SQL Server ORDERY BY clause]
                }
                return _SqlOrderMap;
            }
        }
    }
}