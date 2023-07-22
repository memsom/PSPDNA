using System;
using System.Collections.Generic;
using Psp;

// If you are using the app menu, you really can't mix
// graphics libs... you must use BasicGraphics2...
namespace tet
{
    // This is a fairly rough translation of 
    // https://github.com/superjer/tinyc.games/blob/master/tet/tet.c
    // To C#. All of the SDL calls int he original are converted to 
    // BasicGraphics calls.
    //
    // The code was raken from the TinyC.Games repo:
    // https://github.com/superjer/tinyc.games
    //
    // It was chosen because it was a pretty simple conversion and 
    // didn't require too much extra work, as most of the graphics
    // are rendered as blocks.
    //
    // TODO: text output is not done.
    public class Tet
    {
        const int BWIDTH = 6;  // board width, height
        const int BHEIGHT = 12;
        const int BS = 18;      // size of one block
        const int BS2 = (BS / 2); // size of half a block
        const int PREVIEW_BOX_X = (10 + BS * BWIDTH + 10 + BS2);
        static int MAX(int a, int b) => ((a) > (b) ? (a) : (b));

        readonly static string shapes = (
            ".... .... .... .... .... .... .... .O.. " +
            ".... .OO. .OO. .OO. ..O. .O.. .... .O.. " +
            ".... .O.. ..O. .OO. .OO. .OO. OOO. .O.. " +
            ".... .O.. ..O. .... .O.. ..O. .O.. .O.. " +
            ".... .... .... .... .... .... .... .... " +
            ".... .O.. ..O. .OO. OO.. .OO. .O.. .... " +
            ".... .OOO OOO. .OO. .OO. OO.. .OO. OOOO " +
            ".... .... .... .... .... .... .O.. .... " +
            ".... .... .... .... .... .... .... .O.. " +
            ".... ..O. .O.. .OO. ..O. .O.. .O.. .O.. " +
            ".... ..O. .O.. .OO. .OO. .OO. OOO. .O.. " +
            ".... .OO. .OO. .... .O.. ..O. .... .O.. " +
            ".... .... .... .... .... .... .... .... " +
            ".... .OOO OOO. .OO. OO.. .OO. .O.. .... " +
            ".... ...O O... .OO. .OO. OO.. OO.. OOOO " +
            ".... .... .... .... .... .... .O.. .... "
            );

        readonly static int[] center = { // helps center shapes in preview box
            0,0, 0,0, 0,0, 0,1, 0,0, 0,0, 1,-1,1,1,
        };

        readonly static byte[] colors = {
                0,     0,   0, // unused
                242, 245, 237, // J-piece
                255, 194,   0, // L-piece
                15,  127, 127, // square
                255,  91,   0, // Z
                184,   0,  40, // S
                74,  192, 242, // T
                132,   0,  46, // line-piece
                255, 255, 255, // shine color
        };

        static List<byte[]> board;
        readonly static int[] killy_lines = new int[BHEIGHT];

        static int falling_x = 0;
        static int falling_y = 0;
        static int falling_shape = 0;
        static int falling_rot = 0;
        static int next_shape = 0;
        static int lines = 0;
        static int score = 0;
        static int best = 0;
        static int idle_time = 0;
        static int shine_time = 0;
        static int dead_time = 0;

        static Random rand;

        //the entry point and main game loop
        static void Main()
        {
            Psp.Debug.WriteLine("Starting main");
            Setup();
            New_game();

            while (State.IsRunning())
            {
                //Display.WaitVblankStart();
                Draw_stuff();
                BasicGraphics2.SwapBuffers();

                Controls.PollLatch();
                Key_down();
                Update_stuff();
                
                ///Display.WaitVblankStart();

                idle_time++;
            }
        }

        //initial setup to get the window and rendering going
        static void Setup()
        {
            Psp.Debug.WriteLine("Starting Setup");

            rand = new Random();

            Psp.Debug.WriteLine("Init graphics");
            BasicGraphics2.Init();
            Psp.Debug.WriteLine("Exit setup");
        }

        //handle a key press from the player
        static public void Key_down()
        {
            Psp.Debug.WriteLine("Start keydown");
            if (falling_shape > 0)
            {
                if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_LEFT))
                {
                    Move(-1, 0);
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_RIGHT))
                {
                    Move(1, 0);
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_UP))
                {
                    Slam();
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_DOWN))
                {
                    Move(0, 1);
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_CROSS))
                {
                    Spin(-1);
                }
                else if (Controls.IsKeyDown(PspCtrlButtons.PSP_CTRL_CIRCLE))
                {
                    Spin(1);
                }
            }
        }

        //update everything that needs to update on its own, without input
        static void Update_stuff()
        {
            if (!(falling_shape > 0) && !(shine_time > 0) && !(dead_time > 0))
            {
                New_piece();
                falling_x = 3;
                falling_y = -3;
                falling_rot = 0;
            }

            if (shine_time > 0)
            {
                shine_time--;
                if (shine_time == 0)
                {
                    Kill_lines();
                }
            }

            if (dead_time > 0)
            {
                int x = (dead_time) % BWIDTH;
                int y = (dead_time) / BWIDTH;

                if (y >= 0 && y < BHEIGHT && x >= 0 && x < BWIDTH)
                    board[y][x] = (byte)(rand.Next() % 7 + 1);

                if (--dead_time == 0)
                {
                    New_game();
                }
            }

            if (idle_time >= 50)
            {
                Move(0, 1);
            }
        }

        //reset score and pick one extra random piece
        static void New_game()
        {
            Psp.Debug.WriteLine("Starting new_game");
            //memset(board, 0, sizeof board);
            //board = new byte[BHEIGHT * BWIDTH];
            board = new List<byte[]>();
            for (int i = 0; i < BHEIGHT; i++)
            {
                board.Add(new byte[BWIDTH]);
            }

            New_piece();
            if (best < score) best = score;
            score = 0;
            lines = 0;
            falling_shape = 0;
            Psp.Debug.WriteLine("Exit new_game");
        }

        //randomly pick a new next piece, and put the old on in play
        static void New_piece()
        {
            falling_shape = next_shape;
            next_shape = (byte)(rand.Next() % 7 + 1); // 7 shapes
        }

        //move the falling piece left, right, or down
        static void Move(int dx, int dy)
        {
            if (!(Collide(falling_x + dx, falling_y + dy, falling_rot) > 0))
            {
                falling_x += dx;
                falling_y += dy;
            }
            else if (dy > 0)
            {
                Bake();
                falling_shape = 0;
            }

            if (dy > 0)
            {
                idle_time = 0;
            }
        }

        //check if a sub-part of the falling shape is solid at a particular rotation
        static bool Is_solid_part(int shape, int rot, int i, int j)
        {
            int bases = shape * 5 + rot * 5 * 8 * 4;
            return shapes[bases + j * 5 * 8 + i] == 'O';
        }

        //check if the falling piece would collide at a certain position and rotation
        static int Collide(int x, int y, int rot)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int world_i = i + x;
                    int world_j = j + y;

                    if (!Is_solid_part(falling_shape, rot, i, j))
                    {
                        continue;
                    }

                    if (world_i < 0 || world_i >= BWIDTH || world_j >= BHEIGHT)
                    {
                        return 1;
                    }

                    if (world_j < 0)
                    {
                        continue;
                    }

                    if (board[world_j][world_i] > 0)
                    {
                        return 1;
                    }
                }
            }

            return 0;
        }

        //bake the falling piece into the background/board
        static void Bake()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int world_i = i + falling_x;
                    int world_j = j + falling_y;

                    if (!Is_solid_part(falling_shape, falling_rot, i, j))
                    {
                        continue;
                    }

                    if (world_i < 0 || world_i >= BWIDTH || world_j < 0 || world_j >= BHEIGHT)
                    {
                        continue;
                    }

                    if (board[world_j][world_i] > 0) // already a block here? game over
                    {
                        dead_time = BWIDTH * BHEIGHT;
                        next_shape = 0;
                    }

                    board[world_j][world_i] = (byte)falling_shape;
                }
            }

            //check if there are any completed horizontal lines
            for (int j = BHEIGHT - 1; j >= 0; j--)
            {
                for (int i = 0; i < BWIDTH && board[j][i] > 0; i++)
                {
                    if (i == BWIDTH - 1) Shine_line(j);
                }
            }
        }

        //make a completed line "shine" and mark it to be removed
        static void Shine_line(int y)
        {
            shine_time = 50;
            killy_lines[y] = 1;
            for (int i = 0; i < BWIDTH; i++)
            {
                board[y][i] = 8; //shiny!
            }
        }

        //remove lines that were marked to be removed by shine_line()
        static void Kill_lines()
        {
            int new_lines = 0;
            for (int y = 0; y < BHEIGHT; y++)
            {
                if (!(killy_lines[y] > 0))
                    continue;

                lines++;
                new_lines++;
                killy_lines[y] = 0;

                //board = new byte[BHEIGHT, BWIDTH];

                for (int j = y; j > 0; j--)
                {
                    for (int i = 0; i < BWIDTH; i++)
                    {
                        board[j][i] = board[(j - 1)][i];
                    }
                }
            }

            switch (new_lines)
            {
                case 1: score += 100; break;
                case 2: score += 250; break;
                case 3: score += 500; break;
                case 4: score += 1000; break;
            }
        }

        //move the falling piece as far down as it will go
        static void Slam()
        {
            for (; !(Collide(falling_x, falling_y + 1, falling_rot) > 0); falling_y++)
            {
                idle_time = 0;
            }
        }

        //spin the falling piece left or right, if possible
        static void Spin(int dir) //TODO - work out why dir is no longer used
        {
            int new_rot = (falling_rot + 1) % 4;

            if (!(Collide(falling_x, falling_y, new_rot) > 0))
            {
                falling_rot = new_rot;
            }
            else if (!(Collide(falling_x - 1, falling_y, new_rot) > 0))
            {
                falling_x -= 1;
                falling_rot = new_rot;
            }
        }

        static Color back = Color.FromRGBA(25, 40, 35);
        static Color box = Color.FromRGBA(0, 0, 0);
        static Color other = Color.FromRGBA(8, 13, 12);

        //draw everything in the game on the screen
        static void Draw_stuff()
        {
            BasicGraphics2.Clear(back);
            BasicGraphics2.DrawRect(10, 10, BS * BWIDTH, BS * BHEIGHT, box);
            BasicGraphics2.DrawRect(10 + BS * BWIDTH + 10, 10, BS * 5, BS * 5, box);

            //draw falling piece & shadow
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int world_i = i + falling_x;
                    int world_j = j + falling_y;
                    int shadow_j = MAX(world_j + 1, 0);

                    if (!Is_solid_part(falling_shape, falling_rot, i, j))
                    {
                        continue;
                    }

                    BasicGraphics2.DrawRect(
                        10 + BS * world_i,
                        10 + BS * shadow_j,
                        BS,
                        BS * (BHEIGHT - shadow_j),
                        other
                    );

                    if (world_j >= 0)
                    {
                        Draw_square(10 + BS * world_i, 10 + BS * world_j, falling_shape);
                    }
                }
            }

            //draw next piece, centered in the preview box
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Is_solid_part(next_shape, 0, i, j))
                    {
                        Draw_square(
                                PREVIEW_BOX_X + BS * i + BS2 * center[2 * next_shape],
                                10 + BS * j + BS2 * center[2 * next_shape + 1],
                                next_shape
                        );
                    }
                }
            }

            //draw board pieces
            for (int i = 0; i < BWIDTH; i++)
            {
                for (int j = 0; j < BHEIGHT; j++)
                {
                    if (board[j][i] > 0)
                    {
                        Draw_square(10 + BS * i, 10 + BS * j, board[j][i]);
                    }
                }
            }

            //draw counters and instructions
            // we re do this as the original layout doe not work for us on the PSP screen
            int tx = 10 + BS * BWIDTH + 10;
            int ty = 10 + BS * 5 + 10;
            Text("Lines:", tx, ty);
            Text(lines.ToString(), tx, ty + 15);
            Text("Score:", tx, ty + 30);
            Text(score.ToString(), tx, ty + 45);
            Text("Best:", tx, ty + 60);
            Text(best.ToString(), tx, ty + 75);
        }

        //draw a single square/piece of a shape
        static void Draw_square(int x, int y, int shape)
        {
            Color col1 = Set_color_from_shape(shape, -25);
            BasicGraphics2.DrawRect(x, y, BS, BS, col1);
            Color col2 = Set_color_from_shape(shape, 0);
            BasicGraphics2.DrawRect(1 + x, 1 + y, BS - 2, BS - 2, col2);
        }

        //set the current draw color to the color assoc. with a shape
        static Color Set_color_from_shape(int shape, int shade)
        {
            byte r = (byte)MAX(colors[shape * 3 + 0] + shade, 0);
            byte g = (byte)MAX(colors[shape * 3 + 1] + shade, 0);
            byte b = (byte)MAX(colors[shape * 3 + 2] + shade, 0);
            return Color.FromRGBA(r, g, b);
        }

        //render a centered line of text optionally with a %d value in it
        static void Text(string str, int x, int y)
        {
            BasicGraphics2.DrawText(
                str,
                x,
                y,
                new Color
                {
                    A = 255,
                    R = 80,
                    G = 90,
                    B = 85
                });
        }
    }
}
