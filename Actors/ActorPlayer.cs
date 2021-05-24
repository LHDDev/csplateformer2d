using Godot;
using System;
using TestCs.StateMachine.States;

public class ActorPlayer : ActorBase
{

    [Signal]
    delegate void UpdatedPlayerHp(float newCurrentHP);
    [Signal]
    delegate void UpdatedPlayerSp(float newCurrentSP);


    // Called when the node enters the scene tree for the first time.
    public async override void _Ready()
    {
        base.Init();
        FacingDirection = 0;

        await ToSignal(GetParent<GameManager>(), "ready");
        EmitSignal(nameof(UpdatedPlayerHp), CurrentHealt * 100 / maxHealth);
        EmitSignal(nameof(UpdatedPlayerSp), CurrentStamina * 100 / maxStamina);
    }

    public override void _Process(float delta)
    {
        if(!(finiteStateMachine.PreviousState is WallState) && finiteStateMachine.CurrentState.CanUpdateDirection())
            UpdateFacingDirection(0);
    }

    private void UpdateFacingDirection(int newFacingDirection)
    {
        float actionStrength = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        FacingDirection = newFacingDirection == 0 ? Math.Sign(actionStrength):newFacingDirection;
        if(FacingDirection < 0)
        {
            ActorSprite.FlipH = true;
        }
        else if(FacingDirection > 0)
        {
            ActorSprite.FlipH = false;
        }

    }

    public void ReverseDirection()
    {
        UpdateFacingDirection(-FacingDirection);
    }

    public override void UpdateHP(int amount)
    {
        base.UpdateHP(amount);
        EmitSignal(nameof(UpdatedPlayerHp), CurrentHealt*100/maxHealth);
    }

    public override void UpdateSP(int amount)
    {
        base.UpdateSP(amount);
        EmitSignal(nameof(UpdatedPlayerSp), CurrentStamina*100/maxStamina);
    }
}
