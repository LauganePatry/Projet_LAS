using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna; 

namespace AtelierXNA
{
    static class Item
    {
        static List<Item> Items { get; set; }
        string Nom { get; set; }
        string Catégorie { get; set; }

        public Item(string nom, string catégorie)
        {
            Nom = nom;
            Catégorie = catégorie;
            Items = new List<Item>();
        }

        
        private void CréerItem(string description)
        {
        //    // Normaliser le nom de la catégorie pour calquer le nom des classes
        //    catégorie = Char.ToUpper(catégorie[0]) + catégorie.Substring(1).ToLower();

        //    // Ajouter le nom du Namespace pour qualifier entièrement
        //    catégorie = typeof(Program).Namespace + "." + catégorie;

        //    // Détermination d'un type en fonction de la chaine 'catégorie'
        //    Type typeVoulu = Type.GetType(catégorie);

        //    // Tentative d'instanciation : le type de la valeur de retour est 'Object'
        //    var objetCréé = Activator.CreateInstance(typeVoulu, nom, titre, durée);

        //    Items.Add(objetCréé as Participant);
        }


    }
}
