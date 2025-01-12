using ClientApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class eliminarUsuarioForm : Form
    {
        private Socket server;
        private bool isConnected;
        private string selectedUser;

        public eliminarUsuarioForm(Socket server,string selectedUser, bool isConnected)
        {
            InitializeComponent();
            this.server = server;
            this.isConnected = isConnected;
            this.selectedUser = selectedUser;
        }
        private void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                SafeInvoke(this, () =>
                {
                    MessageBox.Show("You are not connected to the server.");
                });
                return;
            }

            // Send a delete request to the server
            string mensaje = $"30/9991/" + selectedUser;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);

            try
            {
                server.Send(msg);
                // Close the form immediately after sending the request
                this.Close();
            }
            catch (SocketException ex)
            {
                SafeInvoke(this, () =>
                {
                    MessageBox.Show("Error sending data to the server: " + ex.Message);
                });
                return;
            }
        }


        private void btnRechazar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eliminarUsuarioForm_Load(object sender, EventArgs e)
        {

        }
    }
}
