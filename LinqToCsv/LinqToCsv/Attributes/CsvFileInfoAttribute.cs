using LinqToCsv.Csv;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinqToCsv.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class CsvFileInfoAttribute: Attribute
	{
		private CsvSettings _csvSettings;

		public bool HasSeparator
		{
			get
			{
				return this._csvSettings.HasSeparator;
			}
			set
			{
				this.HasSeparator = value;
			}
		}

		public char Separator
		{
			get
			{
				return this._csvSettings.Separator;
			}
			set
			{
				this._csvSettings.Separator = value;
			}
		}

		public char Delimiter
		{
			get
			{
				return this._csvSettings.Delimiter;
			}
			set
			{
				this._csvSettings.Delimiter = value;
			}
		}

		public bool HasColumnHeaders
		{
			get
			{
				return this._csvSettings.HasColumnHeaders;
			}
			set
			{
				this._csvSettings.HasColumnHeaders = value;
			}
		}

		public CsvFileInfoAttribute()
		{
			this._csvSettings = CsvSettings.Default;
		}
		public CsvFileInfoAttribute(char separator, char delimiter)
			:this()
		{
			this.HasSeparator = true;
			this.Separator = separator;
			this.Delimiter = delimiter;
		}

		public CsvSettings GetSettings()
		{
			return this._csvSettings;
		}
	}
}
