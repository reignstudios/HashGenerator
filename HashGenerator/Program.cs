using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace HashGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args == null || args.Length < 2)
			{
				Console.WriteLine("HashGenerator v1.1.0");
				Console.WriteLine("Arg format: -type=<md5 | sha1 | sha256 | sha384 | sha512> -file=<\"PathToFile\"> | -hash=<HashValue> -search=<\"PathToFolder\">");
				return;
			}

			string hashType = null;
			string path = null;
			string hashValue = null;
			foreach (string arg in args)
			{
				var keyValue = arg.Split('=');
				if (keyValue == null || keyValue.Length != 2)
				{
					Console.WriteLine("Invalid arg: " + arg);
					return;
				}

				switch (keyValue[0])
				{
					case "-type": hashType = keyValue[1]; break;
					case "-file": case "-search": path = keyValue[1]; break;
					case "-hash": hashValue = keyValue[1]; break;
				}
			}

			if (string.IsNullOrEmpty(hashType))
			{
				Console.WriteLine("Error: '-type' not set!");
				return;
			}

			if (!string.IsNullOrEmpty(hashValue) && !string.IsNullOrEmpty(path))
			{
				if (SearchPathForFileHash(hashType, hashValue, path)) Console.WriteLine("Search completed!");
				else Console.WriteLine("No file found with a hash of: " + hashValue);
			}
			else if (!string.IsNullOrEmpty(path))
			{
				Console.WriteLine(GenerateFileHash(hashType, path));
			}
			else
			{
				Console.WriteLine("Error: '-file' or ('-hash' + '-search') much be set!");
			}
		}

		[Pure]
		private static string GenerateFileHash(string hashType, string pathToFile)
		{
			try
			{
				using (var stream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None))
				{
					if (hashType == "md5")
					{
						using (var hash = new System.Security.Cryptography.MD5CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							return BitConverter.ToString(data).Replace("-", "").ToLower();
						}
					}
					else if (hashType == "sha1")
					{
						using (var hash = new System.Security.Cryptography.SHA1CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							return BitConverter.ToString(data).Replace("-", "").ToLower();
						}
					}
					else if (hashType == "sha256")
					{
						using (var hash = new System.Security.Cryptography.SHA256CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							return BitConverter.ToString(data).Replace("-", "").ToLower();
						}
					}
					else if (hashType == "sha384")
					{
						using (var hash = new System.Security.Cryptography.SHA384CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							return BitConverter.ToString(data).Replace("-", "").ToLower();
						}
					}
					else if (hashType == "sha512")
					{
						using (var hash = new System.Security.Cryptography.SHA512CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							return BitConverter.ToString(data).Replace("-", "").ToLower();
						}
					}
					else
					{
						throw new Exception("Unsupported hashType: " + hashType);
					}
				}
			}
			catch (Exception e)
			{
				return "Error: " + e.Message;
			}
		}

		[Pure]
		private static bool SearchPathForFileHash(string hashType, string hash, string searchPath)
		{
			try
			{
				foreach (string filePath in Directory.GetFiles(searchPath))
				{
					string fileHash = GenerateFileHash(hashType, filePath);
					if (hash == fileHash)
					{
						Console.WriteLine(string.Format("File found: '{0}'", filePath));
						return true;
					}
				}

				foreach (string subSearchPath in Directory.GetDirectories(searchPath))
				{
					if (SearchPathForFileHash(hashType, hash, subSearchPath)) return true;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
			
			return false;
		}
	}
}
