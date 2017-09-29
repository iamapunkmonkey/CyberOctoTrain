using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CyberOctoTrain.Input
{
    public class Xin : GameComponent
    {
        public static KeyboardState KeyboadState { get; set; } = Keyboard.GetState();
        public static KeyboardState PreviousKeyboardState { get; set; } = Keyboard.GetState();

        public static MouseState MouseState { get; set; } = Mouse.GetState();
        public static MouseState PreviousMouseState { get; set; } = Mouse.GetState();

        public Xin(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeyboardState = KeyboadState;
            KeyboadState = Keyboard.GetState();

            PreviousMouseState = MouseState;
            MouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        public static void FlushInput()
        {
            MouseState = PreviousMouseState;
            KeyboadState = PreviousKeyboardState;
        }

        public static bool CheckKeyReleased(Keys key)
        {
            //return KeyboadState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
            return Keyboard.GetState().IsKeyDown(key);
        }

        public static bool CheckMouseReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return (MouseState.LeftButton == ButtonState.Released) &&
                           (PreviousMouseState.LeftButton == ButtonState.Pressed);
                case MouseButtons.Right:
                    return (MouseState.RightButton == ButtonState.Released) &&
                           (PreviousMouseState.RightButton == ButtonState.Pressed);
                case MouseButtons.Center:
                    return (MouseState.MiddleButton == ButtonState.Released) &&
                           (PreviousMouseState.MiddleButton == ButtonState.Pressed);
                default:
                    return false;
            }
        }
    }
}