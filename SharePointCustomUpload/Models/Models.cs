using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointCustomUpload.Models
{
    public class Models
    {
        public class FormDigestInfo
        {
            public class Rootobject
            {
                public int FormDigestTimeoutSeconds { get; set; }
                public string FormDigestValue { get; set; }
                public string LibraryVersion { get; set; }
                public string SiteFullUrl { get; set; }
                public string[] SupportedSchemaVersions { get; set; }
                public string WebFullUrl { get; set; }
            }
        }
    }
}