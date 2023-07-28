using Psp;
using System.Collections.Generic;

namespace Rockbound2
{
    // If you are using the app menu, you really can't mix
    // graphics libs... you must use BasicGraphics2...
    class Program
    {
        /*
        If you don't want to extract the raw audio from the wav files beforehand, you could read the wav directly (if it's uncompressed wav).
        The file format is pretty simple.
        In the first 44 bytes of the file you have the header and all the necessary info like data length, number of channels, ... and the size of the raw data to read.
        */


        int[] playerPos = { 1, 1 };

        int SFX_CHANNEL1 = 0;
        int SFX_CHANNEL2 = 1;

        int[] SND_CHANNELS = {
            0, 1, 2, 3
        };

        bool gameOver = false;
        bool onMenu = true;


        int score = 0;

        int steps = 0;

        int bonusSteps = 180;

        int difficultyLevel = 0;

        int collectedTreasure = 0;

        int level = 0;

        //uint16_t* sound_pickup2;
        //uint16_t* sound_wall;
        //uint16_t* sound_select;
        //uint16_t* sound_tada;

        List<List<char[]>> levels = new List<List<char[]>>
        {
            new List<char[]>
            {new char[] {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'P', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'W', 'T', 'D', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'D', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'W', 'T', 'W', 'D', 'W', 'D', 'W', 'T', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'W', 'D', 'W', 'T', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'W', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'D', 'D', 'D', 'D', 'T', 'W'},
             new char[] {'W', 'D', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'D', 'T', 'W', 'D', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'E', 'W'},
             new char[] {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'}},
            new List<char[]>
            {new char[] {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'P', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'W', 'W'},
             new char[] {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'T', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'W', 'T', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'T', 'W', 'T', 'W', 'D', 'W', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'D', 'T', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'W'},
             new char[] {'W', 'T', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'E', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'D', 'W', 'T', 'T', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'}},
            new List<char[]>
            {new char[] {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'P', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'W', 'T', 'D', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'D', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'W', 'T', 'W', 'D', 'W', 'D', 'W', 'T', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'W'},
             new char[] {'W', 'W', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'W', 'D', 'W', 'T', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'W', 'D', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'W', 'W', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'W'},
             new char[] {'W', 'D', 'D', 'D', 'D', 'D', 'W', 'D', 'W', 'W', 'W', 'W', 'W', 'D', 'D', 'D', 'D', 'D', 'T', 'W'},
             new char[] {'W', 'D', 'W', 'W', 'W', 'D', 'W', 'D', 'W', 'D', 'D', 'D', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
             new char[] {'W', 'D', 'T', 'W', 'D', 'D', 'W', 'D', 'D', 'D', 'W', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'E', 'W'},
             new char[] {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'}}
        };

        List<char[]> board = new List<char[]>{
            new char[] {'W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','P','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','E'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','T','T','T','R','R','R','D','D','W','D','D','D','D','D','R','T','T','T','W'},
            new char[] {'W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W'}
        };

        static void Main()
        {
            new Program().Run();
        }

        // leaving out all the audio for now
        public void Run()
        {
            #region audio
            /*
            sceCtrlSetSamplingCycle(0);
    sceCtrlSetSamplingMode(PSP_CTRL_MODE_ANALOG);
    SceCtrlData ctrlData, old;
	
	int size = 0;
	FILE *fp;
	fp = fopen("pickup.raw", "rb");
	if (fp){
		while(fgetc(fp) != EOF)
			size++;
		// align file size to the closest 64 bytes
		size = PSP_AUDIO_SAMPLE_ALIGN(size);
		
		sound_pickup2 = (uint16_t*)malloc(sizeof(uint16_t) * size);
		if (sound_pickup2){
			memset(sound_pickup2, 0, sizeof(uint16_t) * size);
			
			rewind(fp);
			// read raw data into buffer
			fread(sound_pickup2, sizeof(uint16_t), size, fp);
		}
		
		fclose(fp);
	}
	
	// setup channel 1 to size and MONO because pickup.wav is MONO
    SND_CHANNELS[0] = sceAudioChReserve(PSP_AUDIO_NEXT_CHANNEL, size, PSP_AUDIO_FORMAT_MONO);


    size = 0;
	fp = fopen("wall.raw", "rb");
	if (fp){
		while(fgetc(fp) != EOF)
			size++;
		// align file size to the closest 64 bytes
		size = PSP_AUDIO_SAMPLE_ALIGN(size);
		
		sound_wall = (uint16_t*)malloc(sizeof(uint16_t) * size);
		if (sound_wall){
			memset(sound_wall, 0, sizeof(uint16_t) * size);
			
			rewind(fp);
			// read raw data into buffer
			fread(sound_wall, sizeof(uint16_t), size, fp);
		}
		
		fclose(fp);
	}

    SND_CHANNELS[1] = sceAudioChReserve(PSP_AUDIO_NEXT_CHANNEL, size, PSP_AUDIO_FORMAT_MONO);
    
	

    size = 0;
	fp = fopen("select.raw", "rb");
	if (fp){
		while(fgetc(fp) != EOF)
			size++;
		// align file size to the closest 64 bytes
		size = PSP_AUDIO_SAMPLE_ALIGN(size);
		
		sound_select = (uint16_t*)malloc(sizeof(uint16_t) * size);
		if (sound_select){
			memset(sound_select, 0, sizeof(uint16_t) * size);
			
			rewind(fp);
			// read raw data into buffer
			fread(sound_select, sizeof(uint16_t), size, fp);
		}
		
		fclose(fp);
	}

    SND_CHANNELS[2] = sceAudioChReserve(PSP_AUDIO_NEXT_CHANNEL, size, PSP_AUDIO_FORMAT_MONO);

    size = 0;
	fp = fopen("tada.raw", "rb");
	if (fp){
		while(fgetc(fp) != EOF)
			size++;
		// align file size to the closest 64 bytes
		size = PSP_AUDIO_SAMPLE_ALIGN(size);
		
		sound_tada = (uint16_t*)malloc(sizeof(uint16_t) * size);
		if (sound_tada){
			memset(sound_tada, 0, sizeof(uint16_t) * size);
			
			rewind(fp);
			// read raw data into buffer
			fread(sound_tada, sizeof(uint16_t), size, fp);
		}
		
		fclose(fp);
	}

    SND_CHANNELS[3] = sceAudioChReserve(PSP_AUDIO_NEXT_CHANNEL, size, PSP_AUDIO_FORMAT_MONO);
            
            */
            #endregion

            Psp.Debug.WriteLine("Run");

            BasicGraphics.Init();

            Psp.Debug.WriteLine("Initialised");

            bool isButtonHeld = false;

            while (true)
            {
                Psp.Debug.WriteLine("loop");
                if (onMenu)
                {
                    DrawMenu();
                }
            }
        }

        void DrawMenu()
        {
            Psp.Debug.WriteLine("DrawMenu");

            BasicGraphics.Clear(Color.Black);

            BasicGraphics.DrawImage(0, 0, 480, 272, ImageResources.IMAGE_MENU);

            BasicGraphics.SwapBuffers();

            Display.WaitVblankStart();
        }
    }
}