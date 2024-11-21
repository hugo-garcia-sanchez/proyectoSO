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
    public partial class FormGame : Form
    {
        private bool maximizado = false;
        private string selectedUser;
        private string nameGame;

        public FormGame()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maximizeBtn_Click(object sender, EventArgs e)
        {
            if (!maximizado)
            {
                this.WindowState = FormWindowState.Maximized;
                maximizado = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                maximizado = false;
            }
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        /*
        
        private void fourcards_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            // Send a request to the server to get the list of connected players
            string mensaje = "22/LIST/";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);

            try
            {
                server.Send(msg);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Error sending data to the server: " + ex.Message);
                return;
            }
            esperandoRespuesta = false;
        }

        private void middlecard_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            // Send a request to the server to get the list of connected players
            string mensaje = "21/LIST/";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);

            try
            {
                server.Send(msg);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Error sending data to the server: " + ex.Message);
                return;
            }
            //esperandoRespuesta = false;
        }

        private void card1_Click(object sender, EventArgs e)
        {
            if (PuedeTirarCarta(cartaJugador1, cartaMedio))
            {
                cartaMedio.Color = cartaJugador1.Color;
                buttoncartamedio.BackColor = buttoncarta1.BackColor;
                cartaJugador1.Color = "Gray";
                buttoncarta1.BackColor = Color.Gray;



                cartaMedio.Numero = cartaJugador1.Numero;
                buttoncartamedio.Text = buttoncarta1.Text;
                cartaJugador1.Numero = null;
                buttoncarta1.Text = "";

                EnviarMensajeCodigoCarta("23/" + cartaMedio.Numero + "," + cartaMedio.Color);

            }
            else
            {
                MessageBox.Show("No puedes tirar esta carta");
            }
            //esperandoRespuesta = true;
        }

        private void card2_Click(object sender, EventArgs e)
        {
            if (PuedeTirarCarta(cartaJugador2, cartaMedio))
            {
                cartaMedio.Color = cartaJugador2.Color;
                buttoncartamedio.BackColor = buttoncarta2.BackColor;
                cartaJugador2.Color = "Gray";
                buttoncarta2.BackColor = Color.Gray;

                cartaMedio.Numero = cartaJugador2.Numero;
                buttoncartamedio.Text = buttoncarta2.Text;
                cartaJugador2.Numero = null;
                buttoncarta2.Text = "";
                EnviarMensajeCodigoCarta("23/" + cartaMedio.Numero + "," + cartaMedio.Color);

            }
            else
            {
                MessageBox.Show("No puedes tirar esta carta");
            }
            //esperandoRespuesta = true;
        }

        private void card3_Click(object sender, EventArgs e)
        {
            if (PuedeTirarCarta(cartaJugador3, cartaMedio))
            {

                cartaMedio.Color = cartaJugador3.Color;
                buttoncartamedio.BackColor = buttoncarta3.BackColor;
                cartaJugador3.Color = "Gray";
                buttoncarta3.BackColor = Color.Gray;

                cartaMedio.Numero = cartaJugador3.Numero;
                buttoncartamedio.Text = buttoncarta3.Text;
                cartaJugador3.Numero = null;
                buttoncarta3.Text = "";
                EnviarMensajeCodigoCarta("23/" + cartaMedio.Numero + "," + cartaMedio.Color);
            }
            else
            {
                MessageBox.Show("No puedes tirar esta carta");
            }
            //esperandoRespuesta = true;
        }

        private void card4_Click(object sender, EventArgs e)
        {
            if (PuedeTirarCarta(cartaJugador4, cartaMedio))
            {
                cartaMedio.Color = cartaJugador4.Color;
                buttoncartamedio.BackColor = buttoncarta4.BackColor;
                cartaJugador4.Color = "Gray";
                buttoncarta4.BackColor = Color.Gray;

                cartaMedio.Numero = cartaJugador4.Numero;
                buttoncartamedio.Text = buttoncarta4.Text;
                cartaJugador4.Numero = null;
                buttoncarta4.Text = "";
                EnviarMensajeCodigoCarta("23/" + cartaMedio.Numero + "," + cartaMedio.Color);
            }
            else
            {
                MessageBox.Show("No puedes tirar esta carta");
            }
            //esperandoRespuesta = true;
        }

        private void onlineGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string invited;
            //if (creator != null)    //NOMES POT CONVIDAR EL HOST DE LA PARTIDA
            {
                if (onlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    invited = onlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    string mensaje = "97/" + selectedUser + "/" + invited + "/";
                    MessageBox.Show(mensaje);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
        }
        
        private void onlineGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string invited;
            //if (creator != null)    //NOMES POT CONVIDAR EL HOST DE LA PARTIDA
            {
                if (onlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    invited = onlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    string mensaje = "97/" + selectedUser + "/" + invited + "/";
                    MessageBox.Show(mensaje);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
        }
        */
    }
}
