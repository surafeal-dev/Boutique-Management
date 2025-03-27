using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Managment
{
    public partial class Form1 : Form
    {
        float totalPrice;
        //Image file;
        String Cname="";
        String password = "";
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            TBEmpid.MaxLength = 4;
            NotifyIcon notifyIcon = new NotifyIcon();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

            
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("exec GetPassword @FirstName=@FirstName ", con);
                    SqlCommand cmd1 = new SqlCommand("exec GetRoll @FirstName=@FirstName ", con);
                    cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@FirstName", textBox1.Text);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        list.Add(dr.GetString(0));

                    }

                    dr.Close();

                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    while (dr1.Read())
                    {
                        list1.Add(dr1.GetString(0));

                    }

                    dr1.Close();

                    if (list.Contains(textBox2.Text))
                    {
                        password = textBox2.Text;
                        Cname = textBox1.Text;

                        if (list1.Contains("Manager"))
                        {
                            BtnManagerPre.Visible = true;
                        }
                        panel1.Visible = false;
                        panel2.Visible = true;

                        textBox2.ResetText();


                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username or Password!\nPlease Enter the correct input and try again.", "Login Error");
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
           var result =  MessageBox.Show("Are you sure you want to logout","Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                panel2.Visible = false;
                panel1.Visible = true;
                BtnManagerPre.Visible = false;
            }
        }
        /// <summary>
        /// ///////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            List<string> Tlist = new List<string>();

            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";


            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    //SELECT PRODUCTtYPE
                    SqlCommand cmd = new SqlCommand("exec GetProductType ", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (!Tlist.Contains(dr.GetString(0)))
                            Tlist.Add(dr.GetString(0));
                    }
                    comboBox1.DataSource = Tlist;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }


            panel2.Visible = false;
            panel3.Visible = true;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            panel2.Visible = true;
            panel3.Visible = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        DataTable dtstock = new DataTable();

        private void button4_Click(object sender, EventArgs e)
        {
            
            PFullStock.Visible = true;
            panel2.Visible = false;

            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";


            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("exec GetStockData", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(dtstock);
                    DVGStock.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }

        }

                private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        public Image ConvertBytetoImage(Byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }

        }

        private void PModelcombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            
                
            
            List<float> list = new List<float>();
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            if (PModelcombo.Text != "")
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("exec GetImageForStock @ProductModel=@ProductModel ", con);
                    cmd.Parameters.AddWithValue("@ProductModel", PModelcombo.Text);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        pictureBox1.Image = ConvertBytetoImage((byte[])dr.GetValue(0));
                    }
                    pictureBox1.BackgroundImage = null;
                }
                    double x;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("exec GetPrice @ProductModel=@ProductModel; ", con);
                    cmd1.Parameters.AddWithValue("@ProductModel", PModelcombo.Text);
                    cmd1.ExecuteNonQuery();
                    SqlDataReader dr1 = cmd1.ExecuteReader();

                    while (dr1.Read())
                    {
                        x = dr1.GetDouble(0);
                    SPriceLabel.Text = x.ToString();
                    }

                }

            }else
            {
                SPriceLabel.Text ="";
            }

        }

        private void Sexcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> Mlist = new List<string>();
            Mlist.Add("");
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec GetProductModel @ProductType=@ProductType,@Sex=@Sex,@ProductName=@ProductName ", con);
                cmd.Parameters.AddWithValue("@ProductType", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Sex", Sexcombo.Text);
                cmd.Parameters.AddWithValue("@ProductName", PNameCombo.Text);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!Mlist.Contains(dr.GetString(0)))
                        Mlist.Add(dr.GetString(0));
                }

                PModelcombo.DataSource = Mlist;
                
            }
        }

        private void PNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec GetSex @ProductType=@ProductType, @ProductName=@ProductName", con);
                cmd.Parameters.AddWithValue("@ProductType", comboBox1.Text);
                cmd.Parameters.AddWithValue("@ProductName", PNameCombo.Text);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!list.Contains(dr.GetString(0)))
                        list.Add(dr.GetString(0));
                }

                Sexcombo.DataSource = list;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec GetProductName @ProductType=@ProductType ", con);
                cmd.Parameters.AddWithValue("@ProductType", comboBox1.Text);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if(!list.Contains(dr.GetString(0)))
                    list.Add(dr.GetString(0));
                }

                PNameCombo.DataSource = list;

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (PModelcombo.Text != "")
            {
                string[] rows = { PModelcombo.Text, PNameCombo.Text, comboBox1.Text, Sexcombo.Text, SPriceLabel.Text };

                var ListViewItem = new ListViewItem(rows);
                totalPrice += float.Parse(SPriceLabel.Text);
                //UPDATE PRICE
                listView1.Items.Add(ListViewItem);
                TotalPrice.Text = totalPrice.ToString();
                string x=listView1.Items[0].Text;
                //MessageBox.Show("You have " + listView1.Items.Count+" items on the list ", "Error");
               
            }
            else
            {
                MessageBox.Show("Please Enter all required information and try again. ", "Error");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PurchaseBut_Click(object sender, EventArgs e)
        {
           
                
                var result = MessageBox.Show("Are you sure you want to continue with the purchase? ", "Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    TimeSpan currentTime = DateTime.Now.TimeOfDay;
                    //TODAY'S DATE
                    DateTime today = DateTime.Today;
                    String Date = today.ToString("dd/MM/yyyy");

                    String Time = DateTime.Now.ToString("HH:mm");
                    String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
                    int x = listView1.Items.Count;
                    /*for (int i = 0; i < listView1.Items.Count; i++)
                    {
                    MessageBox.Show("x"+listView1.Items.Count+" "+i);
                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("insert into AllSales1 (ProductModel,ProductName,ProductType,sex,price) select ProductModel, ProductName, ProductType, sex, price from Stock where ProductModel = @ProductModel ", con);
                            cmd.Parameters.AddWithValue("@ProductModel", listView1.Items[i].Text);
                            SqlDataReader dr = cmd.ExecuteReader();
                        }

                    }*/
                ///////////////////////////////////////
                foreach (ListViewItem itemRow in this.listView1.Items)
                {
                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("exec InsertSell @ProductModel=@ProductModel,@ProductName=@ProductName,@ProductType=@ProductType,@sex=@sex,@price=@price,@SaleDate=@SaleDate,@SaleTime=@SaleTime,@Cashier=@Cashier ", con);
                            cmd.Parameters.AddWithValue("@ProductModel", itemRow.SubItems[0].Text);
                            cmd.Parameters.AddWithValue("@ProductName", itemRow.SubItems[1].Text);
                            cmd.Parameters.AddWithValue("@ProductType", itemRow.SubItems[2].Text);
                            cmd.Parameters.AddWithValue("@sex", itemRow.SubItems[3].Text);
                            cmd.Parameters.AddWithValue("@price", itemRow.SubItems[4].Text);
                            cmd.Parameters.AddWithValue("@SaleDate", Date);
                            cmd.Parameters.AddWithValue("@SaleTime", Time);
                            cmd.Parameters.AddWithValue("@Cashier", Cname);
                            SqlDataReader dr = cmd.ExecuteReader();
                        }
                }
                    NotifyIcon notifyIcon = new NotifyIcon();
                    notifyIcon.Icon = SystemIcons.Application;
                    notifyIcon.ShowBalloonTip(3000, "Message", "You have successfully made a purchase.", ToolTipIcon.Info);
                    MessageBox.Show("You have successfully made a purchase.", "Message!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    totalPrice = 0;
                    TotalPrice.Text = "0";
                    listView1.Items.Clear();
                    PurchaseBut.Enabled = false;
            }

        }

            private void button7_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            PFullStock.Visible= false;
        }

        private void DVGStock_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DVGStock_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec GetImagetoStockRecord @ProductModel=@ProductModel; ", con);
                cmd.Parameters.AddWithValue("@ProductModel", DVGStock.CurrentCell.Value);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    pictureBox2.Image = ConvertBytetoImage((byte[])dr.GetValue(0));
                }
                pictureBox2.BackgroundImage = null;
            }

        }

        DataTable salesdt = new DataTable();

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            PSalesRec.Visible = true;

            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";


            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("exec GetAllSellRecord", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(salesdt);
                    DVGSales.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }

        }

        private void ViewRecBack_Click(object sender, EventArgs e)
        {
            panel2.Visible= true;
            PSalesRec.Visible= false;
        }

        private void BSearch_Click(object sender, EventArgs e)
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("exec SearchAllSaleDate @SaleDate=@SaleDate", con);
                    cmd.Parameters.AddWithValue("@SaleDate", TBSearch.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DVGSales.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void BRecSearch_Click(object sender, EventArgs e)
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    //SEARCH BASED ON THE VALUE IN THE TEXT BOX
                    SqlCommand cmd = new SqlCommand("exec SearchStockProductName @ProductName=@ProductName", con);
                    cmd.Parameters.AddWithValue("@ProductName", TBRecSearch.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DVGStock.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void visible_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }else if (!checkBox1.Checked)
            {
                textBox2.PasswordChar = '*';
            }
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            DVGSales.DataSource = salesdt;


        }

        private void BtnStockReset_Click(object sender, EventArgs e)
        {

            DVGStock.DataSource = dtstock;

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            var result=MessageBox.Show("Are You sure you want to erase the list", "Clear List", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            {
                listView1.Items.Clear();
                TotalPrice.Text = "0";
                totalPrice = 0;
            }
            
        }

        private void TBSearch_MouseHover(object sender, EventArgs e)
        {
            label17.Visible = true;
        }

        private void TBSearch_MouseLeave(object sender, EventArgs e)
        {
            label17.Visible = false;
        }

        private void TBRecSearch_MouseHover(object sender, EventArgs e)
        {
            label18.Visible=true;
        }

        private void TBRecSearch_MouseLeave(object sender, EventArgs e)
        {
            label18.Visible = false;
        }

        private void BtnManagerPre_Click(object sender, EventArgs e)////////manager privelage
        {
            PManagerPre.Visible = true;
            panel2.Visible = false;
        }

        private void BtnAddP_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            BtnAddImg.Visible = true;
            PRemove.Visible = false;
        }


        void reload()
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("exec GetStockData  ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGVDel.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label36.Text = "Remove Product";
            panel7.Visible = false;
            BtnAddImg.Visible = false;
            PRemove.Visible = true;
            EditBTN.Visible = false;
            RemoveBTN.Visible = true;
            reload();
           


                ;
        }

        private void BtnMPrevBack_Click(object sender, EventArgs e)
        {
            PManagerPre.Visible = false;
            panel2.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {

            int y = 0;
            int s = 0;
            for (int i = 0; i < DGVDel.RowCount; i++)
            {
                if (DGVDel[0, i].Value == DGVDel.SelectedCells[0].Value)
                {
                    s = 1;
                    y = i;
                    break;
                }

            }

            if (s == 1)
            {

                String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            var result = Microsoft.VisualBasic.Interaction.InputBox("For verification, Please enter your password?", "Veridy", "Password");
            if (result == password)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from Stock where ProductModel=@ProductModel;Select ProductModel,ProductName,ProductType,sex,price from Stock  ", con);
                        cmd.Parameters.AddWithValue("@ProductModel", DGVDel.CurrentCell.Value);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DGVDel.DataSource = dt;
                        MessageBox.Show("Deleted Successfully!!", "Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                }
                catch (Exception)
                {
                    MessageBox.Show("Connection Error!!", "Error!!");
                }
            }
            else
            {
                MessageBox.Show("Incorrect Password!!", "Error!!");
            }
            }
            else
            {
                MessageBox.Show("Please select the correct cell under Product Model(ProductModel) and try again.", "Error!!");
            }

        }
        
        Byte[] Convertimgtobyte(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void BADDImg_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.*;)|*.*", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox3.Image = Image.FromFile(ofd.FileName);
                    pictureBox3.BackgroundImage = null;
                }
            }
        }

        int PmodelCheck()
        {
            List<string> list = new List<string>();
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";



                try {
                using (SqlConnection con = new SqlConnection(cs))
                {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("exec GetfromSelfProductModel @ProductModel=@ProductModel ", con);
                        cmd.Parameters.AddWithValue("@ProductModel", textBox4.Text);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            list.Add(dr.GetString(0));

                        }

                        dr.Close();



                        if (list.Contains(textBox4.Text))
                        {
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    
                }

                } catch (Exception){

                    MessageBox.Show("Connection Error!!", "Error!!");
                    return 1;
                }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == ""||textBox4.Text==""|| textBox5.Text == ""||comboBox2.Text==""||textBox6.Text==""||pictureBox3.Image==null)
            {
                MessageBox.Show("Please fill all the required info","Error");
            }
            else
            {
                int exists = PmodelCheck();
                if (exists== 1)
                {
                    MessageBox.Show("Product model already exists.\nPlease enter a diffrent input and try again.", "Add Stock Error");
                }
                else
                {


                    var result = Microsoft.VisualBasic.Interaction.InputBox("For verification, Please enter your password?", "Veridy", "Password");
                    if (result == password)
                    {
                        String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

                        try
                        {
                            using (SqlConnection con = new SqlConnection(cs))
                            {

                                con.Open();
                                SqlCommand cmd = new SqlCommand("Insert into Stock (ProductModel,ProductName,ProductType,sex,image,price) VALUES (@ProductModel,@ProductName,@ProductType,@sex,@image,@price); ", con);
                                cmd.Parameters.AddWithValue("@ProductModel", textBox3.Text);
                                cmd.Parameters.AddWithValue("@ProductName", textBox4.Text);
                                cmd.Parameters.AddWithValue("@ProductType", textBox5.Text);
                                cmd.Parameters.AddWithValue("@sex", comboBox2.Text);
                                cmd.Parameters.AddWithValue("@image", Convertimgtobyte(pictureBox3.Image));
                                cmd.Parameters.AddWithValue("@price", textBox6.Text);
                                MessageBox.Show("Data inserted Successfully!!", "Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmd.ExecuteNonQuery();
                                textBox3.Text = "";
                                textBox4.Text = "";
                                textBox5.Text = "";
                                textBox6.Text = "";
                            }

                        }

                        catch (Exception)
                        {
                            MessageBox.Show("Connection Error!!", "Error!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password!!", "Error!!");
                    }
                }
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void TotalPrice_TextChanged(object sender, EventArgs e)
        {
            if (float.Parse(TotalPrice.Text) > 0)
            {
                PurchaseBut.Enabled = true;
            }
            else if (float.Parse(TotalPrice.Text) == 0)
            {
                PurchaseBut.Enabled = false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            label36.Text = "Edit Product";
            reload();
            PRemove.Visible = true;
            EditBTN.Visible = true;
            RemoveBTN.Visible = false;
        }

        private void EditBTN_Click(object sender, EventArgs e)
        {
            
            
            /*var x = DGVDel.Rows[0].Cells[1].Value;
            String z = DGVDel.SelectedCells[0].Value.ToString();*/
            int y = 0;
            int s = 0;
            for (int i=0; i<DGVDel.RowCount;i++)
            {
                if(DGVDel[0, i].Value == DGVDel.SelectedCells[0].Value)
                {
                    s = 1;
                    y = i;
                    break;
                }
               
            }

            if (s == 1)
            {

                panel7.Visible = true;
                PRemove.Visible = false;

                textBox7.Text = DGVDel[0, y].Value.ToString();
                textBox8.Text = DGVDel[1, y].Value.ToString();
                textBox9.Text = DGVDel[2, y].Value.ToString();
                comboBox5.Text = DGVDel[3, y].Value.ToString();
                textBox11.Text = DGVDel[4, y].Value.ToString();


                String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select image from Stock where ProductModel=@ProductModel; ", con);
                    cmd.Parameters.AddWithValue("@ProductModel", textBox7.Text);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        pictureBox4.Image = ConvertBytetoImage((byte[])dr.GetValue(0));
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the correct cell under ProductModel.", "Error!!");
            }

            


        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            PRemove.Visible = true;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.*;)|*.*", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox4.Image = Image.FromFile(ofd.FileName);
                    pictureBox4.BackgroundImage = null;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var result = Microsoft.VisualBasic.Interaction.InputBox("For verification, Please enter your password?", "Veridy", "Password");
            if (result == password)
            {
                String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

                try
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {




                        con.Open();
                        SqlCommand cmd = new SqlCommand("update Stock set ProductModel=@ProductModel,ProductName=@ProductName,ProductType=@ProductType,sex=@sex,image=@image,price=@price where ProductModel=@ProductModel", con);
                        cmd.Parameters.AddWithValue("@ProductModel", textBox7.Text);
                        cmd.Parameters.AddWithValue("@ProductName", textBox8.Text);
                        cmd.Parameters.AddWithValue("@ProductType", textBox9.Text);
                        cmd.Parameters.AddWithValue("@sex", comboBox5.Text);
                        cmd.Parameters.AddWithValue("@image", Convertimgtobyte(pictureBox4.Image));
                        cmd.Parameters.AddWithValue("@price", textBox11.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Updated Successfully!!", "Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panel7.Visible = false;
                        PRemove.Visible = true;
                        reload();
                    }

                }

                catch (Exception)
                {
                    MessageBox.Show("Connection Error!!", "Error!!");
                }
            }
            else
            {
                MessageBox.Show("Incorrect Password!!", "Error!!");
            }
        }

        private void BEditProd_Click(object sender, EventArgs e)
        {
            label20.Text = "Product Settings";
            PEditStaff.Visible = false;
            PRemoveSale.Visible = false;
            PEditProd.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            /*String saleid1 = "";
            String saleid2 =.ToString();*/
            int s=0;
            int y=0;
            for (int i = 0; i < DGVRemove.RowCount; i++)
            {
                

                if (DGVRemove[1, i].Value == DGVRemove.SelectedCells[0].Value)
                {
                    y=i;
                    s = 1;
                    break;
                }
                
            }
            

            if (s == 1)
            {
                
                var result = Microsoft.VisualBasic.Interaction.InputBox("For verification, Please enter your password?", "Veridy", "Password");
                if (result == password)
                {
                    String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

                    try
                    {
                        using (SqlConnection con = new SqlConnection(cs))
                        {

                            con.Open();
                            SqlCommand cmd1 = new SqlCommand("insert into recycle (Saleid,ProductModel,ProductName,ProductType,sex,price,SaleDate,SaleTime,Cashier) select Saleid,ProductModel,ProductName,ProductType,sex,price,SaleDate,SaleTime,Cashier from AllSales1 where Saleid = @Saleid", con);
                            SqlCommand cmd = new SqlCommand("delete from AllSales1 where Saleid=@Saleid;", con);
                            cmd1.Parameters.AddWithValue("@Saleid", DGVRemove[0, y].Value.ToString());
                            cmd.Parameters.AddWithValue("@Saleid", DGVRemove[0, y].Value.ToString());
                            cmd1.ExecuteNonQuery();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Deleted Successfully!!", "Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DGVRemove_Load();
                        }

                    }

                    catch (Exception)
                    {
                        MessageBox.Show("Connection Error!!", "Error!!");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password!!", "Error!!");
                }



            }
            else
            {
                MessageBox.Show("Please select the correct cell under ProductModel and try again.", "Error");
            }
            
        }

        private void BTNPRremove_Click(object sender, EventArgs e)
        {
            DGVRemove_Load();
            PRremove.Visible = true;
            PRrecycle.Visible = false;
        }

        void DGVrecycle_Load()
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM recycle order by removeid ", con);
                    cmd.Parameters.AddWithValue("@SaleDate", TBSearch.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGVrecycle.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void BTNPRrbin_Click(object sender, EventArgs e)
        {
            PRremove.Visible = false;
            PRrecycle.Visible = true;
            DGVrecycle_Load();
        }

        void DGVRemove_Load()
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM AllSales1 order by Saleid ", con);
                    cmd.Parameters.AddWithValue("@SaleDate", TBSearch.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGVRemove.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void BRemoveRec_Click(object sender, EventArgs e)
        {
            label20.Text = "Remove Sale";
            PRemoveSale.Visible = true;
            PEditProd.Visible = false;
            PEditStaff.Visible = false;
            DGVRemove_Load();

        }

        private void button16_Click(object sender, EventArgs e)//////////////search button for removing sales
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM AllSales1 WHERE CHARINDEX(@Saleid, Saleid) > 0 order by Saleid", con);
                    cmd.Parameters.AddWithValue("@Saleid", RemoveSearch.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGVRemove.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void RemoveSearch_MouseHover(object sender, EventArgs e)
        {
            label34.Visible = true;
        }

        private void RemoveSearch_MouseLeave(object sender, EventArgs e)
        {
            label34.Visible = false;
        }

        private void BTNRemoveRefresh_Click(object sender, EventArgs e)
        {
            DGVRemove_Load();
        }

        private void ProductSet_MouseHover(object sender, EventArgs e)
        {
            label37.Visible = true;
        }

        private void ProductSet_MouseLeave(object sender, EventArgs e)
        {
            label37.Visible= false;
        }

        private void ProductSetSearch_Click(object sender, EventArgs e)
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Stock WHERE CHARINDEX(@ProductModel, ProductModel) > 0 ", con);
                    cmd.Parameters.AddWithValue("@ProductModel", ProductSet.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGVDel.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            reload();
        }

        private void BEditStaff_Click(object sender, EventArgs e)
        {
            label20.Text = "Edit Staff";
            PRemoveSale.Visible = false;
            PEditProd.Visible = false;
            PEditStaff.Visible = true;
        }

        int Check_Sid()
        {
            List<string> list = new List<string>();
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";


            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select Sid from Staff where Sid=@Sid ", con);
                    cmd.Parameters.AddWithValue("@Sid", TBEmpid.Text);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        list.Add(dr.GetString(0));

                    }

                    dr.Close();



                    if (list.Contains(TBEmpid.Text))
                    {

                       
                        return 0;

                    }
                    else
                    {
                        return 1; 
                    }
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
            return 2;
        }


        private void button20_Click(object sender, EventArgs e)
        {
            if (TBEmpid.Text != "" && TBEmpFN.Text!="" && TBEmpLN.Text!=""&& CBEmpSex.Text!=""&& CBEmpRoll.Text!=""&& TBEmpPW.Text!="") {

                if (TBEmpid.TextLength == 6)
                {
                    int Return = Check_Sid();
                    if (Return == 1)
                    {
                        var result = Microsoft.VisualBasic.Interaction.InputBox("For verification, Please enter your password?", "Verify", "Password");
                        if (result == password)
                        {
                            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

                            try
                            {
                                using (SqlConnection con = new SqlConnection(cs))
                                {

                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("insert into Staff values (@Sid,@Firstname,@Lastname,@Sex,@Roll,@Password )", con);
                                    cmd.Parameters.AddWithValue("@Sid", TBEmpid.Text);
                                    cmd.Parameters.AddWithValue("@Firstname", TBEmpFN.Text);
                                    cmd.Parameters.AddWithValue("@Lastname", TBEmpLN.Text);
                                    cmd.Parameters.AddWithValue("@Sex", CBEmpSex.Text);
                                    cmd.Parameters.AddWithValue("@Roll", CBEmpRoll.Text);
                                    cmd.Parameters.AddWithValue("@Password", TBEmpPW.Text);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Data inserted Successfully!!", "Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }

                            }

                            catch (Exception)
                            {
                                MessageBox.Show("Connection Error!!", "Error!!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Password!!", "Error!!");
                        }
                    }
                    else if (Return == 0)
                    {
                        MessageBox.Show("Staff Id already exists!\nPlease Enter the correct input and try again.", "Error");

                        TBEmpid.ResetText();
                    }

                }
                else
                {
                    MessageBox.Show("Employee id is not the required length!\nEmployee id must contain 4 numbers.", "Error");
                }
            }
            else
            {
                MessageBox.Show("All data not inputed!\nPlease Enter the all data and try again.", "Error");
            }
        }


        void DGVStaff_Load()
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Staff order by Sid ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGVStaff.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void ESBRemove_Click(object sender, EventArgs e)
        {
            label47.Text = "Edit staff member";
            DGVStaff_Load();
            PESBtnRemove.Visible = true;
            PEStaffM.Visible = false;
            button21.Visible = false;
            PESAdd.Visible=false;
            PESRemove.Visible=true;
        }

        private void ESBAdd_Click(object sender, EventArgs e)
        {
            PESAdd.Visible = true;
            PESRemove.Visible = false;
            PEStaffM.Visible = false;
        }


        private void PESBtnRemove_Click(object sender, EventArgs e)
        {
            string id1="";
            String id2 = DGVStaff.SelectedCells[0].Value.ToString();
            int s = 0;
            int y = 0;
            for (int i = 0; i < DGVStaff.RowCount; i++)
            {
                //id1 = DGVStaff[0, i].Value.ToString();
                if (DGVStaff[0, i].Value== DGVStaff.SelectedCells[0].Value)
                {

                    //id = DGVStaff[0, i].Value.ToString();
                    y = i;
                    s = 1;
                    break;
                }

            }


            if (s == 1)
            {
                //MessageBox.Show("Saleid= " + DGVRemove[0, y].Value.ToString(), "Error");
                var result = Microsoft.VisualBasic.Interaction.InputBox("For verification, Please enter your password?", "Veridy", "Password");
                if (result == password)
                {
                    String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

                    try
                    {
                        using (SqlConnection con = new SqlConnection(cs))
                        {

                            con.Open();
                            SqlCommand cmd = new SqlCommand("delete from Staff where Sid=@Sid;", con);
                            cmd.Parameters.AddWithValue("@Sid", DGVStaff.SelectedCells[0].Value.ToString());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Deleted Successfully!!", "Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DGVStaff_Load();
                        }

                    }

                    catch (Exception)
                    {
                        MessageBox.Show("Connection Error!!", "Error!!");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password!!", "Error!!");
                }



            }
            else
            {
                MessageBox.Show("Please select the correct cell under Student id(Sid) and try again.", "Error");
            }
        }
        int counter = 0;
        
        private void TBEmpid_KeyPress(object sender, KeyPressEventArgs e)
        {

           
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) 
                {
                    e.Handled = true;
                    //counter++;
                }
           
        }

        private void Btnstaffsearch_Click(object sender, EventArgs e)
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Staff WHERE CHARINDEX(@Sid, Sid) > 0 ", con);
                    cmd.Parameters.AddWithValue("@Sid", TBstaffsearch.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGVStaff.DataSource = dt;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
        }

        private void BtnStaffreload_Click(object sender, EventArgs e)
        {
            DGVStaff_Load();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            
            /*string id1 = "";
            string id2 = DGVStaff.SelectedCells[0].Value.ToString();*/
            int y = 0;
            int s = 0;
            for (int i = 0; i < DGVStaff.RowCount; i++)
            {
                

                if (DGVStaff[0, i].Value== DGVStaff.SelectedCells[0].Value)
                {
                    s = 1;
                    y = i;
                    break;
                }

            }

            if (s == 1)
            {
                PESRemove.Visible = false;
                PEStaffM.Visible = true;
               

                ESEmpId.Text = DGVStaff[0, y].Value.ToString();
                textBox14.Text = DGVStaff[1, y].Value.ToString();
                textBox12.Text = DGVStaff[2, y].Value.ToString();
                comboBox4.Text = DGVStaff[3, y].Value.ToString();
                comboBox3.Text = DGVStaff[4, y].Value.ToString();
                textBox13.Text = DGVStaff[5, y].Value.ToString();


                
            }
            else
            {
                MessageBox.Show("Please select the correct cell under Sid.", "Error!!");
            }
        }

        private void TBstaffsearch_MouseHover(object sender, EventArgs e)
        {
            label46.Visible=true;
        }

        private void TBstaffsearch_MouseLeave(object sender, EventArgs e)
        {
            label46.Visible = false;
        }

        private void ESBEdit_Click(object sender, EventArgs e)
        {
            label47.Text = "Edit staff member";
            PESRemove.Visible = true;
            PESBtnRemove.Visible = false;
            button21.Visible=true;
            DGVStaff_Load();
        }

        private void BtnEStaffMBack_Click(object sender, EventArgs e)
        {
            PESRemove.Visible = true;
            PEStaffM.Visible = false;
        }

        private void ESBtnupdate_Click(object sender, EventArgs e)
        {
            String cs = "Data Source=.;Initial Catalog=Managment;Integrated Security=True";

            var result = Microsoft.VisualBasic.Interaction.InputBox("For verification, Please enter your password?", "Verify", "Password");
            if (result == password)
            {
                try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Staff set Firstname=@Firstname,Lastname=@Lastname,Sex=@Sex,Roll=@Roll,Password=@Password where Sid=@Sid ", con);
                    cmd.Parameters.AddWithValue("@Sid", ESEmpId.Text);
                    cmd.Parameters.AddWithValue("@Firstname", textBox14.Text);
                    cmd.Parameters.AddWithValue("@Lastname", textBox12.Text);
                    cmd.Parameters.AddWithValue("@Sex", comboBox4.Text);
                    cmd.Parameters.AddWithValue("@Roll", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox13.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGVStaff.DataSource = dt;
                    MessageBox.Show("Updated Successfully!!", "Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DGVStaff_Load();
                    PESRemove.Visible = true;
                    PEStaffM.Visible = false;
                    }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error!!", "Error!!");
            }
            }
            else
            {
                MessageBox.Show("Incorrect Password!!", "Error!!");
            }
        }
        string temp1 = "";
        private void TBEmpid_Leave(object sender, EventArgs e)
        {
            if (TBEmpid.Text !="") {
                string temp = "BM";
                 temp1= TBEmpid.Text;
                temp += TBEmpid.Text;
                TBEmpid.Text = temp;
            }
            else
            {
                TBEmpid.Text = "";
            }
                
        }

        private void TBEmpid_Enter(object sender, EventArgs e)
        {
            if (TBEmpid.Text != "")
            {
                TBEmpid.Text= temp1;
            }
        }

        private void DVGSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}