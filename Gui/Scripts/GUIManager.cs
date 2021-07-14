using Godot;
using System;
using static Extension;

namespace Heimgaerd.Gui.Scripts
{
    public class GUIManager : MarginContainer
    {
        [Export]
        private NodePath HPBarNodePath;
        [Export]
        private NodePath SPBarNodePath;


        private ProgressBar HPBar;
        private ProgressBar SpBar;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            HPBar = GetNode<ProgressBar>(HPBarNodePath);
            SpBar = GetNode<ProgressBar>(SPBarNodePath);
        }

        public void UpdateHp(float newCurrentHP)
        {
            HPBar.Value = newCurrentHP;
        }
        public void UpdateSp(float newCurrentSP)
        {
            SpBar.Value = newCurrentSP;
        }



    }
}