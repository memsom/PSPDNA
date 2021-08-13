using System;
using System.Collections.Generic;
using Psp;

namespace appmenu
{
    // If you are using the app menu, you really can't mix
    // graphics libs... you must use BasicGraphics2...

    class Program
    {
        static void Main(string[] args)
        {
            // we just exist and set the next app...
            State.SetAppName(string.Empty); // unless we set something, we should not continue

            BasicGraphics2.Init();

            Color back = Color.FromRGBA(0x82, 0xca, 0xff);
            Color fore = Color.FromRGBA(0xff, 0xff, 0);
            Color selection = Color.FromRGBA(0x5, 0xff, 0x5);

            // we don't seem to be able to filter - if we do we only get the running exe
            var files = new List<string>();
            var rawfiles = System.IO.Directory.GetFiles("./apps");
            foreach(var file in rawfiles)
            {
                var tfile = file.ToLower(); // seems like filenales are all upppercase
                if(tfile.EndsWith(".exe"))
                files.Add(tfile);
            }

            // if there are no files, we still try to load the default app
            if (files.Count == 0)
            {
                State.SetAppName("app.exe");
            }
            else
            {
                // we found some files, so we display the names

                var selected = 0;

                while (State.IsRunning())
                {
                    // this seems to mess with the graphics somewhat
                    //Display.WaitVblankStart();

                    BasicGraphics2.Clear(back);

                    BasicGraphics2.DrawText($"DNA PSP APP MENU V1.0 (found {files.Count} files) [Cross runs, Square exits.]", 1, 1, fore);

                    // this does not handle more utems that we can show in the screen currently...
                    for (var i = 0; i < files.Count; i++)
                    {
                        var y = (i + 1) * 15;
                        var tfile = System.IO.Path.GetFileNameWithoutExtension(files[i]);
                        BasicGraphics2.DrawText(tfile, 10, y, fore);
                        if (i == selected)
                        {
                            BasicGraphics2.DrawRect(1, y + 5, 5, 5, selection);
                        }
                    }

                    BasicGraphics2.SwapBuffers();

                    Controls.PollLatch();

                    if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_UP))
                    {
                        selected -= 1;
                        if (selected < 0)
                        {
                            selected = 0;
                        }
                    }

                    if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_DOWN))
                    {
                        selected += 1;
                        if (selected > (files.Count - 1))
                        {
                            selected = files.Count - 1;
                        }
                    }

                    if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_CROSS))
                    {
                        State.SetAppName(files[selected]);
                        break;
                    }

                    if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_SQUARE))
                    { 
                        break;
                    }
                }
            }
        }
    }
}
