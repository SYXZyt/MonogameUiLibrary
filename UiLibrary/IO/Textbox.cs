using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.IO
{
    public sealed class Textbox : Element
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetKeyState(int keyCode);

        private ulong tick;
        private bool caret;

        private Origin origin;

        private static bool CapsLock => (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        private static bool Shift => Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift);

        private readonly int charSize;
        private readonly SpriteFont font;
        private readonly StringBuilder text;

        private readonly int height;
        private readonly float fontScale;

        private bool isFocused;
        private bool isEntered;
        private Vector2 originOffset;
        private readonly Vector2 position;
        private readonly int characterLimit;

        private readonly Texture2D background;

        public AABB GetBoundingBox => new((short)position.X, (short)position.Y, (short)(characterLimit * charSize), (short)height);

        private bool isActive;

        public bool IsEntered
        {
            get => isEntered;
            set => isEntered = value;
        }

        public StringBuilder GetText() => text;

        public void SetActive(bool isActive) => this.isActive = isActive;

        public bool SetFocus(bool isFocused) => this.isFocused = isFocused;

        private void Append(char c)
        {
            if (text.Length < characterLimit) text.Append(c);
        }

        public void ChangeOrigin(Origin origin)
        {
            this.origin = origin;
            originOffset = CalculateOriginOffset();
        }

        public Vector2 CalculateOriginOffset()
        {
            Vector2 size = new(characterLimit * charSize, height);
            Vector2 originOffset = Vector2.Zero;

            //This is a slightly modified version of Label::CalculateOriginOffset()
            if (origin is Origin.TOP_LEFT or Origin.MIDDLE_LEFT or Origin.BOTTOM_LEFT) return originOffset;

            if (origin is Origin.TOP_CENTRE or Origin.MIDDLE_CENTRE or Origin.BOTTOM_CENTRE)
            {
                originOffset.X = size.X / 2;
            }
            else
            {
                originOffset.X = size.X;
            }

            return originOffset;
        }

        public override void Update()
        {
            tick++;
            if (tick % 20 == 0) caret = !caret;

            if (!isActive) return;

            //Check if the textbox is focused
            {
                AABB textBox = new((short)position.X, (short)position.Y, (short)(characterLimit * charSize), (short)height);
                AABB mouse = (AABB)MouseController.GetMousePosition();

                if (textBox.CollisionCheck(mouse) && MouseController.IsPressed(MouseController.MouseButton.LEFT)) isFocused = true;
                else if (MouseController.IsPressed(MouseController.MouseButton.LEFT)) isFocused = false;
            }

            if (!isFocused) return;

            //If you read this, I'm sorry
            if (KeyboardController.IsPressed(Keys.Back)) if (text.Length > 0) text.Length--;
            if (KeyboardController.IsPressed(Keys.Enter)) isEntered = true;
            if (KeyboardController.IsPressed(Keys.Space)) Append(' ');
            if (KeyboardController.IsPressed(Keys.A)) Append(CapsLock ? 'A' : 'a');
            if (KeyboardController.IsPressed(Keys.B)) Append(CapsLock ? 'B' : 'b');
            if (KeyboardController.IsPressed(Keys.C)) Append(CapsLock ? 'C' : 'c');
            if (KeyboardController.IsPressed(Keys.D)) Append(CapsLock ? 'D' : 'd');
            if (KeyboardController.IsPressed(Keys.E)) Append(CapsLock ? 'E' : 'e');
            if (KeyboardController.IsPressed(Keys.F)) Append(CapsLock ? 'F' : 'f');
            if (KeyboardController.IsPressed(Keys.G)) Append(CapsLock ? 'G' : 'g');
            if (KeyboardController.IsPressed(Keys.H)) Append(CapsLock ? 'H' : 'h');
            if (KeyboardController.IsPressed(Keys.I)) Append(CapsLock ? 'I' : 'i');
            if (KeyboardController.IsPressed(Keys.J)) Append(CapsLock ? 'J' : 'j');
            if (KeyboardController.IsPressed(Keys.K)) Append(CapsLock ? 'K' : 'k');
            if (KeyboardController.IsPressed(Keys.L)) Append(CapsLock ? 'L' : 'l');
            if (KeyboardController.IsPressed(Keys.M)) Append(CapsLock ? 'M' : 'm');
            if (KeyboardController.IsPressed(Keys.N)) Append(CapsLock ? 'N' : 'n');
            if (KeyboardController.IsPressed(Keys.O)) Append(CapsLock ? 'O' : 'o');
            if (KeyboardController.IsPressed(Keys.P)) Append(CapsLock ? 'P' : 'p');
            if (KeyboardController.IsPressed(Keys.Q)) Append(CapsLock ? 'Q' : 'q');
            if (KeyboardController.IsPressed(Keys.R)) Append(CapsLock ? 'R' : 'r');
            if (KeyboardController.IsPressed(Keys.S)) Append(CapsLock ? 'S' : 's');
            if (KeyboardController.IsPressed(Keys.T)) Append(CapsLock ? 'T' : 't');
            if (KeyboardController.IsPressed(Keys.U)) Append(CapsLock ? 'U' : 'u');
            if (KeyboardController.IsPressed(Keys.V)) Append(CapsLock ? 'V' : 'v');
            if (KeyboardController.IsPressed(Keys.W)) Append(CapsLock ? 'W' : 'w');
            if (KeyboardController.IsPressed(Keys.X)) Append(CapsLock ? 'X' : 'x');
            if (KeyboardController.IsPressed(Keys.Y)) Append(CapsLock ? 'Y' : 'y');
            if (KeyboardController.IsPressed(Keys.Z)) Append(CapsLock ? 'Z' : 'z');
            if (KeyboardController.IsPressed(Keys.NumPad0)) Append('0');
            if (KeyboardController.IsPressed(Keys.NumPad1)) Append('1');
            if (KeyboardController.IsPressed(Keys.NumPad2)) Append('2');
            if (KeyboardController.IsPressed(Keys.NumPad3)) Append('3');
            if (KeyboardController.IsPressed(Keys.NumPad4)) Append('4');
            if (KeyboardController.IsPressed(Keys.NumPad5)) Append('5');
            if (KeyboardController.IsPressed(Keys.NumPad6)) Append('6');
            if (KeyboardController.IsPressed(Keys.NumPad7)) Append('7');
            if (KeyboardController.IsPressed(Keys.NumPad8)) Append('8');
            if (KeyboardController.IsPressed(Keys.NumPad9)) Append('9');
            if (KeyboardController.IsPressed(Keys.D0)) Append(Shift ? ')' : '0');
            if (KeyboardController.IsPressed(Keys.D1)) Append(Shift ? '!' : '1');
            if (KeyboardController.IsPressed(Keys.D2)) Append(Shift ? '"' : '2');
            if (KeyboardController.IsPressed(Keys.D3)) Append(Shift ? '£' : '3');
            if (KeyboardController.IsPressed(Keys.D4)) Append(Shift ? '$' : '4');
            if (KeyboardController.IsPressed(Keys.D5)) Append(Shift ? '%' : '5');
            if (KeyboardController.IsPressed(Keys.D6)) Append(Shift ? '^' : '6');
            if (KeyboardController.IsPressed(Keys.D7)) Append(Shift ? '&' : '7');
            if (KeyboardController.IsPressed(Keys.D8)) Append(Shift ? '*' : '8');
            if (KeyboardController.IsPressed(Keys.D9)) Append(Shift ? '(' : '9');
            if (KeyboardController.IsPressed(Keys.OemSemicolon)) Append(!Shift ? ';' : ':');
            if (KeyboardController.IsPressed(Keys.OemPlus)) Append(!Shift ? '=' : '+');
            if (KeyboardController.IsPressed(Keys.OemComma)) Append(!Shift ? ',' : '<');
            if (KeyboardController.IsPressed(Keys.OemMinus)) Append(!Shift ? '-' : '_');
            if (KeyboardController.IsPressed(Keys.OemPeriod)) Append(!Shift ? '.' : '>');
            if (KeyboardController.IsPressed(Keys.OemQuestion)) Append(!Shift ? '/' : '?');
            if (KeyboardController.IsPressed(Keys.OemPipe)) Append(!Shift ? '\\' : '|');
            if (KeyboardController.IsPressed(Keys.OemQuotes)) Append(!Shift ? '\'' : '@');
            if (KeyboardController.IsPressed(Keys.Add)) Append('+');
            if (KeyboardController.IsPressed(Keys.Subtract)) Append('-');
            if (KeyboardController.IsPressed(Keys.Multiply)) Append('*');
            if (KeyboardController.IsPressed(Keys.Divide)) Append('/');
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color = isFocused ? Color.White : Color.White * 0.7f;
            if (background is not null) spriteBatch.Draw(background, new Rectangle((int)(position.X - originOffset.X), (int)position.Y, characterLimit * charSize, height), color);

            string _text = $"{text}{(caret && isFocused ? "|" : "")}";
            Label label = new(_text, fontScale, position - originOffset, color, font, Origin.TOP_LEFT, 0);
            label.DrawWithShadow(spriteBatch);
        }

        public Textbox(Vector2 position, SpriteFont font, Texture2D background, int characterLimit, float fontScale, Origin origin, string placeholder = "")
        {
            this.position = position;
            this.font = font;
            this.background = background;
            this.characterLimit = characterLimit + 5;

            //Get the longest character size
            charSize = int.MinValue;
            height = int.MinValue;

            byte ascii = 0;
            for (byte i = 0; i < byte.MaxValue/2; i++)
            {
                if (font.MeasureString($"{Convert.ToChar(i)}").X > charSize)
                {
                    ascii = i;
                }
            }

            string text = new(Convert.ToChar(ascii), characterLimit+1);
            Vector2 max = font.MeasureString(text);
            charSize = (int)Math.Ceiling(max.X * fontScale) / characterLimit+1;
            height = (int)Math.Ceiling((max.Y * fontScale));

            isFocused = false;
            this.text = new(placeholder);
            isEntered = false;
            this.fontScale = fontScale;
            tick = 0;
            caret = true;
            this.origin = origin;
            originOffset = CalculateOriginOffset();
        }
    }
}