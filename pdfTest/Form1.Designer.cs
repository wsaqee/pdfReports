namespace pdfTest
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSearchRes = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1Search = new System.Windows.Forms.Button();
            this.labelDomain = new System.Windows.Forms.Label();
            this.comboBoxDomains = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.labelSelectedGroups = new System.Windows.Forms.Label();
            this.buttonRemoveItem = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelSearchRes);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.button1Search);
            this.groupBox1.Controls.Add(this.labelDomain);
            this.groupBox1.Controls.Add(this.comboBoxDomains);
            this.groupBox1.Controls.Add(this.textBoxSearch);
            this.groupBox1.Controls.Add(this.labelSearch);
            this.groupBox1.Location = new System.Drawing.Point(32, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 188);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tražilica";
            // 
            // labelSearchRes
            // 
            this.labelSearchRes.AutoSize = true;
            this.labelSearchRes.Location = new System.Drawing.Point(15, 93);
            this.labelSearchRes.Name = "labelSearchRes";
            this.labelSearchRes.Size = new System.Drawing.Size(93, 13);
            this.labelSearchRes.TabIndex = 16;
            this.labelSearchRes.Text = "Rezultati pretrage:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(507, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 42);
            this.button1.TabIndex = 15;
            this.button1.Text = "Dodaj u izvještaj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(114, 83);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(355, 69);
            this.listBox1.TabIndex = 14;
            // 
            // button1Search
            // 
            this.button1Search.Location = new System.Drawing.Point(507, 36);
            this.button1Search.Name = "button1Search";
            this.button1Search.Size = new System.Drawing.Size(75, 23);
            this.button1Search.TabIndex = 13;
            this.button1Search.Text = "Pretraži";
            this.button1Search.UseVisualStyleBackColor = true;
            this.button1Search.Click += new System.EventHandler(this.button1Search_Click);
            // 
            // labelDomain
            // 
            this.labelDomain.AutoSize = true;
            this.labelDomain.Location = new System.Drawing.Point(292, 41);
            this.labelDomain.Name = "labelDomain";
            this.labelDomain.Size = new System.Drawing.Size(50, 13);
            this.labelDomain.TabIndex = 12;
            this.labelDomain.Text = "Domena:";
            // 
            // comboBoxDomains
            // 
            this.comboBoxDomains.FormattingEnabled = true;
            this.comboBoxDomains.Items.AddRange(new object[] {
            "Local Machine"});
            this.comboBoxDomains.Location = new System.Drawing.Point(348, 38);
            this.comboBoxDomains.Name = "comboBoxDomains";
            this.comboBoxDomains.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDomains.TabIndex = 11;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(114, 38);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(172, 20);
            this.textBoxSearch.TabIndex = 10;
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(73, 41);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(39, 13);
            this.labelSearch.TabIndex = 9;
            this.labelSearch.Text = "Grupa:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonRemoveAll);
            this.groupBox2.Controls.Add(this.labelSelectedGroups);
            this.groupBox2.Controls.Add(this.buttonRemoveItem);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Controls.Add(this.buttonPrint);
            this.groupBox2.Location = new System.Drawing.Point(32, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(597, 188);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Izvještaj";
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Location = new System.Drawing.Point(507, 135);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(75, 33);
            this.buttonRemoveAll.TabIndex = 17;
            this.buttonRemoveAll.Text = "Ukloni sve";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // labelSelectedGroups
            // 
            this.labelSelectedGroups.AutoSize = true;
            this.labelSelectedGroups.Location = new System.Drawing.Point(24, 41);
            this.labelSelectedGroups.Name = "labelSelectedGroups";
            this.labelSelectedGroups.Size = new System.Drawing.Size(84, 13);
            this.labelSelectedGroups.TabIndex = 16;
            this.labelSelectedGroups.Text = "Odabrane grupe";
            // 
            // buttonRemoveItem
            // 
            this.buttonRemoveItem.Location = new System.Drawing.Point(507, 92);
            this.buttonRemoveItem.Name = "buttonRemoveItem";
            this.buttonRemoveItem.Size = new System.Drawing.Size(75, 33);
            this.buttonRemoveItem.TabIndex = 15;
            this.buttonRemoveItem.Text = "Ukloni redak";
            this.buttonRemoveItem.UseVisualStyleBackColor = true;
            this.buttonRemoveItem.Click += new System.EventHandler(this.buttonRemoveItem_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(114, 37);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(355, 134);
            this.listBox2.Sorted = true;
            this.listBox2.TabIndex = 14;
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(507, 41);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(75, 40);
            this.buttonPrint.TabIndex = 13;
            this.buttonPrint.Text = "Generiraj izvještaj";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.button1Search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 475);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelSearchRes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1Search;
        private System.Windows.Forms.Label labelDomain;
        private System.Windows.Forms.ComboBox comboBoxDomains;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Label labelSelectedGroups;
        private System.Windows.Forms.Button buttonRemoveItem;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button buttonPrint;
    }
}

