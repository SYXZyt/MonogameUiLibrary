using SpriteFontPlus;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary
{
    public static class SpriteFontLoader
    {
        public static SpriteFont LoadFontFromFile(string fontName, GraphicsDevice graphicsDevice)
        {
            if (!File.Exists(fontName)) throw new FileNotFoundException($"File not found: {fontName}", fontName);

            TtfFontBakerResult fontBakerResult = TtfFontBaker.Bake(File.ReadAllBytes(fontName), 25, 1024, 1024, new[]
            {
                CharacterRange.BasicLatin,
                CharacterRange.Latin1Supplement,
                CharacterRange.LatinExtendedA,
                CharacterRange.Cyrillic,
            });
            return fontBakerResult.CreateSpriteFont(graphicsDevice);
        }
    }
}