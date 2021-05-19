using Godot;
using System;
using static Extension;

public class GUIManager : MarginContainer
{
    private Label currentHpLabel;
    private Label currentSpLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentHpLabel = this.GetNode<Label>("HBoxContainer/VBoxContainer/HBoxContainer/CurrentHPLabel");
        currentSpLabel = this.GetNode<Label>("HBoxContainer/VBoxContainer/HBoxContainer2/CurrentSPLabel");
    }

    public void UpdateHp(float newCurrentHP)
    {
        currentHpLabel.Text = newCurrentHP.ToString();
    }
    public void UpdateSp(float newCurrentSP)
    {
        currentSpLabel.Text = newCurrentSP.ToString();
    }
}
