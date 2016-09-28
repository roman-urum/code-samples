using System.Threading.Tasks;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements.Medias;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IMediaFileHelper.
    /// </summary>
    public interface IMediaFileHelper
    {
        /// <summary>
        /// Creates the media file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        Task<Media> CreateMediaFile(BaseMediaRequestDto source);
    }
}