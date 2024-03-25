using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace cash_register
{
    public partial class Form1 : Form
    {
        string Conn = "Server=localhost;Database=market_db;Uid=root;Pwd=0000;";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // MySqlConnection을 사용하여 데이터베이스에 연결
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                // 연결 열기
                conn.Open();

                // MySqlCommand를 사용하여 SQL 쿼리 실행
                using (MySqlCommand msc = new MySqlCommand("UPDATE product SET stock = stock - @QuantityToReduce WHERE p_name = @PName", conn))
                {
                    // 사용자가 입력한 값들을 SQL 쿼리의 매개변수로 대체
                    msc.Parameters.AddWithValue("@QuantityToReduce", int.Parse(textBox2.Text)); // textbox2에 입력된 값을 int로 변환하여 QuantityToReduce 매개변수로 설정
                    msc.Parameters.AddWithValue("@PName", textBox1.Text); // textbox1에 입력된 값을 PName 매개변수로 설정
                                                                       // 가져온 가격을 price 변수에 할당
                    // SQL 쿼리 실행
                    msc.ExecuteNonQuery();
                }
            }

            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();

                // Fetch the price from the product table based on the product name
                decimal price;
                using (MySqlCommand getPriceCmd = new MySqlCommand("SELECT price FROM product WHERE p_name = @PName", conn))
                {
                    getPriceCmd.Parameters.AddWithValue("@PName", textBox1.Text);
                    price = Convert.ToDecimal(getPriceCmd.ExecuteScalar());
                }

                // Calculate the total price by multiplying the product price with the quantity from textBox2.Text
                decimal totalPrice = price * int.Parse(textBox2.Text);

                // Insert the data into the shoping_cart table
                using (MySqlCommand msc = new MySqlCommand("INSERT INTO shoping_cart (s_name, s_number, s_price) VALUES (@PName, @Quantity, @Price)", conn))
                {
                    msc.Parameters.AddWithValue("@PName", textBox1.Text);
                    msc.Parameters.AddWithValue("@Quantity", int.Parse(textBox2.Text));
                    msc.Parameters.AddWithValue("@Price", totalPrice); // Use the calculated total price
                    msc.ExecuteNonQuery();
                }
            }

            //장바구니 출력
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM shoping_cart";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "shoping_cart");

                StringBuilder sb = new StringBuilder();

                foreach (DataRow row in ds.Tables["shoping_cart"].Rows)
                {
                    foreach (DataColumn col in ds.Tables["shoping_cart"].Columns)
                    {
                        sb.Append(row[col].ToString());
                        sb.Append("\t"); 
                    }
                    sb.AppendLine(); 
                }
                textBox7.Text = sb.ToString();
            }
            textBox1.Text = "";
            textBox2.Text = "";

            //수량
            decimal totalPrice1 = 0;

            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                string sql = "SELECT s_number FROM shoping_cart";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "shoping_cart");

                // Iterate through each row in the DataSet
                foreach (DataRow row in ds.Tables["shoping_cart"].Rows)
                {
                    // Retrieve the value of s_price from the current row and add it to totalPrice
                    totalPrice1 += Convert.ToDecimal(row["s_number"]);
                }
            }

            // Display the total price in textBox3.Text
            textBox3.Text = totalPrice1.ToString();

            //총 금액
            decimal totalPrice2 = 0;

            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                string sql = "SELECT s_price FROM shoping_cart";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "shoping_cart");

                // Iterate through each row in the DataSet
                foreach (DataRow row in ds.Tables["shoping_cart"].Rows)
                {
                    // Retrieve the value of s_price from the current row and add it to totalPrice
                    totalPrice2 += Convert.ToDecimal(row["s_price"]);
                }
            }

            // Display the total price in textBox3.Text
            textBox4.Text = totalPrice2.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
