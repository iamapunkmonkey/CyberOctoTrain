using System.Collections.Generic;
using CyberOctoTrain.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CyberOctoTrain.Components
{
    public class MenuComponent
    {
        private SpriteFont _font;
        private List<string> _menuItems = new List<string>();
        private int _selectedIndex;
        public bool MouseOver { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        private Texture2D Texture { get; }

        public Color NormalColor { get; set; }
        public Color HiliteColor { get; set; }
        public Vector2 Position { get; set; }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = (int)MathHelper.Clamp(value, 0, _menuItems.Count - 1); }
        }

        public MenuComponent(SpriteFont font, Texture2D tex)
        {
            MouseOver = false;
            _font = font;
            Texture = tex;
        }

        public MenuComponent(SpriteFont font, Texture2D tex, string[] menuItems)
            :this(font, tex)
        {
            _selectedIndex = 0;

            foreach (var item in menuItems)
            {
                _menuItems.Add(item);
            }

            MessureMenu();
        }

        public void SetMenuItems(string[] items)
        {
            _menuItems.Clear();
            _menuItems.AddRange(items);
            MessureMenu();

            _selectedIndex = 0;
        }

        private void MessureMenu()
        {
            Width = Texture.Width;
            Height = 0;

            foreach (var item in _menuItems)
            {
                var size = _font.MeasureString(item);

                if (size.X > Width)
                    Width = (int) size.X;

                Height += Texture.Height + 50;
            }

            Height -= 50;
        }

        public void Update(GameTime gameTime)
        {
            var menuPos = Position;
            var p = Xin.MouseState.Position;

            MouseOver = false;

            for (var i = 0; i < _menuItems.Count; i++)
            {
                var buttonRect = new Rectangle((int) menuPos.X, (int) menuPos.Y, Texture.Width, Texture.Height);

                if (!buttonRect.Contains(p)) continue;
                _selectedIndex = i;
                MouseOver = true;
            }

            if (!MouseOver && (Xin.CheckKeyReleased(Keys.Up)))
            {
                _selectedIndex--;
                if (_selectedIndex < 0)
                    _selectedIndex = _menuItems.Count;
            } else if (!MouseOver && (Xin.CheckKeyReleased(Keys.Down)))
            {
                _selectedIndex++;
                if (_selectedIndex > _menuItems.Count - 1)
                    _selectedIndex = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var menuPosition = Position;

            for (var i = 0; i < _menuItems.Count; i++)
            {
                var menuItem = _menuItems[i];

                var myColor = i == SelectedIndex ? HiliteColor : NormalColor;

                spriteBatch.Draw(Texture, menuPosition, Color.White);

                var textSize = _font.MeasureString(menuItem);

                var  textPosition = menuPosition + new Vector2((int)(Texture.Width -textSize.X) / 2,
                                        (int)(Texture.Height - textSize.Y) / 2);

                spriteBatch.DrawString(_font, menuItem, textPosition, myColor);

                menuPosition.Y += Texture.Height + 50;
            }
        }

    }
}