namespace Knjizara_Milance_NRT820
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label autorLabel;
            System.Windows.Forms.Label nazivLabel;
            System.Windows.Forms.Label cenaLabel;
            System.Windows.Forms.Label popustLabel;
            System.Windows.Forms.Label broj_stranaLabel;
            System.Windows.Forms.Label label1;
            this.knjizaraDataSet = new Knjizara_Milance_NRT820.KnjizaraDataSet();
            this.knjigaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.knjigaTableAdapter = new Knjizara_Milance_NRT820.KnjizaraDataSetTableAdapters.KnjigaTableAdapter();
            this.tableAdapterManager = new Knjizara_Milance_NRT820.KnjizaraDataSetTableAdapters.TableAdapterManager();
            this.zanrTableAdapter = new Knjizara_Milance_NRT820.KnjizaraDataSetTableAdapters.ZanrTableAdapter();
            this.zanrBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            autorLabel = new System.Windows.Forms.Label();
            nazivLabel = new System.Windows.Forms.Label();
            cenaLabel = new System.Windows.Forms.Label();
            popustLabel = new System.Windows.Forms.Label();
            broj_stranaLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.knjizaraDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.knjigaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zanrBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // autorLabel
            // 
            autorLabel.AutoSize = true;
            autorLabel.Location = new System.Drawing.Point(26, 34);
            autorLabel.Name = "autorLabel";
            autorLabel.Size = new System.Drawing.Size(35, 13);
            autorLabel.TabIndex = 3;
            autorLabel.Text = "Autor:";
            // 
            // nazivLabel
            // 
            nazivLabel.AutoSize = true;
            nazivLabel.Location = new System.Drawing.Point(26, 60);
            nazivLabel.Name = "nazivLabel";
            nazivLabel.Size = new System.Drawing.Size(37, 13);
            nazivLabel.TabIndex = 5;
            nazivLabel.Text = "Naziv:";
            // 
            // cenaLabel
            // 
            cenaLabel.AutoSize = true;
            cenaLabel.Location = new System.Drawing.Point(26, 86);
            cenaLabel.Name = "cenaLabel";
            cenaLabel.Size = new System.Drawing.Size(35, 13);
            cenaLabel.TabIndex = 7;
            cenaLabel.Text = "Cena:";
            // 
            // popustLabel
            // 
            popustLabel.AutoSize = true;
            popustLabel.Location = new System.Drawing.Point(26, 112);
            popustLabel.Name = "popustLabel";
            popustLabel.Size = new System.Drawing.Size(43, 13);
            popustLabel.TabIndex = 9;
            popustLabel.Text = "Popust:";
            // 
            // broj_stranaLabel
            // 
            broj_stranaLabel.AutoSize = true;
            broj_stranaLabel.Location = new System.Drawing.Point(26, 138);
            broj_stranaLabel.Name = "broj_stranaLabel";
            broj_stranaLabel.Size = new System.Drawing.Size(60, 13);
            broj_stranaLabel.TabIndex = 11;
            broj_stranaLabel.Text = "Broj strana:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(26, 167);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(91, 13);
            label1.TabIndex = 14;
            label1.Text = "Izaberite zanrove:";
            // 
            // knjizaraDataSet
            // 
            this.knjizaraDataSet.DataSetName = "KnjizaraDataSet";
            this.knjizaraDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // knjigaBindingSource
            // 
            this.knjigaBindingSource.DataMember = "Knjiga";
            this.knjigaBindingSource.DataSource = this.knjizaraDataSet;
            // 
            // knjigaTableAdapter
            // 
            this.knjigaTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.KnjigaTableAdapter = this.knjigaTableAdapter;
            this.tableAdapterManager.PripadnostTableAdapter = null;
            this.tableAdapterManager.RacunTableAdapter = null;
            this.tableAdapterManager.Stavka_racunaTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = Knjizara_Milance_NRT820.KnjizaraDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.ZanrTableAdapter = this.zanrTableAdapter;
            // 
            // zanrTableAdapter
            // 
            this.zanrTableAdapter.ClearBeforeFill = true;
            // 
            // zanrBindingSource
            // 
            this.zanrBindingSource.DataMember = "Zanr";
            this.zanrBindingSource.DataSource = this.knjizaraDataSet;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(29, 183);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(244, 95);
            this.listBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(91, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 25);
            this.button1.TabIndex = 7;
            this.button1.Text = "Dodaj knjigu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(91, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(91, 57);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 20);
            this.textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(91, 83);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(182, 20);
            this.textBox3.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(91, 109);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(182, 20);
            this.textBox4.TabIndex = 4;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(91, 135);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(182, 20);
            this.textBox5.TabIndex = 5;
            // 
            // Form3
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 346);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(autorLabel);
            this.Controls.Add(nazivLabel);
            this.Controls.Add(cenaLabel);
            this.Controls.Add(popustLabel);
            this.Controls.Add(broj_stranaLabel);
            this.Name = "Form3";
            this.Text = "Dodaj knjigu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.knjizaraDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.knjigaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zanrBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KnjizaraDataSet knjizaraDataSet;
        private System.Windows.Forms.BindingSource knjigaBindingSource;
        private KnjizaraDataSetTableAdapters.KnjigaTableAdapter knjigaTableAdapter;
        private KnjizaraDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private KnjizaraDataSetTableAdapters.ZanrTableAdapter zanrTableAdapter;
        private System.Windows.Forms.BindingSource zanrBindingSource;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
    }
}