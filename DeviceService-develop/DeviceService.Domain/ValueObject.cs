using System;
using System.Collections.Generic;

namespace DeviceService.Domain
{
	/// <summary>
	/// Base class for all Value Objects
	/// </summary>
	public abstract class ValueObject
	{
		#region operators

		/// <summary>
		/// Returns a value indicating whether two <see cref="ValueObject"/>s are equal.
		/// </summary>
		/// <param name="x">The first <see cref="ValueObject"/> to compare.</param>
		/// <param name="y">The second <see cref="ValueObject"/> to compare.</param>
		/// <returns>True if <paramref name="x"/> and <paramref name="y"/> are equal; otherwise, false.</returns>
		public static bool operator ==(ValueObject x, ValueObject y)
		{
			return object.Equals(x, y);
		}

		/// <summary>
		/// Returns a value indicating whether two <see cref="ValueObject"/>s are not equal.
		/// </summary>
		/// <param name="x">The first <see cref="ValueObject"/> to compare.</param>
		/// <param name="y">The second <see cref="ValueObject"/> to compare.</param>
		/// <returns>True if <paramref name="x"/> and <paramref name="y"/> are not equal; otherwise, false.</returns>
		public static bool operator !=(ValueObject x, ValueObject y)
		{
			return !object.Equals(x, y);
		}

		#endregion

		#region methods

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		/// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (this.GetType() != obj.GetType())
			{
				return false;
			}

			return PublicInstancePropertiesEqual(this, obj);
		}
		
		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return GetHashCodeByPublicInstanceProperties(this);
		}

		#endregion

		#region helpers

		private int GetHashCodeByPublicInstanceProperties<T>(T self, params string[] ignore) where T : class
		{
			if (self != null)
			{
				var resultHashCode = self.GetType().GetHashCode();
				Type type = self.GetType();
				var ignoreList = new List<string>(ignore);

				foreach (System.Reflection.PropertyInfo pi in type.GetProperties(
					System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
				{
					if (!ignoreList.Contains(pi.Name))
					{
						object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
						if (selfValue != null)
						{
							resultHashCode = resultHashCode ^ selfValue.GetHashCode();
						}
					}
				}
				
				return resultHashCode;
			}

			return base.GetHashCode();
		}

		private bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
		{
			if (self != null && to != null)
			{
				Type type = self.GetType();
				var ignoreList = new List<string>(ignore);

				foreach (System.Reflection.PropertyInfo pi in type.GetProperties(
					System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
				{
					if (!ignoreList.Contains(pi.Name))
					{
						object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
						object toValue = type.GetProperty(pi.Name).GetValue(to, null);

						if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
						{
							return false;
						}
					}
				}

				return true;
			}

			return self == to;
		}

		#endregion
	}
}