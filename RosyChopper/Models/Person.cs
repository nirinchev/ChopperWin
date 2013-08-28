using System;

namespace RosyChopper.Models
{
	public class Person
	{
		private string name;

		public Person(string name)
		{
			this.Name = name;
		}

		public string Name
		{
			get
			{
				return this.name;
			}

			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("Name can not be null or whitespace.");
				}

				this.name = value;
			}
		}

		public override bool Equals(object obj)
		{
			var person = obj as Person;
			if (person != null)
			{
				return this.Name == person.Name;
			}

			return false;
		}

		public override string ToString()
		{
			return this.Name;
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}
	}
}