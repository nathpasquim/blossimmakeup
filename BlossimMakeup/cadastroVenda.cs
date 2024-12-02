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


namespace BlossimMakeup
{
    public partial class cadastroVenda : Form
    {
        private string connectionString = "Server=127.0.0.1;Database=blossommakeup;Uid=root;Pwd=;";
        private List<int> produtosSelecionados = new List<int>();
        private decimal valorTotal = 0;


        public cadastroVenda()
        {
            InitializeComponent();
            CarregarFuncionarios();
            CarregarProdutos();
            CarregarClientes();
            

        }

        // Carrega nomes dos funcionários no comboFunc
        private void CarregarFuncionarios()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, nome FROM funcionario";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboFunc.DataSource = dt;
                comboFunc.DisplayMember = "nome";
                comboFunc.ValueMember = "id";
            }
        }

        // Carrega nomes dos produtos no comboProd
        private void CarregarProdutos()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, nome FROM produto";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboProd.DataSource = dt;
                comboProd.DisplayMember = "nome";
                comboProd.ValueMember = "id";
            }
        }

        // Novo: Carrega os clientes no comboClien
        private void CarregarClientes()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, nome FROM cliente";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboClien.DataSource = dt;
                comboClien.DisplayMember = "nome";
                comboClien.ValueMember = "id";
            }
        }

        private void txtFiltroClien_TextChanged(object sender, EventArgs e)
        {
            // Verifica se o campo de filtro está vazio
            if (string.IsNullOrWhiteSpace(txtFiltroClien.Text))
            {
                // Se estiver vazio, carrega todos os clientes
                CarregarClientes();
            }
            else
            {
                // Caso contrário, realiza o filtro
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "SELECT id, nome FROM cliente WHERE nome LIKE @nome";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    // Adiciona o parâmetro de filtro
                    cmd.Parameters.AddWithValue("@nome", "%" + txtFiltroClien.Text + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Atualiza o ComboBox com os dados filtrados
                    comboClien.DataSource = dt;
                    comboClien.DisplayMember = "nome";
                    comboClien.ValueMember = "id";
                }
            }
        }

    



    private void btnCad_Click(object sender, EventArgs e)
        {
            if (comboFunc.SelectedValue != null && comboClien.SelectedValue != null && produtosSelecionados.Count > 0)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Inserir a venda
                        string insertVenda = "INSERT INTO venda (FK_funcionario_id, FK_cliente_id, valor) VALUES (@funcionario, @cliente, @valor)";
                        MySqlCommand cmdVenda = new MySqlCommand(insertVenda, conn, transaction);
                        cmdVenda.Parameters.AddWithValue("@funcionario", comboFunc.SelectedValue);
                        cmdVenda.Parameters.AddWithValue("@cliente", comboClien.SelectedValue); // Salva o ID do cliente
                        cmdVenda.Parameters.AddWithValue("@valor", valorTotal);
                        cmdVenda.ExecuteNonQuery();

                        // Obter o ID da venda criada
                        int vendaId = (int)cmdVenda.LastInsertedId;

                        // Inserir os produtos na tabela vendaproduto
                        foreach (int produtoId in produtosSelecionados)
                        {
                            string insertVendaProduto = "INSERT INTO vendaproduto (FK_produto_id, FK_venda_id) VALUES (@produto, @venda)";
                            MySqlCommand cmdVendaProduto = new MySqlCommand(insertVendaProduto, conn, transaction);
                            cmdVendaProduto.Parameters.AddWithValue("@produto", produtoId);
                            cmdVendaProduto.Parameters.AddWithValue("@venda", vendaId);
                            cmdVendaProduto.ExecuteNonQuery();
                        }

                        // Confirma a transação
                        transaction.Commit();
                        MessageBox.Show("Venda cadastrada com sucesso!");

                        // Limpa os campos
                        LimparFormulario();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Erro ao cadastrar venda: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos e adicione ao menos um produto.");
            }
        }

        private void txtFiltroProduto_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, nome FROM produto WHERE nome LIKE @nome";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nome", "%" + txtFiltroProduto.Text + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboProd.DataSource = dt;
                comboProd.DisplayMember = "nome";
                comboProd.ValueMember = "id";
            }
        }
     

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            if (comboProd.SelectedValue != null)
            {
                int codigoProduto = Convert.ToInt32(comboProd.SelectedValue);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "SELECT preco FROM produto WHERE id = @codigo";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@codigo", codigoProduto);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        decimal preco = Convert.ToDecimal(result);
                        produtosSelecionados.Add(codigoProduto);
                        valorTotal += preco;
                        txtValor.Text = valorTotal.ToString("F2");

                        // Adiciona o produto à lista (visualização)
                        listBoxProdutos.Items.Add(comboProd.Text + " - R$" + preco.ToString("F2"));
                    }
                }
            }
        }
        private void LimparFormulario()
        {
            txtFiltroClien.Clear();
            txtValor.Clear();
            produtosSelecionados.Clear();
            listBoxProdutos.Items.Clear();
            valorTotal = 0;

            CarregarClientes(); // Novo: Recarregar clientes
        }

        private void comboClien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
