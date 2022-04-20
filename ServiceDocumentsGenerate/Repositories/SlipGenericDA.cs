using System;
using System.IO;
using System.Linq;
using ServiceDocumentsGenerate.Entities;
using ServiceDocumentsGenerate.Entities.SlipPrint;
using ServiceDocumentsGenerate.Util;
using System.Collections.Generic;

namespace ServiceDocumentsGenerate.Repositories
{
    public class SlipGenericDA : GenericMethods
    {
        public void downloadAssistencePDF(InfoAssistanceQuotationVM obj, SlipPathVM printPathsVM, SlipPrintVM generateQuotationBM)
        {
            string baseUrlApi = GetValueConfig("baseUrlWSP");
            string codRamo = Convert.ToString(generateQuotationBM.NBRANCH);

            try
            {
                var values = new Dictionary<string, string>();
                values.Add("nbranch", codRamo);
                values.Add("codUrl", obj.COD_URL);
                var resReference = PrintGenericService<ResponseGraph, InfoAssistanceQuotationVM>.GetWithParameters(baseUrlApi, "/PolicyManager/getReferenceURL", values);

                if (resReference != null)
                {
                    if (resReference.errors == null)
                    {
                        var pathTempPDF = printPathsVM.SRUTA_DESTINO + generateQuotationBM.NID_COTIZACION + "\\";
                        string pathNameFilePDF = pathTempPDF + "z_" + obj.SDESCRIPT_ASSIT + ".pdf";

                        if (!Directory.Exists(pathTempPDF))
                        {
                            Directory.CreateDirectory(pathTempPDF);
                        }
                        else
                        {
                            if (File.Exists(pathNameFilePDF))
                            {
                                File.Delete(pathNameFilePDF);
                            }
                        }

                        if (!String.IsNullOrEmpty(resReference.data.referenceURL))
                        {
                            using (System.Net.WebClient client = new System.Net.WebClient())
                            {
                                client.DownloadFile(resReference.data.referenceURL, pathNameFilePDF);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ELog.saveDocumentos(this, ex.ToString());
            }
        }

        public PrintResponseVM quotationFormat(SlipPrintVM generateQuotationBM, SlipPathVM printPathsVM, SlipGeneralInfoVM genericView)
        {
            var response = new PrintResponseVM() { NCODE = 0 };
            try
            {
                if (!Directory.Exists(printPathsVM.SRUTA_TEMP))
                {
                    Directory.CreateDirectory(printPathsVM.SRUTA_TEMP);
                }

                string formatDate = DateTime.Now.Day + "" + DateTime.Now.Month + "" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second;
                string pathNameFileTemplate = (printPathsVM.SRUTA_TEMPLATE + printPathsVM.SNAME_TEMPLATE + ".docx").Replace(" ", "");

                string pathTempPDF = "";
                string pathNameFileTemplateTemp = "";

                pathTempPDF = printPathsVM.SRUTA_DESTINO + generateQuotationBM.NID_COTIZACION + "\\";
                pathNameFileTemplateTemp = (printPathsVM.SRUTA_TEMP + printPathsVM.SNAME_TEMPLATE + "_" + generateQuotationBM.NID_COTIZACION + "_" + generateQuotationBM.SPLANTYPE + ".docx").Replace(" ", "");

                string pathNameFilePDF = pathTempPDF + generateQuotationBM.SCONDICIONADO + ".pdf";
                string pathNameFilePDF_test = pathTempPDF + generateQuotationBM.SCONDICIONADO + "_test.pdf";
                string nameFilePDFFull = generateQuotationBM.SCONDICIONADO + "_full.pdf";

                string codRamo = Convert.ToString(generateQuotationBM.NBRANCH);

                DeleteFilesPath(pathTempPDF);

                response = CreateQuotationDocument(pathNameFileTemplateTemp, pathNameFileTemplate, pathTempPDF, pathNameFilePDF_test, pathNameFilePDF, generateQuotationBM.NCOD_CONDICIONADO, genericView);

                if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vgrupoBranch") }.Contains(codRamo))
                {
                    //    joinDocumentsQuotation(pathTempPDF, nameFilePDFFull);
                }

                response.PATH_PDF = pathTempPDF;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = "QutationPlanBasicPDF: " + ex.Message + " - Nro Cotizacion: " + generateQuotationBM.NID_COTIZACION;
                response.PATH_PDF = null;
                //ELog.saveDocumentos(this, "Error: " + ex.ToString() + " - Nro Cotizacion: " + generateQuotationBM.NID_COTIZACION);
            }

            return response;
        }

        public PrintResponseVM generateDocumentCCE(SlipDebtVM generateQuotation, SlipPathVM printPaths, SlipGeneralInfoVM genericView)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                //validate which temporary directory isn't created, otherwise make sure it will there
                if (!Directory.Exists(printPaths.SRUTA_TEMP))
                {
                    Directory.CreateDirectory(printPaths.SRUTA_TEMP);
                }


                string formatDate = DateTime.Now.Day + "" + DateTime.Now.Month + "" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second;
                string pathNameFileTemplate = printPaths.SRUTA_TEMPLATE + printPaths.SNAME_TEMPLATE + ".docx";

                //string pathTempPDF = "";
                //string pathNameFileTemplateTemp = "";
                //string pathNameFilePDF = "";

                //PrintEnum.Condicionado ncod_condicionado = PrintGenerateUtil.validateDocumetType(param.documentCode);

                //if (new int[] { (int)PrintEnum.Condicionado.EECC }.Contains((int)param.documentCode))
                //{
                string pathNameFileTemplateTemp = printPaths.SRUTA_TEMP + printPaths.SNAME_TEMPLATE + "_" + genericView.baseAccountStatusVM.DOCUMENTO + ".docx";
                string pathTempPDF = printPaths.SRUTA_DESTINO + genericView.baseAccountStatusVM.DOCUMENTO + "\\";
                //}

                string pathNameFilePDF = pathTempPDF + printPaths.SNAME_TEMPLATE + ".pdf";
                string pathNameFilePDF_test = pathTempPDF + printPaths.SNAME_TEMPLATE + "_test.pdf";

                //GenericObservableCollection genericObservableCollection = new GenericObservableCollection();
                //if (new int[] { (int)PrintEnum.Condicionado.EECC }.Contains((int)param.documentCode))
                //{
                //    genericObservableCollection.accountStatusCollection = new ObservableCollection<BaseGenerateAccountStatusVM>();
                //    genericObservableCollection.accountStatusCollection.Add(genericView.baseAccountStatusVM);
                //}

                //response = CreateDocument(pathNameFileTemplateTemp, pathNameFileTemplate, pathTempPDF, pathNameFilePDF_test, pathNameFilePDF, generateQuotation.COD_CONDICIONADO, null, genericView);

                //response.responseCode = responseDocument.NCODE;
                //response.message = responseDocument.SMESSAGE;
                response.PATH_PDF = response.NCODE == 0 ? pathNameFilePDF : null;

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = "EEC: " + ex.Message;
                response.PATH_PDF = null;
                //ELog.saveDocumentos(this, ex.ToString());
                //throw ex;
            }

            return response;
        }

        private PrintResponseVM CreateQuotationDocument(string pathNameFileTemplateTemp, string pathNameFileTemplate, string pathTempPDF, string pathNameFilePDF_test, string pathNameFilePDF,
                                    dynamic NCOD_CONDICIONADO, SlipGeneralInfoVM data)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                DeleteFile(pathNameFileTemplateTemp);

                File.Copy(pathNameFileTemplate, pathNameFileTemplateTemp);

                using (var factory = NGS.Templater.Configuration.Factory.Open(pathNameFileTemplateTemp))
                {
                    if (data.coverComplementList.Count == 0)
                    {
                        factory.Templater.Resize(new[] { "has_covers" }, 0);
                    }
                    else
                    {
                        factory.Templater.Replace("has_covers", null);
                    }
                    if (data.ratePremiumExList.Count == 0)
                    {
                        factory.Templater.Resize(new[] { "has_exc" }, 0);
                    }
                    else
                    {
                        factory.Templater.Replace("has_exc", null);
                    }

                    if (data.benefitList.Count == 0)
                    {
                        data.STEXT_BENEFITS = "NO APLICA";
                        factory.Templater.Resize(new[] { "has_benefit" }, 0);
                    }
                    else
                    {
                        data.STEXT_BENEFITS = string.Empty;
                        factory.Templater.Replace("has_benefit", null);
                    }

                    if (data.assistanceList.Count == 0)
                    {
                        factory.Templater.Resize(new[] { "has_assistence" }, 0);
                    }
                    else
                    {
                        factory.Templater.Replace("has_assistence", null);
                    }

                    if (data.FLAG_VIAJE == 0)
                    {
                        factory.Templater.Resize(new[] { "FLAG_VIAJE" }, 0);
                    }
                    else
                    {
                        factory.Templater.Replace("FLAG_VIAJE", null);
                    }

                    if (data.FLAG_COVER == 0)
                    {
                        factory.Templater.Resize(new[] { "HAS_FLAG_COVER" }, 0);
                    }
                    else
                    {
                        factory.Templater.Replace("HAS_FLAG_COVER", null);
                    }

                    if (data.FLAG_INDIVIDUAL == 0)
                    {
                        factory.Templater.Resize(new[] { "HAS_FLAG_INDIVIDUAL" }, 0);
                    }
                    else
                    {
                        factory.Templater.Replace("HAS_FLAG_INDIVIDUAL", null);
                    }

                    if (data.FLAG_INDIVIDUAL == 0)
                    {
                        factory.Templater.Replace("HAS_FLAG_NO_INDIVIDUAL", null);
                        factory.Templater.Replace("HAS_FLAG_NO_INDIVIDUAL_2", null);
                    }
                    else
                    {
                        factory.Templater.Resize(new[] { "HAS_FLAG_NO_INDIVIDUAL" }, 0);
                        factory.Templater.Resize(new[] { "HAS_FLAG_NO_INDIVIDUAL_2" }, 0);
                    }

                    if (data.coverList7.Count == 0)
                    {
                        data.SLIMITE_AGREG = "NO APLICA";
                        factory.Templater.Resize(new[] { "has_limit" }, 0);
                    }
                    else
                    {
                        data.SLIMITE_AGREG = string.Empty;
                        factory.Templater.Replace("has_limit", null);
                    }

                    factory.Process(data);
                }

                response = RemoveAdvertising(pathNameFileTemplateTemp);
                response = response.NCODE == 0 ? ConvertWordToPdf(pathTempPDF, pathNameFileTemplateTemp, pathNameFilePDF) : response;
                //response = response.NCODE == 0 ? DeletePages(pathNameFilePDF_test, pathNameFilePDF) : response;
                DeleteFile(pathNameFileTemplateTemp);


                response.NCODE = response.NCODE;
                response.SMESSAGE = response.NCODE == 0 ? "Se completó el proceso de slip" : response.SMESSAGE;
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = "QutationPlanBasicPDF: " + ex.Message + " - Cotizacion: " + data.NID_COTIZACION;
                response.PATH_PDF = null;
                //ELog.saveDocumentos(this, "Error: " + ex.ToString() + " - Cotizacion: " + data.NID_COTIZACION);
            }

            return response;
        }
    }
}
