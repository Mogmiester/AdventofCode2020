using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AoCD4B
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
            int acceptableDocumentsWithValidData = 0;

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

                if (acceptableID) { 
                    acceptableDocuments++;
                    passportInfo.TryGetValue("byr", out string byr);
                    passportInfo.TryGetValue("iyr", out string iyr);
                    passportInfo.TryGetValue("eyr", out string eyr);
                    passportInfo.TryGetValue("hgt", out string hgt);
                    passportInfo.TryGetValue("hcl", out string hcl);
                    passportInfo.TryGetValue("ecl", out string ecl);
                    passportInfo.TryGetValue("pid", out string pid);
                    bool validbyr = BirthYearIsValid(byr);
                    bool validiyr = IssueYearIsValid(iyr);
                    bool valideyr = ExpirationYearIsValid(eyr);
                    bool validhgt = HeightIsValid(hgt);
                    bool validhcl = HairColourIsValid(hcl);
                    bool validecl = EyeColourIsValid(ecl);
                    bool validpid = PassportIDIsValid(pid);

                    if (validbyr && validiyr && valideyr && validhgt && validhcl && validecl && validpid)
                    {
                        acceptableDocumentsWithValidData++;
                    }
                }
            }

            Console.WriteLine(acceptableDocuments.ToString());
            Console.WriteLine(acceptableDocumentsWithValidData.ToString());


        }

        public static bool BirthYearIsValid(string birthYear)
        {
            if(int.TryParse(birthYear, out int numericBirthYear))
            {
                if (numericBirthYear >= 1920 && numericBirthYear <= 2002)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IssueYearIsValid(string issueYear)
        {
            if (int.TryParse(issueYear, out int numericIssueYear))
            {
                if (numericIssueYear >= 2010 && numericIssueYear <= 2020)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ExpirationYearIsValid(string expirationYear)
        {
            if (int.TryParse(expirationYear, out int numericExpirationYear))
            {
                if (numericExpirationYear >= 2020 && numericExpirationYear <= 2030)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool HeightIsValid(string height)
        {
            if (height.Length > 2)
            {
                if (height.Substring(height.Length - 2, 2) == "cm")
                {
                    if (int.TryParse(height.Substring(0, height.Length - 2), out int heightInCenimetres))
                    {
                        if (heightInCenimetres >= 150 && heightInCenimetres <= 193)
                        {
                            return true;
                        }
                    }
                }
                else if (height.Substring(height.Length - 2, 2) == "in")
                {
                    if (int.TryParse(height.Substring(0, height.Length - 2), out int heightInInches))
                    {
                        if (heightInInches >= 59 && heightInInches <= 76)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool HairColourIsValid(string hairColour)
        {
            if (hairColour.Length > 0 && hairColour[0] == '#')
            {
                string hairColourData = hairColour.Substring(1, hairColour.Length - 1);
                return hairColourData.Length == 6 && Int32.TryParse(hairColourData, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture, out _);
            }
            return false;
        }

        public static bool EyeColourIsValid(string eyeColour)
        {
            switch (eyeColour)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    return true;
                default:
                    return false;
            }
        }

        public static bool PassportIDIsValid(string passportID)
        {
            return passportID.Length == 9 && Int32.TryParse(passportID, out _);
        }

    }
}