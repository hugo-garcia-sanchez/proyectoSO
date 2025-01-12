using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    
    public enum ColorCarta
    {
        Rojo,
        Amarillo,
        Verde,
        Azul,
        Comodin,
        Gris
    }

    public enum TipoCarta
    {
        Numero,
        Salto,
        Cambio,
        RobaDos,
        Comodin,
        Comodin4
    }

    public class Carta
    {
        public ColorCarta Color { get; }
        public TipoCarta Tipo { get; }
        public int? Valor { get; }
        public string Propietario { get; set; } // Esta propiedad almacena el nombre del jugador o "Centro"

        // Constructor para cartas numéricas
        public Carta(ColorCarta color, int valor, string propietario)
        {
            if (valor < 0 || valor > 9)
                throw new ArgumentOutOfRangeException(nameof(valor), "El valor debe estar entre 0 y 9.");

            Color = color;
            Tipo = TipoCarta.Numero;
            Valor = valor;
            Propietario = propietario; // Establecer propietario al crear la carta
        }

        // Constructor para cartas especiales (sin valor numérico)
        public Carta(ColorCarta color, TipoCarta tipo, string propietario)
        {
            if (tipo == TipoCarta.Numero)
                throw new ArgumentException("No puedes usar este constructor para cartas numéricas.");

            Color = color;
            Tipo = tipo;
            Valor = null;
            Propietario = propietario; // Establecer propietario al crear la carta
        }

        // Método para cambiar el propietario de la carta (cuando se roba o se juega)
        public void CambiarPropietario(string nuevoPropietario)
        {
            Propietario = nuevoPropietario;
        }

        public override string ToString()
        {
            if (Valor.HasValue)
                return $"{Color} {Valor.Value} ({Propietario})";
            else
                return $"{Color} {Tipo} ({Propietario})";
        }
    }


}
