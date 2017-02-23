﻿using System;

namespace WizardLib.Core {
	/// <summary>
	/// The delegate for the method that returns ToString() for the wrapped item.
	/// </summary>
	public delegate Object ToStringDelegate<T>(T t);

	/// <summary>
	/// A utility class for use with ListBox and other controls that take items of type Object and display
	/// them using their ToString() methods. By wrapping it in an instance of this class, a custom
	/// ToString() representation can be provided while allowing easy access to the original object.
	/// </summary>
	/// <typeparam name="T">The type of the wrapped object.</typeparam>
	public class ToStringWrapper<T> : IComparable<ToStringWrapper<T>>, IEquatable<ToStringWrapper<T>> where T : IEquatable<T> {
		#region For use with controls like DataGridViewComboBoxCell

		public const string ValueMember = "Self";

		public const string DisplayMember = "ToStringProperty";

		/// <summary>
		/// Gets the string returned by ToString()
		/// (Property wrapper for ToString(); this property's name can be used to set
		/// the DisplayMember property of controls like DataGridViewComboBoxCell)
		/// </summary>
		public string ToStringProperty { get { return ToString(); } }

		/// <summary>
		/// Returns this ToStringWrapper instance (can be used to set
		/// the ValueMember property of controls like DataGridViewComboBoxCell)
		/// </summary>
		public ToStringWrapper<T> Self { get { return this; } }

		#endregion

		/// <summary>
		/// The wrapped object.
		/// </summary>
		public readonly T item;

		/// <summary>
		/// The string returned by ToString() on this ToStringWrapper instance.
		/// </summary>
		private readonly string toString;
		/// <summary>
		/// The delegate executed to provide ToString() on this ToStringWrapper instance.
		/// </summary>
		private readonly ToStringDelegate<T> toStringDelegate;

		/// <summary>
		/// Takes and subsequently returns a fixed ToString() value.
		/// </summary>
		/// <param name="item">The item to wrap.</param>
		/// <param name="toString">The ToString() string that is returned by the new wrapper.</param>
		public ToStringWrapper(T item, string toString) {
			this.item = item;
			this.toString = toString;
		}

		/// <summary>
		/// Takes a ToStringDelegate, executed each time ToString() is called.
		/// </summary>
		/// <param name="item">The item to wrap.</param>
		/// <param name="toStringDelegate">A method that should return the desired ToString()
		/// value for the wrapped object. Calls to this ToStringWrapper's ToString() method
		/// evaluate the supplied delegate method and return its value.</param>
		public ToStringWrapper(T item, ToStringDelegate<T> toStringDelegate) {
			this.item = item;
			this.toStringDelegate = toStringDelegate;
		}

		public override string ToString() {
			System.Diagnostics.Trace.Assert(toString == null ^ toStringDelegate == null);
			if (toStringDelegate != null) {
				return toStringDelegate(item).ToString();
			}
			else {
				return toString;
			}
		}

		public bool Equals(ToStringWrapper<T> other) {
			return other != null && item.Equals(other.item);
		}

		public int CompareTo(ToStringWrapper<T> other) {
			if (item is IComparable<T>) {
				return (item as IComparable<T>).CompareTo(other.item);
			}
			else {
				return StringUtility.NaturalCompare(ToString(), other.ToString());
			}
		}
	}
}
