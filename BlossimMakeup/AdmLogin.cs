using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlossimMakeup
{
    public partial class AdmLogin : Form
    {
        public AdmLogin()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria uma instância do formulário Form1
            Forml forml = new Forml();

            // Exibe o formulário Form1
            forml.Show();

            // (Opcional) Esconde o formulário atual
            this.Hide();
        }
    }
}
