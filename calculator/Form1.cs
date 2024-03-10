using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        public string num1, num2;
        public int f;
        bool to = false;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && to == false)
                textBox1.Text += "0";
        }
        private void button11_KeyDown(object sender, KeyEventArgs e)
        {
            if(textBox1.Text.Length > 0 && to == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    textBox1.Text += "0";
                }
            }
        }
        private void button11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox1.Text.Length == 0 && e.KeyChar == '0')
            {
                // Cancel the input of "0" as the first character
                e.Handled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
           
                if (textBox1.Text.Length > 0 && to == false)
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                    textBox1.SelectionStart = textBox1.Text.Length;
                }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "1";
        }
        private void button7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "1";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "2";
        }
        private void button8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "2";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "3";
        }
        private void button9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "3";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "4";
        }
        private void button4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "4";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "5";
        }
        private void button5_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "5";
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "6";
        }
        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "6";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "7";
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "7";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "8";
        }
        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "8";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (to == false)
                textBox1.Text += "9";
        }
        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += "9";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
             
            num2 = textBox1.Text;
            var str1 = double.Parse(num1);
            var str2 = double.Parse(num2);
            switch (f)
            {
                case 1:
                    textBox1.Text = num1 + " + "+num2+" = "+(str1 + str2).ToString();
                    to = true;
                    break;
                case 2:
                    textBox1.Text = num1 + " - " + num2 + " = " + (str1 - str2).ToString();
                    break;
                case 3:
                    textBox1.Text = num1 + " X " + num2 + " = " + (str1 * str2).ToString();
                    break;
                case 4:
                    textBox1.Text = num1 + " ÷ " + num2 + " = " + (str1 / str2).ToString();
                    break;

            }
            num1 = "";
            num2 = "";
            
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            to = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            num1 = textBox1.Text;
            textBox1.Text = "";
            f = 2; //2 = '-'
        }

        private void button14_Click(object sender, EventArgs e)
        {
            num1 = textBox1.Text;
            textBox1.Text = "";
            f = 3; //3 = '*'
        }

        private void button13_Click(object sender, EventArgs e)
        {
            num1 = textBox1.Text;
            textBox1.Text = "";
            f = 4; //4 = '/'
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += ".";
        }

        private void button12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && to == false)
            {
                textBox1.Text += ".";
            }
        }

        

        private void button16_Click(object sender, EventArgs e)
        {
            num1 = textBox1.Text;
            textBox1.Text = "";
            f = 1; //1 = '+'
        }
    }
}
