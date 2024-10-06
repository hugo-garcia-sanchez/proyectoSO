using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        Socket server;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Creamos un IPEndPoint con la IP del servidor y el puerto del servidor al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");  // Cambia la IP por la de tu servidor
            IPEndPoint ipep = new IPEndPoint(direc, 9051);

            // Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep); // Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Connected");

            }
            catch (SocketException ex)
            {
                // Si hay excepción, imprimimos error y salimos del programa con return 
                MessageBox.Show("Could not connect to the server: " + ex.Message);
                return;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Botón para registrar un nuevo jugador
            string mensaje = "1/" + username.Text + "/" + password.Text;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Recibimos la respuesta del servidor
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

            MessageBox.Show(response);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Mensaje de desconexión
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            // Botón para registrar un nuevo jugador
            string mensaje = "2/" + username.Text + "/" + password.Text;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Recibimos la respuesta del servidor
            byte[] msg2 = new byte[512];
            int bytesReceived = server.Receive(msg2);
            string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived); // Cambié la forma de leer la respuesta

            MessageBox.Show(response);
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
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
    }
}

