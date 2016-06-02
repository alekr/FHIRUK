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
        public EnumContactSystem system { get; set; }   //  <!-- ?? 0..1 phone | fax | email | url -->
        public String value { get; set; }   //  <!-- 0..1 The actual contact details -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumContactUse use { get; set; } //  <!-- 0..1 home | work | temp | old | mobile - purpose of this address -->
        public Period period { get; set; }  //  <!-- 0..1 Period Time period when the contact was/is in use --></period>
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
                result += contact.value;
            }

            return result;
        }

    }

    public class OrganizationContact
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumOrganizationContactPurpose purpose { get; set; } //  <!-- 0..1 CodeableConcept The type of contact -->
        public HumanName name { get; set; } //  <!-- 0..1 HumanName A name associated with the contact -->
        public List<Contact> telecom { get; set; }    //  <!-- 0..* Contact Contact details (telephone, email, etc)  for a contact -->
        public Address address { get; set; }    //  <!-- 0..1 Address Visiting or postal addresses for the contact -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumGender gender { get; set; }  //  <!-- 0..1 CodeableConcept Gender for administrative purposes -->

    }

    public class PatientContact
    {
        //[JsonConverter(typeof(StringEnumConverter))]
        public List<EnumPatientRelationship> relationship { get; set; }   //  <!-- 0..* CodeableConcept The kind of relationship -->
        public HumanName name { get; set; } //  <!-- 0..1 HumanName A name associated with the person -->
        public Contacts telecom { get; set; }    //  <!-- 0..* Contact A contact detail for the person -->
        public Address address { get; set; }    //  <!-- 0..1 Address Address for the contact person -->
        public EnumGender gender { get; set; }  //  <!-- 0..1 CodeableConcept Gender for administrative purposes -->
#if false
        public Organization organization { get; set; }  //  <!-- ?? 0..1 Resource(Organization) Organization that is associated with the contact -->
#endif
        public Reference organization { get; set; }  //  <!-- ?? 0..1 Resource(Organization) Organization that is associated with the contact -->

    }

    public class PatientContacts : List<PatientContact>
    {
        public override String ToString()
        {
            String result = String.Empty;

            foreach (PatientContact contact in this)
            {
                if (result != String.Empty)
                    result += ", ";

                if (IsCareGiver(contact))
                    result += contact.organization.ToString();
                else
                    result += contact.name.ToString();
            }

            return result;
        }

        private Boolean IsCareGiver(PatientContact patientContact)
        {
            List<EnumPatientRelationship> relationships = patientContact.relationship;

            foreach (EnumPatientRelationship relationship in relationships)
            {
                if (relationship == EnumPatientRelationship.caregiver)
                    return true;
            }

            return false;
        }

    }
}
