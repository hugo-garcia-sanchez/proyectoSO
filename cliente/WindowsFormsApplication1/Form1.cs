using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        Socket server;
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        


        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maximizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void label6_Click(object sender, EventArgs e)
        {
            //este label es es el titulo de arriba izquierda "Welcome to UNO Game!"
        }


        /// <summary>
        /// A partir de aquí está todo lo relacionado con cliente-servidor.
        /// </summary>
        bool isConnected = false;  

        void button1_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("10.4.119.5");  // Cambia la IP por la de tu servidor
            IPEndPoint ipep = new IPEndPoint(direc, 50061);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep); // Intentamos conectar el socket
                indicadorConexion_label.ForeColor = Color.Green;
                MessageBox.Show("Connected");
                isConnected = true;  // Marcamos como conectado
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Could not connect to the server: " + ex.Message);
                return;
            }
        }

        void button2_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            // Botón para registrar un nuevo jugador
            string mensaje = "1/" + username.Text + "/" + password.Text;

            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Recibimos la respuesta del servidor
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);

            MessageBox.Show(response);
        }

        void button3_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            // Mensaje de desconexión
            string mensaje = "0/";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos correctamente
            indicadorConexion_label.ForeColor = Color.Red;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            server = null;

            // Actualizamos el estado de conexión
            isConnected = false;  // Marcamos como desconectado
        }


        void button4_Click(object sender, EventArgs e)
        {
            // Botón para registrar un nuevo jugador
            string mensaje = "2/" + username.Text + "/" + password.Text;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Recibimos la respuesta del servidor
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);

            MessageBox.Show(response);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (PlayingG1.Checked)
            {
                // Botón para registrar un nuevo jugador
                string mensaje = "3/" + username.Text + "/" + password.Text;
                // Enviamos al servidor el nombre de usuario y la contraseña tecleados
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                byte[] msg2 = new byte[512];
                int bytesReceived = server.Receive(msg2);
                string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

                MessageBox.Show(response);

            }
            if (WhatGame.Checked)
            {
                // Botón para registrar un nuevo jugador
                string mensaje = "4/" + username.Text + "/" + password.Text;
                // Enviamos al servidor el nombre de usuario y la contraseña tecleados
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                byte[] msg2 = new byte[512];
                int bytesReceived = server.Receive(msg2);
                string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

                MessageBox.Show(response);
            }
            
            if (NumPlayers.Checked)
            {
                // Botón para registrar un nuevo jugador
                string mensaje = "5/" + username.Text + "/" + password.Text;
                // Enviamos al servidor el nombre de usuario y la contraseña tecleados
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                byte[] msg2 = new byte[512];
                int bytesReceived = server.Receive(msg2);
                string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

                MessageBox.Show(response);
            }

        }

        private void PlayingG1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            string mensaje = "6/" + username.Text + "/" + password.Text;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);

            cardlbl.Text = "Last card: " + response;
        }




        private void contlbl_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            string mensaje = "7/" + username.Text + "/" + password.Text;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);

            cardlbl.Text = "Last card: " + response;
        }





        private void button7_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            string mensaje = "8/" + username.Text + "/" + password.Text;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);

            cardlbl.Text = "Last card: " + response;
        }





        private void button8_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            string mensaje = "9/" + username.Text + "/" + password.Text;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);

            cardlbl.Text = "Last card: " + response;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            // Enviamos la solicitud al servidor para obtener la última carta jugada
            string mensaje = "10/" + username.Text + "/" + password.Text;  // No necesitamos el nombre y contraseña para esto, pero puedes dejarlo si lo deseas
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Recibimos la respuesta del servidor
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);

            // Mostramos la última carta en la interfaz
            cardlbl.Text = "Last card: " + response;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This functionality shows you the last card selected on the server by all the clients that have connected to it. To see how it works, select one of the four available cards, it will be shown in the box below your card. If you want to see what the last card selected was, you just have to press the \"Last Card!\" button.");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            string mensaje = "11/" + username.Text + "/" + password.Text + "/" + 1;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

            MessageBox.Show(response);
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            string mensaje = "12/" + username.Text + "/" + password.Text + "/" + 2;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

            MessageBox.Show(response);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string mensaje = "13/" + username.Text + "/" + password.Text + "/" + 3;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

            MessageBox.Show(response);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string mensaje = "14/" + username.Text + "/" + password.Text + "/" + 4;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

            MessageBox.Show(response);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            // Send a request to the server to get the list of connected players
            string mensaje = "15/LIST/";
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

            byte[] msg2 = new byte[1024];
            int bytesReceived = 0;

            try
            {
                bytesReceived = server.Receive(msg2);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Error receiving data from the server: " + ex.Message);
                return;
            }

            if (bytesReceived > 0)
            {
                string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);
                if (response.StartsWith("Connected players:"))
                {
                    string players = response.Substring("Connected players:".Length);
                    MessageBox.Show("Connected players: " + players);
                    string[] connectedPlayerList = players.Split(',');
                    int i = 0;
                    dt.Columns.Add("PlayerName");
                    while (i < connectedPlayerList.Length)
                    {
                        dt.Rows.Add(connectedPlayerList[i].Trim());
                        i++;
                    }
                    onlineGrid.DataSource = dt;
                    
                }
                else
                {
                    MessageBox.Show("There are no players");
                }
            }
            else
            {
                MessageBox.Show("No data received from the server.");
            }
        }

        private void indicadorConexion_label_Click(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void onlineGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}