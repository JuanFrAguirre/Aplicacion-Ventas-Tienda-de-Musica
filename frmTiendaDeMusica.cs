using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stream_23_05
{
    public partial class frmTiendaDeMusica : Form
    {
        private double totalVentas, totalDescuentos;
        private int contEfectivo, contTC, contTD, contVientos, contCuerdas, contPerc, contInstrumentos;

        private void chkDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDescuento.Checked)
            {
                txtDescuento.Enabled = true;
            }
            else
            {
                txtDescuento.Enabled = false;
            }
        }

        private bool esLaPrimeraVenta = true;
        private Negocio InstrumentoMasCaro;

        public frmTiendaDeMusica()
        {
            InitializeComponent();
            totalVentas = totalDescuentos = contEfectivo =
                contTC = contTD = contVientos = contCuerdas = contPerc = contInstrumentos = 0;
        }

        private bool validarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                mostrarMensajeError("nombre");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                mostrarMensajeError("apellido");
                txtApellido.Focus();
                return false;
            }

            if (cboProducto.SelectedIndex == -1)
            {
                mostrarMensajeError("producto");
                cboProducto.Focus();
                return false;
            }

            if (rdoMasculino.Checked == false && rdoMasculino.Checked == false
                && rdoMasculino.Checked == false)
            {
                mostrarMensajeError("sexo");
                gpbSexo.Focus();
                return false;
            }

            if (cboFormaPago.SelectedIndex == -1)
            {
                mostrarMensajeError("forma de pago");
                cboFormaPago.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                mostrarMensajeError("precio");
                txtPrecio.Focus();
                return false;
            }
            return true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                // codigo para el boton de registrar

                // instancia de clase y asignacion a propiedades segun valores en el form

                Negocio musimundo = new Negocio();

                musimundo.Nombre = txtNombre.Text;
                musimundo.Apellido = txtApellido.Text;
                musimundo.Producto = cboProducto.SelectedIndex;
                if (rdoMasculino.Checked) musimundo.Sexo = 1;
                if (rdoFemenino.Checked) musimundo.Sexo = 2;
                if (rdoOtro.Checked) musimundo.Sexo = 3;
                musimundo.FormaPago = cboFormaPago.SelectedIndex + 1;
                musimundo.AplicaDescuento = chkDescuento.Checked;
                if (chkDescuento.Checked) musimundo.Descuento = double.Parse(txtDescuento.Text);
                else musimundo.Descuento = 0;
                musimundo.Precio = double.Parse(txtPrecio.Text);

                //---------------------------------------------------------------------------------------

                // calculos para el formulario

                // opcion de calculo del precio a pagar extendida
                //double descuentoDePago = (100 - musimundo.Descuento);
                //double numeroAPagar = musimundo.Precio - descuentoDePago;
                //double totalAPagar = (numeroAPagar) / 100;

                //opcion de calculo de precio simplificada
                double totalAPagar = (musimundo.Precio * (100 - musimundo.Descuento)) / 100;

                // basicamente el total a pagar seria el precio multiplicado por (100 menos el descuento, para obtener el complemento (por ejemplo si el descuento es de 15, al hacer 100 - 15 me quedaria 85), al multiplicar este complemento por el precio, me queda lo que tengo que pagar pero 100 veces mas grande, entonces lo divido por 100 y me queda el resultado real a pagar
                // suponiendo un precio de 1000 y un descuento del 15, me quedaria:
                //              (1000 * (100 - 15)) / 100 ---> 850, que es el precio correcto
                double descuentoDeEstaVenta = musimundo.Precio - totalAPagar;

                totalVentas += totalAPagar;
                totalDescuentos += descuentoDeEstaVenta;

                // contadores de metodos de pago

                switch (musimundo.FormaPago)
                {
                    case 1: contEfectivo++; break;
                    case 2: contTC++; break;
                    case 3: contTD++; break;
                }

                // contadores de instrumentos

                contInstrumentos++;

                switch (musimundo.Producto)
                {
                    case 0: contVientos++; break;
                    case 1: contCuerdas++; break;
                    case 2: contPerc++; break;
                }

                // implementacion de bandera (esLaPrimeraVenta) para definir el tipo del instrumento mas caro (mas caro en cuanto quedo el precio final que pago el usuario, y no mas caro por tener el precio mas alto)
                if (esLaPrimeraVenta)
                {
                    esLaPrimeraVenta = false;
                    InstrumentoMasCaro = musimundo;
                }
                else
                {
                    // aca abajo utilizo en el if una formula rara, porque no cree una propiedad de Negocio para almacenar el total a pagar, entonces uso directamente la formula del total a pagar
                    if (totalAPagar > ((InstrumentoMasCaro.Precio * (100 - InstrumentoMasCaro.Descuento)) / 100))
                    {
                        InstrumentoMasCaro = musimundo;
                    }
                }

                //---------------------------------------------------------------------------------------

                // asignacion de resultados de calculos en el formulario
                txtTotalAPagar.Text = totalAPagar.ToString();
                txtTotalDelDia.Text = totalVentas.ToString();
                txtDecuentoTotal.Text = totalDescuentos.ToString();

                txtCantEfectivo.Text = contEfectivo.ToString();
                txtCantTC.Text = contTC.ToString();
                txtCantTD.Text = contTD.ToString();

                txtCantVientos.Text = contVientos.ToString();
                txtCantCuerdas.Text = contCuerdas.ToString();
                txtCantPercusion.Text = contPerc.ToString();

                txtPorcVientos.Text = ((contVientos * 100) / contInstrumentos).ToString();
                txtPorcCuerdas.Text = ((contCuerdas * 100) / contInstrumentos).ToString();
                txtPorcPercusion.Text = ((contPerc * 100) / contInstrumentos).ToString();

                txtComodin.Text = InstrumentoMasCaro.ProductoToString();

                txtNombre.Text = "";
                txtApellido.Text = "";
                cboFormaPago.SelectedIndex = -1;
                cboProducto.SelectedIndex = -1;
                rdoMasculino.Checked = false;
                rdoFemenino.Checked = false;
                rdoOtro.Checked = false;
                chkDescuento.Checked = false;
                txtDescuento.Text = "";
                txtPrecio.Text = "";
            }
            else
            {
            }
        }

        private void mostrarMensajeError(string campo)
        {
            MessageBox.Show("          ------------------------------\n\n" +
                    "Aun no se puede registrar la venta\n" +
                    $"Falta ingresar algunos datos en el campo {campo}\n\n" +
                    "          ------------------------------", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // codigo para el boton de resetear los campos del registro diario

            txtTotalAPagar.Text = "-";
            txtTotalDelDia.Text = "-";
            txtDecuentoTotal.Text = "-";
            txtCantEfectivo.Text = "-";
            txtCantTC.Text = "-";
            txtCantTD.Text = "-";
            txtCantVientos.Text = "-";
            txtCantCuerdas.Text = "-";
            txtCantPercusion.Text = "-";
            txtPorcVientos.Text = "-";
            txtPorcCuerdas.Text = "-";
            txtPorcPercusion.Text = "-";
            txtComodin.Text = "-";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // codigo para el boton de salir

            Close();
        }
    }
}