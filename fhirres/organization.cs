using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public class Organization
    {
        public List<Identifier> Identifier { get; set; }  //  <!-- ?? 0..* Identifier Identifies this organization  across multiple systems -->
        public String Name { get; set; }    //  <!-- ?? 0..1 Name used for the organization -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumOrganizationType Type { get; set; }  //  <!-- 0..1 CodeableConcept Kind of organization -->
        public List<Contact> Telecom { get; set; }    //  <!-- ?? 0..* Contact A contact detail for the organization --></telecom>
        public List<Address> Address { get; set; }    //  <!-- ?? 0..* Address An address for the organization --></address>
        public Organization PartOf { get; set; }    //  <!-- 0..1 Resource(Organization) The organization of which this organization forms a part -->
        public List<OrganizationContact> Contact { get; set; }    //  <!-- 0..* Contact for the organization for a certain purpose -->
        public List<Location> Location { get; set; }  //  <!-- 0..* Resource(Location) Location(s) the organization uses to provide services -->
        public Boolean Active { get; set; } //  <!-- 0..1 Whether the organization's record is still in active use -->

    }
}
