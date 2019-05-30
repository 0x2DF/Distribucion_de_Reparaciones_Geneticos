<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto2IAweb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Distribucion de reparaciones</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/javascript.util/0.12.12/javascript.util.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="text-center" style="background-color: mediumblue; color: white;">Distribución de reparaciones</h1>
            <br />
            <div class="input-group mb-3 text-center">
                <label>Seleccione la lista de agentes de servicio</label>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="Button1" runat="server" Text="Cargar" CssClass="btn btn-primary btn-md" OnClick="Button1_Click" />
            </div>
        </div>
        <br />
        <div class="container">
            <h2 class="text-center">Agentes</h2>
            <span>1-0 de 50</span>
            <asp:Table CssClass="table text-center" ID="Table1" Caption="Agentes de servicio" runat="server">
                <asp:TableHeaderRow CssClass="thead-dark">
                    <asp:TableHeaderCell Scope="Column">ID</asp:TableHeaderCell>
                    <asp:TableHeaderCell Scope="Column">Nombre de Agente</asp:TableHeaderCell>
                    <asp:TableHeaderCell Scope="Column">Códigos que atiende</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <%--<nav aria-label="Page navigation example">
              <ul class="pagination">
                <li class="page-item">
                  <a class="page-link" href="#" aria-label="Previous">
                    <span aria-hidden="true">&laquo;</span>
                  </a>
                </li>
                <li class="page-item"><a class="page-link" href="#">1</a></li>
                <li class="page-item"><a class="page-link" href="#">2</a></li>
                <li class="page-item"><a class="page-link" href="#">3</a></li>
                <li class="page-item">
                  <a class="page-link" href="#" aria-label="Next">
                    <span aria-hidden="true">&raquo;</span>
                  </a>
                </li>
              </ul>
            </nav>--%>
        </div>
        <br />
        <br />
        <div class="container">
            <h2 class="text-center">Servicios</h2>
            <span>1-0 de 50</span>
            <asp:Table CssClass="table text-center" ID="Table2" Caption="Ordenes de Servicio" runat="server">
                <asp:TableHeaderRow CssClass="thead-dark">
                    <asp:TableHeaderCell Scope="Column">ID</asp:TableHeaderCell>
                    <asp:TableHeaderCell Scope="Column">Nombre de Cliente</asp:TableHeaderCell>
                    <asp:TableHeaderCell Scope="Column">Códigos de Servicio</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <%--<nav aria-label="Page navigation example">
              <ul class="pagination">
                <li class="page-item">
                  <a class="page-link" href="#" aria-label="Previous">
                    <span aria-hidden="true">&laquo;</span>
                  </a>
                </li>
                <li class="page-item"><a class="page-link" href="#">1</a></li>
                <li class="page-item"><a class="page-link" href="#">2</a></li>
                <li class="page-item"><a class="page-link" href="#">3</a></li>
                <li class="page-item">
                  <a class="page-link" href="#" aria-label="Next">
                    <span aria-hidden="true">&raquo;</span>
                  </a>
                </li>
              </ul>
            </nav>--%>
        </div>
        <br />
        <div class="container">
            <h2 class="text-center">Distribución final</h2>
            <div class="row">
                <div class="col-3">
                    <div class="nav flex-column nav-pills" id="v_pills_tab" role="tablist" aria-orientation="vertical">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="col-9">
                    <asp:Panel ID="v_pills_tabContent" runat="server" CssClass="tab-content" >
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
