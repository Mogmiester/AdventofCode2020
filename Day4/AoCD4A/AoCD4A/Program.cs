using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AoCD4A
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day4A.txt";
            string fileAsString = File.ReadAllText(InputFileLocation + InputFileName);

            string[] initialSplit = new string[1];
            initialSplit[0] = "\n\n";
            string[] documentsAllInfo = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);

            string[] secondSplit = new string[2];
            secondSplit[0] = " ";
            secondSplit[1] = "\n";

            string[] fieldsRequired = new string[8];
            fieldsRequired[0] = "byr";
            fieldsRequired[1] = "iyr";
            fieldsRequired[2] = "eyr";
            fieldsRequired[3] = "hgt";
            fieldsRequired[4] = "hcl";
            fieldsRequired[5] = "ecl";
            fieldsRequired[6] = "pid";
            fieldsRequired[7] = "cid";

            HashSet<Dictionary<string, string>> allPassportInfo = new HashSet<Dictionary<string, string>>();
            int acceptableDocuments = 0;

            foreach (string documentInfo in documentsAllInfo)
            {
                Dictionary<string, string> passwordInfo = new Dictionary<string, string>();
                string[] documentElements = documentInfo.Split(secondSplit, StringSplitOptions.RemoveEmptyEntries);
                foreach (string passportField in documentElements)
                {
                    string[] keyvalues = passportField.Split(':');
                    passwordInfo.Add(keyvalues[0], keyvalues[1]);
                }
                allPassportInfo.Add(passwordInfo);
            }

            foreach (Dictionary<string, string> passportInfo in allPassportInfo)
            {
                bool acceptableID = true;
                //skip cid - not needed :)
                for (int i = 0; i < 7; i++)
                {
                    if (!passportInfo.ContainsKey(fieldsRequired[i]))
                    {
                        acceptableID = false;
                        break;
                    }
                }

                if (acceptableID) { acceptableDocuments++; };
            }

            Console.WriteLine(acceptableDocuments.ToString());
        }
    }
}