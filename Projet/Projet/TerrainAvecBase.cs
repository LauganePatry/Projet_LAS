using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    public class TerrainAvecBase : PrimitiveDeBaseAnimée
    {
        const int NB_TRIANGLES_PAR_TUILE = 2;
        const int NB_SOMMETS_PAR_TRIANGLE = 3;
        const float MAX_COULEUR = 255f;

        Vector3 Étendue { get; set; }
        string NomTextureCarte { get; set; }
        string[] NomTextureTerrain { get; set; }
        string NomTextureBase { get; set; }

        BasicEffect EffetDeBase { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        Texture2D CarteTerrain { get; set; }
        Texture2D[] TextureTerrain { get; set; }
        Texture2D TextureBase { get; set; }
        Vector3 Origine { get; set; }
        Vector3 Delta { get; set; }
        Color[] DataTerrain { get; set; }
        Color[] DataTexture0 { get; set; }
        Color[] DataTexture1 { get; set; }
        public int NbColonnes { get; private set; }
        public int NbRangées { get; private set; }
        Vector3[,] PtsSommets { get; set; }
        Vector2[,] PtsTexture { get; set; }
        Vector3[,] Normales { get; set; }
        VertexPositionTexture[] Sommets { get; set; }
        VertexPositionTexture[] Base { get; set; }


        public TerrainAvecBase(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale,
                       Vector3 étendue, string nomTextureCarte, string[] nomTextureTerrain, string nomTextureBase, float intervalleMAJ)
           : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
        {
            Étendue = étendue;
            NomTextureCarte = nomTextureCarte;
            NomTextureTerrain = nomTextureTerrain;
            NomTextureBase = nomTextureBase;
        }

        public override void Initialize()
        {
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            InitialiserDonnéesCarte();
            TextureTerrain = new Texture2D[NomTextureTerrain.Length];
            InitialiserDonnéesTexture();
            Origine = new Vector3(-Étendue.X / 2, 0, Étendue.Z / 2);
            AllouerTableaux();
            CréerTableauPoints();
            CalculerNormale();
            base.Initialize();
        }

        void CalculerNormale()
        {
            for (int i = 0; i < NbRangées - 1; ++i)
            {
                if (i < NbRangées - 2)
                {
                    for (int j = 0; j < NbColonnes - 1; ++j)
                    {
                        Normales[j, i] = Vector3.Normalize(Vector3.Cross(PtsSommets[j + 1, i], PtsSommets[j, i + 1]));
                    }
                }
                else
                {
                    for (int j = 0; j < NbColonnes - 1; ++j)
                    {
                        Normales[j, i] = Vector3.Normalize(Vector3.Cross(PtsSommets[j, i - 1], PtsSommets[j + 1, i]));
                    }
                }
                Normales[NbColonnes - 1, i] = Vector3.Normalize(Vector3.Cross(PtsSommets[NbColonnes - 1, i + 1], PtsSommets[NbColonnes - 2, i]));
            }
            Normales[NbColonnes - 1, NbRangées - 1] = Vector3.Normalize(Vector3.Cross(PtsSommets[NbColonnes - 2, NbRangées - 1], PtsSommets[NbColonnes - 1, NbRangées - 2]));

        }

        void InitialiserDonnéesCarte()
        {
            CarteTerrain = GestionnaireDeTextures.Find(NomTextureCarte);
            NbColonnes = CarteTerrain.Width;
            NbRangées = CarteTerrain.Height;
            Delta = new Vector3(Étendue.X / NbColonnes, Étendue.Y / MAX_COULEUR, -Étendue.Z / NbRangées);
            DataTerrain = new Color[NbColonnes * NbRangées];
            CarteTerrain.GetData<Color>(DataTerrain);
            NbTriangles = NbColonnes * NbRangées * NB_TRIANGLES_PAR_TUILE;
        }

        void InitialiserDonnéesTexture()
        {
            for (int i = 0; i < NomTextureTerrain.Length; ++i)
            {
                TextureTerrain[i] = GestionnaireDeTextures.Find(NomTextureTerrain[i]);
            }
            TextureBase = GestionnaireDeTextures.Find(NomTextureBase);
            DataTexture0 = new Color[NbColonnes * NbRangées];
            TextureTerrain[0].GetData<Color>(DataTexture0);
            DataTexture1 = new Color[NbColonnes * NbRangées];
            TextureTerrain[1].GetData<Color>(DataTexture1);
        }

        void AllouerTableaux()
        {
            PtsSommets = new Vector3[NbColonnes, NbRangées];
            Normales = new Vector3[NbColonnes, NbRangées];
            Sommets = new VertexPositionTexture[NbTriangles * NB_SOMMETS_PAR_TRIANGLE];
            Base = new VertexPositionTexture[4 * NbColonnes + 4 * NbRangées - 2 * 3];
            PtsTexture = new Vector2[NbColonnes, NbRangées];
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            EffetDeBase = new BasicEffect(GraphicsDevice);
            InitialiserParamètresEffetDeBase();
        }

        void InitialiserParamètresEffetDeBase()
        {
            EffetDeBase.TextureEnabled = true;
        }

        private void CréerTableauPoints()
        {
            int indiceColor = -1;
            for (int i = PtsSommets.GetLength(1) - 1; i >= 0; --i)
            {
                for (int j = 0; j < PtsSommets.GetLength(0); ++j)
                {
                    PtsSommets[j, i] = new Vector3(Origine.X + j * Delta.X, DataTerrain[++indiceColor].B * Delta.Y, Origine.Z + i * Delta.Z);
                    DataTexture0[indiceColor].A = (byte)(MAX_COULEUR - DataTerrain[indiceColor].B);
                    DataTexture1[indiceColor].A = DataTerrain[indiceColor].B;
                }
            }
            TextureTerrain[0].SetData(DataTexture0);
            TextureTerrain[1].SetData(DataTexture1);
            for (int i = 0; i < PtsTexture.GetLength(1); ++i)
            {
                for (int j = 0; j < PtsTexture.GetLength(0); ++j)
                {
                    PtsTexture[j, i] = new Vector2((float)j / NbColonnes, 1 - (float)i / NbRangées);
                }
            }
        }

        protected override void InitialiserSommets()
        {
            int NoSommet = -1;
            for (int i = 0; i < NbRangées - 1; ++i)
            {
                for (int j = 0; j < NbColonnes - 1; ++j)
                {
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[j, i], PtsTexture[j, i]);
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[j, i + 1], PtsTexture[j, i + 1]);
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[j + 1, i], PtsTexture[j + 1, i]);
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[j, i + 1], PtsTexture[j, i + 1]);
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[j + 1, i + 1], PtsTexture[j + 1, i + 1]);
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[j + 1, i], PtsTexture[j + 1, i]);
                }
            }
            NoSommet = -1;
            for (int j = 0; j < NbColonnes; ++j)
            {
                Base[++NoSommet] = new VertexPositionTexture(new Vector3(PtsSommets[j, 0].X, 0, PtsSommets[j, 0].Z), PtsTexture[NoSommet % 4 / 2, 1]);
                Base[++NoSommet] = new VertexPositionTexture(PtsSommets[j, 0], PtsTexture[NoSommet % 4 / 2, 0]);
            }
            for (int i = 1; i < NbRangées - 1; ++i)
            {
                Base[++NoSommet] = new VertexPositionTexture(new Vector3(PtsSommets[NbColonnes - 1, i].X, 0, PtsSommets[NbColonnes - 1, i].Z), PtsTexture[NoSommet % 4 / 2, 1]);
                Base[++NoSommet] = new VertexPositionTexture(PtsSommets[NbColonnes - 1, i], PtsTexture[NoSommet % 4 / 2, 0]);
            }
            for (int j = NbColonnes - 1; j >= 0; --j)
            {
                Base[++NoSommet] = new VertexPositionTexture(new Vector3(PtsSommets[j, NbRangées - 1].X, 0, PtsSommets[j, NbRangées - 1].Z), PtsTexture[NoSommet % 4 / 2, 1]);
                Base[++NoSommet] = new VertexPositionTexture(PtsSommets[j, NbRangées - 1], PtsTexture[NoSommet % 4 / 2, 0]);
            }
            for (int i = NbRangées - 2; i >= 0; --i)
            {
                Base[++NoSommet] = new VertexPositionTexture(new Vector3(PtsSommets[0, i].X, 0, PtsSommets[0, i].Z), PtsTexture[NoSommet % 4 / 2, 1]);
                Base[++NoSommet] = new VertexPositionTexture(PtsSommets[0, i], PtsTexture[NoSommet % 4 / 2, 0]);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            EffetDeBase.World = GetMonde();
            EffetDeBase.View = CaméraJeu.Vue;
            EffetDeBase.Projection = CaméraJeu.Projection;
            foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
            {
                EffetDeBase.Texture = TextureTerrain[0];
                passeEffet.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets, 0, NbTriangles);
                EffetDeBase.Texture = TextureTerrain[1];
                passeEffet.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets, 0, NbTriangles);
                EffetDeBase.Texture = TextureBase;
                passeEffet.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, Base, 0, Base.Length - 2);
            }
            base.Draw(gameTime);
        }

        public Vector3 GetPointSpatial(int x, int y)
        {
            Vector3 pointSpatial = PtsSommets[x, y];
            return new Vector3(pointSpatial.X, pointSpatial.Y, pointSpatial.Z);
        }
        public Vector3 GetNormale(int x, int y)
        {
            Vector3 normale = Normales[x, y];
            return new Vector3(normale.X, normale.Y, normale.Z);
        }
    }
}

