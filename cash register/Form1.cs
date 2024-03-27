using System;
using System.Data;
using System.Text;
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
        public void refresh_all()
        {
            using (MySqlConnection conn1 = new MySqlConnection(Conn))
            {
                DataSet ds1 = new DataSet();
                string sql1 = "SELECT * FROM shoping_cart";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql1, conn1);
                adpt.Fill(ds1, "shoping_cart");

                StringBuilder sb = new StringBuilder();

                foreach (DataRow row in ds1.Tables["shoping_cart"].Rows)
                {
                    foreach (DataColumn col in ds1.Tables["shoping_cart"].Columns)
                    {
                        sb.Append(row[col].ToString());
                        sb.Append("\t");
                    }
                    sb.AppendLine();
                }

                //수량
                decimal totalPrice1 = 0;


                    DataSet ds2 = new DataSet();
                    string sql2 = "SELECT s_number FROM shoping_cart";
                    MySqlDataAdapter adpt2 = new MySqlDataAdapter(sql2, conn1);
                    adpt.Fill(ds2, "shoping_cart");

                    // Iterate through each row in the DataSet
                    foreach (DataRow row in ds2.Tables["shoping_cart"].Rows)
                    {
                        // Retrieve the value of s_price from the current row and add it to totalPrice
                        totalPrice1 += Convert.ToDecimal(row["s_number"]);
                    }


                // Display the total price in textBox3.Text
                textBox3.Text = totalPrice1.ToString();

                //총 금액
                decimal totalPrice2 = 0;

              
                    DataSet ds3 = new DataSet();
                    string sql3 = "SELECT s_price FROM shoping_cart";
                    MySqlDataAdapter adpt3 = new MySqlDataAdapter(sql3, conn1);
                    adpt.Fill(ds3, "shoping_cart");

                    // Iterate through each row in the DataSet
                    foreach (DataRow row in ds3.Tables["shoping_cart"].Rows)
                    {
                        // Retrieve the value of s_price from the current row and add it to totalPrice
                        totalPrice2 += Convert.ToDecimal(row["s_price"]);
                    }
                // Display the total price in textBox3.Text
                textBox4.Text = totalPrice2.ToString();

                textBox7.Text = sb.ToString();
            }
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
            refresh_all();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
           try
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    string copyCommand = "insert into cash_payment(c_name, c_stock, c_price, c_date) select s_name, s_number, s_price, NOW() from shoping_cart; ";
                    MySqlCommand copytable = new MySqlCommand(copyCommand, conn);
                    copytable.ExecuteNonQuery();

                    string deleteCommand = "DROP TABLE IF EXISTS shoping_cart";
                    MySqlCommand deleteTable = new MySqlCommand(deleteCommand, conn);
                    deleteTable.ExecuteNonQuery();

                    string createCommand = "CREATE TABLE shoping_cart (s_name VARCHAR(20), s_number INT(10), s_price INT(10))";
                    MySqlCommand createTable = new MySqlCommand(createCommand, conn);
                    createTable.ExecuteNonQuery();
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            
        }

        private void button4_Click(object sender, EventArgs e) //목록 삭제
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    string deleteCommand = "DELETE FROM shoping_cart WHERE s_name = @s_name";
                    MySqlCommand cmd = new MySqlCommand(deleteCommand, conn);

                    
                    cmd.Parameters.AddWithValue("@s_name", textBox1.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // 화면 최신화
                    refresh_all();
                    textBox1.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting row: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("프로그램을 종료하시겠습니까?", "cash_register", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 프로그램을 종료합니다.
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    string copyCommand = "insert into card_payment(ca_name, ca_stock, ca_price, ca_date) select s_name, s_number, s_price, NOW() from shoping_cart; ";
                    MySqlCommand copytable = new MySqlCommand(copyCommand, conn);
                    copytable.ExecuteNonQuery();

                    string deleteCommand = "DROP TABLE IF EXISTS shoping_cart";
                    MySqlCommand deleteTable = new MySqlCommand(deleteCommand, conn);
                    deleteTable.ExecuteNonQuery();

                    string createCommand = "CREATE TABLE shoping_cart (s_name VARCHAR(20), s_number INT(10), s_price INT(10))";
                    MySqlCommand createTable = new MySqlCommand(createCommand, conn);
                    createTable.ExecuteNonQuery();
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void 재고조회ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory_Inquiry form2 = new Inventory_Inquiry();
            form2.ShowDialog();
        }

        private void 재고주문ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory_orders order = new Inventory_orders();
            order.ShowDialog();
        }

        private void 정산ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_settlement settlement = new Sales_settlement();
            settlement.ShowDialog();
        }
    }
}
