using Oracle.ManagedDataAccess.Client;
using ServiceDocumentsGenerate.Entities;
using ServiceDocumentsGenerate.Entities.PolicyPrint;
using ServiceDocumentsGenerate.Entities.SlipPrint;
using ServiceDocumentsGenerate.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Repositories
{
    public class PolicyGenericDA : GenericMethods
    {
        public PrintResponseVM RequestPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "RequestPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                if (new string[] { "REQUEST_COVER_ALL", "CERTIFICATE" }.Contains(item.SNAME_SP))
                                {
                                    cmd.Parameters.Add("P_NCERTIF", OracleDbType.Int64).Value = 1;
                                }
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.requestVM = reader.ReadRowsList<PrintRequestVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 1)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.requestVM.insuredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 2)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestPaymentVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.requestVM.paymentList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 3)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.requestVM.coverList.Add(obj);
                                        }

                                        string codRamo = Convert.ToString(generatePolicy.NBRANCH);
                                        if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                        {
                                            var res = generateCoverList(genericView.requestVM.coverList);
                                            genericView.requestVM.coverList2 = res.coverNotSubItemList;
                                            genericView.requestVM.coverList3 = res.coverMainList;
                                            genericView.requestVM.coverList4 = res.coverAditionalList;
                                            genericView.requestVM.coverList5 = res.coverMainNotSubItemList;
                                            genericView.requestVM.coverList6 = res.coverAditionalNotSubItemList;
                                            genericView.requestVM.coverList7 = res.coverLimitZeroList;
                                            genericView.requestVM.coverList8 = res.coverClauseList;
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 4)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestCoverComplement>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.requestVM.coverComplementList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 5)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestCoverAdditional>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.requestVM.coverAdditionalList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 6)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestGlossExclusiveCoVM>())
                                        {
                                            if (!string.IsNullOrEmpty(obj.SGLOSA_EXCLU_CO))
                                            {
                                                obj.NITEM = getCorrelativeAlphabet() + ". ";
                                            }
                                            genericView.requestVM.glossExclusiveCoList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 7)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestGlossExclusiveAdVM>())
                                        {
                                            if (!string.IsNullOrEmpty(obj.SGLOSA_EXCLU_AD))
                                            {
                                                obj.NITEM = getCorrelativeAlphabet() + ". ";
                                            }
                                            genericView.requestVM.glossExclusiveAdList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 8)
                                    {
                                        foreach (var obj in reader.ReadRowsList<PrintRequestGlossProtectaVM>())
                                        {
                                            genericView.requestVM.glossProtectaList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 9)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoInsuredDetailVM>())
                                        {
                                            genericView.requestVM.infoInsuredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 10)
                                    {
                                        genericView.requestVM.certificateVM = reader.ReadRowsList<CertificateVM>()[0];
                                        continue;
                                    }
                                    if (item.NORDER == 11)
                                    {
                                        foreach (var obj in reader.ReadRowsList<CertificateGlossProtectaVM>())
                                        {
                                            genericView.requestVM.glossProtectaList1.Add(obj);
                                            genericView.requestVM.glossProtectaList2.Add(obj);
                                        }
                                        continue;
                                    }
                                    if (item.NORDER == 12)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                        {
                                            genericView.requestVM.coverList1.Add(obj);
                                        }

                                        string codRamo = Convert.ToString(generatePolicy.NBRANCH);
                                        if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                        {
                                            var res = generateCoverList(genericView.requestVM.coverList1);
                                            genericView.requestVM.coverList2 = res.coverNotSubItemList;
                                            genericView.requestVM.coverList3 = res.coverMainList;
                                            genericView.requestVM.coverList4 = res.coverAditionalList;
                                            genericView.requestVM.coverList5 = res.coverMainNotSubItemList;
                                            genericView.requestVM.coverList6 = res.coverAditionalNotSubItemList;
                                            genericView.requestVM.coverList7 = res.coverLimitZeroList;
                                            genericView.requestVM.coverList8 = res.coverClauseList;
                                        }

                                        continue;
                                    }

                                    if (item.NORDER == 13)
                                    {
                                        genericView.requestVM.glossOthersList = new List<InfoGlosOtherVM>();
                                        foreach (var obj in reader.ReadRowsList<InfoGlosOtherVM>())
                                        {
                                            genericView.requestVM.glossOthersList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 14)
                                    {
                                        genericView.requestVM.benefitList = new List<CertificateBenefits>();
                                        foreach (var obj in reader.ReadRowsList<CertificateBenefits>())
                                        {
                                            genericView.requestVM.benefitList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 15)
                                    {
                                        genericView.requestVM.exclusionList = new List<ExclusionVM>();
                                        foreach (var obj in reader.ReadRowsList<ExclusionVM>())
                                        {
                                            genericView.requestVM.exclusionList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 16)
                                    {
                                        genericView.requestVM.glossOthersComerList = new List<InfoGlossOtherVM>();
                                        foreach (var obj in reader.ReadRowsList<InfoGlossOtherVM>())
                                        {
                                            genericView.requestVM.glossOthersComerList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM ParticularConditionsPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "ParticularConditionsPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                if (new string[] { "REQUEST_COVER_ALL", "REQUEST_COVER_REQUIRED", "REQUEST_COVER_OPTIONAL" }.Contains(item.SNAME_SP))
                                {
                                    cmd.Parameters.Add("P_NCERTIF", OracleDbType.Int64).Value = 1;
                                }
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.particularConditionsVM = reader.ReadRowsList<ParticularConditionsVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 1)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsModuleVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.particularConditionsVM.moduleList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 2)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.particularConditionsVM.coverList.Add(obj);
                                        }

                                        string codRamo = Convert.ToString(generatePolicy.NBRANCH);
                                        if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                        {
                                            var res = generateCoverList(genericView.particularConditionsVM.coverList);
                                            genericView.particularConditionsVM.coverList2 = res.coverNotSubItemList;
                                            genericView.particularConditionsVM.coverList3 = res.coverMainList;
                                            genericView.particularConditionsVM.coverList4 = res.coverAditionalList;
                                            genericView.particularConditionsVM.coverList5 = res.coverMainNotSubItemList;
                                            genericView.particularConditionsVM.coverList6 = res.coverAditionalNotSubItemList;
                                            genericView.particularConditionsVM.coverList7 = res.coverLimitZeroList;
                                            genericView.particularConditionsVM.coverList8 = res.coverClauseList;
                                        }

                                        continue;
                                    }

                                    if (item.NORDER == 3)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsCoverCoVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.particularConditionsVM.coverComplementList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 4)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsCoverAdVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.particularConditionsVM.coverAdditionalList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 5)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsPremiumRateCoVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.particularConditionsVM.premiumListCo.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 6)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsPremiumRateAdVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.particularConditionsVM.premiumListAd.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 7)
                                    {
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsGlossProtectaVM>())
                                        {
                                            genericView.particularConditionsVM.glossProtectaList.Add(obj);
                                            genericView.particularConditionsVM.glossProtectaList1.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 8)
                                    {
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsClaimGlossProtectaVM>())
                                        {
                                            genericView.particularConditionsVM.claimGlossProtectaList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 9)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoInsuredVM>())
                                        {
                                            genericView.particularConditionsVM.infoInsuredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 10)
                                    {
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsBenefits>())
                                        {
                                            genericView.particularConditionsVM.benefitList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 11)
                                    {
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsBenefits>())
                                        {
                                            genericView.particularConditionsVM.beneficiaryList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 12)
                                    {
                                        foreach (var obj in reader.ReadRowsList<CertificateVM>())
                                        {
                                            genericView.particularConditionsVM.infoCertificate.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 15)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsCoverVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.particularConditionsVM.coverRequiredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 16)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsCoverVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.particularConditionsVM.coverOptionalList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM GeneralConditionsPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "GeneralConditionsPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                if (new string[] { "REQUEST_COVER_ALL" }.Contains(item.SNAME_SP))
                                {
                                    cmd.Parameters.Add("P_NCERTIF", OracleDbType.Int64).Value = 1;
                                }
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.generalConditionsVM = reader.ReadRowsList<GeneralConditionsVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 7)
                                    {
                                        genericView.generalConditionsVM.glossProtectaList = new List<ParticularConditionsGlossProtectaVM>();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsGlossProtectaVM>())
                                        {
                                            genericView.generalConditionsVM.glossProtectaList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 8)
                                    {
                                        genericView.generalConditionsVM.glossOthersList = new List<InfoGlosOtherVM>();
                                        foreach (var obj in reader.ReadRowsList<InfoGlosOtherVM>())
                                        {
                                            genericView.generalConditionsVM.glossOthersList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 9)
                                    {
                                        genericView.generalConditionsVM.exclusionList = new List<ExclusionVM>();
                                        foreach (var obj in reader.ReadRowsList<ExclusionVM>())
                                        {
                                            genericView.generalConditionsVM.exclusionList.Add(obj);

                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 10)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                        {
                                            genericView.generalConditionsVM.coverList.Add(obj);
                                        }

                                        string codRamo = Convert.ToString(generatePolicy.NBRANCH);
                                        if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                        {
                                            var res = generateCoverList(genericView.generalConditionsVM.coverList);
                                            genericView.generalConditionsVM.coverList2 = res.coverNotSubItemList;
                                            genericView.generalConditionsVM.coverList3 = res.coverMainList;
                                            genericView.generalConditionsVM.coverList4 = res.coverAditionalList;
                                            genericView.generalConditionsVM.coverList5 = res.coverMainNotSubItemList;
                                            genericView.generalConditionsVM.coverList6 = res.coverAditionalNotSubItemList;
                                            genericView.generalConditionsVM.coverList7 = res.coverLimitZeroList;
                                            genericView.generalConditionsVM.coverList8 = res.coverClauseList;
                                        }

                                        continue;
                                    }

                                    if (item.NORDER == 11)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoBenefitQuotationVM>())
                                        {
                                            genericView.generalConditionsVM.benefitList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM DetailDocumentsPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "DetailDocumentsPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                if (new string[] { "REQUEST_COVER_ALL" }.Contains(item.SNAME_SP))
                                {
                                    cmd.Parameters.Add("P_NCERTIF", OracleDbType.Int64).Value = 1;
                                }
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.coverComplementAdVM = reader.ReadRowsList<CoverComplementAdVM>()[0];
                                        generatePolicy.NTYPE_POLICY = genericView.coverComplementAdVM.NTYPE_POLICY;
                                        continue;
                                    }
                                    if (item.NORDER == 1)
                                    {
                                        foreach (var obj in reader.ReadRowsList<BaseCover>())
                                        {
                                            genericView.coverComplementAdVM.CONDITION_COVER.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 2)
                                    {
                                        genericView.coverComplementAdVM.exclusionList = new List<ExclusionVM>();
                                        foreach (var obj in reader.ReadRowsList<ExclusionVM>())
                                        {
                                            genericView.coverComplementAdVM.exclusionList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 3)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                        {
                                            //obj.NITEM = util.getCorrelativeNumber();
                                            genericView.coverComplementAdVM.coverList.Add(obj);
                                        }

                                        string codRamo = Convert.ToString(generatePolicy.NBRANCH);
                                        if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                        {
                                            var res = generateCoverList(genericView.coverComplementAdVM.coverList);
                                            genericView.coverComplementAdVM.coverList2 = res.coverNotSubItemList;
                                            genericView.coverComplementAdVM.coverList3 = res.coverMainList;
                                            genericView.coverComplementAdVM.coverList4 = res.coverAditionalList;
                                            genericView.coverComplementAdVM.coverList5 = res.coverMainNotSubItemList;
                                            genericView.coverComplementAdVM.coverList6 = res.coverAditionalNotSubItemList;
                                            genericView.coverComplementAdVM.coverList7 = res.coverLimitZeroList;
                                            genericView.coverComplementAdVM.coverList8 = res.coverClauseList;
                                        }

                                        continue;
                                    }

                                    if (item.NORDER == 4)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoBenefitQuotationVM>())
                                        {
                                            genericView.coverComplementAdVM.benefitList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM ElectronicPolicyPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "ElectronicPolicyPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.electronicPolicyVM = reader.ReadRowsList<ElectronicPolicyVM>()[0];
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM SummaryPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "SummaryPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                if (new string[] { "REQUEST_COVER_ALL" }.Contains(item.SNAME_SP))
                                {
                                    cmd.Parameters.Add("P_NCERTIF", OracleDbType.Int64).Value = 1;
                                }
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.summaryVM = reader.ReadRowsList<SummaryVM>()[0];
                                        continue;
                                    }


                                    if (item.NORDER == 1)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                        {
                                            genericView.summaryVM.coverList.Add(obj);
                                        }

                                        string codRamo = Convert.ToString(generatePolicy.NBRANCH);
                                        if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                        {
                                            var res = generateCoverList(genericView.summaryVM.coverList);
                                            genericView.summaryVM.coverList2 = res.coverNotSubItemList;
                                            genericView.summaryVM.coverList3 = res.coverMainList;
                                            genericView.summaryVM.coverList4 = res.coverAditionalList;
                                            genericView.summaryVM.coverList5 = res.coverMainNotSubItemList;
                                            genericView.summaryVM.coverList6 = res.coverAditionalNotSubItemList;
                                            genericView.summaryVM.coverList7 = res.coverLimitZeroList;
                                            genericView.summaryVM.coverList8 = res.coverClauseList;
                                        }

                                        continue;
                                    }

                                    if (item.NORDER == 2)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<SummaryCoverCoVM>())
                                        {
                                            genericView.summaryVM.coverComplementList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 3)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<SummaryGlossProtectaVM>())
                                        {
                                            genericView.summaryVM.glossProtectaList.Add(obj);
                                            genericView.summaryVM.glossProtectaList1.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 4)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<SummaryClaimGlossProtectaVM>())
                                        {
                                            genericView.summaryVM.claimGlossProtectaList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 5)
                                    {
                                        genericView.summaryVM.glossOtherList = new List<InfoGlossOtherVM>();
                                        foreach (var obj in reader.ReadRowsList<InfoGlossOtherVM>())
                                        {
                                            genericView.summaryVM.glossOtherList.Add(obj);
                                        }
                                        continue;
                                    }
                                    if (item.NORDER == 6)
                                    {
                                        foreach (var obj in reader.ReadRowsList<BaseCover>())
                                        {
                                            genericView.summaryVM.CONDITION_COVER.Add(obj);
                                        }
                                        continue;
                                    }
                                    if (item.NORDER == 8)
                                    {
                                        genericView.summaryVM.glossOthersList = new List<InfoGlossOtherVM>();
                                        foreach (var obj in reader.ReadRowsList<InfoGlossOtherVM>())
                                        {
                                            genericView.summaryVM.glossOthersList.Add(obj);
                                        }
                                        continue;
                                    }
                                    if (item.NORDER == 9)
                                    {
                                        genericView.summaryVM.glossOthersComerList = new List<InfoGlossOtherVM>();
                                        foreach (var obj in reader.ReadRowsList<InfoGlossOtherVM>())
                                        {
                                            genericView.summaryVM.glossOthersComerList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 10)
                                    {
                                        genericView.summaryVM.exclusionList = new List<ExclusionVM>();
                                        foreach (var obj in reader.ReadRowsList<ExclusionVM>())
                                        {
                                            genericView.summaryVM.exclusionList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM InsuredProofPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "InsuredProofPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.insuredProofVM = reader.ReadRowsList<InsuredProofVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 1)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InsuredProofClientVM>())
                                        {
                                            genericView.insuredProofVM.clientList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM provisionalRecordPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "provisionalRecordPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.provisionalRecordVM = reader.ReadRowsList<ProvisionalRecordVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 1)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<InsuredProofClientVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.provisionalRecordVM.clientList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM EndorsementPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "EndorsementPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.endorsementVM = reader.ReadRowsList<EndorsementVM>()[0];
                                        continue;
                                    }


                                    if (item.NORDER == 1)
                                    {
                                        genericView.endorsementVM.requestVM = reader.ReadRowsList<PrintRequestVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 2)
                                    {
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            //genericView.endorsementVM.requestVM = new PrintRequestVM();
                                            genericView.endorsementVM.requestVM.insuredList.Add(obj);
                                        }
                                        continue;
                                    }
                                    if (genericView.endorsementVM.particularConditionsVM == null)
                                        genericView.endorsementVM.particularConditionsVM = new ParticularConditionsVM();

                                    if (item.NORDER == 3)
                                    {
                                        genericView.endorsementVM.particularConditionsVM = reader.ReadRowsList<ParticularConditionsVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 4)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsPremiumRateCoVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.endorsementVM.particularConditionsVM.premiumListCo.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 5)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsPremiumRateAdVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.endorsementVM.particularConditionsVM.premiumListAd.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 6)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            obj.N = util.getCorrelativeNumber();
                                            genericView.endorsementVM.insuredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 7)
                                    {
                                        genericView.endorsementVM.requestVM = new PrintRequestVM();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            //genericView.endorsementVM.requestVM = new PrintRequestVM();
                                            genericView.endorsementVM.requestVM.insuredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 8)
                                    {
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            //genericView.endorsementVM.requestVM = new PrintRequestVM();
                                            genericView.endorsementVM.requestVM.insuredListMod.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM GeneratePDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "GeneratePDF: " };
            var genericView = new PolicyGeneralInfoVM();

            genericView.generalConditionsVM = new GeneralConditionsVM();
            genericView.generalConditionsVM.NPOLICY = generatePolicy.NPOLICY;

            try
            {
                response = policyFormat(generatePolicy, pathsList, genericView);
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM ProofExcludePDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "ProofExcludePDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.requestVM = reader.ReadRowsList<PrintRequestVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 1)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            obj.N = util.getCorrelativeNumber();
                                            genericView.requestVM.insuredList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + ex.Message + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;

                response = response.NCODE == 0 ? SendElectronicPolicy(generatePolicy, generatePolicy.NORDER.ToString().PadLeft(2, '0') + "_" + generatePolicy.SCONDICIONADO + ".pdf") : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM CoverExcPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "CoverExcPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        var list = reader.ReadRowsList<CoverComplementAdVM>();
                                        genericView.coverComplementAdVM = list.Count > 0 ? list[0] : null;
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                if (genericView.coverComplementAdVM != null)
                {
                    response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
                }
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM EndosoPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "EndosoPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.endorsementVM = reader.ReadRowsList<EndorsementVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 1)
                                    {
                                        genericView.endorsementVM.requestVM = new PrintRequestVM();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            genericView.endorsementVM.requestVM.insuredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 2)
                                    {
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            genericView.endorsementVM.requestVM.insuredListMod.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM EndorsementPolicyPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "EndorsementPolicyPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.endorsementVM.requestVM = new PrintRequestVM();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            genericView.endorsementVM.requestVM.insuredList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                genericView.endorsementVM.NPOLICY = response.NCODE == 0 ? generatePolicy.NPOLICY : 0;
                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM EndorsementModifPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "EndorsementModifPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView.endorsementVM = reader.ReadRowsList<EndorsementVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 1)
                                    {
                                        genericView.endorsementVM.requestVM = new PrintRequestVM();
                                        foreach (var obj in reader.ReadRowsList<PrintRequestInsuredVM>())
                                        {
                                            genericView.endorsementVM.requestVM.insuredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 2)
                                    {
                                        genericView.endorsementVM.particularConditionsVM = new ParticularConditionsVM();
                                        genericView.endorsementVM.particularConditionsVM = reader.ReadRowsList<ParticularConditionsVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 3)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsPremiumRateCoVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.endorsementVM.particularConditionsVM.premiumListCo.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 4)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<ParticularConditionsPremiumRateCoVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.endorsementVM.particularConditionsVM.premiumListCoMod.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM AnexosPDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "AnexosPDF: " };
            var genericView = new PolicyGeneralInfoVM();
            //string spError = string.Empty;

            try
            {
                foreach (var item in generatePolicy.PROCEDURE_LIST)
                {
                    using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            try
                            {
                                cmd.Connection = cn;
                                IDataReader reader = null;
                                cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();
                                //spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        foreach (var obj in reader.ReadRowsList<AnexoPDF>())
                                        {
                                            genericView.requestVM.anexosList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }


                if (genericView.requestVM.anexosList.Count > 0)
                {
                    foreach (var item in genericView.requestVM.anexosList)
                    {
                        downloadAnexoPDF(item, pathsList, generatePolicy);
                    }
                }
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public PrintResponseVM CertificatePDF(PolicyPrintVM generatePolicy, SlipPathVM pathsList, string msjUpdate)
        {
            var response = new PrintResponseVM() { NCODE = 0, SMESSAGE = "CertificatePDF: " };
            var genericView = new PolicyGeneralInfoVM();
            int index = 1;
            //string spError = string.Empty;

            try
            {
                var certificatesList = GetCertificateList(generatePolicy);

                if (certificatesList.Count() > 0)
                {
                    foreach (var certificate in certificatesList)
                    {
                        foreach (var item in generatePolicy.PROCEDURE_LIST)
                        {
                            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                            {
                                using (OracleCommand cmd = new OracleCommand())
                                {
                                    try
                                    {
                                        cmd.Connection = cn;
                                        IDataReader reader = null;
                                        cmd.CommandText = GenericProcedures.pkg_Condicionados + "." + item.SNAME_SP;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                                        cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                                        cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                                        cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                                        if (item.NORDER == 0)
                                        {
                                            cmd.Parameters.Add("P_NCERTIF", OracleDbType.Double).Value = certificate.NCERTIF;
                                        }

                                        if (new string[] { "REQUEST_COVER_ALL" }.Contains(item.SNAME_SP))
                                        {
                                            cmd.Parameters.Add("P_NCERTIF", OracleDbType.Double).Value = 1;
                                        }

                                        cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                                        cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                        cn.Open();
                                        reader = cmd.ExecuteReader();
                                        //spError = item.SNAME_SP;

                                        if (reader != null)
                                        {
                                            if (item.NORDER == 0)
                                            {
                                                genericView.certificateVM = reader.ReadRowsList<CertificateVM>()[0];
                                                continue;
                                            }

                                            if (item.NORDER == 1)
                                            {
                                                PrintGenerateUtil util = new PrintGenerateUtil();
                                                foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                                {
                                                    obj.NITEM = util.getCorrelativeNumber();
                                                    genericView.certificateVM.coverList.Add(obj);
                                                    genericView.certificateVM.coverList2.Add(obj);
                                                }

                                                string codRamo = Convert.ToString(generatePolicy.NBRANCH);
                                                if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                                {
                                                    var res = generateCoverList(genericView.certificateVM.coverList);
                                                    genericView.certificateVM.coverList2 = res.coverNotSubItemList;
                                                    genericView.certificateVM.coverList3 = res.coverMainList;
                                                    genericView.certificateVM.coverList4 = res.coverAditionalList;
                                                    genericView.certificateVM.coverList5 = res.coverMainNotSubItemList;
                                                    genericView.certificateVM.coverList6 = res.coverAditionalNotSubItemList;
                                                    genericView.certificateVM.coverList7 = res.coverLimitZeroList;
                                                    genericView.certificateVM.coverList8 = res.coverClauseList;
                                                }

                                                continue;
                                            }

                                            if (item.NORDER == 2)
                                            {
                                                PrintGenerateUtil util = new PrintGenerateUtil();
                                                foreach (var obj in reader.ReadRowsList<CertificateCoverCoVM>())
                                                {
                                                    obj.NITEM = util.getCorrelativeNumber();
                                                    genericView.certificateVM.coverComplementList.Add(obj);
                                                    genericView.certificateVM.coverComplementList2.Add(obj);
                                                    genericView.certificateVM.coverComplementList3.Add(obj);
                                                }
                                                continue;
                                            }

                                            if (item.NORDER == 3)
                                            {
                                                PrintGenerateUtil util = new PrintGenerateUtil();
                                                foreach (var obj in reader.ReadRowsList<CertificateCoverAdVM>())
                                                {
                                                    obj.NITEM = util.getCorrelativeNumber();
                                                    genericView.certificateVM.coverAdditionalList.Add(obj);
                                                    genericView.certificateVM.coverAdditionalList2.Add(obj);
                                                }
                                                continue;
                                            }

                                            if (item.NORDER == 4)
                                            {
                                                foreach (var obj in reader.ReadRowsList<CertificateGlossProtectaVM>())
                                                {
                                                    genericView.certificateVM.glossProtectaList.Add(obj);
                                                    genericView.certificateVM.glossProtectaList1.Add(obj);
                                                }
                                                continue;
                                            }

                                            if (item.NORDER == 5)
                                            {
                                                foreach (var obj in reader.ReadRowsList<CertificateClaimGlossProtectaVM>())
                                                {
                                                    genericView.certificateVM.claimGlossProtectaList.Add(obj);
                                                }
                                                if (generatePolicy.NBRANCH == Convert.ToInt64(GetValueConfig("vidaLeyBranch")))
                                                {
                                                    continue;
                                                }
                                            }

                                            if (item.NORDER == 6)
                                            {
                                                PrintGenerateUtil util = new PrintGenerateUtil();
                                                foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                                {
                                                    genericView.certificateVM.coverList.Add(obj);
                                                }

                                                string codRamo = Convert.ToString(generatePolicy.NBRANCH);
                                                if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                                {
                                                    var res = generateCoverList(genericView.certificateVM.coverList);
                                                    genericView.certificateVM.coverList2 = res.coverNotSubItemList;
                                                    genericView.certificateVM.coverList3 = res.coverMainList;
                                                    genericView.certificateVM.coverList4 = res.coverAditionalList;
                                                    genericView.certificateVM.coverList5 = res.coverMainNotSubItemList;
                                                    genericView.certificateVM.coverList6 = res.coverAditionalNotSubItemList;
                                                    genericView.certificateVM.coverList7 = res.coverLimitZeroList;
                                                    genericView.certificateVM.coverList8 = res.coverClauseList;
                                                }

                                                if (new string[] { GetValueConfig("accperBranch") }.Contains(codRamo))
                                                {
                                                    continue;
                                                }
                                            }

                                            if (item.NORDER == 7)
                                            {
                                                foreach (var obj in reader.ReadRowsList<CertificateCoverAdVM>())
                                                {
                                                    genericView.certificateVM.coverAdditionalList.Add(obj);
                                                }
                                            }

                                            if (item.NORDER == 8)
                                            {
                                                genericView.certificateVM.glossOthersList = new List<InfoGlosOtherVM>();
                                                foreach (var obj in reader.ReadRowsList<InfoGlosOtherVM>())
                                                {
                                                    genericView.certificateVM.glossOthersList.Add(obj);
                                                }
                                                continue;
                                            }

                                            if (item.NORDER == 9)
                                            {
                                                foreach (var obj in reader.ReadRowsList<BaseCover>())
                                                {
                                                    genericView.certificateVM.CONDITION_COVER.Add(obj);
                                                }
                                            }

                                            if (item.NORDER == 10)
                                            {
                                                genericView.certificateVM.glossOthersComerList = new List<InfoGlosOtherVM>();
                                                foreach (var obj in reader.ReadRowsList<InfoGlosOtherVM>())
                                                {
                                                    genericView.certificateVM.glossOthersComerList.Add(obj);
                                                }
                                                continue;
                                            }

                                            if (item.NORDER == 11)
                                            {
                                                genericView.certificateVM.glossOtherList = new List<InfoGlosOtherVM>();
                                                foreach (var obj in reader.ReadRowsList<InfoGlosOtherVM>())
                                                {
                                                    genericView.certificateVM.glossOtherList.Add(obj);
                                                }
                                                continue;
                                            }

                                            if (item.NORDER == 12)
                                            {
                                                genericView.certificateVM.benefitList = new List<CertificateBenefits>();
                                                foreach (var obj in reader.ReadRowsList<CertificateBenefits>())
                                                {
                                                    genericView.certificateVM.benefitList.Add(obj);
                                                }

                                                genericView.certificateVM.benefitList = new List<CertificateBenefits>(genericView.certificateVM.benefitList.Where(x => !String.IsNullOrEmpty(x.COD_URL)));

                                                continue;
                                            }

                                            if (item.NORDER == 13)
                                            {
                                                genericView.certificateVM.exclusionList = new List<ExclusionVM>();
                                                foreach (var obj in reader.ReadRowsList<ExclusionVM>())
                                                {
                                                    genericView.certificateVM.exclusionList.Add(obj);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        response.NCODE = 1;
                                        response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " | SP: " + item.SNAME_SP + " || ";
                                    }
                                    finally
                                    {
                                        if (cn.State == ConnectionState.Open) cn.Close();
                                    }
                                }
                            }
                        }

                        generateObjUpdateState(generatePolicy, 1,
                                               Convert.ToInt32(PrintEnum.State.EN_PROCESO),
                                               msjUpdate + " | Estamos en el " + generatePolicy.SCONDICIONADO + " " + index + " de " + certificatesList.Count());

                        response = response.NCODE == 0 ? policyFormat(generatePolicy, pathsList, genericView) : response;
                        index++;
                    }

                    response = response.NCODE == 0 ? generarCuadroPoliza(generatePolicy, response, msjUpdate) : response;
                }
                else
                {
                    string pathTempPDF = string.Empty;

                    if (generatePolicy.NTRANSAC == "E")
                    {
                        pathTempPDF = pathsList.SRUTA_DESTINO + generatePolicy.NPOLICY + "\\" + generatePolicy.NIDHEADERPROC + "\\" + generatePolicy.STRANSAC + "\\";
                    }
                    else
                    {
                        pathTempPDF = pathsList.SRUTA_DESTINO + generatePolicy.NPOLICY + "\\" + generatePolicy.NIDHEADERPROC + "\\" + generatePolicy.STRANSAC + "\\" + generatePolicy.DSTARTDATE + "\\";
                    }

                    var paths = new PrintResponseVM()
                    {
                        NCODE = 0,
                        PATH_PDF = pathTempPDF,
                        PATH_PDF_CERTIF = null
                    };

                    response = generarCuadroPoliza(generatePolicy, paths, msjUpdate);
                }

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = response.SMESSAGE + " | Condicionado " + generatePolicy.SCONDICIONADO + "(" + generatePolicy.NCOD_CONDICIONADO + ") | Error: " + ex.Message + " || ";
            }

            return response;
        }

        public List<CertificateVM> GetCertificateList(PolicyPrintVM generatePolicy)
        {
            var certificatesList = new List<CertificateVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_Condicionados + ".LISTA_CERTIFICADOS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = generatePolicy.NIDHEADERPROC;
                        cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = generatePolicy.NIDDETAILPROC;
                        cmd.Parameters.Add("P_SCERTYPE", OracleDbType.Double).Value = generatePolicy.SCERTYPE;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = generatePolicy.NBRANCH;
                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = generatePolicy.NPRODUCT;
                        cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                        cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = generatePolicy.NMOVEMENT;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            certificatesList = reader.ReadRowsList<CertificateVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        certificatesList = new List<CertificateVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return certificatesList;
        }

        public PrintResponseVM generarCuadroPoliza(PolicyPrintVM printGenerateBM, PrintResponseVM paths, string msjUpdate)
        {
            var response = new PrintResponseVM() { NCODE = 0 };
            var pathCertificatePDF = paths.PATH_PDF;
            var formatOrderVM = GetFormatLastOrder(printGenerateBM);

            try
            {
                if (formatOrderVM[0].NORDER == printGenerateBM.NORDER)
                {
                    generateObjUpdateState(printGenerateBM, 1,
                                               Convert.ToInt32(PrintEnum.State.EN_PROCESO),
                                               msjUpdate + " | Estamos generando el cuadro de póliza");


                    var NoProcesar = new string[] { "11863", "11867", "11868", "12257", "12076", "11865", "12253", "11869", "11864", "12078", "12252", "11870", "12077", "11871", "11866", "12258" }.ToList();
                    bool procesar = true;


                    procesar = NoProcesar.Contains(printGenerateBM.NIDHEADERPROC.ToString()) ? false : procesar;

                    if (procesar)
                    {
                        bool certFlag = true;
                        string codRamo = Convert.ToString(printGenerateBM.NBRANCH);

                        if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                        {
                            certFlag = GetTipoPolizaByProc(printGenerateBM);
                        }

                        var certificadoPath = certFlag ? paths.PATH_PDF_CERTIF : null;
                        response = joinDocuments(paths.PATH_PDF, certificadoPath, formatOrderVM[0].SNAME_ATTACHED_DOC, (int)printGenerateBM.NPRODUCT, (int)printGenerateBM.NBRANCH);
                    }


                    if (!string.IsNullOrEmpty(paths.PATH_PDF_CERTIF))
                    {
                        zipFiles(paths.PATH_PDF_CERTIF, paths.PATH_PDF + "\\certificados.zip");
                        Directory.Delete(paths.PATH_PDF_CERTIF, true);
                    }

                    if (paths.NCODE == 0)
                    {
                        if (procesar)
                        {
                            response = SendElectronicPolicy(printGenerateBM, formatOrderVM[0].SNAME_ATTACHED_DOC);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = "generarCuadroPoliza Error: " + ex.Message;
            }

            return response;
        }

        public PrintResponseVM SendElectronicPolicy(PolicyPrintVM printGenerateBM, string nameAttached)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            var baseDataQuotation = GetCotizacionByProc(printGenerateBM);
            response = sendDocumentsService(printGenerateBM, baseDataQuotation, new PolicyPrintDA().GetUsersPolicyList(printGenerateBM), nameAttached);

            response = generateObjUpdateState(printGenerateBM, 2,
                                                response.NCODE == 1 ? Convert.ToInt32(PrintEnum.State.ERROR_PE) : Convert.ToInt32(PrintEnum.State.CORRECTO),
                                                response.SMESSAGE);
            response.SEND_PE = true;

            return response;
        }

        public bool GetTipoPolizaByProc(PolicyPrintVM generatePolicy)
        {
            var response = true;
            var ntypePolicy = 0;

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".GET_TYPE_POLICY_BY_HPROC";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Int32).Value = generatePolicy.NIDHEADERPROC;
                        var P_NFLAG = new OracleParameter("P_NTYPE_POLICY", OracleDbType.Int32, ParameterDirection.Output);
                        cmd.Parameters.Add(P_NFLAG);
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        ntypePolicy = Convert.ToInt32(P_NFLAG.Value.ToString());
                        response = ntypePolicy == 1 ? false : true;
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        ntypePolicy = 0;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return response;
        }

        public PolicyDataVM GetCotizacionByProc(PolicyPrintVM generatePolicy)
        {
            var response = new PolicyDataVM();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".GET_COTIZACION_BY_HPROC";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Int32).Value = generatePolicy.NIDHEADERPROC;
                        var P_NID_COTIZACION = new OracleParameter("P_NID_COTIZACION", OracleDbType.Int32, ParameterDirection.Output);
                        var P_MONTO_PLANILLA = new OracleParameter("P_MONTO_PLANILLA", OracleDbType.Double, ParameterDirection.Output);

                        P_NID_COTIZACION.Size = 200;
                        P_MONTO_PLANILLA.Size = 200;
                        cmd.Parameters.Add(P_NID_COTIZACION);
                        cmd.Parameters.Add(P_MONTO_PLANILLA);
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        response.NID_COTIZACION = Convert.ToInt32(P_NID_COTIZACION.Value.ToString());
                        response.NAMOUNT_PLANILLA = Convert.ToDouble(P_NID_COTIZACION.Value.ToString());
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        response.NID_COTIZACION = 0;
                        response.NAMOUNT_PLANILLA = 0;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return response;
        }

        public List<PolicyFormatOrderVM> GetFormatLastOrder(PolicyPrintVM request)
        {
            var formatOrderList = new List<PolicyFormatOrderVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_FORMAT_LAST_ORDER";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NTRANSAC", OracleDbType.Double).Value = request.NNTRANSAC;
                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = request.NPRODUCT;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = request.NBRANCH;
                        cmd.Parameters.Add("P_STRANSAC", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(request.NTRANSAC) ? string.Empty : request.NTRANSAC;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            formatOrderList = reader.ReadRowsList<PolicyFormatOrderVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        formatOrderList = new List<PolicyFormatOrderVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return formatOrderList;
        }

        public PrintResponseVM policyFormat(PolicyPrintVM printGenerateBM, SlipPathVM printPathsVM, PolicyGeneralInfoVM genericView)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                if (!Directory.Exists(printPathsVM.SRUTA_TEMP))
                {
                    Directory.CreateDirectory(printPathsVM.SRUTA_TEMP);
                }

                string pathNameFileTemplate = printPathsVM.SRUTA_TEMPLATE + printPathsVM.SNAME_TEMPLATE + ".docx";
                string pathTempPDF = string.Empty;
                string pathNameFileTemplateTemp = string.Empty;
                string pathNameFilePDF = string.Empty;
                string pathNameFilePDF_test = string.Empty;

                if (new int[] { (int)PrintEnum.Condicionado_Poliza.CERTIFICADO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_BASICO,
                                     (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_COMPLETO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_ESPECIAL,
                                     (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_SCTR, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_AP,
                                     (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_VG, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_CERTIFICADO_AP,
                                     (int)PrintEnum.Condicionado_Poliza.SOLICITUD_VG }.Contains((int)printGenerateBM.NCOD_CONDICIONADO))
                {
                    pathNameFileTemplateTemp = printPathsVM.SRUTA_TEMP + printPathsVM.SNAME_TEMPLATE + "_" + printGenerateBM.NPOLICY + "_" + printGenerateBM.NIDHEADERPROC + "_" + printGenerateBM.STRANSAC + "_" + genericView.certificateVM.NCERTIF + ".docx";

                    if (printGenerateBM.NTRANSAC == "E")
                    {
                        pathTempPDF = printPathsVM.SRUTA_DESTINO + printGenerateBM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\certificados\\";
                    }
                    else
                    {
                        pathTempPDF = printPathsVM.SRUTA_DESTINO + printGenerateBM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\" + printGenerateBM.DSTARTDATE + "\\certificados\\";
                    }

                    pathNameFilePDF = pathTempPDF + printGenerateBM.NORDER.ToString().PadLeft(2, '0') + "_" + printGenerateBM.SCONDICIONADO + "_" + printGenerateBM.NPOLICY + "_" + genericView.certificateVM.NCERTIF + ".pdf";
                    pathNameFilePDF_test = pathTempPDF + printGenerateBM.NORDER.ToString().PadLeft(2, '0') + "_" + printGenerateBM.SCONDICIONADO + "_" + printGenerateBM.NPOLICY + "_" + genericView.certificateVM.NCERTIF + "_test.pdf";
                }
                else
                {
                    pathNameFileTemplateTemp = printPathsVM.SRUTA_TEMP + printPathsVM.SNAME_TEMPLATE + "_" + printGenerateBM.NPOLICY + "_" + printGenerateBM.NIDHEADERPROC + "_" + printGenerateBM.STRANSAC + ".docx";

                    if (printGenerateBM.NTRANSAC == "E")
                    {
                        pathTempPDF = printPathsVM.SRUTA_DESTINO + printGenerateBM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\";

                    }
                    else
                    {
                        pathTempPDF = printPathsVM.SRUTA_DESTINO + printGenerateBM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\" + printGenerateBM.DSTARTDATE + "\\";

                    }

                    pathNameFilePDF = pathTempPDF + printGenerateBM.NORDER.ToString().PadLeft(2, '0') + "_" + printGenerateBM.SCONDICIONADO + ".pdf";
                    pathNameFilePDF_test = pathTempPDF + printGenerateBM.NORDER.ToString().PadLeft(2, '0') + "_" + printGenerateBM.SCONDICIONADO + "_test.pdf";
                }

                response = CreateDocument(pathNameFileTemplateTemp, pathNameFileTemplate, pathTempPDF, pathNameFilePDF_test, pathNameFilePDF, printGenerateBM.NCOD_CONDICIONADO, printGenerateBM.STRANSAC, genericView);

                if (new int[] { (int)PrintEnum.Condicionado_Poliza.CERTIFICADO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_BASICO,
                                (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_COMPLETO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_ESPECIAL,
                                (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_SCTR, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_AP,
                                (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_VG, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_CERTIFICADO_AP,
                                (int)PrintEnum.Condicionado_Poliza.SOLICITUD_VG }.Contains((int)printGenerateBM.NCOD_CONDICIONADO))
                {
                    if (printGenerateBM.NTRANSAC == "E")
                    {
                        pathTempPDF = printPathsVM.SRUTA_DESTINO + genericView.certificateVM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\";
                    }
                    else
                    {
                        pathTempPDF = printPathsVM.SRUTA_DESTINO + genericView.certificateVM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\" + printGenerateBM.DSTARTDATE + "\\";
                    }

                    response.PATH_PDF_CERTIF = pathTempPDF + "\\certificados\\";
                }

                response.NCODE = response.NCODE == 0 ? File.Exists(pathNameFilePDF) ? response.NCODE : 1 : response.NCODE;
                response.SMESSAGE = File.Exists(pathNameFilePDF) ? response.SMESSAGE : "El documento no se ha generado";
                response.PATH_PDF = response.NCODE == 0 ? pathTempPDF : null;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = "policyFormat Error: " + ex.Message;
            }

            return response;
        }

        private PrintResponseVM CreateDocument(string pathNameFileTemplateTemp, string pathNameFileTemplate, string pathTempPDF, string pathNameFilePDF_test, string pathNameFilePDF,
                                    dynamic NCOD_CONDICIONADO, dynamic STRANSAC, PolicyGeneralInfoVM data)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                DeleteFile(pathNameFileTemplateTemp);
                DeleteFile(pathNameFilePDF);

                File.Copy(pathNameFileTemplate, pathNameFileTemplateTemp);

                using (var factory = NGS.Templater.Configuration.Factory.Open(pathNameFileTemplateTemp))
                {
                    if (new int[] { (int)PrintEnum.Condicionado_Poliza.SOLICITUD, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_LEY,
                                    (int)PrintEnum.Condicionado_Poliza.SOLICITUD_COMPLETO, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_ESPECIAL,
                                    (int)PrintEnum.Condicionado_Poliza.COVID_SOLICITUD,
                                    (int)PrintEnum.Condicionado_Poliza.SOLICITUD_SCTR, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_SCTR_SIN_SUELDO,
                                    (int)PrintEnum.Condicionado_Poliza.SOLICITUD_AP, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_VILP }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.SOLICITUD_LEY }.Contains((int)NCOD_CONDICIONADO))
                        {
                            factory.Templater.Resize(new[] { "has_covers" }, 0);
                        }
                        else
                        {
                            factory.Templater.Replace("has_covers", null);
                        }

                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.SOLICITUD_AP }.Contains((int)NCOD_CONDICIONADO))
                        {
                            if (data.requestVM.NTYPE_POLICY == 1)
                            {
                                factory.Templater.Resize(new[] { "has_grupal" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_grupal", null);
                            }

                            if (data.requestVM.NTYPE_POLICY == 2)
                            {
                                factory.Templater.Resize(new[] { "has_individual" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_individual", null);
                            }

                            if (data.requestVM.TYPDOC_TIT == 1)
                            {
                                factory.Templater.Resize(new[] { "has_juridico" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_juridico", null);
                            }

                            if (data.requestVM.TYPDOC_TIT == 2)
                            {
                                factory.Templater.Resize(new[] { "has_natural" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_natural", null);
                            }
                        }

                        factory.Process(data.requestVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES, (int)PrintEnum.Condicionado_Poliza.COVID_CON_PARTICULAR,
                                         (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_SCTR, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_AP,
                                         (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_VG, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_VILP }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_AP, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_VG,
                                        (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_VILP }.Contains((int)NCOD_CONDICIONADO))
                        {
                            if (data.particularConditionsVM.coverList7.Count == 0)
                            {
                                data.particularConditionsVM.SLIMITE_AGREG = "NO APLICA";
                                factory.Templater.Resize(new[] { "has_limit" }, 0);
                            }
                            else
                            {
                                data.particularConditionsVM.SLIMITE_AGREG = string.Empty;
                                factory.Templater.Replace("has_limit", null);
                            }

                            if (data.particularConditionsVM.benefitList.Count == 0)
                            {
                                factory.Templater.Resize(new[] { "has_benefit" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_benefit", null);
                            }

                            if (data.particularConditionsVM.NTYPE_POLICY == 1)
                            {
                                factory.Templater.Resize(new[] { "has_grupal" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_grupal", null);
                            }

                            if (data.particularConditionsVM.NTYPE_POLICY == 2)
                            {
                                factory.Templater.Resize(new[] { "has_individual" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_individual", null);
                            }

                            if (data.particularConditionsVM.TYPDOC_TIT == 1)
                            {
                                factory.Templater.Resize(new[] { "has_juridico" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_juridico", null);
                            }

                            if (data.particularConditionsVM.TYPDOC_TIT == 2)
                            {
                                factory.Templater.Resize(new[] { "has_natural" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_natural", null);
                            }

                            if (data.particularConditionsVM.benefitList.Count == 0)
                            {
                                data.particularConditionsVM.STEXT_BENEFITS = "NO APLICA";
                            }
                            else
                            {
                                data.particularConditionsVM.STEXT_BENEFITS = string.Empty;
                            }
                        }

                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_VG }.Contains((int)NCOD_CONDICIONADO))
                        {
                            if (string.IsNullOrEmpty(data.particularConditionsVM.SNAME_COMER))
                            {
                                factory.Templater.Resize(new[] { "HAS_COMER" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("HAS_COMER", null);
                            }

                            if (string.IsNullOrEmpty(data.particularConditionsVM.SNAME_BROKER))
                            {
                                factory.Templater.Resize(new[] { "HAS_CORRE" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("HAS_CORRE", null);
                            }

                            if (string.IsNullOrEmpty(data.particularConditionsVM.NCOMMI_PROMO))
                            {
                                factory.Templater.Resize(new[] { "HAS_PROMO" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("HAS_PROMO", null);
                            }

                            if (string.IsNullOrEmpty(data.particularConditionsVM.SNAME_COMER) &&
                                string.IsNullOrEmpty(data.particularConditionsVM.SNAME_BROKER) &&
                                string.IsNullOrEmpty(data.particularConditionsVM.NCOMMI_PROMO))
                            {
                                factory.Templater.Resize(new[] { "HAS_COMISION" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("HAS_COMISION", null);
                            }

                            if (data.particularConditionsVM.NTYPE_POLICY == 1)
                            {
                                factory.Templater.Resize(new[] { "has_grupal" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_grupal", null);
                            }

                            if (data.particularConditionsVM.NTYPE_POLICY == 2)
                            {
                                factory.Templater.Resize(new[] { "has_individual" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_individual", null);
                            }

                            if (data.particularConditionsVM.TYPDOC_TIT == 1)
                            {
                                factory.Templater.Resize(new[] { "has_juridico" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_juridico", null);
                            }

                            if (data.particularConditionsVM.TYPDOC_TIT == 2)
                            {
                                factory.Templater.Resize(new[] { "has_natural" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_natural", null);
                            }
                        }

                        factory.Process(data.particularConditionsVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES, (int)PrintEnum.Condicionado_Poliza.COVID_CON_GENERAL,
                                         (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_SCTR, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_AP,
                                         (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_VG, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_VILP }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_AP, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_VG }.Contains((int)NCOD_CONDICIONADO))
                        {
                            if (data.particularConditionsVM.NTYPE_POLICY == 1)
                            {
                                factory.Templater.Resize(new[] { "has_grupal" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_grupal", null);
                            }

                            if (data.particularConditionsVM.NTYPE_POLICY == 2)
                            {
                                factory.Templater.Resize(new[] { "has_individual" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_individual", null);
                            }

                            if (data.particularConditionsVM.TYPDOC_TIT == 1)
                            {
                                factory.Templater.Resize(new[] { "has_juridico" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_juridico", null);
                            }

                            if (data.particularConditionsVM.TYPDOC_TIT == 2)
                            {
                                factory.Templater.Resize(new[] { "has_natural" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_natural", null);
                            }
                        }

                        factory.Process(data.generalConditionsVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.POLIZA_ELECTRONICA }.Contains((int)NCOD_CONDICIONADO))
                    {
                        factory.Process(data.electronicPolicyVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CLAUSULAS_ADICIONALES, (int)PrintEnum.Condicionado_Poliza.CLAUSULAS_ADICIONALES_BASICO,
                                         (int)PrintEnum.Condicionado_Poliza.CLAUSULAS_ADICIONALES_COMPLETO, (int)PrintEnum.Condicionado_Poliza.CLAUSULAS_ADICIONALES_ESPECIAL,
                                         (int)PrintEnum.Condicionado_Poliza.COVID_CON_ESPECIAL, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_ESPECIALES_AP,
                                         (int)PrintEnum.Condicionado_Poliza.CONDICIONES_ESPECIALES_VG }.Contains((int)NCOD_CONDICIONADO))
                    {

                        foreach (var item in data.coverComplementAdVM.CONDITION_COVER)
                        {
                            if (item.SFLAG == "0")
                            {
                                factory.Templater.Resize(new[] { item.SNAMEVARIABLE }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace(item.SNAMEVARIABLE, null);
                                factory.Templater.Replace(item.SNAMEVARIABLE + "_NMAXAGE", item.NMAXAGE);
                            }
                        }

                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONDICIONES_ESPECIALES_AP, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_ESPECIALES_VG }.Contains((int)NCOD_CONDICIONADO))
                        {
                            if (data.coverComplementAdVM.NTYPE_POLICY == 1)
                            {
                                factory.Templater.Resize(new[] { "has_grupal" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_grupal", null);
                            }

                            if (data.coverComplementAdVM.NTYPE_POLICY == 2)
                            {
                                factory.Templater.Resize(new[] { "has_individual" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_individual", null);
                            }

                            if (data.coverComplementAdVM.TYPDOC_TIT == 1)
                            {
                                factory.Templater.Resize(new[] { "has_juridico" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_juridico", null);
                            }

                            if (data.coverComplementAdVM.TYPDOC_TIT == 2)
                            {
                                factory.Templater.Resize(new[] { "has_natural" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_natural", null);
                            }


                            if (data.coverComplementAdVM.coverList2.Where(x => Convert.ToInt32(x.NCOVER) == 1041 || Convert.ToInt32(x.NCOVER) == 1076).ToList().Count() == 0)
                            {
                                factory.Templater.Resize(new[] { "has_gastos" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_gastos", null);
                            }
                        }

                        factory.Process(data.coverComplementAdVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.RESUMEN, (int)PrintEnum.Condicionado_Poliza.RESUMEN_BASICO,
                                         (int)PrintEnum.Condicionado_Poliza.RESUMEN_COMPLETO, (int)PrintEnum.Condicionado_Poliza.RESUMEN_ESPECIAL,
                                         (int)PrintEnum.Condicionado_Poliza.COVID_RESUMEN, (int)PrintEnum.Condicionado_Poliza.RESUMEN_SCTR,
                                         (int)PrintEnum.Condicionado_Poliza.RESUMEN_AP, (int)PrintEnum.Condicionado_Poliza.RESUMEN_VG,
                                         (int)PrintEnum.Condicionado_Poliza.RESUMEN_VILP }.Contains((int)NCOD_CONDICIONADO))
                    {
                        foreach (var item in data.summaryVM.CONDITION_COVER)
                        {
                            if (item.SFLAG == "0")
                            {
                                factory.Templater.Resize(new[] { item.SNAMEVARIABLE }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace(item.SNAMEVARIABLE, null);
                            }
                        }

                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.RESUMEN_AP, (int)PrintEnum.Condicionado_Poliza.RESUMEN_VG }.Contains((int)NCOD_CONDICIONADO))
                        {
                            if (data.summaryVM.NTYPE_POLICY == 1)
                            {
                                factory.Templater.Resize(new[] { "has_grupal" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_grupal", null);
                            }

                            if (data.summaryVM.NTYPE_POLICY == 2)
                            {
                                factory.Templater.Resize(new[] { "has_individual" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_individual", null);
                            }

                            if (data.summaryVM.TYPDOC_TIT == 1)
                            {
                                factory.Templater.Resize(new[] { "has_juridico" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_juridico", null);
                            }

                            if (data.summaryVM.TYPDOC_TIT == 2)
                            {
                                factory.Templater.Resize(new[] { "has_natural" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_natural", null);
                            }
                        }

                        factory.Process(data.summaryVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ASEGURADO, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ASEGURADO_AP,
                                     (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ASEGURADO_VG }.Contains((int)NCOD_CONDICIONADO))
                    {
                        factory.Process(data.insuredProofVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_PROVISIONAL_VL, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_PROVISIONAL_SCTR }.Contains((int)NCOD_CONDICIONADO))
                    {
                        factory.Process(data.provisionalRecordVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CERTIFICADO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_BASICO,
                                         (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_COMPLETO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_ESPECIAL,
                                         (int)PrintEnum.Condicionado_Poliza.COVID_CERTIFICADO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_SCTR,
                                         (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_AP, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_VG,
                                        (int)PrintEnum.Condicionado_Poliza.SOLICITUD_CERTIFICADO_AP, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_VG }.Contains((int)NCOD_CONDICIONADO))
                    {

                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.CERTIFICADO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_BASICO,
                                       (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_COMPLETO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_ESPECIAL }.Contains((int)NCOD_CONDICIONADO))
                        {
                            int countCoverActive = data.certificateVM.CONDITION_COVER.Count(c => c.SFLAG == "1");
                            if (countCoverActive == 0)
                            {
                                factory.Templater.Resize(new[] { "has_covers" }, 0);
                                factory.Templater.Resize(new[] { "has_covers" + "_t1" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_covers", null);
                                factory.Templater.Replace("has_covers" + "_t1", null);
                            }
                            foreach (var item in data.certificateVM.CONDITION_COVER)
                            {
                                if (item.SFLAG == "0")
                                {
                                    factory.Templater.Resize(new[] { item.SNAMEVARIABLE }, 0);
                                    factory.Templater.Resize(new[] { item.SNAMEVARIABLE + "_T1" }, 0);
                                }
                                else
                                {
                                    factory.Templater.Replace(item.SNAMEVARIABLE, null);
                                    factory.Templater.Replace(item.SNAMEVARIABLE + "_T1", null);
                                    factory.Templater.Replace(item.SNAMEVARIABLE + "_NMAXAGE", item.NMAXAGE);
                                }
                            }
                        }

                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_AP, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_VG,
                            (int)PrintEnum.Condicionado_Poliza.SOLICITUD_CERTIFICADO_AP, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_VG }.Contains((int)NCOD_CONDICIONADO))
                        {
                            if (data.certificateVM.benefitList.Count == 0)
                            {
                                data.certificateVM.STEXT_BENEFITS = "NO APLICA";
                                factory.Templater.Resize(new[] { "has_benefit" }, 0);
                            }
                            else
                            {
                                data.certificateVM.STEXT_BENEFITS = string.Empty;
                                factory.Templater.Replace("has_benefit", null);
                            }

                            if (data.certificateVM.NTYPE_POLICY == 1)
                            {
                                factory.Templater.Resize(new[] { "has_grupal" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_grupal", null);
                            }

                            if (data.certificateVM.NTYPE_POLICY == 2)
                            {
                                factory.Templater.Resize(new[] { "has_individual" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_individual", null);
                            }

                            if (string.IsNullOrEmpty(data.certificateVM.SCLIENAME_BEN1))
                            {
                                factory.Templater.Resize(new[] { "has_benefit_ase" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_benefit_ase", null);
                            }

                            if (string.IsNullOrEmpty(data.certificateVM.SNAME_COMER))
                            {
                                factory.Templater.Resize(new[] { "has_comer" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_comer", null);
                            }

                            if (string.IsNullOrEmpty(data.certificateVM.SNAME_BROKER))
                            {
                                factory.Templater.Resize(new[] { "has_corre" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_corre", null);
                            }

                            if (string.IsNullOrEmpty(data.certificateVM.NCOMMI_PROMO))
                            {
                                factory.Templater.Resize(new[] { "has_promo" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_promo", null);
                            }

                            if (string.IsNullOrEmpty(data.certificateVM.SNAME_COMER) &&
                                string.IsNullOrEmpty(data.certificateVM.SNAME_BROKER) &&
                                string.IsNullOrEmpty(data.certificateVM.NCOMMI_PROMO))
                            {
                                factory.Templater.Resize(new[] { "has_comision" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_comision", null);
                            }

                            if (data.certificateVM.TYPDOC_TIT == 1)
                            {
                                factory.Templater.Resize(new[] { "has_juridico" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_juridico", null);
                            }

                            if (data.certificateVM.TYPDOC_TIT == 2)
                            {
                                factory.Templater.Resize(new[] { "has_natural" }, 0);
                            }
                            else
                            {
                                factory.Templater.Replace("has_natural", null);
                            }
                        }

                        factory.Process(data.certificateVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ENDORSEMENT, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_INCLUSION_AP,
                                        (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_INCLUSION_VIAJES_AP, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_DECLARACION_AP,
                                        (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_MODIFICACION_AP, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_EXCLUSION_AP,
                                        (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ANULACION_AP, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_EXCLUSION_VG,
                                        (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_INCLUSION_VG, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_DECLARACION_VG,
                                        (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ANULACION_VG, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_MODIFICACION_VG }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_INCLUSION_AP, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_INCLUSION_VIAJES_AP,
                                        (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_DECLARACION_AP }.Contains((int)NCOD_CONDICIONADO))
                        {
                            foreach (var item in data.endorsementVM.particularConditionsVM.premiumListCo)
                            {
                                if (item.FLAG == "1")
                                {
                                    factory.Templater.Resize(new[] { "GP1" }, 0);
                                    factory.Templater.Resize(new[] { "GP2" }, 0);
                                    factory.Templater.Replace("IN", null);

                                }
                                else
                                {
                                    factory.Templater.Resize(new[] { "IN" }, 0);
                                    factory.Templater.Replace("GP1", null);
                                    factory.Templater.Replace("GP2", null);
                                }
                            }
                        }

                        if (data.endorsementVM.NTYPE_POLICY == 1)
                        {
                            factory.Templater.Resize(new[] { "has_grupal" }, 0);
                        }
                        else
                        {
                            factory.Templater.Replace("has_grupal", null);
                        }

                        if (data.endorsementVM.NTYPE_POLICY == 2)
                        {
                            factory.Templater.Resize(new[] { "has_individual" }, 0);
                        }
                        else
                        {
                            factory.Templater.Replace("has_individual", null);
                        }

                        factory.Process(data.endorsementVM);
                    }

                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.EECC }.Contains((int)NCOD_CONDICIONADO))
                    {
                        factory.Process(data.baseAccountStatusVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.EXCLUSION_VL }.Contains((int)NCOD_CONDICIONADO))
                        factory.Process(data.requestVM);
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ANULACION_AP }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (data.cancellationVM.FLAG == 1)
                        {
                            factory.Templater.Resize(new[] { "p1" }, 0);
                            factory.Templater.Replace("p2", null);
                        }
                        else
                        {
                            factory.Templater.Resize(new[] { "p2" }, 0);
                            factory.Templater.Replace("p1", null);
                        }
                        factory.Process(data.cancellationVM);
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.COBERTURA_EXCEDENTE }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(data.coverComplementAdVM.NAGEMAXPERM)))
                        {
                            factory.Process(data.coverComplementAdVM);
                        }

                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ENDOSO }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (data.endorsementVM.requestVM.insuredList.Count() > 0)
                        {
                            factory.Process(data.endorsementVM);
                        }
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ENDOSO_POLIZA }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (data.endorsementVM.requestVM.insuredList.Count() > 0)
                        {
                            factory.Process(data.endorsementVM);
                        }
                    }
                    else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ENDOSO_MODIFIC_REM }.Contains((int)NCOD_CONDICIONADO))
                    {
                        if (data.endorsementVM.requestVM.insuredList.Count() > 0)
                        {
                            factory.Process(data.endorsementVM);
                        }
                    }
                }

                response = RemoveAdvertising(pathNameFileTemplateTemp);
                response = response.NCODE == 0 ? ConvertWordToPdf(pathTempPDF, pathNameFileTemplateTemp, pathNameFilePDF) : response;
                //response = response.NCODE == 0 ? DeletePages(pathNameFilePDF_test, pathNameFilePDF) : response;
                DeleteFile(pathNameFileTemplateTemp);

                response.NCODE = response.NCODE;
                response.SMESSAGE = response.NCODE == 0 ? "Se realizó de forma correcta el PDF" : response.SMESSAGE;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = "createDocument Error: " + ex.Message;
            }

            return response;
        }
    }
}
