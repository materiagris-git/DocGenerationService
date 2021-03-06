using System;
using System.Configuration;
using DocumentFormat.OpenXml.Packaging;
using System.IO.Packaging;
using System.IO.Compression;
using System.IO;
using ServiceDocumentsGenerate.Entities;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using ServiceDocumentsGenerate.Entities.PolicyPrint;
using ServiceDocumentsGenerate.Entities.SlipPrint;
using ServiceDocumentsGenerate.Entities.ElectronicPolicy;
using ServiceDocumentsGenerate.Repositories;
using Newtonsoft.Json;
using Word = NetOffice.WordApi;
using System.Diagnostics;

namespace ServiceDocumentsGenerate.Util
{
    public class GenericMethods
    {
        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";
        private int count;
        private int correlativeNumber;


        protected string GetValueConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public PrintResponseVM RemoveAdvertising(string fileTemp)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                using (var document = WordprocessingDocument.Open(fileTemp, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(document.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    docText = docText.Replace("Unlicensed version. Please register @ templater.info", "");

                    using (StreamWriter sw = new StreamWriter(document.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = ex.Message;
                saveLog("RemoveAdvertising", ex.Message, "Error");
            }

            return response;
        }

        public PrintResponseVM ConvertWordToPdf(string pathTemp, string pathNameFileTemplateTemp, string pathNameFilePDF)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                if (!Directory.Exists(pathTemp))
                {
                    Directory.CreateDirectory(pathTemp);
                }

                //DocumentCore dc = DocumentCore.Load(pathNameFileTemplateTemp);
                //dc.Save(pathNameFilePDF);

                Word.Application wordApplication = new Word.Application();
                wordApplication.DisplayAlerts = Word.Enums.WdAlertLevel.wdAlertsNone; //Do not display alerts from Word app. 
                Word.Document doc;
                doc = wordApplication.Documents.Open(pathNameFileTemplateTemp, false);
                //Inheret convert tool see https://msdn.microsoft.com/en-us/VBA/Word-VBA/articles/document-exportasfixedformat-method-word
                doc.ExportAsFixedFormat(pathNameFilePDF, Word.Enums.WdExportFormat.wdExportFormatPDF, false, 1);
                doc.Close();

                wordApplication.Quit();
                wordApplication.Dispose();
            }
            catch (Exception ex)
            {
                response.NCODE = 3;
                response.SMESSAGE = ex.Message;
                saveLog("ConvertWordToPdf", ex.Message, "Error");
            }

            return response;
        }

        public PrintResponseVM DeletePages(string SourcePdfPath, string OutputPdfPath)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                using (Stream resultPDFOutputStream = new FileStream(path: OutputPdfPath, mode: FileMode.Create))
                {
                    var reader = new iTextSharp.text.pdf.PdfReader(SourcePdfPath);
                    int numberPage = reader.NumberOfPages;
                    reader.SelectPages("1-" + (numberPage - 1).ToString());

                    PdfStamper pdfStamper = new PdfStamper(reader, resultPDFOutputStream);
                    pdfStamper.Close();
                    reader.Close();
                }

                DeleteFile(SourcePdfPath);
            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = ex.Message;
            }
            return response;
        }

        public PrintResponseVM DeleteFile(string pathFile)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                if (File.Exists(pathFile))
                {
                    File.Delete(pathFile);
                }
            }
            catch (Exception ex)
            {
                response.NCODE = 0;
                response.SMESSAGE = ex.Message;
            }

            return response;
        }

        public PrintResponseVM DeleteFilesPath(string pathFiles)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                if (Directory.Exists(pathFiles))
                {
                    string[] listFiles = Directory.GetFiles(pathFiles);
                    foreach (string file in listFiles)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                response.NCODE = 0;
                response.SMESSAGE = ex.Message;
            }

            return response;


        }

        private dynamic GetProductSend(dynamic NTRANSAC, dynamic NPRODUCT, dynamic NBRANCH)
        {
            dynamic product = NPRODUCT;

            if (new string[] { GetValueConfig("accperBranch"), GetValueConfig("vidaGrupoBranch"), GetValueConfig("vilpBranch") }.ToList().Contains(NBRANCH.toString()))
            {
                product = 0;
            }

            return product;
        }

        public async void downloadAnexoPDF(AnexoPDF obj, SlipPathVM printPathsVM, PolicyPrintVM printGenerateBM)
        {
            string baseUrlApi = GetValueConfig("baseUrlWSP");
            string codRamo = Convert.ToString(printGenerateBM.NBRANCH);

            try
            {
                var pathTempPDF = string.Empty;

                if (printGenerateBM.NTRANSAC == "E")
                {
                    pathTempPDF = printPathsVM.SRUTA_DESTINO + GetValueConfig("anexo" + codRamo) + "\\" + printGenerateBM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\anexos\\";

                }
                else  //if (printGenerateBM.NTRANSAC == "I" || printGenerateBM.NTRANSAC == "R" || printGenerateBM.NTRANSAC == "M")
                {
                    pathTempPDF = printPathsVM.SRUTA_DESTINO + GetValueConfig("anexo" + codRamo) + "\\" + printGenerateBM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\" + printGenerateBM.DSTARTDATE + "\\anexos\\";
                }

                if (!Directory.Exists(pathTempPDF))
                {
                    Directory.CreateDirectory(pathTempPDF);
                }

                if (printGenerateBM.NCOD_CONDICIONADO == (int)PrintEnum.Condicionado_Poliza.ANEXO_ASISTENCIA)
                {
                    string pathNameFilePDF = nameFilePDF(obj, pathTempPDF, printGenerateBM);

                    var values = new Dictionary<string, string>();
                    values.Add("nbranch", codRamo);
                    values.Add("codUrl", obj.COD_URL);
                    var resReference = PrintGenericService<ResponseGraph, InfoAssistanceQuotationVM>.GetWithParameters(baseUrlApi, "/PolicyManager/getReferenceURL", values);

                    //var resReference = await new GraphqlDA().getReferenceURL(codRamo, obj.COD_URL);

                    if ( resReference.data !=null)
                    {
                        if (!String.IsNullOrEmpty(resReference.data.referenceURL))
                        {
                            using (System.Net.WebClient client = new System.Net.WebClient())
                            {
                                client.DownloadFile(resReference.data.referenceURL, pathNameFilePDF);
                            }
                        }
                       
                    }
                }

                if (printGenerateBM.NCOD_CONDICIONADO == (int)PrintEnum.Condicionado_Poliza.ANEXO_CLAUSULA)
                {
                    string fileClausula = printPathsVM.SRUTA_TEMPLATE + GetValueConfig("anexo" + codRamo) + "\\" + obj.NCOVERGEN + ".pdf";

                    string pathNameFilePDF = nameFilePDF(obj, pathTempPDF, printGenerateBM);

                    if (File.Exists(fileClausula))
                    {
                        File.Copy(fileClausula, pathNameFilePDF, true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ELog.saveDocumentos(this, ex.ToString());
            }

            //}
        }

        public string nameFilePDF(AnexoPDF obj, string pathTempPDF, PolicyPrintVM printGenerateBM)
        {
            string pathNameFilePDF = string.Empty;

            if (printGenerateBM.NCOD_CONDICIONADO == (int)PrintEnum.Condicionado_Poliza.ANEXO_ASISTENCIA)
            {
                pathNameFilePDF = pathTempPDF + "99_1_" + obj.SDESCRIPT_ASSIT + ".pdf";
            }

            if (printGenerateBM.NCOD_CONDICIONADO == (int)PrintEnum.Condicionado_Poliza.ANEXO_CLAUSULA)
            {
                pathNameFilePDF = pathTempPDF + "99_2_" + obj.NCOVERGEN + ".pdf";
            }

            if (File.Exists(pathNameFilePDF))
            {
                File.Delete(pathNameFilePDF);
            }

            return pathNameFilePDF;
        }

        public CoverResponseVM generateCoverList(List<CertificateCoverVM> coverList)
        {
            var response = new CoverResponseVM();

            response.coverNotSubItemList = new List<CertificateCoverVM>(coverList.Where(x => !String.IsNullOrEmpty(x.NR)));
            response.coverMainList = new List<CertificateCoverVM>(coverList.Where(x => x.SCOVERUSE == "1"));
            response.coverAditionalList = new List<CertificateCoverVM>(coverList.Where(x => x.SCOVERUSE == "0"));
            response.coverMainNotSubItemList = new List<CertificateCoverVM>(coverList.Where(x => x.SCOVERUSE == "1").Where(x => !String.IsNullOrEmpty(x.NR)));
            response.coverAditionalNotSubItemList = new List<CertificateCoverVM>(coverList.Where(x => x.SCOVERUSE == "0").Where(x => !String.IsNullOrEmpty(x.NR)));
            response.coverLimitZeroList = new List<CertificateCoverVM>(coverList.Where(x => Convert.ToInt32(x.NCOVER_LIMIT) > 0));
            response.coverClauseList = new List<CertificateCoverVM>(generateClause(response.coverAditionalNotSubItemList));
            
            var listFlagAdc = new List<bool> {false,false };
            listFlagAdc[0] = response.coverAditionalList.Count > 0 ? true : false;
            listFlagAdc[1] = response.coverAditionalNotSubItemList.Count > 0 ? true : false;
            if (!listFlagAdc[0])
            {
                response.coverAditionalList.Add(new CertificateCoverVM { SDESCRIPT_COVER = "NO APLICA" });
            }
            
            if (!listFlagAdc[1])
            {
                response.coverAditionalNotSubItemList.Add(new CertificateCoverVM { SDESCRIPT_COVER = "NO APLICA" });
            }

            return response;
        }

        public List<CertificateCoverVM> generateClause(List<CertificateCoverVM> coverClauseList)
        {
            foreach (var item in coverClauseList)
            {
                item.SDESCRIPT_COVER = "Cláusula de " + item.SDESCRIPT_COVER;
            }

            return coverClauseList;
        }

        public static void zipFiles(string pathCertificateFileName, string pathFileName)
        {
            try
            {
                if (File.Exists(pathFileName))
                {
                    File.Delete(pathFileName);

                }

                ZipFile.CreateFromDirectory(pathCertificateFileName, pathFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string getCorrelativeAlphabet()
        {
            string stralphabet = alphabet.Substring(count, 1).ToString();
            count++;
            return stralphabet;
        }

        public int getCorrelativeNumber()
        {
            correlativeNumber++;
            return correlativeNumber;
        }

        public PrintResponseVM joinDocuments(string pathPDF, string pathCertifPDF, string nameAttachedPdf, int productId, int branchId)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            string pathAttachedAnexos = pathPDF + "\\anexos\\";
            string pathAttachedTemp = pathPDF + "\\temp\\";

            if (File.Exists(pathPDF + "certificados.zip"))
            {
                File.Delete(pathPDF + "certificados.zip");
            }

            try
            {
                string pathNameFilePDF = pathPDF + nameAttachedPdf;
                DeleteFile(pathNameFilePDF);

                if (!Directory.Exists(pathAttachedTemp))
                {
                    Directory.CreateDirectory(pathAttachedTemp);
                }
                else
                {
                    Directory.Delete(pathAttachedTemp, true);
                    Directory.CreateDirectory(pathAttachedTemp);
                }

                string[] fileDocuments = Directory.GetFiles(pathPDF);
                string[] fileCertificates = !string.IsNullOrEmpty(pathCertifPDF) ? Directory.GetFiles(pathCertifPDF) : new string[] { };

                if (new string[] { GetValueConfig("vidaLeyBranch"), GetValueConfig("sctrBranch") }.Contains(branchId.ToString()))
                {
                    foreach (var item in new DirectoryInfo(pathPDF).GetFiles()
                        .Where(x => x.Name != GetValueConfig("constanciaProvisional" + branchId)).ToList().OrderBy(x => x.CreationTime).ToList())
                    {
                        string fileToMove = pathPDF + item.Name;
                        string moveTo = pathAttachedTemp + item.Name;
                        File.Copy(fileToMove, moveTo);
                    }
                }
                else
                {
                    foreach (var item in new DirectoryInfo(pathPDF).GetFiles().OrderBy(x => x.CreationTime).ToList())
                    {
                        string fileToMove = pathPDF + item.Name;
                        string moveTo = pathAttachedTemp + item.Name;
                        File.Copy(fileToMove, moveTo);
                    }
                }

                if (!string.IsNullOrEmpty(pathCertifPDF))
                {
                    foreach (var item in new DirectoryInfo(pathCertifPDF).GetFiles().OrderBy(x => x.CreationTime).ToList())
                    {
                        string fileToMove = pathCertifPDF + item.Name;
                        string moveTo = pathAttachedTemp + item.Name;
                        File.Copy(fileToMove, moveTo);
                    }
                }

                if (Directory.Exists(pathAttachedAnexos))
                {
                    foreach (var item in new DirectoryInfo(pathAttachedAnexos).GetFiles().OrderBy(x => x.CreationTime).ToList())
                    {
                        string fileToMove = pathAttachedAnexos + item.Name;
                        string moveTo = pathAttachedTemp + item.Name;
                        File.Copy(fileToMove, moveTo);
                    }

                    Directory.Delete(pathAttachedAnexos, true); // Se elimina la carpeta de anexos
                }

                List<string> listaDocsPaths = new List<string>();
                foreach (var item in new DirectoryInfo(pathAttachedTemp).GetFiles().OrderBy(x => x.CreationTime).ToList())
                {
                    listaDocsPaths.Add(pathAttachedTemp + item.Name);
                }

                var document = new PdfSharp.Pdf.PdfDocument();
                foreach (string pdfFile in listaDocsPaths)
                {
                    try
                    {
                        using (var inputPDFDocument = PdfSharp.Pdf.IO.PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import))
                        {
                            // document.Version = inputPDFDocument.Version;
                            foreach (PdfSharp.Pdf.PdfPage page in inputPDFDocument.Pages)
                            {
                                document.AddPage(page);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //ELog.save(this, ex.ToString());
                    }
                }

                XFont font = new XFont("Verdana", 9);
                XBrush brush = XBrushes.Black;
                string noPages = document.Pages.Count.ToString();
                for (int i = 0; i < document.Pages.Count; ++i)
                {
                    PdfSharp.Pdf.PdfPage page = document.Pages[i];
                    XRect layoutRectangle = new XRect(240, page.Height - font.Height - 10, page.Width, font.Height);
                    using (XGraphics gfx = XGraphics.FromPdfPage(page))
                    {
                        gfx.DrawString((i + 1).ToString(), font, brush, layoutRectangle, XStringFormats.Center);
                    }
                }

                document.Save(pathNameFilePDF);
                document.Dispose();
                Directory.Delete(pathAttachedTemp, true);

                response.NCODE = 0;
                response.SMESSAGE = "Se realizó el cuadro de póliza de forma correcta";

            }
            catch (Exception ex)
            {
                Directory.Delete(pathAttachedTemp, true);
                response.NCODE = 1;
                response.SMESSAGE = "joinDocuments " + ex.Message;
            }

            return response;
        }

        public PrintResponseVM joinDocumentsQuotation(string pathPDF, string nameAttachedPdf)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            string pathAttachedTemp = pathPDF + "\\temp\\";

            try
            {
                string pathNameFilePDF = pathPDF + nameAttachedPdf;
                DeleteFile(pathNameFilePDF);

                if (!Directory.Exists(pathAttachedTemp))
                {
                    Directory.CreateDirectory(pathAttachedTemp);
                }
                else
                {
                    Directory.Delete(pathAttachedTemp, true);
                    Directory.CreateDirectory(pathAttachedTemp);
                }

                string[] fileDocuments = Directory.GetFiles(pathPDF);

                foreach (var item in new DirectoryInfo(pathPDF).GetFiles().OrderByDescending(x => x.CreationTime).ToList())
                {
                    string fileToMove = pathPDF + item.Name;
                    string moveTo = pathAttachedTemp + item.Name;
                    File.Copy(fileToMove, moveTo);
                }

                List<string> listaDocsPaths = new List<string>();
                foreach (var item in new DirectoryInfo(pathAttachedTemp).GetFiles().OrderBy(x => x.CreationTime).ToList())
                {
                    listaDocsPaths.Add(pathAttachedTemp + item.Name);
                }

                var document = new PdfSharp.Pdf.PdfDocument();
                foreach (string pdfFile in listaDocsPaths)
                {
                    try
                    {
                        using (var inputPDFDocument = PdfSharp.Pdf.IO.PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import))
                        {
                            foreach (PdfSharp.Pdf.PdfPage page in inputPDFDocument.Pages)
                            {
                                document.AddPage(page);
                            }
                        }
                    }
                    catch (Exception ex) { }
                }

                XFont font = new XFont("Verdana", 9);
                XBrush brush = XBrushes.Black;
                string noPages = document.Pages.Count.ToString();
                for (int i = 0; i < document.Pages.Count; ++i)
                {
                    PdfSharp.Pdf.PdfPage page = document.Pages[i];
                    XRect layoutRectangle = new XRect(240, page.Height - font.Height - 10, page.Width, font.Height);
                    using (XGraphics gfx = XGraphics.FromPdfPage(page))
                    {
                        gfx.DrawString((i + 1).ToString(), font, brush, layoutRectangle, XStringFormats.Center);
                    }
                }

                document.Save(pathNameFilePDF);
                document.Dispose();
                Directory.Delete(pathAttachedTemp, true);
                foreach (string f in fileDocuments)
                {
                    File.Delete(f);
                }
            }
            catch (Exception ex)
            {
                Directory.Delete(pathAttachedTemp, true);
                response.NCODE = 1;
                response.SMESSAGE = "joinDocumentsQuotation " + ex.Message;
            }

            return response;
        }

        public PrintResponseVM generateObjUpdateState(PolicyPrintVM generatePolicy, int nopc, int nstate, string smessage)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            generatePolicy.NOPC = nopc;
            generatePolicy.NSTATE = nstate;
            generatePolicy.SMESSAGE_IMP = smessage;
            response = new PolicyPrintDA().UpdateStatePolicy(generatePolicy);

            return response;
        }

        public PolicyPrintVM generateObjPolicy(PolicyJobVM job, PolicyFormatVM format, List<PolicyProcedureVM> proceduresList = null)
        {
            var generatePolicy = new PolicyPrintVM();

            try
            {
                DateTime fecha = format.DSTARTDATE != null ? Convert.ToDateTime(format.DSTARTDATE) : DateTime.Today;

                generatePolicy = new PolicyPrintVM()
                {
                    NCOD_CONDICIONADO = format.NCOD_CONDICIONADO,
                    NPOLICY = format.NPOLICY,
                    PROCEDURE_LIST = proceduresList,
                    NIDHEADERPROC = job.NIDHEADERPROC,
                    NIDDETAILPROC = job.NIDDETAILPROC,
                    SCERTYPE = format.SCERTYPE,
                    NBRANCH = format.NBRANCH,
                    NPRODUCT = format.NPRODUCT,
                    NMOVEMENT = format.NMOVEMENT,
                    STRANSAC = format.STRANSAC,
                    SCONDICIONADO = format.SCONDICIONADO,
                    NNTRANSAC = format.NNTRANSAC,
                    NORDER = format.NORDER,
                    DSTARTDATE = format.NBRANCH == Convert.ToInt32(GetValueConfig("sctrBranch")) ? String.Format("{0:dd/MM/yyyy}", fecha).Replace("/", "") : fecha.ToShortDateString().Replace("/", ""),
                    NTRANSAC = format.NTRANSAC
                };
            }
            catch (Exception ex)
            {
                generatePolicy = new PolicyPrintVM()
                {
                    NIDHEADERPROC = job.NIDHEADERPROC,
                    NIDDETAILPROC = job.NIDDETAILPROC
                };
            }

            return generatePolicy;
        }

        public PrintResponseVM sendDocumentsService(PolicyPrintVM printGenerateBM, PolicyDataVM baseDataQuotationVM, List<PolicyUserVM> userInfoList, string nameAttachDoc)
        {
            var response = new PrintResponseVM() { NCODE = 0 };

            try
            {
                WsGenerarPDFReference.WsGenerarPDFexternoClient client = new WsGenerarPDFReference.WsGenerarPDFexternoClient();

                if (printGenerateBM != null)
                {
                    string templateName = new PolicyPrintDA().GetTemplateName(printGenerateBM);
                    templateName = string.IsNullOrEmpty(templateName) ? GetValueConfig("nombrePlanillaPL" + printGenerateBM.NBRANCH + printGenerateBM.NPRODUCT) : templateName;

                    var listDoc = new List<ReportDocPDF>();
                    var report = new ReportDocPDF()
                    {
                        Nombre = nameAttachDoc.Substring(0, nameAttachDoc.IndexOf(".")),
                        Poliza = printGenerateBM.NPOLICY.ToString(),
                        SumaAsegurada = "0",
                        Certificado = "1",
                        NombrePlantilla = templateName
                    };

                    listDoc.Add(report);

                    var listUser = new List<ReportUserPDF>();
                    foreach (var userItem in userInfoList)
                    {
                        if (!string.IsNullOrEmpty(userItem.sdocumento.Trim()) && !string.IsNullOrEmpty(userItem.semail.Trim()))
                        {
                            var user = new ReportUserPDF();
                            user.Nombre = userItem.slegalName == "" ? userItem.snombre : userItem.slegalName;
                            user.Apellido = (userItem.sapePat + " " + userItem.sapeMat).Trim();
                            user.Correo = userItem.semail == "" ? "" : userItem.semail;
                            user.DNI = userItem.sdocumento;

                            user.Documentos = new List<Documento>();
                            var item = new Documento();
                            item.nombre = nameAttachDoc.Substring(0, nameAttachDoc.IndexOf("."));
                            user.Documentos.Add(item);
                            listUser.Add(user);
                        }
                    }

                    string informacionDocumentos = JsonConvert.SerializeObject(listDoc);
                    string informacionUsuarios = JsonConvert.SerializeObject(listUser);
                    string rutaDirectorio = string.Empty;

                    if (printGenerateBM.NTRANSAC == "E")
                    {
                        rutaDirectorio = GetValueConfig("pathImpresion" + printGenerateBM.NBRANCH) + "" + printGenerateBM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\";
                    }
                    else
                    {
                        rutaDirectorio = GetValueConfig("pathImpresion" + printGenerateBM.NBRANCH) + "" + printGenerateBM.NPOLICY + "\\" + printGenerateBM.NIDHEADERPROC + "\\" + printGenerateBM.STRANSAC + "\\" + printGenerateBM.DSTARTDATE + "\\";
                    }

                    var jsonEPolicy = client.CargaDocumentoExterno(informacionDocumentos, informacionUsuarios, rutaDirectorio);
                    var resEPolicy = JsonConvert.DeserializeObject<PolicyElecResponseVM>(jsonEPolicy, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    response.NCODE = resEPolicy.Estatus == "ERROR" ? 1 : 0;
                    response.SMESSAGE = resEPolicy.Estatus == "ERROR" ? resEPolicy.Error + " | Ruta: " + rutaDirectorio : "Se envió correctamente a Póliza electrónica";
                }
                else
                {
                    response.NCODE = 0;
                    response.SMESSAGE = "Se culminó correctamente la generación de documentos";
                }

            }
            catch (Exception ex)
            {
                response.NCODE = 1;
                response.SMESSAGE = ex.Message;
            }

            return response;
        }

        #region Guardar Log
        public static void saveLog(string obj, string msj, string process)
        {
            try
            {
                string fecha = System.DateTime.Now.ToString("yyyyMMdd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");
                string path = "D:/log/GeneradorDocumentos/";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string pathName = path + process + "_" + fecha + ".txt";
                StreamWriter sw = new StreamWriter(pathName, true);

                StackTrace stacktrace = new StackTrace();
                sw.WriteLine(obj + ": " + hora);
                sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + msj);
                sw.WriteLine("");
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
