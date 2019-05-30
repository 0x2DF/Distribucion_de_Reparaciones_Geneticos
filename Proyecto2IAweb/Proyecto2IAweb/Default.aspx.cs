using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            //Fill_People_Pills_Services();

            int total = 5;
            int rowHeader = 4;
            //for (int j = 0; j < total; j++)
            //{
            //    Table t = new Table();
            //    t.Attributes["CssClass"] = "table table-borderless";
            //    TableHeaderRow thr = new TableHeaderRow();
            //    for (int i = 0; i < rowHeader; i++)
            //    {
            //        TableHeaderCell thc = new TableHeaderCell();
            //        if (i == 0) thc.Controls.Add(new LiteralControl("ID"));
            //        else if (i == 1) thc.Controls.Add(new LiteralControl("Nombre"));
            //        else if (i == 2) thc.Controls.Add(new LiteralControl("Total de Comisión"));
            //        else thc.Controls.Add(new LiteralControl("Total de Horas de Atención"));
            //        thc.Attributes.Add("Scope", "col");
            //    }

            //    for (int i = 0; i < rowHeader; i++) {
            //        TableRow r = new TableRow();
            //        for (int k = 0; i < rowHeader; k++)
            //        {
            //            TableCell c = new TableCell();
            //            if (k == 0) {
            //                c.Controls.Add(new LiteralControl(j + 1.ToString()));
            //                c.Font.Bold.ToString();
            //            }
            //            else if (k == 1) c.Controls.Add(new LiteralControl("Nombre de la persona"));
            //            else if (k == 2) c.Controls.Add(new LiteralControl("10000"));
            //            else c.Controls.Add(new LiteralControl("90h"));
            //            r.Controls.Add(c);
            //        }
            //        t.Rows.Add(r);
            //    }

            //    v_pills_list.Controls.Add(t);
            //    v_pills_list.Attributes["id"] = string.Format("v_pills_{0}", j.ToString());
            //    v_pills_list.Attributes["role"] = "tabpanel";
            //    v_pills_list.Attributes["aria-labelledby"] = string.Format("v_pills_{0}_tab", j.ToString());
            //}
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

        private void Fill_People_Pills() {
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
                h.Attributes["id"]= string.Format("v_pills_{0}_tab", i.ToString()); 
                h.Attributes["role"] = "tab";
                h.Attributes["aria-controls"] = string.Format("v_pill_{0}", i.ToString());
                h.Controls.Add(new LiteralControl(string.Format("Persona {0}", i.ToString())));
                Repeater1.Controls.Add(h);
            }
        }

        //private void Fill_People_Pills_Services() {
        //    int totalServices = 3;
            
        //    for (int i = 1; i <= totalServices; i++) {
        //        ListItem id = new ListItem();
        //        id.Text = string.Format("ID: {0}", (i-1).ToString());
        //        ListItem servicio = new ListItem();
        //        servicio.Text = string.Format("Código de Servicio: {0}-SER", i.ToString());
        //        ListItem horas = new ListItem();
        //        horas.Text = string.Format("Horas de atención: {0}", (i*5).ToString());
        //        ListItem comision = new ListItem();
        //        comision.Text = string.Format("Comisión: {0}", (i*150).ToString());
        //        BuLS.Items.Add(id);
        //        BuLS.Items.Add(servicio);
        //        BuLS.Items.Add(horas);
        //        BuLS.Items.Add(comision);
        //    }
        //}
    }
}