using System;
using System.Collections.Generic;
using System.Text;

namespace LinqToCsv.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class CsvColumnInfoAttribute : Attribute
	{
		public int Index { get; private set; }

		public string Title { get; set; }

		public int Length { get; set; }

		public CsvColumnInfoAttribute(int index)
		{
			this.Index = index;
			this.Title = String.Empty;
			this.Length = -1;
		}
	}
}
