using Godot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heimgaerd.Core.Hook
{
    public abstract class HookableBase : Position2D, IComparable<HookableBase>
    {
        public float DistFromPlayer { get; protected set; }
        public bool IsSelected { get; protected set; }
        public int CompareTo(HookableBase other)
        {
            int indexOrder = DistFromPlayer.CompareTo(other.DistFromPlayer);

            if (indexOrder != 0)
            {
                return indexOrder;
            }

            return 0;
        }

        public virtual void SetSelected(bool newSelectedValue)
        {
            IsSelected = newSelectedValue;
        }
    }
}
