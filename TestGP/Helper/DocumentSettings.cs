
	namespace ImageHandling.Helper
	{
		public static class DocumentSettings
		{
			public static async Task<string> UploadFileAsync(IFormFile file, string FolderName)
			{
				string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", FolderName);
				if (!Directory.Exists(folderPath))
					Directory.CreateDirectory(folderPath);
				string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
				string filePath = Path.Combine(folderPath, fileName);
				using var fileStream = new FileStream(filePath, FileMode.Create);
				await file.CopyToAsync(fileStream);
				return fileName;

			}
			public static void DeleteFile(string fileName, string FolderName)
			{
				string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", FolderName, fileName);
				if (File.Exists(folderPath))
					File.Delete(folderPath);
			}
			/*public static string GetFullPath(string fileName,string FolderName) {
				string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", FolderName, fileName);
				return fullPath;
			}*/
			public static string GetImageUrl(string imageName, string folderName)
			{
				// Assuming the images are served from a specific domain
				string baseUrl = "https://theseventh.azurewebsites.net"; // Replace "yourdomain.com" with your actual domain

				// Construct the URL based on the file name and folder name
				string imageUrl = $"{baseUrl}/{folderName}/{imageName}";

				return imageUrl;
			}
			public static async Task<string> EditFile(IFormFile newImage, string oldImage, string folderName)
			{
				DeleteFile(oldImage, folderName);
				return await UploadFileAsync(newImage, folderName);
			}
		}
	}
