using Oracle.ManagedDataAccess.Client;
using ServiceDocumentsGenerate.Entities.PolicyPrint;
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
    public class ReSendPeDA : GenericMethods
    {
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
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_JOBPRINT_PE";
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
                        cmd.CommandText = GenericProcedures.pkg_CargaMasivaPD + ".REA_FORMATOS_TEST";
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
    }
}
