using Godot;
using System;

namespace Heimgaerd.Gui.HUD
{
    public class Heart : Control
    {
        [Export]
        private NodePath animatedSpritePath;

        private AnimatedSprite animatedSprite;
        public int pvHeart { get; private set; }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            animatedSprite = GetNode<AnimatedSprite>(animatedSpritePath);
        }

        public void SetPv(int currentPvHeart)
        {
            pvHeart = currentPvHeart;
            animatedSprite.Frame = animatedSprite.Frames.GetFrameCount("default")-1 - pvHeart;
        }
    }
}