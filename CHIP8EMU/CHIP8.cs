// Copyright Maurice Montag 2019
// All Rights Reserved
// See LICENSE file for more information

using System;
using System.Timers;

namespace CHIP8EMU
{
    class CHIP8  // this version of the CHIP8 Emulator does not have graphics output or proper key input
        // it was written to gain experience writing a simple emulator
    {
        private readonly byte[] RAM;  // our emulated system RAM
        private const ushort PROGRAM_START = 0x200;  // usual start location of code in program ROM. (512) in decimal
        private const uint CLOCK_SPEED = 2000;  // our emulated clock speed in Hz
        private readonly CPU emuCPU;  // our emulated CHIP-8 CPU
        private readonly Timer cycleTimer;

        public CHIP8()
        {
            RAM = new byte[4096];  // initialize the byte array that holds our emulated memory 
            cycleTimer = new Timer
            {
                AutoReset = true
            };
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

        public void BeginEmulation()
        {
            cycleTimer.Start();
            Console.WriteLine("started timer");
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
