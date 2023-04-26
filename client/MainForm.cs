using System.ComponentModel;
using System.Windows.Forms;
using model;
using Servicies;

namespace client;

public partial class MainForm : Form, IObserver
{
    private IService Service;

    public MainForm(IService service)
    {
        this.Service = service;
        InitializeComponent();
        initViews();
        updateCharityCases();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        Application.Exit();
        base.OnClosing(e);
    }

    private void nameBox_TextChanged(object sender, EventArgs e)
    {
        String name = this.nameBox.Text;
        if (String.IsNullOrEmpty(name))
        {
            return;
        }

        IEnumerator<Donor> donors = this.Service.GetDonorsWithNameContaining(name);
        List<DonorDto> donorDtos = new List<DonorDto>();
        while (donors.MoveNext())
        {
            Donor donor = donors.Current;
            Console.WriteLine(donor);
            donorDtos.Add(new DonorDto(donor.Name, donor.EmailAddress, donor.PhoneNumber));
        }

        this.updateDonorsView(donorDtos.GetEnumerator());
    }

    private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (dataGridView2.SelectedRows.Count > 0)
        {
            String name = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            String email = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            String phone = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            this.nameBox.Text = name;
            this.emailBox.Text = email;
            this.phoneBox.Text = phone;
        }
    }

    private void donationBtn_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count > 0)
        {
            String name = nameBox.Text;
            String email = emailBox.Text;
            String phone = phoneBox.Text;
            double amount = Double.Parse(this.numericUpDown1.Text);
            long charityCaseId = long.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            try
            {
                this.Service.AddNewDonation(charityCaseId, name, email, phone, amount);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }

    public void UpdateEver()
    {
        this.BeginInvoke(() => this.updateCharityCases());
    }
}