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
        public Identifier Identifier { get; set; }  //  <!-- 0..1 Identifier Unique code or number identifying the location to its users -->
        public String Name { get; set; }    //  <!-- 0..1 Name of the location as used by humans -->
        public String Description { get; set; } //  <!-- 0..1 Description of the Location, which helps in finding or referencing the place -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocationRole Type { get; set; }  //  <!-- 0..1 CodeableConcept Indicates the type of function performed at the location -->
        public List<Contact> Telecom { get; set; }    //  <!-- 0..* Contact Contact details of the location --></telecom>
        public Address Address { get; set; }    //  <!-- 0..1 Address Physical location -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocationType PhysicalType { get; set; }  //  <!-- 0..1 CodeableConcept Physical form of the location -->
        public GeoLocation Position { get; set; }  //  <!-- 0..1 The absolute geographic location -->
        public Organization ManagingOrganization { get; set; }  //  <!-- 0..1 Resource(Organization) The organization that is responsible for the provisioning and upkeep of the location -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocationStatus Status { get; set; }  //  <!-- 0..1 active | suspended | inactive -->
        public Location PartOf { get; set; }    //  <!-- 0..1 Resource(Location) Another Location which this Location is physically part of -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocationMode Mode { get; set; }  //  <!-- 0..1 instance | kind -->

    }

    public class GeoLocation
    {
        public Decimal Longitude { get; set; }  //  <!-- 1..1 Longitude as expressed in KML -->
        public Decimal Latitude { get; set; }   //  <!-- 1..1 Latitude as expressed in KML -->
        public Decimal Altitude { get; set; }   //  <!-- 0..1 Altitude as expressed in KML -->
    }
}
