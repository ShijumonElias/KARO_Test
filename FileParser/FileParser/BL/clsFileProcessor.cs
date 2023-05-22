using FileParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser.BL
{
    internal class clsFileProcessor
    {
        public string rec_FileName { get; set; }
        public string rec_DateTimeString { get; set; }
        public string rec_Base64Data { get; set; }


        public void ProcessFile(clsFileData _fileData)
        {
            try
            {

                // Chk if the file exists in the provided path

                if (!File.Exists(_fileData.strSourceFilename))
                {
                    Console.WriteLine("The specified text file does not exist.");
                    return;
                }

                /****************************************************************************************
                if the file exists,read all the records line by line and sperate all 3 parts by "|"
                part 1 : File name
                part 2 : DatetimeStamp
                part3 : base64 data
                ****************************************************************************************/

                string[] lines = File.ReadAllLines(_fileData.strSourceFilename);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    if (parts.Length != 3)
                    {
                        Console.WriteLine("Invalid format in record " + line + ". This record will be skipped.");
                        continue;
                    }

                    rec_FileName = parts[0];
                    rec_DateTimeString = parts[1];
                    rec_Base64Data = parts[2];

                    //validate the file extention. Here only .docx or .pdf is considered.
                    _fileData.strFileExtension = Path.GetExtension(rec_FileName);

                    if (_fileData.strFileExtension != ".pdf" && _fileData.strFileExtension != ".docx")
                    {
                        _fileData.strFileExtension = "";
                        Console.WriteLine("Invalid file extention in record " + line + ". This record will be skipped.");
                        continue;
                    }
                    else
                    {
                        _fileData.strDestFileName = rec_FileName;

                    }

                    // Validate the date format
                    DateTime recordDateTime;
                    if (!DateTime.TryParseExact(rec_DateTimeString, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out recordDateTime))
                    {
                        Console.WriteLine("Invalid date format in record " + line + ". This record will be skipped.");
                        continue;
                    }
                    else
                    {
                        _fileData.dtFileTimeStamp = recordDateTime;
                    }

                    // Convert the base64 data to bytes, to check its validity
                    byte[] recordData;
                    try
                    {
                        recordData = Convert.FromBase64String(rec_Base64Data);
                        _fileData.strbase64Data = rec_Base64Data;
                        _fileData.data = recordData;
                    }
                    catch (FormatException)
                    {
                        _fileData.strbase64Data = null;
                        _fileData.data = null;
                        Console.WriteLine("Invalid base64 data in record " + line + ". This record will be skipped.");
                        continue;
                    }


                    //The read records are valid!!, So create the respective
                    //files in the destination folder.

                    try
                    {
                        clsFileGenerator.CreatePdfOrDocxFromBase64(_fileData);
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("Error generating the " + _fileData.strFileExtension.Substring(1) +" file: " + ex.Message);
                    }

        }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing the file: " + ex.Message);
            }
        }
    }
}
