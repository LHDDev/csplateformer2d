using Godot;
using TestCs.Core.Audio;

public class SoundMenu : HBoxContainer
{
    [Export]
    private NodePath musicVolumeSliderPath;
    [Export]
    private NodePath sfxVolumeSliderPath;

    private Slider musicVolumeSlider;
    private Slider sfxVolumeSlider;
    private AudioController audioController;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetTree().Paused = true;
        musicVolumeSlider = GetNode<Slider>(musicVolumeSliderPath);
        sfxVolumeSlider = GetNode<Slider>(sfxVolumeSliderPath);

        audioController = GetNode<AudioController>("/root/AudioController");

        musicVolumeSlider.Connect("value_changed", audioController, nameof(AudioController.ChangeMusicVolume));
        sfxVolumeSlider.Connect("value_changed", audioController, nameof(AudioController.ChangeSFXVolume));
    }
}
