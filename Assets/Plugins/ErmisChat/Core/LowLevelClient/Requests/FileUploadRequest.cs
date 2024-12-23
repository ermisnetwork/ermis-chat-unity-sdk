﻿using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class FileUploadRequest : RequestObjectBase, ISavableTo<FileUploadRequestInternalDTO>
    {
        /// <summary>
        /// multipart/form-data file field
        /// </summary>
        public string File { get; set; }

        FileUploadRequestInternalDTO ISavableTo<FileUploadRequestInternalDTO>.SaveToDto() =>
            new FileUploadRequestInternalDTO()
            {
                File = File,
                AdditionalProperties = AdditionalProperties
            };
    }
}