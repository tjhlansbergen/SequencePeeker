/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  ASP Model for files tab
/// </summary>

using SequencePeeker.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SequencePeeker.Models
{
    public class FilesModel
    {
        public string PackageRoot { get; private set; }
        public List<string> Files { get; private set; }

        public FilesModel(Guid ID)
        {
            Files = new List<string>();

            //check if there is data
            if (string.IsNullOrWhiteSpace(DataStore.GetPath(ID, PackageFileType.FILESYSTEMMETADATA)))
                return;
            
            //parse xml
            XDocument xdoc = XDocument.Load(DataStore.GetPath(ID, PackageFileType.FILESYSTEMMETADATA));
            var filesystem = xdoc.Descendants().SingleOrDefault(a => a.Name.LocalName == "Filesystem");

            //packege root
            PackageRoot = "Package Root: ";
            PackageRoot += filesystem.Attributes().SingleOrDefault(a => a.Name.LocalName == "Root").Value;

            //files
            foreach(var item in filesystem.Descendants())
            {
                if (item.Name.LocalName == "Entry" && item.Descendants().Count() == 0)  //only get files, not directories
                {
                    //file path
                    Files.Add(Path.Combine(item.AncestorsAndSelf().Where(a => a.Name.LocalName == "Entry").Select(a => a.Attribute("Long").Value).Reverse().ToArray()));
                }
            }
        }
    }
}