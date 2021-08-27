using Psp;

namespace testSimple
{
    // If you are using the app menu, you really can't mix
    // graphics libs... you must use BasicGraphics2...
    class Program
    {
        static void Main()
        {
            int x = 0, x2 = 50;
            int y = 0, y2 = 50;

            BasicGraphics2.Init();

            Color fore = Color.FromRGBA(0x82, 0xca, 0xff);
            Color back = Color.FromRGBA(0xff, 0, 0xff);
            Color mask = Color.FromRGBA(0xff, 0xff, 0xff);

            var surface = BasicGraphics2.CreateSurface("res/test.bmp", true, mask);

            var texture = BasicGraphics2.CreateTexture(surface);

            while (State.IsRunning())
            {
                BasicGraphics2.Clear(back);

                BasicGraphics2.DrawTexture(texture, x2, y2, 50, 50);

                BasicGraphics2.DrawRect(x, y, 10, 10, fore);

                BasicGraphics2.DrawText($"{surface.Handle.ToInt32()}", 10, 10, fore);
                BasicGraphics2.DrawText($"{texture.Handle.ToInt32()}", 10, 20, fore);
                BasicGraphics2.DrawText($"{System.IntPtr.Zero.ToInt32()}", 10, 30, fore);

                BasicGraphics2.SwapBuffers();

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

                //process held keys
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
                {
                    y += 2;
                }

                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_SQUARE))
                {
                    x2 -= 1;
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_CIRCLE))
                {
                    x2 += 1;
                }

                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_TRIANGLE))
                {
                    y2 -= 1;
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_CROSS))
                {
                    y2 += 1;
                }

                

                if (Controls.IsKeyHeld(PspCtrlButtons.PSP_CTRL_START))
                {
                    fore.B = 0x55;
                }
                else
                {
                    fore.B = 0xff;
                }

                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_SELECT))
                {
                    break;
                }

                Display.WaitVblankStart();
            }
        }
    }
}
