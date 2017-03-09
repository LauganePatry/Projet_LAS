using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AtelierXNA
{
    enum OrdreDraw { ARRIÈRE_PLAN, MILIEU, AVANT_PLAN};
    enum États { MENU, INVENTAIRE, QUITTER, CONNEXION, JEU, FIN_DE_JEU };
    public class Jeu : Microsoft.Xna.Framework.Game
    {
        const float INTERVALLE_CALCUL_FPS = 1f;
        const float INTERVALLE_MAJ_STANDARD = 1f / 60f;

        GraphicsDeviceManager PériphériqueGraphique { get; set; }
        SpriteBatch GestionSprites { get; set; }
        RessourcesManager<SpriteFont> GestionnaireDeFonts { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        RessourcesManager<Model> GestionnaireDeModèles { get; set; }
        RessourcesManager<Effect> GestionnaireDeShaders { get; set; }
        Caméra CaméraJeu { get; set; }
        InputManager GestionInput { get; set; }
        États ÉtatJeu = États.MENU;
        DialogueMenu MenuAccueil;

        public Jeu()
        {
            PériphériqueGraphique = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            PériphériqueGraphique.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Vector3 positionObjet1 = new Vector3(-2, 0, 0);
            Vector3 positionObjet2 = new Vector3(0, 1.5f, 0);
            Vector3 positionObjet3 = new Vector3(0, 0, 0);
            Vector3 positionObjet4 = new Vector3(0, -1.5f, 0);
            Vector3 positionObjet5 = new Vector3(2, 0, 0);
            Vector3 positionLumière = new Vector3(0, 0f, 3f);
            Vector3 positionCaméra = new Vector3(0, 0, 5);
            Vector3 cibleCaméra = new Vector3(0, 0, 0);
            Vector2 dimensionDialogue = new Vector2(Window.ClientBounds.Width / 3, Window.ClientBounds.Height);
            MenuAccueil = new DialogueMenu(this, dimensionDialogue);
            CaméraJeu = new CaméraSubjective(this, positionCaméra, cibleCaméra, Vector3.Up, INTERVALLE_MAJ_STANDARD);

            CréationDuPanierDeServices();
            
            Components.Add(new ArrièrePlanSpatial(this, "CielÉtoilé", INTERVALLE_MAJ_STANDARD));
            Components.Add(new Afficheur3D(this));
            Components.Add(new AfficheurFPS(this, "Arial20", Color.Gold, INTERVALLE_CALCUL_FPS));
            Components.Add(GestionInput);
            Components.Add(CaméraJeu);
            Components.Add(MenuAccueil);
            base.Initialize();
        }

        private void CréationDuPanierDeServices()
        {
            GestionnaireDeFonts = new RessourcesManager<SpriteFont>(this, "Fonts");
            GestionnaireDeTextures = new RessourcesManager<Texture2D>(this, "Textures");
            GestionnaireDeModèles = new RessourcesManager<Model>(this, "Models");
            GestionnaireDeShaders = new RessourcesManager<Effect>(this, "Effects");
            GestionInput = new InputManager(this);
            GestionSprites = new SpriteBatch(GraphicsDevice);

            Services.AddService(typeof(RessourcesManager<SpriteFont>), GestionnaireDeFonts);
            Services.AddService(typeof(RessourcesManager<Texture2D>), GestionnaireDeTextures);
            Services.AddService(typeof(RessourcesManager<Model>), GestionnaireDeModèles);
            Services.AddService(typeof(RessourcesManager<Effect>), GestionnaireDeShaders);
            Services.AddService(typeof(InputManager), GestionInput);
            Services.AddService(typeof(Caméra), CaméraJeu);
            Services.AddService(typeof(SpriteBatch), GestionSprites);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GérerClavier();
            GérerTransition();
            
            base.Update(gameTime);
        }

        private void GérerTransition()
        {
            switch (ÉtatJeu)
            {
                case États.MENU:
                    if (MenuAccueil.ÉtatJouer)
                    {
                        ÉtatJeu = États.JEU;
                        DémarrerPhaseDeJeu();
                        MenuAccueil.VoirBouttonMenu(false);
                    }
                    if (MenuAccueil.ÉtatInventaire)
                    {
                        ÉtatJeu = États.INVENTAIRE;
                        MenuAccueil.VoirBouttonMenu(false);
                    }
                    break;
                case États.JEU:
                    break;
            }
        }

        private void DémarrerPhaseDeJeu()
        {
            Guerrier pion = new Guerrier(this, "GuerrierB", 0.03f, Vector3.Zero, Vector3.Zero, "bob", 0, 0, 0, 0, 1);
            pion.DrawOrder = (int)OrdreDraw.MILIEU;
            Components.Add(pion);
        }

        private void GérerClavier()
        {
            if (GestionInput.EstEnfoncée(Keys.Escape))
            {
                MenuAccueil.VoirBouttonMenu(true);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
