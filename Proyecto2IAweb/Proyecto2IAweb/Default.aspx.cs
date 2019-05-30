using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Proyecto2IAweb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Fill_Table_Agents();
            Fill_Table_Services();
            Fill_People_Pills();
            Fill_People_Pills_Info();
        }

        private void Fill_Table_Agents()
        {
            int numrows = 3;
            int numcells = 3;
            for (int i = 0; i < numrows; i++)
            {
                TableRow r = new TableRow();
                for (int j = 0; j < numcells; j++)
                {
                    TableCell c = new TableCell();
                    if (j == 0)
                    {
                        c.Controls.Add(new LiteralControl(j + 1.ToString()));
                        c.Font.Bold.ToString();
                    }
                    else if (j == 1)
                    {
                        c.Controls.Add(new LiteralControl("Nombre de la persona"));
                    }
                    else
                    {
                        c.Controls.Add(new LiteralControl("Cógido que atiendo xD"));
                    }
                    r.Controls.Add(c);
                }
                Table1.Rows.Add(r);
            }
        }

        private void Fill_Table_Services()
        {
            int numrows = 3;
            int numcells = 3;
            for (int i = 0; i < numrows; i++)
            {
                TableRow r = new TableRow();
                for (int j = 0; j < numcells; j++)
                {
                    TableCell c = new TableCell();
                    if (j == 0)
                    {
                        c.Controls.Add(new LiteralControl(j + 1.ToString()));
                        c.Font.Bold.ToString();
                    }
                    else if (j == 1)
                    {
                        c.Controls.Add(new LiteralControl("Nombre de la persona"));
                    }
                    else
                    {
                        c.Controls.Add(new LiteralControl("Cógido que servicio"));
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
    }
}