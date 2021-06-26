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

        private int musicAudioBusIndex;
        private int sfxAudioBusIndex;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            musicPlayer = GetNode<AudioStreamPlayer>(musicPlayerNode);
            sfxPlayers = GetNode<Node>(sfxPlayersNode);

            musicAudioBusIndex = AudioServer.GetBusIndex("Music");
            sfxAudioBusIndex = AudioServer.GetBusIndex("SoundEffect");
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

        public void ChangeSFXVolume(float newValue)
        {
            AudioServer.SetBusVolumeDb(sfxAudioBusIndex, GD.Linear2Db(newValue));
            PlaySFX(AudioTypes.SFX_SWORD_SWOOSH_PATH);
        }

        public void ChangeMusicVolume(float newValue)
        {
            AudioServer.SetBusVolumeDb(musicAudioBusIndex, GD.Linear2Db(newValue));
            PlayMusic(AudioTypes.SFX_SWORD_SWOOSH_PATH);
        }
    }
}