using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonReader
{
    class Program
    {
        static void Main(string[] args)
        {
            // Reading setting file
            FileReader fr = new FileReader("settings.json");
            dynamic settings= fr.ReadFile();
            bool isActive = settings.activeExceptions.Value;
            if (isActive)
            {

                var fileOfExceptionsPath = settings.filename.Value;
                string encode = settings.encode.Value;
                string listName = settings.listName.Value;
                // Reading the product exception to follow CRM flux instead of Odoo
                FileReader fileExceptions = new FileReader(fileOfExceptionsPath, encode);
                dynamic exceptions = fileExceptions.ReadFile();
                string[] productIds = exceptions[listName].ToObject<string[]>();
            }
        }
    }
}
