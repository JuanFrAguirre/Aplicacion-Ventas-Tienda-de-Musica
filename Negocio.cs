using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_23_05
{
    internal class Negocio
    {
        // -------------------------------------------------
        // atributos

        private string nombre;
        private string apellido;
        private int producto;
        private int sexo;
        private int formaPago;
        private bool aplicaDescuento;
        private double descuento;
        private double precio;

        // -------------------------------------------------
        // constructores

        public Negocio()
        {
            nombre = apellido = "";
            descuento = precio = producto = sexo = formaPago = 0;
            aplicaDescuento = false;
        }

        public Negocio(string nombre, string apellido, int producto, int sexo, int formaPago, bool aplicaDescuento, double descuento, double precio)
        {
            Nombre = nombre;
            Apellido = apellido;
            Producto = producto;
            Sexo = sexo;
            FormaPago = formaPago;
            AplicaDescuento = aplicaDescuento;
            Descuento = descuento;
            Precio = precio;
        }

        // -------------------------------------------------
        // propiedades implementadas

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public int Producto { get => producto; set => producto = value; }
        public int Sexo { get => sexo; set => sexo = value; }
        public int FormaPago { get => formaPago; set => formaPago = value; }
        public bool AplicaDescuento { get => aplicaDescuento; set => aplicaDescuento = value; }
        public double Descuento { get => descuento; set => descuento = value; }
        public double Precio { get => precio; set => precio = value; }

        // -------------------------------------------------
        // metodos

        public string ProductoToString()
        {
            switch (Producto)
            {
                case 0: return "Instrumento de viento";
                case 1: return "Instrumento de cuerda";
                default: return "Instrumento de percusion";
            }
        }

        public string FormaPagoToString()
        {
            switch (FormaPago)
            {
                case 1: return "Efectivo";
                case 2: return "Tarjeta de Credito";
                default: return "Tarjeta de Debito";
            }
        }

        public string toString()
        {
            string mensaje = $"Venta registrada con exito\n" +
                $"--------------------\n" +
                $"Nombre del cliente: {Nombre} {Apellido}\n" +
                $"Forma de pago: {FormaPago}" +
                $"Compra: {ProductoToString()}, {Precio}\n";

            if (AplicaDescuento)
            {
                mensaje += $"Aplico un descuento del {Descuento}%";
            }

            return mensaje;
        }
    }
}