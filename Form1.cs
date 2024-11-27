using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gato
{
    public partial class Form1 : Form
    {
        //se declaran las variables que se ocuparan
        int turno = 1;
        int[,] Gato;
        int PuntosPlayer1 = 0;
        int PuntosPlayer2 = 0;
        bool YaHayGanador;
        public Form1()
        {
            InitializeComponent();
            IniciarJuego();
        }
        public void IniciarJuego()
        {
            //Iniciar valores en el juego
            turno = 1;
            Gato = new int[3, 3];
            YaHayGanador = false;

            picGanador.Image = Properties.Resources.img_negro;
            FichasGato.Controls.Clear();

            // Este arreglo es para mostrar las fichas en el tablero y meter los valores en una matriz
            for (var i = 0; i<3; i++)
            {
                for (var j = 0; j<3; j++)
                {
                    var FichaJuego = new PictureBox();
                    FichaJuego.Image = Properties.Resources.img_negro;
                    FichaJuego.Name = string.Format("{0}", i + "_" + j);
                    FichaJuego.Dock = DockStyle.Fill;
                    FichaJuego.Cursor = Cursors.Hand;
                    FichaJuego.SizeMode = PictureBoxSizeMode.StretchImage;
                    FichaJuego.Click += Jugar;

                    FichasGato.Controls.Add(FichaJuego, j, i);
                    Gato[i, j] = 0;
                }
            }
        }

        private void Jugar(object sender, EventArgs e)
        {
            var FichaSeleccionadaUsuario = (PictureBox)sender;
            FichaSeleccionadaUsuario.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("f_" + turno);
            string[] Posicion = FichaSeleccionadaUsuario.Name.Split("_".ToCharArray());
            int Fila = Convert.ToInt32(Posicion[0]);
            int Columna = Convert.ToInt32(Posicion[1]);
            Gato[Fila, Columna] = turno;
            VerificarJuego(Fila, Columna);
            turno = (turno == 1) ? 2 : 1;
        }

        public void VerificarJuego(int Fila, int Columna)
        {
            //contador y verificador de fichas del gato, para saber en que momento alguien gana
            int GanoFilas = 0;
            int GanoColumnas = 0;
            int DiagonalPrincipal = 0;
            int DiagonalInversa = 0;
            int TamanioGato = 3;

            for (var i = 0; i<TamanioGato; i++)
            {
                for (var j = 0; j < TamanioGato ; j++)
                {
                    if (i == Fila)
                    {
                        if (Gato[i, j] == turno)
                        {
                            GanoFilas++;
                        }
                    }
                    if (j == Columna)
                    {
                        if (Gato[i, j] == turno)
                        {
                            GanoColumnas++;
                        }
                    }
                    if (i == j)
                    {
                        if (Gato[i, j] == turno)
                        {
                            DiagonalPrincipal++;
                        }
                    }
                    if ((i + j) == (TamanioGato - 1))
                    {
                        if (Gato[i, j] == turno)
                        {
                            DiagonalInversa++;
                        }
                    }
                }
            }
            if ((GanoFilas == TamanioGato) || (GanoColumnas == TamanioGato) || (DiagonalInversa == TamanioGato) || (DiagonalPrincipal == TamanioGato))
            {
                YaHayGanador = true;
            }
            if (YaHayGanador)
            {
                MessageBox.Show("ya hay un ganador");
                picGanador.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("f_" + turno);
                if (turno == 1)
                {
                    PuntosPlayer1++;
                    lblPlayer1.Text = PuntosPlayer1.ToString();
                }
                else
                {
                    PuntosPlayer2++;
                    lblPlayer2.Text = PuntosPlayer2.ToString();
                }
            }
        }

        private void picReiniciar_Click(object sender, EventArgs e)
        {
            IniciarJuego();
        }
    }
}
