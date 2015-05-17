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
        public Identifiers Identifier { get; set; }  //  <!-- 0..* Identifier An identifier for the person as this patient § -->
        public HumanNames Name { get; set; } //  <!-- 0..* HumanName A name associated with the patient § -->
        public Contacts Telecom { get; set; }    //  <!-- 0..* Contact A contact detail for the individual § -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumGender Gender { get; set; }  //  <!-- 0..1 CodeableConcept Gender for administrative purposes § -->
        public DateTime BirthDate { get; set; } //  <!-- 0..1 The date and time of birth for the individual § -->
        public DateTime Deceased { get; set; }  //  <!-- 0..1 boolean|dateTime Indicates if the individual is deceased or not § --></deceased[x]>
        public Addresses Address { get; set; }    //  <!-- 0..* Address Addresses for the individual § -->
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumMaritalStatus MaritalStatus { get; set; } // <!-- 0..1 CodeableConcept Marital (civil) status of a person § -->
        public Int32 MultipleBirth { get; set; }    //  <!-- 0..1 boolean|integer Whether patient is part of a multiple birth § -->
        public List<Attachment> Photo { get; set; }   //  <!-- 0..* Attachment Image of the person -->
        public List<PatientContact> Contact { get; set; } //  <!-- 0..* A contact party (e.g. guardian, partner, friend) for the patient -->
        public Animal Animal { get; set; }  //  <!-- 0..1 If this patient is an animal (non-human) § -->
        public List<String> Communication { get; set; }   //  <!-- 0..* CodeableConcept Languages which may be used to communicate with the patient about his or her health -->
        public List<Organization> CareProvider { get; set; }  //  <!-- 0..* Resource(Organization|Practitioner) Patient's nominated care provider -->
        public Organization ManagingOrganization { get; set; }  //  <!-- 0..1 Resource(Organization) Organization that is the custodian of the patient record § -->
        public List<PatientLink> Link { get; set; }   //  <!-- 0..* Link to another patient resource that concerns the same actual person § -->
        public Boolean Active { get; set; } //  <!-- 0..1 Whether this patient's record is in active use § -->

#if false
        public bool IsPropertyAList(PropertyInfo propertyInfo)
        {
            Type ifIList = typeof(IList<>);                             //	"System.Collections.Generic.IList`1"
            Type tPropertyType = propertyInfo.PropertyType;             //	"System.Collections.Generic.List`1[[FHIRUK.Identifier]]"
            Type tGeneric = tPropertyType.GetGenericTypeDefinition();   //	"System.Collections.Generic.List`1"

            Type[] tInterfaces = tPropertyType.GetInterfaces();
            //  [0] "System.Collections.Generic.IList`1[[FHIRUK.Identifier]]"
            //  [1] "System.Collections.Generic.ICollection`1[[FHIRUK.Identifier]]"
            //  [2] "System.Collections.Generic.IEnumerable`1[[FHIRUK.Identifier]]"
            //  [3] "System.Collections.IEnumerable"
            //  [4] "System.Collections.IList"
            //  [5] "System.Collections.ICollection"
            //  [6] "System.Collections.Generic.IReadOnlyList`1[[FHIRUK.Identifier]]"
            //  [7] "System.Collections.Generic.IReadOnlyCollection`1[[FHIRUK.Identifier]]"

            if (
                tPropertyType.IsGenericType && 
                ifIList.IsAssignableFrom(tGeneric) ||
                tInterfaces.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == ifIList)
                )
            {
                return true;
            }

            return false;
        }
#endif

#if false
        public String JSON()
        {

            String json = String.Empty;

            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                object z = propertyInfo.GetValue(propertyInfo);
                if (IsPropertyAList(propertyInfo))
                {
                    IList x = propertyInfo as IList;
                    foreach (object o in x)
                    {
                        object a = o;
                    }
                }
            }

            return json;
        }
#endif
    }

    public class PatientLink
    {
        public Patient Other { get; set; }  //  <!-- 1..1 Resource(Patient) The other patient resource that the link refers to § -->
        public EnumPatientLinkType Type { get; set; }   // <!-- 1..1 replace | refer | seealso - type of link § -->
    }
}
