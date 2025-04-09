using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dream_game
{
    public partial class GerUsuario : Form
    {
        public GerUsuario()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.Load += GerUsuario_Load;
            buttonPesquisar.Click += buttonPesquisar_Click;
            buttonExcluir.Click += buttonExcluir_Click;
            buttonEditar.Click += buttonEditar_Click;
            buttonSalvar.Click += buttonSalvar_Click;

            dataGridViewUsuarios.ReadOnly = false;
            dataGridViewUsuarios.AllowUserToAddRows = false;
            dataGridViewUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void GerUsuario_Load(object sender, EventArgs e)
        {
            CarregarUsuarios();
            AjustarCores();
        }

        private void CarregarUsuarios(string filtro = "")
        {
            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123; Convert Zero Datetime=True;";
            string query = "SELECT id_usuario, email, nome_perfil, nome_real, data_nascimento, qtd_jogos, tipo FROM tb_usuario";

            if (!string.IsNullOrWhiteSpace(filtro))
                query += " WHERE nome_perfil LIKE @Filtro OR nome_real LIKE @Filtro";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                if (!string.IsNullOrWhiteSpace(filtro))
                    comando.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabela = new DataTable();

                adapter.Fill(tabela);
                dataGridViewUsuarios.DataSource = tabela;

                foreach (DataGridViewColumn col in dataGridViewUsuarios.Columns)
                    col.ReadOnly = col.Name == "id_usuario" || col.Name == "qtd_jogos";
            }
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = textBoxPesquisa.Text.Trim();
            CarregarUsuarios(filtro);
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um usuário para excluir.");
                return;
            }

            int idUsuario = Convert.ToInt32(dataGridViewUsuarios.SelectedRows[0].Cells["id_usuario"].Value);

            DialogResult confirm = MessageBox.Show("Tem certeza que deseja excluir este usuário?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123; Convert Zero Datetime=True;";
            string query = "DELETE FROM tb_usuario WHERE id_usuario = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Id", idUsuario);
                conexao.Open();
                comando.ExecuteNonQuery();
            }

            CarregarUsuarios();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um usuário para editar.");
                return;
            }

            dataGridViewUsuarios.BeginEdit(true);
            MessageBox.Show("Você pode agora editar os dados do usuário.\nClique em 'Salvar' para aplicar as alterações.");
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um usuário para salvar.");
                return;
            }

            DataGridViewRow row = dataGridViewUsuarios.SelectedRows[0];

            int id = Convert.ToInt32(row.Cells["id_usuario"].Value);
            string email = row.Cells["email"].Value?.ToString();
            string perfil = row.Cells["nome_perfil"].Value?.ToString();
            string nome = row.Cells["nome_real"].Value?.ToString();
            string nascimento = row.Cells["data_nascimento"].Value?.ToString();
            string tipo = row.Cells["tipo"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(perfil) ||
                string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(nascimento))
            {
                MessageBox.Show("Todos os campos obrigatórios devem ser preenchidos.");
                return;
            }

            string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123; Convert Zero Datetime=True;";
            string query = @"
                UPDATE tb_usuario 
                SET email = @Email, nome_perfil = @Perfil, nome_real = @Nome, data_nascimento = @Nascimento, tipo = @Tipo
                WHERE id_usuario = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Email", email);
                comando.Parameters.AddWithValue("@Perfil", perfil);
                comando.Parameters.AddWithValue("@Nome", nome);
                comando.Parameters.AddWithValue("@Nascimento", nascimento);
                comando.Parameters.AddWithValue("@Tipo", tipo);
                comando.Parameters.AddWithValue("@Id", id);

                try
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Usuário atualizado com sucesso.");
                    CarregarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar: " + ex.Message);
                }
            }
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
