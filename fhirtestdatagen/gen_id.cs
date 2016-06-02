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
        public static Identifiers GetRandomIdentifiers(Boolean orgId = false, String url = "http://www.trust.nhs.uk/")
        {
            Identifiers ids = new Identifiers();

            if (orgId)
            {
                GetRandomIdentifiersForOrganizations(ids, url);
            }
            else
            {
                GetRandomIdentifiersForPatient(ids);
            }

            return ids;
        }

        public static void GetRandomIdentifiersForOrganizations(Identifiers ids, String url)
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            Identifier id;
            String alphabet = "ZXCVBNMASDFGHJKLQWERTYUIOP";

            // EnumIdentifierUse = Undefined, Usual, Official, Temp, Secondary
            // always generate an official ID
            String odsCode = alphabet[randomGenerator.Next(0, 26)].ToString() +
                             alphabet[randomGenerator.Next(0, 26)].ToString() +
                             alphabet[randomGenerator.Next(0, 26)].ToString() +
                             "-" +
                            randomGenerator.Next(10, 100).ToString("D2");
            id = new Identifier()
            {
                use = EnumIdentifierUse.Official,
                system = new Uri(url),
                value = odsCode
            };
            ids.Add(id);

            // to do - should we occasionally generate a second ID

        }

        public static void GetRandomIdentifiersForPatient(Identifiers ids)
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            Identifier id;

            // EnumIdentifierUse = Undefined, Usual, Official, Temp, Secondary
            // ALWAYS generate the offical ID (NHS number)
            Int32 uniqueID = randomGenerator.Next(0, 10000000);
            id = new Identifier()
            {
                use = EnumIdentifierUse.Official,
                system = new Uri("urn:fhir.nhs.uk:id/NHSNumber"),
                value = "999" + uniqueID.ToString("D7")
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
                    use = use,
                    system = new Uri("urn:fhir.nhs.uk:id/LocalIdentifier"),
                    value = randomGenerator.Next(0, 1000000).ToString("D6") //  generate a 6 digit number
                };
                ids.Add(id);
            }
        }

    }
}
