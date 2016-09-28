using Maestro.Domain;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IVitalConverter.
    /// </summary>
    public interface IVitalConverter
    {
        /// <summary>
        /// Converts the specified source vitals to the same unit types accordding to the settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="settings">The settings.</param>
        void Convert<T>(T target, VitalSettings settings) where T : IConvertibleVital;
    }
}