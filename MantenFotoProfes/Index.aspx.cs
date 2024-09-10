using System;
using System.Data;
using System.IO;
using System.Web.UI;
//using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LibNegocios;
using System.Collections;
using System.Drawing;
//using System.Drawing;

namespace MantenFotoProfes
{
    public partial class Index : System.Web.UI.Page
    {
        #region[OBJETOS]
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        NegSalud objN = new NegSalud();
        NegWeb objN2 = new NegWeb();
        #endregion //FIN OBJETOS
        #region [VARIABLES]
        bool esExito = false;
        DateTime dttFecha = DateTime.Now.Date;
        string dttHora = DateTime.Now.ToString("hh:mm");
        #endregion //FIN VARIABLES
        #region [METODOS]
        public DateTime parseDate(string valor)
        {
            DateTime tmp = new DateTime();
            DateTime r_valor = DateTime.Parse("31/12/9999");

            if (DateTime.TryParse(valor, out tmp))
                r_valor = tmp;

            return r_valor;
        }
        public TimeSpan parseTimeSpan(string valor)
        {
            TimeSpan tmp = new TimeSpan();
            TimeSpan r_valor = TimeSpan.Parse("00:00");

            if (TimeSpan.TryParse(valor, out tmp))
                r_valor = tmp;

            return r_valor;
        }
        public byte[] imageToByteArray(string imagen)
        {
            byte[] array = null;
            try
            {
                _ = string.Empty;
                _ = new FileInfo(imagen).Extension;
                FileStream fileStream = new FileStream(imagen, FileMode.Open);
                BufferedStream bufferedStream = new BufferedStream(fileStream);
                byte[] array2 = new byte[bufferedStream.Length];
                bufferedStream.Read(array2, 0, array2.Length);
                array = array2;
                fileStream.Close();
                return array;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool bytesAreValid(byte[] valor)
        {
            if (valor == null)
                return false;

            return !Array.Exists(valor, x => x == 0);
        }
        public byte[] dataValue
        {
            get
            {
                try
                {
                    if (bytesAreValid((byte[])Session["imagenBinary"]))
                        return imageToByteArray("~/images/avatar.jpg");
                    else
                        return (byte[])Session["imagenBinary"];
                }
                catch
                {
                    return imageToByteArray("~/images/avatar.jpg");
                }
            }
            set
            {
                try
                {
                    if (bytesAreValid((byte[])value))
                        Session["imagenBinary"] = imageToByteArray("~/images/avatar.jpg");
                    else
                        Session["imagenBinary"] = value;
                }
                catch
                {
                    Session["imagenBinary"] = imageToByteArray("~/images/avatar.jpg");
                }
            }
        }
        public void limpiar()
        {
            lbl_user.Text = "";
            lbl_logincrea.Text = "";
            lbl_rutprof.Text = "";
            lbl_estado.Text = "0";
            lbl_esEncontrado.Text = "0";
            alerta.Text = string.Empty;
            lbl_nombprof.Text = "";
            rd_fotografia.ImageUrl = "~/images/avatar.jpg";
            ((StateManagedCollection)(object)cmb_buscaEspecialista.Items).Clear();
            cmb_buscaEspecialista.Text = "";
            btn_guardar.Enabled = false;
            btn_modificar.Enabled = false;
            btn_eliminar.Enabled = false;
        }
        public void limpiar2()
        {
            lbl_user.Text = "";
            lbl_logincrea.Text = "";
            lbl_rutprof.Text = "";
            lbl_estado.Text = "0";
            lbl_esEncontrado.Text = "0";
            lbl_nombprof.Text = "";
            rd_fotografia.ImageUrl = "~/images/avatar.jpg";
            ((StateManagedCollection)(object)cmb_buscaEspecialista.Items).Clear();
            cmb_buscaEspecialista.Text = "";
            btn_guardar.Enabled = false;
            btn_modificar.Enabled = false;
            btn_eliminar.Enabled = false;
        }
        public void ListaProfesionales()
        {
            try
            {
                ds = objN.ListaProfesionales();
                if (ds.Tables["table"].Rows.Count > 0)
                {
                    cmb_buscaEspecialista.Items.Clear();
                    foreach (DataRow linea in ds.Tables["Table"].Rows)
                    {
                        RadComboBoxItem item = new RadComboBoxItem(linea["nombprof"].ToString(), linea["rut"].ToString());
                        cmb_buscaEspecialista.Items.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public void ListaProfesionales(string strNombre)
        {
            try
            {
                ds = objN.ListaProfesionales(strNombre.ToString().ToUpper());
                if (ds.Tables["table"].Rows.Count > 0)
                {
                    cmb_buscaEspecialista.Items.Clear();
                    foreach (DataRow linea in ds.Tables["Table"].Rows)
                    {
                        RadComboBoxItem item = new RadComboBoxItem(linea["nombprof"].ToString(), linea["rut"].ToString());
                        cmb_buscaEspecialista.Items.Add(item);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void cargar_imagen()
        {
            string imagen = string.Empty;
            int num = 80;
            var w = 0;
            var h = 0;
            lbl_estado.Text = "0";
            try
            {
                foreach (UploadedFile item in (CollectionBase)(object)upload.UploadedFiles)
                {
                    UploadedFile val = item;
                    string path = "C:\\temp";
                    if (!Directory.Exists("C:\\temp"))
                    {
                        Directory.CreateDirectory("C:\\temp");
                    }
                    using (Bitmap bitmap = new Bitmap(val.InputStream))
                    {
                        int width = num;
                        int height = (bitmap.Height * num) / bitmap.Width;

                        Bitmap bitmap2 = new Bitmap(width, height);
                        using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap2))
                        {
                            graphics.DrawImage(bitmap, 0, 0, width, height);
                        }
                        string text = Path.Combine(path, $"{val.GetNameWithoutExtension()}{val.GetExtension()}");
                        bitmap2.Save(text);
                        imagen = text;
                    }
                    byte[] array = imageToByteArray(imagen);
                    rd_fotografia.DataValue = (array);
                    int num2 = array.Length;
                    if (num2 >= 23000)
                    {
                        MensageBox("success", "  La fotografía es demaciado grande, favor reducir tamaño");
                        btn_modificar.Enabled = false;
                        btn_eliminar.Enabled = false;
                        btn_guardar.Enabled = false;
                        lbl_estado.Text = "2";
                        continue;
                    }
                    dataValue = array;
                    lbl_estado.Text = "1";
                    alerta.Text = string.Empty;
                    if (lbl_esEncontrado.Text == "1")
                    {
                        btn_guardar.Enabled = false;
                        btn_modificar.Enabled = true;
                        btn_eliminar.Enabled = true;
                    }
                    else
                    {
                        btn_guardar.Enabled = true;
                        btn_modificar.Enabled = false;
                        btn_eliminar.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void MensageBox(string strTipo, string strMensage)
        {
            string html = string.Empty;
            switch (strTipo)
            {
                case "danger":
                    html += "<div class='alert alert-danger alert-dismissible fade show' role='alert'>";
                    html += "<strong>¡Atención!</strong>" + strMensage;
                    html += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>";
                    html += "<span aria-hidden='true'>&times;</span>";
                    html += "</button>";
                    html += "</div>";
                    break;
                case "success":
                    html += "<div class='alert alert-success alert-dismissible fade show' role='alert'>";
                    html += "<strong>¡Atención!</strong>" + strMensage;
                    html += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>";
                    html += "<span aria-hidden='true'>&times;</span>";
                    html += "</button>";
                    html += "</div>";
                    break;
                case "warning":
                    html += "<div class='alert alert-warning alert-dismissible fade show' role='alert'>";
                    html += "<strong>¡Atención!</strong>" + strMensage;
                    html += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>";
                    html += "<span aria-hidden='true'>&times;</span>";
                    html += "</button>";
                    html += "</div>";
                    break;
            }
            alerta.Text = html;
        }
        private void buscar_rut(int intRut)
        {
            this.alerta.Text = string.Empty;
            try
            {
                var Dato = string.Empty;
                ds = objN2.BuscaFotoProfesional(intRut);
                if (ds.Tables["table"].Rows.Count > 0)
                {
                    foreach (DataRow linea in ds.Tables["Table"].Rows)
                    {
                        Dato = linea["fotografia"].ToString();
                        dataValue = Convert.FromBase64String(Dato);
                        rd_fotografia.DataValue = dataValue;
                    }
                    this.MensageBox("warning", "  Se encontró fotografía, puede eliminar o modificar imagen");
                    btn_guardar.Enabled = false;
                    btn_modificar.Enabled = true;
                    btn_eliminar.Enabled = true;
                    lbl_estado.Text = "1";
                }
                else
                {
                    this.MensageBox("danger", " No se encontró fotografía para este profesional, seleccione imagen y guarde fotografía");
                    btn_guardar.Enabled = true;
                    btn_modificar.Enabled = false;
                    btn_eliminar.Enabled = false;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion //FIN METODOS
        #region [EVENTOS]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.limpiar();
                this.ListaProfesionales();
                string strControl = string.Empty;
                string urlclr = "";
                urlclr = (string)Request.Url.ToString();
                string url = string.Empty;
                try
                {
                    strControl = "rhoz";
                    Session["profesiona"] = strControl;
                    lbl_logincrea.Text = strControl;

                    if (urlclr.Contains("?") || (Session["profesiona"].ToString() == "" || Session["profesiona"] == null))
                    {
                        url = urlclr.Split(new Char[] { '?' })[0];
                        strControl = (string)Request.QueryString["Loginusintra"];
                        Session["profesiona"] = strControl;
                        Response.Redirect(url);
                        
                    }
                }
                catch
                {
                    #region cierra_ventana
                    string popupScript = "<script language='JavaScript'>window.open('','_self','');window.close();<" + "/script>";
                    Response.Write(popupScript);
                    #endregion //cierra_ventana
                    return;
                }
                strControl = (string)Session["profesiona"];
                try
                {
                    lbl_user.Text = strControl;
                }
                catch
                {
                    return;
                }
            }
        }
        protected void cmb_buscaEspecialista_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string cmdArg = ((RadComboBox)sender).Attributes["CommandArgument"];

            switch (cmdArg)
            {
                case "busca_nombre":
                    //if (e.Value == "")
                    //{
                    //    MessageBox1.show("Debe seleccionar especialista");
                    //    return;
                    //}
                    lbl_rutprof.Text = e.Value;
                    lbl_nombprof.Text = e.Text;
                    if (lbl_rutprof.Text != "")
                    {
                        buscar_rut(int.Parse(lbl_rutprof.Text));
                    }
                    break;
            }
        }
        protected void cmb_buscaEspecialista_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //if (e.Text == "")
            //{
            //    MessageBox1.show("Debe ingresar patrones de busqueda");
            //    return;
            //}

            string cmdArg = ((RadComboBox)sender).Attributes["CommandRequest"];

            switch (cmdArg)
            {
                case "busca_nombre":
                    this.ListaProfesionales(e.Text);
                    //metodsis.profxcodicentmatch(ref cmb_buscaEspecialista, e.Text);
                    break;
            }
        }
        protected void btn_cargar_Click(object sender, EventArgs e)
        {
            this.alerta.Text = string.Empty;
            try
            {
                if (lbl_rutprof.Text == "")
                {
                    this.MensageBox("warning", " ¡ Debe seleccionar Profesional !");
                    return;
                }
                this.cargar_imagen();
                if (lbl_estado.Text == "0")
                {
                    this.MensageBox("warning", " ¡ Debe seleccionar una Fotografíca !");
                    return;
                }
            }
            catch (Exception EX)
            {
                this.MensageBox("danger", "Ha ocurrido un Error, por favor comunicarse con Informática (" + EX.Message + ")");
            }
        }
        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            try
            {
                this.limpiar();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            this.alerta.Text = string.Empty;
            try
            {
                if (lbl_rutprof.Text == "")
                {
                    this.MensageBox("warning", " Debe seleccionar Profesional ");
                    return;
                }
                if (lbl_estado.Text == "0")
                {
                    this.MensageBox("warning", " Debe seleccionar una Fotografíca ");
                    return;
                }
                bool esExito = false;
                objN2.rutprof = int.Parse(lbl_rutprof.Text);
                objN2.fotografia = Convert.ToBase64String(dataValue);
                objN2.logincrea = lbl_logincrea.Text;
                objN2.fcrea = DateTime.Now.Date;
                objN2.hcrea = DateTime.Now.ToString("HH:mm");
                objN2.fmodifica = DateTime.Now.Date;
                objN2.hmodifica = DateTime.Now.ToString("HH:mm");
                esExito = objN2.GrabaFotoProfesional(objN2);
                if (esExito)
                {
                    this.MensageBox("success", " Registro fue grabado con exito ");
                    this.limpiar2();
                }
                else
                {
                    this.MensageBox("warning", " Registro no fue grabado ");
                }
            }
            catch (Exception EX)
            {
                this.MensageBox("danger", " Ha ocurrido un Error, por favor comunicarse con Informática (" + EX.Message + ")");
            }
        }
        protected void btn_modificar_Click(object sender, EventArgs e)
        {
            this.alerta.Text = string.Empty;
            try
            {
                int rutprof = 0;
                string fotografia = string.Empty;
                string loginmodifica = string.Empty;
                DateTime fmodifica;
                string hmodifica = string.Empty;
                bool esExito = false;
                if (lbl_rutprof.Text == "")
                {
                    this.MensageBox("warning", " Debe seleccionar Profesional ");
                    return;
                }
                if (lbl_estado.Text == "0")
                {
                    this.MensageBox("warning", " Debe seleccionar una Fotografíca ");
                    return;
                }
                rutprof = int.Parse(lbl_rutprof.Text);
                fotografia = Convert.ToBase64String(dataValue);
                loginmodifica = lbl_logincrea.Text;
                fmodifica = DateTime.Now.Date;
                hmodifica = DateTime.Now.ToString("HH:mm");
                esExito = objN2.ModificaFotoProfesional(rutprof,fotografia,loginmodifica,fmodifica,hmodifica);
                if (esExito)
                {
                    this.MensageBox("success", " Registro fue modificado con exito ");
                    this.limpiar2();
                }
                else
                {
                    this.MensageBox("warning", " Registro no fue modificado ");
                }
            }
            catch (Exception EX)
            {
                this.MensageBox("danger", " Ha ocurrido un Error, por favor comunicarse con Informática (" + EX.Message + ")");
            }
        }
        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            this.alerta.Text = string.Empty;
            try
            {
                int rutprof = 0;
                bool esExito = false;
                if (lbl_rutprof.Text == "")
                {
                    this.MensageBox("warning", " ¡ Debe seleccionar Profesional !");
                    return;
                }
                if (lbl_estado.Text == "0")
                {
                    this.MensageBox("warning", " ¡ Debe seleccionar una Fotografíca !");
                    return;
                }
                rutprof = int.Parse(lbl_rutprof.Text);
                esExito = objN2.EliminaFotoProfesional(rutprof);
                if (esExito)
                {
                    this.MensageBox("success", " Registro fue eliminado con exito ");
                    this.limpiar2();
                }
                else
                {
                    this.MensageBox("warning", " Registro no fue eliminado ");
                }
            }
            catch (Exception EX)
            {
                this.MensageBox("danger", " Ha ocurrido un Error, por favor comunicarse con Informática (" + EX.Message + ")");
            }
        }
        #endregion //FIN EVENTOS
    }
}