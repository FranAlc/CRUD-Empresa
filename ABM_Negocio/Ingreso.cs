using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABM_Negocio
{
    public partial class Ingreso : Form
    {
        List<ClsLista> MiList = new List<ClsLista>();

        public Ingreso(string user)
        {
            InitializeComponent();

            lblRespuesta.Text = user;

            lblConsultar.Enabled = false;
            lblEliminar.Enabled = false;

        }

        /**********************************************
        * Cerrar Pestaña
        **********************************************/
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void lblRespuesta_Click(object sender, EventArgs e)
        {


        }

        /*******************************************************************************
         *                                      REGISTRO - MENU
         ******************************************************************/
        private void lblRegistrar_Click(object sender, EventArgs e)
        {
            if (ValidarNombre() == false) //NOMBRE
            {
                return;
            }
            if (ValidarPuesto() == false) //PUESTO DETRABAJO
            {
                return;
            }
            if (ValidarEdad() == false) //EDAD
            {
                return;
            }
            if (ValidarSueldo() == false) //SUELDO
            {
                return;
            }

            if (Existe(txtNombre.Text))
            {
                erpError.SetError(txtNombre, "El/la empleada/o con ese nombre ya esta registrado");
                txtNombre.Focus();
                return;
            }
            erpError.SetError(txtNombre, "");

            /**********************************************
           * Objeto clase lista TABLA
           **********************************************/
            ClsLista empleo = new ClsLista();
            empleo.nombre = txtNombre.Text;
            empleo.edad = int.Parse(txtEdad.Text);
            empleo.sueldo = int.Parse(txtSueldo.Text);
            empleo.puesto = cmbPuesto.SelectedItem.ToString();
          

            MiList.Add(empleo);

            //Registro para la tabla
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = MiList;
            Limpiar();
            txtNombre.Focus();
            txtEdad.Focus();
            txtSueldo.Focus();
            cmbPuesto.Focus();
            lblConsultar.Enabled = true;


        }

        ///  EXISTE <-------------------- metodo para no ingresar con el mismo nombre
        
        private bool Existe(string nombre)
        {
            foreach (ClsLista empleo in MiList)
            {
                if( empleo.nombre == nombre)
                {
                    return true;
                }

            }
            return false;
        }

        //Resto del code ;)

        //validar el Nombre
        private bool ValidarNombre()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                erpError.SetError(txtNombre, "Debes ingresar un nombre");
                return false;
            }
            else
            {
                erpError.SetError(txtNombre, "");
                return true;
            }
        }

        //valida el Puesto de trabajo
        private bool ValidarPuesto()
        {
            if (string.IsNullOrEmpty(cmbPuesto.Text))
            {
                erpError.SetError(cmbPuesto, "Debes seleccionar un puesto de trabajo");
                return false;
            }
            else
            {
                erpError.SetError(cmbPuesto, "");
                return true;
            }
        }

        //valida la Edad del trabajador
        private bool ValidarEdad()
        {
            int edad1;
            if (!int.TryParse(txtEdad.Text, out edad1) || txtEdad.Text == "")
            {
                erpError.SetError(txtEdad, "Debes ingresar un valor numerico");

                txtEdad.Clear();    //
                                    // En caso q no se ingrese un valor, se muestra que si se ingresa un valor tipo string no corre y sale error
                txtEdad.Focus();    //


                return false;
            }
            else
            {
                erpError.SetError(txtEdad, "");
                return true;
            }
        }

        //valida el sueldo
        private bool ValidarSueldo()
        {

            int sueldo;
            if (!int.TryParse(txtSueldo.Text, out sueldo) || txtSueldo.Text == "")
            {
                erpError.SetError(txtSueldo, "Debes ingresar un valor numerico");
                txtSueldo.Clear();    //
                                    // En caso q no se ingrese un valor, se muestra que si se ingresa un valor tipo string no corre y sale error
                txtSueldo.Focus();    //
                return false;
            }
            else
            {
                erpError.SetError(txtSueldo, "");
                return true;
            }



        }

        //Clean
        private void Limpiar()
        {
            txtNombre.Clear();
            txtEdad.Clear();
            txtSueldo.Clear();
            cmbPuesto.SelectedIndex = 0; // o cmbPuesto.Items.Clear() o incluso ""
        }

        /********************************************************
        *                   SALIR MENU
        *******************************************************/
        private void lblSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// CONSULTA
        private void lblConsultar_Click(object sender, EventArgs e)
        {
            if (ValidarNombre() == false)
            {
                return;
            }
            ClsLista empleo = GetEmpleo(txtNombre.Text);
            if (empleo == null)
            {
                erpError.SetError(txtNombre, "El nombre no se encuentra ubicado en la lista");
                Limpiar();
                txtNombre.Focus();
                return;

            }
            else
            {
                erpError.SetError(txtNombre, "");
                txtNombre.Text = empleo.nombre;
                cmbPuesto.SelectedItem = empleo.puesto;
                txtEdad.Text = empleo.edad.ToString();
                txtSueldo.Text = empleo.sueldo.ToString();

                lblEliminar.Enabled = true;
            }

        }

        //metodo consultar
        private ClsLista GetEmpleo(string text)
        {
            return MiList.Find(empleo => empleo.nombre.Contains(txtNombre.Text));
        }



        /**************************************************
         * ELIMINAR
         **************************************************/
        private void lblEliminar_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text == "")
            {
                erpError.SetError(txtNombre, "El nombre no se encuentra ubicado en la lista");
                Limpiar();
                txtNombre.Focus();
                lblEliminar.Enabled = false;
                return;

            }
            else
            {
                erpError.SetError(txtNombre, "");
                DialogResult respuesta = MessageBox.Show("Esta seguro que desea eliminar el siguiente usuario?", "Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (respuesta == DialogResult.Yes)
                {
                    foreach (ClsLista empleo in MiList)
                    {
                        if (empleo.nombre == txtNombre.Text)
                        {
                            MiList.Remove(empleo);
                            break;
                        }
                    }
                    Limpiar();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = MiList;

                }
            }
        }

    }
}
