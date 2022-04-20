using ServiceDocumentsGenerate.Entities;
using ServiceDocumentsGenerate.Entities.SlipPrint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities.PolicyPrint
{
    public class PolicyGeneralInfoVM
    {
        public PrintRequestVM requestVM { get; set; } = new PrintRequestVM();
        public ParticularConditionsVM particularConditionsVM { get; set; } = new ParticularConditionsVM();
        public GeneralConditionsVM generalConditionsVM { get; set; } = new GeneralConditionsVM();
        public ElectronicPolicyVM electronicPolicyVM { get; set; } = new ElectronicPolicyVM();
        public CoverComplementAdVM coverComplementAdVM { get; set; }
        public SummaryVM summaryVM { get; set; } = new SummaryVM();
        public InsuredProofVM insuredProofVM { get; set; } = new InsuredProofVM();
        public CertificateVM certificateVM { get; set; } = new CertificateVM();
        public CancellationVM cancellationVM { get; set; } = new CancellationVM();
        public InfoGeneralQuotationVM infoGeneralQuotationVM { get; set; } = new InfoGeneralQuotationVM();
        public EndorsementVM endorsementVM { get; set; } = new EndorsementVM();
        public BaseGenerateAccountStatusVM baseAccountStatusVM { get; set; } = new BaseGenerateAccountStatusVM();
        public ProvisionalRecordVM provisionalRecordVM { get; set; } = new ProvisionalRecordVM();
    }

    public class PrintRequestVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic SCLIENT_EMP { get; set; }
        public dynamic SIDDOC_EMP { get; set; }
        public dynamic SSTREET_EMP { get; set; }
        public dynamic SPHONE1_EMP { get; set; }
        public dynamic SPHONE2_EMP { get; set; }
        public dynamic SMAIL_EMP { get; set; }
        public dynamic SCLIENT_TIT { get; set; }
        public dynamic SIDDOC_TIT { get; set; }
        public dynamic SSTREET_TIT { get; set; }
        public dynamic SPHONE1_TIT { get; set; }
        public dynamic SCLIENT_BR { get; set; }
        public dynamic SIDDOC_BR { get; set; }
        public dynamic SSTREET_BR { get; set; }
        public dynamic SPHONE1_BR { get; set; }
        public dynamic COD_SBS { get; set; }
        public dynamic SMAIL_BR { get; set; }
        public dynamic NTIEMPO_ASE { get; set; }
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }
        public dynamic NRENOVACION { get; set; }
        public dynamic SDESCRIPT_RENOVACION { get; set; }
        public dynamic NPREMIUM { get; set; }
        public dynamic SGLOSA1_EXCLU { get; set; }
        public dynamic SGLOSA2_EXCLU { get; set; }
        public dynamic SGLOSA3_EXCLU { get; set; }
        public dynamic SGLOSA1_PROTECTA { get; set; }
        public dynamic SGLOSA2_PROTECTA { get; set; }
        public dynamic SGLOSA3_PROTECTA { get; set; }
        public dynamic SGLOSA4_PROTECTA { get; set; }

        public dynamic SMUNI_PROV_TIT { get; set; }
        public dynamic NAGEMININSM { get; set; }
        public dynamic DISSUEDAT { get; set; }
        public dynamic SWAY_PAY { get; set; }
        public dynamic NPREMIUM_COVID { get; set; }
        public dynamic NPREMIUM_TOTAL_COVID { get; set; }
        public dynamic SACTIVIDAD { get; set; }
        public dynamic SDEVOL { get; set; }
        public dynamic NAMOUNT { get; set; }
        public dynamic SCOMMENT { get; set; }
        public dynamic SDATE_FINAL { get; set; }
        public dynamic NBILLNUM { get; set; }
        public dynamic STEXT_MINA { get; set; }
        public dynamic NCONSTANCIA { get; set; }
        public dynamic NREM_EXC { get; set; }

        /*INI CHANGES COVID MARC*/
        public dynamic SCLIENAME_ASE { get; set; }
        public dynamic SSTREET_ASE { get; set; }
        public dynamic SIDDOC_ASE { get; set; }
        public dynamic DBIRTHDAT_ASE { get; set; }
        public dynamic SSEXO_ASE { get; set; }
        public dynamic SPHONE_ASE { get; set; }
        public dynamic SMAIL_ASE { get; set; }
        public dynamic NAGE_MIN_TIT { get; set; }
        public dynamic NAGE_MAX_TIT { get; set; }
        public dynamic NAGE_LIM_TIT { get; set; }
        public dynamic SCOD_SBS { get; set; }
        public dynamic NSOL_CERTIF { get; set; }
        /*FIN CHANGES COVID MARC*/

        public dynamic INI_VIG_POLICY { get; set; }
        public dynamic FIN_VIG_POLICY { get; set; }
        public dynamic L_FECHA { get; set; }

        /*INI RQ-IMPL-CUADRO-POLICY-SCTR MARC */
        public dynamic SCOD_SBS_EMP { get; set; }
        public dynamic SACTECO_TIT { get; set; }
        public dynamic SCODE_CIU_TIT { get; set; }
        public dynamic SCHARGE_CONTR_TIT { get; set; }
        public dynamic SMAIL_TIT { get; set; }
        public dynamic SPROVINCE_TIT { get; set; }
        public dynamic SDEPARTMENT_TIT { get; set; }
        public dynamic SRATEPAY { get; set; }
        public dynamic SSITE_PAY { get; set; }
        public dynamic SNAME_CONTR { get; set; }
        public dynamic SIDDOC_CONTR { get; set; }
        public dynamic SCURRENCY_DES { get; set; }
        /*FIN RQ-IMPL-CUADRO-POLICY-SCTR MARC */
        //ACCPER
        public dynamic NPREMIUM_N { get; set; }
        public dynamic NPREMIUM_T { get; set; }
        public dynamic PORCEN_PN { get; set; }
        public dynamic HOURS_ANUAL { get; set; }
        public dynamic N_EVENTS { get; set; }
        public dynamic NMESES_R1 { get; set; }
        public dynamic NMESES_R2 { get; set; }
        public dynamic NMESES_S1 { get; set; }
        public dynamic NMESES_S2 { get; set; }
        public dynamic NSEMESTT_T1 { get; set; }
        public dynamic NSEMESTT_T2 { get; set; }
        public dynamic NANIOS_T { get; set; }
        public dynamic NPENSION_U { get; set; }
        public dynamic NSESIONES_AA { get; set; }
        public dynamic NMESES_BB { get; set; }
        public dynamic NANEXO { get; set; }
        public dynamic P_TCEA { get; set; }
        public dynamic SDEDUCIBLE { get; set; }
        public dynamic SCOPAGO { get; set; }
        public dynamic SLIMITE_AGREG { get; set; }
        public dynamic P_CARENCIA { get; set; }
        public dynamic SIDDOC { get; set; }
        public dynamic SPHONE_TIT { get; set; }
        public dynamic SNAME_ASE { get; set; }
        public dynamic SFECHANAC_ASE { get; set; }
        public dynamic SGENERO_ASE { get; set; }
        public dynamic SEMAIL_ASE { get; set; }
        public dynamic SCLIENAME_BEN1 { get; set; }
        public dynamic SSTREET_BEN1 { get; set; }
        public dynamic SCLIENAME_BEN2 { get; set; }
        public dynamic SSTREET_BEN2 { get; set; }
        public dynamic SRELACION_BEN1 { get; set; }
        public dynamic SRELACION_BEN2 { get; set; }
        public dynamic STEXT_ASSISTENCES { get; set; }
        public dynamic NTYPE_POLICY { get; set; } //
        public dynamic SRELACION_ASE { get; set; } //
        public dynamic NCOMMI_COMER { get; set; }
        public dynamic NCOMMI_BROKER { get; set; }
        public dynamic NCOMMI_PROMO { get; set; }
        public dynamic DINI_VIG_CERT { get; set; }
        public dynamic DFIN_VIG_CERT { get; set; }
        public dynamic NAGEMAXINSM { get; set; }
        public dynamic NAGEMAXPERM { get; set; }
        public dynamic WAYPAY { get; set; }
        public dynamic STEXT_BENEFITS { get; set; }
        public dynamic SIDDOC_BEN1 { get; set; }
        public dynamic NPARTICIP_BEN1 { get; set; }
        public dynamic TYPDOC_TIT { get; set; }
        public dynamic L_SCLIENT_BEN1 { get; set; }
        public dynamic L_SSTREET_BEN1 { get; set; }
        public dynamic L_SRELACION_BEN1 { get; set; }
        public dynamic L_NPARTICIP_BEN1 { get; set; }
        public dynamic L_SCLIENT_BEN2 { get; set; }
        public dynamic L_SSTREET_BEN2 { get; set; }
        public dynamic L_SRELACION_BEN2 { get; set; }
        public dynamic L_NPARTICIP_BEN2 { get; set; }
        public dynamic PCONDITIONS { get; set; }
        public dynamic SREGISTERNUM { get; set; }
        public dynamic SPLANILLA_TOTAL { get; set; }
        //
        //Cambios Solicitud Seguro Degravamen
        public dynamic SCLIENT_TIT_AP { get; set; }
        public dynamic SCLIENT_TIT_AM { get; set; }
        public dynamic SCLIENT_TIT_N { get; set; }
        public dynamic DBIRTHD_TIT { get; set; }
        public dynamic SPROFESSION_TIT { get; set; }
        public dynamic SSEX_TIT { get; set; }
        public dynamic SNATIONALITY_TIT { get; set; }
        public dynamic SPHONEFIJ_TIT { get; set; }
        public dynamic SPHONEMOV_TIT { get; set; }
        public dynamic SCIVILSTAT_TIT { get; set; }
        public dynamic SDISTRICT_TIT { get; set; }
        public dynamic SNOMDOC_TIT { get; set; }
        public dynamic NRNPDP { get; set; }
        public dynamic SDATABANK { get; set; }
        public dynamic NT_CREDIT { get; set; }

        public List<PrintRequestInsuredVM> insuredList = new List<PrintRequestInsuredVM>();
        public List<PrintRequestPaymentVM> paymentList = new List<PrintRequestPaymentVM>();
        public List<CertificateCoverVM> coverList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList2 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList3 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList4 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList5 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList6 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList7 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList8 = new List<CertificateCoverVM>();
        public List<PrintRequestCoverComplement> coverComplementList = new List<PrintRequestCoverComplement>();
        public List<PrintRequestCoverAdditional> coverAdditionalList = new List<PrintRequestCoverAdditional>();
        public List<PrintRequestGlossExclusiveCoVM> glossExclusiveCoList = new List<PrintRequestGlossExclusiveCoVM>();
        public List<PrintRequestGlossExclusiveAdVM> glossExclusiveAdList = new List<PrintRequestGlossExclusiveAdVM>();
        public List<PrintRequestGlossProtectaVM> glossProtectaList = new List<PrintRequestGlossProtectaVM>();
        public List<PrintRequestInsuredVM> insuredListMod = new List<PrintRequestInsuredVM>();
        public List<PrintRequestInsuredVM> insuredList3 = new List<PrintRequestInsuredVM>();
        public List<InfoInsuredDetailVM> infoInsuredList = new List<InfoInsuredDetailVM>();
        public List<CertificateGlossProtectaVM> glossProtectaList1 = new List<CertificateGlossProtectaVM>();
        public List<CertificateGlossProtectaVM> glossProtectaList2 = new List<CertificateGlossProtectaVM>();
        public List<CertificateCoverVM> coverList1 = new List<CertificateCoverVM>();
        public List<InfoGlosOtherVM> glossOthersList = new List<InfoGlosOtherVM>();
        public List<CertificateBenefits> benefitList = new List<CertificateBenefits>();
        public List<ParticularConditionsAssists> assistList = new List<ParticularConditionsAssists>();
        public List<CertificateBenefits> beneficiaryList = new List<CertificateBenefits>();
        public List<ExclusionVM> exclusionList = new List<ExclusionVM>();
        public List<InfoGlossOtherVM> glossOthersComerList = new List<InfoGlossOtherVM>();
        public List<AnexoPDF> anexosList = new List<AnexoPDF>();
        public CertificateVM certificateVM = new CertificateVM();
    }

    public class CertificateVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic NCERTIF { get; set; }
        public dynamic SCLIENT_EMP { get; set; }
        public dynamic SIDDOC_EMP { get; set; }
        public dynamic SSTREET_EMP { get; set; }
        public dynamic SPHONE1_EMP { get; set; }
        public dynamic SPHONE2_EMP { get; set; }
        public dynamic SMAIL_EMP { get; set; }
        public dynamic SCLIENT_TIT { get; set; }
        public dynamic SIDDOC_TIT { get; set; }
        public dynamic SSTREET_TIT { get; set; }
        public dynamic SPHONE_TIT { get; set; }
        public dynamic SCLIENAME_ASE { get; set; }
        public dynamic SIDDOC_ASE { get; set; }
        public dynamic SSTREET_ASE { get; set; }
        public dynamic SPHONE_ASE { get; set; }
        public dynamic DBIRTHDAT_ASE { get; set; }
        public dynamic SRELACION_ASE { get; set; }
        public dynamic DINI_VIG_CERT { get; set; }
        public dynamic DFIN_VIG_CERT { get; set; }
        public dynamic SSEXO_ASE { get; set; }
        public dynamic SMAIL_ASE { get; set; }
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }
        public dynamic NPREMIUM_COVID { get; set; }
        public dynamic NPREMIUM_TOTAL_COVID { get; set; }
        public dynamic SWAY_PAY { get; set; }
        public dynamic STELEFONO_ASE { get; set; }
        public dynamic SDIRECCION_ASE { get; set; }
        public dynamic SDESCRIPT_CLAU_AD { get; set; }
        public dynamic NEDAD_MIN_COV_CO { get; set; }
        public dynamic NEDAD_MAX_COV_CO { get; set; }
        public dynamic NEDAD_LIM_COV_CO { get; set; }
        public dynamic NEDAD_MIN_COV_AD { get; set; }
        public dynamic NEDAD_MAX_COV_AD { get; set; }
        public dynamic NEDAD_LIM_COV_AD { get; set; }
        public dynamic DISSUEDAT { get; set; }
        public dynamic NAGEMININSM { get; set; }
        public dynamic NREM_EXC { get; set; }
        public dynamic NAGE_MIN_TIT { get; set; }
        public dynamic NAGE_MAX_TIT { get; set; }
        public dynamic NAGE_LIM_TIT { get; set; }
        public dynamic SSRELACION { get; set; }
        public dynamic SCOD_SBS { get; set; }
        public dynamic SCLIENAME_BRO { get; set; }
        public dynamic NCOMMI_COMER { get; set; }
        public dynamic COD_SBS { get; set; }
        public dynamic SNACIONALITY { get; set; }
        public dynamic SPROFESION { get; set; }
        public dynamic SCLIENAME_BEN1 { get; set; }
        public dynamic SSTREET_BEN1 { get; set; }
        public dynamic SCLIENAME_BEN2 { get; set; }
        public dynamic SSTREET_BEN2 { get; set; }
        public dynamic SRELACION_BEN1 { get; set; }
        public dynamic SRELACION_BEN2 { get; set; }
        public dynamic NPREMIUM_N { get; set; }
        public dynamic NPREMIUM_T { get; set; }
        public dynamic DINI_VIG_CER { get; set; }
        public dynamic DFIN_VIG_CER { get; set; }
        public dynamic NCOMMI_BROKER { get; set; }
        public dynamic NCOMMI_PROMO { get; set; }
        public dynamic NAGEMAXINSM { get; set; }
        public dynamic NAGEMAXPERM { get; set; }
        public dynamic SWEB_EMP { get; set; }
        public dynamic PORCEN_PN { get; set; }
        public dynamic HOURS_ANUAL { get; set; }
        public dynamic N_EVENTS { get; set; }
        public dynamic NMESES_R1 { get; set; }
        public dynamic NMESES_R2 { get; set; }
        public dynamic NMESES_S1 { get; set; }
        public dynamic NMESES_S2 { get; set; }
        public dynamic NSEMESTT_T1 { get; set; }
        public dynamic NSEMESTT_T2 { get; set; }
        public dynamic NANIOS_T { get; set; }
        public dynamic NPENSION_U { get; set; }
        public dynamic NSESIONES_AA { get; set; }
        public dynamic NMESES_BB { get; set; }
        public dynamic NANEXO { get; set; }
        public dynamic P_TCEA { get; set; }
        public dynamic SDEDUCIBLE { get; set; }
        public dynamic SCOPAGO { get; set; }
        public dynamic SLIMITE_AGREG { get; set; }
        public dynamic P_CARENCIA { get; set; }
        public dynamic WAYPAY { get; set; }
        public dynamic NMESES_PEN { get; set; }
        public dynamic SIDDOC { get; set; }
        public dynamic SEMAIL_ASE { get; set; }
        public dynamic SFECHANAC_ASE { get; set; }
        public dynamic SGENERO_ASE { get; set; }
        public dynamic SIDDOC_BEN1 { get; set; }
        public dynamic NPARTICIP_BEN1 { get; set; }
        public dynamic NPARTICIP_BEN2 { get; set; }
        public dynamic SBS_PROTECTA { get; set; }
        public dynamic SNAME_BROKER { get; set; }
        public dynamic NINTER_BROKER { get; set; }
        public dynamic SNAME_COMER { get; set; }
        public dynamic NCLIENT_COMER { get; set; }
        public dynamic STEXT_ASSISTENCES { get; set; }
        public dynamic SMESES_R1 { get; set; }
        public dynamic STEXT_BENEFITS { get; set; }
        public dynamic NTYPE_POLICY { get; set; }
        public dynamic TYPDOC_TIT { get; set; }
        public dynamic NPREMIUM_N_ASEG { get; set; }
        public dynamic NPREMIUM_T_ASEG { get; set; }

        public List<CertificateCoverVM> coverList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList2 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList3 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList4 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList5 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList6 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList7 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList8 = new List<CertificateCoverVM>();
        public List<CertificateCoverCoVM> coverComplementList = new List<CertificateCoverCoVM>();
        public List<CertificateCoverCoVM> coverComplementList2 = new List<CertificateCoverCoVM>();
        public List<CertificateCoverCoVM> coverComplementList3 = new List<CertificateCoverCoVM>();
        public List<CertificateCoverAdVM> coverAdditionalList = new List<CertificateCoverAdVM>();
        public List<CertificateCoverAdVM> coverAdditionalList2 = new List<CertificateCoverAdVM>();
        public List<CertificateGlossProtectaVM> glossProtectaList = new List<CertificateGlossProtectaVM>();
        public List<CertificateGlossProtectaVM> glossProtectaList1 = new List<CertificateGlossProtectaVM>();
        public List<CertificateClaimGlossProtectaVM> claimGlossProtectaList = new List<CertificateClaimGlossProtectaVM>();
        public List<InfoGlosOtherVM> glossOthersList = new List<InfoGlosOtherVM>();
        public List<InfoGlosOtherVM> glossOtherList = new List<InfoGlosOtherVM>();
        public List<InfoGlosOtherVM> glossOthersComerList = new List<InfoGlosOtherVM>();
        public List<CertificateBenefits> benefitList = new List<CertificateBenefits>();
        public List<ExclusionVM> exclusionList = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList1 = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList2 = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList3 = new List<ExclusionVM>();
        public List<CertificateVM> CERTIFICATE_LIST { get; set; }
        public List<BaseCover> CONDITION_COVER = new List<BaseCover>();
    }

    public class BaseCover
    {
        public string SNAMEVARIABLE { get; set; }
        public string SFLAG { get; set; }
        public dynamic NCOVERGEN { get; set; }
        public string NMINAGE { get; set; }
        public string NMAXAGE { get; set; }
    }

    public class CertificateClaimGlossProtectaVM : BaseFormatVM
    {
        public dynamic SCLAIM_GLOSA_PROTECTA { get; set; }
    }

    public class CertificateCoverAdVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_AD { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD { get; set; }
        public dynamic SDET_COVER_AD { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_TI { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_HI { get; set; }
    }

    public class CertificateCoverCoVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_CO { get; set; }
        public dynamic SDET_COVER_CO { get; set; }
        public dynamic NAGE_MAX_CO_CO { get; set; } // JDD
    }

    //public class CoverResponseVM
    //{
    //    public List<CertificateCoverVM> coverNotSubItemList = new List<CertificateCoverVM>();
    //    public List<CertificateCoverVM> coverMainList = new List<CertificateCoverVM>();
    //    public List<CertificateCoverVM> coverAditionalList = new List<CertificateCoverVM>();
    //    public List<CertificateCoverVM> coverMainNotSubItemList = new List<CertificateCoverVM>();
    //    public List<CertificateCoverVM> coverAditionalNotSubItemList = new List<CertificateCoverVM>();
    //    public List<CertificateCoverVM> coverLimitZeroList = new List<CertificateCoverVM>();
    //    public List<CertificateCoverVM> coverClauseList = new List<CertificateCoverVM>();
    //}

    public class InfoGlossOtherVM
    {
        public dynamic SGLOSA_PROTECTA { get; set; }
        public dynamic SGLOSA_OTROS { get; set; } // AP
        public dynamic NCOMMI_BROKER { get; set; }
        public dynamic NCOMMI_PROMO { get; set; }
        public dynamic NCOMMI_COMER { get; set; }
        public dynamic WAYPAY { get; set; }
        public dynamic NAGEMAXINSM { get; set; }
        public dynamic NAGEMAXPERM { get; set; }
        public dynamic DINI_VIG_CERT { get; set; }
        public dynamic DFIN_VIG_CERT { get; set; }
    }

    public class PrintRequestInsuredVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_MODULEC { get; set; }
        public dynamic NCANT { get; set; }
        public dynamic NPLANILLA { get; set; }
        public dynamic NTASA { get; set; }
        public dynamic SCLIENT { get; set; }
        public dynamic SCLIENAME { get; set; }
        public dynamic STIPDOC { get; set; }
        public dynamic SIDDOC { get; set; }
        public dynamic SDBIRTHDAT { get; set; }
        public dynamic STIPASE { get; set; }
        public dynamic SPHONE { get; set; }
        public dynamic SMAIL { get; set; }
        public dynamic SDIRE { get; set; }
        public dynamic SFIRSTNAME { get; set; }
        public dynamic SLASTNAME { get; set; }
        public dynamic SLASTNAME2 { get; set; }
        public dynamic SDOC { get; set; }
        public dynamic NMODULEC { get; set; }
        public dynamic SCATEGORIA { get; set; }
        public dynamic NPLANILLA_END { get; set; }
        public dynamic NPRI_COMER { get; set; }
        public dynamic N { get; set; }
        public dynamic SAPE_PAT { get; set; }
        public dynamic SAPE_MAT { get; set; }
        public dynamic SNAME { get; set; }
        public dynamic DBIRTHDAT { get; set; }
        public dynamic REQUEST_INSURED_EX { get; set; }

        public void increment()
        {
            NITEM++;
        }
    }

    public class ParticularConditionsAssists
    {
        public dynamic NCOD_ASSISTANCE { get; set; }
        public dynamic SDESCRIPT_ASSIT { get; set; }
        public dynamic COD_URL { get; set; }
    }

    public class AnexoPDF
    {
        public dynamic NCOD_ASSISTANCE { get; set; }
        public dynamic SDESCRIPT_ASSIT { get; set; }
        public dynamic COD_URL { get; set; }
        public dynamic NCOVERGEN { get; set; }
    }

    public class CertificateBenefits
    {
        public dynamic SDESCRIPT_BENEF { get; set; }
        public dynamic SDESCRIPT_BENEF_PORC { get; set; }
        public dynamic BENEF_PORCAMOUNT { get; set; }
        public dynamic COD_URL { get; set; }
        public dynamic SNAME_B { get; set; }
        public dynamic NSIDDOC_B { get; set; }
        public dynamic SRELATION_B { get; set; }
        public dynamic DBIRTHDAT_B { get; set; }
        public dynamic NPARTICIP_B { get; set; }
        public dynamic SNOMDOC_B { get; set; }

    }

    public class InfoGlosOtherVM
    {
        public dynamic SGLOSA_OTROS { get; set; }
        public dynamic SGLOSA_PROTECTA { get; set; }
        public dynamic NCOMMI_BROKER { get; set; }
        public dynamic NCOMMI_PROMO { get; set; }
        public dynamic NCOMMI_COMER { get; set; }
        public dynamic WAYPAY { get; set; }
        public dynamic NAGEMAXINSM { get; set; }
        public dynamic NAGEMAXPERM { get; set; }
        public dynamic DINI_VIG_CERT { get; set; }
        public dynamic DFIN_VIG_CERT { get; set; }
        public dynamic REQUEST_GLOSS_PROTE { get; set; }
    }

    public class CertificateGlossProtectaVM : BaseFormatVM
    {
        public dynamic SGLOSA_PROTECTA { get; set; }
        public dynamic REQUEST_GLOSS_PROTE { get; set; }
    }

    public class PrintRequestCoverComplement : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_CO { get; set; }
        public dynamic NAGE_MAX_CO_CO { get; set; }
    }

    public class PrintRequestCoverAdditional : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_AD { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_TI { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_HI { get; set; }
    }

    public class PrintRequestGlossExclusiveCoVM : BaseFormatVM
    {
        public dynamic SGLOSA_EXCLU_CO { get; set; }
    }

    public class PrintRequestGlossExclusiveAdVM : BaseFormatVM
    {
        public dynamic SGLOSA_EXCLU_AD { get; set; }
    }

    public class PrintRequestGlossProtectaVM : BaseFormatVM
    {
        public dynamic SGLOSA_PROTECTA { get; set; }
        public dynamic REQUEST_GLOSS_PROTE { get; set; }
    }

    public class InfoInsuredDetailVM
    {
        public dynamic SDOCTYPE { get; set; }
        public dynamic SDOCNUMBER { get; set; }
        public dynamic SLASTNAME { get; set; }
        public dynamic SLASTNAME2 { get; set; }
        public dynamic SFIRSTNAME { get; set; }
        public dynamic SSEX { get; set; }
        public dynamic DBIRTHDAT { get; set; }
        public dynamic SWORKERTYPE { get; set; }
        public dynamic SSALARY_TOT { get; set; }
        public dynamic SLOCATION { get; set; }
        public dynamic SMOVEMENT { get; set; }
        public dynamic SCOUNTRY { get; set; }
    }

    public class ParticularConditionsVM : BaseFormatVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic SCURREN_DES { get; set; }
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }
        public dynamic SCLIENT_TIT { get; set; }
        public dynamic SIDDOC_TIT { get; set; }
        public dynamic SSTREET_TIT { get; set; }
        public dynamic SMUNI_PROV_TIT { get; set; }
        public dynamic SPHONE_TIT { get; set; }
        public dynamic SCURREN_SDES { get; set; }
        public dynamic NPREMIUM_N { get; set; }
        public dynamic NPREMIUM_T { get; set; }
        public dynamic NCOMMISSION_BR { get; set; }
        public dynamic NCOMMISSION_PR { get; set; }
        public dynamic SCOD_SBS { get; set; }
        public dynamic SRATEPAY { get; set; }
        public dynamic SBROKER { get; set; }
        public dynamic SREGISTERNUM { get; set; }
        public dynamic DISSUEDAT { get; set; }
        public dynamic SWAY_PAY { get; set; }
        public dynamic NPREMIUM_COVID { get; set; }
        public dynamic NPREMIUM_TOTAL_COVID { get; set; }
        public dynamic SCLIENTNAME { get; set; }
        public dynamic NCOMMI_COMER { get; set; }
        public dynamic NAGE_MIN_TIT { get; set; }
        public dynamic NAGE_MAX_TIT { get; set; }
        public dynamic NAGE_LIM_TIT { get; set; }
        public dynamic NAGE_MIN_HIJO { get; set; }
        public dynamic NAGE_MAX_HIJO { get; set; }
        public dynamic NAGE_LIM_HIJO { get; set; }
        public dynamic SDESC_RENOV { get; set; }
        public dynamic COD_SBS { get; set; }
        public dynamic NREM_EXC { get; set; }
        public dynamic STEXT_ASG { get; set; }
        public dynamic NCOMMI_BROKER { get; set; }
        public dynamic NCOMMI_PROMO { get; set; }
        public dynamic NAGEMININSM { get; set; }
        public dynamic NAGEMAXINSM { get; set; }
        public dynamic NAGEMAXPERM { get; set; }
        public dynamic SCLIENAME_ASE { get; set; }
        public dynamic SIDDOC_ASE { get; set; }
        public dynamic SSTREET_ASE { get; set; }
        public dynamic SPHONE_ASE { get; set; }
        public dynamic SCLIENT_CON { get; set; }
        public dynamic SIDDOC_CON { get; set; }
        public dynamic SSTREET_CON { get; set; }
        public dynamic SPHONE_CON { get; set; }
        public dynamic SCLIENT_DEP { get; set; }
        public dynamic SIDDOC_DEP { get; set; }
        public dynamic SSTREET_DEP { get; set; }
        public dynamic SPHONE_DEP { get; set; }
        public dynamic SCLIENT_EMP { get; set; }
        public dynamic SIDDOC_EMP { get; set; }
        public dynamic SSTREET_EMP { get; set; }
        public dynamic SPHONE1_EMP { get; set; }
        public dynamic SPHONE2_EMP { get; set; }
        public dynamic SMAIL_EMP { get; set; }
        public dynamic SBRANCH { get; set; }
        public dynamic SVALIDITY { get; set; }
        public dynamic SACTECO_TIT { get; set; }
        public dynamic SMAIL_TIT { get; set; }
        public dynamic DVAL_COVER_DT_FROM { get; set; }
        public dynamic DVAL_COVER_DT_TO { get; set; }
        public dynamic SLOCATION_DES { get; set; }
        public dynamic SCOMMERCIAL_PREMIUM { get; set; }
        public dynamic SIGV { get; set; }
        public dynamic SPREMIUM_TOT { get; set; }
        public dynamic SSITE_PAY { get; set; }
        public dynamic NTCEA { get; set; }
        public dynamic SCHARGE_BROKER { get; set; }
        public dynamic SCHARGE_SPONSOR { get; set; }
        public dynamic SCHARGE_BANCA { get; set; }
        public dynamic SDATE_FINAL { get; set; }
        public dynamic PORCEN_PN { get; set; }
        public dynamic HOURS_ANUAL { get; set; }
        public dynamic N_EVENTS { get; set; }
        public dynamic NMESES_R1 { get; set; }
        public dynamic NMESES_R2 { get; set; }
        public dynamic NMESES_S1 { get; set; }
        public dynamic NMESES_S2 { get; set; }
        public dynamic NSEMESTT_T1 { get; set; }
        public dynamic NSEMESTT_T2 { get; set; }
        public dynamic NANIOS_T { get; set; }
        public dynamic NPENSION_U { get; set; }
        public dynamic NSESIONES_AA { get; set; }
        public dynamic NMESES_BB { get; set; }
        public dynamic NANEXO { get; set; }
        public dynamic P_TCEA { get; set; }
        public dynamic SDEDUCIBLE { get; set; }
        public dynamic SCOPAGO { get; set; }
        public dynamic SLIMITE_AGREG { get; set; }
        public dynamic P_CARENCIA { get; set; }
        public dynamic NMESES_PEN { get; set; }
        public dynamic has_benefit { get; set; }
        public dynamic SNAME_BROKER { get; set; }
        public dynamic NINTER_BROKER { get; set; }
        public dynamic SNAME_COMER { get; set; }
        public dynamic NCLIENT_COMER { get; set; }
        public dynamic FLAG_COMER { get; set; }
        public dynamic FLAG_CORRE { get; set; }
        public dynamic FLAG_PROMO { get; set; }
        public dynamic STEXT_ASSISTENCES { get; set; }
        public dynamic NTYPE_POLICY { get; set; }
        public dynamic TYPDOC_TIT { get; set; }
        public dynamic STEXT_BENEFITS { get; set; }

        public List<ParticularConditionsModuleVM> moduleList = new List<ParticularConditionsModuleVM>();
        public List<CertificateCoverVM> coverList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList2 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList3 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList4 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList5 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList6 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList7 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList8 = new List<CertificateCoverVM>();
        public List<ParticularConditionsCoverCoVM> coverComplementList = new List<ParticularConditionsCoverCoVM>();
        public List<ParticularConditionsCoverAdVM> coverAdditionalList = new List<ParticularConditionsCoverAdVM>();
        public List<ParticularConditionsPremiumRateCoVM> premiumListCo = new List<ParticularConditionsPremiumRateCoVM>();
        public List<ParticularConditionsPremiumRateAdVM> premiumListAd = new List<ParticularConditionsPremiumRateAdVM>();
        public List<ParticularConditionsGlossProtectaVM> glossProtectaList = new List<ParticularConditionsGlossProtectaVM>();
        public List<ParticularConditionsGlossProtectaVM> glossProtectaList1 = new List<ParticularConditionsGlossProtectaVM>();
        public List<ParticularConditionsClaimGlossProtectaVM> claimGlossProtectaList = new List<ParticularConditionsClaimGlossProtectaVM>();
        public List<ParticularConditionsPremiumRateCoVM> premiumListCoMod = new List<ParticularConditionsPremiumRateCoVM>();
        public List<InfoInsuredVM> infoInsuredList = new List<InfoInsuredVM>();
        public List<ParticularConditionsBenefits> benefitList = new List<ParticularConditionsBenefits>();
        public List<ParticularConditionsAssists> assistList = new List<ParticularConditionsAssists>();
        public List<CertificateVM> infoCertificate = new List<CertificateVM>();
        public List<ParticularConditionsBenefits> beneficiaryList = new List<ParticularConditionsBenefits>();
        public List<ParticularConditionsCoverVM> coverRequiredList = new List<ParticularConditionsCoverVM>();
        public List<ParticularConditionsCoverVM> coverOptionalList = new List<ParticularConditionsCoverVM>();
    }

    public class ParticularConditionsCoverVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_PR { get; set; }
        public dynamic SDESCRIPT_CAPITAL_PR { get; set; }
        public dynamic SDESCRIPT_CAPITAL_PR_TI { get; set; }
        public dynamic SDESCRIPT_CAPITAL_PR_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_PR_HI { get; set; }
        public dynamic NCAPITAL { get; set; }
        public dynamic SCOVERUSE { get; set; }
        public dynamic NCOVER { get; set; }
        public dynamic SDESCRIPT_COVER { get; set; }
        public dynamic NCOVERGEN { get; set; }
        public dynamic NORDER { get; set; }
        public dynamic FLAG { get; set; }
        public dynamic NR { get; set; }
    }

    public class ParticularConditionsBenefits
    {
        public dynamic SDESCRIPT_BENEF { get; set; }
        public dynamic SDESCRIPT_BENEF_PORC { get; set; }
        public dynamic BENEF_PORCAMOUNT { get; set; }
        public dynamic SDESCRIPT_MOD { get; set; }
        public dynamic SNAME_B { get; set; }
        public dynamic NSIDDOC_B { get; set; }
        public dynamic SNOMDOC_B { get; set; }
        public dynamic SRELATION_B { get; set; }
        public dynamic DBIRTHDAT_B { get; set; }
        public dynamic NPARTICIP_B { get; set; }
    }

    public class ParticularConditionsPremiumRateCoVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_MODULEC { get; set; }
        public dynamic NTASA_MOD_CO { get; set; }
        public dynamic SCURREN_SDES_CO { get; set; }
        public dynamic NPREMIUMN_CO { get; set; }
        public dynamic PREMIUMN { get; set; }
        public dynamic PREMIUM { get; set; }
        public dynamic FLAG { get; set; }
        public dynamic PREMIUMN_DAY { get; set; }
        public dynamic DAYS { get; set; }
    }

    public class InfoInsuredVM
    {
        public dynamic DET_MODULEC { get; set; }
        public dynamic NNUM_TRABAJADORES { get; set; }
        public dynamic TOT_PLANILLA_MENSUAL { get; set; }
        public dynamic SMONTH_PAYROLL_T { get; set; }
        public dynamic SMONTH_PAYROLL_E { get; set; }
        public dynamic SACTIVIDAD { get; set; }
        public dynamic SPAYROLL_TOTAL { get; set; }
        public dynamic SRATE_NET { get; set; }
    }

    public class ParticularConditionsClaimGlossProtectaVM : BaseFormatVM
    {
        public dynamic SCLAIM_GLOSA_PROTECTA { get; set; }
    }

    public class ParticularConditionsGlossProtectaVM : BaseFormatVM
    {
        public dynamic SGLOSA_PROTECTA { get; set; }
    }

    public class ParticularConditionsPremiumRateAdVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_MODULEC { get; set; }
        public dynamic NTASA_MOD_AD { get; set; }
        public dynamic SCURREN_SDES_AD { get; set; }
        public dynamic NPREMIUMN_AD { get; set; }
    }

    public class ParticularConditionsCoverCoVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_CO { get; set; }
        public dynamic NAGE_MAX_CO_CO { get; set; }
        public dynamic NCAPITAL { get; set; }
    }

    public class ParticularConditionsModuleVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_MODULEC { get; set; }
        public dynamic NCANT { get; set; }
        public dynamic NPLANILLA { get; set; }
    }

    public class ParticularConditionsCoverAdVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_AD { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_TI { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_AD_HI { get; set; }
        public dynamic NCAPITAL { get; set; }
    }

    public class GeneralConditionsVM : BaseFormatVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic SCOD_SBS { get; set; }
        public dynamic STEXT_ASSISTENCES { get; set; }
        public dynamic NTYPE_POLICY { get; set; }
        public dynamic TYPDOC_TIT { get; set; }
        public List<ParticularConditionsGlossProtectaVM> glossProtectaList = new List<ParticularConditionsGlossProtectaVM>();
        public List<InfoGlosOtherVM> glossOthersList = new List<InfoGlosOtherVM>();
        public List<ExclusionVM> exclusionList = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList1 = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList2 = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList3 = new List<ExclusionVM>();
        public List<CertificateCoverVM> coverList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList2 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList3 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList4 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList5 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList6 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList7 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList8 = new List<CertificateCoverVM>();
        public List<InfoBenefitQuotationVM> benefitList = new List<InfoBenefitQuotationVM>();
    }

    public class ExclusionResponseVM
    {
        public List<ExclusionVM> exclusionOriginalList = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionModList = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionDeleteList = new List<ExclusionVM>();
    }

    public class InfoGeneralQuotationVM
    {
        public dynamic NID_COTIZACION { get; set; }
        public dynamic SCLIENT_EMP { get; set; }
        public dynamic SIDDOC_EMP { get; set; }
        public dynamic SSTREET_EMP { get; set; }
        public dynamic SPHONE1_EMP { get; set; }
        public dynamic SPHONE2_EMP { get; set; }
        public dynamic SMAIL_EMP { get; set; }
        public dynamic SCLIENT_TIT { get; set; }
        public dynamic SIDDOC_TIT { get; set; }
        public dynamic SSTREET_TIT { get; set; }
        public dynamic SPHONE1_TIT { get; set; }
        public dynamic SCLIENT_BR { get; set; }
        public dynamic SIDDOC_BR { get; set; }
        public dynamic SSTREET_BR { get; set; }
        public dynamic SPHONE1_BR { get; set; }
        public dynamic COD_SBS { get; set; }
        public dynamic SMAIL_BR { get; set; }
        public dynamic NCANT_EMP { get; set; }
        public dynamic NCANT_WORKER { get; set; }
        public dynamic NCANTRISK_WORKER { get; set; }
        /*public dynamic NTOT_PAYMONT1 { get; set; }
        public dynamic NTOT_PAYMONT2 { get; set; }
        public dynamic NTOT_PAY { get; set; }*/
        //public dynamic NRATE_EMP { get; set; }
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }
        public dynamic FECHA_FIRMA { get; set; }
        public dynamic COMMENT_FINAL { get; set; }
        public dynamic SVALIDITY { get; set; } // JDD
        public dynamic SCURRENCY { get; set; } // JDD
        public dynamic NAGE_MAX_PAYROLL { get; set; } // JDD
        public dynamic NAGE_AVG_PAYROLL { get; set; } // JDD
        public dynamic SCOMISSION_BR { get; set; } // JDD
        public dynamic SWAY_PAY { get; set; } // JDD
        public dynamic SDAY_PAY { get; set; } // JDD
        public dynamic SDATE_FINAL { get; set; } // JDD
        public dynamic NAGE_MAX_CO_CO { get; set; }
        public dynamic NAGE_MAX_CO_AD { get; set; }
        public dynamic SPREMIUM_MIN { get; set; }
        public dynamic NDIA_IND_HOSP { get; set; }
        public dynamic NAGE_MIN_TIT { get; set; }
        public dynamic NAGE_MAX_TIT { get; set; }
        public dynamic NAGE_LIM_TIT { get; set; }
        public dynamic NAGE_MIN_HIJO { get; set; }
        public dynamic NAGE_MAX_HIJO { get; set; }
        public dynamic NAGE_LIM_HIJO { get; set; }
        //INI RQ-IMPL-SLIP-SCTR MARC
        public dynamic ACTECO_TIT { get; set; }
        public dynamic SCOMMERCIAL_PREMIUM_TOT { get; set; }
        public dynamic SADVISORY_BR { get; set; }
        public dynamic SFREQUENCY_PAY { get; set; }
        public dynamic SSITE_PAY { get; set; }
        //INI RQ-IMPL-SLIP-SCTR MARC
        //public dynamic SNRO_ACC { get; set; }
        public dynamic STYPE_POLICY { get; set; }
        public dynamic STYPE_FACT { get; set; }
        public dynamic STYPE_MOD { get; set; }
        public dynamic SDESCRIPT_FRECUENCIA { get; set; }
        public dynamic SDESCRIPT_RENOVACION { get; set; }
        public dynamic NCOMISION_CORR { get; set; }
        public dynamic NCOMISION_COM { get; set; }
        public dynamic STEXT_ASG { get; set; }
        public dynamic STEXT_COVER { get; set; }
        public dynamic FLAG_VIAJE { get; set; }
        public dynamic FLAG_COVER { get; set; }
        public dynamic FLAG_INDIVIDUAL { get; set; }
        public dynamic HAS_FLAG_COVER { get; set; }
        public dynamic has_benefit { get; set; }
        public dynamic has_assistence { get; set; }
        public dynamic STEXT_ASSISTENCES { get; set; }
        public dynamic SMONEDA { get; set; }
        public dynamic STEXT_CONDSPECIAL { get; set; }
        public dynamic SLIMITE_AGREG { get; set; }
        public dynamic STEXT_BENEFITS { get; set; }
        public dynamic SDESCRIPTPRODMASTER { get; set; }

        public List<InfoInsuredQuotationVM> insuredList = new List<InfoInsuredQuotationVM>();
        public List<InfoRateQuotationVM> rateList = new List<InfoRateQuotationVM>();
        public List<InfoAccountQuotationVM> accountList = new List<InfoAccountQuotationVM>();
        public List<CertificateCoverVM> coverList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList2 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList3 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList4 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList5 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList6 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList7 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList8 = new List<CertificateCoverVM>();
        public List<InfoCoverCoQuotationVM> coverComplementList = new List<InfoCoverCoQuotationVM>();
        public List<InfoGlossExclusiveCoQuotationVM> glossExclusiveCoList = new List<InfoGlossExclusiveCoQuotationVM>();
        public List<InfoGlossExclusiveAdQuotationVM> glossExclusiveAdList = new List<InfoGlossExclusiveAdQuotationVM>();
        public List<InfoGlossProtectaQuotationVM> glossProtectaList = new List<InfoGlossProtectaQuotationVM>();
        public List<InfoRateDetVM> ratePremiumList = new List<InfoRateDetVM>(); // JDD
        public List<InfoRateDetVM> ratePremiumExList = new List<InfoRateDetVM>(); // JDD
        public List<InfoOverPayVM> coverOverPayList = new List<InfoOverPayVM>();
        public List<InfoRateDetVM> ratePremiumRemExList = new List<InfoRateDetVM>();
        public List<InfoBenefitQuotationVM> benefitList = new List<InfoBenefitQuotationVM>();
        public List<InfoAssistanceQuotationVM> assistanceList = new List<InfoAssistanceQuotationVM>();
        public List<InfoServicesQuotationVM> adicionalServicesList = new List<InfoServicesQuotationVM>();
        public List<ExclusionVM> exclusionList = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList1 = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList2 = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList3 = new List<ExclusionVM>();
    }

    public class InfoOverPayVM
    {
        public dynamic SDESCRIPT_COVER_EX { get; set; }
        public dynamic SDESCRIPT_CAPITAL_EX { get; set; }
    }

    public class InfoRateDetVM
    {
        public dynamic DET_MODULEC { get; set; }
        public dynamic NCOMMERCIAL_RATE { get; set; }
        public dynamic SCOMMERCIAL_PREMIUM { get; set; }
        public dynamic SCOMMERCIAL_PREMIUM_TIGV { get; set; }

    }
    public class InfoGlossProtectaQuotationVM
    {
        public dynamic SGLOSA_PROTECTA { get; set; }
    }

    public class InfoGlossExclusiveAdQuotationVM : BaseFormatVM
    {
        public dynamic SGLOSA_EXCLU_AD { get; set; }
    }

    public class InfoGlossExclusiveCoQuotationVM : BaseFormatVM
    {
        public dynamic SGLOSA_EXCLU_CO { get; set; }
    }

    public class InfoCoverCoQuotationVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_CO { get; set; }
        public dynamic NAGE_MAX_CO_CO { get; set; } // JDD
    }

    public class InfoAccountQuotationVM
    {
        public dynamic SNRO_ACC { get; set; }
    }

    public class InfoInsuredQuotationVM
    {
        public dynamic DET_MODULEC { get; set; }
        public dynamic NNUM_TRABAJADORES { get; set; }
        public dynamic TOT_PLANILLA_MENSUAL { get; set; }
        public dynamic SMONTH_PAYROLL_T { get; set; } // JDD
        public dynamic SMONTH_PAYROLL_E { get; set; } // JDD
        public dynamic SACTIVIDAD { get; set; }
        public dynamic SPAYROLL_TOTAL { get; set; }
        public dynamic SRATE_NET { get; set; }
        public dynamic SNAME_ASEG { get; set; }
    }

    public class InfoRateQuotationVM
    {
        public dynamic DET_MODULEC { get; set; }
        public dynamic NTASA { get; set; }
    }

    //public class InfoAssistanceQuotationVM
    //{
    //    public dynamic SDESCRIPT_ASSIT { get; set; }
    //    public dynamic DEDUC_ASSIT { get; set; }
    //    public dynamic PORCEN_COVER_ASSIT { get; set; }
    //    public dynamic EVENTS_ASSIT { get; set; }
    //    public dynamic COD_URL { get; set; }
    //}

    public class InfoServicesQuotationVM
    {
        public dynamic SDESC_SURCHARGE { get; set; }

    }

    public class ElectronicPolicyVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic SCURREN_DES { get; set; }
        public dynamic SCOD_SBS { get; set; }
    }

    public class CoverComplementAdVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic SCOD_SBS { get; set; }
        public dynamic NAGEMAXPERM { get; set; }
        public dynamic SCURREN_DES { get; set; }
        public dynamic DISSUEDAT { get; set; }
        public dynamic PORCEN_PN { get; set; }
        public dynamic HOURS_ANUAL { get; set; }
        public dynamic N_EVENTS { get; set; }
        public dynamic STEXT_SUBITEMS { get; set; }
        public dynamic STEXT_EXCLUSION { get; set; }
        public dynamic NTYPE_POLICY { get; set; }
        public dynamic TYPDOC_TIT { get; set; }
        public List<BaseCover> CONDITION_COVER = new List<BaseCover>();
        public List<ExclusionVM> exclusionList = new List<ExclusionVM>();
        public List<CertificateCoverVM> coverList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList2 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList3 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList4 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList5 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList6 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList7 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList8 = new List<CertificateCoverVM>();
        public List<InfoBenefitQuotationVM> benefitList = new List<InfoBenefitQuotationVM>();
    }

    public class SummaryVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic SCLIENT_EMP { get; set; }
        public dynamic SIDDOC_EMP { get; set; }
        public dynamic SSTREET_EMP { get; set; }
        public dynamic SPHONE1_EMP { get; set; }
        public dynamic SPHONE2_EMP { get; set; }
        public dynamic SMAIL_EMP { get; set; }
        public dynamic SCOND_CLAU_ADIC { get; set; }
        public dynamic NEDAD_MIN_COV_CO { get; set; }
        public dynamic NEDAD_MAX_COV_CO { get; set; }
        public dynamic NEDAD_LIM_COV_CO { get; set; }
        public dynamic SWAY_PAY { get; set; }
        public dynamic NAGE_MIN_TIT { get; set; }
        public dynamic NAGE_MAX_TIT { get; set; }
        public dynamic NAGE_LIM_TIT { get; set; }
        public dynamic NAGE_MIN_HIJO { get; set; }
        public dynamic NAGE_MAX_HIJO { get; set; }
        public dynamic NAGE_LIM_HIJO { get; set; }
        public dynamic SDESC_RENOV { get; set; }
        public dynamic NREM_EXC { get; set; }
        public dynamic NAGEMININSM { get; set; } //AP
        public dynamic NAGEMAXINSM { get; set; } //AP
        public dynamic NAGEMAXPERM { get; set; } //AP
        // AP
        public dynamic WAYPAY { get; set; }
        public dynamic PORCEN_PN { get; set; }
        public dynamic HOURS_ANUAL { get; set; }
        public dynamic N_EVENTS { get; set; }
        public dynamic NMESES_R1 { get; set; }
        public dynamic NMESES_R2 { get; set; }
        public dynamic NMESES_S1 { get; set; }
        public dynamic NMESES_S2 { get; set; }
        public dynamic NSEMESTT_T1 { get; set; }
        public dynamic NSEMESTT_T2 { get; set; }
        public dynamic NANIOS_T { get; set; }
        public dynamic NPENSION_U { get; set; }
        public dynamic NSESIONES_AA { get; set; }
        public dynamic NMESES_BB { get; set; }
        public dynamic NANEXO { get; set; }
        public dynamic P_TCEA { get; set; }
        public dynamic SDEDUCIBLE { get; set; }
        public dynamic SCOPAGO { get; set; }
        public dynamic SLIMITE_AGREG { get; set; }
        public dynamic P_CARENCIA { get; set; }
        public dynamic NMESES_PEN { get; set; }

        //AP
        //INI RQ-IMPL-CUADRO-POLICY-SCTR MARC
        public dynamic SPHONE1_BR { get; set; }
        public dynamic SMAIL_BR { get; set; }
        public dynamic SSTREET_BR { get; set; }
        public dynamic SWEB_BR { get; set; }
        public dynamic DISSUEDAT { get; set; }
        //FIN RQ-IMPL-CUADRO-POLICY-SCTR MARC

        public dynamic NPREMIUM_N { get; set; }
        public dynamic NPREMIUM_T { get; set; }
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }

        public dynamic NTYPE_POLICY { get; set; }
        public dynamic TYPDOC_TIT { get; set; }
        public dynamic SCOD_SBS { get; set; }

        public List<CertificateCoverVM> coverList = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList2 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList3 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList4 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList5 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList6 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList7 = new List<CertificateCoverVM>();
        public List<CertificateCoverVM> coverList8 = new List<CertificateCoverVM>();
        public List<SummaryCoverCoVM> coverComplementList = new List<SummaryCoverCoVM>();
        public List<SummaryGlossProtectaVM> glossProtectaList = new List<SummaryGlossProtectaVM>();
        public List<SummaryGlossProtectaVM> glossProtectaList1 = new List<SummaryGlossProtectaVM>();
        public List<InfoGlossOtherVM> glossOtherList = new List<InfoGlossOtherVM>();
        public List<InfoGlossOtherVM> glossOthersComerList = new List<InfoGlossOtherVM>(); //AP
        public List<InfoGlossOtherVM> glossOthersList = new List<InfoGlossOtherVM>(); // AP
        public List<SummaryClaimGlossProtectaVM> claimGlossProtectaList = new List<SummaryClaimGlossProtectaVM>();
        public List<BaseCover> CONDITION_COVER = new List<BaseCover>();
        public List<ExclusionVM> exclusionList = new List<ExclusionVM>(); //AP
        public List<ExclusionVM> exclusionList1 = new List<ExclusionVM>(); //AP
        public List<ExclusionVM> exclusionList2 = new List<ExclusionVM>(); //AP
        public List<ExclusionVM> exclusionList3 = new List<ExclusionVM>(); //AP
    }

    public class SummaryCoverCoVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_CO { get; set; }
    }

    public class SummaryGlossProtectaVM : BaseFormatVM
    {
        public dynamic SGLOSA_PROTECTA { get; set; }
        public dynamic SGLOSA_OTROS { get; set; } //AP
    }

    public class SummaryClaimGlossProtectaVM
    {
        public dynamic SCLAIM_GLOSA_PROTECTA { get; set; }
    }

    public class InsuredProofVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic SCLIENT_TIT { get; set; }
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }
        public dynamic SCOD_SBS { get; set; }
        public dynamic NTYPE_POLICY { get; set; }
        public dynamic TYPDOC_TIT { get; set; }

        public List<InsuredProofClientVM> clientList = new List<InsuredProofClientVM>();
    }

    public class ProvisionalRecordVM
    {
        public dynamic NPOLICY { get; set; }
        public dynamic SCLIENT { get; set; }
        public dynamic DEFFECDATE { get; set; }
        public dynamic DEXPIRDAT { get; set; }
        public dynamic SADDRESS { get; set; }
        public dynamic NTASA { get; set; }
        public dynamic NMONTH { get; set; }
        public dynamic NYEAR { get; set; }
        public dynamic NMONTH_AUX { get; set; }
        public dynamic NYEAR_AUX { get; set; }

        public List<InsuredProofClientVM> clientList = new List<InsuredProofClientVM>();
    }

    public class InsuredProofClientVM : BaseFormatVM
    {
        public dynamic N { get; set; }
        public dynamic SIDDOC { get; set; }
        public dynamic SAPE_PAT { get; set; }
        public dynamic SAPE_MAT { get; set; }
        public dynamic SNAME { get; set; }
        public dynamic DBIRTHDAT { get; set; }
    }

    public class CancellationVM
    {
        public dynamic SCLIENT_TIT { get; set; }
        public dynamic SIDDOC_TIT { get; set; }
        public dynamic NPOLICY { get; set; }
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }
        public dynamic TD_D { get; set; }
        public dynamic TD_M { get; set; }
        public dynamic TD_Y { get; set; }
        public dynamic TD_DMY { get; set; }
        public dynamic FLAG { get; set; }
        public dynamic p1 { get; set; }
        public dynamic p2 { get; set; }

    }

    public class EndorsementVM
    {
        public dynamic NENDOSO { get; set; }
        public dynamic INI_VIG_ENDOSO { get; set; }
        public dynamic FIN_VIG_ENDOSO { get; set; }
        public dynamic NPOLICY { get; set; }
        public dynamic DISSUEDAT { get; set; }
        public dynamic SRATEPAY { get; set; }
        public dynamic SWAY_PAY { get; set; }
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }
        public dynamic INI_VIG_POLICY { get; set; }
        public dynamic FIN_VIG_POLICY { get; set; }
        public dynamic SCLIENT_TIT { get; set; }
        public dynamic SIDDOC_TIT { get; set; }
        public dynamic SSTREET_TIT { get; set; }
        public dynamic SMUNI_PROV_TIT { get; set; }
        public dynamic SPHONE1_TIT { get; set; }
        public dynamic L_FECHA { get; set; }
        public dynamic FRECUENCIA_PAGO { get; set; }
        public dynamic FORMA_PAGO { get; set; }
        public dynamic DPUBLIC { get; set; }
        public dynamic NINSURED_M { get; set; }
        public dynamic NTYPE_POLICY { get; set; }
        public dynamic TYPDOC_TIT { get; set; }
        public dynamic NPLANILLA { get; set; }
        public dynamic NCOMMI_BROKER { get; set; }
        public dynamic NCOMMI_PROMO { get; set; }
        public dynamic NCOMMI_COMER { get; set; }
        public PrintRequestVM requestVM { get; set; }
        public ParticularConditionsVM particularConditionsVM { get; set; }
        public PrintRequestInsuredVM requestInsuredVM { get; set; }
        public List<PrintRequestInsuredVM> insuredList = new List<PrintRequestInsuredVM>();
    }

    public class BaseGenerateAccountStatusVM
    {
        public dynamic CONTRATANTE { get; set; }
        public dynamic DOCUMENTO { get; set; }
        public dynamic FECHA_ACTUAL { get; set; }
        public List<GenerateAccountStatusVM> clientList = new List<GenerateAccountStatusVM>();
    }

    public class GenerateAccountStatusVM
    {
        public dynamic CONTRATANTE { get; set; }
        public dynamic POLIZA { get; set; }
        public dynamic FECHA_EMISION { get; set; }
        public dynamic COMPROBANTE { get; set; }
        public dynamic FECHA_EFECTO { get; set; }
        public dynamic FECHA_FIN { get; set; }
        public dynamic MONTO_TOTAL { get; set; }

    }

    public class PrintRequestPaymentVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_MODULEC { get; set; }
        public dynamic NTASA { get; set; }
    }
}