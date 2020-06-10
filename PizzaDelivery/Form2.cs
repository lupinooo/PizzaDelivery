using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.OleDb;

namespace PizzaDelivery
{
    public partial class Form2 : Form
    {
        List<Pizza> orderClient;
        Dictionary<int, string> adrese=new Dictionary<int, string>();
       
        public Form2(List<Pizza> order)
        {
            orderClient = order;
            InitializeComponent();
          
        }

        //afisare date din fisier text
        private void afiseazaBonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            StreamReader sr = new StreamReader("order.txt");
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Text = "BON COMANDA"+Environment.NewLine;
            textBox1.Text += sr.ReadToEnd();
            textBox1.Text += "--------------------------------------------------------------------------"+Environment.NewLine;
            textBox1.Text += "Valoare Comanda: " + tb_valoare.Text;

            

        }
        //doar unde trebuie introduse cifre 
        private void tb_cod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8 || e.KeyChar == '.') 
            {
                e.Handled = false; 
            }
            else e.Handled = true; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float valoare = 0;
             if (tb_nume.Text == "")
            {
                errorProvider1.SetError(tb_nume, "Introduceti numele!");
            }
            else if (tb_nrtel.Text == "")
            {
                errorProvider1.SetError(tb_nrtel, "Introduceti numarul de telefon!");
            }
            //else if (comboBox1.SelectedIndex < 0)
            //{
            //    errorProvider1.SetError(comboBox1, "Alegeti modalitate de plata!");
            //}
            else if (tb_strada.Text == "")
            {
                errorProvider1.SetError(tb_nume, "Introduceti numele strazii!");
            }
            else if (tb_nr.Text == "")
            {
                errorProvider1.SetError(tb_nr, "Introduceti numarul strazii!");
            }
            else
            {
                try
                {
                    string nume = tb_nume.Text;
                    string nrTel = tb_nrtel.Text;
                    
                    string plata = comboBox1.SelectedText;
                    Client c = new Client(nume, plata, nrTel, orderClient);

                    string numeStrada = tb_strada.Text;
                    int nrStrada = Convert.ToInt32(tb_nr.Text);

                   Adresa a = new Adresa(numeStrada, nrStrada);

                    //adrese.Add(cod, a.toString());
                    //foreach (KeyValuePair<int, string> kvp in adrese)
                    //{
                    //    string afisare = "Client: " + kvp.Key + ": " + kvp.Value;
                    //    MessageBox.Show(afisare);
                    //}
                    valoare = c.calculeazaComanda(orderClient);
                    string valoareComanda = valoare.ToString();
                    tb_valoare.Text = valoareComanda;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MemoryStream memStream = new MemoryStream();
            //XmlTextWriter writer = new XmlTextWriter(memStream, Encoding.UTF8);
            //writer.Formatting = Formatting.Indented;
            // writer.WriteStartDocument();
            //writer.WriteStartElement("Date Personale Clienti Pizzerie");
            //writer.WriteStartElement("Clienti");
            //writer.WriteStartElement("Client");
            //writer.WriteStartElement("Cod");
            // writer.WriteValue(tb_cod.Text);
            //writer.WriteEndElement();
            //writer.WriteStartElement("Nume");
            //writer.WriteValue(tb_nume.Text);
            //writer.WriteEndElement();
            //writer.WriteStartElement("Nr Telefon");
            //writer.WriteValue(tb_nrtel.Text);
            // writer.WriteEndElement();
            //writer.WriteStartElement("Adresa");
            //writer.WriteStartAttribute("Strada");
            //writer.WriteValue(tb_strada.Text);
            //writer.WriteEndAttribute();
            // writer.WriteStartAttribute("Numarul");
            //writer.WriteValue(tb_nr.Text);
            // writer.WriteEndAttribute();
            // writer.WriteEndElement();
            // writer.WriteEndElement();
            // writer.WriteEndElement();
            // writer.WriteEndElement();
            // writer.WriteEndDocument();

            //writer.Close();
            //string str = Encoding.UTF8.GetString(memStream.ToArray());
            // memStream.Close();

            // StreamWriter sw = new StreamWriter("clienti.txt");
            // sw.WriteLine(str);
            // sw.Close();
            String connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source=pizzadelivery.accdb";
            OleDbConnection conexiune = new OleDbConnection(connString); 
            OleDbCommand comanda = new OleDbCommand();
            comanda.Connection = conexiune;
            try
            {
                conexiune.Open();
                comanda.CommandText = "SELECT MAX(cod_client) FROM Clienti";
                int codClient = Convert.ToInt32(comanda.ExecuteScalar()) + 1;
                comanda.CommandText = "INSERT INTO clienti(cod_client, nume_full, telefon, plata, nume_strada, nr_strada, valoare_comanda) VALUES(?,?,?,?,?,?,?)";
                comanda.Parameters.Add("cod_client", OleDbType.Integer).Value = codClient;
                comanda.Parameters.Add("nume_full", OleDbType.Char, 30).Value = tb_nume.Text;
                comanda.Parameters.Add("telefon", OleDbType.Char, 30).Value = tb_nrtel.Text;
                comanda.Parameters.Add("plata", OleDbType.Char, 8).Value = comboBox1.SelectedItem.ToString();
                comanda.Parameters.Add("nume_strada", OleDbType.Char, 20).Value = tb_strada.Text;
                comanda.Parameters.Add("nr_strada", OleDbType.Integer).Value = Convert.ToInt32(tb_nr.Text);
                comanda.Parameters.Add("valoare_comanda", OleDbType.Double).Value = Convert.ToDouble(tb_valoare.Text);
                comanda.ExecuteNonQuery();
                foreach (Pizza p in orderClient)
                {
                    OleDbConnection conexiune1 = new OleDbConnection(connString);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Parameters.Clear();
                    cmd.Connection = conexiune1;
                    try
                    {
                        conexiune1.Open();
                        cmd.CommandText = "SELECT MAX(id_pizza) FROM Pizza";
                        int idPizza = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                        cmd.CommandText = "INSERT INTO Pizza(id_pizza, nume, nr_toppinguri, pret, cod_client) VALUES (?,?,?,?,?)";
                        cmd.Parameters.Add("id_pizza", OleDbType.Integer).Value = idPizza;
                        cmd.Parameters.Add("nume", OleDbType.Char, 30).Value = p.Nume;
                        cmd.Parameters.Add("nr_toppinguri", OleDbType.Integer).Value = p.nrTopping();
                        cmd.Parameters.Add("pret", OleDbType.Double).Value =(float)Convert.ToDouble(p.calculeazaPretPizzaTotal());
                        cmd.Parameters.Add("cod_client", OleDbType.Integer).Value = codClient;
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        MessageBox.Show("Felicitari, v-ati inregistrat cu succes!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conexiune1.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
                tb_nume.Clear();
                tb_strada.Clear();
                tb_nr.Clear();
                tb_nrtel.Clear();
                tb_valoare.Clear();
                comboBox1.Text = "";
            }
            


        }

        void pd_print(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBox1.Text, new Font("Calibri", 16), new SolidBrush(Color.Black), 150.0F, 150.0F);
        }

     


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                StreamReader sr = new StreamReader("order.txt");
                textBox1.TextAlign = HorizontalAlignment.Center;
                textBox1.Text = "BON COMANDA" + Environment.NewLine;
                textBox1.Text += sr.ReadToEnd();
                textBox1.Text += "--------------------------------------------------------------------------" + Environment.NewLine;
                textBox1.Text += "Valoare Comanda: " + tb_valoare.Text;

            }
            if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(pd_print);

                PrintPreviewDialog dlg = new PrintPreviewDialog();
                dlg.Document = pd;
                dlg.ShowDialog();
            }
            if (e.KeyCode == Keys.W && e.Modifiers == Keys.Control)
            {
                textBox1.Clear();
            }
        }

        private void printeazaBonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_print);

            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pd;
            dlg.ShowDialog();
        }

        
    }
}
