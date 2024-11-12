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
            this.btnConn = new System.Windows.Forms.Button();
            this.btnDesconnect = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.btnReg = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnGreen = new System.Windows.Forms.Button();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnYellow = new System.Windows.Forms.Button();
            this.cardlbl = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelBarraTitulo = new System.Windows.Forms.Panel();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button18 = new System.Windows.Forms.Button();
            this.GameJoin = new System.Windows.Forms.GroupBox();
            this.indicadorConexion_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.onlineGrid = new System.Windows.Forms.DataGridView();
            this.lblPlayersOnline = new System.Windows.Forms.Label();
            this.joinGameBox = new System.Windows.Forms.TextBox();
            this.lblJoin = new System.Windows.Forms.Label();
            this.lblGames = new System.Windows.Forms.Label();
            this.gamesGrid = new System.Windows.Forms.DataGridView();
            this.panelBarraTitulo.SuspendLayout();
            this.GameJoin.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.onlineGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gamesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConn
            // 
            this.btnConn.BackColor = System.Drawing.Color.Black;
            this.btnConn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnConn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConn.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConn.ForeColor = System.Drawing.Color.White;
            this.btnConn.Location = new System.Drawing.Point(9, 10);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(169, 27);
            this.btnConn.TabIndex = 4;
            this.btnConn.Text = "Connection";
            this.btnConn.UseVisualStyleBackColor = false;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // btnDesconnect
            // 
            this.btnDesconnect.BackColor = System.Drawing.Color.Black;
            this.btnDesconnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDesconnect.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.btnDesconnect.ForeColor = System.Drawing.Color.White;
            this.btnDesconnect.Location = new System.Drawing.Point(9, 43);
            this.btnDesconnect.Name = "btnDesconnect";
            this.btnDesconnect.Size = new System.Drawing.Size(169, 27);
            this.btnDesconnect.TabIndex = 10;
            this.btnDesconnect.Text = "Desconnection";
            this.btnDesconnect.UseVisualStyleBackColor = false;
            this.btnDesconnect.Click += new System.EventHandler(this.btnDesconnect_Click);
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
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(9, 32);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(89, 21);
            this.lblPassword.TabIndex = 14;
            this.lblPassword.Text = "Password:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(5, 6);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(93, 21);
            this.lblUsername.TabIndex = 15;
            this.lblUsername.Text = "Username:";
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
            // btnReg
            // 
            this.btnReg.BackColor = System.Drawing.Color.Black;
            this.btnReg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReg.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.btnReg.ForeColor = System.Drawing.Color.White;
            this.btnReg.Location = new System.Drawing.Point(37, 67);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(169, 30);
            this.btnReg.TabIndex = 17;
            this.btnReg.Text = "Register";
            this.btnReg.UseVisualStyleBackColor = false;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // btnLog
            // 
            this.btnLog.BackColor = System.Drawing.Color.Black;
            this.btnLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLog.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.btnLog.ForeColor = System.Drawing.Color.White;
            this.btnLog.Location = new System.Drawing.Point(37, 103);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(169, 30);
            this.btnLog.TabIndex = 19;
            this.btnLog.Text = "Log In";
            this.btnLog.UseVisualStyleBackColor = false;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnGreen
            // 
            this.btnGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGreen.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreen.Location = new System.Drawing.Point(437, 138);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(82, 110);
            this.btnGreen.TabIndex = 24;
            this.btnGreen.Text = "G";
            this.btnGreen.UseVisualStyleBackColor = false;
            this.btnGreen.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.Color.Red;
            this.btnRed.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRed.Location = new System.Drawing.Point(538, 138);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(82, 110);
            this.btnRed.TabIndex = 25;
            this.btnRed.Text = "R";
            this.btnRed.UseVisualStyleBackColor = false;
            this.btnRed.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnBlue
            // 
            this.btnBlue.BackColor = System.Drawing.Color.Blue;
            this.btnBlue.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBlue.Location = new System.Drawing.Point(437, 269);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(82, 110);
            this.btnBlue.TabIndex = 26;
            this.btnBlue.Text = "B";
            this.btnBlue.UseVisualStyleBackColor = false;
            this.btnBlue.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnYellow
            // 
            this.btnYellow.BackColor = System.Drawing.Color.Yellow;
            this.btnYellow.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYellow.Location = new System.Drawing.Point(538, 269);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(82, 110);
            this.btnYellow.TabIndex = 27;
            this.btnYellow.Text = "Y";
            this.btnYellow.UseVisualStyleBackColor = false;
            this.btnYellow.Click += new System.EventHandler(this.button8_Click);
            // 
            // cardlbl
            // 
            this.cardlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardlbl.Location = new System.Drawing.Point(652, 218);
            this.cardlbl.Name = "cardlbl";
            this.cardlbl.Size = new System.Drawing.Size(190, 90);
            this.cardlbl.TabIndex = 29;
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.Black;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.btnLast.ForeColor = System.Drawing.Color.White;
            this.btnLast.Location = new System.Drawing.Point(671, 322);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(147, 35);
            this.btnLast.TabIndex = 30;
            this.btnLast.Text = "Last card!";
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.button10_Click);
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
            this.panelBarraTitulo.Controls.Add(this.lblTitle);
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
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(11, 8);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(202, 21);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Welcome to UNO Game!";
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
            this.GameJoin.Location = new System.Drawing.Point(177, 287);
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
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateGray;
            this.panel1.Controls.Add(this.username);
            this.panel1.Controls.Add(this.password);
            this.panel1.Controls.Add(this.btnLog);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.btnReg);
            this.panel1.Controls.Add(this.lblUsername);
            this.panel1.Location = new System.Drawing.Point(12, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 143);
            this.panel1.TabIndex = 48;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateGray;
            this.panel2.Controls.Add(this.btnConn);
            this.panel2.Controls.Add(this.indicadorConexion_label);
            this.panel2.Controls.Add(this.btnDesconnect);
            this.panel2.Location = new System.Drawing.Point(12, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 77);
            this.panel2.TabIndex = 49;
            // 
            // onlineGrid
            // 
            this.onlineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.onlineGrid.Location = new System.Drawing.Point(922, 86);
            this.onlineGrid.Name = "onlineGrid";
            this.onlineGrid.Size = new System.Drawing.Size(166, 213);
            this.onlineGrid.TabIndex = 51;
            // 
            // lblPlayersOnline
            // 
            this.lblPlayersOnline.AutoSize = true;
            this.lblPlayersOnline.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayersOnline.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayersOnline.ForeColor = System.Drawing.Color.White;
            this.lblPlayersOnline.Location = new System.Drawing.Point(946, 55);
            this.lblPlayersOnline.Name = "lblPlayersOnline";
            this.lblPlayersOnline.Size = new System.Drawing.Size(124, 21);
            this.lblPlayersOnline.TabIndex = 20;
            this.lblPlayersOnline.Text = "Players Online";
            // 
            // joinGameBox
            // 
            this.joinGameBox.BackColor = System.Drawing.Color.Black;
            this.joinGameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.joinGameBox.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.joinGameBox.ForeColor = System.Drawing.Color.White;
            this.joinGameBox.Location = new System.Drawing.Point(16, 323);
            this.joinGameBox.Name = "joinGameBox";
            this.joinGameBox.Size = new System.Drawing.Size(136, 16);
            this.joinGameBox.TabIndex = 20;
            // 
            // lblJoin
            // 
            this.lblJoin.AutoSize = true;
            this.lblJoin.BackColor = System.Drawing.Color.Transparent;
            this.lblJoin.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJoin.ForeColor = System.Drawing.Color.White;
            this.lblJoin.Location = new System.Drawing.Point(12, 287);
            this.lblJoin.Name = "lblJoin";
            this.lblJoin.Size = new System.Drawing.Size(115, 21);
            this.lblJoin.TabIndex = 21;
            this.lblJoin.Text = "Search game:";
            // 
            // lblGames
            // 
            this.lblGames.AutoSize = true;
            this.lblGames.BackColor = System.Drawing.Color.Transparent;
            this.lblGames.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGames.ForeColor = System.Drawing.Color.White;
            this.lblGames.Location = new System.Drawing.Point(946, 316);
            this.lblGames.Name = "lblGames";
            this.lblGames.Size = new System.Drawing.Size(128, 21);
            this.lblGames.TabIndex = 52;
            this.lblGames.Text = "Current Games";
            // 
            // gamesGrid
            // 
            this.gamesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gamesGrid.Location = new System.Drawing.Point(922, 340);
            this.gamesGrid.Name = "gamesGrid";
            this.gamesGrid.Size = new System.Drawing.Size(166, 211);
            this.gamesGrid.TabIndex = 53;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.fondo_oscuro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1100, 609);
            this.Controls.Add(this.lblGames);
            this.Controls.Add(this.gamesGrid);
            this.Controls.Add(this.joinGameBox);
            this.Controls.Add(this.lblJoin);
            this.Controls.Add(this.lblPlayersOnline);
            this.Controls.Add(this.onlineGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GameJoin);
            this.Controls.Add(this.panelBarraTitulo);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.cardlbl);
            this.Controls.Add(this.btnYellow);
            this.Controls.Add(this.btnBlue);
            this.Controls.Add(this.btnRed);
            this.Controls.Add(this.btnGreen);
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
            ((System.ComponentModel.ISupportInitialize)(this.onlineGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gamesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button btnDesconnect;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Label cardlbl;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelBarraTitulo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button minimizeBtn;
        private System.Windows.Forms.GroupBox GameJoin;
        private System.Windows.Forms.Button maximizeBtn;
        private System.Windows.Forms.Label indicadorConexion_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView onlineGrid;
        private System.Windows.Forms.Label lblPlayersOnline;
        private System.Windows.Forms.TextBox joinGameBox;
        private System.Windows.Forms.Label lblJoin;
        private System.Windows.Forms.Label lblGames;
        private System.Windows.Forms.DataGridView gamesGrid;
    }
}


