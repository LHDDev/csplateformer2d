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

    private BehaviourMachine behaviourMachine;
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

    public void updateFacingDirection(int newFacingDirection)
    {
        FacingDirection = newFacingDirection;
    }
}
