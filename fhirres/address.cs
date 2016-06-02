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
        public EnumAddressUse use { get; set; } //  <!-- 0..1 home | work | temp | old - purpose of this address -->
        public String text { get { return GetText(); } }    //  <!-- 0..1 Text representation of the address -->
        public List<String> line {get; set; } //  <!-- 0..* Street name, number, direction & P.O. Box etc -->
        public String city { get; set; }    //  <!-- 0..1 Name of city, town etc. -->
        public String state { get; set; }   //  <!-- 0..1 Sub-unit of country (abreviations ok) -->
        public String zip { get; set; } //  <!-- 0..1 Postal code for area -->
        public String country { get; set; } //  <!-- 0..1 Country (can be ISO 3166 3 letter code) -->
        public Period period { get; set; }  //  <!-- 0..1 Period Time period when address was/is in use -->

        public Address()
        {
            line = new List<string>();
        }

        private String GetText()
        {
            String result = String.Empty;
            if (line != null)
                foreach (String line in line)
                    result += line + ", ";
            if (String.IsNullOrEmpty(city) == false)
                result += city + ", ";
            if (String.IsNullOrEmpty(state) == false)
                result += state + ", ";
            if (String.IsNullOrEmpty(zip) == false)
                result += zip + ", ";
            if (String.IsNullOrEmpty(country) == false)
                result += country + ", ";

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
                result = a.text;

                if (this.Count > 1)
                    result += " ... + " + (this.Count - 1).ToString() + " more...";
            }

            return result;
        }
    }
}
