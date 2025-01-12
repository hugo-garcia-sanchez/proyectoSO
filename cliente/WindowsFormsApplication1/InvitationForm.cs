using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class InvitationForm : Form
    {
        public bool IsAccepted { get; private set; }

        // Constructor con parámetros
        public InvitationForm(string invitador, string partida)
        {
            InitializeComponent();

            // Asigna los datos a la etiqueta de mensaje
            lblMessage.Text = $"Has recibido una invitación de {invitador} para unirte a la partida: {partida}.";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            IsAccepted = true; // El usuario ha aceptado
            this.Close();      // Cierra el formulario
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            IsAccepted = false; // El usuario ha rechazado
            this.Close();       // Cierra el formulario
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            IsAccepted = true; // El usuario ha aceptado
            this.Close();      // Cierra el formulario
        }

        private void btnRechazar_Click_1(object sender, EventArgs e)
        {
            IsAccepted = false; // El usuario ha rechazado
            this.Close();       // Cierra el formulario

        }

        private void InvitationForm_Load(object sender, EventArgs e)
        {

        }
    }
    
}

