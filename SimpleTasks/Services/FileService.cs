using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SimpleTasks.Controllers
{
	public abstract class FileService<T>
	{
		public virtual List<T> Import(string fileName) => null;
		public virtual bool Export(string fileName, List<T> list) => false;
	}

	public abstract class ImportBooks : FileService<Book>
	{
	}

	public abstract class ImportBooksTextFile : ImportBooks
	{
	}

	public class ImportBookJson : ImportBooksTextFile
	{
        public override List<Book> Import(string fileName)
        {
			string data = Regex.Replace(File.ReadAllText(fileName), "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

            return JsonSerializer.Deserialize<List<Book>>(data, new JsonSerializerOptions() { IncludeFields = true, PropertyNameCaseInsensitive = true });
        }
    }
}

