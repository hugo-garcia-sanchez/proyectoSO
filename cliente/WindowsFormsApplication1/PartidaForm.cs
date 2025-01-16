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
using System.Net;
using System.Net.Sockets;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using ClientApplication;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApplication1
{
    public partial class PartidaForm : Form
    {
        int nForm;
        Socket server;
        string selectedUser;
        string partidaName; // Variable para almacenar el nombre de la partida
        public string turn;        // Almacena el nombre de a quien le toca jugar
        private bool maximizado = false;
        private bool isConnected;
        private string isCreator;
        private bool alreadyStarted = false;
        private List<Carta> cartasEnJuego = new List<Carta>(); // Lista de cartas en juego
        private List<CartaBoton> botonesEnJuego = new List<CartaBoton>(); // Lista de botones de cartas en juego
        private List<Control> controlesGenericos = new List<Control>(); // Lista de controles adicionales

        private Carta cartaCentro;
        private CartaBoton cartaBotonCentro;

        

        public PartidaForm(int nForm, Socket server, string selectedUser, bool isConnected, Form1 form1, string partidaName, string isCreator)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.selectedUser = selectedUser;
            this.isConnected = isConnected;
            this.partidaName = partidaName; // Asigna el nombre de la partida a la variable local
            this.isCreator = isCreator;
        }


        private void PartidaForm_Load(object sender, EventArgs e)
        {
            fourcards.Hide();
            label1.Text = "Formulario: " + nForm;
            lblGameTitle.Text = $"You are playing as {selectedUser} in game {partidaName}"; // Muestra el nombre de la partida
            label2.Text = "La partida aún no ha comenzado.";
        }


        public void TomaTurno(string turno)
        {
            turn = turno;
            if (label2.InvokeRequired)
            {
                label2.Invoke(new Action(() =>
                {
                    label2.Text = "Turn: " + turn;
                }));
            }
            else
            {
                label2.Text = "Turn: " + turn;
            }
        }


        private void sendbuttom_Click(object sender, EventArgs e)
        {
            if (server == null || !server.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedUser))
            {
                MessageBox.Show("You must log in first before sending a message.");
                return;
            }

            if (string.IsNullOrWhiteSpace(lblsend.Text))
            {
                MessageBox.Show("To send a message, you must first type it in the input bar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(lblGameTitle.Text)) // Verificar si se tiene el nombre de la partida
            {
                MessageBox.Show("You must be in a game to send a message.");
                return;
            }

            // Extraer el nombre puro de la partida desde lblGameTitle.Text
            string gameName = lblGameTitle.Text.Split(new[] { "in game" }, StringSplitOptions.None).Last().Trim();

            // Crear el mensaje incluyendo el nombre de la partida
            string mensaje = $"24/{nForm}/{selectedUser}/{lblsend.Text}/{gameName}";

            // Depuración: Mostrar el mensaje antes de enviarlo
            Console.WriteLine($"Mensaje enviado al servidor: {mensaje}");

            // Enviar el mensaje al servidor
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Limpiar el campo de texto después de enviar el mensaje
            lblsend.Text = string.Empty;

            // Esperando respuesta (si es necesario)
            // esperandoRespuesta = true;
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void TomaMensajeChat(string chatMessage)
        {
            this.Invoke(new Action(() =>
            {
                this.textBoxCHAT.AppendText(chatMessage + Environment.NewLine);
            }));
        }

        public void TomaMazoJugador(string response)
        {
            this.Invoke(new Action(() =>
            {
                ProcesarCartasJugador(response, cartasJugador);
            }));
        }
        public void TomaCartaCentral(string response)
        {
            this.Invoke(new Action(() =>
            {
                ProcesarCartaCentro(response);
            }));
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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




        /////////////////////////////////////////////////////////// Cartas /////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////// MÉTODOS /////////////////////////////////////////////////////////////////////////

        // Para tirar cartas
        public bool PuedeTirarCarta(Carta cartaJugador, Carta cartaMedio)
        {
            if (turn == selectedUser)
            {
                // Validar si cartaMedio es null
                if (cartaMedio == null)
                {
                    return false; // No se puede comparar con una carta nula
                }

                // Validar si cartaJugador es null (opcional, pero puede ser útil)
                if (cartaJugador == null)
                {
                    return false; // No se puede tirar si la carta del jugador es nula
                }

                // Si la carta es especial (por ejemplo "salto", "cambio", "roba2"), siempre se puede tirar
                if (EsCartaEspecial(cartaJugador))
                {
                    return true; // Siempre se puede tirar una carta especial
                }

                // Si la carta es comodín (en este caso asumimos que no lo estamos procesando ahora, pero se podría agregar)
                if (cartaJugador.Color == ColorCarta.Comodin)
                {
                    return true; // El comodín puede ser jugado en cualquier momento
                }

                // Para cartas numéricas o de tipo específico, compararlas por color o número
                return cartaJugador.Valor == cartaMedio.Valor || cartaJugador.Color == cartaMedio.Color;
            }
            else
            {
                return false;
            }

        }
        private bool EsCartaEspecial(Carta carta)
        {
            // Consideramos como especiales las cartas que no son numéricas
            return Convert.ToString(carta.Tipo) == "salto" || Convert.ToString(carta.Tipo) == "cambio" || Convert.ToString(carta.Tipo) == "roba2";
        }// Método para verificar si la carta es especial (salto, cambio, roba2, etc.)

        // Para borrar cartas
        public void BorrarCartas(int index)
        {
            if (index == 999)
            {
                cartasEnJuego.Clear(); // Elimina todas las cartas
            }
            else if (index >= 0 && index < cartasEnJuego.Count)
            {
                cartasEnJuego.RemoveAt(index); // Elimina una carta específica
            }
        }
        public void BorrarBotonesCarta(int index)
        {
            if (index == 999)
            {
                botonesEnJuego.Clear(); // Limpiar la lista de botones
            }
            else if (index >= 0 && index < botonesEnJuego.Count)
            {
                botonesEnJuego.RemoveAt(index); // Elimina el botón de la lista
            }
        }
        public void BorrarControles(int index)
        {
            if (index == 999)
            {
                foreach (var control in controlesGenericos)
                {
                    this.Controls.Remove(control); // Eliminar el control del formulario
                    control.Dispose(); // Liberar recursos asociados
                }
                controlesGenericos.Clear(); // Limpiar la lista de controles
            }
            else if (index >= 0 && index < controlesGenericos.Count)
            {
                var control = controlesGenericos[index];
                this.Controls.Remove(control);
                control.Dispose();
                controlesGenericos.RemoveAt(index); // Elimina el control de la lista
            }
        }
        public void BorrarLasTres(int index)
        {
            BorrarCartas(index);
            BorrarBotonesCarta(index);
            BorrarControles(index);
        }

        List<CartaBoton> cartasJugador = new List<CartaBoton>();


        // Para procesar las cartas del player
        private void ProcesarCartasJugador(string response, List<CartaBoton> cartasJugador)
        {
            if ((!string.IsNullOrEmpty(response)))
            {
                string[] ListaCartas = response.Split('.');

                for (int i = 0; i < ListaCartas.Length; i++)
                {
                    string tipoOValor = ListaCartas[i].Split(',')[0];
                    string color = ListaCartas[i].Split(',')[1];

                    System.Drawing.Color colorCarta;
                    WindowsFormsApplication1.ColorCarta c;

                    switch (color.ToLower())
                    {
                        case "rojo":
                            colorCarta = System.Drawing.Color.Red;
                            c = ColorCarta.Rojo;
                            break;
                        case "verde":
                            colorCarta = System.Drawing.Color.Green;
                            c = ColorCarta.Verde;
                            break;
                        case "azul":
                            colorCarta = System.Drawing.Color.Blue;
                            c = ColorCarta.Azul;
                            break;
                        case "amarillo":
                            colorCarta = System.Drawing.Color.Yellow;
                            c = ColorCarta.Amarillo;
                            break;
                        default:
                            colorCarta = System.Drawing.Color.Gray;
                            c = ColorCarta.Gris;
                            break;
                    }

                    Carta nuevaCarta;
                    if (tipoOValor == "salto" || tipoOValor == "cambio" || tipoOValor == "roba2")
                    {
                        nuevaCarta = new Carta(c, (TipoCarta)Enum.Parse(typeof(TipoCarta), tipoOValor), "Usuario");
                    }
                    else
                    {
                        int valor = int.Parse(tipoOValor);
                        nuevaCarta = new Carta(c, valor, "Usuario");
                    }

                    // Crear el botón de la carta
                    var cartaBoton = new CartaBoton(nuevaCarta);
                    cartaBoton.Boton.BackColor = colorCarta;
                    cartaBoton.Boton.Tag = nuevaCarta;
                    cartaBoton.Boton.Click += CartaBoton_Click;

                    // Agregar carta y botón a sus respectivas listas
                    cartasEnJuego.Add(nuevaCarta);
                    botonesEnJuego.Add(cartaBoton);
                    controlesGenericos.Add(cartaBoton.Boton);
                    cartasJugador.Add(cartaBoton);
                }

                // Verificar y eliminar duplicados
                for (int i = 0; i < cartasJugador.Count;)
                {
                    bool seElimino = false;

                    for (int j = i + 1; j < cartasJugador.Count; j++)
                    {
                        // Comparar las cartas en la posición i y j
                        if (cartasJugador[i].Carta.Color == cartasJugador[j].Carta.Color &&
                            cartasJugador[i].Carta.Valor == cartasJugador[j].Carta.Valor)
                        {
                            // Llamar a la función de borrar con el índice j
                            BorrarCartas(j);
                            BorrarControles(j);
                            BorrarBotonesCarta(j);
                            cartasJugador.RemoveAt(j);
                            seElimino = true;
                            break; // Salir del segundo bucle para revalidar
                        }
                    }

                    if (!seElimino)
                    {
                        i++; // Avanzar al siguiente elemento si no se eliminaron duplicados
                    }
                }

                // Eliminar todas las cartas grises
                for (int i = 0; i < cartasJugador.Count;)
                {
                    if (cartasJugador[i].Carta.Color == ColorCarta.Gris)
                    {
                        // Buscar y eliminar de las listas relacionadas
                        int indexEnJuego = cartasEnJuego.IndexOf(cartasJugador[i].Carta);
                        if (indexEnJuego != -1)
                        {
                            cartasEnJuego.RemoveAt(indexEnJuego);
                            botonesEnJuego.RemoveAt(indexEnJuego);
                            controlesGenericos.RemoveAt(indexEnJuego);
                        }

                        // Eliminar de cartasJugador
                        BorrarCartas(i);
                        BorrarControles(i);
                        BorrarBotonesCarta(i);
                        cartasJugador.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                // Posicionar las cartas
                int xCentro = 487;
                int xSeparacion = 30;
                int cantidadCartas = cartasJugador.Count;

                if (cartasEnJuego.Count != 0)
                {
                    PosicionarCartasJugador(botonesEnJuego, xCentro, xSeparacion, cartasEnJuego.Count);
                }
                else
                {
                    MessageBox.Show("you win!");
                }
                

            }
            else
            {
                MessageBox.Show("No se recibió información del servidor.");
            }
        }


        // Para colocar bien las cartas del player
        private void PosicionarCartasJugador(List<CartaBoton> cartasJugador, int xCentro, int xSeparacion, int cantidadCartas)
        {
            // Validar que la lista no sea nula y tenga elementos
            if (cartasJugador == null || cartasJugador.Count == 0)
            {
                Console.WriteLine("Error: La lista cartasJugador está vacía o es nula.");
                return; // Salir del método si la lista no es válida
            }

            // Asegurar que cantidadCartas no exceda el tamaño de la lista
            if (cantidadCartas > cartasJugador.Count)
            {
                Console.WriteLine("Advertencia: cantidadCartas es mayor que el número de elementos en cartasJugador.");
                cantidadCartas = cartasJugador.Count; // Ajustar para evitar acceso fuera de rango
            }

            try
            {
                // Calcular el ancho total necesario (ancho de todas las cartas + espacio entre ellas)
                int anchoTotal = cantidadCartas * cartasJugador[0].Boton.Width + (cantidadCartas - 1) * xSeparacion;

                // Calcular la posición inicial para que el centro total esté alineado con xCentro
                int startX = xCentro - anchoTotal / 2;

                // Posicionar cada carta en base al cálculo anterior
                for (int i = 0; i < cantidadCartas; i++)
                {
                    // Calcular la posición X para cada carta
                    int posicionX = startX + i * (cartasJugador[i].Boton.Width + xSeparacion);

                    // Asignar la nueva posición al botón de la carta
                    cartasJugador[i].Boton.Location = new System.Drawing.Point(posicionX, 390);

                    // Agregar el botón al formulario
                    this.Controls.Add(cartasJugador[i].Boton);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error al posicionar las cartas: {ex.Message}");
            }
        }


        private void PedirCartasJugador(int numCards)
        {
            if (server == null || !server.Connected)
            {
                // Uso de SafeInvoke para asegurarse de que se ejecute en el hilo de la UI
                SafeInvoke(this, () =>
                {
                    MessageBox.Show("You are not connected to the server.");
                });
                return;
            }
            // Send a request to the server to get the number of cards
            string mensaje = $"22/{nForm}/{numCards}/";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);

            try
            {
                server.Send(msg);
            }
            catch (SocketException ex)
            {
                // Usamos SafeInvoke también para mostrar mensajes de error de forma segura en la UI
                SafeInvoke(this, () =>
                {
                    MessageBox.Show("Error sending data to the server: " + ex.Message);
                });
                return;
            }
        }

        private void RepartirCartasATodos(int numCards)
        {
            if (server == null || !server.Connected)
            {
                // Uso de SafeInvoke para asegurarse de que se ejecute en el hilo de la UI
                SafeInvoke(this, () =>
                {
                    MessageBox.Show("You are not connected to the server.");
                });
                return;
            }
            // Send a request to the server to get the number of cards
            string mensaje = $"34/{nForm}/{numCards}/{partidaName}";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);

            try
            {
                server.Send(msg);
            }
            catch (SocketException ex)
            {
                // Usamos SafeInvoke también para mostrar mensajes de error de forma segura en la UI
                SafeInvoke(this, () =>
                {
                    MessageBox.Show("Error sending data to the server: " + ex.Message);
                });
                return;
            }
        }



        // CARTA CENTRO
        private void PedirCartaCentro(int numCards)
        {
            if (server == null || !server.Connected)
            {
                // Uso de SafeInvoke para asegurarse de que se ejecute en el hilo de la UI
                SafeInvoke(this, () =>
                {
                    MessageBox.Show("You are not connected to the server.");
                });
                return;
            }
            // Send a request to the server to get the number of cards
            string mensaje = $"37/{nForm}/{numCards}/{selectedUser}/{partidaName}";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);

            try
            {
                server.Send(msg);
            }
            catch (SocketException ex)
            {
                // Usamos SafeInvoke también para mostrar mensajes de error de forma segura en la UI
                SafeInvoke(this, () =>
                {
                    MessageBox.Show("Error sending data to the server: " + ex.Message);
                });
                return;
            }
        }


        bool primerapartida = true;
        private void ProcesarCartaCentro(string response)
        {
            if (primerapartida == false)
            {
                ControlarGanador();
            }

            if (primerapartida == true)
            {
                primerapartida = false;
            }


            string[] phrase = response.Split('/');
            if (phrase.Length > 1)
            {
                turn = response.Split('/')[1];
                label2.Text = "Turn: " + turn;
            }
            
            if (!string.IsNullOrEmpty(response))
            {
                // Dividir el string response en un arreglo de cartas usando el punto (.) como separador
                string[] ListaCartas = response.Split('.');

                // Tomamos la carta recibida (asumimos que siempre habrá solo una carta)
                string tipoOValor = ListaCartas[0].Split(',')[0]; // El número o tipo de carta
                string color = ListaCartas[0].Split(',')[1];      // El color de la carta

                // Asignar el color basado en el string directamente
                System.Drawing.Color colorCarta;
                WindowsFormsApplication1.ColorCarta c;

                switch (color.ToLower())
                {
                    case "rojo":
                        colorCarta = System.Drawing.Color.Red;
                        c = ColorCarta.Rojo;
                        break;
                    case "verde":
                        colorCarta = System.Drawing.Color.Green;
                        c = ColorCarta.Verde;
                        break;
                    case "azul":
                        colorCarta = System.Drawing.Color.Blue;
                        c = ColorCarta.Azul;
                        break;
                    case "amarillo":
                        colorCarta = System.Drawing.Color.Yellow;
                        c = ColorCarta.Amarillo;
                        break;
                    default:
                        colorCarta = System.Drawing.Color.Gray;  // Si no coincide, color gris por defecto
                        c = ColorCarta.Gris;
                        break;
                }

                // Verificamos si la carta es especial o numérica
                Carta nuevaCarta;
                if (tipoOValor == "salto" || tipoOValor == "cambio" || tipoOValor == "roba2") // Tipo especial
                {
                    nuevaCarta = new Carta(c, (TipoCarta)Enum.Parse(typeof(TipoCarta), tipoOValor), "Usuario");
                }
                else // Carta numérica
                {
                    int valor = int.Parse(tipoOValor);
                    nuevaCarta = new Carta(c, valor, "Usuario");
                }

                // Si no existe carta del medio, crearla y agregarla
                if (cartaCentro == null)
                {
                    cartaCentro = nuevaCarta;
                    cartaCentro.CambiarPropietario("Centro");

                    // Crear el botón de la carta
                    cartaBotonCentro = new CartaBoton(nuevaCarta);
                    cartaBotonCentro.Boton.BackColor = colorCarta;

                    // Agregar el control al formulario
                    this.Controls.Add(cartaBotonCentro.Boton);

                    // Posicionar la carta en el centro
                    int xCentro = 446; // Valor del centro
                    int yCentro = 197; // Ajusta esta posición en Y como desees
                    cartaBotonCentro.Boton.Location = new System.Drawing.Point(xCentro, yCentro);
                }
                else
                {
                    // Si ya existe una carta del medio, la actualizamos con el nuevo valor
                    cartaCentro = nuevaCarta;
                    cartaCentro.CambiarPropietario("Centro");

                    // Actualizar el botón asociado
                    //System.Drawing.Color mi_color = CartaBoton.ConvertirAColor(cartaMedio.Color);
                    //cartaBotonMedio.Boton.BackColor = mi_color;
                    cartaBotonCentro.Boton.BackColor = colorCarta;
                    cartaBotonCentro.Boton.Text = tipoOValor;  // Si deseas cambiar el texto del botón (por ejemplo, para mostrar el número o tipo)
                }
            }
            else
            {
                MessageBox.Show("No se recibió información del servidor.");
            }
        }


        // Jugada

        private void RealizarJugada(Carta cartaJugador)
        {
            if (PuedeTirarCarta(cartaJugador, cartaCentro))
            {
                // Enviar mensaje al servidor con la carta jugada y el nombre de la partida
                string mensaje = $"29/{nForm}/{cartaJugador.Valor},{cartaJugador.Color}/{partidaName}/{selectedUser}";

                // Eliminar la carta del jugador
                BorrarLasTres(cartasEnJuego.IndexOf(cartaJugador));

                // Enviar el mensaje al servidor
                EnviarMensajeCodigoCarta(mensaje);
                PosicionarCartasJugador(botonesEnJuego, 487, 30, cartasEnJuego.Count);
            }
            else
            {
                MessageBox.Show("No puedes jugar esa carta en este momento o no es tu turno.");
            }
        }


        bool hayganador = false;
        public void DameWinner(string partida, string player)
        {
            MessageBox.Show("El ganador de la partida " + partida + " es " + player);
            hayganador = true;
        }
        private bool ControlarGanador()
        {
            if ((cartasEnJuego.Count == 0) || (cartasJugador.Count == 0))
            {
                if (server == null || !server.Connected)
                {
                    // Uso de SafeInvoke para asegurarse de que se ejecute en el hilo de la UI
                    SafeInvoke(this, () =>
                    {
                        MessageBox.Show("You are not connected to the server.");
                    });
                    return false;
                }
                // Send a request to the server to get the number of cards
                string mensaje = $"44/{nForm}/{partidaName}/{selectedUser}/";
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);

                try
                {
                    server.Send(msg);
                }
                catch (SocketException ex)
                {
                    // Usamos SafeInvoke también para mostrar mensajes de error de forma segura en la UI
                    SafeInvoke(this, () =>
                    {
                        MessageBox.Show("Error sending data to the server: " + ex.Message);
                    });
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /////////////////////////////////////////////////////////// BOTONES /////////////////////////////////////////////////////////////////////////

        private void CartaBoton_Click(object sender, EventArgs e)
        {
            // Obtener el botón que fue clickeado
            var boton = (Button)sender;

            // Obtener la carta asociada al botón desde la propiedad Tag
            var cartaJugador = (Carta)boton.Tag;

            // Llamar al método RealizarJugada con la carta asociada al botón
            RealizarJugada(cartaJugador);
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
            //esperandoRespuesta = false;
        }



        private void fourcards_Click(object sender, EventArgs e)
        {
            BorrarLasTres(999);
            PedirCartasJugador(5);
        }

        private void middlecard_Click(object sender, EventArgs e)
        {
            if (alreadyStarted == true)
            {
                middlecard.Hide();
                return;
            }
            if (isCreator == selectedUser)
            {
                PedirCartaCentro(1);
                //RepartirCartasATodos(5);
                middlecard.Hide();
                alreadyStarted = true;
                return;
            }
            else
            {
                MessageBox.Show("Only the creator can start the game.");
            }

        }
        private void robarboton_Click(object sender, EventArgs e)
        {
            if (turn == selectedUser)
            {
                if (cartasEnJuego.Count < 7)
                {
                    PedirCartasJugador(1);
                    //MoverTurnoPorRobar();
                }
                else
                {
                    MessageBox.Show("Already 7 cards in your deck!");
                    //MoverTurnoPorRobar();
                }
            }
            else
            {
                MessageBox.Show("It is not your turn!");
            }

        }

        

        private void MoverTurnoPorRobar()
        {
            
            string message = $"31/{nForm}/{partidaName}/{selectedUser}";
            if (!isConnected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            byte[] msg = Encoding.ASCII.GetBytes(message);
            server.Send(msg); 
            
        }

        // Mover el form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void lblsend_TextChanged(object sender, EventArgs e)
        {

        }

        private void pasaturno_Click(object sender, EventArgs e)
        {
            if (turn == selectedUser)
            {
                MoverTurnoPorRobar();
            }
            else
            {
                MessageBox.Show("It is not your turn.");
            }
            
            

        }
    }
}