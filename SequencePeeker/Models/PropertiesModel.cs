/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  ASP Model for properties tab
/// </summary>
/// 
using SequencePeeker.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SequencePeeker.Models
{
    public class PropertiesModel
    {
        
        public string DisplayName { get; private set; }
        public string AppVPackageDescription { get; private set; }
        public string Version { get; private set; }
        public string PackageId { get; private set; }
        public string VersionId { get; private set; }
        public string FullVFSWriteMode { get; private set; }
        public string SequencingStationProcessorArchitecture { get; private set; }
        public List<string> TargetOSes { get; private set; }

        public PropertiesModel(Guid ID)
        {
            //check if there is data
            if (string.IsNullOrWhiteSpace(DataStore.GetPath(ID, PackageFileType.APPXMANIFEST)))
            {
                TargetOSes = new List<string>();
                return;
            }
                

            //parse xml
            XDocument xdoc = XDocument.Load(DataStore.GetPath(ID, PackageFileType.APPXMANIFEST));
            var elements = xdoc.Descendants();

            //get fields
            DisplayName = elements.SingleOrDefault(a => a.Name.LocalName == "DisplayName").Value;
            AppVPackageDescription = elements.SingleOrDefault(a => a.Name.LocalName == "AppVPackageDescription").Value;
            FullVFSWriteMode = elements.SingleOrDefault(a => a.Name.LocalName == "FullVFSWriteMode").Value;

            var identity = elements.SingleOrDefault(a => a.Name.LocalName == "Identity");
            Version = identity.Attributes().SingleOrDefault(a => a.Name.LocalName == "Version").Value;
            PackageId = identity.Attributes().SingleOrDefault(a => a.Name.LocalName == "PackageId").Value;
            VersionId = identity.Attributes().SingleOrDefault(a => a.Name.LocalName == "VersionId").Value;

            SequencingStationProcessorArchitecture = elements.SingleOrDefault(a => a.Name.LocalName == "TargetOSes").Attributes().SingleOrDefault(a => a.Name.LocalName == "SequencingStationProcessorArchitecture").Value;

            if (elements.Any(a => a.Name.LocalName == "TargetOS"))
                TargetOSes = elements.Where(a => a.Name.LocalName == "TargetOS").Select(a => a.Value).ToList();
            else
                TargetOSes = new List<string> { "Any" };
        }
    }
}