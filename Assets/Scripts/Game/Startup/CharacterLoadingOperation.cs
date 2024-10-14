using System;
using System.Threading.Tasks;

namespace Game.Startup
{
    public class CharacterLoadingOperation : ILoadingOperation
    {
        private Action<float> onProgressUpdate;
        public CharacterContainer Character { get; private set; }
        public string Description => "Loading character";

        public async Task Load(Action<float> onUpdate)
        {
            onProgressUpdate = onUpdate;
            onProgressUpdate.Invoke(0.3f);

            var loader = new LocalContentLoader();
            Character = await loader.LoadAndInstantiate<CharacterContainer>("Character");

            onProgressUpdate.Invoke(1f);
        }
    }
}