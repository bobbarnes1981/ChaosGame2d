using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChaosGame2d
{
    class Program
    {
        private static Surface m_video;

        private static float m_elapsed = 0.0f;
        private static float m_step = 0.01f;

        private static int m_width = 640;
        private static int m_height = 480;

        private static IDrawing m_drawing;

        static void Main(string[] args)
        {
            m_video = Video.SetVideoMode(m_width, m_height, 32, false, false, false, true);

//            m_drawing = new SierpinskiGasket(m_video);
            m_drawing = new BarnsleyFern(m_video);

            Events.Quit += new EventHandler<QuitEventArgs>(ApplicationQuitEventHandler);
            Events.Tick += new EventHandler<TickEventArgs>(ApplicationTickEventHandler);
//            Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(ApplicationKeyboardEventHandler);
//            Events.KeyboardUp += new EventHandler<KeyboardEventArgs>(ApplicationKeyboardEventHandler);
//            Events.MouseButtonDown += new EventHandler<MouseButtonEventArgs>(ApplicationMouseEventHandler);
            Events.Run();
        }

        private static void ApplicationTickEventHandler(object sender, TickEventArgs args)
        {
            m_elapsed += args.SecondsElapsed;
            Video.WindowCaption = string.Format("ChaosGame2d [FPS: {0} | Elapsed: {1} | Points: {2}]", args.Fps, m_elapsed, m_drawing.NumberOfPoints);

            if (m_elapsed > m_step)
            {
                m_drawing.Update();
                m_drawing.Draw();

                m_video.Update();

                m_elapsed -= m_step;
            }
        }

        private static void ApplicationQuitEventHandler(object sender, QuitEventArgs args)
        {
            Events.QuitApplication();
        }
    }
}
