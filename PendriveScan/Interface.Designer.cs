namespace PendriveScan
{
    partial class Interface
    {

        private System.ComponentModel.IContainer components = null;

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
            this.Lista = new System.Windows.Forms.RichTextBox();
            this.btiniciar = new System.Windows.Forms.Button();
            this.List_Drivers = new System.Windows.Forms.ComboBox();
            this.ScanProgress = new System.Windows.Forms.ProgressBar();
            this.btatualizar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nm_folder = new System.Windows.Forms.TextBox();
            this.bt_sobre = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lista
            // 
            this.Lista.Location = new System.Drawing.Point(12, 40);
            this.Lista.Name = "Lista";
            this.Lista.ReadOnly = true;
            this.Lista.Size = new System.Drawing.Size(402, 291);
            this.Lista.TabIndex = 0;
            this.Lista.Text = "";
            // 
            // btiniciar
            // 
            this.btiniciar.Enabled = false;
            this.btiniciar.Location = new System.Drawing.Point(12, 422);
            this.btiniciar.Name = "btiniciar";
            this.btiniciar.Size = new System.Drawing.Size(402, 42);
            this.btiniciar.TabIndex = 1;
            this.btiniciar.Text = "Iniciar";
            this.btiniciar.UseVisualStyleBackColor = true;
            this.btiniciar.Click += new System.EventHandler(this.button1_Click);
            // 
            // List_Drivers
            // 
            this.List_Drivers.FormattingEnabled = true;
            this.List_Drivers.Location = new System.Drawing.Point(14, 10);
            this.List_Drivers.Name = "List_Drivers";
            this.List_Drivers.Size = new System.Drawing.Size(249, 24);
            this.List_Drivers.TabIndex = 2;
            // 
            // ScanProgress
            // 
            this.ScanProgress.Location = new System.Drawing.Point(14, 388);
            this.ScanProgress.Name = "ScanProgress";
            this.ScanProgress.Size = new System.Drawing.Size(400, 23);
            this.ScanProgress.Step = 1;
            this.ScanProgress.TabIndex = 3;
            // 
            // btatualizar
            // 
            this.btatualizar.Location = new System.Drawing.Point(269, 10);
            this.btatualizar.Name = "btatualizar";
            this.btatualizar.Size = new System.Drawing.Size(110, 24);
            this.btatualizar.TabIndex = 4;
            this.btatualizar.Text = "Atualizar";
            this.btatualizar.UseVisualStyleBackColor = true;
            this.btatualizar.Click += new System.EventHandler(this.btatualizar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nome da pasta protegida:";
            // 
            // nm_folder
            // 
            this.nm_folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nm_folder.Location = new System.Drawing.Point(213, 348);
            this.nm_folder.Name = "nm_folder";
            this.nm_folder.Size = new System.Drawing.Size(201, 26);
            this.nm_folder.TabIndex = 6;
            this.nm_folder.Text = "Arquivos";
            this.nm_folder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bt_sobre
            // 
            this.bt_sobre.Location = new System.Drawing.Point(385, 10);
            this.bt_sobre.Name = "bt_sobre";
            this.bt_sobre.Size = new System.Drawing.Size(30, 23);
            this.bt_sobre.TabIndex = 7;
            this.bt_sobre.Text = "?";
            this.bt_sobre.UseVisualStyleBackColor = true;
            this.bt_sobre.Click += new System.EventHandler(this.bt_sobre_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(427, 472);
            this.Controls.Add(this.bt_sobre);
            this.Controls.Add(this.nm_folder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btatualizar);
            this.Controls.Add(this.ScanProgress);
            this.Controls.Add(this.List_Drivers);
            this.Controls.Add(this.btiniciar);
            this.Controls.Add(this.Lista);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Pendrive Scan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Lista;
        private System.Windows.Forms.Button btiniciar;
        private System.Windows.Forms.ComboBox List_Drivers;
        private System.Windows.Forms.ProgressBar ScanProgress;
        private System.Windows.Forms.Button btatualizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nm_folder;
        private System.Windows.Forms.Button bt_sobre;
    }
}

