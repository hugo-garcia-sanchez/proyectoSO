using System;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        Socket server;
        DataTable dt = new DataTable();
        Thread atender;
        char name;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // necesario para que los elementos de los forms
                                                     // puedan acceder entre varios threads
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

        private void AtenderServidor()
        {
            while (true)
            {
                try
                { 
                    byte[] msg2 = new byte[512];
                    int bytesReceived = server.Receive(msg2);
                    string response = Encoding.ASCII.GetString(msg2, 0, bytesReceived);
                    string[] trozos = response.Split('/');
                    int codigo = Convert.ToInt32(trozos[0]);
                    string mensaje = trozos[1].Split('\0')[0];

                    switch (codigo)
                    {
                        case 1: //respuesta a registrarse
                            MessageBox.Show(mensaje);
                            break;
                        case 2: //respuesta a log in
                            MessageBox.Show(mensaje);
                            break;
                        case 6: //green
                            MessageBox.Show(mensaje);
                            cardlbl.Text = "Last card: " + mensaje;
                            break;
                        case 7: //red
                            MessageBox.Show(mensaje);
                            cardlbl.Text = "Last card: " + mensaje;
                            break;
                        case 8: //blue
                            MessageBox.Show(mensaje);
                            cardlbl.Text = "Last card: " + mensaje;
                            break;
                        case 9: // yellow
                            MessageBox.Show(mensaje);
                            cardlbl.Text = "Last card: " + mensaje;
                            break;
                        case 10: // last card
                            MessageBox.Show(mensaje);
                            cardlbl.Text = "Last card: " + mensaje;
                            break;
                        case 11: // partida 1
                            MessageBox.Show(mensaje);
                            break;
                        case 12: // partida 2
                            MessageBox.Show(mensaje);
                            break;
                        case 13: // partida 3
                            MessageBox.Show(mensaje);
                            break;
                        case 14: // partida 4
                            MessageBox.Show(mensaje);
                            break;

                        case 15: //notificacion
                            // MessageBox.Show(mensaje);
                            if (mensaje.StartsWith("Connected players:"))
                            {
                                string players = mensaje.Substring("Connected players:".Length);
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
                                MessageBox.Show(mensaje);
                            }
                            break;
                    } 
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error gestionando el mensaje recibido");
                }
            }
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
       


        /// 
        /// A partir de aquí está todo lo relacionado con cliente-servidor.
        /// 

        bool isConnected = false;

        void btnConn_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");  // Cambia la IP por la de tu servidor
            //ip shiva 10.4.119.5
            //ip vbox 192.168.56.102
            IPEndPoint ipep = new IPEndPoint(direc, 9057);
            //puerto shiva 50061
            //puerto vbox 9050


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

            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

        void btnDesconnect_Click(object sender, EventArgs e)
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
            atender.Abort();
            indicadorConexion_label.ForeColor = Color.Red;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            server = null;

            // Actualizamos el estado de conexión
            isConnected = false;  // Marcamos como desconectado
        }

        void btnReg_Click(object sender, EventArgs e)
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
        }


        void btnLog_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            // Botón para registrar un nuevo jugador
            string mensaje = "2/" + username.Text + "/" + password.Text;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
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
        }

       
        private void button13_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            string mensaje = "11/" + username.Text + "/" + password.Text + "/" + 1;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            string mensaje = "12/" + username.Text + "/" + password.Text + "/" + 2;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            string mensaje = "13/" + username.Text + "/" + password.Text + "/" + 3;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            string mensaje = "14/" + username.Text + "/" + password.Text + "/" + 4;
            // Enviamos al servidor el nombre de usuario y la contraseña tecleados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

    }
}