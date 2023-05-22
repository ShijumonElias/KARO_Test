using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser.Model
{
    public class clsFileData
    {
        public string strSourceFilename { get; set; }
        public string strDestFolderPath { get; set; }
        public string strDestFileName { get; set; }
        public string strFileExtension { get; set; }
        public string strbase64Data { get; set; }
        public byte[] data { get; set; } = null;
        public DateTime dtFileTimeStamp { get; set; }
        
         
    }
}
