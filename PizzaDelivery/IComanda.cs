using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelivery
{
    interface IComanda
    {
        //List<Pizza> plaseazaComanda(Pizza[] p);

        float calculeazaComanda(List<Pizza>order);
    }
}
