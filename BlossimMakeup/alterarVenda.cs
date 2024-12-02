using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alterar
{
    public partial class alterarVenda : Form
    {
        private string connectionString = "Server=127.0.0.1;Database=blossommakeup;Uid=root;Pwd=;";
        private int vendaId;
        private List<int> produtosSelecionados = new List<int>();
        private decimal valorTotal = 0;


        public alterarVenda()
        {
            InitializeComponent();
             CarregarFuncionarios();
            CarregarProdutos();

        }

        private void CarregarFuncionarios()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT id, nome FROM funcionario";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboFunc.DataSource = dt;
                    comboFunc.DisplayMember = "nome";
                    comboFunc.ValueMember = "id";
                    comboFunc.Enabled = false; // Funcionário não pode ser alterado
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar funcionários: " + ex.Message);
                }
            }
        }
        private void CarregarProdutos()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT id, nome FROM produto";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboProd.DataSource = dt;
                    comboProd.DisplayMember = "nome";
                    comboProd.ValueMember = "id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
                }
            }
        }








        private void btnCarregarVenda_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                vendaId = id;
                CarregarDadosVenda();
            }
            else
            {
                MessageBox.Show("Por favor, insira um ID válido.");
            }
        }
        private void CarregarDadosVenda()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT v.FK_funcionario_id, c.nome AS cliente, v.valor FROM venda v " +
                                   "JOIN cliente c ON v.FK_cliente_id = c.id WHERE v.id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", vendaId);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        comboFunc.SelectedValue = reader["FK_funcionario_id"];
                        txtClien.Text = reader["cliente"].ToString();
                        valorTotal = Convert.ToDecimal(reader["valor"]);
                        txtValor.Text = valorTotal.ToString("F2");
                        reader.Close();

                        CarregarProdutosVenda();
                    }
                    else
                    {
                        MessageBox.Show("Venda não encontrada.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados da venda: " + ex.Message);
                }
            }
        }
        private void CarregarProdutosVenda()
        {
            listBoxProdutos.Items.Clear();
            produtosSelecionados.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT p.id, p.nome, p.preco FROM produto p " +
                                   "JOIN vendaproduto vp ON vp.FK_produto_id = p.id " +
                                   "WHERE vp.FK_venda_id = @vendaId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@vendaId", vendaId);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int produtoId = Convert.ToInt32(reader["id"]);
                        string nomeProduto = reader["nome"].ToString();
                        decimal precoProduto = Convert.ToDecimal(reader["preco"]);

                        produtosSelecionados.Add(produtoId);
                        listBoxProdutos.Items.Add($"{nomeProduto} - R${precoProduto:F2}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar produtos da venda: " + ex.Message);
                }
            }
        }

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            if (comboProd.SelectedValue != null)
            {
                int produtoId = Convert.ToInt32(comboProd.SelectedValue);
                if (!produtosSelecionados.Contains(produtoId))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            string query = "SELECT preco FROM produto WHERE id = @id";
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", produtoId);

                            conn.Open();
                            decimal preco = Convert.ToDecimal(cmd.ExecuteScalar());

                            produtosSelecionados.Add(produtoId);
                            listBoxProdutos.Items.Add($"{comboProd.Text} - R${preco:F2}");
                            valorTotal += preco;
                            txtValor.Text = valorTotal.ToString("F2");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao adicionar produto: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Produto já adicionado.");
                }
            }

        }



        private void btnAlterarVenda_Click(object sender, EventArgs e)
        {
            if (produtosSelecionados.Count > 0)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        string updateVenda = "UPDATE venda SET valor = @valor WHERE id = @id";
                        MySqlCommand cmdVenda = new MySqlCommand(updateVenda, conn, transaction);
                        cmdVenda.Parameters.AddWithValue("@valor", valorTotal);
                        cmdVenda.Parameters.AddWithValue("@id", vendaId);
                        cmdVenda.ExecuteNonQuery();

                        string deleteProdutos = "DELETE FROM vendaproduto WHERE FK_venda_id = @vendaId";
                        MySqlCommand cmdDelete = new MySqlCommand(deleteProdutos, conn, transaction);
                        cmdDelete.Parameters.AddWithValue("@vendaId", vendaId);
                        cmdDelete.ExecuteNonQuery();

                        foreach (int produtoId in produtosSelecionados)
                        {
                            string insertProduto = "INSERT INTO vendaproduto (FK_venda_id, FK_produto_id) VALUES (@vendaId, @produtoId)";
                            MySqlCommand cmdInsert = new MySqlCommand(insertProduto, conn, transaction);
                            cmdInsert.Parameters.AddWithValue("@vendaId", vendaId);
                            cmdInsert.Parameters.AddWithValue("@produtoId", produtoId);
                            cmdInsert.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Venda alterada com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Erro ao salvar alterações: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Adicione ao menos um produto.");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            // Verifica se há algum item selecionado no listBox
            if (listBoxProdutos.SelectedIndex >= 0)
            {
                try
                {
                    // Obtém o índice do produto selecionado no listBox
                    int produtoIndex = listBoxProdutos.SelectedIndex;

                    // Obtém o ID do produto a ser removido, com base no índice
                    int produtoId = produtosSelecionados[produtoIndex];

                    // Conecta ao banco para obter o preço do produto
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        string query = "SELECT preco FROM produto WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", produtoId);

                        conn.Open();
                        decimal preco = Convert.ToDecimal(cmd.ExecuteScalar());

                        // Remove o produto da lista visual e lógica
                        produtosSelecionados.RemoveAt(produtoIndex);
                        listBoxProdutos.Items.RemoveAt(produtoIndex);

                        // Atualiza o valor total
                        valorTotal -= preco;
                        txtValor.Text = valorTotal.ToString("F2");

                        MessageBox.Show("Produto removido com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover o produto: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecione um produto para remover.");
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();

        }
        private void LimparCampos()
        {
            txtId.Clear();
            txtClien.Clear();
            txtValor.Clear();
            produtosSelecionados.Clear();
            listBoxProdutos.Items.Clear();
            valorTotal = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
