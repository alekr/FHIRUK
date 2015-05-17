using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FHIRUK.Resources;

namespace fhirtestdatagen
{
    public class IdentifierGenerator
    {
        public static Identifiers GetRandomIdentifiers()
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());

            Identifiers ids = new Identifiers();
            Identifier id;

            // EnumIdentifierUse = Undefined, Usual, Official, Temp, Secondary
            // generate at least one ID 99% of the time, and no ID 1% of the time
            if (randomGenerator.Next(0, 99) > 0)
            {
                // generate Official 99% of the time
                if (randomGenerator.Next(0, 99) == 0)
                {
                    // no ID generated
                }
                else
                {
                    // generating offical (NHS number) ID
                    Int32 uniqueID = randomGenerator.Next(0, 10000000);
                    id = new Identifier()
                    {
                        Use = EnumIdentifierUse.Official,
                        System = new Uri("http://www.nhs.uk/NHSEngland/thenhs/records/Pages/thenhsnumber.aspx"),
                        Value = "999" + uniqueID.ToString("D7")
                    };
                    ids.Add(id);

                    // generate an additional ID 20% of time
                    if (randomGenerator.Next(0, 100) < 20)
                    {
                        EnumIdentifierUse use;
                        do
                        {
                            // need 1, 3 or 4 to map to Usual, Temp or Secondary
                            use = (EnumIdentifierUse)(randomGenerator.Next(0, 4) + 1);
                        } while (use == EnumIdentifierUse.Official);

                        id = new Identifier()
                        {
                            Use = use,
                            Value = randomGenerator.Next(0,1000000).ToString("D6") //  generate a 6 digit number
                        };
                        ids.Add(id);
                    }
                }
            }

            return ids;
        }
    }
}
