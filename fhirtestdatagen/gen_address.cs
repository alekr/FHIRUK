using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FHIRUK.Resources;

using System.Diagnostics;

namespace fhirtestdatagen
{
    public class AddressGenerator
    {
        internal class District
        {
            public String Postcode { get; set; }
            public String Town { get; set; }   
            public String Region { get; set; }

            internal District(String postcode, String town, String region)
            {
                this.Postcode = postcode;
                this.Town = town;
                this.Region = region;
            }
        }

        private List<District> districts = new List<District>();
        private List<String> streetNames = new List<String>();
        private List<String> streetTypes = new List<String>();

        private Random randomGenerator;

        private static String districtsFile = @"..\..\lists\postcodes.eng.txt";
        private static String streetNamesFile = @"..\..\lists\streets.names.txt";
        private static String streetTypesFile = @"..\..\lists\streets.types.txt";

        public AddressGenerator()
        {
            randomGenerator = new Random(Guid.NewGuid().GetHashCode());

            LoadDistrictsFromFile();
            LoadStreetNamesFromFile();
            LoadStreetTypesFromFile();
        }

        private void LoadDistrictsFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(districtsFile))
                {
                    while (reader.EndOfStream == false)
                    {
                        String line = reader.ReadLine();
                        line = line.Trim();
                        if (String.IsNullOrEmpty(line) == false)
                        {
                            String[] items = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length == 3)
                                districts.Add(new District(items[0], items[1], items[2]));
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception e)
            {
                // do something
                Debug.Write(e.Message);
            }
        }

        private void LoadStreetNamesFromFile()
        {
            using (StreamReader reader = new StreamReader(streetNamesFile))
            {
                while (reader.EndOfStream == false)
                {
                    String line = reader.ReadLine();
                    line = line.Trim();
                    if (String.IsNullOrEmpty(line) == false)
                        streetNames.Add(line);
                }

                reader.Close();
            }
        }

        private void LoadStreetTypesFromFile()
        {
            using (StreamReader reader = new StreamReader(streetTypesFile))
            {
                while (reader.EndOfStream == false)
                {
                    String line = reader.ReadLine();
                    line = line.Trim();
                    if (String.IsNullOrEmpty(line) == false)
                        streetTypes.Add(line);
                }

                reader.Close();
            }
        }

        public Addresses GetRandomAddresses(EnumAddressUse principalAddressUse)
        {
            Addresses addresses = new Addresses();
            addresses.Add(GetRandomAddress(principalAddressUse));

            return addresses;
        }

        private District GetRandomDistrict()
        {
            String letters = "ZXCVBNMASDFGHJKLQWERTYUIOP";

            District fullDistrict = districts[randomGenerator.Next(0, districts.Count)];
            String zip = fullDistrict.Postcode + " " + 
                         randomGenerator.Next(1, 10).ToString() +
                         letters[randomGenerator.Next(0, letters.Length)] +
                         letters[randomGenerator.Next(0, letters.Length)];
            String[] items = fullDistrict.Town.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            String town = String.Empty;
            if (items.Length > 0)
                town = items[randomGenerator.Next(0, items.Length)].Trim();
            String region = fullDistrict.Region;

            return new District(zip, town, region);
        }

        public Address GetRandomAddress(EnumAddressUse use)
        {
#if false
        public EnumAddressUse Use { get; set; } //  <!-- 0..1 home | work | temp | old - purpose of this address -->
        public String Text { get { return GetText(); } }    //  <!-- 0..1 Text representation of the address -->
        public List<String> Line {get; set; } //  <!-- 0..* Street name, number, direction & P.O. Box etc -->
        public String City { get; set; }    //  <!-- 0..1 Name of city, town etc. -->
        public String State { get; set; }   //  <!-- 0..1 Sub-unit of country (abreviations ok) -->
        public String Zip { get; set; } //  <!-- 0..1 Postal code for area -->
        public String Country { get; set; } //  <!-- 0..1 Country (can be ISO 3166 3 letter code) -->
        public Period Period { get; set; }  //  <!-- 0..1 Period Time period when address was/is in use -->

#endif
            Address address = new Address();

            address.use = use;
            String addressLine = randomGenerator.Next(1, 100).ToString() + " " +
                           streetNames[randomGenerator.Next(0, streetNames.Count)] + " " +
                           streetTypes[randomGenerator.Next(0, streetTypes.Count)];
            address.line.Add(addressLine);
            District district = GetRandomDistrict();
            address.city = district.Town;
            address.state = district.Region;
            address.zip = district.Postcode;

            return address;
        }
    }
}
