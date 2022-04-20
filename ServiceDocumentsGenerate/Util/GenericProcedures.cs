using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Util
{
    public sealed class GenericProcedures
    {
        private GenericProcedures() { }

        public static readonly string pkg_CargaMasivaPD = "PKG_CARGA_MASIVA_IMPRESION";
        public static readonly string pkg_CargaMasiva = "PKG_PV_CARGA_MASIVA";
        public static readonly string pkg_Condicionados = "PKG_PV_CONDICIONADOS";
        public static readonly string pkg_EstadoCiente = "PKG_PV_VAL_BLOCK_CLIENT";

        public static readonly string sp_LeerPolizaEstado = "PD_REA_POL_ESTADO";

    }
}
