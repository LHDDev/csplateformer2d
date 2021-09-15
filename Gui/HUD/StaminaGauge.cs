using Godot;
using System;

namespace Heimgaerd.Gui.HUD
{
    public class StaminaGauge : Control
    {
        [Export]
        private NodePath mainGaugePath;
        [Export]
        private NodePath secondaryGaugePath;



        private TextureProgress mainGauge;
        private TextureProgress secondaryGauge;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            mainGauge = GetNode<TextureProgress>(mainGaugePath);
            secondaryGauge = GetNode<TextureProgress>(secondaryGaugePath);
        }

        public void UpdateStamina(float newValue)
        {
            if(mainGauge.MaxValue < newValue && ! secondaryGauge.Visible)
            {
                secondaryGauge.Visible = true;
            }
            if (secondaryGauge.Visible)
            {
                float secondaryValue = newValue - (float)mainGauge.MaxValue;
                if(secondaryGauge.Value < secondaryValue)
                {
                    newValue = (float)secondaryGauge.Value - secondaryValue;
                    secondaryGauge.Value = 0;
                    secondaryGauge.Visible = false;
                }
                else
                {
                    secondaryGauge.Value = secondaryValue;
                }
            }
            if(mainGauge.MaxValue > newValue)
            {
                if (mainGauge.Value < newValue)
                {
                    mainGauge.Value = 0;
                }
                else
                {
                    mainGauge.Value = newValue;
                }
            }
        }
    }
}