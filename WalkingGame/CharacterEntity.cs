using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace WalkingGame
{
    public class CharacterEntity
    {
        //Using a static field allows us to share the same Texture2D across all CharacterEntity instances.
        static Texture2D characterSheetTexture;

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
        }

        //good practice to use the same SpriteBatch instances for all draw calls, makes for the most efficient rendering possible
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 topLeftOfSprite = new Vector2(this.x, this.y);
            Color tintColor = Color.White;
            spriteBatch.Draw(characterSheetTexture, topLeftOfSprite, tintColor);


        }




    }





}