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

        /// <summary>
        /// consulta segun el entero Producto
        /// </summary>
        /// <returns> el instrumento vendido en formato string </returns>
        public string ProductoToString()
        {
            switch (Producto)
            {
                case 0: return "Instrumento de viento";
                case 1: return "Instrumento de cuerda";
                default: return "Instrumento de percusion";
            }
        }

        /// <summary>
        /// consulta segun el entero FormaPago
        /// </summary>
        /// <returns> la forma de pago en formato string </returns>
        public string FormaPagoToString()
        {
            switch (FormaPago)
            {
                case 1: return "Efectivo";
                case 2: return "Tarjeta de Credito";
                default: return "Tarjeta de Debito";
            }
        }

        /// <summary>
        /// toma casi todos los datos de las propiedades del objeto
        /// </summary>
        /// <returns> un mensaje informando sobre la venta </returns>
        public string toString()
        {
            string mensaje = $"Venta registrada con exito!\n" +
                $"\t--------------------\n" +
                $"Nombre del cliente: {Apellido}, {Nombre}\n" +
                $"Forma de pago: {FormaPagoToString()}\n" +
                $"Rubro: {ProductoToString()}\n" +
                $"Precio: {Precio}\n";

            if (AplicaDescuento) mensaje += $"Se le aplico un descuento del {Descuento}%\n\t--------------------";
            else mensaje += $"\t--------------------";

            return mensaje;
        }
    }
}