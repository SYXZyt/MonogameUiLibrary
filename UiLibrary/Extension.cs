using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary
{
    public static class Extension
    {
        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            //Initialize a texture
            Texture2D texture = new(device, width, height);

            //The array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Length; pixel++)
            {
                //The function applies the color according to the specified pixel
                data[pixel] = paint(pixel);
            }

            //Set the colour of the texture
            texture.SetData(data);

            return texture;
        }

        /// <summary>
        /// Draw the texture
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position">The position to draw the texture at</param>
        /// <param name="spriteBatch">The spritebatch to draw with</param>
        /// <param name="color">The colour of the texture</param>
        public static void Draw(this Texture2D texture, Vector2 position, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, position, color);
        }

        public static void DrawWithShadow(this Texture2D texture, Vector2 position, SpriteBatch spriteBatch, int quality, int thickness, Color offsetColor, Color color)
        {
            //Most of this code is ripped from my in development game "Stranded In Space II"
            //That game was developed using GMS2, so some of the code is different (but mostly the same)
            //Two functions also need defining. These are functions that are in GMS2. They are simple trigonometry calculations, and simply move a value in a given direction,
            //and a given length

            //Another thing to note with this function, is increasing the quality and thickness will slow down the game, as it causes the for loop to
            //execute additional times

            //GMS2 function definitions
            //Thanks to YAL for providing the source calculation of these
            //https://stackoverflow.com/questions/55399799/game-maker-lengthdir-inner-implementation
            float lengthdir_x(float l, float d) => (float)(l * Math.Cos(d * Math.PI / -180f));
            float lengthdir_y(float l, float d) => (float)(l * Math.Sin(d * Math.PI / -180f));

            //This entire function is ripped from a personal project of mine, and just converted to c#
            /* Below is the function from the Game
            =====================================================================
             function OutlineText(xp, yp, text, thickness, colour, quality) {
	        //Created by Andrew McCluskey http://nalgames.com/
	        //Edited by Jake Milner, for Stranded In Space II
	        draw_set_color(c_black);
	        for(var dto_i=45; dto_i<405; dto_i+=360/quality)
	        {
	            draw_text(xp+lengthdir_x(thickness,dto_i),yp+lengthdir_y(thickness,dto_i),string_hash_to_newline(text));
	        }
	        draw_set_color(colour);
	        draw_text(xp,yp,string_hash_to_newline(text));
            }
            ===================================================================== */
            for (int dto_i = 45; dto_i < 405; dto_i += 360 / quality)
            {
                float x = position.X;
                float y = position.Y;

                x += lengthdir_x(thickness, dto_i);
                y += lengthdir_y(thickness, dto_i);

                Vector2 outlineOffset = new(x, y);
                Draw(texture, outlineOffset, spriteBatch, offsetColor);
            }

            //Make sure we draw the label last to make sure it appears on top of the drop shadow
            Draw(texture, position, spriteBatch, color);
        }
    }
}