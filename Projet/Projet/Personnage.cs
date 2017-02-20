using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    abstract class Personnage : PrimitiveDeBaseAnimée
    {
        public string Nom { get; protected set; }
        public int PtsDeVie { get; protected set; }
        public int Dextérité { get; protected set; }
        public int Force { get; protected set; }
        public int Intelligence { get; protected set; }
        public int Sagesse { get; protected set; }
        public int PtsDéfense { get; protected set; }

        protected Personnage(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, string nom, int force, int dextérité, int intelligence, int sagesse, int ptsDeVie)
            : base(jeu, homothétieInitiale,rotationInitiale,positionInitiale,intervalleMAJ)
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
