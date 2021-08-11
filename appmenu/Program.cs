using System;
using Psp;

namespace appmenu
{
    class Program
    {
        static void Main(string[] args)
        {
            // we just exist and set the next app...
            State.SetAppName("tet.exe");

            int x = 0;
            int y = 0;

            BasicGraphics2.Init();

            Color fore = Color.FromRGBA(0x82, 0xca, 0xff);
            Color back = Color.FromRGBA(0xff, 0xff, 0);

            while (State.IsRunning())
            {
                BasicGraphics2.Clear(back);

                BasicGraphics2.DrawText("**Press square**", x, y, fore);

                BasicGraphics2.SwapBuffers();

                Controls.PollLatch();

                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_SQUARE))
                {
                    break;
                }

                Display.WaitVblankStart();
            }
        }
    }
}
