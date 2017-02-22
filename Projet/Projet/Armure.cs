using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierXNA
{
    class Armure : Item
    {
        int Défense { get; set; }

        public Armure(int numeroID, string nom, int niveauRequis, string rareté, string description)
            :base(numeroID, nom, niveauRequis, rareté)
        {
            string[] tableauDescription = description.Split('|');
            Défense = int.Parse(tableauDescription[0]);
        }
    }
}
