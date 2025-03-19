using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace ChessOut.UI
{
    //Draws the player's inventory and the player HP bar
    public class HUD : IMyDrawable
    {
        //Placment Data
        private const int IconSize = 64;
        private const int TextRectWidth = 16;
        private const float TextScale = 3f;  
        private const int HorizontalTextPad = 16;
        private const int VerticalIconPad = 16;
        private Vector2 _panelPosition = new Vector2(0, 128);

        //Elements Positions
        private Vector2 _iconStartPosition;
        private Vector2 _keyIconPosition;
        private Vector2 _potionIconPosition;
        private Vector2 _damageIconPosition;
        private Vector2 _attackSpeedIconPosition;
        private Vector2 _speedIconPosition;

        private Texture2D _panel;
        private Texture2D _keyIcon;
        private Texture2D _potionIcon;
        private Texture2D _damageIcon;
        private Texture2D _attackSpeedIcon;
        private Texture2D _speedIcon;

        private TextBox _keyCounter;
        private TextBox _potionCounter;
        private TextBox _damageCounter;
        private TextBox _attackSpeedCounter;
        private TextBox _speedCounter;

        private PercentSlider _hpBar;
        private Vector2 _hpBarPosition = Vector2.Zero;

        public HUD(PlayerElement player) 
        {
            //Subscribe to events that update the HUD
            player.Inventory.OnInventoryChange += HandleInventoryChange;
            player.HealthHandler.OnHealthChanged += HandleHealthChange;

            //Positiong and create all the HUD elements
            _iconStartPosition = new Vector2(_panelPosition.X+32, _panelPosition.Y+32);
            _keyIconPosition = _iconStartPosition;
            _potionIconPosition = new Vector2(_iconStartPosition.X, _iconStartPosition.Y + IconSize + VerticalIconPad);
            _damageIconPosition = new Vector2(_iconStartPosition.X + IconSize + (HorizontalTextPad*2) + TextRectWidth, _iconStartPosition.Y);
            _attackSpeedIconPosition = new Vector2(_damageIconPosition.X, _potionIconPosition.Y);
            _speedIconPosition = new Vector2(_damageIconPosition.X, _attackSpeedIconPosition.Y + IconSize + VerticalIconPad);

            _panel = AssetsManager.GetAsset(Asset.Panel);
            _keyIcon = AssetsManager.GetAsset(Asset.KeyIcon);
            _potionIcon = AssetsManager.GetAsset(Asset.PotionIcon);
            _damageIcon = AssetsManager.GetAsset(Asset.DamageIcon);
            _attackSpeedIcon = AssetsManager.GetAsset(Asset.AttackSpeedIcon);
            _speedIcon = AssetsManager.GetAsset(Asset.SpeedIcon);

            int pad = IconSize + HorizontalTextPad;

            _keyCounter = new TextBox(new Vector2(_keyIconPosition.X + pad, _keyIconPosition.Y), TextRectWidth, IconSize, TextScale, "x0",Color.Gold);
            _potionCounter = new TextBox(new Vector2(_potionIconPosition.X + pad, _potionIconPosition.Y), TextRectWidth, IconSize, TextScale, "x0", Color.Black);
            _damageCounter = new TextBox(new Vector2(_damageIconPosition.X + pad, _damageIconPosition.Y), TextRectWidth, IconSize, TextScale, "x0", Color.Red);
            _attackSpeedCounter = new TextBox(new Vector2(_attackSpeedIconPosition.X + pad, _attackSpeedIconPosition.Y), TextRectWidth, IconSize, TextScale, "x0", Color.Blue);
            _speedCounter = new TextBox(new Vector2(_speedIconPosition.X + pad, _speedIconPosition.Y), TextRectWidth, IconSize, TextScale, "x0", Color.Green);

            _hpBar = new PercentSlider(_hpBarPosition,1f,Color.White,AssetsManager.GetAsset(Asset.HpBar), AssetsManager.GetAsset(Asset.HpBarFill));
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_panel, _panelPosition, new Rectangle(0, 0, _panel.Width, _panel.Height), Color.White, 0f, Vector2.Zero, 1.15f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_keyIcon, _keyIconPosition, Color.White);
            spriteBatch.Draw(_potionIcon, _potionIconPosition, Color.White);
            spriteBatch.Draw(_damageIcon, _damageIconPosition, Color.White);
            spriteBatch.Draw(_attackSpeedIcon, _attackSpeedIconPosition, Color.White);
            spriteBatch.Draw(_speedIcon, _speedIconPosition, Color.White);

            _keyCounter.Draw(gameTime, spriteBatch);
            _potionCounter.Draw(gameTime, spriteBatch);
            _damageCounter.Draw(gameTime, spriteBatch);
            _attackSpeedCounter.Draw(gameTime, spriteBatch);
            _speedCounter.Draw(gameTime, spriteBatch);

            _hpBar.Draw(gameTime, spriteBatch);
        }

        //Update the health bar on health changes
        private void HandleHealthChange(object sender, HealthChangeEventArgs args)
        {
            _hpBar.SetValue((float)args.NewHealth / args.MaxHealth);
        }

        //Update the inventory showcase when items are moved in or out of the player's inventory
        private void HandleInventoryChange(Inventory inventory)
        {
            _keyCounter.UpdateText($"x{inventory.GetItemAmount(ItemType.Key)}");
            _potionCounter.UpdateText($"x{inventory.GetItemAmount(ItemType.Potion)}");
            _damageCounter.UpdateText($"x{inventory.GetItemAmount(ItemType.DamageToken)}");
            _attackSpeedCounter.UpdateText($"x{inventory.GetItemAmount(ItemType.AttackSpeedToken)}");
            _speedCounter.UpdateText($"x{inventory.GetItemAmount(ItemType.SpeedToken)}");
        } 
    }
}
