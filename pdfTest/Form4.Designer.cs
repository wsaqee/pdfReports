namespace pdfTest
{
    partial class Form4
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
            this.checkedListAttributes = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // checkedListAttributes
            // 
            this.checkedListAttributes.FormattingEnabled = true;
            this.checkedListAttributes.Location = new System.Drawing.Point(12, 12);
            this.checkedListAttributes.Name = "checkedListAttributes";
            this.checkedListAttributes.Size = new System.Drawing.Size(361, 409);
            this.checkedListAttributes.TabIndex = 0;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 448);
            this.Controls.Add(this.checkedListAttributes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form4";
            this.Text = "Atributi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListAttributes;
    }
}