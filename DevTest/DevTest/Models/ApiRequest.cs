using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTest.Models
{
    public class ApiRequest
    {
        /// <summary>
        /// DEVUELVE LISTADO DE TOP 10 DE REGIONES CON MAYOR NUMERO DE CASOS DE MUERTE POR COVID
        /// </summary>
        /// <returns>LISTADO DE OBJETO REGIONES</returns>
        internal List<DataCovid> ObtenerRegiones()
        {
            List<DataCovid> listado = new List<DataCovid>();
            IRestResponse response = RequestApi();
            // PARSEANDO RESPUESTA
            JObject data = (JObject)JsonConvert.DeserializeObject(response.Content);   
            // RECORRIENDO OBJETO
            foreach (var Registro in data["data"])
            {
                List<JToken> matches = new List<JToken>();
                ObtenerTokenPorNombre(Registro, "region", matches);
                // OBTENIENDO DATA DE REGION ENCONTRADA
                DataCovid regiones = new DataCovid
                {
                    NoMuertes = Registro["deaths"].ToObject<int>(),
                    NoCasos = Registro["confirmed"].ToObject<int>(),
                    Id = matches[0]["name"].ToObject<string>(),
                    iso = matches[0]["iso"].ToObject<string>()
                };
                // AGREGANDO OBJETO A LISTADO
                listado.Add(regiones);
            }
            // FILTRAR LISTADO DE REGIONES
            return FiltrarListadoRegiones(listado);
        }


        internal List<DataCovid> ObtenerProvincias(string nombreRegion) {
            List<DataCovid> listado = new List<DataCovid>();
            IRestResponse response = RequestApi(nombreRegion);
            JObject data = (JObject)JsonConvert.DeserializeObject(response.Content);

            foreach (var Registro in data["data"])
            {
                List<JToken> matches = new List<JToken>();
                ObtenerTokenPorNombre(Registro, "region", matches);
                // OBTENIENDO DATA DE REGION ENCONTRADA
                DataCovid Provincias = new DataCovid
                {
                    NoMuertes = Registro["deaths"].ToObject<int>(),
                    NoCasos = Registro["confirmed"].ToObject<int>(),
                    Id = matches[0]["province"].ToObject<string>(),
                    iso = matches[0]["iso"].ToObject<string>()
                };
                // AGREGANDO OBJETO A LISTADO
                listado.Add(Provincias);
            }

            return FiltrarListadoProvincias(listado); 
        }


        private IRestResponse RequestApi(string nombreRegion = "") 
        {
            // REQUEST A API CON RESTSHARP
            //var client = new RestClient("https://covid-19-statistics.p.rapidapi.com/reports?" + DateTime.Now.ToString("yyyy-MM-dd"));
            string strurl = "https://covid-19-statistics.p.rapidapi.com/reports?date=2020-04-16";
            if (!string.IsNullOrEmpty(nombreRegion))
                strurl = strurl + "&region_name=" + nombreRegion;
            var client = new RestClient(strurl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "e4ccd9f78dmsh02f6088237142d3p1cef7cjsn310b1f351263");
            request.AddHeader("x-rapidapi-host", "covid-19-statistics.p.rapidapi.com");
            return client.Execute(request);
        }

        /// <summary>
        /// FILTRA LISTADO DE REGIONES
        /// </summary>
        /// <param name="listado">LISTADO DE REGIONES</param>
        /// <returns>RETORNA LISTADO FILTRADO</returns>
        private List<DataCovid> FiltrarListadoRegiones(List<DataCovid> listado)
        {
            List<DataCovid> regiones = listado
                .GroupBy(n => n.iso)
                .Select(r => new DataCovid
                {
                    Id = r.First().Id,
                    NoCasos = r.Sum(s => s.NoCasos),
                    NoMuertes = r.Sum(m => m.NoMuertes)
                }
                ).OrderByDescending(o => o.NoMuertes).Take(10).ToList();
            return regiones;
        }

        /// <summary>
        /// FILTRA LISTADO DE PROVINCIAS
        /// </summary>
        /// <param name="listado">LISTADO DE PROVINCIAS</param>
        /// <returns>RETORNA LISTADO FILTRADO</returns>
        private List<DataCovid> FiltrarListadoProvincias(List<DataCovid> listado)
        {
            List<DataCovid> Provincias = listado
                .OrderByDescending(o => o.NoMuertes).Take(10).ToList();
            return Provincias;
        }

        /// <summary>
        /// RETORNA UN TOKEN DE UN JSON PROPORCIONADO DE ACUERDO AL NOMBRE DADO
        /// </summary>
        /// <param name="ObjetoJson"></param>
        /// <param name="Nombre"></param>
        /// <param name="Resultado"></param>
        private static void ObtenerTokenPorNombre(JToken ObjetoJson, string Nombre, List<JToken> Resultado)
        {
            if (ObjetoJson.Type == JTokenType.Object)
            {
                foreach (JProperty child in ObjetoJson.Children<JProperty>())
                {
                    if (child.Name == Nombre)
                    {
                        Resultado.Add(child.Value);
                    }
                    ObtenerTokenPorNombre(child.Value, Nombre, Resultado);
                }
            }
            else if (ObjetoJson.Type == JTokenType.Array)
            {
                foreach (JToken child in ObjetoJson.Children())
                {
                    ObtenerTokenPorNombre(child, Nombre, Resultado);
                }
            }
        }
    }
}