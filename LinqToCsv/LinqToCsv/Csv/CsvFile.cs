using LinqToCsv.Attributes;
using LinqToCsv.CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace LinqToCsv.Csv
{
	public class CsvFile<T>
	{
		private CsvModelSchema _schema;

		public CsvSettings CsvSettings
		{
			get
			{
				return this._schema.CsvSettings;
			}
		}

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

		private string[] SplitLine(string line)
		{
			if(this.CsvSettings.HasSeparator)
				return line.SplitBySeparator(this.CsvSettings.Separator, this.CsvSettings.Delimiter);

			var cellLengths = new List<int>();

			foreach(var column in this._schema.GetAllColumns())
			{
				cellLengths.Add(column.Length);
			}

			return line.SplitByCellLength(cellLengths.ToArray());
		}

		public IEnumerable<T> GetRecords()
		{
			using(StreamReader sr = new StreamReader(new FileStream(this.Path, FileMode.Open, FileAccess.Read)))
			{
				while(!sr.EndOfStream)
				{
					if(this.CsvSettings.HasColumnHeaders)
						continue;
					var line = sr.ReadLine();
					var cells = this.SplitLine(line);

					var model = Activator.CreateInstance<T>();
					int i = 0;
					foreach(var cell in cells)
					{
						object value = null;
						PropertyInfo pi = this._schema.GetBindingByIndex(i);
						var propType = pi.PropertyType;
						if (!String.IsNullOrEmpty(cell))
							value = Convert.ChangeType(cell, propType);
						pi.SetValue(model, value);
						i++;
					}
					yield return model;
				}
				yield break;
			}
		}
	}
}
