using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaDelivery
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
       public static void Main(String[]args)
        {

            Pizza p1 = new Pizza("Margherita", new String[2] { "Masline", "Ciuperci" });

            //Console.WriteLine(p1.toString());
            float pret1 = p1.calculeazaPretDupaNume();
            float pret = p1.calculeazaPretPizzaTotal ();
            Console.WriteLine(pret1+" "+pret);

            Pizza p2 = (Pizza)p1.Clone();
            //Console.WriteLine(p2.toString());

            List<Pizza> order = new List<Pizza>();
            Client c1 = new Client("Adi", "cash", "07865372718", order);
            order=c1.plaseazaComanda(new Pizza[2] { p1, p2 });
            foreach(Pizza p in order)
            {
                Console.WriteLine(p.toString());
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            
            
        }
    }
}
