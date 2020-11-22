using HelloWorld.Helpers;
using HelloWorldCurrentTime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HelloWorld
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            string currentTime = CurrentTime.GetCurrentTime();
            MessageBox.Show($"{currentTime} Hello, {name}!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;

            KeyValuePair<IEnumerable<string>, bool> nameCheckResult = NameChecker.CheckIsNameCorrect(name);

            bool isNameCorrect = nameCheckResult.Value;
            IEnumerable<string> nameCheckErrors = nameCheckResult.Key;

            if (!isNameCorrect)
            {
                string labelMessage = $"Name \"{name}\" is incorrect!\n";
                string errorList = MessageBuilder.CreateErrorList(nameCheckErrors);

                label2.Text = labelMessage + errorList;
                label2.ForeColor = Color.Red;
                button1.Enabled = isNameCorrect;
            }
            else
            {
                label2.Text = $"Name \"{name}\" is correct!";
                label2.ForeColor = Color.Green;
                button1.Enabled = isNameCorrect;
            }

            label2.Visible = true;
        }
    }
}
