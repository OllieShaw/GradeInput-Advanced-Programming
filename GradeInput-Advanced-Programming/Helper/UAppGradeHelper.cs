using System.Text.Json;

namespace Models.UappGrade
{
    public class UAppGradeHelper
    {
        public string GetSplitDataFromViewModel(List<SplitData>? splitData)
        {
            return GetSplitDataFromViewModelStatic(splitData);
        }
        public List<SplitData> GetSplitDataFromDbInstance(string? splitData)
        {
            return GetSplitDataFromDb(splitData);
        }

        public static string GetSplitDataFromViewModelStatic(List<SplitData>? splitData)
        {
            if (splitData == null || splitData.Count == 0)
            {
                return "[]";
            }

            var serialized = JsonSerializer.Serialize(splitData);

            if (string.IsNullOrEmpty(serialized))
            {
                throw new Exception("The split data is null after serialization for some reason");
            }

            return serialized;
        }

        public static List<SplitData> GetSplitDataFromDb(string? splitData)
        {
            if (string.IsNullOrWhiteSpace(splitData))
                return new List<SplitData>();

            try
            {
                return JsonSerializer.Deserialize<List<SplitData>>(splitData) ?? new List<SplitData>();
            }
            catch (JsonException)
            {
                // fallback for invalid JSON
                return new List<SplitData>();
            }
        }
    }
}
