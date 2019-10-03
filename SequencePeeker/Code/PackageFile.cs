/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  Class for single package
/// </summary>

using System;
using System.IO;
using System.IO.Compression;
using System.Web;

namespace SequencePeeker.Code
{
    public class PackageFile
    {
        //props
        public bool IsAppVPackage { get; set; } = false;
        public Guid ID { get; private set; }
        public string Status { get; private set; }
        
        //constructor
        public PackageFile(HttpPostedFileBase file)
        {
            //get ID
            ID = Guid.NewGuid();

            //figure out if upload seems valid
            if (!_acceptFile(file)) return;

            //save file
            string path = Path.Combine(Common.App_Data, ID.ToString() + ".appv");
            file.SaveAs(path);

            //pre-inspect file
            if (!_inspectFile(path)) return;

            //everything seems fine
            IsAppVPackage = true;
        }

        private bool _acceptFile(HttpPostedFileBase file)
        {
            //check if we have a file, with enough content to be an .appv
            if (file == null || file.ContentLength < 1000)
            {
                Status = "Inputfile is 'null' or has no content";
                return false;
            }

            //check if correct extension
            if (Path.GetExtension(file.FileName) != ".appv")
            {
                Status = "Inputfile does not have the '.appv' extension";
                return false;
            }

            /* this breaks the app on atleast google chrome
            //check if of type x-zip-compressed
            if (file.ContentType != "application/x-zip-compressed")
            {
                Status = String.Format("Inputfile is not of type 'application/x-zip-compressed'. It is of type: {0}", file.ContentType);
                return false;
            }
            */

            //passed
            Status = "File checks passed";
            return true;
        }

        private bool _inspectFile(string path)
        {
            string folder = Path.Combine(Path.GetDirectoryName(path), ID.ToString());

            //try to extract package
            try
            {
                ZipFile.ExtractToDirectory(path, folder);
            }
            catch (Exception ex)
            {
                Status = ex.Message;
                return false;
            }

            //Check for files           
            foreach (var filename in Common.PackageFiles.Values)
            {
                if(!File.Exists(Path.Combine(folder, filename)))
                {
                    Status = string.Format("Package is missing the file {0}", filename);
                    return false;
                }
            }

            //check for rootdir
            if(!Directory.Exists(Path.Combine(folder, "Root")))
            {
                Status = "Package is missing the Root-directory";
                return false;
            }

            //cleanup .appv and rootdir to preserve space
            File.Delete(path);
            Directory.Delete(Path.Combine(folder, "Root"), true);

            //passed
            Status = "Package inspection succesful";
            return true;
        }

    }
}