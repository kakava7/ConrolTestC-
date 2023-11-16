using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportShop.UserForms;


namespace SportShop
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
            passwordTextBox.UseSystemPasswordChar = true;
        }
        SQL database = new SQL();
        private string text = String.Empty;
        int countClick = 1;
        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            Color accentColor = Color.FromArgb(73, 140, 81);
            signInBtn.BackColor = accentColor;
            skipBtn.BackColor = accentColor;   
            updateCaptcha.BackColor = accentColor;
            viewPasswordBtn.BackColor = accentColor;
        }
        
        private void skipBtn_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            Visible = false;
            userForm.ShowDialog();
            Visible = true;

        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            var currentLogin = database.ExecuteReader($"SELECT UserID, UserPassword, UserRole FROM [User] WHERE UserLogin = {loginTextBox.Text}");
            if (currentLogin.HasRows)
            {
                string pass = (string)currentLogin.GetValues(1);
                if(pass == passwordTextBox.Text)
                {
                    if ((int)currentLogin.GetValue(0) == 1)
                    {
                        UserForm userForm = new UserForm();
                        Visible = false;
                        userForm.ShowDialog();
                        Visible = true;
                    }
                    else if ((int)currentLogin.GetValue(0) == 2)
                    {
                        MessageBox.Show("Админ");
                    }
                    else if((int)currentLogin.GetValue(0) == 3)
                    {
                        MessageBox.Show("Менеджер");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль неверный");
                }
            }
        }

        private void viewPasswordBtn_Click(object sender, EventArgs e)
        {
            countClick++;
            if (countClick % 2 == 0)
            {
                passwordTextBox.UseSystemPasswordChar = false;
                viewPasswordBtn.Text = "Hide";
            }
            else
            {
                passwordTextBox.UseSystemPasswordChar = true;
                viewPasswordBtn.Text = "Show";
            }
        }
    }
}
