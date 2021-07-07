using DevTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DevTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApiRequest request = new ApiRequest();
            List<DataCovid> regiones = request.ObtenerRegiones();
            return View(regiones);
        }


          
        public FileResult ExpotarXML() {
            ApiRequest request = new ApiRequest();
            TipoDatos xml = new TipoDatos();
            return xml.ExpotarXML(request.ObtenerRegiones());
        }

        public FileResult ExportarJson()
        {
            ApiRequest request = new ApiRequest();
            TipoDatos xml = new TipoDatos();
            return xml.ExportarJson(request.ObtenerRegiones());
        }

        public FileResult ExportarCSV()
        {
            ApiRequest request = new ApiRequest();
            TipoDatos xml = new TipoDatos();
            return xml.ExportarCSV(request.ObtenerRegiones());
        }




    }
}