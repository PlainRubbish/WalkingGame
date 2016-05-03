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

        //returns the total duration of the animation, this is obtained by adding the duration of all the contained AnimationFrame instances...
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

        //will be called every frame, Its purpose is to increase (timeIntoAnimation)
        //We use this for the walking animation, we check to see if (timeIntoAnimation) is larger than the duration value
        //if so we will cycle back to the beginning
        public void Update(GameTime gameTime)
        {

            double secondsIntoAnimation = timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;

            double remainder = secondsIntoAnimation % Duration.TotalSeconds;

            //time to be added
            timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }

        
        public Rectangle CurrentRectangle
        {
            

            get
            {
                AnimationFrame currentFrame = null;

                //Check to find the frame
                TimeSpan accumulatedTime;

                accumulatedTime = TimeSpan.Zero;

                foreach (var frame in frames)
                {
            

                    if (accumulatedTime + frame.Duration >= timeIntoAnimation)
                    {
                        currentFrame = frame;
                        break;

                    }
                    else
                    {
                        accumulatedTime += frame.Duration;
                    }

                }

                //If no frame was found, then try the last frame
                // just in case timeIntoAnimation exceeds duration
                if (currentFrame == null)
                {
                    currentFrame = frames.LastOrDefault();
                }

                //if we find a frame, return its rectangle
                //else return an empty one
                if (currentFrame != null)
                {
                    return currentFrame.SourceRectangle;
                }
                else
                {
                    return Rectangle.Empty;
                }

            }


        }

    }



}