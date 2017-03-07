﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna; 

namespace AtelierXNA
{
    class Item
    {
        int NumeroID { get; set; }
        string CatégoriePersonnage { get; set; }
        string Nom { get; set; }
        int NiveauRequis { get; set; }
        int Rareté { get; set; }

        public Item(int numeroID, string catégoriePersonnage, string nom, int niveauRequis, int rareté)
        {
            Nom = nom;
            NiveauRequis = niveauRequis;
            NumeroID = numeroID;
            CatégoriePersonnage = catégoriePersonnage;
            Rareté = rareté;
        }
    }
}
