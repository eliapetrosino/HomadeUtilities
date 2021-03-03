using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using MyUtilities.ClassGenerator;

namespace MyUtilities.CSV_Utilities
{
   public class CSV_Reader
   {
      public string Path;
      public CSV_Reader(string path) => Path = path;


      private void ConvertToDotCS(EmptyClass embrion) {
         var info = new FileInfo(Path);
         embrion.Name = info.Name;
         embrion.Properties = this.GetProperties();
         embrion.SaveAsDotCS(embrion.Name);
      }

      public List<string> GetCSVLines() { return File.ReadLines(Path).ToList(); }
      public List<T> GetData<T>()
      {
         var list = new List<T>();
         var lines = GetCSVLines();
         var header = lines.First();
         var columnNames = header.Split(';');
         var rows = lines.Skip(1);
         PropertyInfo[] properties = typeof(T).GetProperties();

         rows.ToList().ForEach(row =>
         {
            T obj = (T)Activator.CreateInstance(typeof(T));
            var cells = row.Split(';'); int index = 0;

            foreach (var columnName in columnNames)
            {
               var prop = properties
               .SingleOrDefault(p => p.Name == columnName);
               Type propType = prop.PropertyType;
               var value = cells[index++];
               prop.SetValue(obj, Convert.ChangeType(value, propType));
            }

            list.Add(obj);

         });
         return list;
      }
      public List<string> GetProperties() 
      { 
         return GetCSVLines().First()
         .Split(';').ToList();
      }
   }
}