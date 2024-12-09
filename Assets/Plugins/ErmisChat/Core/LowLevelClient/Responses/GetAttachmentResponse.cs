using System.Net.Mail;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;
using System;
namespace Ermis.Core.LowLevelClient.Responses
{
    public  class GetAttachmentResponse : ResponseObjectBase, ILoadableFrom<GetAttachmentResponseInternalDTO, GetAttachmentResponse>
    {
        public AttachmentDataResponse Attachments {  get; set; }
        public string Duration { get; set; }

        GetAttachmentResponse ILoadableFrom<GetAttachmentResponseInternalDTO, GetAttachmentResponse>.LoadFromDto(GetAttachmentResponseInternalDTO dto)
        {
            Duration = dto.Duration;
            Attachments = Attachments.TryLoadFromDto(dto.Attachments);
            return this;
        }
    }

    public class AttachmentDataResponse: ResponseObjectBase, ILoadableFrom<AttachmentDataResponseInternalDTO, AttachmentDataResponse>
    {
        public string Id{  get; set; }
        public string UserId{  get; set; }
        public string Cid{  get; set; }
        public string Url{  get; set; }
        public string ThumbUrl{  get; set; }
        public string FileName{  get; set; }
        public string ContentType{  get; set; }
        public long ContentLength{  get; set; }
        public string ContentDisposition{  get; set; }
        public string MessageId{  get; set; }
        public DateTimeOffset CreatedAt {  get; set; }
        public DateTimeOffset UpdatedAt {  get; set; }

        AttachmentDataResponse ILoadableFrom<AttachmentDataResponseInternalDTO, AttachmentDataResponse>.LoadFromDto(AttachmentDataResponseInternalDTO dto)
        {
            Id = dto.Id;
            UserId = dto.UserId;
            Cid = dto.Cid;
            Url = dto.Url;
            ThumbUrl = dto.ThumbUrl;
            FileName = dto.FileName;
            ContentType = dto.ContentType;
            ContentLength = dto.ContentLength;
            ContentDisposition = dto.ContentDisposition;
            MessageId = dto.MessageId;
            CreatedAt = dto.CreatedAt;
            UpdatedAt = dto.UpdatedAt;
            return this;
        }
    }
}