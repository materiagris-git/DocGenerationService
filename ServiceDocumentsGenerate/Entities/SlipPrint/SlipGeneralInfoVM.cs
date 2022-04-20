using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities.SlipPrint
{
    public class SlipGeneralInfoVM
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
        public dynamic DINI_VIG { get; set; }
        public dynamic DFIN_VIG { get; set; }
        public dynamic FECHA_FIRMA { get; set; }
        public dynamic COMMENT_FINAL { get; set; }
        public dynamic SVALIDITY { get; set; }
        public dynamic SCURRENCY { get; set; }
        public dynamic NAGE_MAX_PAYROLL { get; set; }
        public dynamic NAGE_AVG_PAYROLL { get; set; }
        public dynamic SCOMISSION_BR { get; set; }
        public dynamic SWAY_PAY { get; set; }
        public dynamic SDAY_PAY { get; set; }
        public dynamic SDATE_FINAL { get; set; }
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
        public dynamic ACTECO_TIT { get; set; }
        public dynamic SCOMMERCIAL_PREMIUM_TOT { get; set; }
        public dynamic SADVISORY_BR { get; set; }
        public dynamic SFREQUENCY_PAY { get; set; }
        public dynamic SSITE_PAY { get; set; }
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
        public List<InfoRateDetVM> ratePremiumList = new List<InfoRateDetVM>();
        public List<InfoRateDetVM> ratePremiumExList = new List<InfoRateDetVM>();
        public List<InfoOverPayVM> coverOverPayList = new List<InfoOverPayVM>();
        public List<InfoRateDetVM> ratePremiumRemExList = new List<InfoRateDetVM>();
        public List<InfoBenefitQuotationVM> benefitList = new List<InfoBenefitQuotationVM>();
        public List<InfoAssistanceQuotationVM> assistanceList = new List<InfoAssistanceQuotationVM>();
        public List<InfoServicesQuotationVM> adicionalServicesList = new List<InfoServicesQuotationVM>();
        public List<ExclusionVM> exclusionList = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList1 = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList2 = new List<ExclusionVM>();
        public List<ExclusionVM> exclusionList3 = new List<ExclusionVM>();
        public BaseGenerateAccountStatusVM baseAccountStatusVM = new BaseGenerateAccountStatusVM();
    }

    public class InfoInsuredQuotationVM
    {
        public dynamic DET_MODULEC { get; set; }
        public dynamic NNUM_TRABAJADORES { get; set; }
        public dynamic TOT_PLANILLA_MENSUAL { get; set; }
        public dynamic SMONTH_PAYROLL_T { get; set; }
        public dynamic SMONTH_PAYROLL_E { get; set; }
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

    public class InfoAccountQuotationVM
    {
        public dynamic SNRO_ACC { get; set; }
    }

    public class InfoGlossExclusiveCoQuotationVM : BaseFormatVM
    {
        public dynamic SGLOSA_EXCLU_CO { get; set; }
    }

    public class InfoCoverCoQuotationVM : BaseFormatVM
    {
        public dynamic SDESCRIPT_COVER_CO { get; set; }
        public dynamic SDESCRIPT_CAPITAL_CO { get; set; }
        public dynamic NAGE_MAX_CO_CO { get; set; }
    }

    public class InfoGlossExclusiveAdQuotationVM : BaseFormatVM
    {
        public dynamic SGLOSA_EXCLU_AD { get; set; }
    }

    public class InfoGlossProtectaQuotationVM
    {
        public dynamic SGLOSA_PROTECTA { get; set; }
    }

    public class InfoRateDetVM
    {
        public dynamic DET_MODULEC { get; set; }
        public dynamic NCOMMERCIAL_RATE { get; set; }
        public dynamic SCOMMERCIAL_PREMIUM { get; set; }
        public dynamic SCOMMERCIAL_PREMIUM_TIGV { get; set; }
    }

    public class InfoOverPayVM
    {
        public dynamic SDESCRIPT_COVER_EX { get; set; }
        public dynamic SDESCRIPT_CAPITAL_EX { get; set; }
    }

    public class InfoBenefitQuotationVM
    {
        public dynamic SDESCRIPT_BENEF { get; set; }
        public dynamic SDESCRIPT_BENEF_PORC { get; set; }
        public dynamic BENEF_PORCAMOUNT { get; set; }
    }

    public class InfoAssistanceQuotationVM
    {
        public dynamic SDESCRIPT_ASSIT { get; set; }
        public dynamic DEDUC_ASSIT { get; set; }
        public dynamic PORCEN_COVER_ASSIT { get; set; }
        public dynamic EVENTS_ASSIT { get; set; }
        public dynamic COD_URL { get; set; }
    }

    public class InfoServicesQuotationVM
    {
        public dynamic SDESC_SURCHARGE { get; set; }
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
}
