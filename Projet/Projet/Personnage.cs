using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    abstract class Personnage : ObjetDeBase
    {
        int ptsDeVie;
        public string Nom { get; protected set; }
        public int PtsDeVie
        {
            get { return ptsDeVie; }
            protected set
            {
                if (value < 0)
                {
                    ptsDeVie = 0;
                }
                else ptsDeVie = value;
            }
        }
        public int Dextérité { get; protected set; }
        public int Force { get; protected set; }
        public int Intelligence { get; protected set; }
        public int Sagesse { get; protected set; }
        public int PtsDéfense { get; protected set; }
        public bool EstMort
        {
            get { return PtsDeVie == 0; }
        }

        protected Personnage(Game jeu, String nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, string nom, int force, int dextérité, int intelligence, int sagesse, int ptsDeVie)
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale)
        {
            Nom = nom;
            Dextérité = dextérité;
            Force = force;
            Intelligence = intelligence;
            Sagesse = sagesse;
            PtsDeVie = ptsDeVie;
        }

        public abstract int Attaquer();

        public void ModifierVitalité(int dommages)
        {
            int modificationVitalité;
            if(dommages > 0)
            {
                modificationVitalité = (int)Math.Round((double)dommages*PtsDéfense/100);
            }
            else
            {
                modificationVitalité = dommages;
            }
            PtsDeVie -= modificationVitalité;
        }
    }
}
