using Godot;
using System;
using System.Linq;
using TestCs.StateMachine.States.HookStates;

namespace TestCs.StateMachine.States
{
    class PrepareHook : StateBase
    {

        public override void Do()
        {
            if (!Input.IsActionPressed("g_hook"))
            {
                (actor as ActorPlayer).HookDetectionArea.EnableDetection(false);
                finiteStateMachine.ChangeState<IdleState>();
            }

            if (Input.IsActionJustPressed("g_jump") && (actor as ActorPlayer).HookDetectionArea.accessibleHookSocles.Any())
            {
                // throw Hook to position
                // Wait for hook signal stating it arrived
                // change stat to HookState
                (actor as ActorPlayer).HookDetectionArea.EnableDetection(false);
                finiteStateMachine.ChangeState<HookState>();
            }
        }

        public override bool CanHook()
        {
            return false;
        }

        public override void EnterState()
        {
            // Ralentir temporalité
            if(actor is ActorPlayer)
            {
                (actor as ActorPlayer).HookDetectionArea.EnableDetection(true);
                (actor as ActorPlayer).HookDetectionArea.accessibleHookSocles.Clear();
            }
            else
            {
                GD.PrintErr(Errors.CANT_DETECT_HOOK_IF_NON_PLAYER);
                throw new Exception(Error.CantResolve.ToString());
            }
        }
    }
}