using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HangMan
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btn_newGame_Click(object sender, EventArgs e)
        {
            HangMan form1 = new HangMan();
            form1.Visible = true;
            this.Hide();
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            SettingForm st = new SettingForm();
            st.Visible = true;
            this.Hide();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
    }
}
