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
        AdventureWorksLT2017Entities db = new AdventureWorksLT2017Entities();

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

        [WebMethod]
        public Vehiculos AddVehiculosXML(string marca, int numeroPuertas, string color, long km, string tipoVeh, int garantia,
            bool enStock, bool fotografia, string modelo) //Para introducir los datos desde el proyecto de consumidor
        {
            using (db)
            {
                
                Vehiculos veh = new Vehiculos();
                /*
                 * // Con datos preinsertados
                veh.marca = "Kia";
                veh.numeroPuertas = 5;
                veh.color = "Rosa";
                veh.kilometros = 166666;
                veh.tipoVehiculo = "SUV";
                veh.garantia = 27;
                veh.enStock = true;
                veh.fotografia = false;
                veh.modelo = "Nadal";
                */

                // Con datos ya introducidos desde el propio consumidor
                veh.marca = marca;
                veh.numeroPuertas = numeroPuertas;
                veh.color = color;
                veh.kilometros = km;
                veh.tipoVehiculo = tipoVeh;
                veh.garantia = garantia;
                veh.enStock = enStock;
                veh.fotografia = fotografia;
                veh.modelo = modelo;

                db.Vehiculos.Add(veh);
                db.SaveChanges();
                return veh;
            }
        }

    }
}
