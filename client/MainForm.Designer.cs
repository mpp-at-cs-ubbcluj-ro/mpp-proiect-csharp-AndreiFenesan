using System.ComponentModel;

namespace client;
using model;
partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;
    
    private BindingList<CharityCaseDto> _charityCaseDtos;
    private BindingList<DonorDto> _donors;

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
    private void initViews()
    {
        this._charityCaseDtos = new BindingList<CharityCaseDto>();
        this._donors = new BindingList<DonorDto>();
    }

    public void updateCharityCases()
    {
        List<CharityCaseDto> charityCaseDtos = this.Service.GetAllCharityCases();
        this._charityCaseDtos.Clear();
        foreach (var dto in charityCaseDtos)
        {
            this._charityCaseDtos.Add(dto);
        }
        // var source = new BindingSource(this._charityCaseDtos, null);
        this.dataGridView1.DataSource = _charityCaseDtos;

    }

    private void updateDonorsView(IEnumerator<DonorDto> donors)
    {
        this._donors.Clear();
        while (donors.MoveNext())
        {
            this._donors.Add(donors.Current);
        }

        this.dataGridView2.DataSource = _donors;
    }


    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.dataGridView2 = new System.Windows.Forms.DataGridView();
        // this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.nameBox = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.emailBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.phoneBox = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
        this.label6 = new System.Windows.Forms.Label();
        this.donationBtn = new System.Windows.Forms.Button();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridView2
        // 
        this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView2.Location = new System.Drawing.Point(439, 35);
        this.dataGridView2.Name = "dataGridView2";
        this.dataGridView2.Size = new System.Drawing.Size(325, 165);
        this.dataGridView2.TabIndex = 1;
        this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
        // 
        // dataGridTextBoxColumn1
        // 
        // this.dataGridTextBoxColumn1.Format = "";
        // this.dataGridTextBoxColumn1.FormatInfo = null;
        // this.dataGridTextBoxColumn1.Width = -1;
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(138, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(90, 23);
        this.label1.TabIndex = 2;
        this.label1.Text = "Charity cases";
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(590, 9);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(59, 23);
        this.label2.TabIndex = 3;
        this.label2.Text = "Donors";
        // 
        // nameBox
        // 
        this.nameBox.Location = new System.Drawing.Point(590, 235);
        this.nameBox.Name = "nameBox";
        this.nameBox.Size = new System.Drawing.Size(174, 20);
        this.nameBox.TabIndex = 4;
        this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
        // 
        // label3
        // 
        this.label3.Location = new System.Drawing.Point(490, 238);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(51, 19);
        this.label3.TabIndex = 5;
        this.label3.Text = "Name";
        // 
        // emailBox
        // 
        this.emailBox.Location = new System.Drawing.Point(590, 278);
        this.emailBox.Name = "emailBox";
        this.emailBox.Size = new System.Drawing.Size(174, 20);
        this.emailBox.TabIndex = 6;
        // 
        // label4
        // 
        this.label4.Location = new System.Drawing.Point(490, 281);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(51, 23);
        this.label4.TabIndex = 7;
        this.label4.Text = "Email";
        // 
        // phoneBox
        // 
        this.phoneBox.Location = new System.Drawing.Point(590, 312);
        this.phoneBox.Name = "phoneBox";
        this.phoneBox.Size = new System.Drawing.Size(174, 20);
        this.phoneBox.TabIndex = 8;
        // 
        // label5
        // 
        this.label5.Location = new System.Drawing.Point(476, 315);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(84, 23);
        this.label5.TabIndex = 9;
        this.label5.Text = "Phone number\r\n";
        // 
        // numericUpDown1
        // 
        this.numericUpDown1.Location = new System.Drawing.Point(590, 350);
        this.numericUpDown1.Name = "numericUpDown1";
        this.numericUpDown1.Size = new System.Drawing.Size(174, 20);
        this.numericUpDown1.TabIndex = 10;
        // 
        // label6
        // 
        this.label6.Location = new System.Drawing.Point(490, 347);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(65, 23);
        this.label6.TabIndex = 11;
        this.label6.Text = "amount";
        // 
        // donationBtn
        // 
        this.donationBtn.Location = new System.Drawing.Point(637, 394);
        this.donationBtn.Name = "donationBtn";
        this.donationBtn.Size = new System.Drawing.Size(82, 24);
        this.donationBtn.TabIndex = 12;
        this.donationBtn.Text = "add donation";
        this.donationBtn.UseVisualStyleBackColor = true;
        this.donationBtn.Click += new System.EventHandler(this.donationBtn_Click);
        // 
        // dataGridView1
        // 
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Location = new System.Drawing.Point(33, 35);
        this.dataGridView1.MultiSelect = false;
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.ReadOnly = true;
        this.dataGridView1.Size = new System.Drawing.Size(335, 165);
        this.dataGridView1.TabIndex = 13;
        // 
        // MainForm
        // 
        this.AllowDrop = true;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.dataGridView1);
        this.Controls.Add(this.donationBtn);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.numericUpDown1);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.phoneBox);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.emailBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.nameBox);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.dataGridView2);
        this.Name = "MainForm";
        this.Text = "MainForm";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.DataGridView dataGridView1;

    private System.Windows.Forms.NumericUpDown numericUpDown1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button donationBtn;

    private System.Windows.Forms.TextBox emailBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox phoneBox;
    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox nameBox;
    private System.Windows.Forms.Label label3;

    // private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;

    private System.Windows.Forms.DataGridView dataGridView2;


    #endregion
}