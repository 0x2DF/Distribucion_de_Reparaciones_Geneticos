using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Diagnostics;

namespace Distribucion_de_Reparaciones_Geneticos
{
    public partial class Form1 : Form
    {
        private const string services_xml = "..\\..\\services.xml";

        public Form1()
        {
            InitializeComponent();
        }

        private Dictionary<string, Service> load_services()
        {
            Dictionary<string, Service> services = new Dictionary<string, Service>();

            XmlDocument xml = new XmlDocument();
            xml.Load(services_xml);
            
            var tasks = xml["Services"];

            foreach (XmlNode node in tasks)
            {
                Service service = new Service();
                foreach (XmlNode child_node in node.ChildNodes)
                {
                    switch (child_node.Name)
                    {
                        case "code":
                            service.Code = child_node.FirstChild.Value;
                            break;
                        case "description":
                            service.Description = child_node.FirstChild.Value;
                            break;
                        case "duration":
                            service.Duration = Int32.Parse(child_node.FirstChild.Value);
                            break;
                        case "commission":
                            service.Commission = Int32.Parse(child_node.FirstChild.Value);
                            break;
                        default:
                            Debug.Print("Invalid element in service xml");
                            break;
                    }
                }
                
                if (service.Code != null) services.Add(service.Code, service);
            }

            return services;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<string, Service> services = load_services();

            foreach(KeyValuePair<string, Service> entry in services)
            {
                Service service = entry.Value;
                Debug.Print("Code : " + service.Code + "\nDescription : " + service.Description + "\nDuration : " + service.Duration + "\nCommission : " + service.Commission);
                Debug.Print("\n\n");
            }
        }
    }
}
