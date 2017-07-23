using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChaosGame2d
{
    class SierpinskiGasket : IDrawing
    {
        private Random m_random;

        private int m_numberOfPoints;
        private Vector[] m_points;

        private int m_numberOfDrawing;
        private List<Vector> m_drawing;

        private Surface m_surface;

        private short m_radius;

        public int NumberOfPoints
        {
            get { return m_numberOfDrawing; }
        }

        public SierpinskiGasket(Surface surface)
        {
            m_radius = 1;

            m_surface = surface;

            m_random = new Random();

            m_numberOfPoints = 3;
            m_points = new Vector[m_numberOfPoints];
            for (int p = 0; p < m_numberOfPoints; p++)
            {
                m_points[p] = new Vector(m_random.NextDouble() * m_surface.Width, m_random.NextDouble() * m_surface.Height, 0);
            }

            m_numberOfDrawing = 1;
            m_drawing = new List<Vector>(m_numberOfDrawing);
            m_drawing.Add(new Vector(m_random.NextDouble() * m_surface.Width, m_random.NextDouble() * m_surface.Height, 0));
        }

        public void Update()
        {
            m_numberOfDrawing = m_drawing.Count;

            // generate choice of points
            int towards = m_random.Next(0, 3);

            // move towards choice
            Vector original = m_drawing[m_numberOfDrawing - 1];
            Vector current = m_points[towards] - original;
            current = original + (current / 2);

            // append to drawing vectors
            m_drawing.Add(current);
        }

        public void Draw()
        {
            foreach (Vector p in m_points)
            {
                m_surface.Draw(new SdlDotNet.Graphics.Primitives.Circle(new Point((int)p.X, (int)p.Y), m_radius), Color.Red, false, true);
            }

            foreach (Vector d in m_drawing)
            {
                m_surface.Draw(new SdlDotNet.Graphics.Primitives.Circle(new Point((int)d.X, (int)d.Y), m_radius), Color.Blue, false, true);
            }
        }
    }
}
