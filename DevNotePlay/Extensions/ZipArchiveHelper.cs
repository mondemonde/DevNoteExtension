using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Player.Extensions
{
    public static class ZipArchiveHelper
    {
        /// <summary>
        /// Compresses files into a .zip archive
        /// </summary>
        /// <param name="filesToCompress"></param>
        /// List of file names (i.e. directory + file name)
        /// <param name="destination"></param>
        /// The directory where the archive will end up
        public static void ArchiveFiles(List<string> filesToCompress, string destination)
        {
            using (MemoryStream zipMS = new MemoryStream())
            {
                using (ZipArchive zipArchive = new ZipArchive(zipMS, ZipArchiveMode.Create, true))
                {
                    foreach (string file in filesToCompress)
                    {
                        string fileName = Path.GetFileName(file);

                        ZipArchiveEntry zipFileEntry = zipArchive.CreateEntry(fileName);

                        byte[] fileToZipBytes = System.IO.File.ReadAllBytes(file);

                        using (Stream zipEntryStream = zipFileEntry.Open())
                        using (BinaryWriter zipFileBinary = new BinaryWriter(zipEntryStream))
                        {
                            zipFileBinary.Write(fileToZipBytes);
                        }
                    }
                }
                using (FileStream finalZipFileStream = new FileStream(destination, FileMode.Create))
                {
                    zipMS.Seek(0, SeekOrigin.Begin);
                    zipMS.CopyTo(finalZipFileStream);
                }
            }
        }

        /// <summary>
        /// Extracts files from a give .zip file to a given destination directory
        /// </summary>
        /// <param name="zipFilePath"></param>
        /// File path of .zip
        /// <param name="destinationDirectory"></param>
        /// Directory where files are extracted
        /// <param name="overwrite"></param>
        /// Pass true to overwrite files with same name when extracting
        public static void ExtractFiles(string zipFilePath, string destinationDirectory, bool overwrite)
        {
            var archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Read);
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                // Uses ExtractToFile because it supports overwriting files, ExtractToDirectory does not 
                string destinationFileName = Path.Combine(destinationDirectory, file.FullName);
                file.ExtractToFile(destinationFileName, overwrite);
            }
        }
    }
}
