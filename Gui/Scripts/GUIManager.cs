using Godot;
using Heimgaerd.Gui.HUD;
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


        private HpGauger HPBar;
        private StaminaGauge SpBar;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            HPBar = GetNode<HpGauger>(HPBarNodePath);
            SpBar = GetNode<StaminaGauge>(SPBarNodePath);
        }

        public void UpdateHp(int newCurrentHP)
        {
            HPBar.DisplayPV(newCurrentHP);
        }
        public void UpdateSp(float newCurrentSP)
        {
            SpBar.UpdateStamina(newCurrentSP);
        }



    }
}