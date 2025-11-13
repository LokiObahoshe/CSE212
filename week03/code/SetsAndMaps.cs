using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Creating a hashset
        var stringChar = new HashSet<(char, char)>();
        // Creating a list that will be turned into an array that stores the strings
        var result = new List<string>();

        foreach (var word in words)
        {
            // This if statement checks if both words are duplicates and if the words exceed 2 letters. skips it if so
            if (word.Length != 2 || word[0] == word[1])
                continue;

            // This variable stores the letters of the word in a format in instructions (in parenthesis and sperated by commas)
            var pair = (word[0], word[1]);
            // This variable contains the reversed letters
            var reversed = (word[1], word[0]);

            // This if statement checks if the letters have been reversed
            if (stringChar.Contains(reversed))
            {
                // This line of code combines the original and the reversed word and adds them to the list
                result.Add($"{word} & {word[1]}{word[0]}");
            }
            else
            {
                // If a reverse is not found, then this line of code adds the pair into the hashset to use later if needed
                stringChar.Add(pair);
            }
        }

        // This converts the list "result" into an array
        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            // Learned in my last class; split the line so that they are seperated by commas
            var fields = line.Split(',');

            // This line makes sure there are at least 4 columns before continuing
            if (fields.Length < 4)
                continue;

            // This line just trims whitespace so it looks nice
            var degree = fields[3].Trim();

            // This if statement checks if the degree is empty or not. If it is not empty, it updates the count
            if (!string.IsNullOrEmpty(degree))
            {
                if (degrees.ContainsKey(degree))
                    degrees[degree]++;
                else
                    degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // This makes the words look pretty by lowercasing and removing spaces
        word1 = word1.ToLower().Replace(" ", "");
        word2 = word2.ToLower().Replace(" ", "");

        // If the length is different when compared to each other, they aren't anagrams, so it returns false
        if (word1.Length != word2.Length)
            return false;

        // Creating a new dictionary to count the letters
        Dictionary<char, int> letterCounts = new();

        // This foreach counts the letters in word1 and stores them all individually in the dictionary
        foreach (char c in word1)
        {
            if (letterCounts.ContainsKey(c))
                letterCounts[c]++;
            else
                letterCounts[c] = 1;
        }

        // This foreach counts the letters in word2 and checks if it has any unnecessary letters that don't work with word1. It returns false if this is the case
        foreach (char c in word2)
        {
            if (!letterCounts.ContainsKey(c))
            {
                return false;
            }

            letterCounts[c]--;

            // This checks if word2 has any extra letters (automatically meaning it's false and doesn't work as an anagram)
            if (letterCounts[c] < 0)
                return false;
        }

        // This foreach checks if all words have similiar lengths and that they exactly match to each other
        foreach (var count in letterCounts.Values)
        {
            if (count != 0)
                return false;
        }

        // Returns true if all checks pass
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
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

        // This if statement checks if the featureCollection is null before proceeding. If it is null, it returns an emtpy string array, or "no data"
        if (featureCollection?.Features == null)
            return Array.Empty<string>();

        // Created a new list for storing earthquake summaries
        var list = new List<string>();

        // This foreach loops through each earthquake, going over all the earthquake reports
        foreach (var feature in featureCollection.Features)
        {
            // This if statement checks to make sure the magnitude and the place are not null
            if (feature.Properties?.Mag != null && !string.IsNullOrEmpty(feature.Properties.Place))
            {
                // If both the magnitude and the place are not null, this adds them to the list and formats them as instructed
                list.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
            }
        }

        // This converts the list "list" into an array
        return list.ToArray();
    }

}