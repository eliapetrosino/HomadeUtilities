using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;

namespace MyUtilities.CSV_Utilities
{
   public class CsvGenerator
   {
      static public void SaveToCSV<T>(ICollection<T> source, string path)
      {
         PropertyInfo[] info = typeof(T).GetProperties();

         var header = ""; int index = 0;
         foreach (var property in info) {
            header += $"{property.Name}" + 
            (index++ == info.Length - 1 ? "" : ";");
         }

         File.WriteAllText(path, header);
         using (StreamWriter sw = File.AppendText(path))
         {
            string record = "\n"; index = 0;
            foreach(T obj in source) {
               info.ToList().ForEach(property => {
                  record += property.GetValue(obj) + 
                  (index++ == info.Length - 1 ? "" : ";");
               });
               sw.WriteLine(record);
               record = ""; index = 0;
            }
         }
      }
   }
}