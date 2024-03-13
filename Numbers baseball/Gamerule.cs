using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numbers_baseball
{
    public partial class Gamerule : Form
    {
        public Gamerule()
        {
            InitializeComponent();
        }

        private void Gamerule_Load(object sender, EventArgs e)
        {
            textBox1.Text += "숫자야구는 상대방이 생각한 0~9까지 숫자로 이루어진,";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "3자리 혹은 4자리 숫자가 무엇인지 맞추는 게임입니다!";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "(단, 숫자는 한 번씩만 사용 가능합니다. ex. 484 불가능, 820 가능)";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);


            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "상대방이 생각한 숫자가 3자리 숫자라고 가정했을 시, 규칙은 다음과 같습니다.";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "1) A는 0~9까지 숫자를 반복없이 사용하여 3가지 숫자를 생각한다";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "2) B는 0~9까지 숫자를 반복없이 사용하여 3자리 숫자를 부른다";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "3) 숫자와 자리의 위치가 맞으면 스트라이크, 숫자만 맞으면 볼입니다.";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "숫자가 하나도 맞지 않을 경우 아웃입니다. ";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "B가 불러준 숫자를 A가 판단하여 B에게 이야기해줍니다.";
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);

            textBox1.Text += "4) 이 과정을 반복하며 A가 생각한 3자리 숫자가 무엇인지 B가 맞춘다.";
            textBox1.AppendText(Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
