using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHIRUK.Resources
{
    public class Animal
    {
        public String Species { get; set; }  //  <!-- 1..1 CodeableConcept E.g. Dog, Cow § -->
        public String Breed { get; set; }   //  <!-- 0..1 CodeableConcept E.g. Poodle, Angus § -->
        public String GenderStatus { get; set; }    //  <!-- 0..1 CodeableConcept E.g. Neutered, Intact § -->
    }
}
