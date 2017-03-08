using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierXNA
{
    static class GénérateurAléatoire
    {
        static Random Générateur { get; set; }

        static GénérateurAléatoire()
        {
            Générateur = new Random();
        }

        static public List<Item> GénérerItemsÀDonnerGagnant(List<Item> listeItem)
        {
            List<Item> ListeItemsÀDonner = new List<Item>();

            for (int j = 0; j < 5; ++j)
            {
                double somme = listeItem.Sum(x => x.Rareté);
                somme = Générateur.NextDouble() * somme;
                double cumul = 0;

                for (int i = 0; i < listeItem.Count; ++i)
                {
                    cumul += listeItem[i].Rareté;

                    if (cumul >= somme)
                    {
                        ListeItemsÀDonner.Add(listeItem[i]);
                        listeItem.Remove(listeItem[i]);
                        i = listeItem.Count - 1;
                    }
                }
            }
            return ListeItemsÀDonner;
        }

        static public List<Item> GénérerItemsÀDonnerPerdant(List<Item> listeItem)
        {
            List<Item> listeItemsDisponiblesPerdant = listeItem.Where(x => x.Rareté != 0.1).ToList();
            List<Item> ListeItemsÀDonner = new List<Item>();

            for (int j = 0; j < 5; ++j)
            {
                double somme = listeItemsDisponiblesPerdant.Sum(x => x.Rareté);
                somme = Générateur.NextDouble() * somme;
                double cumul = 0;

                for (int i = 0; i < listeItemsDisponiblesPerdant.Count; ++i)
                {
                    cumul += listeItemsDisponiblesPerdant[i].Rareté;

                    if (cumul >= somme)
                    {
                        ListeItemsÀDonner.Add(listeItemsDisponiblesPerdant[i]);
                        listeItemsDisponiblesPerdant.Remove(listeItemsDisponiblesPerdant[i]);
                        i = listeItem.Count - 1;
                    }
                }
            }
            return ListeItemsÀDonner;
        }

        static public int DonnerNuméroHasardEntreDeuxBornes(int min, int max)
        {
            return Générateur.Next(min, max + 1);
        }

        static public int DonnerNuméroHasardÀPartirDeZéro(int min, int max)
        {
            return Générateur.Next(min, max + 1);
        }
    }
}
