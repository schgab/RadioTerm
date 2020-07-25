using NAudio.Wave;
using RadioTerm.Player;
using RadioTerm.Player.SoundEngine;
using RadioTerm.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace RadioTerm
{
    class Program
    {
        static void Main(string[] args)
        {
            new RadioPlayer(new DisplayEngine(), new NAudioSoundEngine()).Run();
        }

    }
}
