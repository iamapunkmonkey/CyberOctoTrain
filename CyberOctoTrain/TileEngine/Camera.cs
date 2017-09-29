using Microsoft.Xna.Framework;

namespace CyberOctoTrain.TileEngine
{
    public class Camera
    {
        public Vector2 Position { get; set; }

        private float _speed;
        private Vector2 _position;

        public float Speed
        {
            get { return _speed; }
            set { _speed = MathHelper.Clamp(value, 1f, 16f); }
        }

        public Matrix Transformation => Matrix.CreateTranslation(new Vector3(-Position, 0f));

        public Camera()
        {
            Speed = 4f;
        }

        public Camera(Vector2 pos)
        {
            _speed = 4f;
            _position = pos;
        }

        public void LockCamera(TileMap map, Rectangle viewport)
        {
    //        _position = Position;
  //          _position.X = MathHelper.Clamp(Position.X, 0, map.WidthInPixels - viewport.Width);
//            _position.Y = MathHelper.Clamp(Position.Y, 0, map.HeightInPixels - viewport.Height);
        }
    }
}