using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public class Contact
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumContactSystem System { get; set; }   //  <!-- ?? 0..1 phone | fax | email | url -->
        public String Value { get; set; }   //  <!-- 0..1 The actual contact details -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumContactUse Use { get; set; } //  <!-- 0..1 home | work | temp | old | mobile - purpose of this address -->
        public Period Period { get; set; }  //  <!-- 0..1 Period Time period when the contact was/is in use --></period>
    }

    public class Contacts : List<Contact>
    {
        public override String ToString()
        {
            String result = String.Empty;

            foreach (Contact contact in this)
            {
                if (result != String.Empty)
                    result += ", ";

                //result += contact.Value + "(" + contact.Use.ToString() + ")";
                result += contact.Value;
            }

            return result;
        }

    }

    public class OrganizationContact
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumOrganizationContactPurpose Purpose { get; set; } //  <!-- 0..1 CodeableConcept The type of contact -->
        public HumanName Name { get; set; } //  <!-- 0..1 HumanName A name associated with the contact -->
        public List<Contact> Telecom { get; set; }    //  <!-- 0..* Contact Contact details (telephone, email, etc)  for a contact -->
        public Address Address { get; set; }    //  <!-- 0..1 Address Visiting or postal addresses for the contact -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumGender Gender { get; set; }  //  <!-- 0..1 CodeableConcept Gender for administrative purposes -->

    }

    public class PatientContact
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public List<EnumPatientRelationship> Relationship { get; set; }   //  <!-- 0..* CodeableConcept The kind of relationship -->
        public HumanName Name { get; set; } //  <!-- 0..1 HumanName A name associated with the person -->
        public List<Contact> Telecom { get; set; }    //  <!-- 0..* Contact A contact detail for the person -->
        public Address Address { get; set; }    //  <!-- 0..1 Address Address for the contact person -->
        public EnumGender Gender { get; set; }  //  <!-- 0..1 CodeableConcept Gender for administrative purposes -->
        public Organization Organization { get; set; }  //  <!-- ?? 0..1 Resource(Organization) Organization that is associated with the contact -->
    }
}
