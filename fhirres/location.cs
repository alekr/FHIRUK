using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public class Location
    {
        public Identifier identifier { get; set; }  //  <!-- 0..1 Identifier Unique code or number identifying the location to its users -->
        public String name { get; set; }    //  <!-- 0..1 Name of the location as used by humans -->
        public String description { get; set; } //  <!-- 0..1 Description of the Location, which helps in finding or referencing the place -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocationRole type { get; set; }  //  <!-- 0..1 CodeableConcept Indicates the type of function performed at the location -->
        public List<Contact> telecom { get; set; }    //  <!-- 0..* Contact Contact details of the location --></telecom>
        public Address address { get; set; }    //  <!-- 0..1 Address Physical location -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocationType physicalType { get; set; }  //  <!-- 0..1 CodeableConcept Physical form of the location -->
        public GeoLocation position { get; set; }  //  <!-- 0..1 The absolute geographic location -->
        public Organization managingOrganization { get; set; }  //  <!-- 0..1 Resource(Organization) The organization that is responsible for the provisioning and upkeep of the location -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocationStatus status { get; set; }  //  <!-- 0..1 active | suspended | inactive -->
        public Location partOf { get; set; }    //  <!-- 0..1 Resource(Location) Another Location which this Location is physically part of -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocationMode mode { get; set; }  //  <!-- 0..1 instance | kind -->

    }

    public class GeoLocation
    {
        public Decimal longitude { get; set; }  //  <!-- 1..1 Longitude as expressed in KML -->
        public Decimal latitude { get; set; }   //  <!-- 1..1 Latitude as expressed in KML -->
        public Decimal altitude { get; set; }   //  <!-- 0..1 Altitude as expressed in KML -->
    }
}
