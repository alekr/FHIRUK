using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHIRUK.Resources
{
    public class Animal
    {
        public String species { get; set; }  //  <!-- 1..1 CodeableConcept E.g. Dog, Cow § -->
        public String breed { get; set; }   //  <!-- 0..1 CodeableConcept E.g. Poodle, Angus § -->
        public String genderStatus { get; set; }    //  <!-- 0..1 CodeableConcept E.g. Neutered, Intact § -->
    }
}
