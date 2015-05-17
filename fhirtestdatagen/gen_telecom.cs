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
                contacts.Add(GeneratePhoneNumber(randomGenerator, EnumContactUse.Undefined));
                // generate second phone number 40% of time
                if (randomGenerator.Next(0, 100) < 40)
                    contacts.Add(GeneratePhoneNumber(randomGenerator, contacts[0].Use));
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

            contact.System = EnumContactSystem.phone;
            // EnumContactUse { Undefined, home, work, temp, old, mobile };
            do
            {
                contact.Use = (EnumContactUse)(randomGenerator.Next(0, 5) + 1);
            } while (contact.Use == avoidThisContactUse);
            if (contact.Use == EnumContactUse.mobile)
                contact.Value = "(07" + randomGenerator.Next(0, 1000).ToString("D3") + ") " +
                    randomGenerator.Next(0, 1000).ToString("D3") + "-" + randomGenerator.Next(0, 1000).ToString("D3");
            else
                contact.Value = "(0800) " + randomGenerator.Next(0, 1000).ToString("D3") + "-" + randomGenerator.Next(0, 10000).ToString("D4");

            return contact;
        }

        private static Contact GenerateEmailAddress(Random randomGenerator, String familyName)
        {
            Contact contact = new Contact();

            contact.System = EnumContactSystem.email;
            contact.Use = (EnumContactUse)(randomGenerator.Next(0, 2) + 1); // home or work
            contact.Value = familyName + randomGenerator.Next(0,1000).ToString("D3") + "@" + 
                emailProviders[randomGenerator.Next(0, emailProviders.Length)];

            return contact;
        }

        private static Contact GenerateURL(Random randomGenerator)
        {
            Contact contact = new Contact();

            contact.System = EnumContactSystem.url;
            contact.Use = EnumContactUse.work;
            contact.Value = "http://www." + emailProviders[randomGenerator.Next(0, emailProviders.Length)] + "/contact";

            return contact;
        }
    }
}
