using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dream_game
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GerJogo form = new GerJogo();
            form.ShowDialog();
        }

        private void buttonGerUsuario_Click(object sender, EventArgs e)
        {
            GerUsuario form = new GerUsuario();
            form.ShowDialog();
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCompras_Click(object sender, EventArgs e)
        {
            GerCompra form = new GerCompra();
            form.ShowDialog();
        }
    }
}
