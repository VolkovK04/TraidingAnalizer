using System;
using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApp3
{
    internal static class Drawing
    {
        public static void Draw(this Image img, List<Candle> data) //оно как-то рисует, и лучше не трогать
        {
            int w = img.Width;
            int h = img.Height;
            Graphics g = Graphics.FromImage(img);
            float minPrice = float.MaxValue;
            float maxPrice = 0;
            int maxVol = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Low < minPrice)
                    minPrice = data[i].Low;
                if (data[i].High > maxPrice)
                    maxPrice = data[i].High;
                if (data[i].Vol > maxVol)
                    maxVol = data[i].Vol;
            }
            minPrice -= 0.3f * (maxPrice - minPrice);
            maxPrice += 0.1f * (maxPrice - minPrice);
            float scaleTime = w / (data.Count + 1.0f);
            float scalePrice = h / (maxPrice - minPrice);
            float scaleVol = 0.3f * h / maxVol;
            float p = 0.8f;
            float p2 = 0.9f;
            for (int i = 0; i < data.Count; i++)
            {
                float delta = data[i].Close - data[i].Open;
                Color color = Color.FromArgb(0, 189, 167);
                Color contuorColor = Color.FromArgb(0, 168, 132);
                Color volColor = Color.FromArgb(146, 210, 204);

                if (delta < 0)
                {
                    color = Color.FromArgb(236, 69, 89);
                    contuorColor = Color.FromArgb(215, 45, 64);
                    volColor = Color.FromArgb(247, 169, 167);
                }
                Brush brush = new SolidBrush(color);
                Pen pen = new Pen(contuorColor, 1);

                Point point = new Point((int)((i + 0.5f - p2 / 2) * scaleTime), (int)(h - data[i].Vol * scaleVol));
                Size size = new Size((int)(scaleTime * p2), (int)(data[i].Vol * scaleVol));

                Rectangle rect = new Rectangle(point, size);
                g.FillRectangle(new SolidBrush(volColor), rect);

                point = new Point((int)((i + 0.5f - p / 2) * scaleTime), (int)(h + (minPrice - Math.Max(data[i].Close, data[i].Open)) * scalePrice));
                size = new Size((int)(scaleTime * p), (int)(Math.Abs(delta) * scalePrice));
                g.DrawLine(pen, (i + 0.5f) * scaleTime, h + (minPrice - data[i].Low) * scalePrice, (i + 0.5f) * scaleTime, h + (minPrice - data[i].High) * scalePrice);
                if (delta != 0)
                {
                    rect = new Rectangle(point, size);
                    g.FillRectangle(brush, rect);
                    g.DrawRectangle(pen, rect);
                }
                else
                    g.DrawLine(pen, point, new PointF(point.X + scaleTime * p, point.Y));
            }
        }

    }
}
