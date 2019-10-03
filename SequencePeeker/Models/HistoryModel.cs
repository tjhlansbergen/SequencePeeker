/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  ASP Model for history tab
/// </summary>

using SequencePeeker.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SequencePeeker.Models
{
    public class HistoryModel
    {
        public IEnumerable<XElement> History { get; set; }

        public HistoryModel(Guid ID)
        {
            //check if there is data
            if (string.IsNullOrWhiteSpace(DataStore.GetPath(ID, PackageFileType.PACKAGEHISTORY)))
            {
                History = Enumerable.Empty<XElement>();
                return;
            }
                

            //parse xml
            XDocument xdoc = XDocument.Load(DataStore.GetPath(ID, PackageFileType.PACKAGEHISTORY));
            var elements = xdoc.Descendants();

            //get all services
            History = elements.Where(a => a.Name.LocalName == "PackageHistoryItem");
        }
    }
}