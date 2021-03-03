using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.ConsoleColor;

namespace MyUtilities.ClassGenerator.ClassUZI
{
   class Program
   {
      static void Main(string[] args)
      {
         ForegroundColor = DarkGreen;
         WriteLine("** Class Generator Tool **");
         var gen = new EmptyClass();

         bool exit = false;
         while (!exit)
         {
            WriteColoured("Class name");
            string n = ReadLine();
            if (string.IsNullOrEmpty(n)) gen.Name = "ClassName";

            WriteColoured("Class scope(public, protected etc)");
            gen.Scope = ReadLine();
            WriteColoured("Class behaviour(abstract, virtual etc)");
            gen.Behave = ReadLine();
            WriteColoured("Insert properties (<type Name>)");
            gen.Properties = ReadLine().Split(',').ToList();

            WriteColoured("Create fields? Y / N");
            if (ReadLine().ToUpper() == "Y") {
               gen.Fields = new List<string>();
               foreach (var p in gen.Properties)
               gen.Fields.Add(p.ToLower());
            }

            try { gen.SaveAsDotCS(gen.Name); }
            catch (SystemException) {
               ForegroundColor = Red;
               WriteLine("!Something went wrong - file not saved");
            }
            finally {
               WriteColoured("\nCreate another one? Y / N");
               exit = ReadLine().ToUpper() == "Y" ? false : true;
            }
         }
      }
      static public void WriteColoured(string answer) {
         ForegroundColor = DarkCyan;
         Write($"{answer} -> ");
         ForegroundColor = Gray;
      }
   }
}