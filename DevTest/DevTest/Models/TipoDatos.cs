 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization; 
using System.Web.Script.Serialization;
using System.ComponentModel;
using CsvHelper;

namespace DevTest.Models
{
    public class TipoDatos
    {

        public FileResult ExpotarXML(List<DataCovid> listado)
        {
            string strxml = Serializer(typeof(List<DataCovid>), listado);
            var contentType = "text/xml";
            var bytes = System.Text.Encoding.UTF8.GetBytes(strxml);
            var result = new FileContentResult(bytes, contentType);
            result.FileDownloadName = "myfile.xml";
            return result; 
        }

        public FileResult ExportarJson(List<DataCovid> listado) 
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(listado);
            var contentType = "application/json";
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var result = new FileContentResult(bytes, contentType);
            result.FileDownloadName = "myfile.json";
            return result;
        }

        public FileResult ExportarCSV(List<DataCovid> listado) {
            byte[] result;
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    CsvHelper.Configuration.CsvConfiguration configuration = new CsvHelper.Configuration.CsvConfiguration(new System.Globalization.CultureInfo(1));
                    using (var csvWriter = new CsvWriter(streamWriter, configuration))
                    {
                        csvWriter.WriteRecords(listado);
                        streamWriter.Flush();
                        result = memoryStream.ToArray(); 
                    }
                }
            }

            return new FileStreamResult(new MemoryStream(result), "text/csv") { FileDownloadName = "filename.csv" };
             
        } 

        private static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //Serialized object
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }
    
    
    }
}