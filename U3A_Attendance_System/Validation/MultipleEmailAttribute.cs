using System;
using System.ComponentModel.DataAnnotations;

namespace U3A_Attendance_System.Validation
{
	/// <summary>
	/// Supports validating a field containing one or more email 
	/// addresses where each address is separated by a comma
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	internal class MultipleEmailAttribute : RegularExpressionAttribute
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public MultipleEmailAttribute()
			: base(string.Format( @"{0}([,;]\s*{0})*", EmailAttribute.EmailValidationExpression)) 
		{ }

	}
}