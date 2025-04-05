using System.Text.Json;

namespace HumanCapitalManagement.Infrastructure;

public class JsonStoreAccessor
{
	protected readonly string basePath = Path.Combine(AppContext.BaseDirectory, "DummyStore");

	protected List<T> LoadFromJson<T>(string fileName)
	{
		var fullPath = Path.Combine(this.basePath, fileName);

		if (!File.Exists(fullPath))
			throw new FileNotFoundException($"Could not find file at path: {fullPath}");

		var jsonData = File.ReadAllText(fullPath);
		return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
	}


	protected void SaveToJson<T>(string fileName, List<T> data)
	{
		var fullPath = Path.Combine(this.basePath, fileName);
		var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText(fullPath, json);
	}

}
