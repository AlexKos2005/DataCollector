namespace DataCollector
{
    partial class PLCSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLCSettingsForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AutoConnectCheckBox = new System.Windows.Forms.CheckBox();
            this.IpTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.CheckButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.CycleTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DataCollector.Properties.Resources.Omron_PLC_Model_CP1L_M30DT_D;
            this.pictureBox1.Location = new System.Drawing.Point(23, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(144, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // AutoConnectCheckBox
            // 
            this.AutoConnectCheckBox.AutoSize = true;
            this.AutoConnectCheckBox.Location = new System.Drawing.Point(261, 31);
            this.AutoConnectCheckBox.Name = "AutoConnectCheckBox";
            this.AutoConnectCheckBox.Size = new System.Drawing.Size(180, 17);
            this.AutoConnectCheckBox.TabIndex = 1;
            this.AutoConnectCheckBox.Text = "Подключаться автоматически";
            this.AutoConnectCheckBox.UseVisualStyleBackColor = true;
            // 
            // IpTextBox
            // 
            this.IpTextBox.Location = new System.Drawing.Point(261, 82);
            this.IpTextBox.Name = "IpTextBox";
            this.IpTextBox.Size = new System.Drawing.Size(180, 20);
            this.IpTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ip-Адрес ПЛК:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Порт(9600):";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(261, 133);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(180, 20);
            this.PortTextBox.TabIndex = 4;
            // 
            // CheckButton
            // 
            this.CheckButton.Location = new System.Drawing.Point(55, 236);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(112, 40);
            this.CheckButton.TabIndex = 6;
            this.CheckButton.Text = "Проверить подключение";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(292, 236);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(132, 36);
            this.CloseButton.TabIndex = 42;
            this.CloseButton.Text = "Сохранить/Закрыть";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // CycleTextBox
            // 
            this.CycleTextBox.Location = new System.Drawing.Point(261, 187);
            this.CycleTextBox.Name = "CycleTextBox";
            this.CycleTextBox.Size = new System.Drawing.Size(180, 20);
            this.CycleTextBox.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Период запроса к ПЛК (сек):";
            // 
            // PLCSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(487, 284);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CycleTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IpTextBox);
            this.Controls.Add(this.AutoConnectCheckBox);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PLCSettingsForm";
            this.Text = "Настройки подключения к ПЛК";
            this.Load += new System.EventHandler(this.PLCSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox AutoConnectCheckBox;
        private System.Windows.Forms.TextBox IpTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TextBox CycleTextBox;
        private System.Windows.Forms.Label label3;
    }
}