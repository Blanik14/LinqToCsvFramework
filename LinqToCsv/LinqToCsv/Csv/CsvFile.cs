using LinqToCsv.Attributes;
using LinqToCsv.CsvHelper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinqToCsv.Csv
{
	public class CsvFile<T>
	{
		private CsvModelSchema _schema;

		public string Path { get; set; }

		public CsvFile(string path)
			:this(path, null)
		{
		}

		public CsvFile(string path, CsvSettings settings)
		{ 
			this.Path = path;
			this.CreateSchema(settings);
		}

		private void CreateSchema()
		{
			this.CreateSchema(null);
		}

		private void CreateSchema(CsvSettings settings)
		{
			var modelType = typeof(T);
			CsvModelSchema schema;

			if(settings is null)
			{
				schema = new CsvModelSchema(modelType);
			}
			else
			{
				schema = new CsvModelSchema(modelType, settings);
			}

			this._schema = schema;
		}

		
	}
}
