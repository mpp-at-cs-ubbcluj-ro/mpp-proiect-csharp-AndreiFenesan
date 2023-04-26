using Servicies;

namespace client;

public partial class Form1 : Form
{
    private IService _service;

    public Form1(IService service)
    {
        this._service = service;
        AllocConsole();
        InitializeComponent();
    }

    private void loginBtn_Click(object sender, EventArgs e)
    {
        String username = this.usernameText.Text;
        String password = this.passwordText.Text;
        if (this._service.AuthenticateVolunteer(username, password))
        {
            MainForm mainForm = new MainForm(this._service);
            // this.Close();
            mainForm.Show();
            this._service.AddObserver(mainForm);
            Console.WriteLine("Moving to main form");

        }
        else
        {
            this.incorectLabel.Visible = true;
        }
    }
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool AllocConsole();
}