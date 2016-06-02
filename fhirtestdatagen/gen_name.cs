using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FHIRUK.Resources;

namespace fhirtestdatagen
{
    public class HumanNameGenerator
    {
        private List<String> femaleNames = new List<string>();
        private List<String> maleNames = new List<string>();
        private List<String> familyNames = new List<string>();

        private static String femaleNamesListFile = @"..\..\lists\names.female.txt";
        private static String maleNamesListFile = @"..\..\lists\names.male.txt";
        private static String familyNamesListFile = @"..\..\lists\names.family.txt";

        private Random randomGenerator = null;

        public HumanNameGenerator()
        {
            randomGenerator = new Random(Guid.NewGuid().GetHashCode());

            LoadNamesFromFile(femaleNamesListFile, femaleNames);
            LoadNamesFromFile(maleNamesListFile, maleNames);
            LoadNamesFromFile(familyNamesListFile, familyNames);
        }

        private void LoadNamesFromFile(String listFile, List<String> list)
        {
            using (StreamReader reader = new StreamReader(listFile))
            {
                while (reader.EndOfStream == false)
                {
                    String name = reader.ReadLine();
                    name = name.Trim();
                    if (String.IsNullOrEmpty(name) == false)
                        list.Add(name);
                }

                reader.Close();
            }
        }

        public HumanNames GetRandomNames(out EnumGender gender)
        {
            HumanNames names = new HumanNames();

            // generate just one name for now
            HumanName name = GetRandomName(out gender);
            names.Add(name);

            return names;
        }

        public HumanName GetRandomName(out EnumGender gender)
        {
            List<string> given = new List<string>();
            List<string> family = new List<string>();
            gender = EnumGender.UN;

            int givenNamesCount = randomGenerator.Next(0, 100) + 1; // returns N: 1 <= N <= 100
            // we will give 25% probability of just a single name, 65% probability of 1 middle name and 10% probability of 2 middle names
            if (givenNamesCount <= 25) givenNamesCount = 1;
            else if (givenNamesCount <= 90) givenNamesCount = 2;
            else givenNamesCount = 3;

            int familyNamesCount = randomGenerator.Next(0, 100) + 1; // returns N: 1 <= N <= 100
            // we will give 95% probability of just a single family name, and 5% probability of a double family name
            if (familyNamesCount <= 95) familyNamesCount = 1;
            else familyNamesCount = 2;

            if (randomGenerator.Next(0, 2) == 0)    // 0 for female, 1 for male
            {
                gender = EnumGender.F;
                given = GetNamesFromList(givenNamesCount, femaleNames);
            }
            else
            {
                gender = EnumGender.M;
                given = GetNamesFromList(givenNamesCount, maleNames);
            }

            family = GetNamesFromList(familyNamesCount, familyNames);

            return new HumanName()
                    {
                        given = given,
                        family = family
                    };
        }

        private List<String> GetNamesFromList(int count, List<String> list)
        {
            List<String> result = new List<string>();

            for (int i=0; i<count; i++)
            {
                result.Add(list[randomGenerator.Next(0, list.Count - 1)]);
            }

            return result;
        }
    }
}
