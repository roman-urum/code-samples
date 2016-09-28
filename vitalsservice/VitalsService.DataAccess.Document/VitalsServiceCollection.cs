using System;
using System.Collections.Generic;

namespace VitalsService.DataAccess.Document
{
    public enum VitalsServiceCollection
    {
        RawMeasurment
    }

    public static class VitalsServiceCollectionsExtensions
    {
        private static readonly IDictionary<VitalsServiceCollection, string> CollectionsNamesMap = new Dictionary
            <VitalsServiceCollection, string>
        {
            {VitalsServiceCollection.RawMeasurment, "RawMeasurments"}
        };

        /// <summary>
        /// Returns name of collection in document db.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetCollectionName(this VitalsServiceCollection target)
        {
            string collection;

            if (!CollectionsNamesMap.TryGetValue(target, out collection))
            {
                throw new NotSupportedException("Specified collection not supported");
            }

            return collection;
        }
    }
}
