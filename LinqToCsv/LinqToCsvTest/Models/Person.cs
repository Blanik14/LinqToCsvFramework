using LinqToCsv.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinqToCsvTest.Models
{
	public class Person
	{
		[CsvColumnInfo(0)]
		public string FirstName { get; set; }

		[CsvColumnInfo(1)]
		public string LastName { get; set; }

		[CsvColumnInfo(2)]
		public DateTime DateOfBirth { get; set; }

		[CsvColumnInfo(3)]
		public int Gender { get; set; }

		[CsvColumnInfo(4)]
		public string Description { get; set; }

	}
}
