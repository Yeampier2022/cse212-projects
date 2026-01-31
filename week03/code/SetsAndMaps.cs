using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.IO;

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
        // TODO Problem 1 - ADD YOUR CODE HERE
        var seenWords = new HashSet<string>();
        var results = new List<string>();

        foreach (string word in words)
        {
            // Create the symmetric mirror of the current word
            // Since they are always 2 chars, we can just swap indices
            string mirror = $"{word[1]}{word[0]}";

            // If the mirror is in the set, we've found a pair
            if (seenWords.Contains(mirror))
            {
                results.Add($"{mirror} & {word}");
            }
            else
            {
                // Otherwise, add the current word to the set to check against future words
                seenWords.Add(word);
            }
        }

        return results.ToArray();
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
            var fields = line.Split(",");

            // Problem 2 - Solution
            if (fields.Length >= 4)
            {
                // Extract the 4th column (index 3) and clean up whitespace
                string degree = fields[3].Trim();

                // If the degree is already in the dictionary, increment its count
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                // Otherwise, add the degree to the dictionary with an initial count of 1
                else
                {
                    degrees[degree] = 1;
                }
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
        // TODO Problem 3 - ADD YOUR CODE HERE
        // 1. Normalize: Remove spaces and convert to lowercase
        string s1 = word1.Replace(" ", "").ToLower();
        string s2 = word2.Replace(" ", "").ToLower();

        // 2. Quick check: If lengths differ, they can't be anagrams
        if (s1.Length != s2.Length)
        {
            return false;
        }

        var charCounts = new Dictionary<char, int>();

        // 3. Populate the dictionary with counts from the first word
        foreach (char c in s1)
        {
            if (charCounts.ContainsKey(c))
            {
                charCounts[c]++;
            }
            else
            {
                charCounts[c] = 1;
            }
        }

        // 4. Subtract counts using the second word
        foreach (char c in s2)
        {
            // If the character isn't there or the count is already 0, it's not an anagram
            if (!charCounts.ContainsKey(c) || charCounts[c] == 0)
            {
                return false;
            }

            charCounts[c]--;
        }

        // 5. If we successfully decremented everything to zero, they are anagrams
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

        using var response = client.Send(getRequestMessage);
        using var jsonStream = response.Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var earthquakeDescriptions = new List<string>();

        if (featureCollection?.Features != null)
        {
            foreach (var item in featureCollection.Features)
            {
                string place = item.Properties?.Place ?? "Unknown Location";
                double? mag = item.Properties?.Mag;

                earthquakeDescriptions.Add($"{place} - Mag {mag}");
            }
        }

        return earthquakeDescriptions.ToArray();
    }
}