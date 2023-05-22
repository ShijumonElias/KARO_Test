using FileParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace FileParser.BL
{
    public class clsFileGenerator
    {
        public static void CreatePdfOrDocxFromBase64(clsFileData _fileData)
        {
            try
            {
                string outputFilePath = Path.Combine(_fileData.strDestFolderPath, _fileData.strDestFileName);
                byte[] FileBytes = Convert.FromBase64String(_fileData.strbase64Data);

                // check if the destination folder exists and if not try to create the folder

                if (!Directory.Exists(_fileData.strDestFolderPath))
                {
                    Console.WriteLine("The folder does not exist. Creating the folder...");
                    Directory.CreateDirectory(_fileData.strDestFolderPath);
                }
                
                //check if the destination file already exists and delete if its there
                if (File.Exists(outputFilePath))
                {
                    Console.WriteLine("The file already exists. Deleting the existing file...");
                    File.Delete(outputFilePath);
                }
                //create new file

                if (_fileData.strFileExtension == ".pdf") 
                { 
                    using (FileStream fs = new FileStream(outputFilePath, FileMode.Create))
                    {
                        fs.Write(FileBytes, 0, FileBytes.Length);
                    }
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(FileBytes))
                    {
                        using (DocX doc = DocX.Load(ms))
                        {
                            doc.SaveAs(outputFilePath);
                        }
                    }
                }
                // set the time stamp for the new file

                File.SetCreationTime(outputFilePath, _fileData.dtFileTimeStamp);

                Console.WriteLine(_fileData.strFileExtension.Substring(1) + " file created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the "+ _fileData.strFileExtension.Substring(1)  + " file: " + ex.Message);
            }
        }

    }
}
