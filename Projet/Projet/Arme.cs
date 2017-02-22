using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierXNA
{
    class Arme : Item
    {
        int Attaque { get; set; }

        public Arme(int numeroID, string nom, int niveauRequis, string rareté, string description)
            :base(numeroID, nom, niveauRequis, rareté)
        {
            string[] tableauDescription = description.Split('|');
            Attaque = int.Parse(tableauDescription[0]);
        }
    }
}
