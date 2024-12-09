using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class FileUploadResponse : ResponseObjectBase, ILoadableFrom<FileUploadResponseInternalDTO, FileUploadResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        public string File { get; set; }

        FileUploadResponse ILoadableFrom<FileUploadResponseInternalDTO, FileUploadResponse>.LoadFromDto(FileUploadResponseInternalDTO dto)
        {
            Duration = dto.Duration;
            File = dto.File;
            return this;
        }
    }
}