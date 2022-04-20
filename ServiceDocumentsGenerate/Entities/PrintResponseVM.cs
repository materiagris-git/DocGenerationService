﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDocumentsGenerate.Entities
{
    public class PrintResponseVM
    {
        public int NCODE { get; set; }
        public string SMESSAGE { get; set; }
        public string PATH_PDF { get; set; }
        public string PATH_PDF_CERTIF { get; set; }
        public string SCONDICIONADO { get; set; }
        public string SRESULTADO_PRINT { get; set; }
        public int NGEN_CD { get; set; }
        public string PATH_GEN { get; set; }
        public int RESULT_SEND { get; set; }
        public bool SEND_PE { get; set; } = false;
    }

    public class ResponseGraph
    {
        public int codError { get; set; }
        public string message { get; set; }
        public GenericResponse data { get; set; }
        public List<Error> errors { get; set; }
    }

    public class GenericResponse
    {
        public string referenceURL { get; set; }
    }

    public class Error
    {
        public List<string> path { get; set; }
        public string errorType { get; set; }
        public List<Location> locations { get; set; }
        public string message { get; set; }
    }

    public class Location
    {
        public int line { get; set; }
        public int column { get; set; }
        public string sourceName { get; set; }
    }
}
