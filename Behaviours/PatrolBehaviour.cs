using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heimgaerd.Behaviours
{
    class PatrolBehaviour : BehaviourBase
    {
        [Export]
        private NodePath waitingTimerPath;

        private Array<Vector2> nextTargets;
        private Array<Vector2> previousTargets;

        private Vector2 currentTarget;
        private Timer waitingTimer;
        public override void Do()
        {
            // LookTo currentTarget
            if(actor.GlobalPosition.x >= currentTarget.x - 10 && actor.GlobalPosition.x <= currentTarget.x + 10)
            {
                actor.UpdateFacingDirection(0);
                previousTargets.Insert(0,currentTarget);


                if (nextTargets.Count == 0)
                {
                    foreach (Vector2 t in previousTargets)
                    {
                        nextTargets.Add(t);
                    }

                    previousTargets.Clear();
                }

                currentTarget = nextTargets[0];
                nextTargets.RemoveAt(0);
                waitingTimer.Start();
            }
        }

        public override void EnterBehaviour()
        {
            previousTargets = new Array<Vector2>();
            nextTargets = new Array<Vector2>();

            foreach(Vector2 t in actor.PatrolPoints)
            {
                nextTargets.Add(t);
            }
            //nextTargets = actor.PatrolPoints;

            currentTarget = nextTargets[0];
            nextTargets.RemoveAt(0);
            UpdateFacingTarget();

        }

        public override void _Ready()
        {
            base._Ready();
            waitingTimer = GetNode<Timer>(waitingTimerPath);
            waitingTimer.Connect("timeout", this, nameof(UpdateFacingTarget));
        }

        public void UpdateFacingTarget()
        {
            actor.UpdateFacingDirection(Math.Sign(currentTarget.x - actor.GlobalPosition.x));
        }
    }
}
