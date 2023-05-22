using FileParser.BL;
using FileParser.Model;
using System;
using System.IO;

namespace FileParser
{
    internal class Program
    {
        //string strTextFile=string.Empty;
        //string strDestFolder = string.Empty;

        

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    Console.WriteLine(".pdf".Substring(1));
                    Console.WriteLine("Please provide text filename to be parsed and destination folder path.");
                    return;
                }

                string fileName = args[0];
                string folderPath = args[1];

                Console.WriteLine(args[0]);
                Console.WriteLine(args[1]);

                //check for valid file names

                if (fileName.Length < 5 || Path.GetExtension(fileName) != ".txt")
                {
                    Console.WriteLine(fileName);
                    Console.WriteLine("Invalid text file name. Please provide a valid text filename with extension");
                    return;
                }

                //set the file attributed to the object
                clsFileData objFileData = new clsFileData();
                objFileData.strSourceFilename = fileName;
                objFileData.strDestFolderPath = folderPath;

                clsFileProcessor _processor = new clsFileProcessor();
                _processor.ProcessFile(objFileData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);

            }
        }
    }
    
}
