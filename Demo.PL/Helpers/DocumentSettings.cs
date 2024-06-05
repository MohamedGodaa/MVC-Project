using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploudFile(IFormFile file,string folderName)
        {
            //1. Get Located Folder Path
            //string folderPath = "wwwroot\\Files\\" + folderName;
            //string folderPath = Directory.GetCurrentDirectory()+"wwwroot\\" + folderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            //2. GetFile Name And Make It Unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}"; // make it Unique
            // 3. Get File Path [FolderPath + FileName]
            string filePath = Path.Combine(folderPath, fileName);
            //4. Save Files As Streams
            using var fileStream = new FileStream(filePath, FileMode.CreateNew);
            file.CopyTo(fileStream);
            return fileName;

        }

        public static void DeleteFile(string fileName,string folderName)
        {
            //1. GetFile Path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName,fileName);
            //2. Check If File Is Exists Or Not Exists Remove It 
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
