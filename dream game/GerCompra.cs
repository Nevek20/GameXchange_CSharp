using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dream_game
{
    public partial class GerCompra : Form
    {
        public GerCompra()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.Load += GerCompra_Load;
            buttonPesquisar.Click += buttonPesquisar_Click;
            buttonExcluir.Click += buttonExcluir_Click;
            buttonSalvar.Click += buttonSalvar_Click;
            buttonEditar.Click += buttonEditar_Click;
        }

        private void GerCompra_Load(object sender, EventArgs e)
        {
            CarregarCompras();
            AjustarCores();
        }

        private void CarregarCompras(string filtro = "")
        {
            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";
            string query = @"
                SELECT c.id_compras, u.nome_real AS usuario, j.nome AS jogo, c.chave_ativacao, c.data_compra
                FROM tb_compras c
                JOIN tb_usuario u ON u.id_usuario = c.id_usuario
                JOIN tb_jogos j ON j.id_jogos = c.id_jogos";

            if (!string.IsNullOrWhiteSpace(filtro))
                query += " WHERE u.nome_real LIKE @Filtro OR j.nome LIKE @Filtro";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                if (!string.IsNullOrWhiteSpace(filtro))
                    comando.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                dataGridViewCompras.DataSource = tabela;
            }

            // Permitir edição apenas em colunas específicas
            dataGridViewCompras.ReadOnly = false;
            foreach (DataGridViewColumn col in dataGridViewCompras.Columns)
            {
                col.ReadOnly = !(col.Name == "chave_ativacao" || col.Name == "data_compra");
            }
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = textBoxPesquisa.Text.Trim();
            CarregarCompras(filtro);
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridViewCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma compra para excluir.");
                return;
            }

            int idCompra = Convert.ToInt32(dataGridViewCompras.SelectedRows[0].Cells["id_compras"].Value);

            DialogResult confirm = MessageBox.Show("Tem certeza que deseja excluir esta compra?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";
            string query = "DELETE FROM tb_compras WHERE id_compras = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Id", idCompra);
                conexao.Open();
                comando.ExecuteNonQuery();
            }

            CarregarCompras();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (dataGridViewCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma compra para salvar alterações.");
                return;
            }

            DataGridViewRow row = dataGridViewCompras.SelectedRows[0];
            int idCompra = Convert.ToInt32(row.Cells["id_compras"].Value);
            string chave = row.Cells["chave_ativacao"].Value?.ToString();
            DateTime dataCompra;

            if (!DateTime.TryParse(row.Cells["data_compra"].Value?.ToString(), out dataCompra))
            {
                MessageBox.Show("Data inválida.");
                return;
            }

            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";
            string query = @"
                UPDATE tb_compras 
                SET chave_ativacao = @Chave, data_compra = @Data 
                WHERE id_compras = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Chave", chave);
                comando.Parameters.AddWithValue("@Data", dataCompra);
                comando.Parameters.AddWithValue("@Id", idCompra);

                conexao.Open();
                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Alterações salvas com sucesso.");
            CarregarCompras();
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
        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma linha para editar.");
                return;
            }

            dataGridViewCompras.BeginEdit(true);

            MessageBox.Show("Agora você pode editar os campos permitidos.\nClique em 'Salvar' para aplicar as alterações.");
        }
    }
}
