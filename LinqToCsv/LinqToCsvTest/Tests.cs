using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using LinqToCsv.CsvHelper;
using LinqToCsv.Csv;
using LinqToCsvTest.Models;
using System.Linq;

namespace LinqToCsvTest
{	 
	[TestClass]
	public class Tests
	{
		private const string PATH_TO_IMPORT_CSV_FILE = @"..\..\..\Files\TestData.csv";

		[TestMethod]
		public void SplitBySeparatorTest()
		{
			var lines = new List<string>();
			using(var reader = new StreamReader(PATH_TO_IMPORT_CSV_FILE))
			{
				for(int i = 0; i < 4; i++)
				{
					lines.Add(reader.ReadLine());
				}
			}

			var line1 = lines[0].SplitBySeparator(';', '"');
			var line2 = lines[1].SplitBySeparator(';', '"');
			var line3 = lines[2].SplitBySeparator(';', '"');
			var line4 = lines[3].SplitBySeparator(';', '"');

			var expected1 = new string[5] { "FirstName", "LastName", "DateOfBirth", "Gender", "Description" };
			var expected2 = new string[5] { "Nils", "Macke", "24.05.1993", "0", "This is; a Separator and Delimiter Test!" };
			var expected3 = new string[5] { "Person2", "Person2", "24.09.1983", "0", "Description Without Delimiter" };
			var expected4 = new string[5] { "Person3", "Person3", "12.04.1993", "1", ""};

			CollectionAssert.AreEqual(expected1, line1);
			CollectionAssert.AreEqual(expected2, line2);
			CollectionAssert.AreEqual(expected3, line3);
			CollectionAssert.AreEqual(expected4, line4);

		}
		[TestMethod]
		public void SplitByLengthTest()
		{
			Assert.Fail();
		}
		[TestMethod]
		public void ReadAllObjectsFromFileVar1()
		{
			var settings = CsvSettings.Default;
			settings.HasColumnHeaders = true;
			var csvFile = new CsvFile<Person>(PATH_TO_IMPORT_CSV_FILE, settings);

			var persons = csvFile.GetRecords().ToList();
		}
	}
}
