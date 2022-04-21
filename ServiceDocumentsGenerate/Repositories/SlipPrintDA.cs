using Oracle.ManagedDataAccess.Client;
using ServiceDocumentsGenerate.Entities;
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
    public class SlipPrintDA : GenericMethods
    {
        #region Inicio Zona Genérica
        public List<SlipJobVM> GetJobList()
        {
            var jobsList = new List<SlipJobVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_JOB_QUOTATION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            jobsList = reader.ReadRowsList<SlipJobVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        jobsList = new List<SlipJobVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return jobsList;
        }

        public int GetStateProcess(SlipJobVM job)
        {
            var stateDoc = 0;

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".VAL_DOC_QUOTATION_STATE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Int64).Value = job.NID_COTIZACION;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        var P_NSATE_DOC = new OracleParameter("P_NSATE_DOC", OracleDbType.Int32, ParameterDirection.Output);
                        cmd.Parameters.Add(P_NSATE_DOC);
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        stateDoc = Convert.ToInt32(P_NSATE_DOC.Value.ToString());
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        stateDoc = 0;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return stateDoc;
        }

        public List<SlipFormatVM> GetFormatList(SlipJobVM job)
        {
            var formatList = new List<SlipFormatVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_FORMATOS_QUOTATION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Int32).Value = job.NBRANCH;
                        cmd.Parameters.Add("P_NID_COTIZACION", OracleDbType.Int32).Value = job.NID_COTIZACION;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            formatList = reader.ReadRowsList<SlipFormatVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        formatList = new List<SlipFormatVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return formatList;
        }

        public List<SlipProcedureVM> GetProcedureList(int ncod_condicionado)
        {
            var procedureList = new List<SlipProcedureVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_PROCEDURES_QUOTATION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NCOD_CONDICIONADO", OracleDbType.Int32).Value = ncod_condicionado;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            procedureList = reader.ReadRowsList<SlipProcedureVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        procedureList = new List<SlipProcedureVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return procedureList;
        }

        public List<SlipPathVM> GetPathsList(dynamic ncod_condicionado, int nidheaderproc = 0)
        {
            var pathsList = new List<SlipPathVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_PATHS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NCOD_CONDICIONADO", OracleDbType.Int32).Value = ncod_condicionado;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Int32).Value = nidheaderproc;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            pathsList = reader.ReadRowsList<SlipPathVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        pathsList = new List<SlipPathVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return pathsList;
        }

        public PrintResponseVM UpdateStateSlip(SlipPrintVM request)
        {
            var response = new PrintResponseVM();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".UPD_STATE_QUOTATION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NID_COTIZACION", OracleDbType.Int64).Value = request.NID_COTIZACION;
                        cmd.Parameters.Add("P_NSTATE_DOC", OracleDbType.Int64).Value = request.NSTATE_DOC;
                        cmd.Parameters.Add("P_SLOGERROR", OracleDbType.Varchar2).Value = request.SLOGERROR;

                        var P_NCODE = new OracleParameter("P_NCODE", OracleDbType.Int32, ParameterDirection.Output);
                        var P_SMESSAGE = new OracleParameter("P_SMESSAGE", OracleDbType.Varchar2, ParameterDirection.Output);

                        P_SMESSAGE.Size = 4000;

                        cmd.Parameters.Add(P_NCODE);
                        cmd.Parameters.Add(P_SMESSAGE);
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        response.NCODE = Convert.ToInt32(P_NCODE.Value.ToString());
                        response.SMESSAGE = P_SMESSAGE.ToString();
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        response.NCODE = 1;
                        response.SMESSAGE = ex.ToString();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return response;
        }

        public SlipDebtVM ValidateDebt(SlipPrintVM generateQuotation)
        {
            var response = new SlipDebtVM();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_EstadoCiente + ".VALIDAR_DEUDA_COT";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_COTIZACION", OracleDbType.Int64).Value = generateQuotation.NID_COTIZACION;
                        cmd.Parameters.Add("P_NTRANSAC", OracleDbType.Int64).Value = 0;

                        var P_SCLIENT = new OracleParameter("R_SCLIENT", OracleDbType.Varchar2, ParameterDirection.Output);
                        var P_NPRODUCT = new OracleParameter("R_NPRODUCT", OracleDbType.Int32, ParameterDirection.Output);
                        var P_NBRANCH = new OracleParameter("R_NBRANCH", OracleDbType.Int32, ParameterDirection.Output);
                        var P_SMESSAGE = new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, ParameterDirection.Output);
                        var P_COD_ERR = new OracleParameter("P_COD_ERR", OracleDbType.Int32, ParameterDirection.Output);

                        P_SCLIENT.Size = 4000;
                        P_SMESSAGE.Size = 4000;

                        cmd.Parameters.Add(P_SCLIENT);
                        cmd.Parameters.Add(P_NPRODUCT);
                        cmd.Parameters.Add(P_NBRANCH);
                        cmd.Parameters.Add(P_SMESSAGE);
                        cmd.Parameters.Add(P_COD_ERR);

                        cn.Open();
                        reader = cmd.ExecuteReader();

                        response.P_SCLIENT = P_SCLIENT.Value.ToString();
                        response.P_NPRODUCT = Convert.ToInt32(P_NPRODUCT.Value.ToString());
                        response.P_NBRANCH = Convert.ToInt32(P_NBRANCH.Value.ToString());
                        response.P_SMESSAGE = P_SMESSAGE.Value.ToString();
                        response.P_COD_ERR = Convert.ToInt32(P_COD_ERR.Value.ToString());

                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        response = null;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return response;
        }

        public int GetCondicionado(SlipFormatVM format)
        {
            var ncod_condicionado = 0;

            if ((int)format.NBRANCH == Convert.ToInt32(GetValueConfig("vidaLeyBranch")))
            {
                ncod_condicionado = (int)format.NCOD_CONDICIONADO;
            }
            else if ((int)format.NBRANCH == Convert.ToInt32(GetValueConfig("sctrBranch")))
            {
                ncod_condicionado = (int)PrintEnum.Condicionado_Slip.COTIZACION_SCTR;
            }
            else if ((int)format.NBRANCH == Convert.ToInt32(GetValueConfig("accperBranch")))
            {
                ncod_condicionado = (int)PrintEnum.Condicionado_Slip.COTIZACION_AP;
            }
            else if ((int)format.NBRANCH == Convert.ToInt32(GetValueConfig("vgrupoBranch")))
            {
                ncod_condicionado = (int)PrintEnum.Condicionado_Slip.COTIZACION_VG;
            }
            else if ((int)format.NBRANCH == Convert.ToInt32(GetValueConfig("vilpBranch")))
            {
                ncod_condicionado = (int)PrintEnum.Condicionado_Slip.COTIZACION_VILP;
            }
            else
            {
                ncod_condicionado = (int)PrintEnum.Condicionado_Slip.COTIZACION_COVID_GRUPAL;
            }

            return ncod_condicionado;
        }
        #endregion Fin de Zona Genérica


        #region Inicio Zona Generarcion de PDF
        public PrintResponseVM SlipPDF(SlipPrintVM generateQuotation, SlipPathVM printPaths)
        {
            var response = new PrintResponseVM() { NCODE = 0 };
            var genericView = new SlipGeneralInfoVM();

            generateQuotation.SPLANTYPE = generateQuotation.PROCEDURE_LIST.Count > 0 ? generateQuotation.PROCEDURE_LIST[0].SPLANTYPE : null;
            generateQuotation.SCONDICIONADO = generateQuotation.PROCEDURE_LIST.Count > 0 ? generateQuotation.PROCEDURE_LIST[0].SCONDICIONADO : null;

            string spError = string.Empty;

            try
            {
                foreach (var item in generateQuotation.PROCEDURE_LIST)
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
                                cmd.Parameters.Add("P_NID_COTIZACION", OracleDbType.Int64).Value = generateQuotation.NID_COTIZACION;
                                cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                                cn.Open();
                                reader = cmd.ExecuteReader();

                                spError = item.SNAME_SP;

                                if (reader != null)
                                {
                                    if (item.NORDER == 0)
                                    {
                                        genericView = reader.ReadRowsList<SlipGeneralInfoVM>()[0];
                                        continue;
                                    }

                                    if (item.NORDER == 1)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoInsuredQuotationVM>())
                                        {
                                            genericView.insuredList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 2)
                                    {

                                        foreach (var obj in reader.ReadRowsList<InfoRateQuotationVM>())
                                        {
                                            genericView.rateList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 3)
                                    {

                                        foreach (var obj in reader.ReadRowsList<InfoAccountQuotationVM>())
                                        {
                                            genericView.accountList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 4)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<CertificateCoverVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.coverList.Add(obj);
                                        }

                                        string codRamo = Convert.ToString(generateQuotation.NBRANCH);
                                        if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                                        {
                                            var res = generateCoverList(genericView.coverList);
                                            genericView.coverList2 = res.coverNotSubItemList;
                                            genericView.coverList3 = res.coverMainList;
                                            genericView.coverList4 = res.coverAditionalList;
                                            genericView.coverList5 = res.coverMainNotSubItemList;
                                            genericView.coverList6 = res.coverAditionalNotSubItemList;
                                            genericView.coverList7 = res.coverLimitZeroList;
                                            genericView.coverList8 = res.coverClauseList;
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 5)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<InfoCoverCoQuotationVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.coverComplementList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 6)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<InfoGlossExclusiveCoQuotationVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.glossExclusiveCoList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 7)
                                    {
                                        PrintGenerateUtil util = new PrintGenerateUtil();
                                        foreach (var obj in reader.ReadRowsList<InfoGlossExclusiveAdQuotationVM>())
                                        {
                                            obj.NITEM = util.getCorrelativeNumber();
                                            genericView.glossExclusiveAdList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 8)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoGlossProtectaQuotationVM>())
                                        {
                                            genericView.glossProtectaList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 9)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoRateDetVM>())
                                        {
                                            genericView.ratePremiumList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 10)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoRateDetVM>())
                                        {
                                            genericView.ratePremiumExList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 11)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoOverPayVM>())
                                        {
                                            genericView.coverOverPayList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 12)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoBenefitQuotationVM>())
                                        {
                                            genericView.benefitList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 13)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoAssistanceQuotationVM>())
                                        {
                                            genericView.assistanceList.Add(obj);

                                            if (!String.IsNullOrEmpty(obj.COD_URL))
                                            {
                                                new SlipGenericDA().downloadAssistencePDF(obj, printPaths, generateQuotation);
                                            }
                                        }
                                        continue;
                                    }
                                    if (item.NORDER == 14)
                                    {
                                        foreach (var obj in reader.ReadRowsList<InfoServicesQuotationVM>())
                                        {
                                            genericView.adicionalServicesList.Add(obj);
                                        }
                                        continue;
                                    }

                                    if (item.NORDER == 15)
                                    {
                                        genericView.exclusionList = new List<ExclusionVM>();
                                        foreach (var obj in reader.ReadRowsList<ExclusionVM>())
                                        {
                                            genericView.exclusionList.Add(obj);
                                        }
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.NCODE = 1;
                                response.SMESSAGE = ex.ToString();
                            }
                            finally
                            {
                                if (cn.State == ConnectionState.Open) cn.Close();
                            }
                        }
                    }
                }

                response = new SlipGenericDA().quotationFormat(generateQuotation,
                                              printPaths, genericView);

                generateQuotation.SLOGERROR = response.SMESSAGE;
                generateQuotation.NSTATE_DOC = response.NCODE == 0 ? Convert.ToInt32(PrintEnum.State.CORRECTO) : Convert.ToInt32(PrintEnum.State.ERROR);
                response = new SlipPrintDA().UpdateStateSlip(generateQuotation);
                //ELog.save(this, "FIN IMPRESION DE SLIP | N° COTIZACION: " + generateQuotationBM.NID_COTIZACION);

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = "QuotationPlanBasicPDF: " + ex.Message + " - Nro Cotizacion: " + generateQuotation.NID_COTIZACION + " - SP_NAME: " + spError;
                //ELog.saveDocumentos(this, ex.ToString() + " - Nro Cotizacion: " + generateQuotation.NID_COTIZACION + " - SP_NAME: " + spError);
                //return response;
            }

            return response;
        }

        public PrintResponseVM SlipCCEPDF(SlipDebtVM generateQuotation, List<SlipPathVM> printPaths)
        {
            var response = new PrintResponseVM() { NCODE = 0 };
            var genericView = new SlipGeneralInfoVM();


            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_EstadoCiente + ".REA_ESTADO_CUENTA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Int32).Value = generateQuotation.P_NBRANCH;
                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Int32).Value = generateQuotation.P_NPRODUCT;
                        cmd.Parameters.Add("P_SCLIENT", OracleDbType.Int32).Value = generateQuotation.P_SCLIENT;
                        cmd.Parameters.Add("C_CLIENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            genericView.baseAccountStatusVM = reader.ReadRowsList<BaseGenerateAccountStatusVM>()[0];

                            reader.NextResult();

                            foreach (var item in reader.ReadRowsList<GenerateAccountStatusVM>())
                            {
                                genericView.baseAccountStatusVM.clientList.Add(item);
                            }
                        }

                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        response.NCODE = 1;
                        response.SMESSAGE = ex.ToString();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            response = new SlipGenericDA().generateDocumentCCE(generateQuotation, printPaths[0], genericView);

            return response;
        }
        #endregion Fin Zona Impresión
    }
}
