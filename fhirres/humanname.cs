using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public class HumanName
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumHumanNameUse use { get; set; }   //  <!-- 0..1 usual | official | temp | nickname | anonymous | old | maiden -->
        public String text { get; set; }            //  <!-- 0..1 Text representation of the full name -->
        public List<String> family { get; set; }          //  <!-- 0..* Family name (often called 'Surname') -->
        public List<String> given { get; set; }           //  <!-- 0..* Given names (not always 'first'). Includes middle names -->
        public List<String> prefix { get; set; }          //  <!-- 0..* Parts that come before the name -->
        public List<String> suffix { get; set; }          //  <!-- 0..* Parts that come after the name -->
        public Period period { get; set; }          //  <!-- 0..1 Period Time period when name was/is in use --></period>

        public override String ToString()
        {
            String result = String.Empty;

            String givenNames = String.Empty;

            foreach (String giveName in given)
            {
                givenNames += giveName + " ";
            }

            String familyNames = String.Empty;

            foreach (String familyName in family)
            {
                if (familyNames != String.Empty)
                    result += " ";
                familyNames += familyName;
            }

            result += givenNames + familyNames;

            return result;
        }
    }

    public class HumanNames : List<HumanName>
    {
        public override String ToString()
        {
            String result = String.Empty;

            foreach (HumanName name in this)
            {
                if (result != String.Empty)
                    result += ",";

                result += name.ToString();
            }

            return result;
        }
    }
}
