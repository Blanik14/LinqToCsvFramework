using System;
using System.Collections.Generic;
using System.Text;

namespace LinqToCsv.Csv
{
	public class CsvSettings
	{
		public static CsvSettings Default
		{
			get
			{
				return new CsvSettings(';', '"', false);
			}
		}

		public bool HasSeparator { get; set; }

		public char Separator { get; set; }

		public char Delimiter { get; set; }

		public bool HasColumnHeaders { get; set; }

		public CsvSettings(char separator, char delimiter, bool hasColumnHeaders)
		{
			this.Delimiter = delimiter;
			this.HasColumnHeaders = hasColumnHeaders;
			this.Separator = separator;
			this.HasSeparator = true;
		}
	}
}
