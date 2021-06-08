using Godot;
using TestCs.Core.Audio;

namespace TestCs.StateMachine.States.AttackStates
{
    class GroundAttack : StateBase
    {
        private int comboCurrentId;

        public override void EnterState()
        {

            actor.CurrentDashComboID = 0;
            if (!(finiteStateMachine.PreviousState is GroundTransition))
            {
                actor.CurrentComboID = 0;
            }
            actor.ActorAnimation.Play(FormatAnimationName("mc-attack"));
            actor.UpdateSP(10);
            actor.AudioController.PlaySFX(AudioTypes.SFX_SWORD_SWOOSH_PATH);
        }
        public override void Do()
        {
            if (!actor.ActorAnimation.IsPlaying())
            {
                actor.CurrentComboID ++;
                finiteStateMachine.ChangeState<GroundTransition>();
            }
        }

        private string FormatAnimationName(string animationName)
        {
            string formatedAnimationName = $"{animationName}-{actor.CurrentComboID}";
            if (actor.ActorSprite.FlipH)
            {
                formatedAnimationName = $"{formatedAnimationName}-right";
            }
            GD.Print($"{formatedAnimationName}");
            return formatedAnimationName;
        }

        public override bool CanHook()
        {
            return false;
        }
    }
}
