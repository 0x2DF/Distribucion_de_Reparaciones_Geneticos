using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace Proyecto2IAweb
{
    public partial class Default : System.Web.UI.Page
    {

        private Algorithm algol;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["services"] == null)
            {
                Session["services"] = load_services();
            }
            if (Session["orders"] != null)
            {
                Fill_Table_Ordens();
            }
            if (Session["agents"] != null)
            {
                Fill_Table_Agents();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Debug.Print(getFile());
            parseXML(getFile());

            if (Session["agents"] != null && Session["orders"] != null)
            {
                algol = new Algorithm(0, //Politic 
                    3, //Crossings
                    (float)0.009, //Mutation Probability
                    10, //Population Size
                    (float)0.5, //Cross Probability
                    (Dictionary<string, Service>)(Session["services"]), //Services 
                    (Dictionary<int, Agent>)(Session["agents"]), //Agents
                    (Dictionary<int, Order>)(Session["orders"])); //Orders
                //for (int i = 0; i < 70; i++) {
                //    algol.NextGeneration();
                //}
                Fill_People_Pills();
                Fill_People_Pills_Info();
            }
        }

        private void Fill_Table_Agents()
        {
            Table1.Rows.Clear();
            int numcells = 3;
            TableHeaderRow thr = new TableHeaderRow();
            thr.Attributes["class"] = "thead-dark";
            for (int i = 0; i < numcells; i++) {
                TableHeaderCell thc = new TableHeaderCell();
                thc.Attributes["scope"] = "col";
                if (i == 0) thc.Controls.Add(new LiteralControl("ID"));
                else if (i == 1) thc.Controls.Add(new LiteralControl("Nombre de Agente"));
                else thc.Controls.Add(new LiteralControl("Códigos que Atiende"));
                thr.Controls.Add(thc);
            }
            Table1.Controls.Add(thr);
            Dictionary<int, Agent> agents = (Dictionary<int, Agent>)(Session["agents"]);
            foreach (KeyValuePair<int, Agent> agent in agents)
            {
                TableRow r = new TableRow();
                for (int j = 0; j < numcells; j++)
                {
                    TableCell c = new TableCell();
                    if (j == 0)
                    {
                        c.Controls.Add(new LiteralControl(agent.Key.ToString()));
                        c.Font.Bold.ToString();
                    }
                    else if (j == 1)
                    {
                        c.Controls.Add(new LiteralControl(agent.Value.Name));
                    }
                    else
                    {
                        string serviceCodes = "";
                        for(int i = 0; i < agent.Value.ServiceCodes.Count; i++) {
                            serviceCodes += string.Format("{0}\n", agent.Value.ServiceCodes[i]);
                        }
                        c.Controls.Add(new LiteralControl(serviceCodes));
                    }
                    r.Controls.Add(c);
                }
                Table1.Rows.Add(r);
            }
        }

        private void Fill_Table_Ordens()
        {
            Table2.Rows.Clear();
            int numcells = 3;
            TableHeaderRow thr = new TableHeaderRow();
            thr.Attributes["class"] = "thead-dark";
            for (int i = 0; i < numcells; i++)
            {
                TableHeaderCell thc = new TableHeaderCell();
                thc.Attributes["scope"] = "col";
                if (i == 0) thc.Controls.Add(new LiteralControl("ID"));
                else if (i == 1) thc.Controls.Add(new LiteralControl("Nombre de Cliente"));
                else thc.Controls.Add(new LiteralControl("Códigos de Servicio"));
                thr.Controls.Add(thc);
            }
            Table2.Controls.Add(thr);
            Dictionary<int, Order> ordenes = (Dictionary<int, Order>)(Session["orders"]);
            foreach (KeyValuePair<int, Order> order in ordenes) {
                TableRow r = new TableRow();
                for (int j = 0; j < numcells; j++)
                {
                    TableCell c = new TableCell();
                    if (j == 0)
                    {
                        c.Controls.Add(new LiteralControl(order.Key.ToString()));
                        c.Font.Bold.ToString();
                    }
                    else if (j == 1)
                    {
                        c.Controls.Add(new LiteralControl(order.Value.Name));
                    }
                    else
                    {
                        c.Controls.Add(new LiteralControl(order.Value.ServiceCode));
                    }
                    r.Controls.Add(c);
                }
                Table2.Rows.Add(r);
            }
        }

        private void Fill_People_Pills()
        {
            int totalPeople = 5;
            for (int i = 0; i < totalPeople; i++)
            {
                HyperLink h = new HyperLink();
                h.CssClass = "nav-link";
                h.Attributes["aria-selected"] = "false";
                if (i == 0)
                {
                    h.CssClass += " active";
                    h.Attributes["aria-selected"] = "true";
                }
                h.Attributes["data-toggle"] = "pill";
                h.Attributes["href"] = string.Format("#v_pills_{0}", i.ToString());
                h.Attributes["id"] = string.Format("v_pills_{0}_tab", i.ToString());
                h.Attributes["role"] = "tab";
                h.Attributes["aria-controls"] = string.Format("v_pill_{0}", i.ToString());
                h.Controls.Add(new LiteralControl(string.Format("Persona {0}", i.ToString())));
                Repeater1.Controls.Add(h);
            }
        }

        private void Fill_People_Pills_Info()
        {
            int total = 5;
            for (int i = 0; i < total; i++)
            {
                Panel p = new Panel();
                p.Attributes["class"] = "tab-pane fade";
                if (i == 0)
                {
                    p.Attributes["CssClass"] += " show active";
                }
                p.Attributes["role"] = "tabpanel";
                p.Attributes["aria-labelletdby"] = string.Format("v_pills_{0}_tab", (i + 1).ToString());
                p.Attributes["id"] = string.Format("v_pills_{0}", i.ToString());
                Table t = new Table();
                t.Attributes["class"] = "table table-borderless";
                TableHeaderRow thr = new TableHeaderRow();
                TableRow r = new TableRow();
                for (int j = 0; j < 4; j++)
                {
                    TableCell c = new TableCell();
                    TableHeaderCell thc = new TableHeaderCell();
                    thc.Attributes["scope"] = "col";
                    if (j == 0)
                    {
                        thc.Controls.Add(new LiteralControl("ID"));
                        c.Controls.Add(new LiteralControl(j.ToString()));
                        c.Attributes["scope"] = "row";
                    }
                    else if (j == 1)
                    {
                        thc.Controls.Add(new LiteralControl("Nombre"));
                        c.Controls.Add(new LiteralControl("Nombre Persona"));
                    }
                    else if (j == 2)
                    {
                        thc.Controls.Add(new LiteralControl("Total de Comisión"));
                        c.Controls.Add(new LiteralControl("10000"));
                    }
                    else
                    {
                        thc.Controls.Add(new LiteralControl("Total de Horas de Atención"));
                        c.Controls.Add(new LiteralControl("90"));
                    }
                    thr.Controls.Add(thc);
                    r.Controls.Add(c);
                }
                t.Controls.Add(thr);
                t.Controls.Add(r);
                p.Controls.Add(t);

                var l1 = new HtmlGenericControl("h3");
                l1.InnerHtml = "Servicios";
                p.Controls.Add(l1);

                HtmlGenericControl unlist = new HtmlGenericControl("ul");
                
                int totalServices = 3;
                for (int j = 0; j < totalServices; j++) {
                    HtmlGenericControl list = new HtmlGenericControl("li");
                    list.Attributes["class"] = "list-group-item";
                    HtmlGenericControl panelList = new HtmlGenericControl("div");
                    panelList.Attributes["class"] = "container";

                    HtmlGenericControl id = new HtmlGenericControl("p");
                    id.InnerHtml = "<strong>ID :</strong> 1";
                    panelList.Controls.Add(id);
                    HtmlGenericControl ser = new HtmlGenericControl("p");
                    ser.InnerHtml = "<strong>Código de Servicio :</strong> ABCD";
                    panelList.Controls.Add(ser);
                    HtmlGenericControl hor = new HtmlGenericControl("p");
                    hor.InnerHtml = "<strong>Horas de atención :</strong> 90";
                    panelList.Controls.Add(hor);
                    HtmlGenericControl com = new HtmlGenericControl("p");
                    com.InnerHtml = "<strong>Comisión :</strong> 10000";
                    panelList.Controls.Add(com);
                    list.Controls.Add(panelList);
                    unlist.Controls.Add(list);
                }
                p.Controls.Add(unlist);

                v_pills_tabContent.Controls.Add(p);
            }
        }


        // LAS RUTAS SON ABSOLUTAS PORQUE NO ESTABA AGARRANDO LA RELATIVA
        // RECORDAR CAMBIARLAS
        // private const string services_xml = @"C:\Users\papin\Desktop\Distribucion_de_Reparaciones_Geneticos\Proyecto2IAweb\Proyecto2IAweb\services.xml";
        // private const string path = @"C:\Users\papin\Desktop\Distribucion_de_Reparaciones_Geneticos\Proyecto2IAweb\Proyecto2IAweb\";
        private const string services_xml = @"C:\Users\Carole\source\repos\Distribucion_de_Reparaciones_Geneticos\Proyecto2IAweb\Proyecto2IAweb\services.xml";
        private const string path = @"C:\Users\Carole\source\repos\Distribucion_de_Reparaciones_Geneticos\Proyecto2IAweb\Proyecto2IAweb\";

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

        private string getFile()
        {
            string filePath = "";
            if (FileUpload1.HasFile)
            {
                filePath = path + FileUpload1.FileName;
                FileUpload1.SaveAs(filePath);
            }
            return filePath;
        }

        private Dictionary<int, Agent> load_agents(XmlDocument xml)
        {
            Dictionary<int, Agent> agents = new Dictionary<int, Agent>();

            var agentsXML = xml["agents"];

            foreach (XmlNode agentXML in agentsXML)
            {
                Agent agent = new Agent();
                foreach (XmlNode agentXML_node in agentXML.ChildNodes)
                {
                    switch (agentXML_node.Name)
                    {
                        case "ID":
                            agent.ID = Int32.Parse(agentXML_node.FirstChild.Value);
                            break;
                        case "name":
                            agent.Name = agentXML_node.FirstChild.Value;
                            break;
                        case "services":
                            foreach (XmlNode service in agentXML_node.ChildNodes)
                            {
                                agent.addService(service.FirstChild.Value);
                            }
                            break;
                        default:
                            Debug.Print("Invalid element in service xml");
                            break;
                    }
                }

                if (agent.Name != null) agents.Add(agent.ID, agent);
            }

            return agents;
        }

        private Dictionary<int, Order> load_orders(XmlDocument xml)
        {
            Dictionary<int, Order> orders = new Dictionary<int, Order>();

            var ordersXML = xml["orders"];

            foreach (XmlNode orderXML in ordersXML)
            {
                Order order = new Order();
                foreach (XmlNode orderXML_node in orderXML.ChildNodes)
                {
                    switch (orderXML_node.Name)
                    {
                        case "ID":
                            order.ID = Int32.Parse(orderXML_node.FirstChild.Value);
                            break;
                        case "client":
                            order.Name = orderXML_node.FirstChild.Value;
                            break;
                        case "service":
                            order.ServiceCode = orderXML_node.FirstChild.Value;
                            break;
                        default:
                            Debug.Print("Invalid element in service xml");
                            break;
                    }
                }

                if (order.Name != null) orders.Add(order.ID, order);
            }

            return orders;
        }

        private void parseXML(string file)
        {
            if (File.Exists(file))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(file);
                if (xml.SelectSingleNode("agents") != null)
                {
                    Session["agents"] = load_agents(xml);
                    Fill_Table_Agents();
                }
                else
                {
                    Session["orders"] = load_orders(xml);
                    Fill_Table_Ordens();
                }

            }
        }

        
    }
}
