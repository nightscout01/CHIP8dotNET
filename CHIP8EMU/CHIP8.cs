using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Input;

namespace CHIP8EMU
{
    class CHIP8
    {
        //private readonly Dictionary<byte,Key>()
        private readonly byte[] RAM;  // our emulated system RAM
        private byte[,] GraphicsArray;  // our 2D graphics array
        private readonly byte[] key;  // the weird hex keyboard this thing uses
        private const ushort PROGRAM_START = 0x200;  // usual start location of code in program ROM. (512) in decimal
        private const uint CLOCK_SPEED = 2000;  // our emulated clock speed in Hz
        private readonly CPU emuCPU;  // our emulated CHIP-8 CPU
        private readonly System.Timers.Timer cycleTimer;


        // PUT SOME GRAPHICS STUFF HERE (maybe)?

        public CHIP8()
        {
            //System.Windows.Input.KeyStates;
            RAM = new byte[4096];  // initialize the byte array that holds our emulated memory 
            GraphicsArray = new byte[64, 32];  // CHIP-8 graphics are a black and white 64x32 grid
            key = new byte[16];  // CHIP-8 has a strange hex based keyboard, this is where we store the keypresses.
            cycleTimer = new System.Timers.Timer();
            cycleTimer.AutoReset = true;
            double intervalTime = 1.0 / CLOCK_SPEED * 1000.0;  // translate from frequency to period and multiply by 1000 to get the interval time in ms
            cycleTimer.Interval = intervalTime;
            cycleTimer.Elapsed += CycleEvent;  // add our event
            cycleTimer.Enabled = true;  // enable the timer
            emuCPU = new CPU(RAM);
        }

        public void CycleEvent(object source, ElapsedEventArgs e)
        {
            emuCPU.EmulateCycle();
        }

        public void TimedEmulation()
        {
            //loop at 100HZ WHILE()
            // 500 Hz means 500 ops/second. 


            // ENDWHILE
        }

        public void BeginEmulation()
        {
            cycleTimer.Start();
            Console.WriteLine("started timer");
            //while (true)
            //{
            //    emuCPU.EmulateCycle(); // :OOOO
            //    Thread.Sleep(100);
            //}
        }

        /// <summary>
        /// Loads the given CHIP8 program ROM, as a byte array, into our emulated memory (likely at PROGRAM_START)
        /// </summary>
        /// <param name="progROM">The CHIP8 program ROM, as a byte[]</param>
        /// <returns>true if the program was successfully loaded, false if there was an error</returns>
        public bool LoadProgram(byte[] progROM)
        {
            Array.Copy(progROM, 0, RAM, PROGRAM_START,progROM.Length);  // copy the contents of our program ROM into our emulated RAM at the "memory address" PROGRAM_START
            emuCPU.InitializeCPU(PROGRAM_START);  // initialize the CPU, passing in the start address of our program ROM
            return true;  // TODO, implement error checking?
        }
    }
}
