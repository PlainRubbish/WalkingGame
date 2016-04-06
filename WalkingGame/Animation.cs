using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace WalkingGame
{
    public class Animation
    {
        //stores data for the animation
        //adds AnimationFrame instances to the frames list, this is through the AddFrame method..
        List<AnimationFrame> frames = new List<AnimationFrame>();
        TimeSpan timeIntoAnimation;

        //returns the total duration of the animation, this is obtaines by adding the duration of all the contained AnimationFrame instances...
        TimeSpan Duration
        {
            get
            {

                double totalSeconds = 0;

                foreach (var frame in frames)
                {
                    totalSeconds += frame.Duration.TotalSeconds;
                }

                return TimeSpan.FromSeconds(totalSeconds);

            }

        }

        public void AddFrame(Rectangle rectangle, TimeSpan duration)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                Duration = duration

            };


        }

        //will be called every frame
        public void Update(GameTime gameTime)
        {

            double secondsIntoAnimation = timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;

            double remainder = secondsIntoAnimation % Duration.TotalSeconds;

            //time to be added
            timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }


    }



}