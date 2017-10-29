using System;
using System.IO;

namespace HashGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args == null || args.Length != 2)
			{
				Console.WriteLine("Arg format: <hash-type: md5 | sha1 | sha256 | sha384 | sha512> <PathToFile: \"...\">");
				return;
			}

			string hashType = args[0];
			string pathToFile = args[1];

			try
			{
				using (var stream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None))
				{
					if (hashType == "md5")
					{
						using (var hash = new System.Security.Cryptography.MD5CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data).Replace("-", "").ToLower();
							Console.WriteLine("HEX: " + hex);
						}
					}
					else if (hashType == "sha1")
					{
						using (var hash = new System.Security.Cryptography.SHA1CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data).Replace("-", "").ToLower();
							Console.WriteLine("HEX: " + hex);
						}
					}
					else if (hashType == "sha256")
					{
						using (var hash = new System.Security.Cryptography.SHA256CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data).Replace("-", "").ToLower();
							Console.WriteLine("HEX: " + hex);
						}
					}
					else if (hashType == "sha384")
					{
						using (var hash = new System.Security.Cryptography.SHA384CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data).Replace("-", "").ToLower();
							Console.WriteLine("HEX: " + hex);
						}
					}
					else if (hashType == "sha512")
					{
						using (var hash = new System.Security.Cryptography.SHA512CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data).Replace("-", "").ToLower();
							Console.WriteLine("HEX: " + hex);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}
	}
}
