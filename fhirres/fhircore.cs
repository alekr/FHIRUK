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

    public class FhirCore : IJson
    {
        public String JSON()
        {
            return JsonConvert.SerializeObject(this);

        }
    }
}
