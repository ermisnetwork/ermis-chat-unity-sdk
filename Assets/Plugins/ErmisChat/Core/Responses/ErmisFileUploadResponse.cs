namespace Ermis.Core.Responses
{
    public readonly struct ErmisFileUploadResponse
    {
        public string FileUrl { get; }

        internal ErmisFileUploadResponse(string fileUrl)
        {
            FileUrl = fileUrl;
        }
    }
}