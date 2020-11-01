namespace Neural_Chess
{
    partial class Chess_MainMenu
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ChessLobby_Button = new System.Windows.Forms.Button();
            this.ChessPuzzle_Button = new System.Windows.Forms.Button();
            this.Settings_Button = new System.Windows.Forms.Button();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ChessLobby_Button, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ChessPuzzle_Button, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Settings_Button, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Exit_Button, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(366, 284);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ChessLobby_Button
            // 
            this.ChessLobby_Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChessLobby_Button.Location = new System.Drawing.Point(3, 3);
            this.ChessLobby_Button.Name = "ChessLobby_Button";
            this.ChessLobby_Button.Size = new System.Drawing.Size(360, 57);
            this.ChessLobby_Button.TabIndex = 0;
            this.ChessLobby_Button.Text = "ШАХМАТНОЕ ЛОББИ";
            this.ChessLobby_Button.UseVisualStyleBackColor = true;
            // 
            // ChessPuzzle_Button
            // 
            this.ChessPuzzle_Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChessPuzzle_Button.Location = new System.Drawing.Point(3, 76);
            this.ChessPuzzle_Button.Name = "ChessPuzzle_Button";
            this.ChessPuzzle_Button.Size = new System.Drawing.Size(360, 57);
            this.ChessPuzzle_Button.TabIndex = 2;
            this.ChessPuzzle_Button.Text = "ШАХМАТНЫЕ ЗАДАЧИ";
            this.ChessPuzzle_Button.UseVisualStyleBackColor = true;
            // 
            // Settings_Button
            // 
            this.Settings_Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Settings_Button.Location = new System.Drawing.Point(3, 149);
            this.Settings_Button.Name = "Settings_Button";
            this.Settings_Button.Size = new System.Drawing.Size(360, 57);
            this.Settings_Button.TabIndex = 3;
            this.Settings_Button.Text = "НАСТРОЙКИ";
            this.Settings_Button.UseVisualStyleBackColor = true;
            // 
            // Exit_Button
            // 
            this.Exit_Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Exit_Button.Location = new System.Drawing.Point(3, 222);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(360, 59);
            this.Exit_Button.TabIndex = 4;
            this.Exit_Button.Text = "ВЫЙТИ";
            this.Exit_Button.UseVisualStyleBackColor = true;
            // 
            // Chess_MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Chess_MainMenu";
            this.Size = new System.Drawing.Size(366, 284);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Button ChessLobby_Button;
        public System.Windows.Forms.Button ChessPuzzle_Button;
        public System.Windows.Forms.Button Settings_Button;
        public System.Windows.Forms.Button Exit_Button;
    }
}
