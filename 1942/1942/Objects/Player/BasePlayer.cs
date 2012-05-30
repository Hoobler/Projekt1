using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _1942
{
    class BasePlayer: BaseObject
    {
        protected float speedHor;
        protected float speedUp;
        protected float speedDown;
        protected int health = 100;
        protected float timeUntilNextShot;
        protected int playerID;
        protected int myScore = 0;
        protected int damage;
        protected Color projectileColor = Color.Yellow;
        protected bool killed;

        //PowerUp stuff
        protected bool powerupDamage = false;
        protected bool powerupHealth = false;
        protected bool powerupShield = false;
        protected float mActiveDamageTime = 10f;
        protected float resetActiveDamageTime = 10f;
        protected float mActiveArmorTime = 10f;
        protected float resetActiveArmorTime = 10f;

        public BasePlayer(): base()
        {
            
            angle = 0;
            color = Color.White;
            layerDepth = 0f;
            size = Settings.size_player;
            speedHor = 6f;
            speedUp = 4f;
            speedDown = 4f;
            
            texture = Texture2DLibrary.player;
            
        }

        public virtual void Update(KeyboardState keyState, GameTime gameTime)
        {
            if (!killed)
            {
                if (timeUntilNextShot > 0)
                    timeUntilNextShot -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (health <= 0)
                {
                    health = 0;
                    killed = true;
                }
                if (health > 100)
                    health = 100;

                if (powerupDamage)
                {
                    mActiveDamageTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    damage = 20;
                    projectileColor = Color.Red;
                }
                else
                {
                    damage = 10;
                    projectileColor = Color.Yellow;
                }
                if (powerupHealth)
                {
                    powerupHealth = false;
                }
                if (powerupShield)
                {
                    mActiveArmorTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (mActiveDamageTime <= 0)
                {
                    powerupDamage = false;
                    mActiveDamageTime = resetActiveDamageTime;
                }
                if (mActiveArmorTime <= 0)
                {
                    powerupShield = false;
                    mActiveArmorTime = resetActiveArmorTime;
                }

                animationFrame.Y = 0;
                spriteEffect = SpriteEffects.None;

                animationFrame.X++;
                if (animationFrame.X > 2)
                    animationFrame.X = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!killed)
            {
                spriteBatch.Draw(texture,
                    Rectangle,
                    new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 3) + 1,
                        (animationFrame.Y * (texture.Bounds.Height - 1) / 3) + 1,
                        ((texture.Bounds.Width - 1) / 3) - 1,
                        ((texture.Bounds.Height - 1) / 3) - 1),
                    color,
                    angle,
                    new Vector2(0, 0),
                    spriteEffect,
                    0.0f);
                if (powerupShield)
                    spriteBatch.Draw(Texture2DLibrary.shielded, new Rectangle((int)Center.X - 30, (int)Center.Y - 30, 60, 60), Color.White);
            }
        }

        public bool Dead
        {
            get { return dead; }
        }

        public bool Killed
        {
            get { return killed; }
            set { killed = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }


        public void GoLeft()
        {

            position.X -= speedHor;
            animationFrame.Y = 1;

        }

        public void GoRight()
        {

            position.X += speedHor;
            animationFrame.Y = 1;
            spriteEffect = SpriteEffects.FlipHorizontally;

        }

        public void GoUp()
        {

            position.Y -= speedUp;

        }

        public void GoDown()
        {

            position.Y += speedDown;

        }

        public void Fire()
        {

            if (timeUntilNextShot <= 0f)
            {
                if (animationFrame.Y == 1)
                {
                    Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X + size.X / 2 - size.X / 6, position.Y + size.Y / 3), playerID, damage, projectileColor, powerupDamage));
                    Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X + size.X / 2 + size.X / 6, position.Y + size.Y / 3), playerID, damage, projectileColor, powerupDamage));
                }
                else
                {
                    Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X + size.X / 2 - size.X / 4, position.Y + size.Y / 3), playerID, damage, projectileColor, powerupDamage));
                    Objects.playerProjectileList.Add(new Projectile_Player(new Vector2(position.X + size.X / 2 + size.X / 4, position.Y + size.Y / 3), playerID, damage, projectileColor, powerupDamage));
                }
                timeUntilNextShot = Settings.player_projectile_frequency;
                //SoundLibrary.Player_Shot.Play();
            }

            
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public int MyScore
        {
            get { return myScore; }
            set { myScore = value; }
        }

        public bool PowerUpDamage
        {
            get { return powerupDamage; }
            set { powerupDamage = value; }
        }
        public bool PowerUpHealth
        {
            get { return powerupHealth; }
            set { powerupHealth = value; }
        }
        public bool PowerUpShield
        {
            get { return powerupShield; }
            set { powerupShield = value; }
        }

        public Color ProjectileColor
        {
            get { return projectileColor; }
            set { projectileColor = value; }
        }

        public float TimeLeftOnDamagePowerUp
        {
            get { return mActiveDamageTime; }
            set { mActiveDamageTime = value; }
        }
        public float TimeLeftOnArmorPowerUp
        {
            get { return mActiveArmorTime; }
            set { mActiveArmorTime = value; }
        }

    }
}
