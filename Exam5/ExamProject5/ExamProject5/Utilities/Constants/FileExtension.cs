﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject5.Utilities.Constants
{
	
		public static class FileExtension
		{
			public static bool CheckContentType(this IFormFile file, string contentType)
			{
				return file.ContentType.ToLower().Trim().Contains(contentType.ToLower().Trim());
			}
			public static bool CheckFileSize(this IFormFile file, double size)
			{
				return file.Length / 1024 < size;
			}

			public static async Task<string> SaveAsync(this IFormFile file, string rootpath)
			{
				string filename = Guid.NewGuid().ToString() + file.FileName;
				using (FileStream fileStream = new FileStream(Path.Combine(rootpath, filename), FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
				return filename;
			}
		}
}
