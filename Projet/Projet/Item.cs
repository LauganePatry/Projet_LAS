﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna; 

namespace AtelierXNA
{
    class Item
    {
        string Nom { get; set; }
        int NiveauRequis { get; set; }
        int NumeroID { get; set; }

        public Item(int numeroID, string nom, int niveauRequis, string rareté)
        {
            Nom = nom;
            NiveauRequis = niveauRequis;
            NumeroID = numeroID;
        }
    }
}
