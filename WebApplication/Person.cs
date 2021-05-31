using System;

namespace WebApplication
{
    public sealed class Person : IEquatable<Person>
    {
        public string Name { get; set; }
        public Person(string name) => Name = name;

        public bool Equals(Person other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name;
        }
        public override bool Equals(object obj) => Equals(obj as Person);
        public override int GetHashCode() => (Name).GetHashCode();
    }
}