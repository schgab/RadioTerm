using RadioTerm.Player;
using RadioTerm.Player.SoundEngine;
using RadioTerm.Rendering;
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
