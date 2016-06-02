using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHIRUK.Resources
{
    public enum EnumAddressUse { undefined, home, work, temp, old };
    public enum EnumContactSystem { undefined, phone, fax, email, url };
    public enum EnumContactUse { undefined, home, work, temp, old, mobile };
    public enum EnumOrganizationContactPurpose { undefined, BILL, ADMIN, HR, PAYOR, PATINF, PRESS };
    public enum EnumGender { undefined, F, M, UN };
    public enum EnumPatientRelationship { undefined, emergency, family, guardian, friend, partner, work, caregiver, agent, guarantor, owner, parent };
    public enum EnumHumanNameUse { undefined, Usual, Official, Temp, Nickname, Anonymous, Old, Maiden };
    public enum EnumIdentifierUse { undefined, Usual, Official, Temp, Secondary };
    public enum EnumMaritalStatus { undefined, A, D, I, L, M, P, S, T, W };
    public enum EnumPatientLinkType { undefined, replace, refer, seealso };
    public enum EnumOrganizationType { undefined, prov, dept, icu, team, fed, ins, edu, reli, pharm };
    public enum EnumLocationType { undefined, bu, wi, co, ro, ve, ho, ca, rd };
    public enum EnumLocationStatus { undefined, active, suspended, inactive };
    public enum EnumLocationMode { undefined, instance, kind };
    public enum EnumLocationRole
    {
        undefined, DX, CVDX, CATH, ECHO, GIDX, ENDOS, RADDX, RADO, RNEU, HOSP,
        CHR, GACH, MHSP, PSYCHF, RH, RHAT, RHII, RHMAD, RHPI, RHPIH, RHPIMS,
        RHPIVS, RHYAD, HU, BMTU, CCU, CHEST, EPIL, ER, ETU, HD, HLAB, INLAB,
        OUTLAB, HRAD, HUSCS, ICU, PEDICU, PEDNICU, INPHARM, MBL, NCCS, NS,
        OUTPHARM, PEDU, PHU, RHU, SLEEP, NCCF, SNF, OF, ALL, AMPUT, BMTC,
        BREAST, CANC, CAPC, CARD, PEDCARD, COAG, CRS, DERM, ENDO, PEDE, ENT,
        FMC, GI, PEDGI, GIM, GYN, HEM, PEDHEM, HTN, IEC, INFD, PEDID, INV,
        LYMPH, MGEN, NEPH, PEDNEPH, NEUR, OB, OMS, ONCL, PEDHO, OPH, OPTC,
        ORTHO, HAND, PAINCL, PC, PEDC, PEDRHEUM, POD, PREV, PROCTO, PROFF,
        PROS, PSI, PSY, RHEUM, SPMED, SU, PLS, URO, TR, TRAVEL, WND, RTF, PRC,
        SURF, DADDR, MOBL, AMB, PHARM, ACC, COMM, CSC, PTRES, SCHOOL, UPC, WORK
    };

    public class EnumConversion
    {
        public static String OrganizationContactPurposeToString(EnumOrganizationContactPurpose purpose)
        {
            String displayString = String.Empty;

            switch (purpose)
            {
                case EnumOrganizationContactPurpose.BILL: displayString = "Billing"; break;
                case EnumOrganizationContactPurpose.ADMIN: displayString = "Administrative"; break;
                case EnumOrganizationContactPurpose.HR: displayString = "Human Resource"; break;
                case EnumOrganizationContactPurpose.PAYOR: displayString = "Payor"; break;
                case EnumOrganizationContactPurpose.PATINF: displayString = "Patient"; break;
                case EnumOrganizationContactPurpose.PRESS: displayString = "Press"; break;
            }

            return displayString;
        }

        public static String GenderToString(EnumGender gender)
        {
            String displayString = String.Empty;

            switch (gender)
            {
                case EnumGender.F: displayString = "Female"; break;
                case EnumGender.M: displayString = "Male"; break;
                case EnumGender.UN: displayString = "Undifferentiated"; break;
            }

            return displayString;
        }

        public static String LocationRoleToString(EnumLocationRole role)
        {
            String displayString = String.Empty;

            switch (role)
            {
                case EnumLocationRole.DX: displayString = "Diagnostics or therapeutics unit"; break;
                case EnumLocationRole.CVDX: displayString = "Cardiovascular diagnostics or therapeutics unit"; break;
                case EnumLocationRole.CATH: displayString = "Cardiac catheterization lab"; break;
                case EnumLocationRole.ECHO: displayString = "Echocardiography lab"; break;
                case EnumLocationRole.GIDX: displayString = "Gastroenterology diagnostics or therapeutics lab"; break;
                case EnumLocationRole.ENDOS: displayString = "Endoscopy lab"; break;
                case EnumLocationRole.RADDX: displayString = "Radiology diagnostics or therapeutics unit"; break;
                case EnumLocationRole.RADO: displayString = "Radiation oncology unit"; break;
                case EnumLocationRole.RNEU: displayString = "Neuroradiology unit"; break;
                case EnumLocationRole.HOSP: displayString = "Hospital"; break;
                case EnumLocationRole.CHR: displayString = "Chronic Care Facility"; break;
                case EnumLocationRole.GACH: displayString = "Hospitals; General Acute Care Hospital"; break;
                case EnumLocationRole.MHSP: displayString = "Military Hospital"; break;
                case EnumLocationRole.PSYCHF: displayString = "Psychatric Care Facility"; break;
                case EnumLocationRole.RH: displayString = "Rehabilitation hospital"; break;
                case EnumLocationRole.RHAT: displayString = "addiction treatment center"; break;
                case EnumLocationRole.RHII: displayString = "intellectual impairment center"; break;
                case EnumLocationRole.RHMAD: displayString = "parents with adjustment difficulties center"; break;
                case EnumLocationRole.RHPI: displayString = "physical impairment center"; break;
                case EnumLocationRole.RHPIH: displayString = "physical impairment - hearing center"; break;
                case EnumLocationRole.RHPIMS: displayString = "physical impairment - motor skills center"; break;
                case EnumLocationRole.RHPIVS: displayString = "physical impairment - visual skills center"; break;
                case EnumLocationRole.RHYAD: displayString = "youths with adjustment difficulties center"; break;
                case EnumLocationRole.HU: displayString = "Hospital unit"; break;
                case EnumLocationRole.BMTU: displayString = "Bone marrow transplant unit"; break;
                case EnumLocationRole.CCU: displayString = "Coronary care unit"; break;
                case EnumLocationRole.CHEST: displayString = "Chest unit"; break;
                case EnumLocationRole.EPIL: displayString = "Epilepsy unit"; break;
                case EnumLocationRole.ER: displayString = "Emergency room"; break;
                case EnumLocationRole.ETU: displayString = "Emergency trauma unit"; break;
                case EnumLocationRole.HD: displayString = "Hemodialysis unit"; break;
                case EnumLocationRole.HLAB: displayString = "hospital laboratory"; break;
                case EnumLocationRole.INLAB: displayString = "inpatient laboratory"; break;
                case EnumLocationRole.OUTLAB: displayString = "outpatient laboratory"; break;
                case EnumLocationRole.HRAD: displayString = "radiology unit"; break;
                case EnumLocationRole.HUSCS: displayString = "specimen collection site"; break;
                case EnumLocationRole.ICU: displayString = "Intensive care unit"; break;
                case EnumLocationRole.PEDICU: displayString = "Pediatric intensive care unit"; break;
                case EnumLocationRole.PEDNICU: displayString = "Pediatric neonatal intensive care unit"; break;
                case EnumLocationRole.INPHARM: displayString = "inpatient pharmacy"; break;
                case EnumLocationRole.MBL: displayString = "medical laboratory"; break;
                case EnumLocationRole.NCCS: displayString = "Neurology critical care and stroke unit"; break;
                case EnumLocationRole.NS: displayString = "Neurosurgery unit"; break;
                case EnumLocationRole.OUTPHARM: displayString = "outpatient pharmacy"; break;
                case EnumLocationRole.PEDU: displayString = "Pediatric unit"; break;
                case EnumLocationRole.PHU: displayString = "Psychiatric hospital unit"; break;
                case EnumLocationRole.RHU: displayString = "Rehabilitation hospital unit"; break;
                case EnumLocationRole.SLEEP: displayString = "Sleep disorders unit"; break;
                case EnumLocationRole.NCCF: displayString = "Nursing or custodial care facility"; break;
                case EnumLocationRole.SNF: displayString = "Skilled nursing facility"; break;
                case EnumLocationRole.OF: displayString = "Outpatient facility"; break;
                case EnumLocationRole.ALL: displayString = "Allergy clinic"; break;
                case EnumLocationRole.AMPUT: displayString = "Amputee clinic"; break;
                case EnumLocationRole.BMTC: displayString = "Bone marrow transplant clinic"; break;
                case EnumLocationRole.BREAST: displayString = "Breast clinic"; break;
                case EnumLocationRole.CANC: displayString = "Child and adolescent neurology clinic"; break;
                case EnumLocationRole.CAPC: displayString = "Child and adolescent psychiatry clinic"; break;
                case EnumLocationRole.CARD: displayString = "Ambulatory Health Care Facilities; Clinic/Center; Rehabilitation: Cardiac Facilities"; break;
                case EnumLocationRole.PEDCARD: displayString = "Pediatric cardiology clinic"; break;
                case EnumLocationRole.COAG: displayString = "Coagulation clinic"; break;
                case EnumLocationRole.CRS: displayString = "Colon and rectal surgery clinic"; break;
                case EnumLocationRole.DERM: displayString = "Dermatology clinic"; break;
                case EnumLocationRole.ENDO: displayString = "Endocrinology clinic"; break;
                case EnumLocationRole.PEDE: displayString = "Pediatric endocrinology clinic"; break;
                case EnumLocationRole.ENT: displayString = "Otorhinolaryngology clinic"; break;
                case EnumLocationRole.FMC: displayString = "Family medicine clinic"; break;
                case EnumLocationRole.GI: displayString = "Gastroenterology clinic"; break;
                case EnumLocationRole.PEDGI: displayString = "Pediatric gastroenterology clinic"; break;
                case EnumLocationRole.GIM: displayString = "General internal medicine clinic"; break;
                case EnumLocationRole.GYN: displayString = "Gynecology clinic"; break;
                case EnumLocationRole.HEM: displayString = "Hematology clinic"; break;
                case EnumLocationRole.PEDHEM: displayString = "Pediatric hematology clinic"; break;
                case EnumLocationRole.HTN: displayString = "Hypertension clinic"; break;
                case EnumLocationRole.IEC: displayString = "Impairment evaluation center"; break;
                case EnumLocationRole.INFD: displayString = "Infectious disease clinic"; break;
                case EnumLocationRole.PEDID: displayString = "Pediatric infectious disease clinic"; break;
                case EnumLocationRole.INV: displayString = "Infertility clinic"; break;
                case EnumLocationRole.LYMPH: displayString = "Lympedema clinic"; break;
                case EnumLocationRole.MGEN: displayString = "Medical genetics clinic"; break;
                case EnumLocationRole.NEPH: displayString = "Nephrology clinic"; break;
                case EnumLocationRole.PEDNEPH: displayString = "Pediatric nephrology clinic"; break;
                case EnumLocationRole.NEUR: displayString = "Neurology clinic"; break;
                case EnumLocationRole.OB: displayString = "Obstetrics clinic"; break;
                case EnumLocationRole.OMS: displayString = "Oral and maxillofacial surgery clinic"; break;
                case EnumLocationRole.ONCL: displayString = "Medical oncology clinic"; break;
                case EnumLocationRole.PEDHO: displayString = "Pediatric oncology clinic"; break;
                case EnumLocationRole.OPH: displayString = "Opthalmology clinic"; break;
                case EnumLocationRole.OPTC: displayString = "optometry clinic"; break;
                case EnumLocationRole.ORTHO: displayString = "Orthopedics clinic"; break;
                case EnumLocationRole.HAND: displayString = "Hand clinic"; break;
                case EnumLocationRole.PAINCL: displayString = "Pain clinic"; break;
                case EnumLocationRole.PC: displayString = "Primary care clinic"; break;
                case EnumLocationRole.PEDC: displayString = "Pediatrics clinic"; break;
                case EnumLocationRole.PEDRHEUM: displayString = "Pediatric rheumatology clinic"; break;
                case EnumLocationRole.POD: displayString = "Podiatry clinic"; break;
                case EnumLocationRole.PREV: displayString = "Preventive medicine clinic"; break;
                case EnumLocationRole.PROCTO: displayString = "Proctology clinic"; break;
                case EnumLocationRole.PROFF: displayString = "Provider's Office"; break;
                case EnumLocationRole.PROS: displayString = "Prosthodontics clinic"; break;
                case EnumLocationRole.PSI: displayString = "Psychology clinic"; break;
                case EnumLocationRole.PSY: displayString = "Psychiatry clinic"; break;
                case EnumLocationRole.RHEUM: displayString = "Rheumatology clinic"; break;
                case EnumLocationRole.SPMED: displayString = "Sports medicine clinic"; break;
                case EnumLocationRole.SU: displayString = "Surgery clinic"; break;
                case EnumLocationRole.PLS: displayString = "Plastic surgery clinic"; break;
                case EnumLocationRole.URO: displayString = "Urology clinic"; break;
                case EnumLocationRole.TR: displayString = "Transplant clinic"; break;
                case EnumLocationRole.TRAVEL: displayString = "Travel and geographic medicine clinic"; break;
                case EnumLocationRole.WND: displayString = "Wound clinic"; break;
                case EnumLocationRole.RTF: displayString = "Residential treatment facility"; break;
                case EnumLocationRole.PRC: displayString = "Pain rehabilitation center"; break;
                case EnumLocationRole.SURF: displayString = "Substance use rehabilitation facility"; break;
                case EnumLocationRole.DADDR: displayString = "Delivery Address"; break;
                case EnumLocationRole.MOBL: displayString = "Mobile Unit"; break;
                case EnumLocationRole.AMB: displayString = "Ambulance"; break;
                case EnumLocationRole.PHARM: displayString = "Pharmacy"; break;
                case EnumLocationRole.ACC: displayString = "accident site"; break;
                case EnumLocationRole.COMM: displayString = "Community Location"; break;
                case EnumLocationRole.CSC: displayString = "community service center"; break;
                case EnumLocationRole.PTRES: displayString = "Patient's Residence"; break;
                case EnumLocationRole.SCHOOL: displayString = "school"; break;
                case EnumLocationRole.UPC: displayString = "underage protection center"; break;
                case EnumLocationRole.WORK: displayString = "work site"; break;
            }

            return displayString;
        }

        public static String LocationTypeToString(EnumLocationType type)
        {
            String displayString = String.Empty;

            switch (type)
            {
                case EnumLocationType.bu: displayString = "Building"; break;
                case EnumLocationType.wi: displayString = "Wing"; break;
                case EnumLocationType.co: displayString = "Corridor"; break;
                case EnumLocationType.ro: displayString = "Room"; break;
                case EnumLocationType.ve: displayString = "Vehicle"; break;
                case EnumLocationType.ho: displayString = "House"; break;
                case EnumLocationType.ca: displayString = "Cabinet"; break;
                case EnumLocationType.rd: displayString = "Road"; break;
            }

            return displayString;
        }

        public static String OrganizationTypeToString(EnumOrganizationType type)
        {
            String displayString = String.Empty;

            switch (type)
            {
                case EnumOrganizationType.prov: displayString = "Healthcare Provider"; break;
                case EnumOrganizationType.dept: displayString = "Hospital Department"; break;
                case EnumOrganizationType.icu: displayString = "Intensive Care Unit"; break;
                case EnumOrganizationType.team: displayString = "Organizational team"; break;
                case EnumOrganizationType.fed: displayString = "Federal Government"; break;
                case EnumOrganizationType.ins: displayString = "Insurance Company"; break;
                case EnumOrganizationType.edu: displayString = "Educational Institute"; break;
                case EnumOrganizationType.reli: displayString = "Religious Institution"; break;
                case EnumOrganizationType.pharm: displayString = "Pharmacy"; break;
            }

            return displayString;
        }

        public static String MaritalStatusToString(EnumMaritalStatus maritalStatus)
        {
            String displayString = String.Empty;

            switch (maritalStatus)
            {
                case EnumMaritalStatus.A: displayString = "Annulled"; break;
                case EnumMaritalStatus.D: displayString = "Divorced"; break;
                case EnumMaritalStatus.I: displayString = "Interlocutor"; break;
                case EnumMaritalStatus.L: displayString = "Legally Separated"; break;
                case EnumMaritalStatus.M: displayString = "Married"; break;
                case EnumMaritalStatus.P: displayString = "Polygamous"; break;
                case EnumMaritalStatus.S: displayString = "Never Married"; break;
                case EnumMaritalStatus.T: displayString = "Domestic partner"; break;
                case EnumMaritalStatus.W: displayString = "Widowed"; break;
            }

            return displayString;
        }

    }
}
