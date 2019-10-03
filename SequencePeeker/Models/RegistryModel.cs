/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  ASP Model for registry tab
/// </summary>

using SequencePeeker.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SequencePeeker.Models
{
    public class RegistryModel
    {
        public IEnumerable<IGrouping<string, RegKey>> Keys { get; set; }

        public RegistryModel(Guid ID)
        {
            //check if there is data
            if (string.IsNullOrWhiteSpace(DataStore.GetPath(ID, PackageFileType.REGISTRY)))
            {
                Keys = Enumerable.Empty<IGrouping<string, RegKey>>();
                return;
            }

            //get data
            Keys = RegData.Get(DataStore.GetPath(ID, PackageFileType.REGISTRY)).GroupBy(a => a.Key);
        }
    }
}