using System;
using System.IO;

namespace CHIP8EMU
{
    class GlobalMain  // split this class up when needed
    {
        private const string ROM_PATH = @"C:\Users\night\Downloads\DivisionTest.ch8";  // path to rom image to load
        static void Main(string[] args)
        {
            CHIP8 chip8 = new CHIP8();  // temp for now
            byte[] romToLoad = File.ReadAllBytes(ROM_PATH);
            chip8.LoadProgram(romToLoad);
            chip8.BeginEmulation();
            Console.WriteLine("press enter to exit at any time");
            Console.ReadLine();  // stop when user presses a key
        }
    }
}
