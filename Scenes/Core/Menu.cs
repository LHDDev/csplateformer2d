using Godot;
using System;


namespace Heimgaerd.Core.Menu
{
    public class Menu : Panel
    {
        [Export]
        private NodePath SoundMenuNodePath;
        [Export]
        private NodePath TweenNodePath;

        private Tween TweenNode;
        private SoundMenu SoundMenuNode;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            SoundMenuNode = GetNode<SoundMenu>(SoundMenuNodePath);
            TweenNode = GetNode<Tween>(TweenNodePath);
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (Input.IsActionJustPressed("ui_pause"))
            {
                if (!GetTree().Paused)
                {
                    // Jouer pause_menu_open.mp3
                    SoundMenuNode.FirstElementGrabFocus();
                    TweenNode.InterpolateProperty(this, "rect_global_position:x", RectGlobalPosition.x, 240,1f,Tween.TransitionType.Back, Tween.EaseType.Out);
                    GetTree().Paused = true;
                }
                else
                {
                    // Jouer pause_menu_close.mp3
                    SoundMenuNode.ReleaseMenuElementsFocus();
                    TweenNode.InterpolateProperty(this, "rect_global_position:x", RectGlobalPosition.x, 700, 1f, Tween.TransitionType.Back, Tween.EaseType.In);
                    GetTree().Paused = false;

                }

                TweenNode.Start();
            }
        }
    }
}