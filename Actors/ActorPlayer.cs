using Godot;
using Heimgaerd.StateMachine;
using Heimgaerd.StateMachine.States;
using System;
using System.Linq;

public class ActorPlayer : ActorBase
{

    [Signal]
    delegate void UpdatedPlayerHp(float newCurrentHP);
    [Signal]
    delegate void UpdatedPlayerSp(float newCurrentSP);

    [Export]
    private NodePath HookDetectionAreaPath;
    public HookableDetection HookDetectionArea { get; private set; }
    
    public PlayerRespawner Respawner { get; private set; }
    [Export]
    public bool CanRespanw = false;
    protected override void Init()
    {
        base.Init();
        finiteStateMachine = this.FindChildrenOfType<StateMachineBase>().FirstOrDefault();
        if (finiteStateMachine != default)
        {
            finiteStateMachine.ChangeState<IdleState>();
        }
        else
        {
            GD.Print("ERREUR LORS DE LA RECUPERATION DE LA STATE MACHINE");
        }
    }
    // Called when the node enters the scene tree for the first time.
    public async override void _Ready()
    {
        Init();
        FacingDirection = 0;

        HookDetectionArea = this.FindChildrenOfType<HookableDetection>().ElementAt(0);

        await ToSignal(GetParent<GameManager>(), "ready");
        //EmitSignal(nameof(UpdatedPlayerHp), maxHealth);
        EmitSignal(nameof(UpdatedPlayerSp), maxStamina);


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
        EmitSignal(nameof(UpdatedPlayerHp), amount);
        if(CurrentHealt <=0)
        {
            finiteStateMachine.ChangeState<DeathState>();
        }
    }

    public override void UpdateSP(int amount)
    {
        base.UpdateSP(amount);
        EmitSignal(nameof(UpdatedPlayerSp), CurrentStamina);
    }

    public void SetRespawner(PlayerRespawner newRespawner)
    {
        Respawner = newRespawner;
    }

    public override void Death()
    {
        this.Position = Respawner.GlobalPosition;
        Respawn();
    }

    private void Respawn()
    {
        CurrentHealt = maxHealth;
        CurrentStamina = maxStamina;

        EmitSignal(nameof(UpdatedPlayerHp), CurrentHealt * 100 / maxHealth);
        EmitSignal(nameof(UpdatedPlayerSp), maxStamina);

        finiteStateMachine.ChangeState<IdleState>();
        ActorAnimation.Play("mc-RESET");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.IsPressed())
            { 
                if(eventKey.Scancode == (int)KeyList.Tab)
                {
                    finiteStateMachine.ChangeState<DeathState>();
                }
                if(eventKey.Scancode == (int)KeyList.Key1)
                {
                    UpdateHP(-1);
                }
            }
        }
    }

}
