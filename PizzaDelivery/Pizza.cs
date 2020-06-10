using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery
{
   public class Pizza : ICloneable, IComparable

    {
        private string nume;
        private string[] extraTopping;

        public Pizza()
        {
            nume = "Anonim";
            extraTopping = null;
        }

        public Pizza(string n, string[] top)
        {
            nume = n;
            extraTopping = new string[top.Length];
            for (int i = 0; i < extraTopping.Length; i++)
            {
                extraTopping[i] = top[i];
            }
        }

        public string Nume
        {
            get { return nume; }
            set
            {
                if (value != null)
                    nume = value;
            }

        }
        public float calculeazaPretDupaNume() {
            float pret=0;
                if (this.nume=="Margherita")
                {
                    pret = 25;
                }
                else 
                    if(this.nume=="Prosciutto Funghi")
                {
                    pret = 33;
                }
                else if(this.nume=="Quattro Formaggi")
                {
                    pret = 35;
                }
                else if (this.nume == "Capriciosa")
                {
                    pret = 34.5f;
                }
                else if (this.nume == "Calzone")
                {
                    pret = 36;
                }
            else if (this.nume == "Carnivora")
            {
                pret = 35;
            }
            return pret;
            }
 

        public string[] ExtraTopping
        {
            get { return extraTopping; }
            set
            {
                if (value != null)
                    extraTopping = value;
            }
        }

         public int nrTopping()
        {
            int nrToppinguri = 0;
            foreach(string topping in this.extraTopping)
            {
                nrToppinguri++;
            }
            return nrToppinguri;
        }
        public object Clone()
        {
            Pizza clona = (Pizza)this.MemberwiseClone();
            string[] extraToppingNou = (string[])extraTopping.Clone();
            clona.extraTopping = extraToppingNou;
            return clona;
        }

        //compar dupa pret
        public int CompareTo(object obj)
        {
            Pizza p = (Pizza)obj;
            if (this.calculeazaPretDupaNume()> p.calculeazaPretDupaNume())
                return 1;
            else
                if (this.calculeazaPretDupaNume() < p.calculeazaPretDupaNume())
                return -1;
            else return 0;
        }
        public float calculeazaPretPizzaTotal()
        {
            float pret = 0.0f;
            pret += this.calculeazaPretDupaNume();
            for (int i = 0; i < this.extraTopping.Length; i++)
            {
                if (this.extraTopping[i] == "Extra Mozarella")
                    pret += 2;
                if (this.extraTopping[i] == "Sunca")
                    pret += 3;
                if (this.extraTopping[i] == "Masline")
                    pret += 2.5f;
                if (this.extraTopping[i] == "Ciuperci")
                    pret += 2.5f;
                if (this.extraTopping[i] == "Porumb")
                    pret += 2;
            }
            return pret;
        }

        public string toString()
        {
            string rez = "Pizza " + this.nume + " cu extra toppinguri: ";
            for (int i = 0; i < this.extraTopping.Length; i++)
            {
                rez += extraTopping[i] + " ";
            }
            rez += "."+"\nPret: " + this.calculeazaPretPizzaTotal();
            return rez;
        }

        //supraincarcare operator + pt adaugare topping la pizza
        public static Pizza operator +(Pizza p, String topping)
        {
            string[] extraToppingNou = new string[p.extraTopping.Length + 1];
            for (int i = 0; i < p.extraTopping.Length; i++)
            {
                extraToppingNou[i] = p.extraTopping[i];
            }
            extraToppingNou[p.extraTopping.Length + 1] = topping;
            p.extraTopping = extraToppingNou;
            return p;
        }

        //supraincarcare operator index pt accesare unui element din vectorul de toppinguri
        public string this[int index]
        {
            get
            {
                if (extraTopping != null && index >= 0 && index < extraTopping.Length)
                    return extraTopping[index];
                else
                    return null;
            }

            set
            {
                if (value != null)
                    extraTopping[index] = value;
            }
        }

      
    }
}
