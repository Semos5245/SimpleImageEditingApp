using System;
using System.Drawing;

namespace Semi_Photoshop.Models
{
    public class ColorConverter
    {
        public static Color FromHSLToRGB(HSLColor hslColor)
        {
            return FromHSLToRGBPrivate(hslColor.H, hslColor.S, hslColor.L);
        }

        public static Color FromHSLToRGB(float h, float l, float s)
        {
            return FromHSLToRGBPrivate(h, s, l);
        }

        public static HSLColor FromRGBToHSL(Color rgbColor)
        {
            return FromRGBToHSLPrivate(rgbColor.R, rgbColor.G, rgbColor.B);
        }

        public static HSLColor FromRGBToHSL(byte r, byte g, byte b)
        {
            return FromRGBToHSLPrivate(r, g, b);
        }

        private static Color FromHSLToRGBPrivate(float hue, float saturation, float luminous)
        {
            float r = 0, g = 0, b = 0;

            if (saturation == 0)
            {
                r = g = b = (int)(luminous / 100 * 255);
                return Color.FromArgb((int)r, (int)g, (int)b);
            }

            saturation = saturation / 100;
            luminous = luminous / 100;

            var temp1 = luminous * (1 + saturation);

            temp1 = temp1 < 0 ? 1 - temp1 : temp1;
            temp1 = temp1 > 1 ? temp1 - 1 : temp1;

            if (luminous >= 0.5) temp1 = luminous + saturation - (luminous * saturation);

            temp1 = temp1 < 0 ? 1 - temp1 : temp1;
            temp1 = temp1 > 1 ? temp1 - 1 : temp1;

            var temp2 = 2 * luminous - temp1;

            temp2 = temp2 < 0 ? 1 - temp2 : temp2;
            temp2 = temp2 > 1 ? temp2 - 1 : temp2;

            hue = hue / 360;

            hue = hue < 0 ? 1 - hue : hue;
            hue = hue > 1 ? hue - 1 : hue;

            var tempR = hue + 0.333f;

            tempR = tempR < 0 ? 1 - tempR : tempR;
            tempR = tempR > 1 ? tempR - 1 : tempR;

            var tempG = hue;

            var tempB = hue - 0.333f;

            tempR = tempR < 0 ? 1 - tempR : tempR;
            tempR = tempR > 1 ? tempR - 1 : tempR;

            //Get the red component
            if (6 * tempR < 1) r = temp2 + (temp1 - temp2) * 6 * tempR;
            else if (6 * tempR > 1)
            {
                if (2 * tempR < 1)
                {
                    r = temp1;
                }
                else if (2 * tempR > 1)
                {
                    if (3 * tempR < 2)
                    {
                        r = temp2 + (temp1 - temp2) * (0.666f - tempR) * 6;
                    }
                    else if (3 * tempR > 2) r = temp2;
                }
            }

            //Get the blue component
            if (6 * tempB < 1) b = temp2 + (temp1 - temp2) * 6 * tempB;
            else if (6 * tempB > 1)
            {
                if (2 * tempB < 1)
                {
                    b = temp1;
                }
                else if (2 * tempB > 1)
                {
                    if (3 * tempB < 2)
                    {
                        b = tempB + (temp1 - temp2) * (0.666f - tempB) * 6;
                    }
                    else if (3 * tempB > 2) b = temp2;
                }
            }

            //Get the green component
            if (6 * tempG < 1) g = temp2 + (temp1 - temp2) * 6 * tempG;
            else if (6 * tempG > 1)
            {
                if (2 * tempG < 1)
                {
                    g = temp1;
                }
                else if (2 * tempG > 1)
                {
                    if (3 * tempG < 2)
                    {
                        g = temp2 + (temp1 - temp2) * (0.666f - tempG) * 6;
                    }
                    else if (3 * tempG > 2) g = temp2;
                }
            }

            r = r * 255;
            g = g * 255;
            b = b * 255;

            return Color.FromArgb(Convert.ToInt32(Math.Ceiling(r)), Convert.ToInt32(Math.Ceiling(g)), Convert.ToInt32(Math.Ceiling(b)));
        }

        private static HSLColor FromRGBToHSLPrivate(float red, float green, float blue)
        {
            float h = 0;
            float s = 0;
            float l = 0;

            float r = Convert.ToSingle(red);
            float g = Convert.ToSingle(green);
            float b = Convert.ToSingle(blue);

            r /= 255f;
            g /= 255f;
            b /= 255f;

            var max = Math.Max(r, Math.Max(g, b));
            var min = Math.Min(r, Math.Min(g, b));
            var difference = max - min;

            if (difference == 0) h = 0;
            else if (max == r) h = (float)((g - b) / difference /*% 6*/);
            else if (max == g) h = (float)(((b - r) / difference)) + 2;
            else h = ((r - g) / difference) + 4;

            h *= 60;
            if (h < 0) h += 360;
            l = (max + min) / 2;

            if (l < 0.5)
            {
                s = difference / (max + min);
            }
            else if (l > 0.5)
            {
                s = difference / (2 - difference);
            }

            s *= 100;
            l *= 100;

            return new HSLColor { H = h, L = l, S = s};
        }
    }
}
