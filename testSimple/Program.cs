using Psp;

namespace testSimple
{
    class Program
    {
        static void Main()
        {
            int x = 0;
            int y = 0;

            BasicGraphics.Init();

            Color fore = Color.FromRGBA(0x82, 0xca,  0xff ); 
            Color back = Color.FromRGBA(0xff, 0xff, 0);

            while (State.IsRunning())
            {
                BasicGraphics.Clear(back);

                BasicGraphics.DrawRect(x, y, 10, 10, fore);

                BasicGraphics.SwapBuffers();

                Controls.PollLatch();

                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_LEFT))
                {
                    x -= 1;
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_RIGHT))
                {
                    x += 1;
                }

                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_UP))
                {
                    y -= 1;
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_DOWN))
                {
                    y += 1;
                }

                //process held heys
                if (Controls.IsKeyHeld(PspCtrlButtons.PSP_CTRL_LEFT))
                {
                    x -= 2;
                }
                else if (Controls.IsKeyHeld(PspCtrlButtons.PSP_CTRL_RIGHT))
                {
                    x += 2;
                }

                if (Controls.IsKeyHeld(PspCtrlButtons.PSP_CTRL_UP))
                {
                    y -= 2;
                }
                else if (Controls.IsKeyHeld(PspCtrlButtons.PSP_CTRL_DOWN))
                    y += 2;

                if (Controls.IsKeyHeld(PspCtrlButtons.PSP_CTRL_CROSS))
                {
                    fore.B = 0x55;
                }
                else
                {
                    fore.B = 0xff;
                }

                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_SQUARE))
                {
                    break;
                }

                Display.WaitVblankStart();
            }
        }
    }
}
