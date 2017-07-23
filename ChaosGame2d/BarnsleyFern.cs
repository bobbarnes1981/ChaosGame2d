using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChaosGame2d
{
    class BarnsleyFern : IDrawing
    {
        private Random m_random;

        private int m_numberOfDrawing;
        private List<Vector> m_drawing;

        private Surface m_surface;

        private float m_offsetx;
        private float m_offsety;

        private float m_multx;
        private float m_multy;

        private short m_radius;

        public int NumberOfPoints
        {
            get { return m_numberOfDrawing; }
        }

        public BarnsleyFern(Surface surface)
        {
            m_radius = 1;

            m_surface = surface;

            m_multx = m_surface.Width / 6;
            m_multy = m_surface.Height / 10;

            m_offsetx = 3;
            m_offsety = 0;

            m_random = new Random();

            m_numberOfDrawing = 1;
            m_drawing = new List<Vector>(m_numberOfDrawing);
            m_drawing.Add(new Vector(0, 0, 0));
        }

        public void Update()
        {
            m_numberOfDrawing = m_drawing.Count;

            Vector lastVector = m_drawing[m_drawing.Count - 1];

            int chance = m_random.Next(0, 100);

            Vector current;

            if (chance < 1)  // 1%
            {
                current = f1(lastVector);
            }
            else if (chance < 8) // 7%
            {
                current = f3(lastVector);
            }
            else if (chance < 15) // 14%
            {
                current = f4(lastVector);
            }
            else // 85%
            {
                current = f2(lastVector);
            }

            m_drawing.Add(current);
        }

        private Vector f1(Vector lastVector)
        {
            // xn + 1 = 0
            // yn + 1 = 0.16 yn
            return new Vector(
                0,
                0.16 * lastVector.Y,
                0);
        }

        private Vector f2(Vector lastVector)
        {
            // xn + 1 = 0.85 xn + 0.04 yn
            // yn + 1 = −0.04 xn + 0.85 yn + 1.6
            return new Vector(
                (0.85 * lastVector.X) + (0.04 * lastVector.Y),
                (-0.04 * lastVector.X) + (0.85 * lastVector.Y) + 1.6,
                0);
        }

        private Vector f3(Vector lastVector)
        {
            // xn + 1 = 0.2 xn − 0.26 yn
            // yn + 1 = 0.23 xn + 0.22 yn + 1.6
            return new Vector(
                (0.2 * lastVector.X) - (0.26 * lastVector.Y),
                (0.23 * lastVector.X) + (0.22 * lastVector.Y) + 1.6,
                0);
        }

        private Vector f4(Vector lastVector)
        {
            // xn + 1 = −0.15 xn + 0.28 yn
            // yn + 1 = 0.26 xn + 0.24 yn + 0.44
            return new Vector(
                (-0.15 * lastVector.X) + (0.28 * lastVector.Y),
                (0.26 * lastVector.X) + (0.24 * lastVector.Y) + 0.44,
                0);
        }

        public void Draw()
        {
            foreach (Vector d in m_drawing)
            {
                m_surface.Draw(new SdlDotNet.Graphics.Primitives.Circle(new Point((int)((d.X * m_multx) + (m_offsetx * m_multx)), m_surface.Height - (int)((d.Y * m_multy) + (m_offsety * m_multy))), m_radius), Color.Green, false, true);
            }
        }
    }
}
