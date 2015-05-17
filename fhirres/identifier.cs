using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public class Identifier: FhirCore
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumIdentifierUse Use { get; set; }	//	<!-- 0..1 usual | official | temp | secondary (If known) -->                                  
        public String Label { get; set; }         	//	<!-- 0..1 Description of identifier -->                                                       
        public Uri System { get; set; }           	//	<!-- 0..1 The namespace for the identifier -->                                                
        public String Value { get; set; }         	//	<!-- 0..1 The value that is unique -->                                                        
        public Period Period { get; set; }        	//	<!-- 0..1 Period Time period when id is/was valid for use --></period>                        
        public Organization Assigner { get; set; }	//	<!-- 0..1 Resource(Organization) Organization that issued id (may be just text) -->
    }

    public class Identifiers : List<Identifier>
    {

        public override String ToString()
        {
            String result = String.Empty;

            foreach (Identifier id in this)
            {
                if (result != String.Empty)
                    result += ", ";

                //result += id.Value + "(" + id.Use.ToString() + ")";
                result += id.Value;
            }

            return result;
        }
    }
}
