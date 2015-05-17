using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public class Address
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumAddressUse Use { get; set; } //  <!-- 0..1 home | work | temp | old - purpose of this address -->
        public String Text { get { return GetText(); } }    //  <!-- 0..1 Text representation of the address -->
        public List<String> Line {get; set; } //  <!-- 0..* Street name, number, direction & P.O. Box etc -->
        public String City { get; set; }    //  <!-- 0..1 Name of city, town etc. -->
        public String State { get; set; }   //  <!-- 0..1 Sub-unit of country (abreviations ok) -->
        public String Zip { get; set; } //  <!-- 0..1 Postal code for area -->
        public String Country { get; set; } //  <!-- 0..1 Country (can be ISO 3166 3 letter code) -->
        public Period Period { get; set; }  //  <!-- 0..1 Period Time period when address was/is in use -->

        public Address()
        {
            Line = new List<string>();
        }

        private String GetText()
        {
            String result = String.Empty;
            if (Line != null)
                foreach (String line in Line)
                    result += line + ", ";
            if (String.IsNullOrEmpty(City) == false)
                result += City + ", ";
            if (String.IsNullOrEmpty(State) == false)
                result += State + ", ";
            if (String.IsNullOrEmpty(Zip) == false)
                result += Zip + ", ";
            if (String.IsNullOrEmpty(Country) == false)
                result += Country + ", ";

            if (result != String.Empty)
                result = result.Substring(0, result.Length - 2);    // remove trailing ", "

            return result;
        }
    }

    public class Addresses : List<Address>
    {
        public override string ToString()
        {
            String result = String.Empty;

            if (this.Count > 0)
            {
                Address a = this[0];
                result = a.Text;

                if (this.Count > 1)
                    result += " ... + " + (this.Count - 1).ToString() + " more...";
            }

            return result;
        }
    }
}
