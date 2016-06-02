using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FHIRUK.Resources;

namespace fhirtestdatagen
{
    public class TelecomGenerator
    {
        private static String[] emailProviders = { "gmail.com", "aol.com", "outlook.com", "hotmail.com", "yahoo.com", "mail.com", "nhs.net" };

        public static Contacts GetRandomContacts(String familyName, Boolean provideURL = false)
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());

            Contacts contacts = new Contacts();

            // generate at least one phone number 99% of time
            if (randomGenerator.Next(0, 100) > 0)
            {
                contacts.Add(GeneratePhoneNumber(randomGenerator, EnumContactUse.undefined));
                // generate second phone number 40% of time
                if (randomGenerator.Next(0, 100) < 40)
                    contacts.Add(GeneratePhoneNumber(randomGenerator, contacts[0].use));
                // provide email address 70% of time"
                if (randomGenerator.Next(0, 100) < 70)
                    contacts.Add(GenerateEmailAddress(randomGenerator, familyName));
                // do we need to provide a URL?
                if (provideURL)
                    contacts.Add(GenerateURL(randomGenerator));
            }
            else
            {
                // otherwise always generate an email address
                contacts.Add(GenerateEmailAddress(randomGenerator, familyName));
            }

            return contacts;
        }

        private static Contact GeneratePhoneNumber(Random randomGenerator, EnumContactUse avoidThisContactUse)
        {
            Contact contact = new Contact();

            contact.system = EnumContactSystem.phone;
            // EnumContactUse { Undefined, home, work, temp, old, mobile };
            do
            {
                contact.use = (EnumContactUse)(randomGenerator.Next(0, 5) + 1);
            } while (contact.use == avoidThisContactUse);
            if (contact.use == EnumContactUse.mobile)
                contact.value = "(07" + randomGenerator.Next(0, 1000).ToString("D3") + ") " +
                    randomGenerator.Next(0, 1000).ToString("D3") + "-" + randomGenerator.Next(0, 1000).ToString("D3");
            else
                contact.value = "(0800) " + randomGenerator.Next(0, 1000).ToString("D3") + "-" + randomGenerator.Next(0, 10000).ToString("D4");

            return contact;
        }

        private static Contact GenerateEmailAddress(Random randomGenerator, String familyName)
        {
            Contact contact = new Contact();

            contact.system = EnumContactSystem.email;
            contact.use = (EnumContactUse)(randomGenerator.Next(0, 2) + 1); // home or work
            contact.value = familyName + randomGenerator.Next(0,1000).ToString("D3") + "@" + 
                emailProviders[randomGenerator.Next(0, emailProviders.Length)];

            return contact;
        }

        private static Contact GenerateURL(Random randomGenerator)
        {
            Contact contact = new Contact();

            contact.system = EnumContactSystem.url;
            contact.use = EnumContactUse.work;
            contact.value = "http://www." + emailProviders[randomGenerator.Next(0, emailProviders.Length)] + "/contact";

            return contact;
        }
    }
}
