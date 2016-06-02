using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public class Patient : FhirCore
    {
        public String resourceType { get { return "Patient"; } }
        public String id { get; set; }
        public Identifiers identifier { get; set; }  //  <!-- 1..* Identifier An identifier for the person as this patient (1 NHS number and 0 or more other/local identifiers § -->
        public HumanNames name { get; set; } //  <!-- 1..* HumanName A name associated with the patient § -->
        public Contacts telecom { get; set; }    //  <!-- 0..* Contact A contact detail for the individual § -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumGender gender { get; set; }  //  <!-- 1..1 CodeableConcept Gender for administrative purposes § -->
        public DateTime birthDate { get; set; } //  <!-- 1..1 The date and time of birth for the individual § -->
        public DateTime deceasedDateTime { get; set; }  //  <!-- 0..1 boolean|dateTime Indicates if the individual is deceasedDateTime or not § --></deceasedDateTime[x]>
        public Addresses address { get; set; }    //  <!-- 0..* Address Addresses for the individual § -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumMaritalStatus maritalStatus { get; set; } // <!-- 0..1 CodeableConcept Marital (civil) status of a person § -->
        public Int32 multipleBirth { get; set; }    //  <!-- 0..1 boolean|integer Whether patient is part of a multiple birth § -->
#if false
        public Attachments photo { get; set; }   //  <!-- 0..* Attachment Image of the person -->
#endif
        public PatientContacts contact { get; set; } //  <!-- 0..* A contact party (e.g. guardian, partner, friend) for the patient -->
#if false
        public Animal animal { get; set; }  //  <!-- 0..1 If this patient is an animal (non-human) § -->
#endif
        public PatientCommunications communication { get; set; }   //  <!-- 0..* CodeableConcept Languages which may be used to communicate with the patient about his or her health -->
        public References careProvider { get; set; }  //  <!-- 0..* Resource(Organization|Practitioner) Patient's nominated care provider -->
#if false
        public Organization managingOrganization { get; set; }  //  <!-- 0..1 Resource(Organization) Organization that is the custodian of the patient record § -->
        public List<PatientLink> link { get; set; }   //  <!-- 0..* Link to another patient resource that concerns the same actual person § -->
        public Boolean active { get; set; } //  <!-- 0..1 Whether this patient's record is in active use § -->
#endif
        public CodeableConcept religion { get; set; }
        public CodeableConcept ethnicity { get; set; }
        public Boolean organDonor { get; set; }
        public CodeableConcept residentialStatus { get; set; }
        public CodeableConcept treatmentCategory { get; set; }
    }

    public class PatientCommunication
    {
        public CodeableConcept language { get; set; }
        public Boolean preferred { get; set; }
    }

    public class PatientCommunications : List<PatientCommunication>
    {
        public override String ToString()
        {
            String result = String.Empty;

            foreach (PatientCommunication comms in this)
            {
                if (result != String.Empty)
                    result += ",";
                result += comms.language.text;
            }

            return result;
        }
    }

    public class PatientLink
    {
        public Patient other { get; set; }  //  <!-- 1..1 Resource(Patient) The other patient resource that the link refers to § -->
        public EnumPatientLinkType type { get; set; }   // <!-- 1..1 replace | refer | seealso - type of link § -->
    }
}
