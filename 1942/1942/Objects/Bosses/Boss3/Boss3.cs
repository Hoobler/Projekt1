using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{

    class Boss3 : Boss_Base
    {
        float angleGun;
        float angleSpeed;
        float timer;
        float radius;
        Vector2 rotationPoint;

        float timeUntilNextShot;
        float timeBetweenShots = 0.1f;
        bool animationDelay;

        public Boss3(Vector2 position, float timer)
        {
            size = new Point(100, 100);
            speed = new Vector2(0, 0);
            radius = Settings.windowBounds.Y/2 - size.X/2;
            rotationPoint = new Vector2(Settings.windowBounds.X / 2, Settings.windowBounds.Y / 2);
            this.position = position;
            this.position.X = Settings.windowBounds.X/2 + radius;
            angle = (float)Math.PI;
            angleSpeed = (float)Math.PI / 360f;
            texture = Texture2DLibrary.boss3;
            color = Color.White;
            this.timer = timer+1f;
            this.maxHealth = 1000;
            this.health = maxHealth;
            score = 700;
            killable = true;
            killed = false;
            

            
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            targetableRectangles.Clear();
            targetableRectangles.Add(new Rectangle((int)Center.X - 40, (int)Center.Y - 40, 80, 80));

            if(activated)
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (angle >= 0 && angle < (float)Math.PI / 2f)
                angleGun = 0;
            else if (angle >= (float)Math.PI / 2f && angle < (float)Math.PI)
                angleGun = (float)Math.PI / 2f;
            else if (angle >= (float)Math.PI && angle < (float)Math.PI * (3f / 2f))
                angleGun = (float)Math.PI;
            else if (angle >= (float)Math.PI * (3f / 2f) && angle < (float)Math.PI * 2f)
                angleGun = (float)Math.PI * (3f / 2f);

            if (angle >= (float)Math.PI * 2f)
                angle -= (float)Math.PI * 2f;
            else if (angle < 0)
                angle += (float)Math.PI * 2f;

            if (phase >= 2 && !killed)
            {
                timer++;
                if (timer == 600)
                    Objects.powerUpList.Add(new PowerUpShield(new Vector2(100, -50)));
                if (timer == 1200)
                    Objects.powerUpList.Add(new PowerUpDamage(new Vector2(400, -50)));
                if (timer >= 1200)
                    timer = 0;
            }

            if (phase == 0 && timer <= 0)
            {
                MusicManager.SetMusic(SoundLibrary.Boss3);
                phase = 1;
                speed = new Vector2(0, 1);
            }

            if (phase == 1 && position.Y >= Settings.windowBounds.Y / 2 - size.Y / 2)
            {
                position.Y = Settings.windowBounds.Y / 2 - size.Y / 2;
                phase = 2;
                speed = new Vector2(0, 0);
            }

            if (phase >= 2)
            {
                if (!killed)
                {
                    timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (timeUntilNextShot >= timeBetweenShots)
                    {
                        timeUntilNextShot -= timeBetweenShots;
                        Vector2 shotOrigin = new Vector2(0, 0);
                        shotOrigin.X = (float)Math.Cos(angle - (float)Math.PI / 2f) * size.Y / 2 + Center.X;
                        shotOrigin.Y = (float)Math.Sin(angle - (float)Math.PI / 2f) * size.Y / 2 + Center.Y;
                        Vector2 shot1Origin = new Vector2(0, 0);
                        Vector2 shot2Origin = new Vector2(0, 0);
                        float angleChange = (float)Math.PI / 8;
                        shot1Origin.X = (shotOrigin.X - Center.X) * (float)Math.Cos(angleChange) + (shotOrigin.Y - Center.Y) * (float)Math.Sin(angleChange) + Center.X;
                        shot1Origin.Y = -(shotOrigin.X - Center.X) * (float)Math.Sin(angleChange) + (shotOrigin.Y - Center.Y) * (float)Math.Cos(angleChange) + Center.Y;
                        shot2Origin.X = (shotOrigin.X - Center.X) * (float)Math.Cos(-angleChange) + (shotOrigin.Y - Center.Y) * (float)Math.Sin(-angleChange) + Center.X;
                        shot2Origin.Y = -(shotOrigin.X - Center.X) * (float)Math.Sin(-angleChange) + (shotOrigin.Y - Center.Y) * (float)Math.Cos(-angleChange) + Center.Y;
                        Objects.enemyProjectileList.Add(new Boss3_Projectile_Front(shot1Origin, angle - (float)Math.PI / 2f));
                        Objects.enemyProjectileList.Add(new Boss3_Projectile_Front(shot2Origin, angle - (float)Math.PI / 2f));
                    }
                }

                if (phase == 2)
                {
                    angle += angleSpeed;
                    for (int i = 0; i < accessoryList.Count; i++)
                        if (!accessoryList[i].ReallyActivated)
                            accessoryList[i].ReallyActivated = true;

                    float xPrim = ((Center.X - rotationPoint.X) * (float)Math.Cos(angleSpeed)) - ((Center.Y - rotationPoint.Y) * (float)Math.Sin(angleSpeed)) + rotationPoint.X;
                    float yPrim = ((Center.X - rotationPoint.X) * (float)Math.Sin(angleSpeed)) + ((Center.Y - rotationPoint.Y) * (float)Math.Cos(angleSpeed)) + rotationPoint.Y;

                    position.X = xPrim - size.X/2;
                    position.Y = yPrim - size.Y/2;
                    for (int i = 0; i < accessoryList.Count; i++)
                        accessoryList[i].AngleUpdate(angleGun + (float)Math.PI / 2f);
                }
            }
            if (animationDelay)
            {
                animationDelay = false;
                animationFrame.X++;
            }
            else
                animationDelay = true;


            if (animationFrame.X > 3)
                animationFrame.X = 0;

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (activated)
            {
                
                spriteBatch.Draw(texture,
                    new Rectangle((int)Center.X, (int)Center.Y, Size.X, Size.Y),
                    new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 4) + 1,
                        (animationFrame.Y * (texture.Bounds.Height - 1) / 1) + 1,
                        ((texture.Bounds.Width - 1) / 4) - 1,
                        ((texture.Bounds.Height - 1) / 1) - 1),
                    color,
                    angle,
                    new Vector2(50.5f, texture.Bounds.Height / 2),
                    spriteEffect,
                    layerDepth);

                
            }
            base.Draw(spriteBatch);
        }

        

        public override void Accessorize()
        {
            base.Accessorize();
            accessoryList.Add(new Boss3_Gun(new Vector2(Center.X-10, position.Y+11), angleSpeed, rotationPoint));
            accessoryList.Add(new Boss3_Gun(new Vector2(Center.X - 10, position.Y + 52), angleSpeed, rotationPoint));
            accessoryList.Add(new Boss3_Gun(new Vector2(Center.X - 10, position.Y + 70), angleSpeed, rotationPoint));
        }
    }
}
