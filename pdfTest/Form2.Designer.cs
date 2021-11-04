namespace pdfTest
{
    partial class Form2
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
            this.textBoxReportName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxReportName
            // 
            this.textBoxReportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.textBoxReportName.Location = new System.Drawing.Point(39, 62);
            this.textBoxReportName.Name = "textBoxReportName";
            this.textBoxReportName.Size = new System.Drawing.Size(309, 26);
            this.textBoxReportName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.Location = new System.Drawing.Point(34, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Naziv izvještaja";
            // 
            // buttonAck
            // 
            this.buttonAck.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAck.Location = new System.Drawing.Point(366, 57);
            this.buttonAck.Name = "buttonAck";
            this.buttonAck.Size = new System.Drawing.Size(59, 39);
            this.buttonAck.TabIndex = 2;
            this.buttonAck.Text = "Potvrdi";
            this.buttonAck.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AcceptButton = this.buttonAck;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 140);
            this.Controls.Add(this.buttonAck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxReportName);
            this.MaximumSize = new System.Drawing.Size(473, 179);
            this.MinimumSize = new System.Drawing.Size(473, 179);
            this.Name = "Form2";
            this.Text = "Autorizacija korisnika-Izvjestaj";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxReportName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAck;
    }
}