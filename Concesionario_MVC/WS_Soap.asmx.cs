using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml.Serialization;

namespace Concesionario_MVC
{
    /// <summary>
    /// Descripción breve de WS_Soap
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WS_Soap : System.Web.Services.WebService
    {


        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public Vehiculos GetVehiculosXML()
        {
            Vehiculos vehs = new Vehiculos();

                vehs.marca = "Dacia";
                vehs.color = "Verde";

            return vehs;
        }
    }
}
