using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Imagegram.Services
{
    public class FileService
    {
        public string Upload(IFormFile file, Guid accountId)
        {

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            string fileName = string.Concat(Guid.NewGuid(), "_", accountId, ".jpg");

            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return dbPath;
        }

        public void Delete(string imageUrl)
        {
            string imagePathToBeDeleted = Path.Combine(Directory.GetCurrentDirectory(), imageUrl);

            imagePathToBeDeleted = imagePathToBeDeleted.Replace(@"\", "/");

            if (File.Exists(imagePathToBeDeleted))
            {
                File.Delete(imagePathToBeDeleted);
            }
        }
    }
}
