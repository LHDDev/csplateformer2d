﻿using Godot;
using System;
using System.Linq;
using TestCs.StateMachine.States.HookStates;

using static Extension;

namespace TestCs.StateMachine.States
{
    class PrepareHook : StateBase
    {
        [Export]
        private String hookScenePath;
        [Export]
        private NodePath spawnHookPositionPath;

        private Position2D spawnHookPosition;

        private HookableDetection hookableDetection;

        private bool hookThrowed;
        public override async void Do()
        {
            if(!hookThrowed)
            {
                if (!Input.IsActionPressed("g_hook"))
                {
                    hookableDetection.DisableDetection();
                    finiteStateMachine.ChangeState<IdleState>();
                }

                if (Input.IsActionJustPressed("g_jump") && hookableDetection.accessibleHookSocles.Any())
                {
                    // throw Hook to position
                    // Wait for hook signal stating it arrived
                    // change stat to HookState
                    hookThrowed = true;

                    Hook hook = SmartLoader<Hook>(hookScenePath);
                    actor.GetParent().AddChild(hook);
                    hook.Position = actor.GlobalPosition;
                    hook.DirectionTo = hookableDetection.GetSelectedHookableGlobalPosition();
                    hook.LookAt(hook.DirectionTo);
                    hookableDetection.DisableDetection();

                    await ToSignal(hook, nameof(Hook.HookableCollided));

                    if((actor.GlobalPosition.x > hook.DirectionTo.x && !actor.ActorSprite.FlipH) || (actor.GlobalPosition.x < hook.DirectionTo.x && actor.ActorSprite.FlipH))
                    {
                        actor.ActorSprite.FlipH = !actor.ActorSprite.FlipH;
                    }

                    finiteStateMachine.ChangeState<HookState>();
                
                }
                if (Input.IsActionJustPressed("ui_left"))
                {
                    hookableDetection.SelectPreviousHookable();
                }

                if (Input.IsActionJustPressed("ui_right"))
                {
                    hookableDetection.SelectNextHookable();
                }
            }

            //spawnHookPosition.GetParent<Position2D>().LookAt(hookableDetection.getSelectedHookableGlobalPosition());
        }

        public override bool CanHook()
        {
            return false;
        }

        public override bool CanUpdateDirection()
        {
            return false;
        }

        public override bool CanMove()
        {
            return false;
        }

        public override void EnterState()
        {
            // Ralentir temporalité

            if(actor is ActorPlayer player)
            {
                hookableDetection = player.HookDetectionArea;
                hookableDetection.EnableDetection();
                hookableDetection.accessibleHookSocles.Clear();
                hookThrowed = false;

            }
            else
            {
                GD.PrintErr(Errors.CANT_DETECT_HOOK_IF_NON_PLAYER);
                throw new Exception(Error.CantResolve.ToString());
            }
        }
    }
}