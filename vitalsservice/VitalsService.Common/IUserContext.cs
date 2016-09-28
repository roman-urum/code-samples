using System.Threading.Tasks;

namespace VitalsService
{
    public interface IUserContext
    {
        /// <summary>
        /// Adds required data to context to allow use
        /// authorization checks.
        /// </summary>
        /// <param name="controllerName"></param>
        void Initialize(string controllerName);

        /// <summary>
        /// Verifies if user who send request is care innovations admin.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCIUser();
    }
}
