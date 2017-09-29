using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyberOctoTrain.TileEngine
{
	public class AnimatedSprite
	{
		private readonly Dictionary<AnimationKey, Animation> _animations;
		private readonly Texture2D _texture;
		private float _speed;
		private Vector2 _position;

		public Vector2 Position
		{
			get { return _position; }
			set { _position = value; }
		}

		public bool IsActive { get; set; }
		public AnimationKey CurrentAnimation { get; set; }
		public bool IsAnimating { get; set; }
		public int Width => _animations[CurrentAnimation].FrameWidth;
		public int Height => _animations[CurrentAnimation].FrameHeight;

		public float Speed
		{
			get { return _speed; }
			set { _speed = MathHelper.Clamp(value, 1.0f, 400.0f); }
		}

		public Vector2 Velocity { get; set; }

		public AnimatedSprite(Texture2D sprite, IReadOnlyDictionary<AnimationKey, Animation> animations)
		{
			_texture = sprite;
			_animations = new Dictionary<AnimationKey, Animation>();

			foreach (var animation in animations)
			{
				_animations.Add(animation.Key, (Animation) animations[animation.Key].Clone());
			}
		}

		public void ResetAnimation()
		{
			_animations[CurrentAnimation].Reset();
		}

		public virtual void Update(GameTime gameTime)
		{
			if (IsAnimating)
				_animations[CurrentAnimation].Update(gameTime);
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				_texture, 
				Position, 
				_animations[CurrentAnimation].CurrentFrameRect,
				Color.White);
		}

		public void LockToMap(Point mapSize)
		{
			_position.X = MathHelper.Clamp(Position.X, 0, mapSize.X - Width);
			_position.Y = MathHelper.Clamp(Position.Y, 0, mapSize.Y - Height);
		}
	}
}