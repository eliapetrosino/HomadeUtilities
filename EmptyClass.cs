using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyUtilities.ClassGenerator
{
   public class EmptyClass
   {
      public string Name { get; set; }
      public string Scope { get; set; }
      public string Behave { get; set; }
      public ICollection<string> Properties { get; set; }
      public ICollection<string> Fields { get; set; }


      private IEnumerable<string> Shape {
         get => new List<string>
         {
            "using System;", "using System.Linq;",
            "using System.Collections.Generic;",
            $"{Scope.ToLower()} {Behave.ToLower()} class {Name}",
            "{",
            SplitFields(),
            SplitProperties(),
            $"public {Name} () {{}}\n",
            "//ADD SOME COOL METHODS HERE\n",
            "}"
         };
      }

      public string SplitFields() {
         if (Fields == null) return "";
         var builder = new StringBuilder();
         foreach (var f in Fields) builder.Append(f.Trim() + ";\n");
         return builder.ToString();
      }

      public string SplitProperties() {
         if (Properties.First() == "") return "";
         var builder = new StringBuilder();
         foreach (var p in Properties)
         builder.Append("public " + p + " { get; set; }" + "\n");
         return builder.ToString();
      }

      public string SaveAsDotCS(string filename) {
         File.WriteAllLines($@".\{filename}.cs", Shape);
         return $"Class saved to {filename}.cs";
      }
   }
}
