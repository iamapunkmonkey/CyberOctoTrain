using System;
using Microsoft.Xna.Framework;

namespace CyberOctoTrain.TileEngine
{
	public class Animation
	{
		private readonly Rectangle[] _frames;
		private int _framesPerSecond;
		private TimeSpan _frameLength;
		private TimeSpan _frameTimer;
		private int _currentFrame;

		public int FramesPerSecond
		{
			get { return _framesPerSecond; }
			set
			{
				if (value < 1)
					_framesPerSecond = 1;
				else if (value > 60)
					_framesPerSecond = 60;
				else
					FramesPerSecond = value;

				_frameLength = TimeSpan.FromSeconds(1 / (double)FramesPerSecond);
			}
		}

		public Rectangle CurrentFrameRect => _frames[_currentFrame];

		public int CurrentFrame
		{
			get { return _currentFrame; }
			set { _currentFrame = MathHelper.Clamp(value, 0, _frames.Length - 1); }
		}

		public int FrameWidth { get; private set; }
		public int FrameHeight { get; private set; }

		public Animation(int frameCount, int frameWidth, int frameHeight, int xOffset, int yOffset)
		{
			_frames = new Rectangle[frameCount];
			FrameWidth = frameWidth;
			FrameHeight = frameHeight;

			for (var i = 0; i < frameCount; i++)
			{
				_frames[i] = new Rectangle(xOffset + (frameWidth * i),
					yOffset,
					frameWidth,
					frameHeight);
			}

			FramesPerSecond = 5;
			Reset();
		}

		private Animation(Animation animation)
		{
			_frames = animation._frames;
			FramesPerSecond = 5;
		}

		public void Update(GameTime gameTime)
		{
			_frameTimer += gameTime.ElapsedGameTime;

			if (_frameTimer < _frameLength) return;
			_frameTimer = TimeSpan.Zero;
			_currentFrame = (CurrentFrame + 1) % _frames.Length;
		}

		public void Reset()
		{
			_currentFrame = 0;
			_frameTimer = TimeSpan.Zero;
		}

		public object Clone()
		{
			var animationClone = new Animation(this)
			{
				FrameHeight = FrameHeight,
				FrameWidth = FrameWidth
			};
			animationClone.Reset();

			return animationClone;
		}
	}
}