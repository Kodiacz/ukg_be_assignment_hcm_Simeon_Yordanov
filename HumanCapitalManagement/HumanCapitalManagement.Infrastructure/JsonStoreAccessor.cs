namespace HumanCapitalManagement.Infrastructure;

public class JsonStoreAccessor
{
	protected readonly string basePath = Path.Combine(AppContext.BaseDirectory, "DummyStore");

	public JsonStoreAccessor()
	{
		this.EnsureRuntimeDataInitialized();
	}

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

	private void EnsureRuntimeDataInitialized()
	{
		if (!Directory.Exists(basePath))
		{
			Directory.CreateDirectory(basePath);
			CopyInitialData();
		}
	}

	private void CopyInitialData()
	{
		var seedPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "HumanCapitalManagement.Infrastructure", "DummyStore");

		foreach (var file in Directory.GetFiles(seedPath, "*.json"))
		{
			var destFile = Path.Combine(basePath, Path.GetFileName(file));
			File.Copy(file, destFile, overwrite: false);
		}
	}
}
