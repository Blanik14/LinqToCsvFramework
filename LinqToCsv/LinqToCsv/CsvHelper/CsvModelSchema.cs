using LinqToCsv.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using LinqToCsv.Csv;

namespace LinqToCsv.CsvHelper
{
	internal class CsvModelSchema
	{
		private readonly IDictionary<CsvColumnInfoAttribute, PropertyInfo> _schema;

		internal CsvSettings CsvSettings { get; }

		internal Type ModelType { get; private set; }

		private CsvModelSchema()
		{
			this._schema = new Dictionary<CsvColumnInfoAttribute, PropertyInfo>();
			
		}
		internal CsvModelSchema(Type modelType)
			:this()
		{  
			this.Initialize(modelType);
			this.CsvSettings = this.GetSettingsFromModel(modelType);
		}

		internal CsvModelSchema(Type modelType, CsvSettings settings):
			this()
		{
			this.Initialize(modelType);
			this.CsvSettings = settings;
			
		}

		private void Initialize(Type modelType)
		{
			this.ModelType = modelType;

			var properties = this.ModelType.GetProperties(System.Reflection.BindingFlags.Public);
			foreach(PropertyInfo property in properties)
			{
				var attribute = property.GetCustomAttribute<CsvColumnInfoAttribute>();
				this.AddBinding(attribute, property);
			}
		}

		internal PropertyInfo GetBinding(CsvColumnInfoAttribute attribute)
		{
			return this._schema[attribute];
		}

		internal PropertyInfo GetBindingByIndex(int index)
		{
			var pi = (from p in this._schema
					  where p.Key.Index == index
					  select p.Value).FirstOrDefault();

			if(pi == default(PropertyInfo))
			{
				return null;
			}
			return pi;
		}

		internal PropertyInfo GetBindingByName(string name)
		{
			var pi = (from p in this._schema
					  where p.Key.Title == name
					  select p.Value).FirstOrDefault();

			if(pi == default(PropertyInfo))
			{
				return null;
			}
			return pi;
		}

		internal IEnumerable<CsvColumnInfoAttribute> GetAllColumns()
		{
			foreach(var key in this._schema.Keys)
			{
				yield return key;
			}
		}

		private void AddBinding(CsvColumnInfoAttribute attribute, PropertyInfo propertyInfo)
		{
			this._schema.Add(attribute, propertyInfo);
		}

		private CsvSettings GetSettingsFromModel(Type modelType)
		{
			var attribute = modelType.GetCustomAttribute<CsvFileInfoAttribute>();
			return attribute.GetSettings();
		}
	}
}
