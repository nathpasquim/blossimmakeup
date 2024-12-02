using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MySqlConnector;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace BlossimMakeup
{
    public partial class cadastroProduto : Form
    {

        // String de conexão (modifique conforme necessário)
        string connectionString = "server=127.0.0.1;database=blossommakeup;uid=root;pwd=;";

        public cadastroProduto()
        {
            InitializeComponent();
            CarregarComboBoxes();
            CarregarFuncionarios();
        }

        private void CarregarFuncionarios()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, nome FROM funcionario";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        comboFunc.Items.Clear();
                        while (dr.Read())
                        {
                            comboFunc.Items.Add(new ComboBoxItem
                            {
                                Text = dr["nome"].ToString(),
                                Value = dr["id"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar funcionários: " + ex.Message);
                }
            }
        }
        private void CarregarComboBoxes()
        {
            // Preenche os comboboxes com dados do banco
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Preenche o comboTipo
                    MySqlCommand cmdTipo = new MySqlCommand("SELECT id, nome FROM tipo", conn);
                    using (MySqlDataReader drTipo = cmdTipo.ExecuteReader())
                    {
                        while (drTipo.Read())
                        {
                            comboTipo.Items.Add(new ComboBoxItem
                            {
                                Text = drTipo["nome"].ToString(),
                                Value = drTipo["id"].ToString()
                            });
                        }
                    }

                    // Preenche o comboMarca
                    MySqlCommand cmdMarca = new MySqlCommand("SELECT id, nome FROM marca", conn);
                    using (MySqlDataReader drMarca = cmdMarca.ExecuteReader())
                    {
                        while (drMarca.Read())
                        {
                            comboMarca.Items.Add(new ComboBoxItem
                            {
                                Text = drMarca["nome"].ToString(),
                                Value = drMarca["id"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar combos: " + ex.Message);
                }
            }
        }
        private void cadastroProduto_Load(object sender, EventArgs e)
        {

        }

        private void buttonCad_Click(object sender, EventArgs e)
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                comboTipo.SelectedItem == null ||
                comboMarca.SelectedItem == null ||
                comboFunc.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtPreco.Text) ||
                string.IsNullOrWhiteSpace(txtQuant.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos!");
                return;
            }

            try
            {
                // Converte valores de entrada
                if (!decimal.TryParse(txtPreco.Text, out decimal preco) || preco <= 0)
                {
                    MessageBox.Show("Insira um valor válido para o preço!");
                    return;
                }

                if (!int.TryParse(txtQuant.Text, out int quantidade) || quantidade <= 0)
                {
                    MessageBox.Show("Insira um valor válido para a quantidade!");
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Inserção no banco
                    string query = @"INSERT INTO produto 
                             (id, preco, quantidade, nome, FK_tipo_id, FK_marca_id, FK_funcionario_id) 
                             VALUES (NULL, @preco, @quantidade, @nome, @tipo, @marca, @funcionario)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@preco", preco);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
                    cmd.Parameters.AddWithValue("@tipo", ((ComboBoxItem)comboTipo.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@marca", ((ComboBoxItem)comboMarca.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@funcionario", ((ComboBoxItem)comboFunc.SelectedItem).Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produto cadastrado com sucesso!");

                    // Limpeza dos campos
                    txtNome.Clear();
                    txtPreco.Clear();
                    txtQuant.Clear();
                    comboTipo.SelectedIndex = -1;
                    comboMarca.SelectedIndex = -1;
                    comboFunc.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar produto: " + ex.Message);
            }

        }
        // Classe para associar texto e valor nos comboboxes
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBuscarFunc_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscarFunc.Text.Trim();

            // Conexão com o banco
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, nome FROM funcionario WHERE nome LIKE @filtro";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        comboFunc.Items.Clear(); // Limpa itens existentes no ComboBox
                        while (dr.Read())
                        {
                            comboFunc.Items.Add(new ComboBoxItem
                            {
                                Text = dr["nome"].ToString(),
                                Value = dr["id"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao filtrar funcionários: " + ex.Message);
                }
            }
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
