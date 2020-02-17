using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using FileHelpers;
namespace MyFunctions
{
	namespace FileSystem
	{
		public class FileType
		{
			Dictionary<string, string> m_fileTypes = new Dictionary<string, string>();
			Dictionary<string, string> m_fileTypesToWrite = new Dictionary<string, string>();

			//Constructor - initialises the class
			public FileType()
			{
				InitialiseDictionary();
			}

			private void InitialiseDictionary()
			{
				FileHelperEngine<FileTypes> engine = new FileHelperEngine<FileTypes>();
				var result = engine.ReadFile("extensions.txt");
				foreach (FileTypes f in result)
				{
					m_fileTypes[f.extension] = f.filetype;
				}
			}

			public string GetFileType(string fileName)
			{
				string fileType="";
				string ext = "";
				ext = Path.GetExtension(fileName).ToLower().Replace(".","");
				if (m_fileTypes.TryGetValue(ext, out fileType))
				{
					return fileType;
				}
				else
				{
					return GetTypeFromRegistry(ext);
				}
			}

			public void WriteNewExtensionsToFile()
			{
				var engine = new FileHelperAsyncEngine<FileTypes>();

				using (engine.BeginAppendToFile("extensions.txt"))
				{
					foreach (var dic in this.m_fileTypesToWrite)
					{
						var o = new FileTypes();
						o.extension = dic.Key;
						o.filetype = dic.Value;
						engine.WriteNext(o);
					}
				}
			}

			private string GetTypeFromRegistry(string ext)
			{
				string fileType = "";
				string ext1 = "";
				//Search all keys under HKEY_CLASSES_ROOT
				ext1 = (!ext.StartsWith(".")) ? "." + ext : ext;

				foreach (string subKey in Registry.ClassesRoot.GetSubKeyNames())
				{
					
					if (string.IsNullOrEmpty(subKey))
					{
						continue;
					}

					if (subKey.CompareTo(ext1) == 0)
					{
						//File extension found. Get Default Value
						try
						{
							fileType = Registry.ClassesRoot.OpenSubKey(subKey).GetValue("").ToString();
							break;
						}
						catch {continue;}
					}
				}
				fileType = (fileType.Length > 0) ? fileType : ext;
				m_fileTypes[ext] = fileType;
				m_fileTypesToWrite[ext] = fileType;
				return fileType;
			}

			[DelimitedRecord("\t")]
			private class FileTypes
			{
				public string extension="";
				public string filetype="";
			}

		}
	}
}

