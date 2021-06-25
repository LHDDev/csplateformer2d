using Godot;
namespace TestCs.Core.Audio
{

    public class AudioController : Node
    {
        [Export]
        private NodePath musicPlayerNode;
        [Export]
        private NodePath sfxPlayersNode;
        private AudioStreamPlayer musicPlayer;
        private Node sfxPlayers;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            musicPlayer = GetNode<AudioStreamPlayer>(musicPlayerNode);
            sfxPlayers = GetNode<Node>(sfxPlayersNode);
        }

        public void PlayMusic(AudioStream musicStream)
        {
            musicPlayer.Stream = musicStream;
            musicPlayer.Play();
        }

        public void PlaySFX(AudioStream sfxStream)
        {
            foreach ( AudioStreamPlayer sfxPlayer in sfxPlayers.FindChildrenOfType<AudioStreamPlayer>())
            {
                if (!sfxPlayer.Playing)
                {
                    sfxPlayer.Stream = sfxStream;
                    sfxPlayer.Play();
                }
            }
        }
    }
}