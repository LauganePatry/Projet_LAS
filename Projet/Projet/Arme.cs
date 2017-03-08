using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierXNA
{
    class Arme : Item
    {
        int Attaque { get; set; }

        public Arme(int numeroID, string catégoriePersonnage, string nom, int niveauRequis, double rareté, string description)
            :base(numeroID, catégoriePersonnage, nom, niveauRequis, rareté)
        {
            string[] tableauDescription = description.Split('|');
            Attaque = int.Parse(tableauDescription[0]);
        }
    }
}
