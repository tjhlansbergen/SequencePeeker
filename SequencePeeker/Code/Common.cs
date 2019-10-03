/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  Global variables
/// </summary>

using System.Collections.Generic;
using System.Web.Hosting;

namespace SequencePeeker.Code
{
    public enum PackageFileType { CONTENT_TYPES, APPXBLOCKMAP, APPXMANIFEST, FILESYSTEMMETADATA, PACKAGEHISTORY, REGISTRY, STREAMMAP }

    public static class Common
    {
        public static readonly string App_Data = HostingEnvironment.MapPath("~/App_Data/");

        public static readonly Dictionary<PackageFileType, string> PackageFiles = new Dictionary<PackageFileType, string>
        {
            { PackageFileType.CONTENT_TYPES, "[Content_Types].xml" },
            { PackageFileType.APPXBLOCKMAP,  "AppxBlockMap.xml" },
            { PackageFileType.APPXMANIFEST, "AppxManifest.xml" },
            { PackageFileType.FILESYSTEMMETADATA, "FilesystemMetadata.xml" },
            { PackageFileType.PACKAGEHISTORY, "PackageHistory.xml" },
            { PackageFileType.REGISTRY, "Registry.dat" },
            { PackageFileType.STREAMMAP, "StreamMap.xml" }
        };
    }

}