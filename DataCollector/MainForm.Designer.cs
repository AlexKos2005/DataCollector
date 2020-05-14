namespace DataCollector
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DateStartTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DateEndTimePicker = new System.Windows.Forms.DateTimePicker();
            this.MakeReportButton = new System.Windows.Forms.Button();
            this.ErrorPLCLabel = new System.Windows.Forms.Label();
            this.ErrorSQLLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectToPLCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DateStartTimePicker
            // 
            this.DateStartTimePicker.Location = new System.Drawing.Point(12, 49);
            this.DateStartTimePicker.Name = "DateStartTimePicker";
            this.DateStartTimePicker.Size = new System.Drawing.Size(200, 20);
            this.DateStartTimePicker.TabIndex = 0;
            // 
            // DateEndTimePicker
            // 
            this.DateEndTimePicker.Location = new System.Drawing.Point(15, 95);
            this.DateEndTimePicker.Name = "DateEndTimePicker";
            this.DateEndTimePicker.Size = new System.Drawing.Size(200, 20);
            this.DateEndTimePicker.TabIndex = 1;
            // 
            // MakeReportButton
            // 
            this.MakeReportButton.Location = new System.Drawing.Point(38, 141);
            this.MakeReportButton.Name = "MakeReportButton";
            this.MakeReportButton.Size = new System.Drawing.Size(151, 38);
            this.MakeReportButton.TabIndex = 2;
            this.MakeReportButton.Text = "Сформировать отчет";
            this.MakeReportButton.UseVisualStyleBackColor = true;
            this.MakeReportButton.Click += new System.EventHandler(this.MakeReportButton_Click);
            // 
            // ErrorPLCLabel
            // 
            this.ErrorPLCLabel.AutoSize = true;
            this.ErrorPLCLabel.Location = new System.Drawing.Point(12, 203);
            this.ErrorPLCLabel.Name = "ErrorPLCLabel";
            this.ErrorPLCLabel.Size = new System.Drawing.Size(35, 13);
            this.ErrorPLCLabel.TabIndex = 4;
            this.ErrorPLCLabel.Text = "label1";
            // 
            // ErrorSQLLabel
            // 
            this.ErrorSQLLabel.AutoSize = true;
            this.ErrorSQLLabel.Location = new System.Drawing.Point(12, 245);
            this.ErrorSQLLabel.Name = "ErrorSQLLabel";
            this.ErrorSQLLabel.Size = new System.Drawing.Size(35, 13);
            this.ErrorSQLLabel.TabIndex = 5;
            this.ErrorSQLLabel.Text = "label1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(228, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectToPLCToolStripMenuItem,
            this.ConnectDbToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // ConnectToPLCToolStripMenuItem
            // 
            this.ConnectToPLCToolStripMenuItem.Name = "ConnectToPLCToolStripMenuItem";
            this.ConnectToPLCToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ConnectToPLCToolStripMenuItem.Text = "Подключение к ПЛК";
            this.ConnectToPLCToolStripMenuItem.Click += new System.EventHandler(this.ConnectToPLCToolStripMenuItem_Click);
            // 
            // ConnectDbToolStripMenuItem
            // 
            this.ConnectDbToolStripMenuItem.Name = "ConnectDbToolStripMenuItem";
            this.ConnectDbToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ConnectDbToolStripMenuItem.Text = "Подключение к БД";
            this.ConnectDbToolStripMenuItem.Click += new System.EventHandler(this.ConnectDbToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Начальная дата:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Конечная дата:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 274);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ErrorSQLLabel);
            this.Controls.Add(this.ErrorPLCLabel);
            this.Controls.Add(this.MakeReportButton);
            this.Controls.Add(this.DateEndTimePicker);
            this.Controls.Add(this.DateStartTimePicker);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "DataCollector.RT";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DateStartTimePicker;
        private System.Windows.Forms.DateTimePicker DateEndTimePicker;
        private System.Windows.Forms.Button MakeReportButton;
        private System.Windows.Forms.Label ErrorPLCLabel;
        private System.Windows.Forms.Label ErrorSQLLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConnectToPLCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConnectDbToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

