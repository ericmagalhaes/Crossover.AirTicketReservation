﻿using System;

namespace Crossover.AirTicket.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetType() != other.GetType())
                return false;
            if (Id == Guid.Empty || other.Id == Guid.Empty)
                return false;
            return Id == other.Id;

        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}