using System;
using Microsoft.Xna.Framework;

namespace WalkingGame
{
    public class AnimationFrame
    {
        //defines the area of Texture2d, this will be displayed by AnimationFrame
        public Rectangle SourceRectangle { get; set; }

        //defines how long and AnimationFrame is displayed when used in an animation
        public TimeSpan Duration { get; set; }

    }


}