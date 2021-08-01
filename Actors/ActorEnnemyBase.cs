using Godot;
using Godot.Collections;
using Heimgaerd.Behaviours;
using Heimgaerd.StateMachine;
using Heimgaerd.StateMachine.States.NPCStates;
using System;
using System.Collections.Generic;
using System.Linq;

public class ActorEnnemyBase : ActorBase
{
    [Export]
    public Array<Vector2> PatrolPoints { get; private set; }
    
    [Export]
    private NodePath behaviourMachinePath;
    [Export]
    private NodePath fieldOfViewPath;

    private BehaviourMachine behaviourMachine;
    public Area2D fieldOfView { get; private set; }

    public ActorPlayer currentTarget;
    protected override void Init()
    {
        base.Init();
        finiteStateMachine = this.FindChildrenOfType<StateMachineBase>().FirstOrDefault();
        if (finiteStateMachine != default)
        {
            finiteStateMachine.ChangeState<NpcIdleState>();
        }
        else
        {
            GD.Print("ERREUR LORS DE LA RECUPERATION DE LA STATE MACHINE");
        }

        fieldOfView = GetNode<Area2D>(fieldOfViewPath);
        fieldOfView.Connect("body_entered", this, nameof(BeginChase));
        fieldOfView.Connect("body_exited", this, nameof(EndChase));

        behaviourMachine = GetNode<BehaviourMachine>(behaviourMachinePath);
        behaviourMachine.ChangeBehaviour<PatrolBehaviour>();

    }
    public override void UpdateHP(int amount)
    {
        CurrentHealt -= amount;
    }

    public override void UpdateSP(int amount)
    {
        CurrentHealt -= amount;
    }

    public void UpdateFacingDirection(int newFacingDirection)
    {
        FacingDirection = newFacingDirection;
        if (FacingDirection >= 1) fieldOfView.Scale = Vector2.One;
        else if (FacingDirection <= -1) fieldOfView.Scale = Vector2.NegOne;
    }

    public void BeginChase(Node body)
    {
        if (body is ActorPlayer player)
        {
            currentTarget = player;
            behaviourMachine.ChangeBehaviour<ChaseBehaviour>();
        }
    }
    public void EndChase(Node body)
    {
        if (body is ActorPlayer player)
        {
            currentTarget = null;
            behaviourMachine.ChangeBehaviour<PatrolBehaviour>();
        }
    }
}
