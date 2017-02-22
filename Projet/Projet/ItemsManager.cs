using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierXNA
{
    static class ItemsManager
    {
        static List<Item> Items { get; set; }

        static ItemsManager()
        {
            Items = new List<Item>();
        }

        static void CréerItem(string description)
        {
            char séparateur = ';';

            // Extraction du nom et de la catégorie de l'item du string description
            string[] tableauDescription = description.Split(séparateur);
            string catégorie = tableauDescription[0];
            int numeroID = int.Parse(tableauDescription[1]);
            string nom = tableauDescription[2];
            int niveauRequis = int.Parse(tableauDescription[3]);
            string rareté = tableauDescription[4];
            string descriptionRestante = tableauDescription[5];

            // Normaliser le nom de la catégorie pour calquer le nom des classes (Exemple : (J) + (eu) = Jeu)
            catégorie = Char.ToUpper(catégorie[0]) + catégorie.Substring(1).ToLower();

            // Ajouter le nom du Namespace pour qualifier entièrement
            catégorie = typeof(Program).Namespace + "." + catégorie;

            // Détermination d'un type en fonction de la chaine 'catégorie'
            Type typeVoulu = Type.GetType(catégorie);

            // Tentative d'instanciation : le type de la valeur de retour est 'Object'
            var objetCréé = Activator.CreateInstance(typeVoulu, numeroID, nom, niveauRequis, rareté, descriptionRestante);

            Items.Add(objetCréé as Item);
        }
    }
}
