using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class ToolStripEx : ToolStrip
    {
        private Color _backColor = Color.LightGoldenrodYellow;
        private Color _baseColor = Color.Khaki;
        private Color _borderColor = Color.DarkKhaki;

        public ToolStripEx()
        {
            InitializeComponent();
        }

        private void InitColor()
        {
            INIClass ini = new INIClass(Util.INI_FILE);
            string iniBackColor = ini.IniReadValue("SKIN_COLOR", "BACKCOLOR");
            if (!string.IsNullOrEmpty(iniBackColor))
            {
                _backColor = Color.FromName(iniBackColor);
            }
            string iniBaseColor = ini.IniReadValue("SKIN_COLOR", "BASECOLOR");
            if (!string.IsNullOrEmpty(iniBaseColor))
            {
                _baseColor = Color.FromName(iniBaseColor);
            }
            string iniBorderColor = ini.IniReadValue("SKIN_COLOR", "BORDERCOLOR");
            if (!string.IsNullOrEmpty(iniBorderColor))
            {
                _borderColor = Color.FromName(iniBorderColor);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            InitColor();
            if (this.GripDisplayStyle == ToolStripGripDisplayStyle.Horizontal)
                DrawBackground(g, ClientRectangle, _baseColor, _borderColor, .45F, LinearGradientMode.Horizontal);
            else
                DrawBackground(g, ClientRectangle, _baseColor, _borderColor, .135F, LinearGradientMode.Vertical);
            base.OnPaint(pe);            
        }
        
        internal void DrawBackground(Graphics g, Rectangle rect, Color baseColor, Color borderColor, float basePosition, LinearGradientMode mode)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(rect);
                using (LinearGradientBrush brush = new LinearGradientBrush(
                   rect, Color.Transparent, Color.Transparent, mode))
                {
                    Color[] colors = new Color[4];
                    colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                    colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                    colors[2] = baseColor;
                    colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                    ColorBlend blend = new ColorBlend();
                    blend.Positions =
                        new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                    blend.Colors = colors;
                    brush.InterpolationColors = blend;
                    g.FillPath(brush, path);
                }

                if (baseColor.A > 80)
                {
                    Rectangle rectTop = rect;
                    if (mode == LinearGradientMode.Vertical)
                    {
                        rectTop.Height = (int)(rectTop.Height * basePosition);
                    }
                    else
                    {
                        rectTop.Width = (int)(rect.Width * basePosition);
                    }
                    using (SolidBrush brushAlpha =
                        new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                    {
                        g.FillRectangle(brushAlpha, rectTop);
                    }
                }

                rect.Inflate(-1, -1);
                using (GraphicsPath path1 = new GraphicsPath())
                {
                    path1.AddRectangle(rect);
                    using (Pen pen = new Pen(Color.FromArgb(255, 255, 255)))
                    {
                        g.DrawLines(pen, path1.PathPoints);
                    }
                }

                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawLines(pen, path.PathPoints);
                }
            }
        }

        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }
    }
}
