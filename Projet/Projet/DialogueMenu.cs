using System;
using System.Linq;
using Microsoft.Xna.Framework;


namespace AtelierXNA
{
   public class DialogueMenu : Microsoft.Xna.Framework.GameComponent
   {
      const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
      const int NB_ZONES_DIALOGUE = 3; //Cette constante doit valoir 3 au minimum
      Vector2 DimensionDialogue { get; set; }
      Rectangle RectangleDestination { get; set; }
      BoutonDeCommande BtnJouer { get; set; }
      BoutonDeCommande BtnInventaire { get; set; }
      BoutonDeCommande BtnQuitter { get; set; }
 
      public DialogueMenu(Game jeu, Vector2 dimensionDialogue)
         : base(jeu)
      {
         DimensionDialogue = dimensionDialogue;
         RectangleDestination = new Rectangle((int)Game.Window.ClientBounds.Width - (int)DimensionDialogue.X, 0, 
                                              (int)DimensionDialogue.X, (int)DimensionDialogue.Y);
      }

      public override void Initialize()
      {
         int hauteurBouton = RectangleDestination.Height / (NB_ZONES_DIALOGUE + 1);

         Vector2 PositionBouton = new Vector2(RectangleDestination.X + RectangleDestination.Width / 2f, (NB_ZONES_DIALOGUE - 2) * hauteurBouton);
         BtnJouer = new BoutonDeCommande(Game, "Jouer", "Arial20", "BoutonRouge", "BoutonBleu", PositionBouton, true, Jouer, INTERVALLE_MAJ_STANDARD);

         PositionBouton = new Vector2(RectangleDestination.X + RectangleDestination.Width / 2f, (NB_ZONES_DIALOGUE-1) * hauteurBouton);
         BtnInventaire = new BoutonDeCommande(Game, "Inventaire", "Arial20", "BoutonRouge", "BoutonBleu", PositionBouton, true, Inventaire, INTERVALLE_MAJ_STANDARD);

         PositionBouton = new Vector2(RectangleDestination.X + RectangleDestination.Width / 2f, NB_ZONES_DIALOGUE * hauteurBouton);
         BtnQuitter = new BoutonDeCommande(Game, "Quitter", "Arial", "BoutonRouge", "BoutonBleu", PositionBouton, true, Quitter, INTERVALLE_MAJ_STANDARD);

         Game.Components.Add(BtnJouer);
         Game.Components.Add(BtnInventaire);
         Game.Components.Add(BtnQuitter);
      }

        private void Inventaire()
        {
            throw new NotImplementedException();
        }

        private void Jouer()
        {
            throw new NotImplementedException();
        }

        public void Quitter()
      {
         Game.Exit();
      }
   }
}
