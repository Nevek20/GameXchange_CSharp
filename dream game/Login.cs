using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace dream_game
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.buttonEntrar.Click += new EventHandler(this.buttonEntrar_Click);
            this.checkBoxMostrarSenha.CheckedChanged += new EventHandler(this.checkBoxMostrarSenha_CheckedChanged);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            labelInfo.Text = "";
            textBoxSenha.UseSystemPasswordChar = true; // Esconde senha por padrão
        }

        private void checkBoxMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSenha.UseSystemPasswordChar = !checkBoxMostrarSenha.Checked;
        }

        private void buttonEntrar_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text.Trim();
            string senha = textBoxSenha.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                labelInfo.Text = "Por favor, preencha todos os campos.";
                labelInfo.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string conexaoString = "Server=localhost; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();

                    string query = @"
                        SELECT id_usuario, email, nome_perfil, nome_real, senha, tipo 
                        FROM tb_usuario 
                        WHERE email = @Email AND senha = @Senha";

                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@Email", email);
                        comando.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                string tipo = reader["tipo"].ToString();

                                if (tipo == "comum")
                                {
                                    labelInfo.Text = "Usuário comum não tem permissão para login.";
                                    labelInfo.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                                else if (tipo == "admin")
                                {
                                    labelInfo.Text = "Login como administrador bem-sucedido!";
                                    labelInfo.ForeColor = System.Drawing.Color.Green;

                                    Menu menuForm = new Menu();
                                    menuForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    labelInfo.Text = "Tipo de usuário não reconhecido.";
                                    labelInfo.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                labelInfo.Text = "Email ou senha inválidos.";
                                labelInfo.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    labelInfo.Text = "Erro ao conectar ao banco de dados: " + ex.Message;
                    labelInfo.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
