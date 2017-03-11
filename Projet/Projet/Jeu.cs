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
    enum OrdreDraw { ARRI�RE_PLAN, MILIEU, AVANT_PLAN};
    enum �tats { MENU, INVENTAIRE, QUITTER, CONNEXION, JEU, FIN_DE_JEU };
    public class Jeu : Microsoft.Xna.Framework.Game
    {
        const float INTERVALLE_CALCUL_FPS = 1f;
        const float INTERVALLE_MAJ_STANDARD = 1f / 60f;

        GraphicsDeviceManager P�riph�riqueGraphique { get; set; }
        SpriteBatch GestionSprites { get; set; }
        RessourcesManager<SpriteFont> GestionnaireDeFonts { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        RessourcesManager<Model> GestionnaireDeMod�les { get; set; }
        RessourcesManager<Effect> GestionnaireDeShaders { get; set; }
        Cam�ra Cam�raJeu { get; set; }
        InputManager GestionInput { get; set; }
        �tats �tatJeu = �tats.MENU;
        DialogueMenu MenuAccueil;

        public Jeu()
        {
            P�riph�riqueGraphique = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            P�riph�riqueGraphique.SynchronizeWithVerticalRetrace = false;
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
            Vector3 positionLumi�re = new Vector3(0, 0f, 3f);
            Vector3 positionCam�ra = new Vector3(0, 0, 5);
            Vector3 cibleCam�ra = new Vector3(0, 0, 0);
            Vector2 dimensionDialogue = new Vector2(Window.ClientBounds.Width / 3, Window.ClientBounds.Height);
            MenuAccueil = new DialogueMenu(this, dimensionDialogue);
            Cam�raJeu = new Cam�raSubjective(this, positionCam�ra, cibleCam�ra, Vector3.Up, INTERVALLE_MAJ_STANDARD);

            Cr�ationDuPanierDeServices();
            
            Components.Add(new Arri�rePlanSpatial(this, "Ciel�toil�", INTERVALLE_MAJ_STANDARD));
            Components.Add(new Afficheur3D(this));
            Components.Add(new AfficheurFPS(this, "Arial20", Color.Gold, INTERVALLE_CALCUL_FPS));
            Components.Add(GestionInput);
            Components.Add(Cam�raJeu);
            Components.Add(MenuAccueil);
            base.Initialize();
        }

        private void Cr�ationDuPanierDeServices()
        {
            GestionnaireDeFonts = new RessourcesManager<SpriteFont>(this, "Fonts");
            GestionnaireDeTextures = new RessourcesManager<Texture2D>(this, "Textures");
            GestionnaireDeMod�les = new RessourcesManager<Model>(this, "Models");
            GestionnaireDeShaders = new RessourcesManager<Effect>(this, "Effects");
            GestionInput = new InputManager(this);
            GestionSprites = new SpriteBatch(GraphicsDevice);

            Services.AddService(typeof(RessourcesManager<SpriteFont>), GestionnaireDeFonts);
            Services.AddService(typeof(RessourcesManager<Texture2D>), GestionnaireDeTextures);
            Services.AddService(typeof(RessourcesManager<Model>), GestionnaireDeMod�les);
            Services.AddService(typeof(RessourcesManager<Effect>), GestionnaireDeShaders);
            Services.AddService(typeof(InputManager), GestionInput);
            Services.AddService(typeof(Cam�ra), Cam�raJeu);
            Services.AddService(typeof(SpriteBatch), GestionSprites);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            G�rerClavier();
            G�rerTransition();
            
            base.Update(gameTime);
        }

        private void G�rerTransition()
        {
            switch (�tatJeu)
            {
                case �tats.MENU:
                    if (MenuAccueil.�tatJouer)
                    {
                        �tatJeu = �tats.JEU;
                        D�marrerPhaseDeJeu();
                        MenuAccueil.VoirBouttonMenu(false);
                    }
                    if (MenuAccueil.�tatInventaire)
                    {
                        �tatJeu = �tats.INVENTAIRE;
                        MenuAccueil.VoirBouttonMenu(false);
                    }
                    break;
                case �tats.JEU:
                    break;
            }
        }

        private void D�marrerPhaseDeJeu()
        {
            Guerrier pion = new Guerrier(this, "GuerrierB", 0.03f, Vector3.Zero, Vector3.Zero, "bob", 0, 0, 0, 0, 1);
            pion.DrawOrder = (int)OrdreDraw.MILIEU;
            Components.Add(pion);
        }

        private void G�rerClavier()
        {
            if (GestionInput.EstEnfonc�e(Keys.Escape))
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
