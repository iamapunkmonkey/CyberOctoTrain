﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CyberOctoTrain.TileEngine
{
    public class TileMap
    {
        [ContentSerializer]
        public string MapName { get; set; }
        [ContentSerializer]
        public TileLayer GroundLayer { get; set; }
        [ContentSerializer]
        public TileLayer EdgeLayer { get; set; }
        [ContentSerializer]
        public TileLayer BuildingLayer { get; set; }
        [ContentSerializer]
        public TileLayer DecorationLayer { get; set; }
        [ContentSerializer]
        public TileSet TileSet { get; set; }

        [ContentSerializer]
        public Dictionary<string, Point> Characters { get; } = new Dictionary<string, Point>();

        [ContentSerializer]
        public int MapWidth { get; set; }
        [ContentSerializer]
        public int MapHeight { get; set; }

        public int WidthInPixels => MapWidth * Engine.TileWidth;

        public int HeightInPixels => MapHeight * Engine.TileHeight;

        private TileMap() { }

        private TileMap(TileSet tileSet, string mapName)
        {
            TileSet = tileSet;
            MapName = mapName;
        }

        public TileMap(
            TileSet tileSet,
            TileLayer groundLayer,
            TileLayer edgeLayer,
            TileLayer buildingLayer,
            TileLayer decorationLayer,
            string mapName)
            : this(tileSet, mapName)
        {
            GroundLayer = groundLayer;
            EdgeLayer = edgeLayer;
            BuildingLayer = buildingLayer;
            DecorationLayer = decorationLayer;

            MapWidth = groundLayer.Width;
            MapHeight = groundLayer.Height;
        }

        public void SetGroundTile(int x, int y, int index)
        {
            GroundLayer.SetTile(x, y, index);
        }
        public int GetGroundTile(int x, int y)
        {
            return GroundLayer.GetTile(x, y);
        }
        public void SetEdgeTile(int x, int y, int index)
        {
            EdgeLayer.SetTile(x, y, index);
        }
        public int GetEdgeTile(int x, int y)
        {
            return EdgeLayer.GetTile(x, y);
        }
        public void SetBuildingTile(int x, int y, int index)
        {
            BuildingLayer.SetTile(x, y, index);
        }
        public int GetBuildingTile(int x, int y)
        {
            return BuildingLayer.GetTile(x, y);
        }
        public void SetDecorationTile(int x, int y, int index)
        {
            DecorationLayer.SetTile(x, y, index);
        }
        public int GetDecorationTile(int x, int y)
        {
            return DecorationLayer.GetTile(x, y);
        }

        public void FillEdge()
        {
            for (var y = 0; y < MapHeight; y++)
            {
                for (var x = 0; x < MapWidth; x++)
                {
                    EdgeLayer.SetTile(x, y, -1);
                }
            }
        }

        public void FillBuilding()
        {
            for (var y = 0; y < MapHeight; y++)
            {
                for (var x = 0; x < MapWidth; x++)
                {
                    BuildingLayer.SetTile(x, y, -1);
                }
            }
        }

        public void FillDecoration()
        {
            for (var y = 0; y < MapHeight; y++)
            {
                for (var x = 0; x < MapWidth; x++)
                {
                    DecorationLayer.SetTile(x, y, -1);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            GroundLayer?.Update(gameTime);
            EdgeLayer?.Update(gameTime);
            BuildingLayer?.Update(gameTime);
            DecorationLayer?.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            GroundLayer?.Draw(gameTime, spriteBatch, TileSet, camera);
            EdgeLayer?.Draw(gameTime, spriteBatch, TileSet, camera);
            BuildingLayer?.Draw(gameTime, spriteBatch, TileSet, camera);
            DecorationLayer?.Draw(gameTime, spriteBatch, TileSet, camera);
        }
    }
}