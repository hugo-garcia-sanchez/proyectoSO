namespace WindowsFormsApplication1
{
    partial class PartidaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartidaForm));
            this.label1 = new System.Windows.Forms.Label();
            this.sendbuttom = new System.Windows.Forms.Button();
            this.lblsend = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCHAT = new System.Windows.Forms.TextBox();
            this.panelBarraTitulo = new System.Windows.Forms.Panel();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.lblGameTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.robarboton = new System.Windows.Forms.Button();
            this.middlecard = new System.Windows.Forms.Button();
            this.fourcards = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelBarraTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "numForm: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // sendbuttom
            // 
            this.sendbuttom.AutoSize = true;
            this.sendbuttom.BackColor = System.Drawing.Color.Black;
            this.sendbuttom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sendbuttom.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.sendbuttom.ForeColor = System.Drawing.Color.White;
            this.sendbuttom.Location = new System.Drawing.Point(1124, 446);
            this.sendbuttom.Name = "sendbuttom";
            this.sendbuttom.Size = new System.Drawing.Size(64, 31);
            this.sendbuttom.TabIndex = 73;
            this.sendbuttom.Text = "SEND";
            this.sendbuttom.UseVisualStyleBackColor = false;
            this.sendbuttom.Click += new System.EventHandler(this.sendbuttom_Click);
            // 
            // lblsend
            // 
            this.lblsend.BackColor = System.Drawing.Color.Black;
            this.lblsend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblsend.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.lblsend.ForeColor = System.Drawing.Color.White;
            this.lblsend.Location = new System.Drawing.Point(975, 457);
            this.lblsend.Name = "lblsend";
            this.lblsend.Size = new System.Drawing.Size(143, 16);
            this.lblsend.TabIndex = 72;
            this.lblsend.TextChanged += new System.EventHandler(this.lblsend_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1021, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 21);
            this.label4.TabIndex = 71;
            this.label4.Text = "ONLINE CHAT";
            // 
            // textBoxCHAT
            // 
            this.textBoxCHAT.BackColor = System.Drawing.Color.Black;
            this.textBoxCHAT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCHAT.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.textBoxCHAT.ForeColor = System.Drawing.Color.White;
            this.textBoxCHAT.Location = new System.Drawing.Point(975, 86);
            this.textBoxCHAT.Multiline = true;
            this.textBoxCHAT.Name = "textBoxCHAT";
            this.textBoxCHAT.ReadOnly = true;
            this.textBoxCHAT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCHAT.Size = new System.Drawing.Size(212, 354);
            this.textBoxCHAT.TabIndex = 74;
            // 
            // panelBarraTitulo
            // 
            this.panelBarraTitulo.BackColor = System.Drawing.Color.SteelBlue;
            this.panelBarraTitulo.Controls.Add(this.maximizeBtn);
            this.panelBarraTitulo.Controls.Add(this.minimizeBtn);
            this.panelBarraTitulo.Controls.Add(this.lblGameTitle);
            this.panelBarraTitulo.Controls.Add(this.btnClose);
            this.panelBarraTitulo.Controls.Add(this.button18);
            this.panelBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelBarraTitulo.Margin = new System.Windows.Forms.Padding(2);
            this.panelBarraTitulo.Name = "panelBarraTitulo";
            this.panelBarraTitulo.Size = new System.Drawing.Size(1205, 37);
            this.panelBarraTitulo.TabIndex = 75;
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
            this.maximizeBtn.Location = new System.Drawing.Point(1139, 3);
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
            this.minimizeBtn.Location = new System.Drawing.Point(1101, 3);
            this.minimizeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(28, 28);
            this.minimizeBtn.TabIndex = 45;
            this.minimizeBtn.Text = "—";
            this.minimizeBtn.UseVisualStyleBackColor = false;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            // 
            // lblGameTitle
            // 
            this.lblGameTitle.AutoSize = true;
            this.lblGameTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblGameTitle.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameTitle.ForeColor = System.Drawing.Color.White;
            this.lblGameTitle.Location = new System.Drawing.Point(11, 8);
            this.lblGameTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGameTitle.Name = "lblGameTitle";
            this.lblGameTitle.Size = new System.Drawing.Size(44, 21);
            this.lblGameTitle.TabIndex = 8;
            this.lblGameTitle.Text = "Title";
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
            this.btnClose.Location = new System.Drawing.Point(1172, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 28);
            this.btnClose.TabIndex = 44;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.button18.Location = new System.Drawing.Point(1174, 5);
            this.button18.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(26, 28);
            this.button18.TabIndex = 5;
            this.button18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button18.UseVisualStyleBackColor = true;
            // 
            // robarboton
            // 
            this.robarboton.BackColor = System.Drawing.Color.Transparent;
            this.robarboton.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.momento;
            this.robarboton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.robarboton.FlatAppearance.BorderSize = 0;
            this.robarboton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.robarboton.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.robarboton.Location = new System.Drawing.Point(742, 62);
            this.robarboton.Name = "robarboton";
            this.robarboton.Size = new System.Drawing.Size(135, 168);
            this.robarboton.TabIndex = 87;
            this.robarboton.UseVisualStyleBackColor = false;
            this.robarboton.Click += new System.EventHandler(this.robarboton_Click);
            // 
            // middlecard
            // 
            this.middlecard.BackColor = System.Drawing.Color.Black;
            this.middlecard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.middlecard.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.middlecard.ForeColor = System.Drawing.Color.Chartreuse;
            this.middlecard.Location = new System.Drawing.Point(12, 62);
            this.middlecard.Name = "middlecard";
            this.middlecard.Size = new System.Drawing.Size(180, 32);
            this.middlecard.TabIndex = 82;
            this.middlecard.Text = "Start Game ▶";
            this.middlecard.UseVisualStyleBackColor = false;
            this.middlecard.Click += new System.EventHandler(this.middlecard_Click);
            // 
            // fourcards
            // 
            this.fourcards.BackColor = System.Drawing.Color.Black;
            this.fourcards.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.fourcards.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Bold);
            this.fourcards.ForeColor = System.Drawing.Color.White;
            this.fourcards.Location = new System.Drawing.Point(198, 62);
            this.fourcards.Name = "fourcards";
            this.fourcards.Size = new System.Drawing.Size(180, 32);
            this.fourcards.TabIndex = 81;
            this.fourcards.Text = "Give me cards";
            this.fourcards.UseVisualStyleBackColor = false;
            this.fourcards.Click += new System.EventHandler(this.fourcards_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Emoji", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(20, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 27);
            this.label2.TabIndex = 88;
            this.label2.Text = "label2";
            // 
            // PartidaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.fondo_oscuro;
            this.ClientSize = new System.Drawing.Size(1205, 527);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.robarboton);
            this.Controls.Add(this.middlecard);
            this.Controls.Add(this.fourcards);
            this.Controls.Add(this.panelBarraTitulo);
            this.Controls.Add(this.textBoxCHAT);
            this.Controls.Add(this.sendbuttom);
            this.Controls.Add(this.lblsend);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PartidaForm";
            this.Text = "PartidaForm";
            this.Load += new System.EventHandler(this.PartidaForm_Load);
            this.panelBarraTitulo.ResumeLayout(false);
            this.panelBarraTitulo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendbuttom;
        private System.Windows.Forms.TextBox lblsend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCHAT;
        private System.Windows.Forms.Panel panelBarraTitulo;
        private System.Windows.Forms.Button maximizeBtn;
        private System.Windows.Forms.Button minimizeBtn;
        private System.Windows.Forms.Label lblGameTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button robarboton;
        private System.Windows.Forms.Button middlecard;
        private System.Windows.Forms.Button fourcards;
        private System.Windows.Forms.Label label2;
    }
}