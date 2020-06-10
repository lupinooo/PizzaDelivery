using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PizzaDelivery
{
    public partial class Form1 : Form
    {
        List<Pizza> order = new List<Pizza>();
        public Form1() //pt plasare comanda
        {
           
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                ListViewItem itm = new ListViewItem(comboBox1.SelectedItem.ToString());
                string nume = itm.Text;
                string[] extraTopping = new string[checkedListBox1.CheckedItems.Count];
                string topping = "";
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    topping += checkedListBox1.CheckedItems[i].ToString() + ";";

                }
                extraTopping = topping.Split(';'); //transform string in string[]
                Pizza p = new Pizza(nume, extraTopping);

                tb_pret.Text = p.calculeazaPretDupaNume().ToString();
                itm.SubItems.Add(tb_pret.Text);
                itm.SubItems.Add(topping);

                tb_pret_total.Text = p.calculeazaPretPizzaTotal().ToString();
                itm.SubItems.Add(tb_pret_total.Text);

                listView1.Items.Add(itm);

                MessageBox.Show("Pizza a fost adaugata!Daca ati terminat de comandat, apasati 'Finalizeaza Comanda'.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                comboBox1.ResetText();
                tb_pret.Clear();
                tb_pret_total.Clear();
                foreach(int i in checkedListBox1.CheckedIndices)
                {
                    checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        //chiar daca se comanda doar o pizza->trebuie Adauga Pizza si dupa Finalizeaza Comanda
        private void button2_Click(object sender, EventArgs e)
        {
            double pret = 0;
            Random rnd = new Random();
            DialogResult dialogResult = MessageBox.Show("Sunteti siguri ca vreti sa finalizati comanda?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (listView1.Items.Count > 0)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    FileStream fs = new FileStream("order.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);


                    foreach (ListViewItem itm in listView1.Items)
                    {
                        Pizza p = new Pizza();
                        sw.WriteLine("Pizza " + itm.Text + " cu extra toppinguri de: " + itm.SubItems[2].Text + " Pret: " + itm.SubItems[3].Text);
                        pret += Convert.ToDouble(itm.SubItems[3].Text);
                        p.ExtraTopping = itm.SubItems[2].Text.Split(';');
                        p.Nume = itm.Text;
                        order.Add(p);

                    }
                    sw.Close();


                    Form2 frm2 = new Form2(order);
                    frm2.Show();
                }
                else
                {
                    MessageBox.Show("Adaugati cel putin o pizza");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Continua comanda sau Anuleaza");
            }
            }
      


        private void veziPreferinteleCelorCareAuMaiComandatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

 

        private void label5_DragEnter(object sender, DragEventArgs e)
        {
            listView1.Items.Remove(listView1.SelectedItems[0]);

        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView1.DoDragDrop(listView1.SelectedItems, DragDropEffects.Move);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
