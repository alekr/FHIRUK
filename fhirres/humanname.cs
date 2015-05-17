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
        public EnumHumanNameUse Use { get; set; }   //  <!-- 0..1 usual | official | temp | nickname | anonymous | old | maiden -->
        public String Text { get; set; }            //  <!-- 0..1 Text representation of the full name -->
        public List<String> Family { get; set; }          //  <!-- 0..* Family name (often called 'Surname') -->
        public List<String> Given { get; set; }           //  <!-- 0..* Given names (not always 'first'). Includes middle names -->
        public List<String> Prefix { get; set; }          //  <!-- 0..* Parts that come before the name -->
        public List<String> Suffix { get; set; }          //  <!-- 0..* Parts that come after the name -->
        public Period Period { get; set; }          //  <!-- 0..1 Period Time period when name was/is in use --></period>
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

                String givenNames = String.Empty;

                foreach (String giveName in name.Given)
                {
                    givenNames += giveName + " ";
                }

                String familyNames = String.Empty;

                foreach (String familyName in name.Family)
                {
                    if (familyNames != String.Empty)
                        result += " ";
                    familyNames += familyName;
                }

                result += givenNames + familyNames;
            }

            return result;
        }
    }
}
