using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery
{
    class Adresa:ICloneable
    {
        private String numeStrada;
        private int nrStrada;

        public Adresa()
        {
            numeStrada = null;
            nrStrada = 0;
        }
        public Adresa(String nume, int nr)
        {
            numeStrada = nume;
            nrStrada = nr;
        }

        public String NumeStrada
        {
            get { return numeStrada; }
            set
            {
                if (value != null)
                    numeStrada = value;
            }
        }

        public int NrStrada
        {
            get { return nrStrada; }
            set
            {
                if (value != 0)
                    nrStrada = value;
            }
        }

        //de ex: 2 clienti care stau la aceeasi adresa, dar comanda de pe conturi separate
        public object Clone()
        {
            Adresa clona = (Adresa)this.MemberwiseClone();
            return clona;
        }

        public String toString()
        {
            return "Adresa: " + this.numeStrada + ", nr. " + this.nrStrada;
        }
    }
}
