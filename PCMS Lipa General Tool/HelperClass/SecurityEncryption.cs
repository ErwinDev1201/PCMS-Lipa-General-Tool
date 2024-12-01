using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace YourmeeAppLibrary.Security
{
	public  class SecurityEncryption
	{
		public string PassHash(string data)
		{
			SHA1 sha = SHA1.Create();
			byte[] hashdata = sha.ComputeHash(Encoding.Default.GetBytes(data));
			StringBuilder returnValue = new();

			for (int i = 0; i < hashdata.Length; i++)
			{
				returnValue.Append(hashdata[i].ToString());
			}
			return returnValue.ToString();

		}

		public void EncryptConfigSection(string sectionName)
		{
			// Get the configuration file
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			// Get the section to encrypt
			ConfigurationSection section = config.GetSection(sectionName);

			if (section != null && !section.SectionInformation.IsProtected)
			{
				// Encrypt the section using the DataProtectionProvider
				section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");

				// Save the changes to the config file
				section.SectionInformation.ForceSave = true;
				config.Save(ConfigurationSaveMode.Full);

				Console.WriteLine($"{sectionName} section encrypted.");
			}
		}

		public void DecryptConfigSection(string sectionName)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			ConfigurationSection section = config.GetSection(sectionName);

			if (section != null && section.SectionInformation.IsProtected)
			{
				section.SectionInformation.UnprotectSection();
				section.SectionInformation.ForceSave = true;
				config.Save(ConfigurationSaveMode.Full);

				Console.WriteLine($"{sectionName} section decrypted.");
			}
		}

	}
}
