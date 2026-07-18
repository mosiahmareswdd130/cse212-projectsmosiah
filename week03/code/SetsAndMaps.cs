using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Problem 1 - Find Pairs with Sets (O(n))
    ///
    /// Strategy: loop through each word once and add it to a HashSet.
    /// Before adding, check if the reverse of the word is already in the set.
    /// If yes → symmetric pair found. Skip words where both letters are the same (e.g. "aa").
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var pairs = new List<string>();

        foreach (string word in words)
        {
            // Skip words like "aa" where both letters are the same
            if (word[0] == word[1])
                continue;

            string reversed = $"{word[1]}{word[0]}";

            if (seen.Contains(reversed))
            {
                // Found a symmetric pair — order doesn't matter per spec
                pairs.Add($"{word} & {reversed}");
            }
            else
            {
                seen.Add(word);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Problem 2 - Degree Summary with Dictionary
    ///
    /// Strategy: read each line, grab the 4th field (index 3), and use it
    /// as a key in a dictionary. Increment the count each time we see it.
    /// TryGetValue avoids a double lookup compared to ContainsKey + indexer.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // Degree is in the 4th column (index 3), trim whitespace
            if (fields.Length >= 4)
            {
                string degree = fields[3].Trim();
                if (degrees.TryGetValue(degree, out int count))
                    degrees[degree] = count + 1;
                else
                    degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Problem 3 - Anagram Check with Dictionary
    ///
    /// Strategy: build a letter frequency dictionary for word1, then
    /// subtract using word2. If any count goes below 0 or a letter is
    /// missing, they are not anagrams. Ignores spaces and case.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize: lowercase and remove spaces
        word1 = word1.ToLower().Replace(" ", "");
        word2 = word2.ToLower().Replace(" ", "");

        // Different total letters → can't be anagrams
        if (word1.Length != word2.Length)
            return false;

        // Count letter frequencies for word1
        var letterCount = new Dictionary<char, int>();
        foreach (char c in word1)
        {
            if (letterCount.TryGetValue(c, out int count))
                letterCount[c] = count + 1;
            else
                letterCount[c] = 1;
        }

        // Subtract using word2 — if any letter is missing or goes negative, not an anagram
        foreach (char c in word2)
        {
            if (!letterCount.TryGetValue(c, out int count) || count == 0)
                return false;
            letterCount[c] = count - 1;
        }

        return true;
    }

    /// <summary>
    /// Problem 5 - Earthquake Daily Summary from USGS JSON API
    ///
    /// The JSON structure is:
    ///   FeatureCollection → features[] → each Feature has properties → { place, mag }
    /// We deserialize into matching C# classes (defined in FeatureCollection.cs)
    /// and format each earthquake as "place - Mag mag"
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Build one string per earthquake: "place - Mag magnitude"
        return featureCollection?.Features
            .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag}")
            .ToArray() ?? [];
    }
}