using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery
{
    class Client:IComanda
    {

        private string nume;
        private string plata;
        private string nrTel;
        List<Pizza> order;

        public Client()
        {
           
            nume = "Anonim";
            plata= "cash";
            nrTel = "";
            order = null;
        }

        public Client(string n, string tipPlata, string nr, List<Pizza>o)
        {  
            nume = n;
            plata = tipPlata;
            nrTel = nr;
            order = o;
        }

       

        public string Nume
        {
            get { return nume; }
            set { if (value != null)
                    nume = value;

            }
        }

        public string PlataCard
        {
            get { return plata; }
            set
            {
                plata= value;
            }
        }

        public string NrTel
        {
            get { return nrTel; }
            set { if (value != null)
                    nrTel = value;
            }
        }

        public string toString()
        {
            return "Clientul "+ this.nume + ", plateste: " + this.plata + " si are nr de tel: " + this.nrTel;
        }


        public List<Pizza> plaseazaComanda(Pizza[] p)
        {
            List<Pizza> order = new List<Pizza>();
            for(int i = 0; i < p.Length; i++)
            {   
                order.Add(p[i]);
            }
          
            return order;

        }

        public float calculeazaComanda(List<Pizza>order)
        {
            float valoare = 0.0f;
            foreach(Pizza p in order)
            {
                valoare += p.calculeazaPretPizzaTotal();
            }

            return valoare;


        }
    }
}
