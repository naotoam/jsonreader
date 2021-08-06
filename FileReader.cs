using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonReader
{
    public class FileReader
    {
        public string PathFile { get; set; }
        private Encoding Encoding { get; set; }
        private enum ExtensionAdmitted
        {
            json
        }

        public FileReader(string pathFile, string encode = null)
        {
            var values = Enum.GetNames(typeof(ExtensionAdmitted));
            string extension = !string.IsNullOrEmpty(pathFile) ? Path.GetExtension(pathFile).ToLower().Replace(".", "") : string.Empty;
            if(string.IsNullOrEmpty(pathFile) || !values.ToList().Any(x => x == extension))
            {
                throw new Exception("File type is not admitted, please provide a valid file.");
            }
            this.Encoding = SetEncoding(encode);
            this.PathFile = pathFile;
        }

        public object ReadFile()
        {
            // Note: To add file to compilation environment, click properties on the file, then change
            // set Build Action to Content, and Copy to Output
            string fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ProductExceptions\"+this.PathFile);
            string jsonString = File.ReadAllText(fullPath, this.Encoding);
            dynamic fileContent = JsonConvert.DeserializeObject<dynamic>(jsonString);
            return fileContent;
        }

        private Encoding SetEncoding(string encode)
        {
            encode = string.IsNullOrEmpty(encode) ? "default" : encode.ToLower();
            switch (encode)
            {
                case "utf-8":
                    return Encoding.UTF8;
                case "ascii":
                    return Encoding.ASCII;
                case "unicode":
                    return Encoding.Unicode;
                default:
                    return Encoding.UTF8; ;
            }
        }
    }
}
