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
    public partial class Form1 : Form
    {
        //int player=0;
        //string resualt;
        int p1_sc = 0, p2_sc=0;
        public HashSet<int> generatedNumbers = new HashSet<int>();
        

        public Form1()
        {
            InitializeComponent();
        }
        public void random_num()
        {
           
            Random random = new Random();

            while (generatedNumbers.Count < 3)
            {
                int randomNumber = random.Next(0, 10);

                if (!generatedNumbers.Contains(randomNumber))
                {
                    generatedNumbers.Add(randomNumber);
                }
            }
        }

        public void Strike_Decision(int n1, int n2, int n3, int player)
        {
            int Strike = 0, Ball = 0, Out = 0;
            int fn1 = generatedNumbers.FirstOrDefault(); // 첫 번째 요소 접근
            int fn2 = generatedNumbers.Skip(1).FirstOrDefault(); // 두 번째 요소 접근
            int fn3 = generatedNumbers.Skip(2).FirstOrDefault(); // 세 번째 요소 접근

            if ( fn1== n1)
            {
                Strike++;
            }
            else if(fn1 == n2)
            {
                Ball++;
            }
            else if(fn1 == n3)
            {
                Ball++;
            }


            if (fn2 == n2)
            {
                Strike++;
            }
            else if (fn2 == n1)
            {
                Ball++;
            }
            else if (fn2 == n3)
            {
                Ball++;
            }


            if (fn3 == n3)
            {
                Strike++;
            }
            else if (fn3 == n2)
            {
                Ball++;
            }
            else if (fn3 == n1)
            {
                Ball++;
            }

            if(Strike >= 3)
            {
                Strike = 0;
                Ball = 0;
                Out= 1;
                textBox1.Text += " [strike : " + Strike + "  ball : " + Ball + "  out : " + Out+"] ";
                
                if (player == 1)
                {
                    textBox1.Text = "player 1의 승리";
                    textBox1.AppendText(Environment.NewLine);
                }
                else if (player == 2)
                {
                    textBox1.Text = "player 2의 승리";
                    textBox1.AppendText(Environment.NewLine);
                }
                generatedNumbers.Clear();
                random_num();
                score(player);
            }
            else
                textBox1.Text += " [strike : " + Strike + "  ball : " + Ball + "  out : " + Out + "] ";
            textBox1.AppendText(Environment.NewLine);
        }

        public void score(int num)
        {
            if(num==1)
            {
                p1_sc++;
            }
            else if(num==2)
            {
                p2_sc++;
            }
            textBox2.Text = p1_sc+" - "+p2_sc;
        }
        public void Referee(int player)
        {
            if(player == 1)
            {

                textBox5.Text = "player 2 차례 입니다.";
            }
            else if(player == 2)
            {
                textBox5.Text = "player 1 차례 입니다.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int []num_p1=new int[3];
            for(int i=0;i<3;i++)
            {
                num_p1[i]= textBox3.Text[i] - '0';
            }
            textBox1.Text += "P1 : ";
            foreach (int s in num_p1)
                textBox1.Text += ", "+s.ToString();
            textBox3.Text = "";
            Strike_Decision(num_p1[0], num_p1[1], num_p1[2],1);
            Referee(1);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox4.Text //p2
            int[] num_p2 = new int[3];
            for (int i = 0; i < 3; i++)
            {
                num_p2[i] = textBox4.Text[i] - '0';
            }
            textBox1.Text += "P2 : ";
            foreach (int s in num_p2)
                textBox1.Text += ", " + s.ToString();
            textBox4.Text = "";
            Strike_Decision(num_p2[0], num_p2[1], num_p2[2],2);
            Referee(2);
            textBox1.AppendText(Environment.NewLine);
        }

        private void 정답ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int fn1 = generatedNumbers.FirstOrDefault(); // 첫 번째 요소 접근
            int fn2 = generatedNumbers.Skip(1).FirstOrDefault(); // 두 번째 요소 접근
            int fn3 = generatedNumbers.Skip(2).FirstOrDefault();
            MessageBox.Show(fn1.ToString() +", "+ fn2.ToString()+", " + fn3.ToString(),"심판");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            score(0);
            random_num();
        }

        private void 게임규칙ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gamerule rule = new Gamerule();
            rule.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("경기를 종료합니까?","게임 종료",MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            Close();

        }
    }
}
