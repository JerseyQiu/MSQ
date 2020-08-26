using System;
using System.Collections;
using System.IO;
using System.Resources;
using System.Text;

namespace Impac.Mosaiq.IQ.Utilities.AttributeGenerator
{
    /// <summary>
    /// This application takes a resource file, a C# file, and a namespace as input.  It then iterates through
    /// the resource file and generates a C# file with attributes that wrap the various entries in the resource file
    /// using the namespace that is passed in.  It is for use with activity assemblies, and allows the developer to place
    /// the proper string in a resource file, build the project, and then have a generated file of attributes which
    /// can then be placed on activities and activity properties.
    /// </summary>
    class Program
    {
        #region Templates and Constants
        private const string FileTemplate =
@"//THIS IS A GENERATED FILE.  DO NOT MODIFY.

// ReSharper disable RedundantUsingDirective
using System;
using System.ComponentModel;
using Impac.Mosaiq.IQ.Core.Definitions.Activities;
// ReSharper restore RedundantUsingDirective

namespace #NAMESPACE#
{
    #region Generated Attributes
    // ReSharper disable InconsistentNaming
#BODY#
    // ReSharper restore InconsistentNaming
    #endregion
}";

        private const string CategoryTemplate =
@"
    /// <summary> Generated attributes for #RESOURCE#.#KEY# resource </summary>
    public class #KEY#Attribute : CategoryAttribute
    {
        /// <summary>Gets the localized category name </summary>
        protected override string GetLocalizedString(string value)
        {
            return #RESOURCE#.#KEY#;
        }
    }";

        private const string DisplayNameTemplate =
@"
    /// <summary> Generated attributes for #RESOURCE#.#KEY# resource </summary>
    public class #KEY#Attribute : DisplayNameAttribute
    {
        /// <summary>Sets the localized display name </summary>
        public #KEY#Attribute()
        {
            DisplayNameValue = #RESOURCE#.#KEY#;
        }
    }";

        private const string DescriptionTemplate =
@"
    /// <summary> Generated attributes for #RESOURCE#.#KEY# resource </summary>
    public class #KEY#Attribute : DescriptionAttribute
    {
        /// <summary>Sets the localized description </summary>
        public #KEY#Attribute()
        {
            DescriptionValue = #RESOURCE#.#KEY#;
        }
    }";

        private const string ActivityGroupTemplate =
@"
    /// <summary> Generated attributes for #RESOURCE#.#KEY# resource </summary>
    public class #KEY#Attribute : ActivityGroupAttribute
    {
        /// <summary> The localized name of the activity group </summary>
        public override String GroupName { get {return #RESOURCE#.#KEY#;} }
    }";

        // ReSharper disable InconsistentNaming
        private const string NAMESPACE = "#NAMESPACE#";
        private const string RESOURCE = "#RESOURCE#";
        private const string KEY = "#KEY#";
        private const string BODY = "#BODY#";
        // ReSharper restore InconsistentNaming
        #endregion

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Invalid Arguments");
                Console.WriteLine("Argument 1:  Path to the .resx file to process.");
                Console.WriteLine("Argument 2:  Path to the target .cs file to generate.");
                Console.WriteLine("Argument 3:  Namespace which the code will be generated under.");
                throw new ArgumentException("Application takes three arguments.");
            }
                
            string resourceFile = args[0];
            string codeFile = args[1];
            string nspace = args[2];


            if (!resourceFile.EndsWith(".resx") || !File.Exists(resourceFile))
                throw new ArgumentException("Invalid resource file.", resourceFile);

            if (!codeFile.EndsWith(".cs") || !File.Exists(codeFile))
                throw new ArgumentException("Invalid .cs file", codeFile);

            if (string.IsNullOrEmpty(nspace))
                throw new ArgumentException("Namespace cannot be blank");

            string resourceName = resourceFile
                .Substring(resourceFile.LastIndexOf(@"\") + 1)
                .Replace(".resx", String.Empty)
                .Trim();



            //Open resource file and genenerated code file
            var reader = new ResXResourceReader(resourceFile);
            var csFile = new StreamWriter(codeFile, false);
            var builder = new StringBuilder();

            //Loop through all resources in the file and generate an attribute for each one.
            foreach (DictionaryEntry d in reader)
            {
                var strKey = d.Key.ToString();

                //Generate a category attribute.
                if (strKey.EndsWith("Category"))
                {
                    builder.AppendLine(CategoryTemplate
                        .Replace(RESOURCE, resourceName)
                        .Replace(KEY, strKey));
                }

                //Generate a description attribute.
                if (strKey.EndsWith("Description"))
                {
                    builder.AppendLine(DescriptionTemplate
                        .Replace(RESOURCE, resourceName)
                        .Replace(KEY, strKey));
                }

                //Generate a DisplayName attribute.
                if (strKey.EndsWith("DisplayName"))
                {
                    builder.AppendLine(DisplayNameTemplate
                        .Replace(RESOURCE, resourceName)
                        .Replace(KEY, strKey));
                }

                //Generate an activity group attribute
                if(strKey.EndsWith("ActivityGroup"))
                {
                    builder.AppendLine(ActivityGroupTemplate
                        .Replace(RESOURCE, resourceName)
                        .Replace(KEY, strKey));
                }
            }
            
            //Write the contents to the C# file
            csFile.WriteLine(FileTemplate
                .Replace(NAMESPACE, nspace)
                .Replace(BODY, builder.ToString()));

            //Close resource file and C# file.
            csFile.Flush();
            csFile.Close();
            reader.Close();
        }
    }
}
