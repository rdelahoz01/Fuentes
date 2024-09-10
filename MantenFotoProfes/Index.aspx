<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MantenFotoProfes.Index" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css"/>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="Js/bootstrap.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        <br />
        <br />
        <div style="display:none">
            <asp:Label ID="lbl_codiprof" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_logincrea" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_fcrea" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_hcrea" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_user" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_estado" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_esEncontrado" runat="server" Text=""></asp:Label>
        </div>
        <div class="container">
            <h2>Mantención de Fotografía de Profesionales</h2>
            <hr />
            <asp:Literal ID="alerta" runat="server"></asp:Literal>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Selecciona especialista" CssClass="form-control"></asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <telerik:RadComboBox ID="cmb_buscaEspecialista" runat="server" EnableLoadOnDemand="True" Width="310px" Height="150px" 
                            AutoPostBack="True" CausesValidation="False" DropDownWidth="300px" EmptyMessage="Nombre de especialista"  
                            OnSelectedIndexChanged="cmb_buscaEspecialista_SelectedIndexChanged"  CommandRequest="busca_nombre" CommandArgument="busca_nombre" HighlightTemplatedItems="True" OnItemsRequested="cmb_buscaEspecialista_ItemsRequested">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Rut Profesional" CssClass="form-control"></asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="lbl_rutprof" runat="server" Text="" CssClass="form-control"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Nombre Profesional" CssClass="form-control"></asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="lbl_nombprof" runat="server" Text="" CssClass="form-control"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" Text="Seleccionar Imagen" CssClass="form-control"></asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <telerik:RadAsyncUpload ID="upload" runat="server" MaxFileInputsCount="1"
                            AllowedFileExtensions="jpeg,jpg,gif,png,bmp" HideFileInput="true"></telerik:RadAsyncUpload>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Cargar Imagen" CssClass="form-control"></asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Button ID="btn_cargar" runat="server" Text="Cargar Imagen" CssClass="btn btn-info" OnClick="btn_cargar_Click" />
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="Label8" runat="server" Text="Fotografia" CssClass="form-control"></asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <%--<div class="form-group">--%>
                    <telerik:RadBinaryImage ID="rd_fotografia" runat="server" Height="90px" Width="90px" ResizeMode="Fill" ClientIDMode="Static"/>

                    <%--</div>--%>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Button ID="btn_guardar" runat="server" Text="Guardar Imagen" CssClass="btn btn-primary " OnClick="btn_guardar_Click" Enabled="False" />
                        <asp:Button ID="btn_modificar" runat="server" Text="Modificar Imagen" CssClass="btn btn-primary " Enabled="False" OnClick="btn_modificar_Click"  />
                        <asp:Button ID="btn_eliminar" runat="server" Text="Eliminar Imagen" CssClass="btn btn-primary " Enabled="False" OnClick="btn_eliminar_Click"  />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Button ID="btn_limpiar" runat="server" Text="Limpiar" CssClass="btn btn-success" OnClick="btn_limpiar_Click" />
                    </div>
                </div>
            </div>
            <hr />
            
        </div>
    </form>
    
</body>
</html>
