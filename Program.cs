using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Cryptography;
using ceTe.DynamicPDF.Merger;
using System;
using System.Text.RegularExpressions;

namespace example_encrypt_pdf_dotnet
{
    // This example shows how to encrypt a PDF document.
    // It references the ceTe.DynamicPDF.CoreSuite.NET Nuget package.
    class Program
    {
        static void Main(string[] args)
        {
            EncryptNewPDF();

            EncryptExistingPDF();
        }

        // Encrypts a new PDF created from scratch
        // This code uses the DynamicPDF Generator for .NET product.
        // Import the ceTe.DynamicPDF namespace for the Document and Page classes.
        // Import the ceTe.DynamicPDF.Cryptography namespace for the Aes256Security class.
        private static void EncryptNewPDF()
        {
            //Create a Document object
            Document document = new Document();

            //Create a Page and add to Document
            Page page = new Page();
            document.Pages.Add(page);

            //Create a Aes256Security object with passwords and required options
            Aes256Security security = new Aes256Security("owner", "user");
            security.AllowAccessibility = true;
            security.AllowFormFilling = false;

            //Set security to the document
            document.Security = security;

            //Save Document
            document.Draw("output.pdf");
        }

        // Encrypts an existing PDF document
        // This code uses the DynamicPDF Merger for .NET product.
        // Import the ceTe.DynamicPDF.Merger namespace for the MergeDocument class.
        private static void EncryptExistingPDF()
        {
            //Create a MergeDocument object with the existing PDF
            MergeDocument document = new MergeDocument(GetResourcePath("doc-a.pdf"));

            //Create Security object with passwords and other settings
            Aes256Security security = new Aes256Security("owner", "user");
            security.AllowAccessibility = true;
            security.AllowFormFilling = false;

            //Set security to the Document
            document.Security = security;

            //Save document
            document.Draw("outputFromExistingFile.pdf");
        }

        // This is a helper function to get a full path to a file in the Resources folder.
        public static string GetResourcePath(string inputFileName)
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return System.IO.Path.Combine(appRoot, "Resources", inputFileName);
        }
    }
}