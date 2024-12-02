using alterar;
using atualizar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visualizar;

namespace BlossimMakeup
{
    public partial class menuFunc : Form
    {
        public menuFunc()
        {
            InitializeComponent();
            customizeDesing();
        }

        private void customizeDesing()
        {
            panelMediaSubMenu.Visible = false;
            panelToolsSubMenu.Visible = false;
            panel2.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelMediaSubMenu.Visible == true)
                panelMediaSubMenu.Visible = false;
            if (panelToolsSubMenu.Visible == true)
                panelToolsSubMenu.Visible = false;
            if (panel2.Visible == true)
                panel2.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        //Botao Media (Produto)
        private void btnMedia_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }
        //Subs Media (produto)
        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new cadastroProduto());


            hideSubMenu();
        }
        //Botao Tools (cliente)
        private void bntTools_Click(object sender, EventArgs e)
        {
            showSubMenu(panelToolsSubMenu);

        }
        //Subs Tools (cliente)
        private void button15_Click(object sender, EventArgs e)
        {
            openChildForm(new cadastroClien());
            hideSubMenu();
        }
        //Botao Venda
        private void btnVenda_Click(object sender, EventArgs e)
        {
            showSubMenu(panel2);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            openChildForm(new cadastroVenda());
            hideSubMenu();
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        //AlterarProd
        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new alterarProd());
            hideSubMenu();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            openChildForm(new alterarClien());
            hideSubMenu();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            openChildForm(new alterarVenda());
            hideSubMenu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new visproduto());


            hideSubMenu();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            openChildForm(new viscliente());


            hideSubMenu();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            openChildForm(new visvenda());


            hideSubMenu();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            openChildForm(new vismarca());


            hideSubMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openChildForm(new vistipo());


            hideSubMenu();
        }

        private void btnEqualizer_Click(object sender, EventArgs e)
        {
            openChildForm(new visvendaproduto());


            hideSubMenu();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            openChildForm(new cadastrarMarca());


            hideSubMenu();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new cadastrarTipo());


            hideSubMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new excluirproduto());


            hideSubMenu();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            openChildForm(new excluircliente());


            hideSubMenu();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            openChildForm(new excluirvenda());


            hideSubMenu();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            openChildForm(new excluimarca());


            hideSubMenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new excluirtipo());


            hideSubMenu();
        }
    }
}
