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
using System.IO;
using WindowsFormsApplication1;

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
        private string selectedUser = "";    // uso este string para evitar que se loguee un mismo usuario varias veces
        private bool alreadyLogged = false; // con este bool nos aseguramos que no hagas log in repetidamente sin desconectarte antes
        private string isCreator = "admin";
        List<string> Invitations = new List<string>();

        List<PartidaForm> formularios = new List<PartidaForm>();
        int mainform = 999;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            //CheckForIllegalCrossThreadCalls = false;

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
            panelUsuario.Hide();
            // ONLINE GRID DISEÑO

            // Cosas generales + datatable
            onlineGrid.AllowUserToOrderColumns = false;
            onlineGrid.AllowUserToAddRows = false; // Desactiva la fila adicional para agregar nuevas filas
            onlineGrid.ReadOnly = true; // Deshabilita la edición en todas las celdas

            dt.Columns.Add("PlayerName"); // Inicializa la datatable de usuarios
            onlineGrid.DataSource = dt;
            onlineGrid.RowHeadersVisible = false;

            onlineGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // no desbordan las casillas

            // Fondo general 
            onlineGrid.BackgroundColor = Color.Black;

            // Celdas
            onlineGrid.DefaultCellStyle.BackColor = Color.DarkGray;
            onlineGrid.DefaultCellStyle.ForeColor = Color.White;
            onlineGrid.DefaultCellStyle.Font = new Font("Segoe UI Emoji", 10);
            onlineGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Al clicar
            onlineGrid.DefaultCellStyle.SelectionBackColor = Color.Gray;
            onlineGrid.DefaultCellStyle.SelectionForeColor = Color.White;

            // Cabecera
            onlineGrid.EnableHeadersVisualStyles = false;
            onlineGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            onlineGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            onlineGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Emoji", 10, FontStyle.Bold);
            onlineGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // GAME GRID DISEÑO
            gamesGrid.AllowUserToOrderColumns = false;

            gamesGrid.AllowUserToAddRows = false; // Desactiva la fila adicional para agregar nuevas filas

            gamesGrid.ReadOnly = true; // Deshabilita la edición en todas las celdas


            dtGames.Columns.Add("GameName"); // Inicializa la datatable de usuarios
            gamesGrid.DataSource = dtGames;
            gamesGrid.RowHeadersVisible = false;

            gamesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Fondo general 
            onlineGrid.BackgroundColor = Color.Black;
            onlineGrid.BackgroundColor = Color.Black;

            // Celdas
            onlineGrid.DefaultCellStyle.BackColor = Color.DarkGray;
            onlineGrid.DefaultCellStyle.ForeColor = Color.White;
            onlineGrid.DefaultCellStyle.Font = new Font("Segoe UI Emoji", 10);
            onlineGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Cabecera
            onlineGrid.EnableHeadersVisualStyles = false;
            onlineGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            onlineGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            onlineGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Emoji", 10, FontStyle.Bold);
            onlineGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                // Cerrar todos los formularios secundarios.
                foreach (Form formulario in formularios)
                {
                    try
                    {
                        if (formulario.InvokeRequired)
                        {
                            formulario.Invoke((MethodInvoker)delegate
                            {
                                formulario.Close();
                            });
                        }
                        else
                        {
                            formulario.Close();
                        }

                        // Libera los recursos asociados.
                        formulario.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al cerrar el formulario: {ex.Message}");
                    }
                }

                // Cerrar conexión del servidor si está conectada.
                if (server != null && server.Connected)
                {
                    string mensaje = "0/" + mainform + "/" + selectedUser;
                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    indicadorConexion_label.ForeColor = Color.Red;

                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                    server = null;
                }

                // Detener hilo de escucha si está activo.
                isConnected = false;
                if (atender != null && atender.IsAlive)
                {
                    atender.Abort();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en btnClose_Click: {ex.Message}");
            }

            // Cerrar el formulario principal.
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

        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            string mensaje = $"{20}/{mainform}/{selectedUser}/{partida}";
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

            string mensaje = $"{25}/{mainform}/{selectedUser}/{partida}";
            
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            esperandoRespuesta = true;
        }

        //------------------------- LECTURA DE LOS MENSAGES ENVIADOS POR EL SERV -------------------------\\

        private void AtenderServidor()
        {
            HashSet<int> codigosSinMessageBox = new HashSet<int> { 6, 7, 8, 9, 16, 27, 21, 22, 25 };

            while (true)
            {
                try
                {
                    // Limpia el buffer antes de recibir datos
                    byte[] msg2 = new byte[512];
                    Array.Clear(msg2, 0, msg2.Length);
                    if (server != null && server.Connected)
                    {
                        // Recibe los datos del servidor
                        int bytesReceived = server.Receive(msg2);

                        // Procesa los datos si hay algo recibido
                        if (bytesReceived > 0)
                        {
                            string mensajeCompleto = Encoding.ASCII.GetString(msg2, 0, bytesReceived).Trim();
                            Console.WriteLine($"Mensaje completo recibido: {mensajeCompleto}");

                            // Validar que el mensaje no esté vacío
                            if (string.IsNullOrEmpty(mensajeCompleto))
                            {
                                Console.WriteLine("El mensaje recibido está vacío. Descartando.");
                                continue;
                            }

                            // Intentar dividir el mensaje en partes
                            string[] trozos = mensajeCompleto.Split('/');
                            if (trozos.Length < 1 || !int.TryParse(trozos[0], out int codigo))
                            {
                                Console.WriteLine("El mensaje recibido tiene un formato inválido. Descartando.");
                                continue;
                            }

                            // Extraer la respuesta si existe
                            string response = trozos.Length > 1 ? string.Join("/", trozos.Skip(1)) : string.Empty;

                            Console.WriteLine($"Código recibido: {codigo}");
                            Console.WriteLine($"Respuesta extraída: {response}");

                            // Validación adicional de mensajes corruptos o inválidos
                            if (codigo < 0 || codigo > 100)
                            {
                                Console.WriteLine("Mensaje descartado por tener un código fuera del rango permitido.");
                                continue;
                            }
                            int nForm;


                            switch (codigo)
                            {
                                case 1:
                                    nForm = Convert.ToInt32(trozos[1]);
                                    response = trozos[2].Split('\0')[0];
                                    this.Invoke(new Action(() => MostrarMensaje(response)));

                                    //SafeInvoke(username, () => username.Text = "");

                                    //SafeInvoke(password, () => password.Text = "");

                                    esperandoRespuesta = false;
                                    break;
                                case 2:
                                    nForm = Convert.ToInt32(trozos[1]);
                                    response = trozos[2].Split('\0')[0];
                                    if (esperandoRespuesta)
                                    {
                                        esperandoRespuesta = false;
                                        if (response.StartsWith("Player"))
                                        {
                                            alreadyLogged = true;
                                            SafeInvoke(lblTitle, () =>
                                            {
                                                lblTitle.Text = "Welcome to UNO Game Main Menu! You have logged in as " + selectedUser;
                                                
                                            });
                                            //SafeInvoke(username, () => username.Text = "");
                                            //SafeInvoke(password, () => password.Text = "");
                                            SafeInvoke(lblUserNameLittle, () => lblUserNameLittle.Text = selectedUser);
                                            SafeInvoke(panelUsuario, () => panelUsuario.Show());
                                        }
                                        else
                                        {
                                            this.Invoke(new Action(() => MostrarMensaje(response)));
                                            //SafeInvoke(password, () => password.Text = "");
                                        }
                                    }
                                    break;
                                case 53:
                                    // Extraer información del mensaje
                                    if (trozos.Length >= 3)
                                    {
                                        nForm = Convert.ToInt32(trozos[1]); // Número de formulario
                                        string ganador = trozos[2].Split('\0')[0]; // Extraer el nombre del ganador

                                        // Actualizar el label13 en la interfaz de usuario
                                        SafeInvoke(label13, () =>
                                        {
                                            label13.Text = ""; // Limpiar el contenido del label
                                            label13.Text = $"Winner: {ganador}"; // Mostrar el nombre del ganador
                                        });

                                        Console.WriteLine($"Código 53 procesado. Winner: {ganador}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("El mensaje para el código 53 tiene un formato incorrecto.");
                                    }
                                    break;

                                case 20:
                                    nForm = Convert.ToInt32(trozos[1]);
                                    response = trozos[2].Split('\0')[0];
                                    if (esperandoRespuesta)
                                    {
                                        if (response.StartsWith("Done"))
                                        {
                                            gameName = response.Split(' ')[1];
                                            this.Invoke(new Action(() => inicializarform2(gameName)));
                                        }
                                        else
                                        {
                                            this.Invoke(new Action(() => MostrarMensaje(response)));
                                        }
                                        esperandoRespuesta = false;
                                    }
                                    break;
                                case 25:
                                    nForm = Convert.ToInt32(trozos[1]);
                                    response = trozos[2].Split('\0')[0];
                                    if (esperandoRespuesta)
                                    {
                                        if (response.StartsWith("Done"))
                                        {
                                            isCreator = selectedUser;
                                            gameName = response.Split(' ')[1];
                                            this.Invoke(new Action(() => inicializarform2(gameName)));
                                        }
                                        else
                                        {
                                            this.Invoke(new Action(() => MostrarMensaje(response)));
                                        }
                                        esperandoRespuesta = false;
                                    }
                                    break;

                                case 15:
                                    nForm = Convert.ToInt32(trozos[1]);
                                    response = trozos[2].Split('\0')[0];
                                    this.Invoke(new Action(() => ProcesarListaJugadores1(trozos)));
                                    break;

                                case 26:
                                    nForm = Convert.ToInt32(trozos[1]);
                                    response = trozos[2].Split('\0')[0];
                                    this.Invoke(new Action(() => ProcesarLista2(trozos)));
                                    break;

                                case 22: // Maneja el mazo del jugador
                                    trozos = response.Split('/');
                                    nForm = Convert.ToInt32(trozos[0]);
                                    response = trozos[1];
                                    formularios[nForm].TomaMazoJugador(response);
                                    //this.Invoke(new Action(() => ProcesarCartasJugador(response, 4)));
                                    break;

                                case 37: // Maneja la carta central
                                    trozos = response.Split('/');
                                    nForm = Convert.ToInt32(trozos[0]);
                                    response = trozos[1];
                                    string turno = trozos[2];
                                    string mazo = trozos[4];
                                    formularios[nForm].TomaCartaCentral(response);
                                    formularios[nForm].TomaTurno(turno);
                                    formularios[nForm].TomaMazoJugador(mazo);
                                    //this.Invoke(new Action(() => ProcesarCartasJugador(response, 1)));
                                    break;
                                case 29:
                                    trozos = response.Split('/');
                                    nForm = Convert.ToInt32(trozos[0]);
                                    response = trozos[1];
                                    turno = trozos[2];
                                    formularios[nForm].TomaCartaCentral(response);
                                    formularios[nForm].TomaTurno(turno);
                                    //this.Invoke(new Action(() => ProcesarCartasJugador(response, 1)));
                                    break;
                                case 31:
                                    trozos = response.Split('/');
                                    nForm = Convert.ToInt32(trozos[0]);
                                    string turno_robar = trozos[1];
                                    formularios[nForm].TomaTurno(turno_robar);
                                    break;
                                case 73:
                                    Console.WriteLine("Procesando mensaje para código 73:");
                                    Console.WriteLine("Mensaje completo recibido: " + response);

                                    // Validar que el mensaje tiene el formato esperado
                                    if (!response.Contains("/numjug/"))
                                    {
                                        Console.WriteLine("El mensaje no contiene '/numjug/' necesario para procesar.");
                                        break;
                                    }

                                    // Verificar si el mensaje contiene el prefijo 999 y eliminarlo
                                    if (response.StartsWith("999/") || response.StartsWith("9991/"))
                                    {
                                        Console.WriteLine("El mensaje contiene el prefijo '999/' o '9991/'. Eliminándolo...");
                                        response = response.Substring(4); // Eliminar los primeros 4 caracteres ("999/")
                                    }

                                    // Pasar el mensaje modificado a ActualizarPartidasGrid
                                    this.Invoke(new Action(() => ActualizarPartidasGrid(response)));
                                    break;

                                case 30: // Delete confirmation
                                    nForm = Convert.ToInt32(trozos[1]);
                                    response = trozos[2].Split('\0')[0];

                                    // Display the confirmation message received from the server
                                    if (response.StartsWith("Player successfully deleted"))
                                    {
                                        SafeInvoke(this, () =>
                                        {
                                            MessageBox.Show(response, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        });

                                        // Optional: Additional actions, such as updating the UI, can be placed here
                                    }
                                    else
                                    {
                                        SafeInvoke(this, () =>
                                        {
                                            MessageBox.Show(response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        });
                                    }
                                    break;
                               
                                case 58:
                                   
                                    nForm = Convert.ToInt32(trozos[1]); 
                                    response = trozos[2].Split('\0')[0]; 

                                    
                                    SafeInvoke(lblresphours, () =>
                                    {
                                        lblresphours.Text = ""; 
                                        lblresphours.Text = response; 
                                    });
                                    break;



                                case 57:
                                   
                                    nForm = Convert.ToInt32(trozos[1]); 
                                    response = trozos[2].Split('\0')[0];

                                   
                                    SafeInvoke(playerswith, () =>
                                    {
                                        playerswith.Text = ""; 
                                        playerswith.Text = response; 
                                    });
                                    break;


                                case 96: // INVITATION 1
                                    trozos = response.Split('/');
                                    nForm = Convert.ToInt32(trozos[0]);
                                    response = trozos[1];
                                    this.Invoke(new Action(() => Invitation1(mensajeCompleto)));
                                    break;

                                case 97: // INVITATION RESPONSE (RECIBIDA)
                                    {
                                        try
                                        {
                                            // Dividir el mensaje completo en partes
                                            string[] trozos97 = mensajeCompleto.Split('/');

                                            // Verificar que el mensaje tiene al menos los elementos necesarios
                                            if (trozos97.Length >= 5)
                                            {
                                                // Extraer las partes del mensaje
                                                string decision = trozos97[2];  // Decisión tomada (0: rechazado, 1: aceptado o "correcto")
                                                string partida = trozos97[3];   // Nombre de la partida
                                                string invitado = trozos97[4].Trim(); // Última parte es el invitado (sin comas)

                                                // Verificar si el usuario actual es el invitado
                                                if (selectedUser.Trim() == invitado)
                                                {
                                                    MessageBox.Show($"You have joined the game '{partida}'.");

                                                    // Abrir el formulario de la partida
                                                    this.Invoke(new Action(() => inicializarform2(partida)));
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Notification 97 ignored for user '{selectedUser}' as it is for another user.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid format for notification 97. Insufficient parts.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"Exception while processing code 97: {ex.Message}");
                                        }
                                        break;
                                    }



                                case 98: // INVITATION RECEIVED
                                    trozos = response.Split('/');
                                    nForm = Convert.ToInt32(trozos[0]);
                                    response = trozos[1];
                                    string[] partes = mensajeCompleto.Split('/');
                                    Console.WriteLine("Contenido: " + mensajeCompleto);
                                    Console.WriteLine("Contenido 2: " + partes[1]);
                                    if (partes.Length == 2)
                                    {
                                        string invitador = partes[1];
                                        this.Invoke(new Action(() =>
                                        {
                                            if (selectedUser == invitador)
                                            {
                                                MessageBox.Show("Se ha rechazado la invitación.");
                                            }
                                        }));
                                    }
                                    break;

                                case 24:
                                    trozos = response.Split('/');
                                    nForm = Convert.ToInt32(trozos[0]);
                                    response = trozos[1];
                                    {
                                        string username = trozos[1];
                                        string message = trozos[2];
                                        string chatMessage = $"{username}: {message}";

                                        formularios[nForm].TomaMensajeChat(chatMessage);

                                        /*
                                        this.Invoke(new Action(() => {
                                            labelCHAT.Text += chatMessage + Environment.NewLine;
                                        }));
                                        */
                                    }
                                    break;




                                default:
                                    Console.WriteLine("Código no reconocido: " + codigo);
                                    break;
                            }
                        }
                    }
                }
                catch (SocketException ex)
                {
                    this.Invoke(new Action(() => MessageBox.Show("Se perdió la conexión con el servidor: " + ex.Message)));
                    break;
                }
            }
        }




        // Métodos auxiliares para refactorizar el código
        private void Invitation1(string mensajeCompleto)
        {
            // Muestra el mensaje completo recibido en la consola
            Console.WriteLine($"Mensaje completo recibido: {mensajeCompleto}");

            // Divide el mensaje en partes usando "/"
            string[] partes = mensajeCompleto.Split('/');

            // Verifica si el mensaje incluye el formulario al inicio
            if (partes.Length >= 5 && int.TryParse(partes[0], out _))
            {
                partes = partes.Skip(1).ToArray(); // Ignorar el código y el formulario (primer elemento)
            }

            // Comprueba si el mensaje tiene exactamente 4 partes después de la validación
            if (partes.Length == 4)
            {
                string invitador = partes[1]; // Quien envía la invitación
                string invitado = partes[2];  // A quién se envía la invitación
                string partida = partes[3];   // Nombre de la partida
                string decision = partes[0];  // Decisión actual

                // Imprime los valores de los parámetros para verificar su contenido
                //Console.WriteLine($"Invitador: {invitador}");
                //Console.WriteLine($"Invitado: {invitado}");
                //Console.WriteLine($"Partida: {partida}");
                //Console.WriteLine($"Decisión: {decision}");

                // Comprueba si el usuario actual es el invitado
                if (selectedUser.Trim() == invitado.Trim())
                {
                    // Crea una instancia del nuevo formulario con los parámetros
                    using (InvitationForm invitationForm = new InvitationForm(invitador, partida))
                    {
                        // Muestra el formulario de forma modal
                        invitationForm.ShowDialog();

                        // Revisa la respuesta del usuario
                        if (invitationForm.IsAccepted)
                        {
                            MessageBox.Show("Has aceptado la invitación.");
                            // Lógica para unirse a la partida
                            string mensaje = $"97/{mainform}/{invitador}/{invitado}/{partida}/1";
                            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }
                        else
                        {
                            MessageBox.Show("Has rechazado la invitación.");
                            string mensaje = $"97/{mainform}/{invitador}/{invitado}/{partida}/0";
                            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("El mensaje no es relevante para este usuario.");
                }
            }
            else
            {
                Console.WriteLine("El mensaje no tiene el formato esperado.");
            }
        }









        private void ProcesarCodigo97(string response)
        {
            string[] trozos = response.Split('/');

            if (trozos.Length > 0)
            {
                if (int.TryParse(trozos[0], out int type_operation))
                {
                    if (type_operation == 0) // Respuesta a nuestra invitación
                    {
                        ProcesarRespuestaInvitacion(trozos);
                    }
                    else if (type_operation == 1) // Recibimos una invitación de otro jugador
                    {
                        ProcesarInvitacionRecibida(trozos);
                    }
                }
            }
        }

        private void ProcesarRespuestaInvitacion(string[] trozos)
        {
            if (trozos.Length >= 3)
            {
                string tipo = trozos[1];

                if (tipo == "correcto")
                {
                    string invitedList = trozos[2];
                    //MessageBox.Show("Invitation sent successfully to: " + invitedList);
                }
                else
                {
                    //MessageBox.Show("Failed to send the invitation.");
                }
            }
        }

        private void ProcesarInvitacionRecibida(string[] trozos)
        {
            if (trozos.Length >= 2)
            {
                string inviting = trozos[1];
                //MessageBox.Show("You have received an invitation from: " + inviting);
            }
        }




        private void ProcesarListaJugadores1(string[] trozos)
        {
            Console.WriteLine($"Mensaje recibido del servidor: {string.Join("/", trozos)}");

            if (trozos.Length > 6 && !string.IsNullOrEmpty(trozos[2].Trim()) && !string.IsNullOrEmpty(trozos[4].Trim()) && !string.IsNullOrEmpty(trozos[6].Trim()))
            {
                // Procesar jugadores conectados
                string[] connectedPlayerList = trozos[3]
                    .Split(',')
                    .Select(p => p.Trim())
                    .Where(p => !string.IsNullOrEmpty(p))
                    .ToArray();

                Console.WriteLine("Lista actual de jugadores conectados:");
                foreach (var player in connectedPlayerList)
                {
                    Console.WriteLine($"- {player}");

                    // Verificar si el jugador ya está en la tabla
                    bool exists = dt.AsEnumerable().Any(row => row.Field<string>("PlayerName") == player);
                    if (!exists)
                    {
                        dt.Rows.Add(player); // Agregar el jugador si no existe
                    }
                }

                SafeInvoke(onlineGrid, () =>
                {
                    onlineGrid.DataSource = dt;
                    onlineGrid.Refresh(); // Actualizar la vista del grid
                });

                // Procesar partidas disponibles
                string[] gameList = trozos[5]
                    .Split(',')
                    .Select(g => g.Trim())
                    .Where(g => !string.IsNullOrEmpty(g))
                    .ToArray();

                string[] playerCountList = trozos[7]
                    .Split(',')
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToArray();

                Console.WriteLine("Lista actual de partidas disponibles:");
                for (int i = 0; i < gameList.Length; i++)
                {
                    string game = gameList[i];
                    string playerCount = (i < playerCountList.Length) ? playerCountList[i] : "0";

                    Console.WriteLine($"- {game}: {playerCount} jugadores");

                    // Verificar si la partida ya está en la tabla
                    bool exists = dtGames.AsEnumerable().Any(row => row.Field<string>("GameName") == game);
                    if (!exists)
                    {
                        dtGames.Rows.Add(game); // Agregar la partida si no existe
                    }
                }

                SafeInvoke(gamesGrid, () =>
                {
                    gamesGrid.DataSource = dtGames;
                    gamesGrid.Refresh(); // Actualizar la vista del grid

                    // Aplicar colores a las celdas según el número de jugadores
                    for (int i = 0; i < gamesGrid.Rows.Count; i++)
                    {
                        string gameName = gamesGrid.Rows[i].Cells[0].Value.ToString();
                        string playerCount = (i < playerCountList.Length) ? playerCountList[i] : "0";
                        int count = int.TryParse(playerCount, out int result) ? result : 0;

                        if (count == 4)
                        {
                            gamesGrid.Rows[i].Cells[0].Style.BackColor = Color.LightCoral; // Rojo para 4 jugadores
                        }
                        else
                        {
                            gamesGrid.Rows[i].Cells[0].Style.BackColor = Color.LightGreen; // Verde para menos de 4 jugadores
                        }
                    }
                });

                Console.WriteLine("Actualización de DataGrids completada.");
            }
            else
            {
                Console.WriteLine("No hay jugadores conectados o partidas disponibles.");
            }
        }










        private void ProcesarLista2(string[] trozos)
        {
            Console.WriteLine($"Mensaje recibido del servidor: {string.Join("/", trozos)}");

            if (trozos.Length > 3 && !string.IsNullOrEmpty(trozos[1].Trim()) && !string.IsNullOrEmpty(trozos[3].Trim()))
            {
                // Procesar la lista de partidas
                string[] gameList = trozos[2]
                    .Split(',')
                    .Select(g => g.Trim())
                    .Where(g => !string.IsNullOrEmpty(g))
                    .ToArray();

                string[] playerCounts = trozos[4]
                    .Split(',')
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToArray();

                Console.WriteLine("Lista actual de partidas disponibles:");
                dtGames.Rows.Clear(); // Limpiar tabla antes de actualizar

                for (int i = 0; i < gameList.Length; i++)
                {
                    string game = gameList[i];
                    string playerCount = (i < playerCounts.Length) ? playerCounts[i] : "0";

                    dtGames.Rows.Add(game); // Agregar la partida a la tabla

                    Console.WriteLine($"- {game}: {playerCount} jugadores");
                }

                SafeInvoke(gamesGrid, () =>
                {
                    gamesGrid.DataSource = null; // Desvincular para evitar conflictos
                    gamesGrid.DataSource = dtGames; // Vincular nuevamente
                    gamesGrid.Refresh();

                    // Recorrer las filas y colorear según el número de jugadores
                    foreach (DataGridViewRow row in gamesGrid.Rows)
                    {
                        string gameName = row.Cells[0].Value?.ToString();
                        int playerCount = int.Parse(playerCounts[Array.IndexOf(gameList, gameName)]);

                        if (playerCount == 4)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral; // Rojo claro para partidas con 4 jugadores
                        }
                        else if (playerCount < 4)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen; // Verde claro para partidas con menos de 4 jugadores
                        }
                    }
                });

                Console.WriteLine("Actualización de gamesGrid completada.");
            }
            else
            {
                Console.WriteLine("No hay partidas disponibles.");
            }
        }

        private void ActualizarPartidasGrid(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
            {
                Console.WriteLine("El mensaje recibido está vacío.");
                return;
            }

            // Dividir el mensaje en secciones usando "/numjug/"
            string[] secciones = response.Split(new[] { "/numjug/" }, StringSplitOptions.None);
            if (secciones.Length != 2)
            {
                Console.WriteLine("El mensaje no contiene las secciones esperadas. Formato incorrecto.");
                return;
            }

            // Extraer partidas y números de jugadores
            string[] partidas = secciones[0].Split(',');  // Partidas
            string[] numJugadores = secciones[1].Split(',');  // Números de jugadores

            Console.WriteLine("Partidas recibidas: " + string.Join(", ", partidas));
            Console.WriteLine("Número de jugadores recibidos: " + string.Join(", ", numJugadores));

            if (partidas.Length == 0 || numJugadores.Length == 0)
            {
                Console.WriteLine("No hay datos de partidas o jugadores para procesar.");
                return;
            }

            // Actualizar el DataGridView
            SafeInvoke(gamesGrid, () =>
            {
                // Limpia el DataTable antes de agregar nuevas filas
                dtGames.Rows.Clear();

                for (int i = 0; i < partidas.Length; i++)
                {
                    string partida = partidas[i].Trim();
                    string jugadores = i < numJugadores.Length ? numJugadores[i].Trim() : "0";

                    if (int.TryParse(jugadores, out int numJugadoresInt))
                    {
                        // Agregar la fila al DataTable
                        DataRow nuevaFila = dtGames.NewRow();
                        nuevaFila["GameName"] = partida; // Asegúrate de que la columna en el DataTable se llama "GameName"
                        dtGames.Rows.Add(nuevaFila);

                        // Buscar la fila correspondiente en el DataGridView para colorearla
                        var rowIndex = dtGames.Rows.Count - 1;
                        var row = gamesGrid.Rows[rowIndex];
                        row.DefaultCellStyle.BackColor = numJugadoresInt == 4 ? Color.LightCoral : Color.LightGreen;
                    }
                    else
                    {
                        Console.WriteLine($"Error interpretando el número de jugadores para la partida '{partida}'.");
                    }
                }

                gamesGrid.Refresh();
            });
        }







        /*
        private void ProcesarRobadaJugador(string response, string posicion)
        {

            if (!string.IsNullOrEmpty(response))
            {
                string[] ListaCartas = response.Split(',');
                string[] color = ListaCartas[1].Split('/');




                // Código 100: Robar una carta (2 elementos: número y color)
                if (posicion == "1" && ListaCartas.Length == 2)
                {
                    SafeInvoke(buttoncarta1, () => {
                        buttoncarta1.BackColor = ObtenerColorDeCarta(color[0]);
                        buttoncarta1.Text = ListaCartas[0];
                        cartaJugador1.Color = color[0];
                        cartaJugador1.Numero = buttoncarta1.Text;

                    });
                }
                if (posicion == "2" && ListaCartas.Length == 2)
                {
                    SafeInvoke(buttoncarta2, () => {
                        buttoncarta2.BackColor = ObtenerColorDeCarta(color[0]);
                        buttoncarta2.Text = ListaCartas[0];
                        cartaJugador2.Color = color[0];
                        cartaJugador2.Numero = buttoncarta2.Text;
                    });
                }
                if (posicion == "3" && ListaCartas.Length == 2)
                {
                    SafeInvoke(buttoncarta3, () => {
                        buttoncarta3.BackColor = ObtenerColorDeCarta(color[0]);
                        buttoncarta3.Text = ListaCartas[0];
                        cartaJugador3.Color = color[0];
                        cartaJugador3.Numero = buttoncarta3.Text;
                    });
                }
                if (posicion == "4" && ListaCartas.Length == 2)
                {
                    SafeInvoke(buttoncarta4, () => {
                        buttoncarta4.BackColor = ObtenerColorDeCarta(color[0]);
                        buttoncarta4.Text = ListaCartas[0];
                        cartaJugador4.Color = color[0];
                        cartaJugador4.Numero = buttoncarta4.Text;
                    });
                }
            }
        }
        */
        /*
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
                    SafeInvoke(buttoncarta1, () => {
                        buttoncarta1.BackColor = ObtenerColorDeCarta(ListaCartas[1]);
                        buttoncarta1.Text = ListaCartas[0];
                    });

                    SafeInvoke(buttoncarta2, () => {
                        buttoncarta2.BackColor = ObtenerColorDeCarta(ListaCartas[3]);
                        buttoncarta2.Text = ListaCartas[2];
                    });

                    SafeInvoke(buttoncarta3, () => {
                        buttoncarta3.BackColor = ObtenerColorDeCarta(ListaCartas[5]);
                        buttoncarta3.Text = ListaCartas[4];
                    });

                    SafeInvoke(buttoncarta4, () => {
                        buttoncarta4.BackColor = ObtenerColorDeCarta(ListaCartas[7]);
                        buttoncarta4.Text = ListaCartas[6];
                    });



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


        */


        //Nos conectamos al servidor

        private void Connection_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("10.4.119.5");
            IPEndPoint ipep = new IPEndPoint(direc, 50062);
            // CLIENTE IP: SHIVA =  10.4.119.5                VBOX = 192.168.56.102
            // CLIENTE PUERTO: SHIVA =  50061                 VBOX = 9050

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                SafeInvoke(indicadorConexion_label, () => {
                    indicadorConexion_label.ForeColor = Color.Green;
                });
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

        private void Disconnection_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            alreadyLogged = false;
            lblTitle.Text = "Welcome to UNO Game Main Menu! Log in to play!";

            dt.Rows.Clear(); // Limpia todas las filas del DataTable
            onlineGrid.DataSource = null;
            SafeInvoke(onlineGrid, () => {
                onlineGrid.DataSource = dt;
            });
            panelUsuario.Hide();
            string mensaje = "0/" + mainform + "/" + selectedUser;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            SafeInvoke(indicadorConexion_label, () => {
                indicadorConexion_label.ForeColor = Color.Red;
            });
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
            if (username.Text == "admin")
            {
                MessageBox.Show("This username is not available.");
                return;
            }

            string mensaje = "1/" + mainform + "/" + username.Text + "/" + password.Text;
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
                    selectedUser = username.Text;
                    string mensaje = "2/" + mainform + "/" + username.Text + "/" + password.Text;
                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    esperandoRespuesta = true;
                }
                else
                {
                    MessageBox.Show("Ya has iniciado sesión. Desconectate antes de volver a intentarlo.");
                }
            }
            catch { MessageBox.Show("Error del cliente"); }


        }

        // unir a partida
        string gameName;

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarPartida.Text))
            {
                MessageBox.Show("El campo de texto no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string partida = txtBuscarPartida.Text;
            EnviarMensajeUnirPartida(partida);
            gameName = partida;
            //inicializarform2(gameName);
        }
        private void btnCreateGame_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarPartida.Text))
            {
                MessageBox.Show("El campo de texto no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string partida = txtBuscarPartida.Text;
            EnviarMensajeCrearPartida(partida);
            gameName = partida;
            //inicializarform2(gameName);
        }


        private void onlineGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) // Verifica si se hace clic en la cabecera
            {
                return;
            }
            else
            {
                string invited;

                // Verifica si la label xtBuscarPartida está vacía
                if (string.IsNullOrWhiteSpace(txtBuscarPartida.Text))
                {
                    MessageBox.Show("Unete a una partida antes");
                    return;
                }

                // NOMES POT CONVIDAR EL HOST DE LA PARTIDA
                if (onlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    invited = onlineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    string mensaje = $"96/{mainform}/" + selectedUser + "/" + invited + "/" + txtBuscarPartida.Text;
                    //MessageBox.Show(mensaje);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
        }


        private void gamesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) // Verifica si se hace clic en la cabecera
            {
                // No hacer nada, ya que no queremos que pase nada al hacer clic en la cabecera.
                return;
            }
            if (e.RowIndex >= 0)
            {
                // Obtener el nombre de la partida desde la celda seleccionada
                string partida = gamesGrid.Rows[e.RowIndex].Cells[0].Value?.ToString();

                if (!string.IsNullOrEmpty(partida))
                {
                    // Reutilizar el código de btnJoinGame_Click
                    EnviarMensajeUnirPartida(partida);
                    gameName = partida;


                }
                else
                {
                    MessageBox.Show("El nombre de la partida está vacío. Seleccione una partida válida.");
                }
            }

        }

        private void lblAvailableGames_Click(object sender, EventArgs e)
        {

        }

        private void invitationinfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In order to invite a player to your own game, you must first enter the name of the game in the box above the create button. Then go to the table of connected players and click on the player you want to play with. Finally you will get an acceptance message from the guest if he/she accepts or not. ");
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        // multiple
        private void PonerEnMarchaFormulario(string gameName)
        {
            int cont = formularios.Count;
            string partidaName = gameName; // Obtener el nombre de la partida desde el TextBox
            PartidaForm f = new PartidaForm(cont, server, selectedUser, isConnected, this, partidaName, isCreator);
            formularios.Add(f);
            f.ShowDialog();
        }


        private void inicializarform2(string gameName)
        {
            ThreadStart ts = delegate { PonerEnMarchaFormulario(gameName); };
            Thread T = new Thread(ts);
            T.Start();
        }


        private void gamesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonChangePswd_Click(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }


        // Manejador de errores del DataGridView
        private void onlineGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; // Evita mostrar el cuadro de error por defecto
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        
        private Color[] colores = { Color.Red, Color.Green, Color.Blue, Color.Orange, Color.Black, Color.White, Color.Pink }; // Small array of colors
        private int colorIndex = 0; // Índice para rastrear el color actual

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            // Establece el color del Label con el color actual
            lblUserProfile.ForeColor = colores[colorIndex];
            lblUserNameLittle.ForeColor = colores[colorIndex];

            // Avanza al siguiente color, reiniciando al principio cuando llegue al final
            colorIndex = (colorIndex + 1) % colores.Length;

            // Cambia también el color del botón (opcional)
            btnChangeColor.ForeColor = colores[colorIndex];
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            eliminarUsuarioForm nuevo = new eliminarUsuarioForm(server, selectedUser,isConnected);
            nuevo.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedUser))
            {
                MessageBox.Show("You must be logged in to perform this action.");
                return;
            }

            
            string mensaje = $"57/{mainform}/{selectedUser}";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);

            
            server.Send(msg);

            
            esperandoRespuesta = true;

            
        }

        private void panelUsuario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gamesinhours_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            // Obtener el número de formulario y las horas ingresadas
            int numForm = mainform; // Usamos el número de formulario principal
            string horas = lblgameshours.Text.Trim(); // Eliminar espacios adicionales

            // Validar que el usuario haya ingresado un valor válido
            if (string.IsNullOrEmpty(horas) || !int.TryParse(horas, out int horasNum) || horasNum <= 0)
            {
                MessageBox.Show("Please enter a valid number of hours.");
                return;
            }

            // Construir el mensaje para enviar al servidor
            string message = $"58/{numForm}/{horas}";

            // Enviar el mensaje al servidor
            byte[] msg = Encoding.ASCII.GetBytes(message);
            server.Send(msg);

            // Informar en el cliente que el mensaje se envió
            Console.WriteLine($"Mensaje enviado al servidor: {message}");
            esperandoRespuesta = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void lblresphours_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblgameshours_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void resultados_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedUser))
            {
                MessageBox.Show("You must be logged in to perform this action.");
                return;
            }

            // Obtener el jugador ingresado en el textbox
            string jugador2 = resultadostex.Text.Trim();

            if (string.IsNullOrEmpty(jugador2))
            {
                MessageBox.Show("Please enter a valid player name.");
                return;
            }

            // Construir el mensaje para el servidor
            string mensaje = $"53/{mainform}/{selectedUser}/{jugador2}";

            // Enviar el mensaje al servidor
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Indicar que estamos esperando una respuesta
            esperandoRespuesta = true;

            // Informar en la consola del cliente
            Console.WriteLine($"Mensaje enviado al servidor: {mensaje}");
        }
    }
}