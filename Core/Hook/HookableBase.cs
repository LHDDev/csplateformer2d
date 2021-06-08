using Godot;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCs.Core.Hook
{
    public abstract class HookableBase : Position2D, IComparable<HookableBase>
    {
        public float DistFromPlayer { get; protected set; }
        public int CompareTo(HookableBase other)
        {
            int indexOrder = DistFromPlayer.CompareTo(other.DistFromPlayer);

            GD.Print($"{this} dist from player = {DistFromPlayer}");
            if (indexOrder != 0)
            {
                return indexOrder;
            }

            return 0;
        }
    }
}
