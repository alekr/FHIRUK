using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FHIRUK.Resources;

namespace fhirtestdatagen
{
    public partial class FormFHIRTestDatGen : Form
    {
        private static Int32 PATIENT_COUNT = 10;

        HumaneNameGenerator genNames = null;
        AddressGenerator genAddresses = null;

        public FormFHIRTestDatGen()
        {
            InitializeComponent();
        }

        private void butGenerate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PATIENT_COUNT; i++)
            {
                Patient patient = GeneratePatient();
                AddPatientToList(patient);
            }
        }

        private Patient GeneratePatient()
        {
            Patient patient = new Patient();

            GenerateIdentifier(patient);
            GenerateName(patient);
            GenerateTelecom(patient);
            GenerateDOB(patient);
            GenerateAddress(patient);

            return patient;
        }

        private void GenerateIdentifier(Patient patient)
        {
            patient.Identifier = IdentifierGenerator.GetRandomIdentifiers();
        }

        private void GenerateName(Patient patient)
        {
            List<String> given;
            List<String> family;
            EnumGender gender;

            genNames.GetRandomName(out given, out family, out gender);

            patient.Name = new HumanNames()
            {
                new HumanName()
                {
                    Given = given,
                    Family = family
                }
            };

            patient.Gender = gender;
        }

        private void GenerateTelecom(Patient patient)
        {
            String familyName = patient.Name[0].Family[0];
            patient.Telecom = TelecomGenerator.GetRandomContacts(familyName);
        }

        private void GenerateDOB(Patient patient)
        {
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());

            Int32 year = randomGenerator.Next(1920, 2015);
            Int32 month = randomGenerator.Next(0, 12) + 1;
            Int32 day;
            switch (month)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    day = randomGenerator.Next(0, 30) + 1;
                    break;
                case 2:
                    day = randomGenerator.Next(0, 28) + 1;
                    break;
                default:
                    day = randomGenerator.Next(0, 31) + 1;
                    break;
            }

            patient.BirthDate = new DateTime(year, month, day);
        }

        private void GenerateAddress(Patient patient)
        {
            patient.Address = genAddresses.GetRandomAddresses(EnumAddressUse.home); 
        }

        private void AddPatientToList(Patient patient)
        {
            if ((patient == null) || (patient.Identifier == null) || (patient.Identifier.Count < 1))
                return;

            ListViewItem item = listViewData.Items.Add(patient.Identifier.ToString());
            item.SubItems.Add(patient.Name.ToString());
            item.SubItems.Add(patient.Telecom.ToString());
            item.SubItems.Add(patient.Gender.ToString());
            item.SubItems.Add(patient.BirthDate.ToShortDateString());
            item.SubItems.Add(String.Empty);    // deceased
            item.SubItems.Add(patient.Address.ToString());

            item.Tag = patient;
        }

        private void FormFHIRTestDatGen_Load(object sender, EventArgs e)
        {
            genNames = new HumaneNameGenerator();
            genAddresses = new AddressGenerator();
        }

        private void listViewData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewData.SelectedItems.Count == 0)
                textBox1.Text = String.Empty;
            else
            {
                ListViewItem item = listViewData.SelectedItems[0];
                Patient patient = item.Tag as Patient;
                textBox1.Text = patient.JSON();
            }
        }
    }
}
