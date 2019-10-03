/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  Class for retrieving registry data from .dat file
/// </summary>
/// 
using Registry;
using Registry.Abstractions;
using System.Collections.Generic;

namespace SequencePeeker.Code
{
    public struct RegKey
    {
        public string Key { get; private set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }

        public RegKey(string key)
        {
            Key = key;
            Name = Type = Data = string.Empty;
        }
    }

    public static class RegData
    {
        public static List<RegKey> Get(string path)
        {
            //get the root
            RegistryHive hive = new RegistryHive(path);
            hive.ParseHive();

            //create list
            List<RegKey> results = new List<RegKey>();

            //parse recursively
            _getKey(hive.Root, results);

            //and return
            return results;
        }


        private static void _getKey(RegistryKey key, List<RegKey> destination)
        {
            //check if destination is writable
            if (destination == null) return;

            //get values
            foreach (var item in key.Values)
            {
                
                //store key
                string keypath = key.KeyPath;
                RegKey keyinfo = new RegKey(keypath.Substring(keypath.IndexOf("REGISTRY") + 9));

                //store value
                keyinfo.Name = item.ValueName;
                keyinfo.Type = item.ValueType;
                if(item.ValueData != "00-00-00-00") keyinfo.Data = item.ValueData;

                //add to destination
                destination.Add(keyinfo);

            }
            
            //recurse
            foreach (var item in key.SubKeys)
            {
                _getKey(item, destination);
            }
        }
    }
}