using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCs.Core.Audio;
using TestCs.StateMachine;
using TestCs.StateMachine.States;

public abstract class ActorBase : KinematicBody2D
{
    [Export]
    private int gravityForce;
    [Export]
    private Vector2 recoilForce;
    [Export]
    protected int maxHealth = -1; // Neg health on start -> infinite health
    [Export]
    protected int maxStamina = -1; // Neg stamina on start -> infinite stamina
    [Export(PropertyHint.Range, "0.0,1.0")]
    private float flashModifier;
    [Export]
    private float stunTimer = 0.5f;
    [Export]
    public int movementSpeed { get; private set; }
    [Export]
    public int JumpForce { get; private set; }
    [Export]
    public int BaseDamage{ get; private set; }


    public Vector2 Velocity;
    public int CurrentHealt { get; protected set; }
    public int CurrentStamina { get; protected set; }
    public AnimatedSprite ActorSprite { get; private set; }
    public AnimationPlayer ActorAnimation { get; private set; }

    public AudioController AudioController { get; private set; }

    protected StateMachineBase finiteStateMachine;
    public int FacingDirection { get; protected set; }

    protected Vector2 snappedVector;
    public bool isSnapped { get; set; }

    [Export]
    public int MaxGroundCombo { get; private set; }
    [Export]
    public int MaxDashCombo { get; private set; }

    public int CurrentComboID { get; set; }
    public int CurrentDashComboID { get; set; }
    protected void Init()
    {

        List<string> errorList = VerifyActor();
        if (errorList.Any())
        {
            errorList.ForEach(e => GD.PrintErr($"Node {this.Name} - {e}"));
            throw new Exception(Error.CantCreate.ToString());
        }

        CurrentHealt = maxHealth;
        CurrentStamina = maxStamina;

        isSnapped = true;
        snappedVector = new Vector2(0.0f, 20.0f);

        finiteStateMachine = this.FindChildrenOfType<StateMachineBase>().FirstOrDefault();
        if( finiteStateMachine != default)
        {
            finiteStateMachine.ChangeState<IdleState>();
        }

        AudioController = GetNode<AudioController>("/root/AudioController");

    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Init();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public override void _PhysicsProcess(float delta)
    {
        ApplyMovement(delta);
    }

    private void ApplyMovement(float delta)
    {
        Velocity.x *= delta;
        Velocity.y += finiteStateMachine.CurrentState is WallState? gravityForce/10: gravityForce;

        MoveAndSlideWithSnap(Velocity,isSnapped?snappedVector:Vector2.Zero, Vector2.Up);

    }


    public void ApplyRecoil()
    {
        Velocity = recoilForce;
    }

    /// <summary>
    /// Verify if ActorNode has all requirements
    /// </summary>
    /// <returns>List of errors</returns>
    private List<string> VerifyActor()
    {
        List<String> errorList = new List<string>();

        Array<AnimatedSprite> animatedSpriteChildrenList = this.FindChildrenOfType<AnimatedSprite>();
        Array<AnimationPlayer> animationPlayerChildrenList = this.FindChildrenOfType<AnimationPlayer>();
        Array<SoundManager> audioStreamChildrenList = this.FindChildrenOfType<SoundManager>();

        if (!animatedSpriteChildrenList.Any())
        {
            errorList.Add(Errors.NO_ANIMATEDSPRITE_ERROR);
        }
        else if (animatedSpriteChildrenList.Count > 1)
        {
            errorList.Add(Errors.TOO_MUCH_SPRITES_ERROR);
        }
        else
        {
            ActorSprite = animatedSpriteChildrenList.First();
        }

        if (!animationPlayerChildrenList.Any())
        {
            errorList.Add(Errors.NO_ANIMATEDSPRITE_ERROR);
        }
        else if (animationPlayerChildrenList.Count > 1)
        {
            errorList.Add(Errors.TOO_MUCH_SPRITES_ERROR);
        }
        else
        {
            ActorAnimation = animationPlayerChildrenList.First();
        }

        return errorList;
    }

    public virtual void UpdateHP(int amount)
    {
        CurrentStamina = Math.Max(0, CurrentStamina - amount);
    }
    public virtual void UpdateSP(int amount)
    {
        CurrentStamina = Math.Max(0, CurrentStamina - amount);
    }

}
