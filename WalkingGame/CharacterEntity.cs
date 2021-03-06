﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace WalkingGame
{
    public class CharacterEntity
    {
        //Using a static field allows us to share the same Texture2D across all CharacterEntity instances.
        static Texture2D characterSheetTexture;

        Animation walkDown;
        Animation currentAnimation;


        public float x
        {
            get;
            set;

        }

        public float y
        {
            get;
            set;

        }

        public CharacterEntity (GraphicsDevice graphicsDevice)
        {
            //if we dont have character sprite sheet
            if (characterSheetTexture == null)
            {
                //open up file stream, load spritesheet
                using (var stream = TitleContainer.OpenStream("Content/charactersheet.png"))
                {

                    characterSheetTexture = Texture2D.FromStream(graphicsDevice, stream);

                }
  
            }

            walkDown = new Animation();
            walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(16, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(32, 0, 16, 16), TimeSpan.FromSeconds(.25));

        }

        //good practice to use the same SpriteBatch instances for all draw calls, makes for the most efficient rendering possible
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 topLeftOfSprite = new Vector2(this.x, this.y);
            Color tintColor = Color.White;

            //spriteBatch.Draw(characterSheetTexture, topLeftOfSprite, tintColor);

            var sourceRectangle = currentAnimation.CurrentRectangle;
            spriteBatch.Draw(characterSheetTexture, topLeftOfSprite, sourceRectangle, Color.White);

        }

        public void Update(GameTime gameTime)
        {
            var velocity = GetDesiredVelocityFromInput();

            //movement based upon time rather than frames
            //time-based movement multiplies a velocity value by how much time has passed since the game has last updated,
            this.x += velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            currentAnimation = walkDown;
            currentAnimation.Update(gameTime);

        }

        Vector2 GetDesiredVelocityFromInput()
        {
            Vector2 desiredVelocity = new Vector2();

            TouchCollection touchCollection = TouchPanel.GetState();

            if(touchCollection.Count > 0)
            {

                //TouchPanel.GetState returns a TouchCollection which is the information about when a user touches the screen
                //if the user is not touching the screen, the TouchCollection will be empty..
                //if the suer touches the screen, we will move the character to the touch, the TouchLocation will be at index 0 for this.
                desiredVelocity.X = touchCollection[0].Position.X - this.x;
                desiredVelocity.Y = touchCollection[0].Position.Y - this.y;

                //checks if the velocity if non zero..
                if (desiredVelocity.X != 0 || desiredVelocity.Y != 0)
                {
                    desiredVelocity.Normalize();
                    const float desiredSpeed = 200;
                    desiredVelocity *= desiredSpeed;

                }

             }

            return desiredVelocity;

        }

    }





}