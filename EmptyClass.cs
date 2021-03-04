namespace MyUtilities.ClassGenerator
{
   public class EmptyClass
   {
      public string Name { get; set; } = "";
      public string Scope { get; set; } = "";
      public string Behave { get; set; } = "";
      public ICollection<string> Properties { get; set; }
      public ICollection<string> Fields { get; set; }
      private List<string> Types = new List<string>
      { "string", "bool", "int", "uint", "long", "short", "decimal", "float", "double", "byte", "sbyte" };


      public IEnumerable<string> Shape()
      {
         return new List<string>
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

      public void CreateFields()
      {
         Fields = new List<string>();
         foreach (var p in Properties)
            Fields.Add(p.ToLower());
      }

      protected string SplitFields()
      {
         if (Fields == null) return "";
         var builder = new StringBuilder();
         foreach (var f in Fields)
            builder.Append(f.Trim() + ";\n");
         return builder.ToString();
      }

      protected string SplitProperties()
      {
         if (Properties.First() == "") return "";
         var builder = new StringBuilder();
         bool propertyIsRightTyped = false;

         foreach (var p in Properties) {
            Types.ForEach(t => { if (p.Contains(t)) propertyIsRightTyped = true; });
            builder.Append($"public {(propertyIsRightTyped ? p : "string " + p)} " + "{ get; set; }\n");
         }
         return builder.ToString();
      }

      public string SaveAsDotCS(string path)
      {
         File.WriteAllLines($@"{path}\{Name}.cs", Shape());
         return $"Class saved to {Name}.cs into {path}";
      }
   }
}
