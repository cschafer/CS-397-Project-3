using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CS397Project3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NWS.ndfdXML forecast = new NWS.ndfdXML();
            string xmlForecast = forecast.NDFDgenByDay(40, -85, DateTime.Now, "5", NWS.unitType.e, NWS.formatType.Item24hourly);
            XmlDocument weather = new XmlDocument();
            weather.LoadXml(xmlForecast);
            XmlNodeList temperatureNodes = weather.GetElementsByTagName("temperature");
            foreach (XmlNode t in temperatureNodes)
            {
                if (t.Attributes["type"].Value.Equals("maximum"))
                {
                    foreach (XmlNode v in t.ChildNodes)
                    {
                        if (v.Name.Equals("value"))
                        {
                            Response.Write("<p>" + v.InnerText + "</p>");
                        }
                    }
                }
            }
            XmlNodeList weatherNodes = weather.GetElementsByTagName("weather");
            foreach (XmlNode w in weatherNodes)
            {
                foreach (XmlNode wc in w.ChildNodes)
                {
                    if (wc.Name.Equals("weather-conditions"))
                    {
                        Response.Write("<p>" + wc.Attributes["weather-summary"].Value + "</p>");
                    }
                }
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}