using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HangMan
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();

            try
            {
                using (var sr = File.OpenText(@"dificulty.txt"))
                {
                    comboBox1.SelectedIndex = int.Parse(sr.ReadLine());
                }
            }
            catch
            {
                using (var sr = File.CreateText(@"dificulty.txt"))
                {
                    sr.WriteLine(0);
                    comboBox1.SelectedIndex = 0;
                }
            }
            
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            using (var sr = File.CreateText(@"dificulty.txt"))
            {
                sr.WriteLine(comboBox1.SelectedIndex);
            }
            CloseSettingForm();
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseSettingForm();
        }

        void CloseSettingForm()
        {
            this.Hide();
            Menu m = new Menu();
            m.Show();
        }
    }
}
