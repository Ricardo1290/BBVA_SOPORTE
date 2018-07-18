using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace BBVA_SOPORTE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();

                string ConStr = "";
                //getting the path of the file    

                //string path = Server.MapPath("InsertDatos.xlsx");
                string fileName = "InformeDiario.xlsx";

                string ruta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                string PathArchivo = Path.Combine(ruta, fileName);
                //connection string for that file which extantion is .xlsx    
                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + PathArchivo + ";Extended Properties=\"Excel 12.0;ReadOnly=False;HDR=Yes;\"";
                //making query
                string query = "INSERT INTO [Hoja1$]([Fecha], [Pasajero], [Nº Viaje], [Motivo de la llamada], [Duración de la llamada],[Es consulta de soporte navegacional], [Comentarios])" +
                    " VALUES('" + dtp1.Text + "','" + txtPasajero.Text + "','" + txtViaje.Text + "','" + txtMotivo.Text + "','"
                        + txtDuracion.Text + "','" + txtConsulta.Text + "','" + txtComentarios.Text + "')";
                //Providing connection     
                OleDbConnection conn = new OleDbConnection(ConStr);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //create command object    
                OleDbCommand cmd = new OleDbCommand(query, conn);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("SE AGREGARON LOS DATOS SATISFACTORIAMENTE", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtComentarios.Text = "";
                    txtConsulta.Text = "";
                    txtDuracion.Text = "";
                    txtMotivo.Text = "";
                    txtPasajero.Text = "";
                    txtViaje.Text = "";

                }
                
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("POR FAVOR INTENTE DE NUEVO", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidarCampos()
        {
            Regex rx = new Regex(@"^[+-]?\d+(\.\d+)?$");
            bool ok = true;

            if (rx.IsMatch(txtViaje.Text))
            {
                ok = false;
                ErrorProvider.SetError(txtPasajero, "INGRESE PASAJARE");

            }

            if (txtViaje.Text == "")
            {
                ok = false;
                ErrorProvider.SetError(txtViaje, "INGRESE N°VIAJE");

            }
            if (txtMotivo.Text == "")
            {
                ok = false;
                ErrorProvider.SetError(txtMotivo, "INGRESE MOTIVO LLAMADA");

            }
            if (txtDuracion.Text == "")
            {
                ok = false;
                ErrorProvider.SetError(txtDuracion, "INGRESE DURACION LLAMADA");

            }

            if (txtConsulta.Text == "")
            {
                ok = false;
                ErrorProvider.SetError(txtConsulta, "INGRESE CONSULTA");

            }
            if (txtComentarios.Text == "")
            {
                ok = false;
                ErrorProvider.SetError(txtComentarios, "INGRESE COMENTARIOS");

            }
            return ok;
        }

    }
}
