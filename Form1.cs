using LaureadoPrueba.Dato;
using LaureadoPrueba.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaureadoPrueba
{
    public partial class MainForm : Form
    {
        private DataTable tabla;
        PersonaClass gestionPersona = new PersonaClass();
        private void Inicializar()
        {
            tabla = new DataTable();
            tabla.Columns.Add("Id");
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Fecha", typeof(DateTime));
            
            PersonaGridView.DataSource = tabla;
            PersonaGridView.Columns["Fecha"].DefaultCellStyle.Format = "dd/MMM/yyyy";

            DateTimePicker.MaxDate = DateTime.Now;
        }
        public MainForm()
        {
            InitializeComponent();
            Consultar();
        }

        private void Consultar()
        {
            Inicializar();
            List<Persona> lista = gestionPersona.Consultar();
            foreach(var item in lista)
            {
                DataRow row = tabla.NewRow();
                row["Id"] = item.Id;
                row["Nombre"] = item.Nombre;
                row["Fecha"] = item.FechaDeNacimiento.Date.ToString();
                tabla.Rows.Add(row);
            }

            PersonaGridView.Columns["Fecha"].DefaultCellStyle.Format = "d";
        }
        private void Guardar()
        {
            //Validacion
            if (NombreTextBox.Text.Length > 50 || NombreTextBox.Text.Length < 3)
            {
                MessageBox.Show("El campo Nombre no puede tener más de 50 caracteres ni menos de 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NombreTextBox.Focus();
                return;
            }

            //Acceso a BD - Agregar Persona
            try
            {
                Persona modelo = new Persona()
                {
                    Nombre = NombreTextBox.Text,
                    FechaDeNacimiento = DateTimePicker.Value
                };
                gestionPersona.Guardar(modelo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar a la persona, intente de nuevo más tarde. \n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void Eliminar()
        {
            //Validacion
            if (PersonaGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Primero debe seleccionar una Persona", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Confirmar eliminacion
            var id = Convert.ToInt32(PersonaGridView.SelectedRows[0].Cells[0].Value);
            if (MessageBox.Show("¿Desea eliminar a la persona con Id (" + id.ToString() + ") del sistema? Una vez hecho no podrá recuperarlo.", "Eliminar Persona", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            //Acceso a BD - Eliminar Persona
            try
            {
                Persona modelo = new Persona()
                {
                    Id = Convert.ToInt32(id)
                };
                gestionPersona.Eliminar(modelo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar a la persona, intente de nuevo más tarde. \n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Limpiar()
        {
            NombreTextBox.Text = "";
            DateTimePicker.Value = DateTime.Now.Date;
            NombreTextBox.Focus();
        }
        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Guardar();
            Consultar();
            Limpiar();
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            Eliminar();
            Consultar();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void LimpiarButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
