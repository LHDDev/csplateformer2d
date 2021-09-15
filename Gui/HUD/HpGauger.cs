using Godot;
using static Extension;

namespace Heimgaerd.Gui.HUD
{
    public class HpGauger : Control
    {
        [Export]
        private int maxHealthToDisplay;

        private int currentHealthToDisplay;
        private int lastHeartWithPVIdx;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            for (int i = 0; i < maxHealthToDisplay; i = i + 4)
            {
                AddNewheart(4);
            }
            lastHeartWithPVIdx = maxHealthToDisplay / 4;
            if (maxHealthToDisplay % 4 != 0)
            {
                AddNewheart(maxHealthToDisplay % 4);
                lastHeartWithPVIdx++;
            }
            currentHealthToDisplay = maxHealthToDisplay;

        }

        private void AddNewheart(int pvHeart)
        {
            Heart newHeart = SmartLoader<Heart>("res://Scenes/Core/GUI/HUD/Scene_Heart.tscn");
            AddChild(newHeart);
            newHeart.SetPv(pvHeart);
        }

        public void DisplayPV(int healthModifier)
        {
            Heart currentHeart = GetChild<Heart>(lastHeartWithPVIdx);

            currentHeart.SetPv(currentHeart.pvHeart + healthModifier);

            currentHealthToDisplay += healthModifier;

            if(currentHeart.pvHeart == 4 && currentHealthToDisplay < maxHealthToDisplay)
            {
                lastHeartWithPVIdx++;
            }
            GD.Print($"test 1 = {currentHeart.pvHeart == 0}");
            GD.Print($"test 2 = {currentHealthToDisplay > 0}");
            if (currentHeart.pvHeart == 0 && currentHealthToDisplay > 0)
            {
                GD.Print("passer ICI !");
                lastHeartWithPVIdx--;
            }
            GD.Print($"idx = {lastHeartWithPVIdx}");
        }
    }
}