using DevTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevTest.Controllers
{
    public class ProvinciaController : Controller
    {
        
        public ActionResult Index(string id)
        {
            ApiRequest request = new ApiRequest();
            List<DataCovid> regiones = request.ObtenerProvincias(id);
            Session.Add("id", id);
            return View(regiones);
        }

        public FileResult ExpotarXML()
        {
            ApiRequest request = new ApiRequest();
            TipoDatos xml = new TipoDatos();
            return xml.ExpotarXML(request.ObtenerProvincias(Session["id"].ToString()));
        }

        public FileResult ExportarJson()
        {
            ApiRequest request = new ApiRequest();
            TipoDatos xml = new TipoDatos();
            return xml.ExpotarXML(request.ObtenerProvincias(Session["id"].ToString()));
        }

        public FileResult ExportarCSV()
        {
            ApiRequest request = new ApiRequest();
            TipoDatos xml = new TipoDatos();
            return xml.ExportarCSV(request.ObtenerProvincias(Session["id"].ToString()));
        }
         
    }
}