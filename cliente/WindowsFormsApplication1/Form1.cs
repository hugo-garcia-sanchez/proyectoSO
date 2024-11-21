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
using System.Threading;
using System.Web;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        private Socket server;
        private DataTable dt = new DataTable();
        BindingSource bindingSourcePlayers = new BindingSource();
        private DataTable dtGames = new DataTable();
        private Thread atender;
        private bool esperandoRespuesta = false; //utilizaremos esta variable una vez enviamos desde el cliente al serv un mensage
        private bool isConnected = false;
        private bool maximizado = false;
        private string selectedUser;    // uso este string para evitar que se loguee un mismo usuario varias veces
        private bool alreadyLogged = false; // con este bool nos aseguramos que no hagas log in repetidamente sin desconectarte antes
        
        List<string> Invitations = new List<string>();
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            CheckForIllegalCrossThreadCalls = false;

            // Asocia el evento DataError para manejar errores en el DataGridView
            onlineGrid.DataError += onlineGrid_DataError;
        }


        //------------------------- ESTÉTICA FORMS -------------------------\\

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Form1_Load(object sender, EventArgs e)
        {
            onlineGrid.AllowUserToAddRows = false; // Desactiva la fila adicional para agregar nuevas filas
            gamesGrid.AllowUserToAddRows = false; // Desactiva la fila adicional para agregar nuevas filas

            onlineGrid.ReadOnly = true; // Deshabilita la edición en todas las celdas
            gamesGrid.ReadOnly = true; // Deshabilita la edición en todas las celdas

            dt.Columns.Add("PlayerName"); // Inicializa la datatable de usuarios
            onlineGrid.DataSource = dt;

            dtGames.Columns.Add("GameName"); // Inicializa la datatable de usuarios
            gamesGrid.DataSource = dtGames;


           
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



        //------------------------- FUNCIONES NECESARIAS A LO LARGO DEL CODIGO -------------------------\\


        private void MostrarMensaje(string mensaje) //la utilizaremos para proporcionar mesageBox en el atender cliente
        {
            if (InvokeRequired) //verificamos si estamos llamando al método desde un hilo distinto, por ejemplo atenderclientes
            {
                Invoke(new Action(() => MessageBox.Show(mensaje)));
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }


        private void EnviarMensajeCodigo(string mensaje) //esta funcion me envia un mensage al serv
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            esperandoRespuesta = false;
        }

        private void EnviarMensajeCodigoCarta(string mensaje) //esta funcion me envia un mensage de lac carta clicada al serv
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            esperandoRespuesta = false;
        }

        private void EnviarMensajeUnirPartida(string partida) 
            // Este nuevo unir partida permite mas flexibilidad (las partidas pueden tener ahora cualquier nombre,
            // no tiene pq ser un ID de partida.

            // Tambien elimina la necesidad de enviar tu contraseña de juego (FUTURO)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            string mensaje = $"{20}/{username.Text}/{password.Text}/{partida}";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            esperandoRespuesta = true;
        }

        private void EnviarMensajeCrearPartida(string partida)
        // Este nuevo crear partida 
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            string mensaje = $"{25}/{username.Text}/{password.Text}/{partida}";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            esperandoRespuesta = true;
        }

        public void InvitationSent(string invitedList)
        {
            MessageBox.Show("Invitaion to " + invitedList + " received correctly", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
        }
        //REBEM UNA INVITACIO 
        public void InvitationReceived(List<string> invitations)
        {
            this.Invitations = invitations;




        }

        //------------------------- LECTURA DE LOS MENSAGES ENVIADOS POR EL SERV -------------------------\\

        private void AtenderServidor()
        {
            HashSet<int> codigosSinMessageBox = new HashSet<int> { 6, 7, 8, 9, 16, 27, 21, 22 };

            while (true)
            {
                try
                {
                    // Limpia el buffer antes de recibir datos
                    byte[] msg2 = new byte[512];
                    Array.Clear(msg2, 0, msg2.Length);

                    // Recibe los datos del servidor
                    int bytesReceived = server.Receive(msg2);

                    // Procesa los datos si hay algo recibido
                    if (bytesReceived > 0)
                    {
                        string mensajeCompleto = Encoding.ASCII.GetString(msg2, 0, bytesReceived).Trim();
                        string[] trozos = mensajeCompleto.Split('/');
                        int codigo = Convert.ToInt32(trozos[0]);
                        string response = trozos.Length > 1 ? trozos[1].Split('\0')[0] : string.Empty;

                        Console.WriteLine($"Mensaje completo recibido: {mensajeCompleto}");
                        Console.WriteLine($"Código recibido: {codigo}");

                        // Validación de mensajes corruptos o inválidos
                        if (mensajeCompleto == "" || codigo < 0 || codigo > 100)
                        {
                            Console.WriteLine("Mensaje descartado por estar corrupto o ser inválido.");
                            continue;
                        }

                        if (!esperandoRespuesta && codigosSinMessageBox.Contains(codigo))
                        {
                            if (codigo == 16)
                            {
                                cardlbl.Text = response;
                            }
                            else if (codigo == 27)
                            {
                                if (trozos.Length > 1)
                                {
                                    string[] ListaCartas = response.Split(' ');

                                    if (ListaCartas.Length == 2)
                                    {
                                        buttoncartamedio.BackColor = ObtenerColorDeCarta(ListaCartas[1]);
                                        buttoncartamedio.Text = ListaCartas[0];
                                        cartaMedio = new Carta(ListaCartas[0], ListaCartas[1]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("El mensaje recibido no tiene el formato esperado para código 27.");
                                    }
                                }
                                continue;
                            }
                        }

                        switch (codigo)
                        {
                            case 1:
                            case 2:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 20:
                            case 25:
                                if (esperandoRespuesta)
                                {
                                    MostrarMensaje(response);
                                    esperandoRespuesta = false;
                                }
                                break;

                            case 15:
                                ProcesarListaJugadores(trozos);
                                break;

                            case 22: // Maneja 4 cartas
                                ProcesarCartasJugador(response, 4);
                                break;

                            case 21: // Maneja 1 carta
                                ProcesarCartasJugador(response, 1);
                                break;
                            case 97:  //INVITATION RECEIVED
                                {
                                    int type_operation = Convert.ToInt32(response);

                                    
                                   
                                    if (type_operation == 0) // Recibimos respuesta a nuestra invitación a otro usuario
                                    {
                                        // msg del tipo "97/0/correcto/s,"

                                        string tipo = trozos[2];

                                        if (tipo == "correcto")
                                        {
                                            // Dividimos todos los nombres separados por comas
                                            string invitedList = trozos[3];

                                            // Llamamos a InvitationSent con la lista completa
                                            InvitationSent(invitedList);
                                        }
                                        
                                    
                                    }
                                    else if (type_operation == 1)   //rebem una invitacio d'un altre jugador
                                    {
                                        //msg del tipus "97/1/%s,"
                                        string inviting = trozos[2];

                                        //HEM DE FER ARRIBAR LA INVITACIÓ AL CLIENT:
                                        //SI ESTA AL FORMS DEL CLIENT, -> MESSAGEBOX
                                        //SI ESTA AL FORMS DEL GAME, -> L'ENVIEM A TOTS ELS FORMS GAME QUE TINGUI OBERT VIA FUNCIÓ

                                        MessageBox.Show("Invitaion from " + inviting + " received. Added to your invitation log in game", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                                    }
                                    break;
                                }

                            default:
                                Console.WriteLine("Código no reconocido: " + codigo);
                                break;
                        }
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("Se perdió la conexión con el servidor: " + ex.Message);
                    break;
                }
            }
        }

        // Métodos auxiliares para refactorizar el código
        private void ProcesarListaJugadores(string[] trozos)
        {
            Console.WriteLine($"Mensaje recibido del servidor: {string.Join("/", trozos)}");

            if (trozos.Length > 2 && !string.IsNullOrEmpty(trozos[2].Trim()))
            {
                string[] connectedPlayerList = trozos[2]
                    .Split(',')
                    .Select(p => p.Trim())
                    .Where(p => !string.IsNullOrEmpty(p))
                    .ToArray();

                Console.WriteLine("Lista actual de jugadores conectados:");
                foreach (var player in connectedPlayerList)
                {
                    Console.WriteLine($"- {player}");
                }

                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row = dt.Rows[i];
                    string playerName = row["PlayerName"].ToString().Trim();

                    if (!connectedPlayerList.Contains(playerName))
                    {
                        Console.WriteLine($"Eliminando jugador desconectado: {playerName}");
                        dt.Rows.RemoveAt(i);
                    }
                }

                foreach (string playerName in connectedPlayerList)
                {
                    bool exists = dt.AsEnumerable().Any(row => row.Field<string>("PlayerName") == playerName);

                    if (!exists)
                    {
                        Console.WriteLine($"Añadiendo jugador conectado: {playerName}");
                        dt.Rows.Add(playerName);
                    }
                }

                onlineGrid.DataSource = dt;
                onlineGrid.Refresh();
                Console.WriteLine("Actualización del DataGridView completada.");
            }
            else if (trozos.Length > 2)
            {
                Console.WriteLine("No hay jugadores conectados. Limpiando la tabla.");
                dt.Rows.Clear();
                onlineGrid.DataSource = dt;
                onlineGrid.Refresh();
            }
            else
            {
                Console.WriteLine("Mensaje mal formado o sin datos de jugadores.");
            }
        }

        private void ProcesarCartasJugador(string response, int cantidadCartasEsperadas)
        {
            if (!string.IsNullOrEmpty(response))
            {
                string[] ListaCartas = response.Split(',');

                // Código 21: Solo una carta (2 elementos: número y color)
                if (cantidadCartasEsperadas == 1 && ListaCartas.Length == 2)
                {
                    buttoncartamedio.BackColor = ObtenerColorDeCarta(ListaCartas[1]);
                    buttoncartamedio.Text = ListaCartas[0];
                    cartaMedio = new Carta(ListaCartas[0], ListaCartas[1]);
                }
                // Código 22: Múltiples cartas (pares de número y color)
                else if (cantidadCartasEsperadas == 4 && ListaCartas.Length == cantidadCartasEsperadas * 2)
                {
                    buttoncarta1.BackColor = ObtenerColorDeCarta(ListaCartas[1]);
                    buttoncarta2.BackColor = ObtenerColorDeCarta(ListaCartas[3]);
                    buttoncarta3.BackColor = ObtenerColorDeCarta(ListaCartas[5]);
                    buttoncarta4.BackColor = ObtenerColorDeCarta(ListaCartas[7]);
                    buttoncarta1.Text = ListaCartas[0];
                    buttoncarta2.Text = ListaCartas[2];
                    buttoncarta3.Text = ListaCartas[4];
                    buttoncarta4.Text = ListaCartas[6];

                    cartaJugador1 = new Carta(ListaCartas[0], ListaCartas[1]);
                    cartaJugador2 = new Carta(ListaCartas[2], ListaCartas[3]);
                    cartaJugador3 = new Carta(ListaCartas[4], ListaCartas[5]);
                    cartaJugador4 = new Carta(ListaCartas[6], ListaCartas[7]);
                }
                else
                {
                    MessageBox.Show($"El mensaje recibido no tiene el formato esperado para {cantidadCartasEsperadas} cartas.");
                }
            }
            else
            {
                MessageBox.Show("No se recibió información del servidor.");
            }
        }





        //Nos conectamos al servidor

        private void Connection_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9032);
            // CLIENTE IP: SHIVA =  10.4.119.5                VBOX = 192.168.56.102
            // CLIENTE PUERTO: SHIVA =  50061                 VBOX = 9050

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                indicadorConexion_label.ForeColor = Color.Green;
                MessageBox.Show("Connected");
                isConnected = true;
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


        //Nos desconectamos

        private void Desconnection_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            alreadyLogged = false;
            lblTitle.Text = "Welcome to UNO Game! Log in to play!";
            
            dt.Rows.Clear(); // Limpia todas las filas del DataTable
            onlineGrid.DataSource = null;
            onlineGrid.DataSource = dt;

            string mensaje = "0/" + username.Text;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            indicadorConexion_label.ForeColor = Color.Red;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            server = null;
            atender.Abort();
            isConnected = false;
        }


        // Registro

        private void Register_Click(object sender, EventArgs e)
{
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Both username and password fields must be filled out.");
                return;
            }

            string mensaje = "1/" + username.Text + "/" + password.Text;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            esperandoRespuesta = true;
}


        //Log In

        private void LogIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isConnected)
                {
                    MessageBox.Show("You are not connected to the server.");
                    return;
                }
                if (!alreadyLogged)
                {
                    alreadyLogged = true;
                    selectedUser = username.Text;
                    string mensaje = "2/" + username.Text + "/" + password.Text;
                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    esperandoRespuesta = true;
                    lblTitle.Text = "Welcome to UNO Game! You have logged in as " + selectedUser;
                }
                else
                {
                    MessageBox.Show("Ya has iniciado sesión. Desconectate antes de volver a intentarlo.");
                }
            } catch { MessageBox.Show("Error del cliente"); }
            
            
        }

        // Juego para comprobar notificiones entre varios clientes

        private void NotiRed_Click(object sender, EventArgs e)
        {
            EnviarMensajeCodigo("6/");
        }

        private void NotiBlue_Click(object sender, EventArgs e)
        {
            EnviarMensajeCodigo("7/");
        }

        private void NotiYellow_Click(object sender, EventArgs e)
        {
            EnviarMensajeCodigo("8/");
        }

        private void NotiGreen_Click(object sender, EventArgs e)
        {
            EnviarMensajeCodigo("9/");
        }

        private void howitworks_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This functionality shows you the last card selected on the server by all the clients that have connected to it.");
        }



        // unir a partida

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            string partida = txtBuscarPartida.Text;
            EnviarMensajeUnirPartida(partida);
        }
        private void btnCreateGame_Click(object sender, EventArgs e)
        {
            string partida = txtBuscarPartida.Text;
            EnviarMensajeCrearPartida(partida);
        }

        //------------------------- JUEGO  -------------------------\\



        // Estructura para representar una carta
        public class Carta
        {
            public string Numero { get; set; }
            public string Color { get; set; }

            public Carta(string numero, string color)
            {
                Numero = numero;
                Color = color;
            }
        }

        private Color ObtenerColorDeCarta(string color)
        {
            switch (color.ToLower())
            {
                case "azul":
                    return Color.Blue;
                case "rojo":
                    return Color.Red;
                case "verde":
                    return Color.Green;
                case "amarillo":
                    return Color.Yellow;
                default:
                    return Color.Gray; // Si no se encuentra el color, se pinta el botón de gris
            }
        }


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


        Carta cartaJugador1, cartaJugador2, cartaJugador3, cartaJugador4, cartaMedio;
        // Función para comprobar si la carta del jugador puede tirarse
        public bool PuedeTirarCarta(Carta cartaJugador, Carta cartaMedio)
        {
            return cartaJugador.Numero == cartaMedio.Numero || cartaJugador.Color == cartaMedio.Color;
        }

        private void button24_Click(object sender, EventArgs e)
        {

            MessageBox.Show(Convert.ToString(cartaMedio.Numero));

            MessageBox.Show(Convert.ToString(cartaJugador1.Numero));

            MessageBox.Show(Convert.ToString(cartaJugador2.Numero));

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

        private void panelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void onlineGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtenemos los nombres de los jugadores conectados desde la DataTable
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

            // Mostramos un mensaje para confirmar el envío
            //MessageBox.Show("Mensaje enviado con los jugadores: {string.Join(", ", playersList)}");
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










        // Manejador de errores del DataGridView
        private void onlineGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; // Evita mostrar el cuadro de error por defecto
        }


     

    }
}
