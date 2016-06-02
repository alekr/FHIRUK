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
using System.IO;

namespace fhirtestdatagen
{
    public partial class FormFHIRTestDatGen : Form
    {
        private static Int32 PATIENT_COUNT = 10;

        private static String FILE_GPS = @"..\..\lists\sds.gppractices.txt";
        private static String FILE_PCTS = @"..\..\lists\sds.pcts.txt";
        private static String FILE_TRUSTS = @"..\..\lists\sds.nhstrust.txt";
        private static String FILE_INDEPENDENTS = @"..\..\lists\sds.independent.txt";

        private static String FILE_EXPORT_ORGS = @"export.orgs.json.txt";
        private static String FILE_EXPORT_PATIENTS = @"export.patients.json.txt";

        private List<SDSConfigNodeLines> gpItems = new List<SDSConfigNodeLines>();
        private List<SDSConfigNode> gpKeyValueObjects = new List<SDSConfigNode>();

        private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();

        private PatientGenerator genPatient = null;
        OrganizationGenerator genOrgs = null;

        public FormFHIRTestDatGen()
        {
            InitializeComponent();
        }

        private void butGenerate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PATIENT_COUNT; i++)
            {
                Patient patient = genPatient.GeneratePatient(genOrgs);
                AddPatientToList(patient);
            }
        }

        private void AddPatientToList(Patient patient)
        {
            if ((patient == null) || (patient.identifier == null) || (patient.identifier.Count < 1))
                return;

            ListViewItem item = listViewPatients.Items.Add(patient.identifier.ToString());
            item.SubItems.Add(patient.name.ToString());
            item.SubItems.Add(patient.telecom.ToString());
            item.SubItems.Add(patient.gender.ToString());
            item.SubItems.Add(patient.birthDate.ToShortDateString());
            item.SubItems.Add(String.Empty);    // deceasedDateTime
            item.SubItems.Add(patient.address.ToString());
            //item.SubItems.Add(EnumConversion.MaritalStatusToString(patient.MaritalStatus));
            item.SubItems.Add(patient.maritalStatus.ToString());
            item.SubItems.Add(patient.multipleBirth.ToString());
            item.SubItems.Add(patient.contact.ToString());
            item.SubItems.Add(patient.communication.ToString());
            item.SubItems.Add(patient.careProvider.ToString());

            item.Tag = patient;
        }

        private void FormFHIRTestDatGen_Load(object sender, EventArgs e)
        {
            genPatient = new PatientGenerator();
            genOrgs = new OrganizationGenerator();
        }

        private void listViewData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPatients.SelectedItems.Count == 0)
                textBoxJSON.Text = String.Empty;
            else
            {
                ListViewItem item = listViewPatients.SelectedItems[0];
                Patient patient = item.Tag as Patient;
                textBoxJSON.Text = patient.JSON();
            }
        }

        private void ClearListItems()
        {
            listViewOrgs.BeginUpdate();
            listViewOrgs.Items.Clear();
            listViewOrgs.EndUpdate();
        }

        private void butGPs_Click(object sender, EventArgs e)
        {
            LoadandDisplaySDSFile(FILE_GPS);
        }

        private void butPCTs_Click(object sender, EventArgs e)
        {
            LoadandDisplaySDSFile(FILE_PCTS);
        }

        private void butTrusts_Click(object sender, EventArgs e)
        {
            LoadandDisplaySDSFile(FILE_TRUSTS);
        }

        private void butIndependents_Click(object sender, EventArgs e)
        {
            LoadandDisplaySDSFile(FILE_INDEPENDENTS);
        }

        private void LoadandDisplaySDSFile(String sdsFile, Boolean clearList = true)
        {
            toolStripStatusLabelInfo.Text = "Clearing list items...";
            if (clearList == true)
            {
                ClearListItems();
            }

            toolStripStatusLabelInfo.Text = "Reading SDS file...";
            ReadSDSFile(sdsFile);

            toolStripStatusLabelInfo.Text = "Creating configuration objects...";
            ParseConfigurationItems();

            toolStripStatusLabelInfo.Text = "Displaying organisations...";
            DisplayItems();

            toolStripStatusLabelInfo.Text = listViewOrgs.Items.Count.ToString() + " items.";
        }

        private void ReadSDSFile(String sdsFile)
        {
            gpItems.Clear();

            SDSConfigNodeLines gpItem = new SDSConfigNodeLines();

            using (StreamReader reader = new StreamReader(sdsFile))
            {
                while (reader.EndOfStream == false)
                {
                    String line = reader.ReadLine();
                    if (String.IsNullOrEmpty(line) == false)
                    {
                        gpItem.Add(line);
                    }
                    else
                    {
                        if (gpItem.Count > 0)
                        {
                            String firstLine = gpItem[0];
                            if (firstLine.StartsWith("dn:") == false)
                            {
                                gpItem.Clear();
                            }
                            else
                            {
                                gpItems.Add(gpItem);
                                gpItem = new SDSConfigNodeLines();
                            }
                        }
                    }
                }

                reader.Close();
            }
        }

        private void ParseConfigurationItems()
        {
            gpKeyValueObjects.Clear();

            foreach (SDSConfigNodeLines gpItem in gpItems)
            {
                SDSConfigNode gpKeyValueObject = new SDSConfigNode(gpItem);

                gpKeyValueObjects.Add(gpKeyValueObject);
            }
        }

        private void DisplayItems()
        {
            listViewOrgs.BeginUpdate();

            foreach (SDSConfigNode pairs in gpKeyValueObjects)
            {
                String uniqueidentifier = pairs.GetValue("uniqueidentifier");
                String odsCode = pairs.GetValue("nhsdhsccode");
                String shaCode = pairs.GetValue("nhsshacode");
                String orgName = pairs.GetValue("o");
                String telephonenumber = pairs.GetValue("telephonenumber");
                String postaladdress = pairs.GetValue("postaladdress");
                String postalcode = pairs.GetValue("postalcode");
                String orgTypeCode = pairs.GetValue("nhsorgtypecode");
                String fax = pairs.GetValue("facsimiletelephonenumber");

                String street = String.Empty;
                String town = String.Empty;
                String county = String.Empty;

                if (orgName != null)
                {
                    ListViewItem item = listViewOrgs.Items.Add(uniqueidentifier);
                    item.SubItems.Add(orgTypeCode);
                    item.SubItems.Add(odsCode);
                    item.SubItems.Add(shaCode);
                    item.SubItems.Add(orgName);
                    item.SubItems.Add(telephonenumber);
                    item.SubItems.Add(fax);

                    ParsePostalAddress(item, postaladdress, out street, out town, out county);

                    item.SubItems.Add(street);
                    item.SubItems.Add(town);
                    item.SubItems.Add(postalcode);
                    item.SubItems.Add(county);

                    item.SubItems[9].Text = postalcode;

                    Organization org = new Organization
                    {
                        active = true,
                        address = new Addresses
                        {
                            new Address
                            {
                                city = town,
                                line = new List<string> { street },
                                state = county,
                                zip = postalcode,
                                use = EnumAddressUse.work
                            }
                        },
                        identifier = new Identifiers
                        {
                            new Identifier
                            {
                                use = EnumIdentifierUse.Official,
                                value = uniqueidentifier,
                                system = new Uri("http://checkit/")
                            },
                            new Identifier
                            {
                                use = EnumIdentifierUse.Secondary,
                                value = odsCode,
                                system = new Uri("http://systems.hscic.gov.uk/data/ods")
                            }
                        },
                        name = orgName,
                        type = GetOrgType(orgTypeCode)
                    };

                    if ((String.IsNullOrEmpty(telephonenumber) == false) || (String.IsNullOrEmpty(fax) == false))
                    {
                        org.telecom = new Contacts();
                        Contact contact = null;

                        if (String.IsNullOrEmpty(telephonenumber) == false)
                        {
                            contact = new Contact
                            {
                                system = EnumContactSystem.phone,
                                use = EnumContactUse.work,
                                value = telephonenumber
                            };
                            org.telecom.Add(contact);
                        }

                        if (String.IsNullOrEmpty(fax) == false)
                        {
                            contact = new Contact
                            {
                                system = EnumContactSystem.fax,
                                use = EnumContactUse.work,
                                value = fax
                            };
                            org.telecom.Add(contact);
                        }
                    }

                    item.Tag = org;
                    genOrgs.AddOrganization(org);
                }
            }

            listViewOrgs.EndUpdate();
        }

        private EnumOrganizationType GetOrgType(String orgTypeCode)
        {
            EnumOrganizationType orgType = EnumOrganizationType.prov;

            if (orgTypeCode == "PR")                    //  GP practice
                orgType = EnumOrganizationType.prov;    
            else if (orgTypeCode == "PT")               //  PCT
                orgType = EnumOrganizationType.prov;
            else if (orgTypeCode == "TR")               //  NHS Trust
                orgType = EnumOrganizationType.prov;
            else if (orgTypeCode == "PP")               //  Independent
                orgType = EnumOrganizationType.prov;

            return orgType;
        }

        private void ParsePostalAddress(ListViewItem item, String postaladdress, out String street, out String town, out String county)
        {
            street = String.Empty;
            town = String.Empty;
            county = String.Empty;

            String[] addressItems = postaladdress.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (addressItems.Length < 3)
            {
                if (addressItems.Length == 2)
                {
                    town = addressItems[0];
                    county = addressItems[1];
                }
                else
                    street = postaladdress;
            }
            else
            {
                county = addressItems[addressItems.Length - 1].Trim();
                town = addressItems[addressItems.Length - 2].Trim();
                street = String.Empty;

                for (int i = 0; i < (addressItems.Length - 2); i++)
                {
                    if (street != String.Empty)
                        street += ",";

                    street += addressItems[i].Trim();
                }
            }

        }

        private void listViewOrgs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                    lvwColumnSorter.Order = SortOrder.Descending;
                else
                    lvwColumnSorter.Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            listViewOrgs.BeginUpdate();

            this.listViewOrgs.ListViewItemSorter = lvwColumnSorter;

            this.listViewOrgs.Sort();

            this.listViewOrgs.ListViewItemSorter = null;

            listViewOrgs.EndUpdate();
        }

        private void listViewOrgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewOrgs.SelectedItems.Count == 0)
                textBoxJSON.Text = String.Empty;
            else
            {
                ListViewItem item = listViewOrgs.SelectedItems[0];
                Organization organization = item.Tag as Organization;
                textBoxJSON.Text = organization.JSON();
            }
        }

        private void butCopyJSON_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxJSON.Text);
        }

        private void butAllOrgs_Click(object sender, EventArgs e)
        {
            ClearListItems();

            LoadandDisplaySDSFile(FILE_GPS, false);
            LoadandDisplaySDSFile(FILE_PCTS, false);
            LoadandDisplaySDSFile(FILE_TRUSTS, false);
            LoadandDisplaySDSFile(FILE_INDEPENDENTS, false);
        }

        private void butExportOrgsToFile_Click(object sender, EventArgs e)
        {
            if (listViewOrgs.Items.Count == 0)
            {
                MessageBox.Show("There are no organizations to export.");
                return;
            }

            using (StreamWriter writer = new StreamWriter(FILE_EXPORT_ORGS))
            {
                foreach (ListViewItem item in listViewOrgs.Items)
                {
                    Organization org = item.Tag as Organization;
                    String json = org.JSON();

                    writer.WriteLine(json);
                }

                writer.Close();
            }

            MessageBox.Show("Exported " + listViewOrgs.Items.Count.ToString() + "organizations to the [" + FILE_EXPORT_ORGS + "] file.");
        }

        private void butExportPatientsToFile_Click(object sender, EventArgs e)
        {
            if (listViewPatients.Items.Count == 0)
            {
                MessageBox.Show("There are no patients to export.");
                return;
            }

            using (StreamWriter writer = new StreamWriter(FILE_EXPORT_PATIENTS))
            {
                foreach (ListViewItem item in listViewPatients.Items)
                {
                    Patient patient = item.Tag as Patient;
                    String json = patient.JSON();

                    writer.WriteLine(json);
                }

                writer.Close();
            }

            MessageBox.Show("Exported " + listViewPatients.Items.Count.ToString() + "patients to the [" + FILE_EXPORT_PATIENTS + "] file.");
        }
    }
}
