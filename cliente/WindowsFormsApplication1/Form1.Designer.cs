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
            this.Connection = new System.Windows.Forms.Button();
            this.Disconnection = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.Register = new System.Windows.Forms.Button();
            this.LogIn = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelBarraTitulo = new System.Windows.Forms.Panel();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button18 = new System.Windows.Forms.Button();
            this.indicadorConexion_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.onlineGrid = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtBuscarPartida = new System.Windows.Forms.TextBox();
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.btnCreateGame = new System.Windows.Forms.Button();
            this.lblBuscarPartida = new System.Windows.Forms.Label();
            this.lblPlayersOnline = new System.Windows.Forms.Label();
            this.lblAvailableGames = new System.Windows.Forms.Label();
            this.gamesGrid = new System.Windows.Forms.DataGridView();
            this.invitationinfo = new System.Windows.Forms.Button();
            this.panelUsuario = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.resultados = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.resultadostex = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblresphours = new System.Windows.Forms.Label();
            this.playerswith = new System.Windows.Forms.Label();
            this.gamesinhours = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lblgameshours = new System.Windows.Forms.TextBox();
            this.Playersplayed = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.lblUserProfile = new System.Windows.Forms.Label();
            this.lblUserNameLittle = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.panelBarraTitulo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.onlineGrid)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamesGrid)).BeginInit();
            this.panelUsuario.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connection
            // 
            this.Connection.BackColor = System.Drawing.Color.Black;
            this.Connection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Connection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Connection.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connection.ForeColor = System.Drawing.Color.White;
            this.Connection.Location = new System.Drawing.Point(9, 10);
            this.Connection.Name = "Connection";
            this.Connection.Size = new System.Drawing.Size(169, 27);
            this.Connection.TabIndex = 4;
            this.Connection.Text = "Connection";
            this.Connection.UseVisualStyleBackColor = false;
            this.Connection.Click += new System.EventHandler(this.Connection_Click);
            // 
            // Disconnection
            // 
            this.Disconnection.BackColor = System.Drawing.Color.Black;
            this.Disconnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Disconnection.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.Disconnection.ForeColor = System.Drawing.Color.White;
            this.Disconnection.Location = new System.Drawing.Point(9, 43);
            this.Disconnection.Name = "Disconnection";
            this.Disconnection.Size = new System.Drawing.Size(169, 27);
            this.Disconnection.TabIndex = 10;
            this.Disconnection.Text = "Disconnection";
            this.Disconnection.UseVisualStyleBackColor = false;
            this.Disconnection.Click += new System.EventHandler(this.Disconnection_Click);
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
            // Register
            // 
            this.Register.BackColor = System.Drawing.Color.Black;
            this.Register.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Register.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.Register.ForeColor = System.Drawing.Color.White;
            this.Register.Location = new System.Drawing.Point(37, 67);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(169, 30);
            this.Register.TabIndex = 17;
            this.Register.Text = "Register";
            this.Register.UseVisualStyleBackColor = false;
            this.Register.Click += new System.EventHandler(this.Register_Click);
            // 
            // LogIn
            // 
            this.LogIn.BackColor = System.Drawing.Color.Black;
            this.LogIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LogIn.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.LogIn.ForeColor = System.Drawing.Color.White;
            this.LogIn.Location = new System.Drawing.Point(37, 103);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(169, 30);
            this.LogIn.TabIndex = 19;
            this.LogIn.Text = "Log In";
            this.LogIn.UseVisualStyleBackColor = false;
            this.LogIn.Click += new System.EventHandler(this.LogIn_Click);
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
            this.btnClose.Location = new System.Drawing.Point(1066, 3);
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
            this.panelBarraTitulo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelBarraTitulo.Name = "panelBarraTitulo";
            this.panelBarraTitulo.Size = new System.Drawing.Size(1099, 47);
            this.panelBarraTitulo.TabIndex = 45;
            this.panelBarraTitulo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBarraTitulo_Paint);
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
            this.maximizeBtn.Location = new System.Drawing.Point(1033, 3);
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
            this.minimizeBtn.Location = new System.Drawing.Point(995, 3);
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
            this.lblTitle.Size = new System.Drawing.Size(414, 21);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Welcome to UNO Game Main Menu! Log in to play!";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
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
            this.button18.Location = new System.Drawing.Point(1067, 5);
            this.button18.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(26, 28);
            this.button18.TabIndex = 5;
            this.button18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button18.UseVisualStyleBackColor = true;
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
            this.panel1.Controls.Add(this.LogIn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Register);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 143);
            this.panel1.TabIndex = 48;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateGray;
            this.panel2.Controls.Add(this.Connection);
            this.panel2.Controls.Add(this.indicadorConexion_label);
            this.panel2.Controls.Add(this.Disconnection);
            this.panel2.Location = new System.Drawing.Point(12, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 77);
            this.panel2.TabIndex = 49;
            // 
            // onlineGrid
            // 
            this.onlineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.onlineGrid.Location = new System.Drawing.Point(482, 144);
            this.onlineGrid.Name = "onlineGrid";
            this.onlineGrid.RowHeadersWidth = 51;
            this.onlineGrid.Size = new System.Drawing.Size(166, 338);
            this.onlineGrid.TabIndex = 51;
            this.onlineGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.onlineGrid_CellContentClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SlateGray;
            this.panel3.Controls.Add(this.txtBuscarPartida);
            this.panel3.Controls.Add(this.btnJoinGame);
            this.panel3.Controls.Add(this.btnCreateGame);
            this.panel3.Controls.Add(this.lblBuscarPartida);
            this.panel3.Location = new System.Drawing.Point(12, 339);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 143);
            this.panel3.TabIndex = 52;
            // 
            // txtBuscarPartida
            // 
            this.txtBuscarPartida.BackColor = System.Drawing.Color.Black;
            this.txtBuscarPartida.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscarPartida.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.txtBuscarPartida.ForeColor = System.Drawing.Color.White;
            this.txtBuscarPartida.Location = new System.Drawing.Point(12, 36);
            this.txtBuscarPartida.Name = "txtBuscarPartida";
            this.txtBuscarPartida.Size = new System.Drawing.Size(231, 16);
            this.txtBuscarPartida.TabIndex = 13;
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.BackColor = System.Drawing.Color.Black;
            this.btnJoinGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnJoinGame.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.btnJoinGame.ForeColor = System.Drawing.Color.White;
            this.btnJoinGame.Location = new System.Drawing.Point(41, 104);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(169, 30);
            this.btnJoinGame.TabIndex = 19;
            this.btnJoinGame.Text = "Join";
            this.btnJoinGame.UseVisualStyleBackColor = false;
            this.btnJoinGame.Click += new System.EventHandler(this.btnJoinGame_Click);
            // 
            // btnCreateGame
            // 
            this.btnCreateGame.BackColor = System.Drawing.Color.Black;
            this.btnCreateGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateGame.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.btnCreateGame.ForeColor = System.Drawing.Color.White;
            this.btnCreateGame.Location = new System.Drawing.Point(41, 68);
            this.btnCreateGame.Name = "btnCreateGame";
            this.btnCreateGame.Size = new System.Drawing.Size(169, 30);
            this.btnCreateGame.TabIndex = 17;
            this.btnCreateGame.Text = "Create";
            this.btnCreateGame.UseVisualStyleBackColor = false;
            this.btnCreateGame.Click += new System.EventHandler(this.btnCreateGame_Click);
            // 
            // lblBuscarPartida
            // 
            this.lblBuscarPartida.AutoSize = true;
            this.lblBuscarPartida.BackColor = System.Drawing.Color.Transparent;
            this.lblBuscarPartida.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscarPartida.ForeColor = System.Drawing.Color.White;
            this.lblBuscarPartida.Location = new System.Drawing.Point(35, 12);
            this.lblBuscarPartida.Name = "lblBuscarPartida";
            this.lblBuscarPartida.Size = new System.Drawing.Size(175, 21);
            this.lblBuscarPartida.TabIndex = 15;
            this.lblBuscarPartida.Text = "Search/Create Game:";
            // 
            // lblPlayersOnline
            // 
            this.lblPlayersOnline.AutoSize = true;
            this.lblPlayersOnline.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayersOnline.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayersOnline.ForeColor = System.Drawing.Color.White;
            this.lblPlayersOnline.Location = new System.Drawing.Point(506, 56);
            this.lblPlayersOnline.Name = "lblPlayersOnline";
            this.lblPlayersOnline.Size = new System.Drawing.Size(128, 21);
            this.lblPlayersOnline.TabIndex = 20;
            this.lblPlayersOnline.Text = "Players Online:";
            // 
            // lblAvailableGames
            // 
            this.lblAvailableGames.AutoSize = true;
            this.lblAvailableGames.BackColor = System.Drawing.Color.Transparent;
            this.lblAvailableGames.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableGames.ForeColor = System.Drawing.Color.White;
            this.lblAvailableGames.Location = new System.Drawing.Point(296, 57);
            this.lblAvailableGames.Name = "lblAvailableGames";
            this.lblAvailableGames.Size = new System.Drawing.Size(144, 21);
            this.lblAvailableGames.TabIndex = 53;
            this.lblAvailableGames.Text = "Available Games:";
            this.lblAvailableGames.Click += new System.EventHandler(this.lblAvailableGames_Click);
            // 
            // gamesGrid
            // 
            this.gamesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gamesGrid.Location = new System.Drawing.Point(289, 92);
            this.gamesGrid.Name = "gamesGrid";
            this.gamesGrid.RowHeadersWidth = 51;
            this.gamesGrid.Size = new System.Drawing.Size(166, 390);
            this.gamesGrid.TabIndex = 54;
            this.gamesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gamesGrid_CellClick);
            this.gamesGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gamesGrid_CellContentClick);
            // 
            // invitationinfo
            // 
            this.invitationinfo.BackColor = System.Drawing.Color.Black;
            this.invitationinfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.invitationinfo.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.invitationinfo.ForeColor = System.Drawing.Color.White;
            this.invitationinfo.Location = new System.Drawing.Point(482, 92);
            this.invitationinfo.Name = "invitationinfo";
            this.invitationinfo.Size = new System.Drawing.Size(166, 28);
            this.invitationinfo.TabIndex = 71;
            this.invitationinfo.Text = "invitation info";
            this.invitationinfo.UseVisualStyleBackColor = false;
            this.invitationinfo.Click += new System.EventHandler(this.invitationinfo_Click);
            // 
            // panelUsuario
            // 
            this.panelUsuario.BackColor = System.Drawing.Color.SlateGray;
            this.panelUsuario.Controls.Add(this.label14);
            this.panelUsuario.Controls.Add(this.resultados);
            this.panelUsuario.Controls.Add(this.label13);
            this.panelUsuario.Controls.Add(this.label12);
            this.panelUsuario.Controls.Add(this.resultadostex);
            this.panelUsuario.Controls.Add(this.label11);
            this.panelUsuario.Controls.Add(this.lblresphours);
            this.panelUsuario.Controls.Add(this.playerswith);
            this.panelUsuario.Controls.Add(this.gamesinhours);
            this.panelUsuario.Controls.Add(this.label9);
            this.panelUsuario.Controls.Add(this.lblgameshours);
            this.panelUsuario.Controls.Add(this.Playersplayed);
            this.panelUsuario.Controls.Add(this.label8);
            this.panelUsuario.Controls.Add(this.label7);
            this.panelUsuario.Controls.Add(this.label6);
            this.panelUsuario.Controls.Add(this.panel5);
            this.panelUsuario.Controls.Add(this.label5);
            this.panelUsuario.Controls.Add(this.label1);
            this.panelUsuario.Controls.Add(this.btnDeleteUser);
            this.panelUsuario.Location = new System.Drawing.Point(679, 57);
            this.panelUsuario.Name = "panelUsuario";
            this.panelUsuario.Size = new System.Drawing.Size(413, 425);
            this.panelUsuario.TabIndex = 49;
            this.panelUsuario.Paint += new System.Windows.Forms.PaintEventHandler(this.panelUsuario_Paint);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(5, 194);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(297, 21);
            this.label14.TabIndex = 87;
            this.label14.Text = "List of players you have played with:";
            // 
            // resultados
            // 
            this.resultados.BackColor = System.Drawing.Color.Transparent;
            this.resultados.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.resultados.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.resultados.ForeColor = System.Drawing.Color.White;
            this.resultados.Location = new System.Drawing.Point(314, 299);
            this.resultados.Name = "resultados";
            this.resultados.Size = new System.Drawing.Size(87, 30);
            this.resultados.TabIndex = 86;
            this.resultados.Text = "Obtain:";
            this.resultados.UseVisualStyleBackColor = false;
            this.resultados.Click += new System.EventHandler(this.resultados_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI Emoji", 10F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(9, 336);
            this.label13.MinimumSize = new System.Drawing.Size(300, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(300, 19);
            this.label13.TabIndex = 85;
            this.label13.Text = "the winner was";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(270, 304);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 21);
            this.label12.TabIndex = 84;
            this.label12.Text = ":";
            // 
            // resultadostex
            // 
            this.resultadostex.BackColor = System.Drawing.Color.DarkGray;
            this.resultadostex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultadostex.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.resultadostex.ForeColor = System.Drawing.Color.White;
            this.resultadostex.Location = new System.Drawing.Point(140, 307);
            this.resultadostex.Name = "resultadostex";
            this.resultadostex.Size = new System.Drawing.Size(124, 16);
            this.resultadostex.TabIndex = 83;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(5, 304);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 21);
            this.label11.TabIndex = 82;
            this.label11.Text = "Results against";
            // 
            // lblresphours
            // 
            this.lblresphours.AutoSize = true;
            this.lblresphours.BackColor = System.Drawing.Color.Transparent;
            this.lblresphours.Font = new System.Drawing.Font("Segoe UI Emoji", 10F, System.Drawing.FontStyle.Bold);
            this.lblresphours.ForeColor = System.Drawing.Color.White;
            this.lblresphours.Location = new System.Drawing.Point(1, 275);
            this.lblresphours.MinimumSize = new System.Drawing.Size(300, 0);
            this.lblresphours.Name = "lblresphours";
            this.lblresphours.Size = new System.Drawing.Size(300, 19);
            this.lblresphours.TabIndex = 81;
            this.lblresphours.Text = "list of games";
            this.lblresphours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerswith
            // 
            this.playerswith.AutoSize = true;
            this.playerswith.BackColor = System.Drawing.Color.Transparent;
            this.playerswith.Font = new System.Drawing.Font("Segoe UI Emoji", 10F, System.Drawing.FontStyle.Bold);
            this.playerswith.ForeColor = System.Drawing.Color.White;
            this.playerswith.Location = new System.Drawing.Point(8, 219);
            this.playerswith.MinimumSize = new System.Drawing.Size(300, 0);
            this.playerswith.Name = "playerswith";
            this.playerswith.Size = new System.Drawing.Size(300, 19);
            this.playerswith.TabIndex = 24;
            this.playerswith.Text = "list of players";
            this.playerswith.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gamesinhours
            // 
            this.gamesinhours.BackColor = System.Drawing.Color.Transparent;
            this.gamesinhours.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gamesinhours.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.gamesinhours.ForeColor = System.Drawing.Color.White;
            this.gamesinhours.Location = new System.Drawing.Point(314, 246);
            this.gamesinhours.Name = "gamesinhours";
            this.gamesinhours.Size = new System.Drawing.Size(87, 30);
            this.gamesinhours.TabIndex = 80;
            this.gamesinhours.Text = "Obtain:";
            this.gamesinhours.UseVisualStyleBackColor = false;
            this.gamesinhours.Click += new System.EventHandler(this.gamesinhours_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(253, 252);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 21);
            this.label9.TabIndex = 79;
            this.label9.Text = "hours.";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // lblgameshours
            // 
            this.lblgameshours.BackColor = System.Drawing.Color.DarkGray;
            this.lblgameshours.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblgameshours.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.lblgameshours.ForeColor = System.Drawing.Color.White;
            this.lblgameshours.Location = new System.Drawing.Point(209, 256);
            this.lblgameshours.Name = "lblgameshours";
            this.lblgameshours.Size = new System.Drawing.Size(41, 16);
            this.lblgameshours.TabIndex = 78;
            this.lblgameshours.TextChanged += new System.EventHandler(this.lblgameshours_TextChanged);
            // 
            // Playersplayed
            // 
            this.Playersplayed.BackColor = System.Drawing.Color.Transparent;
            this.Playersplayed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Playersplayed.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.Playersplayed.ForeColor = System.Drawing.Color.White;
            this.Playersplayed.Location = new System.Drawing.Point(314, 185);
            this.Playersplayed.Name = "Playersplayed";
            this.Playersplayed.Size = new System.Drawing.Size(87, 30);
            this.Playersplayed.TabIndex = 76;
            this.Playersplayed.Text = "Obtain:";
            this.Playersplayed.UseVisualStyleBackColor = false;
            this.Playersplayed.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(9, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 21);
            this.label8.TabIndex = 75;
            this.label8.Text = "QUERIES:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Emoji", 8F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(108, 365);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 15);
            this.label7.TabIndex = 74;
            this.label7.Text = "I want to delete this user:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Emoji", 8F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 15);
            this.label6.TabIndex = 73;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel5.Controls.Add(this.btnChangeColor);
            this.panel5.Controls.Add(this.lblUserProfile);
            this.panel5.Controls.Add(this.lblUserNameLittle);
            this.panel5.Location = new System.Drawing.Point(121, 35);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(159, 110);
            this.panel5.TabIndex = 72;
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.BackColor = System.Drawing.Color.Transparent;
            this.btnChangeColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChangeColor.Font = new System.Drawing.Font("Segoe UI Emoji", 15F, System.Drawing.FontStyle.Bold);
            this.btnChangeColor.ForeColor = System.Drawing.Color.Red;
            this.btnChangeColor.Location = new System.Drawing.Point(113, 3);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(30, 36);
            this.btnChangeColor.TabIndex = 23;
            this.btnChangeColor.Text = "🔄";
            this.btnChangeColor.UseVisualStyleBackColor = false;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // lblUserProfile
            // 
            this.lblUserProfile.AutoSize = true;
            this.lblUserProfile.BackColor = System.Drawing.Color.Transparent;
            this.lblUserProfile.Font = new System.Drawing.Font("Segoe UI Emoji", 30F, System.Drawing.FontStyle.Bold);
            this.lblUserProfile.ForeColor = System.Drawing.Color.White;
            this.lblUserProfile.Location = new System.Drawing.Point(40, 12);
            this.lblUserProfile.Name = "lblUserProfile";
            this.lblUserProfile.Size = new System.Drawing.Size(79, 53);
            this.lblUserProfile.TabIndex = 21;
            this.lblUserProfile.Text = "👾";
            this.lblUserProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUserProfile.Click += new System.EventHandler(this.label6_Click);
            // 
            // lblUserNameLittle
            // 
            this.lblUserNameLittle.AutoSize = true;
            this.lblUserNameLittle.BackColor = System.Drawing.Color.Transparent;
            this.lblUserNameLittle.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNameLittle.ForeColor = System.Drawing.Color.White;
            this.lblUserNameLittle.Location = new System.Drawing.Point(0, 81);
            this.lblUserNameLittle.MinimumSize = new System.Drawing.Size(159, 0);
            this.lblUserNameLittle.Name = "lblUserNameLittle";
            this.lblUserNameLittle.Size = new System.Drawing.Size(159, 21);
            this.lblUserNameLittle.TabIndex = 22;
            this.lblUserNameLittle.Text = "name";
            this.lblUserNameLittle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserNameLittle.Click += new System.EventHandler(this.label7_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(131, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 21);
            this.label5.TabIndex = 20;
            this.label5.Text = "USER SETTINGS:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 21);
            this.label1.TabIndex = 14;
            this.label1.Text = "Games played in the last ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.BackColor = System.Drawing.Color.Red;
            this.btnDeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteUser.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.btnDeleteUser.ForeColor = System.Drawing.Color.White;
            this.btnDeleteUser.Location = new System.Drawing.Point(111, 386);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(169, 30);
            this.btnDeleteUser.TabIndex = 17;
            this.btnDeleteUser.Text = "Delete user";
            this.btnDeleteUser.UseVisualStyleBackColor = false;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.fondo_oscuro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1099, 511);
            this.Controls.Add(this.invitationinfo);
            this.Controls.Add(this.lblAvailableGames);
            this.Controls.Add(this.gamesGrid);
            this.Controls.Add(this.lblPlayersOnline);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.onlineGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBarraTitulo);
            this.Controls.Add(this.panelUsuario);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelBarraTitulo.ResumeLayout(false);
            this.panelBarraTitulo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.onlineGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamesGrid)).EndInit();
            this.panelUsuario.ResumeLayout(false);
            this.panelUsuario.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connection;
        private System.Windows.Forms.Button Disconnection;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Button Register;
        private System.Windows.Forms.Button LogIn;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelBarraTitulo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button minimizeBtn;
        private System.Windows.Forms.Button maximizeBtn;
        private System.Windows.Forms.Label indicadorConexion_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView onlineGrid;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtBuscarPartida;
        private System.Windows.Forms.Button btnJoinGame;
        private System.Windows.Forms.Button btnCreateGame;
        private System.Windows.Forms.Label lblBuscarPartida;
        private System.Windows.Forms.Label lblPlayersOnline;
        private System.Windows.Forms.Label lblAvailableGames;
        private System.Windows.Forms.DataGridView gamesGrid;
        private System.Windows.Forms.Button invitationinfo;
        private System.Windows.Forms.Panel panelUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUserProfile;
        private System.Windows.Forms.Label lblUserNameLittle;
        private System.Windows.Forms.Button btnChangeColor;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Playersplayed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox lblgameshours;
        private System.Windows.Forms.Button gamesinhours;
        private System.Windows.Forms.Label playerswith;
        private System.Windows.Forms.Label lblresphours;
        private System.Windows.Forms.Button resultados;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox resultadostex;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
    }
}


