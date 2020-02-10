namespace MultimeterMonitor
{
    partial class monitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(monitor));
            this.monitorBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // monitorBox
            // 
            this.monitorBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.monitorBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monitorBox.ForeColor = System.Drawing.Color.Lime;
            this.monitorBox.Location = new System.Drawing.Point(0, 0);
            this.monitorBox.Name = "monitorBox";
            this.monitorBox.ReadOnly = true;
            this.monitorBox.Size = new System.Drawing.Size(800, 450);
            this.monitorBox.TabIndex = 0;
            this.monitorBox.Text = "";
            // 
            // monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.monitorBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "monitor";
            this.Text = "Text monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.monitor_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox monitorBox;
    }
}