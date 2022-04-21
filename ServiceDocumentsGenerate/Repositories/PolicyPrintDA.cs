using Oracle.ManagedDataAccess.Client;
using ServiceDocumentsGenerate.Entities;
using ServiceDocumentsGenerate.Entities.PolicyPrint;
using ServiceDocumentsGenerate.Entities.SlipPrint;
using ServiceDocumentsGenerate.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Repositories
{
    public class PolicyPrintDA : GenericMethods
    {
        #region Inicio Zona Genérica
        public List<PolicyJobVM> GetJobList()
        {
            var jobsList = new List<PolicyJobVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_JOBPRINT";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            jobsList = reader.ReadRowsList<PolicyJobVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        jobsList = new List<PolicyJobVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return jobsList;
        }

        public List<PolicyFormatVM> GetFormatList(PolicyJobVM job)
        {
            var formatList = new List<PolicyFormatVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_FORMATOS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Int32).Value = job.NIDHEADERPROC;
                        cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Int32).Value = job.NIDDETAILPROC;
                        cmd.Parameters.Add("P_NIDFILECONFIG", OracleDbType.Int32).Value = job.NIDFILECONFIG;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            formatList = reader.ReadRowsList<PolicyFormatVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        formatList = new List<PolicyFormatVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return formatList;
        }

        public List<PolicyProcedureVM> GetProcedureList(PolicyFormatVM request)
        {
            var procedureList = new List<PolicyProcedureVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_PROCEDURES";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NIDFILECONFIG", OracleDbType.Int32).Value = request.NIDFILECONFIG;
                        cmd.Parameters.Add("P_STRANSAC", OracleDbType.Varchar2).Value = request.NTRANSAC;
                        cmd.Parameters.Add("P_NCOD_CONDICIONADO", OracleDbType.Double).Value = request.NCOD_CONDICIONADO;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            procedureList = reader.ReadRowsList<PolicyProcedureVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        procedureList = new List<PolicyProcedureVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return procedureList;
        }

        public PrintResponseVM UpdateStatePolicy(PolicyPrintVM request)
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
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".INSUPD_ENV_PRINT";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Double).Value = request.NIDHEADERPROC;
                        cmd.Parameters.Add("P_NIDDETAILPROC", OracleDbType.Double).Value = request.NIDDETAILPROC;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = request.NBRANCH;
                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = request.NPRODUCT;
                        cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = request.NPOLICY;
                        cmd.Parameters.Add("P_NSTATE", OracleDbType.Int32).Value = request.NSTATE;
                        cmd.Parameters.Add("P_NOPC", OracleDbType.Int32).Value = request.NOPC;
                        cmd.Parameters.Add("P_SMESSAGE_IMP", OracleDbType.Varchar2).Value = request.SMESSAGE_IMP;
                        cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Double).Value = request.NMOVEMENT;

                        //var P_NCODE = new OracleParameter("P_NCODE", OracleDbType.Int32, ParameterDirection.Output);
                        //var P_SMESSAGE = new OracleParameter("P_SMESSAGE", OracleDbType.Varchar2, ParameterDirection.Output);

                        //P_SMESSAGE.Size = 4000;

                        //cmd.Parameters.Add(P_NCODE);
                        //cmd.Parameters.Add(P_SMESSAGE);
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        response.NCODE = 0;
                        response.SMESSAGE = "Se actualizó el estado correctamente";
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

        public List<PolicyFormatDetailVM> GetFormatDetail(PolicyPrintVM request)
        {
            var procedureList = new List<PolicyFormatDetailVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_FORMATOS_DETAIL";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Double).Value = request.NBRANCH;
                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Double).Value = request.NPRODUCT;
                        cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = request.NPOLICY;
                        cmd.Parameters.Add("P_NCOD_CONDICIONADO", OracleDbType.Double).Value = request.NCOD_CONDICIONADO;
                        cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            procedureList = reader.ReadRowsList<PolicyFormatDetailVM>();
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        procedureList = new List<PolicyFormatDetailVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return procedureList;
        }

        public int GetStatePolicy(PolicyPrintVM generatePolicy)
        {
            var statePolicy = 0;

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.sp_LeerPolizaEstado;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Int64).Value = generatePolicy.NBRANCH;
                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Int64).Value = generatePolicy.NPRODUCT;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Int64).Value = generatePolicy.NPOLICY;
                        var P_NFLAG = new OracleParameter("P_NFLAG", OracleDbType.Int32, ParameterDirection.Output);
                        cmd.Parameters.Add(P_NFLAG);
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        statePolicy = Convert.ToInt32(P_NFLAG.Value.ToString());
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        statePolicy = 0;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return statePolicy;
        }

        public List<PolicyUserVM> GetUsersPolicyList(PolicyPrintVM request)
        {
            var usersList = new List<PolicyUserVM>();

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasiva + ".REA_POLIZA_USERS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Int32).Value = request.NBRANCH;
                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Int32).Value = request.NPRODUCT;
                        cmd.Parameters.Add("P_NPOLICY", OracleDbType.Int64).Value = request.NPOLICY;
                        cmd.Parameters.Add("P_NMOVEMENT", OracleDbType.Int32).Value = request.NMOVEMENT;
                        cmd.Parameters.Add("C_TABLE", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                var user = new PolicyUserVM();
                                user.snombre = reader["SNOMBRE"].ToString();
                                user.sapePat = reader["SAPELLIDOP"].ToString();
                                user.sapeMat = reader["SAPELLIDOM"].ToString();
                                user.slegalName = reader["SLEGALNAME"].ToString();
                                user.sdocumento = reader["SDOCUMENTO"].ToString();
                                user.semail = reader["SEMAIL"].ToString();

                                usersList.Add(user);
                            }
                        }
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        usersList = new List<PolicyUserVM>();
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return usersList;
        }

        public string GetTemplateName(PolicyPrintVM generatePolicy)
        {
            var template = string.Empty;

            using (OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        cmd.Connection = cn;
                        IDataReader reader = null;
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_ELEC_POL_TEMPLATE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_NBRANCH", OracleDbType.Int32).Value = generatePolicy.NBRANCH;
                        cmd.Parameters.Add("P_NPRODUCT", OracleDbType.Int32).Value = generatePolicy.NPRODUCT;
                        cmd.Parameters.Add("P_NIDHEADERPROC", OracleDbType.Int32).Value = generatePolicy.NPOLICY;
                        var P_STEMPLATE = new OracleParameter("P_STEMPLATE", OracleDbType.Varchar2, ParameterDirection.Output);
                        P_STEMPLATE.Size = 2000;
                        cmd.Parameters.Add(P_STEMPLATE);
                        cn.Open();

                        reader = cmd.ExecuteReader();

                        template = P_STEMPLATE.Value.ToString() == "null" ? null : P_STEMPLATE.Value.ToString();
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        template = string.Empty;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
            }

            return template;
        }
        #endregion

        #region Inicio Zona Impresión
        public PrintResponseVM PolicyGeneratePDF(PolicyPrintVM generatePolicy, List<SlipPathVM> pathsList)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            if (new int[] { (int)PrintEnum.Condicionado_Poliza.SOLICITUD, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_LEY,
                                (int)PrintEnum.Condicionado_Poliza.SOLICITUD_COMPLETO, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_ESPECIAL,
                                (int)PrintEnum.Condicionado_Poliza.COVID_SOLICITUD,/* (int)PrintEnum.Condicionado_Poliza.AP_SOLICITUD,*/
                                (int)PrintEnum.Condicionado_Poliza.SOLICITUD_SCTR, (int) PrintEnum.Condicionado_Poliza.SOLICITUD_SCTR_SIN_SUELDO,
                                (int)PrintEnum.Condicionado_Poliza.SOLICITUD_AP, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_VILP }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().RequestPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES, (int)PrintEnum.Condicionado_Poliza.COVID_CON_PARTICULAR,
                                    /* (int)PrintEnum.Condicionado_Poliza.AP_CON_PARTICULAR, */(int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_SCTR,
                                     (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_AP, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_VG,
                                     (int)PrintEnum.Condicionado_Poliza.CONDICIONES_PARTICULARES_VILP}.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().ParticularConditionsPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES, (int)PrintEnum.Condicionado_Poliza.COVID_CON_GENERAL,
                                     /*(int)PrintEnum.Condicionado_Poliza.AP_CON_GENERAL, */(int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_SCTR,
                                     (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_AP, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_VG,
                                     (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_VILP }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().GeneralConditionsPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CLAUSULAS_ADICIONALES, (int)PrintEnum.Condicionado_Poliza.CLAUSULAS_ADICIONALES_BASICO,
                                     (int)PrintEnum.Condicionado_Poliza.CLAUSULAS_ADICIONALES_COMPLETO , (int)PrintEnum.Condicionado_Poliza.CLAUSULAS_ADICIONALES_ESPECIAL,
                                     (int)PrintEnum.Condicionado_Poliza.COVID_CON_ESPECIAL, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_ESPECIALES_AP,
                                     (int)PrintEnum.Condicionado_Poliza.CONDICIONES_ESPECIALES_VG/*, (int)PrintEnum.Condicionado_Poliza.CONDICIONES_GENERALES_VILP*/}.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                //var formatsDetailVMs = GetFormatDetail(generatePolicy);
                response = new PolicyGenericDA().DetailDocumentsPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.POLIZA_ELECTRONICA }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().ElectronicPolicyPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.RESUMEN, (int)PrintEnum.Condicionado_Poliza.RESUMEN_BASICO,
                                     (int)PrintEnum.Condicionado_Poliza.RESUMEN_COMPLETO, (int)PrintEnum.Condicionado_Poliza.RESUMEN_ESPECIAL,
                                     (int)PrintEnum.Condicionado_Poliza.COVID_RESUMEN, /*(int)PrintEnum.Condicionado_Poliza.AP_RESUMEN,*/
                                     (int)PrintEnum.Condicionado_Poliza.RESUMEN_SCTR, (int)PrintEnum.Condicionado_Poliza.RESUMEN_AP,
                                     (int)PrintEnum.Condicionado_Poliza.RESUMEN_VG, (int)PrintEnum.Condicionado_Poliza.RESUMEN_VILP }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                //var formatsDetailVMs = GetFormatDetail(generatePolicy);
                response = new PolicyGenericDA().SummaryPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ASEGURADO, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ASEGURADO_AP,
                                     (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ASEGURADO_VG}.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().InsuredProofPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_PROVISIONAL_VL, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_PROVISIONAL_SCTR }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                //var polizaEstado = GetStatePolicy(generatePolicy);
                response = GetStatePolicy(generatePolicy) == 1 ? new PolicyGenericDA().provisionalRecordPDF(generatePolicy, pathsList[0]) : new PrintResponseVM() { NCODE = 0 };
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.CERTIFICADO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_BASICO,
                                     (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_COMPLETO, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_ESPECIAL,
                                     (int)PrintEnum.Condicionado_Poliza.COVID_CERTIFICADO,/* (int)PrintEnum.Condicionado_Poliza.AP_CERTIFICADO,*/
                                     (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_SCTR, (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_AP,
                                     (int)PrintEnum.Condicionado_Poliza.CERTIFICADO_VG, (int)PrintEnum.Condicionado_Poliza.SOLICITUD_CERTIFICADO_AP,
                                     (int)PrintEnum.Condicionado_Poliza.SOLICITUD_VG }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                //var formatsDetailVMs = GetFormatDetail(generatePolicy);
                response = new PolicyGenericDA().CertificatePDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ENDORSEMENT, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_INCLUSION_AP,
                                     (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_INCLUSION_VIAJES_AP, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_DECLARACION_AP,
                                     (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_MODIFICACION_AP, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_EXCLUSION_AP,
                                     (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ANULACION_AP, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_EXCLUSION_VG,
                                     (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_INCLUSION_VG, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_DECLARACION_VG,
                                     (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_ANULACION_VG, (int)PrintEnum.Condicionado_Poliza.CONSTANCIA_MODIFICACION_VG }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().EndorsementPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.COVID_INDEMNIZACION }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().GeneratePDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.EXCLUSION_VL }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().ProofExcludePDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.COBERTURA_EXCEDENTE }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().CoverExcPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ENDOSO }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().EndosoPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ENDOSO_POLIZA }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().EndorsementPolicyPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ENDOSO_MODIFIC_REM }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().EndorsementModifPDF(generatePolicy, pathsList[0]);
            }
            else if (new int[] { (int)PrintEnum.Condicionado_Poliza.ANEXO_ASISTENCIA, (int)PrintEnum.Condicionado_Poliza.ANEXO_CLAUSULA }.Contains((int)generatePolicy.NCOD_CONDICIONADO))
            {
                response = new PolicyGenericDA().AnexosPDF(generatePolicy, pathsList[0]);
            }

            return response;
        }
        #endregion Fin Zona Impresión
    }
}
