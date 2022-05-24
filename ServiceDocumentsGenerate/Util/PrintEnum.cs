using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Util
{
    public class PrintEnum
    {
        public enum Condicionado_Slip
        {
            SOLICITUD = 1,
            CONDICIONES_PARTICULARES = 2,
            CLAUSULAS_ADICIONALES = 3,
            RESUMEN = 4,
            CONDICIONES_GENERALES = 6,
            CONSTANCIA_ASEGURADO = 5,
            CERTIFICADO = 7,
            COTIZACION_PLAN_BASICO = 10,
            COTIZACION_PLAN_COMPLETO = 11,
            COTIZACION_PLAN_ESPECIAL = 12,
            COTIZACION_PLAN_LEY = 13,
            COTIZACION_COVID_GRUPAL = 29,
            COTIZACION_COVID_INDIVIDUAL = 0,
            COTIZACION_SCTR = 52,
            COTIZACION_AP = 58,
            COTIZACION_VG = 79,
            COTIZACION_VILP = 101,
            DEFAULT = 0
        }

        public enum Condicionado_Poliza
        {
            SOLICITUD = 1,
            SOLICITUD_LEY = 24,
            SOLICITUD_COMPLETO = 25,
            SOLICITUD_ESPECIAL = 26,
            CONDICIONES_PARTICULARES = 2,
            CLAUSULAS_ADICIONALES = 3,
            CLAUSULAS_ADICIONALES_BASICO = 21,
            CLAUSULAS_ADICIONALES_COMPLETO = 22,
            CLAUSULAS_ADICIONALES_ESPECIAL = 23,
            RESUMEN = 4,
            RESUMEN_BASICO = 15,
            RESUMEN_COMPLETO = 16,
            RESUMEN_ESPECIAL = 17,
            CONDICIONES_GENERALES = 6,
            CONSTANCIA_ASEGURADO = 5,
            CERTIFICADO = 7,
            CERTIFICADO_BASICO = 18,
            CERTIFICADO_COMPLETO = 19,
            CERTIFICADO_ESPECIAL = 20,
            ENDORSEMENT = 8,
            COTIZACION_PLAN_BASICO = 10,
            COTIZACION_PLAN_COMPLETO = 11,
            COTIZACION_PLAN_ESPECIAL = 12,
            COTIZACION_PLAN_LEY = 13,
            POLIZA_ELECTRONICA = 14,
            EECC = 27,
            COVID_INDEMNIZACION = 30,
            COVID_CON_ESPECIAL = 31,
            COVID_CON_GENERAL = 32,
            COVID_CON_PARTICULAR = 33,
            COVID_RESUMEN = 34,
            COVID_SOLICITUD = 35,
            COVID_CERTIFICADO = 36,
            EXCLUSION_VL = 37,
            //COVID_SOLICITUD_N = 38,
            COBERTURA_EXCEDENTE = 47,
            ENDOSO = 48,
            ENDOSO_POLIZA = 49,
            ENDOSO_MODIFIC_REM = 50,
            //AP_CON_GENERAL = 41,
            //AP_CON_PARTICULAR = 42,
            //AP_RESUMEN = 43,
            //AP_SOLICITUD = 44,
            //AP_CERTIFICADO = 45,
            CONDICIONES_GENERALES_SCTR = 54,
            CONDICIONES_PARTICULARES_SCTR = 53,
            RESUMEN_SCTR = 55,
            SOLICITUD_SCTR = 56,
            SOLICITUD_SCTR_SIN_SUELDO = 106,
            CERTIFICADO_SCTR = 57,

            CONDICIONES_ESPECIALES_AP = 61,
            CONDICIONES_GENERALES_AP = 62,
            CONDICIONES_PARTICULARES_AP = 63,
            RESUMEN_AP = 64,
            SOLICITUD_AP = 65,
            ANEXO_CLAUSULA = 68,
            CONSTANCIA_ASEGURADO_AP = 74,
            ANEXO_ASISTENCIA = 100,
            SOLICITUD_CERTIFICADO_AP = 66,
            CERTIFICADO_AP = 67,

            CONSTANCIA_ANULACION_AP = 73,
            CONSTANCIA_EXCLUSION_AP = 75,
            CONSTANCIA_INCLUSION_AP = 76,
            CONSTANCIA_INCLUSION_VIAJES_AP = 77,
            CONSTANCIA_DECLARACION_AP = 78,
            CONSTANCIA_MODIFICACION_AP = 69,

            /*INI JDD */
            CONDICIONES_ESPECIALES_VG = 81,
            CONDICIONES_GENERALES_VG = 82,
            CONDICIONES_PARTICULARES_VG = 83,
            RESUMEN_VG = 84,
            SOLICITUD_VG = 85,
            CERTIFICADO_VG = 86,

            CONSTANCIA_EXCLUSION_VG = 87,
            CONSTANCIA_ANULACION_VG = 80,
            CONSTANCIA_INCLUSION_VG = 88,
            CONSTANCIA_DECLARACION_VG = 89,
            CONSTANCIA_MODIFICACION_VG = 90,
            CONSTANCIA_ASEGURADO_VG = 72,
            /*FIN  JDD */

            CONSTANCIA_PROVISIONAL_VL = 91,
            CONSTANCIA_PROVISIONAL_SCTR = 92,
            /*INI  RQ-VILP-POLICY */
            CONDICIONES_GENERALES_VILP = 102,
            CONDICIONES_PARTICULARES_VILP = 103,
            RESUMEN_VILP = 104,
            SOLICITUD_VILP = 105,
            CERTIFICADO_VILP = 107,
            /*FIN  RQ-VILP-POLICY */

            DEFAULT = 0
        }

        public enum State
        {
            SIN_INCIAR = 0,
            CORRECTO = 1,
            INICIALIZADO = 2,
            EN_PROCESO = 3,
            ERROR = 5,
            ERROR_PE = 7
        }
    }
}
