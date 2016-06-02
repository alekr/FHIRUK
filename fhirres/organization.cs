using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public class Organization : FhirCore
    {
        public String resourceType { get { return "Organization"; } }
        public Identifiers identifier { get; set; }  //  <!-- ?? 0..* Identifier Identifies this organization  across multiple systems -->
        public String name { get; set; }    //  <!-- ?? 0..1 Name used for the organization -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumOrganizationType type { get; set; }  //  <!-- 0..1 CodeableConcept Kind of organization -->
        public Contacts telecom { get; set; }    //  <!-- ?? 0..* Contact A contact detail for the organization --></telecom>
        public Addresses address { get; set; }    //  <!-- ?? 0..* Address An address for the organization --></address>
        public Organization partOf { get; set; }    //  <!-- 0..1 Resource(Organization) The organization of which this organization forms a part -->
        public List<OrganizationContact> contact { get; set; }    //  <!-- 0..* Contact for the organization for a certain purpose -->
        public List<Location> location { get; set; }  //  <!-- 0..* Resource(Location) Location(s) the organization uses to provide services -->
        public Boolean active { get; set; } //  <!-- 0..1 Whether the organization's record is still in active use -->

        public override string ToString()
        {
            return name;                
        }
    }

    public class Organizations : List<Organization>
    {

    }
}
