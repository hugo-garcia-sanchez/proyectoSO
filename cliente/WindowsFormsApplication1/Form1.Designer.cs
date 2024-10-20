namespace ClientApplication
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.WhatGame = new System.Windows.Forms.RadioButton();
            this.NumPlayers = new System.Windows.Forms.RadioButton();
            this.PlayingG1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.cardlbl = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelBarraTitulo = new System.Windows.Forms.Panel();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button18 = new System.Windows.Forms.Button();
            this.GameJoin = new System.Windows.Forms.GroupBox();
            this.indicadorConexion_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.onlineGrid = new System.Windows.Forms.DataGridView();
            this.panelBarraTitulo.SuspendLayout();
            this.GameJoin.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.onlineGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(9, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 27);
            this.button1.TabIndex = 4;
            this.button1.Text = "Connection";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Black;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(9, 43);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(169, 27);
            this.button3.TabIndex = 10;
            this.button3.Text = "Desconnection";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.Color.Black;
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.password.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.password.ForeColor = System.Drawing.Color.White;
            this.password.Location = new System.Drawing.Point(104, 36);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(136, 16);
            this.password.TabIndex = 13;
            this.password.TextChanged += new System.EventHandler(this.password_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(9, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 21);
            this.label2.TabIndex = 14;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(5, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 21);
            this.label3.TabIndex = 15;
            this.label3.Text = "Username:";
            // 
            // username
            // 
            this.username.BackColor = System.Drawing.Color.Black;
            this.username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.username.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.ForeColor = System.Drawing.Color.White;
            this.username.Location = new System.Drawing.Point(104, 10);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(136, 16);
            this.username.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(37, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 30);
            this.button2.TabIndex = 17;
            this.button2.Text = "Register";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Black;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(37, 103);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(169, 30);
            this.button4.TabIndex = 19;
            this.button4.Text = "Log In";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // WhatGame
            // 
            this.WhatGame.AutoSize = true;
            this.WhatGame.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.WhatGame.Location = new System.Drawing.Point(3, 55);
            this.WhatGame.Name = "WhatGame";
            this.WhatGame.Size = new System.Drawing.Size(175, 20);
            this.WhatGame.TabIndex = 7;
            this.WhatGame.TabStop = true;
            this.WhatGame.Text = "What game are you playing?";
            this.WhatGame.UseVisualStyleBackColor = true;
            // 
            // NumPlayers
            // 
            this.NumPlayers.AutoSize = true;
            this.NumPlayers.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.NumPlayers.Location = new System.Drawing.Point(3, 80);
            this.NumPlayers.Name = "NumPlayers";
            this.NumPlayers.Size = new System.Drawing.Size(250, 20);
            this.NumPlayers.TabIndex = 7;
            this.NumPlayers.TabStop = true;
            this.NumPlayers.Text = "How many players are there in your game?";
            this.NumPlayers.UseVisualStyleBackColor = true;
            // 
            // PlayingG1
            // 
            this.PlayingG1.AutoSize = true;
            this.PlayingG1.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.PlayingG1.Location = new System.Drawing.Point(3, 29);
            this.PlayingG1.Name = "PlayingG1";
            this.PlayingG1.Size = new System.Drawing.Size(163, 20);
            this.PlayingG1.TabIndex = 8;
            this.PlayingG1.TabStop = true;
            this.PlayingG1.Text = "Who is playing in game 1?";
            this.PlayingG1.UseVisualStyleBackColor = true;
            this.PlayingG1.CheckedChanged += new System.EventHandler(this.PlayingG1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(96, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "Queries";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Black;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(92, 106);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Send";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button9.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(398, 145);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(82, 110);
            this.button9.TabIndex = 24;
            this.button9.Text = "G";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Red;
            this.button6.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(499, 145);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(82, 110);
            this.button6.TabIndex = 25;
            this.button6.Text = "R";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Blue;
            this.button7.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(398, 276);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(82, 110);
            this.button7.TabIndex = 26;
            this.button7.Text = "B";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Yellow;
            this.button8.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(499, 276);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(82, 110);
            this.button8.TabIndex = 27;
            this.button8.Text = "Y";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // cardlbl
            // 
            this.cardlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardlbl.Location = new System.Drawing.Point(613, 225);
            this.cardlbl.Name = "cardlbl";
            this.cardlbl.Size = new System.Drawing.Size(190, 90);
            this.cardlbl.TabIndex = 29;
            this.cardlbl.Click += new System.EventHandler(this.contlbl_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Black;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(632, 329);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(147, 35);
            this.button10.TabIndex = 30;
            this.button10.Text = "Last card!";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Black;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button11.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.button11.ForeColor = System.Drawing.Color.White;
            this.button11.Location = new System.Drawing.Point(613, 174);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(190, 35);
            this.button11.TabIndex = 31;
            this.button11.Text = "How does it work?";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.Black;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button13.Font = new System.Drawing.Font("Segoe UI Emoji", 8F, System.Drawing.FontStyle.Bold);
            this.button13.ForeColor = System.Drawing.Color.White;
            this.button13.Location = new System.Drawing.Point(6, 19);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(60, 20);
            this.button13.TabIndex = 35;
            this.button13.Text = "1";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.Black;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button12.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.ForeColor = System.Drawing.Color.White;
            this.button12.Location = new System.Drawing.Point(6, 45);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(60, 20);
            this.button12.TabIndex = 40;
            this.button12.Text = "2";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click_1);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Black;
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button15.Font = new System.Drawing.Font("Segoe UI Emoji", 8F, System.Drawing.FontStyle.Bold);
            this.button15.ForeColor = System.Drawing.Color.White;
            this.button15.Location = new System.Drawing.Point(72, 19);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(60, 20);
            this.button15.TabIndex = 41;
            this.button15.Text = "3";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.Black;
            this.button16.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button16.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.ForeColor = System.Drawing.Color.White;
            this.button16.Location = new System.Drawing.Point(72, 45);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(60, 20);
            this.button16.TabIndex = 42;
            this.button16.Text = "4";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.Black;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button17.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.button17.ForeColor = System.Drawing.Color.White;
            this.button17.Location = new System.Drawing.Point(922, 49);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(166, 30);
            this.button17.TabIndex = 43;
            this.button17.Text = "Players Online: ";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1067, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 28);
            this.btnClose.TabIndex = 44;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelBarraTitulo
            // 
            this.panelBarraTitulo.BackColor = System.Drawing.Color.SteelBlue;
            this.panelBarraTitulo.Controls.Add(this.maximizeBtn);
            this.panelBarraTitulo.Controls.Add(this.minimizeBtn);
            this.panelBarraTitulo.Controls.Add(this.label6);
            this.panelBarraTitulo.Controls.Add(this.btnClose);
            this.panelBarraTitulo.Controls.Add(this.button18);
            this.panelBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelBarraTitulo.Margin = new System.Windows.Forms.Padding(2);
            this.panelBarraTitulo.Name = "panelBarraTitulo";
            this.panelBarraTitulo.Size = new System.Drawing.Size(1100, 37);
            this.panelBarraTitulo.TabIndex = 45;
            this.panelBarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBarraTitulo_MouseDown);
            // 
            // maximizeBtn
            // 
            this.maximizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeBtn.BackColor = System.Drawing.Color.Transparent;
            this.maximizeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.maximizeBtn.FlatAppearance.BorderSize = 0;
            this.maximizeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.maximizeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.maximizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maximizeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximizeBtn.ForeColor = System.Drawing.Color.White;
            this.maximizeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.maximizeBtn.Location = new System.Drawing.Point(1034, 3);
            this.maximizeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.maximizeBtn.Name = "maximizeBtn";
            this.maximizeBtn.Size = new System.Drawing.Size(28, 28);
            this.maximizeBtn.TabIndex = 46;
            this.maximizeBtn.Text = "⬜";
            this.maximizeBtn.UseVisualStyleBackColor = false;
            this.maximizeBtn.Click += new System.EventHandler(this.maximizeBtn_Click);
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeBtn.BackColor = System.Drawing.Color.Transparent;
            this.minimizeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimizeBtn.FlatAppearance.BorderSize = 0;
            this.minimizeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.minimizeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeBtn.ForeColor = System.Drawing.Color.White;
            this.minimizeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.minimizeBtn.Location = new System.Drawing.Point(996, 3);
            this.minimizeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(28, 28);
            this.minimizeBtn.TabIndex = 45;
            this.minimizeBtn.Text = "—";
            this.minimizeBtn.UseVisualStyleBackColor = false;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(11, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 21);
            this.label6.TabIndex = 8;
            this.label6.Text = "Welcome to UNO Game!";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // button18
            // 
            this.button18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button18.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button18.FlatAppearance.BorderSize = 0;
            this.button18.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button18.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(222)))), ((int)(((byte)(65)))));
            this.button18.Location = new System.Drawing.Point(1069, 5);
            this.button18.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(26, 28);
            this.button18.TabIndex = 5;
            this.button18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button18.UseVisualStyleBackColor = true;
            // 
            // GameJoin
            // 
            this.GameJoin.BackColor = System.Drawing.Color.SlateGray;
            this.GameJoin.Controls.Add(this.button13);
            this.GameJoin.Controls.Add(this.button12);
            this.GameJoin.Controls.Add(this.button16);
            this.GameJoin.Controls.Add(this.button15);
            this.GameJoin.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameJoin.ForeColor = System.Drawing.Color.White;
            this.GameJoin.Location = new System.Drawing.Point(938, 201);
            this.GameJoin.Name = "GameJoin";
            this.GameJoin.Size = new System.Drawing.Size(137, 70);
            this.GameJoin.TabIndex = 46;
            this.GameJoin.TabStop = false;
            this.GameJoin.Text = "Join a game!";
            // 
            // indicadorConexion_label
            // 
            this.indicadorConexion_label.BackColor = System.Drawing.Color.Black;
            this.indicadorConexion_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indicadorConexion_label.ForeColor = System.Drawing.Color.Red;
            this.indicadorConexion_label.Location = new System.Drawing.Point(184, 11);
            this.indicadorConexion_label.Name = "indicadorConexion_label";
            this.indicadorConexion_label.Size = new System.Drawing.Size(56, 59);
            this.indicadorConexion_label.TabIndex = 47;
            this.indicadorConexion_label.Text = "⚫";
            this.indicadorConexion_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.indicadorConexion_label.Click += new System.EventHandler(this.indicadorConexion_label_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateGray;
            this.panel1.Controls.Add(this.username);
            this.panel1.Controls.Add(this.password);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 143);
            this.panel1.TabIndex = 48;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateGray;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.indicadorConexion_label);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Location = new System.Drawing.Point(12, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 77);
            this.panel2.TabIndex = 49;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SlateGray;
            this.panel3.Controls.Add(this.WhatGame);
            this.panel3.Controls.Add(this.NumPlayers);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.PlayingG1);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(12, 292);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 139);
            this.panel3.TabIndex = 50;
            // 
            // onlineGrid
            // 
            this.onlineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.onlineGrid.Location = new System.Drawing.Point(922, 86);
            this.onlineGrid.Name = "onlineGrid";
            this.onlineGrid.Size = new System.Drawing.Size(166, 104);
            this.onlineGrid.TabIndex = 51;
            this.onlineGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.onlineGrid_CellContentClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.fondo_oscuro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1100, 609);
            this.Controls.Add(this.onlineGrid);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GameJoin);
            this.Controls.Add(this.panelBarraTitulo);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.cardlbl);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button9);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelBarraTitulo.ResumeLayout(false);
            this.panelBarraTitulo.PerformLayout();
            this.GameJoin.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.onlineGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RadioButton WhatGame;
        private System.Windows.Forms.RadioButton NumPlayers;
        private System.Windows.Forms.RadioButton PlayingG1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label cardlbl;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelBarraTitulo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button minimizeBtn;
        private System.Windows.Forms.GroupBox GameJoin;
        private System.Windows.Forms.Button maximizeBtn;
        private System.Windows.Forms.Label indicadorConexion_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView onlineGrid;
    }
}


