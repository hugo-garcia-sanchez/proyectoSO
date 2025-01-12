using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class CartaBoton
    {
        public Carta Carta { get; set; }
        public Button Boton { get; set; }

        public CartaBoton(Carta carta)
        {
            Carta = carta;

            // Crear el botón
            Boton = new Button
            {
                // Tamaño del botón
                Size = new System.Drawing.Size(82, 110),

                // Fuente: Comic Sans MS, 17.25 pt
                Font = new System.Drawing.Font("Comic Sans MS", 17.25f),

                // Color de fondo
                BackColor = ConvertirAColor(Carta.Color),

                // Texto alineado en el centro
                Text = Convert.ToString(Carta.Valor),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,

                // Borde de 1 píxel
                FlatStyle = FlatStyle.Popup,
                FlatAppearance = { BorderSize = 1 },

                // Configuración del botón (puedes agregar otros ajustes aquí si lo necesitas)
                Margin = new Padding(0),
            };
        }

        private Color ConvertirAColor(ColorCarta colorCarta)
        {
            switch (colorCarta)
            {
                case ColorCarta.Rojo:
                    return Color.Red;
                case ColorCarta.Amarillo:
                    return Color.Yellow;
                case ColorCarta.Verde:
                    return Color.Green;
                case ColorCarta.Azul:
                    return Color.Blue;
                default:
                    return Color.Gray;
            }
        }
    }

}
