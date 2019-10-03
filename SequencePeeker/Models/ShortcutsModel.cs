/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  ASP Model for shortcuts tab
/// </summary>

using SequencePeeker.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SequencePeeker.Models
{
    public class ShortcutsModel
    {
        public IEnumerable<XElement> Shortcuts { get; set; }

        public ShortcutsModel(Guid ID)
        {
            //check if there is data
            if (string.IsNullOrWhiteSpace(DataStore.GetPath(ID, PackageFileType.APPXMANIFEST)))
            {
                Shortcuts = Enumerable.Empty<XElement>();
                return;
            }

            //parse xml
            XDocument xdoc = XDocument.Load(DataStore.GetPath(ID, PackageFileType.APPXMANIFEST));
            var elements = xdoc.Descendants();

            //get all services
            Shortcuts = elements.Where(a => a.Name.LocalName == "Shortcut");
        }
    }

}