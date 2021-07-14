using Godot;
using Heimgaerd.StateMachine.States;
using System.Linq;

namespace Heimgaerd.StateMachine
{
    public class StateMachineBase : Node
    {
        public StateBase CurrentState { get; private set; }
        public StateBase PreviousState { get; private set; }
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            CurrentState.Do();
            if (CurrentState.CanHook() && Input.IsActionJustPressed("g_hook"))
            {
                ChangeState<PrepareHook>();
            }
        }

        public void ChangeState<T>() where T : StateBase
        {
            PreviousState = CurrentState ?? this.FindChildrenOfType<IdleState>().FirstOrDefault();

            CurrentState = this.FindChildrenOfType<T>().First();
            CurrentState.EnterState();
            GD.Print($"previous state = {PreviousState.Name}");
            //GD.Print($"current state = {CurrentState.Name}");
        }

        public void ChangeToPreviousState()
        {
            if (CurrentState is JumpState)
            {
                ChangeState<FallState>();
            }
            else
            {
                var changeStateMethod = typeof(StateMachineBase).GetMethod(nameof(ChangeState));
                var methodRef = changeStateMethod.MakeGenericMethod(PreviousState.GetType());
                methodRef.Invoke(this, null);
            }
        }
    }
}