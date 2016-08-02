using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HashGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args == null || args.Length != 2)
			{
				Console.WriteLine("Arg format: <hash-type: -md5, -sha1, -sha256, -sha384, -sha512> <pathToFile>");
				return;
			}

			try
			{
				using (var stream = new FileStream(args[1], FileMode.Open, FileAccess.Read, FileShare.None))
				{
					if (args[0] == "-md5")
					{
						using (var hash = new System.Security.Cryptography.MD5CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data);
							Console.WriteLine("HEX: " + hex);
						}
					}
					else if (args[0] == "-sha1")
					{
						using (var hash = new System.Security.Cryptography.SHA1CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data);
							Console.WriteLine("HEX: " + hex);
						}
					}
					else if (args[0] == "-sha256")
					{
						using (var hash = new System.Security.Cryptography.SHA256CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data);
							Console.WriteLine("HEX: " + hex);
						}
					}
					else if (args[0] == "-sha384")
					{
						using (var hash = new System.Security.Cryptography.SHA384CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data);
							Console.WriteLine("HEX: " + hex);
						}
					}
					else if (args[0] == "-sha512")
					{
						using (var hash = new System.Security.Cryptography.SHA512CryptoServiceProvider())
						{
							var data = hash.ComputeHash(stream);
							string hex = BitConverter.ToString(data);
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
