using System;

namespace ET
{
    public interface IEqualityComparer<T>
    {
        bool Equals(T x, T y);
        int GetHashCode(T obj);
    }

    public struct Address : IEquatable<Address>
    {
        public int Process;
        public int Fiber;

        public bool Equals(Address other)
        {
            return Process == other.Process && Fiber == other.Fiber;
        }

        public override bool Equals(object obj)
        {
            return obj is Address other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Process, Fiber);
        }

        public Address(int process, int fiber)
        {
            Process = process;
            Fiber = fiber;
        }
    }

    public class AddressEqualityComparer : IEqualityComparer<Address>
    {
        public bool Equals(Address x, Address y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Address obj)
        {
            return obj.GetHashCode();
        }
    }

    public struct ActorId : IEquatable<ActorId>
    {
        public Address Address;
        public long InstanceId;

        public bool Equals(ActorId other)
        {
            return Address.Equals(other.Address) && InstanceId == other.InstanceId;
        }

        public override bool Equals(object obj)
        {
            return obj is ActorId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, InstanceId);
        }

        public ActorId(int process, int fiber) : this(new Address(process, fiber), 1)
        {
        }

        public ActorId(int process, int fiber, long instanceId) : this(new Address(process, fiber), instanceId)
        {
        }

        public ActorId(Address address) : this(address, 1)
        {
        }

        public ActorId(Address address, long instanceId)
        {
            Address = address;
            InstanceId = instanceId;
        }
    }

    public class ActorIdEqualityComparer : IEqualityComparer<ActorId>
    {
        public bool Equals(ActorId x, ActorId y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(ActorId obj)
        {
            return obj.GetHashCode();
        }
    }
}
