using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PizzaDelivery
{
    public partial class Form3 : Form
    {
        List<TextBox> listaTb = new List<TextBox>();
        
        public Form3()
        {
            InitializeComponent();
        }

        public void deseneazaPieChart(double[] procente, Color[] culori, Graphics myPieGraphic, Point pieLocation, Size pieSize)
        {
            double sum = 0;
            foreach (double procent in procente) {
                sum += procent;
            }
            //if (sum != 100)
            //{
            //    MessageBox.Show("Ne pare rau! A aparut o eroare.");
            //}
            double procentPieTotal = 0;
            for (int i = 0; i < procente.Length; i++)
            {
                using (SolidBrush brush = new SolidBrush(culori[i]))
                {
                    myPieGraphic.FillPie(brush, new Rectangle(pieLocation, pieSize),
                        Convert.ToSingle(procentPieTotal * 360 / 100), Convert.ToSingle(procente[i] * 360 / 100));

                }
                procentPieTotal += procente[i];
            }
            listaTb.Add(textBox1);
            listaTb.Add(textBox2);
            listaTb.Add(textBox3);
            listaTb.Add(textBox4);
            listaTb.Add(textBox5);
            listaTb.Add(textBox6);
            String[] nume = { "Margherita", "Prosciutto Funghi", "Quattro Formaggi", "Capriciosa", "Calzone", "Carnivora" };
            for (int i = 0; i < listaTb.Count; i++)
            {
                listaTb[i].Text = nume[i] + " " + procente[i] + "%";
                listaTb[i].BackColor = culori[i];
            }
        }

        public void deseneazaPieChartForm()
        {
            //int[] procente = { 10, 20, 18, 34, 8, 10 };
            String connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source=pizzadelivery.accdb";
            OleDbConnection conexiune = new OleDbConnection(connString);
            OleDbCommand comanda = new OleDbCommand();
            comanda.Connection = conexiune;
            double[] procente = new double[6];
            int total = 0;
            try
            {
                conexiune.Open();
                comanda.CommandText = "SELECT count(*) FROM Pizza";
                total = Convert.ToInt32(comanda.ExecuteScalar());
                comanda.CommandText = "SELECT count(id_pizza) FROM Pizza WHERE nume='Margherita'";
                procente[0] = (Convert.ToDouble(comanda.ExecuteScalar())*100)/total;
                comanda.CommandText = "SELECT count(id_pizza) FROM Pizza WHERE nume='Prosciutto Funghi'";
                procente[1] = (Convert.ToDouble(comanda.ExecuteScalar())*100)/total;
                comanda.CommandText = "SELECT count(id_pizza) FROM Pizza WHERE nume='Quattro Formaggi'";
                procente[2] = (Convert.ToDouble(comanda.ExecuteScalar())*100)/total;
                comanda.CommandText = "SELECT count(id_pizza) FROM Pizza WHERE nume='Capriciosa'";
                procente[3] = (Convert.ToDouble(comanda.ExecuteScalar())*100)/total;
                comanda.CommandText = "SELECT count(id_pizza) FROM Pizza WHERE nume='Calzone'";
                procente[4] =( Convert.ToDouble(comanda.ExecuteScalar())*100)/total;
                comanda.CommandText = "SELECT count(id_pizza) FROM Pizza WHERE nume='Carnivora'";
                procente[5] = (Convert.ToDouble(comanda.ExecuteScalar())*100)/total;
                Color[] culori = { Color.LightSalmon, Color.LightGray, Color.LightYellow, Color.LightPink, Color.LightSkyBlue, Color.LightSeaGreen };
                using (Graphics myPieGraphic = this.CreateGraphics())
                {
                    Point pieLocation = new Point(50, 10);
                    Size pieSize = new Size(300, 300);
                    deseneazaPieChart(procente, culori, myPieGraphic, pieLocation, pieSize);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
            }
        }
 

            private void button1_Click(object sender, EventArgs e)
            {
            deseneazaPieChartForm();
            }



        }
    }




