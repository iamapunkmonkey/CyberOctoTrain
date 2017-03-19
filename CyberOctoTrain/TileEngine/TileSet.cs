using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CyberOctoTrain.TileEngine
{
    public class TileSet
    {
        public int TilesWide = 8;
        public int TilesHigh = 8;
        public int TileWidth = 64;
        public int TileHeight = 64;

        private Rectangle[] _sourceRectangles;

        public Texture2D Texture { get; set; }
        public string TextureName { get; set; }

        [ContentSerializerIgnore]
        public Rectangle[] SourceRectangles => (Rectangle[]) _sourceRectangles.Clone();

        public TileSet()
        {
            MakeGrid();
        }

        public TileSet(int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            MakeGrid();
        }

        private void MakeGrid()
        {
            _sourceRectangles = new Rectangle[TilesWide * TilesHigh];

            var tile = 0;

            for (var y = 0; y < TilesHigh; y++)
            {
                for (var x = 0; x < TilesWide; x++)
                {
                    _sourceRectangles[tile] = new Rectangle(
                        x * TilesWide,
                        y * TileHeight,
                        TileWidth,
                        TileHeight
                    );

                    tile++;
                }
            }
        }
    }
}