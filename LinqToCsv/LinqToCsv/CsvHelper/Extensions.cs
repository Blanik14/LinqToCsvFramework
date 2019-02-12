using System;
using System.Collections.Generic;
using System.Text;

namespace LinqToCsv.CsvHelper
{
	public static class Extensions
	{
		public static string[] SplitBySeparator(this string s, char separator, char delimiter)
		{
			var cells = new List<string>();
			StringBuilder sb = new StringBuilder();	
			bool ignoreSeparator = false;

			foreach(var c in s)
			{
				if(c == delimiter)
					ignoreSeparator = !ignoreSeparator;
				if(c == separator && !ignoreSeparator)
				{
					cells.Add(sb.ToString());
					sb.Clear();
					continue;
				}
					
				sb.Append(c);
			}

			if(sb.Length > 0)
				cells.Add(sb.ToString());

			return cells.ToArray();
		}

		public static string[] SplitByCellLength(this string s, params int[] cellLengts)
		{
			int offset = 0;
			var cells = new List<string>();

			foreach(var cellLength in cellLengts)
			{
				cells.Add(s.Substring(offset, cellLength));
				offset += cellLength;
			}

			return cells.ToArray();
		}
	}
}
