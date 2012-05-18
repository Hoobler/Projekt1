using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _1942
{
    class TileTexture
    {
        private int timer = 0;
        private int animationFrames;
        private int currentFrame;
        private char symbol;
        private bool animated;
        private Texture2D texture;
        private SpriteEffects spriteEffect;

        #region Constructor

        public TileTexture()
        {
        }

        public TileTexture(Texture2D texture, char symbol, bool hFlip, bool vFlip, bool animated , int animationFrames)
        {
            this.texture = texture;
            this.symbol = symbol;
            this.animationFrames = animationFrames;
            this.animated = animated;

            if (hFlip)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            else if (vFlip)
            {
                spriteEffect = SpriteEffects.FlipVertically;
            }

            else
            {
                spriteEffect = SpriteEffects.None;
            }

        }

        #endregion

        public void AnimationFrame()
        {
            timer++;
            if (timer > 3)
            {
                timer = 0;
                currentFrame++;
                if (currentFrame > animationFrames -1)
                {
                    currentFrame = 0;
                }
            }
        }

        #region Properties

        public Texture2D Tex
        {
            get { return texture; }
        }

        public char Symbol
        {
            get { return symbol; }
        }

        /// <summary>
        /// The current frame in the animation
        /// </summary>
        public int CFrame
        {
            get { return currentFrame; }
        }

        /// <summary>
        /// Total frames of animation
        /// </summary>
        public int TFrame
        {
            get { return animationFrames; }
        }

        public bool Animated
        {
            get { return animated; }
        }

        public SpriteEffects SpriteEffect
        {
            get { return spriteEffect; }
        }

        #endregion
    }
}
