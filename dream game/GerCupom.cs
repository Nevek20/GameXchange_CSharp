using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dream_game
{
    public partial class GerCupom : Form
    {
        public GerCupom()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.Load += GerCupom_Load;
            buttonPesquisar.Click += buttonPesquisar_Click;
            buttonExcluir.Click += buttonExcluir_Click;
            buttonEditar.Click += buttonEditar_Click;
            buttonSalvar.Click += buttonSalvar_Click;
            buttonCriar.Click += buttonCriar_Click;

            dataGridViewCupons.ReadOnly = false;
            dataGridViewCupons.AllowUserToAddRows = false;
            dataGridViewCupons.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void GerCupom_Load(object sender, EventArgs e)
        {
            CarregarCupons();
            AjustarCores();
        }

        private void CarregarCupons(string filtro = "")
        {
            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";
            string query = "SELECT id_cupom, nome_cupom, desconto FROM tb_cupom";

            if (!string.IsNullOrWhiteSpace(filtro))
                query += " WHERE nome_cupom LIKE @Filtro";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                if (!string.IsNullOrWhiteSpace(filtro))
                    comando.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                dataGridViewCupons.DataSource = tabela;

                // Bloqueia tudo, exceto colunas editáveis
                foreach (DataGridViewColumn col in dataGridViewCupons.Columns)
                    col.ReadOnly = !(col.Name == "nome_cupom" || col.Name == "desconto");
            }
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = textBoxPesquisa.Text.Trim();
            CarregarCupons(filtro);
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridViewCupons.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cupom para excluir.");
                return;
            }

            int idCupom = Convert.ToInt32(dataGridViewCupons.SelectedRows[0].Cells["id_cupom"].Value);

            DialogResult confirm = MessageBox.Show("Tem certeza que deseja excluir este cupom?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";
            string query = "DELETE FROM tb_cupom WHERE id_cupom = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Id", idCupom);
                conexao.Open();
                comando.ExecuteNonQuery();
            }

            CarregarCupons();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewCupons.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma linha para editar.");
                return;
            }

            dataGridViewCupons.BeginEdit(true);
            MessageBox.Show("Você pode agora editar o nome do cupom ou o valor de desconto.\nClique em 'Salvar' para aplicar.");
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (dataGridViewCupons.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cupom para salvar.");
                return;
            }

            DataGridViewRow row = dataGridViewCupons.SelectedRows[0];
            int idCupom = Convert.ToInt32(row.Cells["id_cupom"].Value);
            string nomeCupom = row.Cells["nome_cupom"].Value?.ToString();
            decimal desconto;

            if (!decimal.TryParse(row.Cells["desconto"].Value?.ToString(), out desconto))
            {
                MessageBox.Show("Valor de desconto inválido.");
                return;
            }

            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";
            string query = "UPDATE tb_cupom SET nome_cupom = @Nome, desconto = @Desconto WHERE id_cupom = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Nome", nomeCupom);
                comando.Parameters.AddWithValue("@Desconto", desconto);
                comando.Parameters.AddWithValue("@Id", idCupom);

                conexao.Open();
                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Cupom atualizado com sucesso.");
            CarregarCupons();
        }

        private void buttonCriar_Click(object sender, EventArgs e)
        {
            string nomeCupom = textBoxNomeCupom.Text.Trim();
            string descontoTexto = textBoxDescontoCupom.Text.Trim();

            if (string.IsNullOrEmpty(nomeCupom) || string.IsNullOrEmpty(descontoTexto))
            {
                MessageBox.Show("Preencha o nome do cupom e o desconto.");
                return;
            }

            if (!decimal.TryParse(descontoTexto, out decimal desconto))
            {
                MessageBox.Show("Desconto inválido. Digite um número válido.");
                return;
            }

            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";
            string query = "INSERT INTO tb_cupom (nome_cupom, desconto) VALUES (@Nome, @Desconto)";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Nome", nomeCupom);
                comando.Parameters.AddWithValue("@Desconto", desconto);

                conexao.Open();
                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Cupom criado com sucesso!");
            textBoxNomeCupom.Clear();
            textBoxDescontoCupom.Clear();
            CarregarCupons();
        }

        private void AjustarCores()
        {
            this.BackColor = Color.FromArgb(64, 0, 64);
            foreach (Control ctrl in this.Controls)
            {
                ctrl.ForeColor = Color.White;

                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.DarkSlateBlue;
                    btn.ForeColor = Color.White;
                }
                else if (ctrl is TextBox tb)
                {
                    tb.BackColor = Color.White;
                    tb.ForeColor = Color.Black;
                }
                else if (ctrl is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                }
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
