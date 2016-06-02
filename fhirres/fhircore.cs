using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FHIRUK.Resources
{
    public interface IJson
    {
        String JSON();
    }

    public abstract class FhirCore : IJson
    {
        public String JSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Reference
    {
        public Uri reference { get; set; }
        public String display { get; set; }
    }

    public class References : List<Reference>
    {
        public override String ToString()
        {
            String result = String.Empty;
            foreach (Reference item in this)
            {
                if (result != String.Empty)
                    result += ",";
                result += item.reference;
            }

            return result;
        }
    }

    public class CodeableConcept
    {
        public Coding coding { get; set; }
        public String text { get; set; }
    }

    public class Coding
    {
        public Uri system { get; set; } //  <!-- 0..1	uri Identity of the terminology system -->
        public String version { get; set; } //  <!-- 0..1	string Version of the system - if relevant -->
        public String code { get; set; } //  <!-- 0..1	code Symbol in syntax defined by the system -->
        public String display { get; set; } //  <!-- 0..1	string Representation defined by the system -->
        public Boolean primary { get; set; } //  <!-- 0..1	boolean If this code was chosen directly by the user -->
    }
}
