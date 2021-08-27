using Psp;

namespace Flappy
{
    // If you are using the app menu, you really can't mix
    // graphics libs... you must use BasicGraphics2...
    class Program
    {
        const int W = 480;
        const int H = 272;
        const int GROUND = 80;
        const int PIPE_W = 86;
        const int PHYS_W = (W + PIPE_W + 80);
        const int GAP = 220;
        const int GRACE = 4;

        static System.Random r;
        static int rand() => r.Next();
        static int RANDOM_PIPE_HEIGHT => (rand() % (H - GROUND - GAP - 120) + 60);
        const int PLYR_X = 80;
        const int PLYR_SZ = 60;

        enum gamestates { READY, ALIVE, GAMEOVER }

        static gamestates gamestate = gamestates.READY;

        static float player_y = (H - GROUND) / 2;
        static float player_vel;
        static int[] pipe_x = { W, W };
        static float[] pipe_y = new float[2];
        static int score;
        static int best;
        static int idle_time = 30;
        static float frame = 0;

        static Texture pillar;

        static Surface background_surface;
        static Texture background;
        static Texture[] bird = new Texture[4];
        //TTF_Font *font;

        static void Setup()
        {
            BasicGraphics2.Init();

            r = new System.Random(); // TODO: need to make a seed....

            var mask = Color.FromRGBA(0xff, 0xff, 0);

            //var p_surf = BasicGraphics2.CreateSurface("res/pillar.bmp", true, mask);
            //pillar = BasicGraphics2.CreateTexture(p_surf);

            background_surface = BasicGraphics2.CreateSurface("res/background.bmp", false);
            background = BasicGraphics2.CreateTexture(background_surface);

            for (int i = 0; i < 4; i++)
            {
                var surf = BasicGraphics2.CreateSurface($"res/bird-{i}.bmp", true, mask);
                bird[i] = BasicGraphics2.CreateTexture(surf);
            }

            // TTF_Init();
            // font = TTF_OpenFont("res/LiberationMono-Regular.ttf", 42);
        }

        // start a new game
        static void New_game()
        {
            gamestate = gamestates.ALIVE;
            player_y = (H - GROUND) / 2;
            player_vel = -11.7f;
            score = 0;
            // pipe_x[0] = PHYS_W + PHYS_W / 2 - PIPE_W;
            // pipe_x[1] = PHYS_W - PIPE_W;
            // pipe_y[0] = RANDOM_PIPE_HEIGHT;
            // pipe_y[1] = RANDOM_PIPE_HEIGHT;
        }

        // when we hit something
        static void Game_over()
        {
            gamestate = gamestates.GAMEOVER;
            idle_time = 0;
            if (best < score) best = score;
        }

        // update everything that needs to update on its own, without input
        static void Update_stuff()
        {
            if (gamestate != gamestates.ALIVE)
            {
                return;
            }

            player_y += player_vel;
            player_vel += 0.61f; // gravity

            if (player_vel > 10.0f)
            {
                frame = 0;
            }
            else
            {
                frame -= (player_vel - 10.0f) * 0.03f; //fancy animation
            }

            if (player_y > H - GROUND - PLYR_SZ)
            {
                Game_over();
            }

            // for (int i = 0; i < 2; i++)
            // {
            //     Update_pipe(i);
            // }
        }

        static void Update_pipe(int i)
        {
            if (PLYR_X + PLYR_SZ >= pipe_x[i] + GRACE && PLYR_X <= pipe_x[i] + PIPE_W - GRACE &&
                (player_y <= pipe_y[i] - GRACE || player_y + PLYR_SZ >= pipe_y[i] + GAP + GRACE))
            {
                Game_over(); // player hit pipe
            }

            // move pipes and increment score if we just passed one
            pipe_x[i] -= 5;
            if (pipe_x[i] + PIPE_W < PLYR_X && pipe_x[i] + PIPE_W > PLYR_X - 5)
            {
                score++;
            }

            // respawn pipe once far enough off screen
            if (pipe_x[i] <= -PIPE_W)
            {
                pipe_x[i] = PHYS_W - PIPE_W;
                pipe_y[i] = RANDOM_PIPE_HEIGHT;
            }
        }

        static int framecounter = 0;
        //draw everything in the game on the screen
        static void Draw_stuff()
        {
            BasicGraphics2.Clear(white);

            // because the screen is not the size that the app expects, this might go horribly wrong.
            BasicGraphics2.DrawTexture(background, 0, 0, W, H);

            // for (int i = 0; i < 2; i++)
            // {
            //     int lower = (int)(pipe_y[i] + GAP);

            //     // we can't do this yet...
            //     //SDL_RenderCopy(renderer, pillar, NULL, &(SDL_Rect){ pipe_x[i], pipe_y[i] - H, PIPE_W, H});
            //     //SDL_Rect src = { 0, 0, 86, H - lower - GROUND };
            //     //SDL_RenderCopy(renderer, pillar, &src, &(SDL_Rect){ pipe_x[i], lower, PIPE_W, src.h});
            // }

            //draw player
            var birdTexture = bird[(int)frame % 4];

            BasicGraphics2.DrawText($"GS : {gamestate}", 10, 0, black);

            BasicGraphics2.DrawTexture(birdTexture, PLYR_X, (int)player_y, PLYR_SZ, PLYR_SZ);

            if (gamestate != gamestates.READY)
            {
                //Text($"{score}", 10);
            }
            if (gamestate == gamestates.READY)
            {
                //Text("Press any key", 150);
            }
            if (gamestate == gamestates.GAMEOVER)
            {
                //Text("High score: {best}", 150);
            }

            BasicGraphics2.DrawText($"FC : {framecounter++}", 10, 10, black);

            BasicGraphics2.SwapBuffers();
        }

        static Color black = Color.FromRGBA(0, 0, 0);
        static Color white = Color.FromRGBA(255, 255, 255);

        static void Text(string fstr, int height)
        {
            // TODO - 
            //BasicGraphics2.DrawTextCentered(fstr, W, height, black);

            BasicGraphics2.DrawText(fstr, 0, 0, black);
        }

        static void Main()
        {
            Setup();

            while (State.IsRunning())
            {
                Controls.PollLatch();

                // this is th key we will use to make the bird action for now...
                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_UP))
                {
                    if (gamestate == gamestates.ALIVE)
                    {
                        player_vel = -11.7f;

                        frame += 1.0f;

                        // I don't know how this worked in the C version, 
                        // but this basically exploded in the C#
                        if (frame == 5.0f)
                        {
                            frame = 0;
                        }

                    }
                    else if (idle_time > 30)
                    {
                        New_game();
                    }
                }

                //Update_stuff();
                Draw_stuff();

                //Display.WaitVblankStart();

                //System.Threading.Thread.Sleep(1000 / 60); // was: SDL_Delay(1000 / 60);
                idle_time++;
            }
        }
    }
}
