/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  ASP Model for Services tab
/// </summary>

using SequencePeeker.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SequencePeeker.Models
{
    public class ServicesModel
    {
        public IEnumerable<XElement> Services { get; set; }

        public ServicesModel(Guid ID)
        {
            //check if there is data
            if (string.IsNullOrWhiteSpace(DataStore.GetPath(ID, PackageFileType.APPXMANIFEST)))
            {
                Services = Enumerable.Empty<XElement>();
                return;
            }

            //parse xml
            XDocument xdoc = XDocument.Load(DataStore.GetPath(ID, PackageFileType.APPXMANIFEST));
            var elements = xdoc.Descendants();

            //get all services
            Services = elements.Where(a => a.Name.LocalName == "Service");
        }
    }
}