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
            this.Desconnection = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.Register = new System.Windows.Forms.Button();
            this.LogIn = new System.Windows.Forms.Button();
            this.NotiGreen = new System.Windows.Forms.Button();
            this.NotiRed = new System.Windows.Forms.Button();
            this.NotiBlue = new System.Windows.Forms.Button();
            this.NotiYellow = new System.Windows.Forms.Button();
            this.cardlbl = new System.Windows.Forms.Label();
            this.howitworks = new System.Windows.Forms.Button();
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
            this.buttoncarta2 = new System.Windows.Forms.Button();
            this.buttoncarta1 = new System.Windows.Forms.Button();
            this.buttoncarta3 = new System.Windows.Forms.Button();
            this.buttoncarta4 = new System.Windows.Forms.Button();
            this.buttoncartamedio = new System.Windows.Forms.Button();
            this.fourcards = new System.Windows.Forms.Button();
            this.middlecard = new System.Windows.Forms.Button();
            this.card1 = new System.Windows.Forms.Button();
            this.card2 = new System.Windows.Forms.Button();
            this.card3 = new System.Windows.Forms.Button();
            this.card4 = new System.Windows.Forms.Button();
            this.panelBarraTitulo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.onlineGrid)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamesGrid)).BeginInit();
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
            // Desconnection
            // 
            this.Desconnection.BackColor = System.Drawing.Color.Black;
            this.Desconnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Desconnection.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.Desconnection.ForeColor = System.Drawing.Color.White;
            this.Desconnection.Location = new System.Drawing.Point(9, 43);
            this.Desconnection.Name = "Desconnection";
            this.Desconnection.Size = new System.Drawing.Size(169, 27);
            this.Desconnection.TabIndex = 10;
            this.Desconnection.Text = "Desconnection";
            this.Desconnection.UseVisualStyleBackColor = false;
            this.Desconnection.Click += new System.EventHandler(this.Desconnection_Click);
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
            // NotiGreen
            // 
            this.NotiGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.NotiGreen.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotiGreen.Location = new System.Drawing.Point(35, 460);
            this.NotiGreen.Name = "NotiGreen";
            this.NotiGreen.Size = new System.Drawing.Size(82, 110);
            this.NotiGreen.TabIndex = 24;
            this.NotiGreen.Text = "G";
            this.NotiGreen.UseVisualStyleBackColor = false;
            this.NotiGreen.Click += new System.EventHandler(this.NotiGreen_Click);
            // 
            // NotiRed
            // 
            this.NotiRed.BackColor = System.Drawing.Color.Red;
            this.NotiRed.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotiRed.Location = new System.Drawing.Point(136, 460);
            this.NotiRed.Name = "NotiRed";
            this.NotiRed.Size = new System.Drawing.Size(82, 110);
            this.NotiRed.TabIndex = 25;
            this.NotiRed.Text = "R";
            this.NotiRed.UseVisualStyleBackColor = false;
            this.NotiRed.Click += new System.EventHandler(this.NotiRed_Click);
            // 
            // NotiBlue
            // 
            this.NotiBlue.BackColor = System.Drawing.Color.Blue;
            this.NotiBlue.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotiBlue.Location = new System.Drawing.Point(35, 592);
            this.NotiBlue.Name = "NotiBlue";
            this.NotiBlue.Size = new System.Drawing.Size(82, 110);
            this.NotiBlue.TabIndex = 26;
            this.NotiBlue.Text = "B";
            this.NotiBlue.UseVisualStyleBackColor = false;
            this.NotiBlue.Click += new System.EventHandler(this.NotiBlue_Click);
            // 
            // NotiYellow
            // 
            this.NotiYellow.BackColor = System.Drawing.Color.Yellow;
            this.NotiYellow.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotiYellow.Location = new System.Drawing.Point(136, 592);
            this.NotiYellow.Name = "NotiYellow";
            this.NotiYellow.Size = new System.Drawing.Size(82, 110);
            this.NotiYellow.TabIndex = 27;
            this.NotiYellow.Text = "Y";
            this.NotiYellow.UseVisualStyleBackColor = false;
            this.NotiYellow.Click += new System.EventHandler(this.NotiYellow_Click);
            // 
            // cardlbl
            // 
            this.cardlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardlbl.Location = new System.Drawing.Point(250, 540);
            this.cardlbl.Name = "cardlbl";
            this.cardlbl.Size = new System.Drawing.Size(190, 90);
            this.cardlbl.TabIndex = 29;
            // 
            // howitworks
            // 
            this.howitworks.BackColor = System.Drawing.Color.Black;
            this.howitworks.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.howitworks.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.howitworks.ForeColor = System.Drawing.Color.White;
            this.howitworks.Location = new System.Drawing.Point(250, 489);
            this.howitworks.Name = "howitworks";
            this.howitworks.Size = new System.Drawing.Size(190, 35);
            this.howitworks.TabIndex = 31;
            this.howitworks.Text = "How does it work?";
            this.howitworks.UseVisualStyleBackColor = false;
            this.howitworks.Click += new System.EventHandler(this.howitworks_Click);
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
            this.btnClose.Location = new System.Drawing.Point(1076, 3);
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
            this.panelBarraTitulo.Size = new System.Drawing.Size(1109, 37);
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
            this.maximizeBtn.Location = new System.Drawing.Point(1043, 3);
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
            this.minimizeBtn.Location = new System.Drawing.Point(1005, 3);
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
            this.lblTitle.Size = new System.Drawing.Size(321, 21);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Welcome to UNO Game! Log in to play!";
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
            this.button18.Location = new System.Drawing.Point(1078, 5);
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
            this.panel1.Location = new System.Drawing.Point(12, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 143);
            this.panel1.TabIndex = 48;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateGray;
            this.panel2.Controls.Add(this.Connection);
            this.panel2.Controls.Add(this.indicadorConexion_label);
            this.panel2.Controls.Add(this.Desconnection);
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
            this.onlineGrid.RowHeadersWidth = 51;
            this.onlineGrid.Size = new System.Drawing.Size(166, 195);
            this.onlineGrid.TabIndex = 51;
            this.onlineGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.onlineGrid_CellContentClick);
            this.onlineGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.onlineGrid_CellContentDoubleClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SlateGray;
            this.panel3.Controls.Add(this.txtBuscarPartida);
            this.panel3.Controls.Add(this.btnJoinGame);
            this.panel3.Controls.Add(this.btnCreateGame);
            this.panel3.Controls.Add(this.lblBuscarPartida);
            this.panel3.Location = new System.Drawing.Point(12, 296);
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
            this.lblPlayersOnline.Location = new System.Drawing.Point(929, 62);
            this.lblPlayersOnline.Name = "lblPlayersOnline";
            this.lblPlayersOnline.Size = new System.Drawing.Size(128, 21);
            this.lblPlayersOnline.TabIndex = 20;
            this.lblPlayersOnline.Text = "Players Online:";
            this.lblPlayersOnline.Click += new System.EventHandler(this.lblPlayersOnline_Click);
            // 
            // lblAvailableGames
            // 
            this.lblAvailableGames.AutoSize = true;
            this.lblAvailableGames.BackColor = System.Drawing.Color.Transparent;
            this.lblAvailableGames.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableGames.ForeColor = System.Drawing.Color.White;
            this.lblAvailableGames.Location = new System.Drawing.Point(929, 308);
            this.lblAvailableGames.Name = "lblAvailableGames";
            this.lblAvailableGames.Size = new System.Drawing.Size(144, 21);
            this.lblAvailableGames.TabIndex = 53;
            this.lblAvailableGames.Text = "Available Games:";
            // 
            // gamesGrid
            // 
            this.gamesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gamesGrid.Location = new System.Drawing.Point(922, 332);
            this.gamesGrid.Name = "gamesGrid";
            this.gamesGrid.RowHeadersWidth = 51;
            this.gamesGrid.Size = new System.Drawing.Size(166, 169);
            this.gamesGrid.TabIndex = 54;
            // 
            // buttoncarta2
            // 
            this.buttoncarta2.BackColor = System.Drawing.Color.Gray;
            this.buttoncarta2.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncarta2.Location = new System.Drawing.Point(464, 296);
            this.buttoncarta2.Name = "buttoncarta2";
            this.buttoncarta2.Size = new System.Drawing.Size(82, 110);
            this.buttoncarta2.TabIndex = 55;
            this.buttoncarta2.Text = "UNO";
            this.buttoncarta2.UseVisualStyleBackColor = false;
            this.buttoncarta2.Click += new System.EventHandler(this.buttoncarta2_Click);
            // 
            // buttoncarta1
            // 
            this.buttoncarta1.BackColor = System.Drawing.Color.Gray;
            this.buttoncarta1.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncarta1.Location = new System.Drawing.Point(361, 296);
            this.buttoncarta1.Name = "buttoncarta1";
            this.buttoncarta1.Size = new System.Drawing.Size(82, 110);
            this.buttoncarta1.TabIndex = 56;
            this.buttoncarta1.Text = "UNO";
            this.buttoncarta1.UseVisualStyleBackColor = false;
            // 
            // buttoncarta3
            // 
            this.buttoncarta3.BackColor = System.Drawing.Color.Gray;
            this.buttoncarta3.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncarta3.Location = new System.Drawing.Point(569, 296);
            this.buttoncarta3.Name = "buttoncarta3";
            this.buttoncarta3.Size = new System.Drawing.Size(82, 110);
            this.buttoncarta3.TabIndex = 57;
            this.buttoncarta3.Text = "UNO";
            this.buttoncarta3.UseVisualStyleBackColor = false;
            // 
            // buttoncarta4
            // 
            this.buttoncarta4.BackColor = System.Drawing.Color.Gray;
            this.buttoncarta4.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncarta4.Location = new System.Drawing.Point(671, 296);
            this.buttoncarta4.Name = "buttoncarta4";
            this.buttoncarta4.Size = new System.Drawing.Size(82, 110);
            this.buttoncarta4.TabIndex = 58;
            this.buttoncarta4.Text = "UNO";
            this.buttoncarta4.UseVisualStyleBackColor = false;
            this.buttoncarta4.Click += new System.EventHandler(this.buttoncarta4_Click);
            // 
            // buttoncartamedio
            // 
            this.buttoncartamedio.BackColor = System.Drawing.Color.Gray;
            this.buttoncartamedio.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncartamedio.Location = new System.Drawing.Point(512, 162);
            this.buttoncartamedio.Name = "buttoncartamedio";
            this.buttoncartamedio.Size = new System.Drawing.Size(82, 110);
            this.buttoncartamedio.TabIndex = 59;
            this.buttoncartamedio.Text = "UNO";
            this.buttoncartamedio.UseVisualStyleBackColor = false;
            this.buttoncartamedio.Click += new System.EventHandler(this.buttoncartamedio_Click);
            // 
            // fourcards
            // 
            this.fourcards.BackColor = System.Drawing.Color.Black;
            this.fourcards.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.fourcards.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.fourcards.ForeColor = System.Drawing.Color.White;
            this.fourcards.Location = new System.Drawing.Point(695, 163);
            this.fourcards.Name = "fourcards";
            this.fourcards.Size = new System.Drawing.Size(180, 28);
            this.fourcards.TabIndex = 60;
            this.fourcards.Text = "4 cards";
            this.fourcards.UseVisualStyleBackColor = false;
            // 
            // middlecard
            // 
            this.middlecard.BackColor = System.Drawing.Color.Black;
            this.middlecard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.middlecard.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.middlecard.ForeColor = System.Drawing.Color.White;
            this.middlecard.Location = new System.Drawing.Point(695, 203);
            this.middlecard.Name = "middlecard";
            this.middlecard.Size = new System.Drawing.Size(180, 32);
            this.middlecard.TabIndex = 61;
            this.middlecard.Text = "Middle Card";
            this.middlecard.UseVisualStyleBackColor = false;
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.Black;
            this.card1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.card1.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.card1.ForeColor = System.Drawing.Color.White;
            this.card1.Location = new System.Drawing.Point(359, 423);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(84, 32);
            this.card1.TabIndex = 62;
            this.card1.Text = "card 1";
            this.card1.UseVisualStyleBackColor = false;
            this.card1.Click += new System.EventHandler(this.card1_Click);
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.Black;
            this.card2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.card2.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.card2.ForeColor = System.Drawing.Color.White;
            this.card2.Location = new System.Drawing.Point(464, 423);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(84, 32);
            this.card2.TabIndex = 63;
            this.card2.Text = "card 2";
            this.card2.UseVisualStyleBackColor = false;
            this.card2.Click += new System.EventHandler(this.card2_Click);
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.Black;
            this.card3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.card3.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.card3.ForeColor = System.Drawing.Color.White;
            this.card3.Location = new System.Drawing.Point(567, 423);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(84, 32);
            this.card3.TabIndex = 64;
            this.card3.Text = "card 3";
            this.card3.UseVisualStyleBackColor = false;
            //this.card3.Click += new System.EventHandler(this.card3_Click);
            // 
            // card4
            // 
            this.card4.BackColor = System.Drawing.Color.Black;
            this.card4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.card4.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.card4.ForeColor = System.Drawing.Color.White;
            this.card4.Location = new System.Drawing.Point(669, 423);
            this.card4.Name = "card4";
            this.card4.Size = new System.Drawing.Size(84, 32);
            this.card4.TabIndex = 65;
            this.card4.Text = "card 4";
            this.card4.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.fondo_oscuro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1109, 640);
            this.Controls.Add(this.card4);
            this.Controls.Add(this.card3);
            this.Controls.Add(this.card2);
            this.Controls.Add(this.card1);
            this.Controls.Add(this.middlecard);
            this.Controls.Add(this.fourcards);
            this.Controls.Add(this.buttoncartamedio);
            this.Controls.Add(this.buttoncarta4);
            this.Controls.Add(this.buttoncarta3);
            this.Controls.Add(this.buttoncarta1);
            this.Controls.Add(this.buttoncarta2);
            this.Controls.Add(this.lblAvailableGames);
            this.Controls.Add(this.gamesGrid);
            this.Controls.Add(this.lblPlayersOnline);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.onlineGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBarraTitulo);
            this.Controls.Add(this.howitworks);
            this.Controls.Add(this.cardlbl);
            this.Controls.Add(this.NotiYellow);
            this.Controls.Add(this.NotiBlue);
            this.Controls.Add(this.NotiRed);
            this.Controls.Add(this.NotiGreen);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connection;
        private System.Windows.Forms.Button Desconnection;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Button Register;
        private System.Windows.Forms.Button LogIn;
        private System.Windows.Forms.Button NotiGreen;
        private System.Windows.Forms.Button NotiRed;
        private System.Windows.Forms.Button NotiBlue;
        private System.Windows.Forms.Button NotiYellow;
        private System.Windows.Forms.Label cardlbl;
        private System.Windows.Forms.Button howitworks;
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
        private System.Windows.Forms.Button buttoncarta2;
        private System.Windows.Forms.Button buttoncarta1;
        private System.Windows.Forms.Button buttoncarta3;
        private System.Windows.Forms.Button buttoncarta4;
        private System.Windows.Forms.Button buttoncartamedio;
        private System.Windows.Forms.Button fourcards;
        private System.Windows.Forms.Button middlecard;
        private System.Windows.Forms.Button card1;
        private System.Windows.Forms.Button card2;
        private System.Windows.Forms.Button card3;
        private System.Windows.Forms.Button card4;
    }
}


