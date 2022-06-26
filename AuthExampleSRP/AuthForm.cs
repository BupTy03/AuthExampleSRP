using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthExampleSRP
{
    public partial class AuthForm : Form
    {
        private Dictionary<string, string> _logins = new Dictionary<string, string>();

        public AuthForm()
        {
            InitializeComponent();
        }

        private void OnOkButtonClicked(object sender, EventArgs e)
        {
            if(!File.Exists("auth.txt"))
            {
                using (var writer = new StreamWriter("auth.txt"))
                {
                    writer.WriteLine("Admin Admin");
                }
            }

            using (var reader = new StreamReader("auth.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    _logins.TryAdd(line.Split(' ')[0], line.Split(' ')[1]);
            }

            if(!_logins.ContainsKey(loginTextBox.Text))
            {
                MessageBox.Show("Login isn't found!", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }

            string password = _logins[loginTextBox.Text];
            if(password != passwordTextBox.Text)
            {
                MessageBox.Show("Wrong password!", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }

            MessageBox.Show("Authorized successfully", "Message",
            MessageBoxButtons.OK, MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
