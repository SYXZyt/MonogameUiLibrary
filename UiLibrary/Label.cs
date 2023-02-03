using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary
{
    /// <summary>
    /// Class representing text on the screen
    /// </summary>
    public sealed class Label
    {
        private string label;
        private float scale;
        private Vector2 position;
        private Color colour;
        private readonly SpriteFont font;
        private Origin origin;
        private Vector2 originOffset;
        private float rotation;
        private float opacity;

        /// <summary>
        /// The position of the text
        /// </summary>
        public Vector2 Position => position;

        /// <summary>
        /// The scale of the text
        /// </summary>
        public float Scale => scale;

        /// <summary>
        /// The opacity of the text
        /// </summary>
        public float Opacity => opacity;

        /// <summary>
        /// Change the colour of the text
        /// </summary>
        /// <param name="colour">The new colour</param>
        public void SetColour(Color colour) => this.colour = colour;

        /// <summary>
        /// Update the label text
        /// </summary>
        /// <param name="label">The new text</param>
        public void SetLabelText(string label)
        {
            this.label = label;
            originOffset = CalculateOriginOffset();
        }

        /// <summary>
        /// Update the scale of the text
        /// </summary>
        /// <param name="scale">The new scale of the text</param>
        public void SetScale(float scale)
        {
            this.scale = scale;
            originOffset = CalculateOriginOffset();
        }

        [Obsolete("Rotation breaks the origin offset of the label and should not be used")]
        public void SetRotation(float rotation) => this.rotation = rotation;

        /// <summary>
        /// Update the text opacity
        /// </summary>
        /// <param name="opacity">The new opacity (Must be a normalised value (0.0f - 1.0f))</param>
        public void SetOpacity(float opacity) => this.opacity = Math.Clamp(opacity, 0f, 1f);

        /// <summary>
        /// Update the text position
        /// </summary>
        /// <param name="position">The new position</param>
        public void MoveLabel(Vector2 position) => this.position = position;

        /// <summary>
        /// Change the origin of the label
        /// </summary>
        /// <param name="origin">The new origin</param>
        public void ChangeOrigin(Origin origin)
        {
            this.origin = origin;
            originOffset = CalculateOriginOffset();
        }

        /// <summary>
        /// Draw the label to the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used to draw</param>
        public void Draw(SpriteBatch spriteBatch) => spriteBatch.DrawString(font, label, position - originOffset, colour * opacity, rotation, Vector2.Zero, scale, SpriteEffects.None, 0f);

        /// <summary>
        /// Draw the label with an outline
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used to draw</param>
        public void DrawWithShadow(SpriteBatch spriteBatch)
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

            int quality = 15; //Changing this, affects the quality of the outline
            int thickness = (int)(2 * scale); //Changing this, affects how large the outline of the label us

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
                float x = position.X - originOffset.X;
                float y = position.Y - originOffset.Y;

                x += lengthdir_x(thickness, dto_i);
                y += lengthdir_y(thickness, dto_i);

                Vector2 outlineOffset = new(x, y);
                spriteBatch.DrawString(font, label, outlineOffset, Color.Black * opacity, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

            //Make sure we draw the label last to make sure it appears on top of the drop shadow
            Draw(spriteBatch);
        }

        /// <summary>
        /// Calculate the draw offset given the set origin
        /// </summary>
        /// <returns>The vector containing the offset</returns>
        public Vector2 CalculateOriginOffset()
        {
            Vector2 labelSize = font.MeasureString(label) * scale;
            Vector2 originOffset = Vector2.Zero;

            //The idea is that the position of the label is in the desired position, but it will always be drawn top left
            //so we need to calculate an offset that we can pass to the draw routine

            //If the origin is top left, then no offset is needed
            if (origin == Origin.TOP_LEFT) return originOffset;

            //Calculate the x offset first
            if (origin is Origin.TOP_CENTRE or Origin.MIDDLE_CENTRE or Origin.BOTTOM_CENTRE)
            {
                originOffset.X = labelSize.X / 2;
            }
            else if (origin is Origin.TOP_RIGHT or Origin.MIDDLE_RIGHT or Origin.BOTTOM_RIGHT)
            {
                originOffset.X = labelSize.X;
            }

            //Calculate the y offset next
            if (origin is Origin.MIDDLE_LEFT or Origin.MIDDLE_CENTRE or Origin.MIDDLE_RIGHT)
            {
                originOffset.Y = labelSize.Y / 2;
            }
            else if (origin is Origin.BOTTOM_LEFT or Origin.BOTTOM_CENTRE or Origin.BOTTOM_RIGHT)
            {
                originOffset.Y = labelSize.Y;
            }

            return originOffset;
        }

        /// <summary>
        /// Create a new label object
        /// </summary>
        /// <param name="label">The text of the label</param>
        /// <param name="scale">The text scale</param>
        /// <param name="position">The position of the label</param>
        /// <param name="colour">The colour of the text</param>
        /// <param name="font">The font of the text</param>
        /// <param name="origin">The origin of the text</param>
        /// <param name="rotation">The degrees of the label (DEPRECATED. DO NOT CHANGE FROM 0.0f)</param>
        public Label(string label, float scale, Vector2 position, Color colour, SpriteFont font, Origin origin, float rotation = 0f)
        {
            this.label = label;
            this.scale = scale;
            this.position = position;
            this.colour = colour;
            this.font = font;
            this.origin = origin;
            originOffset = CalculateOriginOffset();
            this.rotation = rotation;
            opacity = 1;
        }
    }
}