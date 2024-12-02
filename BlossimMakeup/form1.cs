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
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Cria uma instância do formulário AdmLogin
            cadastro cadastro = new cadastro();

            // Exibe o formulário AdmLogin
           cadastro.Show();

            // (Opcional) Esconde o formulário atual, se desejar
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

           
        }
    }
}
