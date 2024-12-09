﻿using System.Net.Http;

namespace Ermis.Libs.Http
{
    /// <summary>
    /// Simple structure to wrap file name and byte content
    /// </summary>
    public readonly struct FileWrapper
    {
        public readonly string FileName;
        public readonly byte[] FileContent;

        public FileWrapper(string fileName, byte[] fileContent)
        {
            FileName = fileName;
            FileContent = fileContent;
        }
    }
}