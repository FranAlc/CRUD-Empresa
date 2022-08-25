
namespace ABM_Negocio
{
    public partial class Form1 : Form
    {

        //Variables de LOGIN Globales
       



        string user = "francoGalan";
        string pass = "admin123";
        

        public Form1()
        {
            InitializeComponent();
           
        }

        /**********************************************
         * FormLogin
         **********************************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            txtUser.Text = user;
            txtPassword.Text = pass;

        }

        /**********************************************
         * Cerrar Pestaña
         **********************************************/
        private void btnCerrar(object sender, EventArgs e)
        {
            this.Close();
        }

        /**********************************************
         * Ingresar 
         **********************************************/
       
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            /*Login del usuario, su nombre o clave dependen de la variable global,
             * en caso que la clave o el nombre sean cambiados 
             * aparecera un mensaje nombrando un error
             */
            if(txtUser.Text == user)
            {
                if(txtPassword.Text == pass)
                {
                    Ingreso ingresarNegocio = new Ingreso(user);
                    ingresarNegocio.Show();
                    
                    this.Hide(); //Cierro el login
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta","Login incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Usuario incorrecto", "Login incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            


            }


        /**********************************************
         * Limpiar formulario
         **********************************************/

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtUser.Text = "";
            txtPassword.Text = "";
        }
    }
}