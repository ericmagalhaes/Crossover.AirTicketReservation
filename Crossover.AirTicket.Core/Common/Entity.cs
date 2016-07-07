using System;
using MongoDB.Bson.Serialization.Attributes;


namespace Crossover.AirTicket.Core.Common
{
    public abstract class Entity
    {
        /***
        Property should have a protected set.
        I have to remove becouse of IEntity, MongoRepository
        instead I write a private field to allow only one change,
        this will maintain the immutable concept **/
        private string _id = string.Empty;
        [BsonId]
        public string Id {
            get { return _id;}
            set
            {
                if (string.Empty == _id)
                    _id = value;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetType() != other.GetType())
                return false;
            if (Id == string.Empty || other.Id == string.Empty)
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
            return (GetType() + Id).GetHashCode();
        }
    }
}