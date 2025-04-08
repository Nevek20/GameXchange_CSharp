namespace dream_game
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGerUsuario = new System.Windows.Forms.Button();
            this.buttonSair = new System.Windows.Forms.Button();
            this.buttonGerJogo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCompras = new System.Windows.Forms.Button();
            this.buttonCupom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGerUsuario
            // 
            this.buttonGerUsuario.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGerUsuario.Location = new System.Drawing.Point(281, 160);
            this.buttonGerUsuario.Name = "buttonGerUsuario";
            this.buttonGerUsuario.Size = new System.Drawing.Size(241, 49);
            this.buttonGerUsuario.TabIndex = 0;
            this.buttonGerUsuario.Text = "Gerenciamento de Usuários";
            this.buttonGerUsuario.UseVisualStyleBackColor = true;
            this.buttonGerUsuario.Click += new System.EventHandler(this.buttonGerUsuario_Click);
            // 
            // buttonSair
            // 
            this.buttonSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonSair.Font = new System.Drawing.Font("MS Gothic", 15.75F);
            this.buttonSair.Location = new System.Drawing.Point(354, 400);
            this.buttonSair.Name = "buttonSair";
            this.buttonSair.Size = new System.Drawing.Size(96, 38);
            this.buttonSair.TabIndex = 2;
            this.buttonSair.Text = "Sair";
            this.buttonSair.UseVisualStyleBackColor = false;
            this.buttonSair.Click += new System.EventHandler(this.buttonSair_Click);
            // 
            // buttonGerJogo
            // 
            this.buttonGerJogo.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGerJogo.Location = new System.Drawing.Point(281, 105);
            this.buttonGerJogo.Name = "buttonGerJogo";
            this.buttonGerJogo.Size = new System.Drawing.Size(241, 49);
            this.buttonGerJogo.TabIndex = 3;
            this.buttonGerJogo.Text = "Gerenciamento de Jogos";
            this.buttonGerJogo.UseVisualStyleBackColor = true;
            this.buttonGerJogo.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 30F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(353, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "Menu";
            // 
            // buttonCompras
            // 
            this.buttonCompras.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCompras.Location = new System.Drawing.Point(281, 215);
            this.buttonCompras.Name = "buttonCompras";
            this.buttonCompras.Size = new System.Drawing.Size(241, 49);
            this.buttonCompras.TabIndex = 5;
            this.buttonCompras.Text = "Gerenciamento de Compras";
            this.buttonCompras.UseVisualStyleBackColor = true;
            this.buttonCompras.Click += new System.EventHandler(this.buttonCompras_Click);
            // 
            // buttonCupom
            // 
            this.buttonCupom.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCupom.Location = new System.Drawing.Point(281, 270);
            this.buttonCupom.Name = "buttonCupom";
            this.buttonCupom.Size = new System.Drawing.Size(241, 49);
            this.buttonCupom.TabIndex = 6;
            this.buttonCupom.Text = "Gerenciamento de Cupons";
            this.buttonCupom.UseVisualStyleBackColor = true;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCupom);
            this.Controls.Add(this.buttonCompras);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGerJogo);
            this.Controls.Add(this.buttonSair);
            this.Controls.Add(this.buttonGerUsuario);
            this.Name = "Menu";
            this.Text = "GameXchange - Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGerUsuario;
        private System.Windows.Forms.Button buttonSair;
        private System.Windows.Forms.Button buttonGerJogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCompras;
        private System.Windows.Forms.Button buttonCupom;
    }
}