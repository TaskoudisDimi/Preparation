using System;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;
class Program
{
    static void Main(string[] args)
    {

        Task.Run(async () =>
        {
            await RunAsync();
        }).Wait();

    }

    static async Task RunAsync()
    {
        var httpClient = new HttpClient();
        var responseString = await httpClient.GetStringAsync("https://api.open-meteo.com/v1/forecast?latitude=35&longitude=25&current_weather=true");

        //var weatherData = JsonSerializer.Deserialize<WeatherResponse>(responseString);
        var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(responseString);

        if (weatherData?.current_weather != null)
        {
            var output = $"Temperature: {weatherData.current_weather.temperature}°C\n" +
                         $"Wind Speed: {weatherData.current_weather.windspeed} km/h\n" +
                         $"Time: {weatherData.current_weather.time}";

            Console.WriteLine(output);

            // Write the data to file
            File.WriteAllText("weather_output.txt", output);
            Console.WriteLine("Weather written to weather_output.txt");
        }
        else
        {
            Console.WriteLine("No weather data found.");
        }
    }
    // TwoSum problem
    public static int[] TwoSum(int[] nums, int target)
    {
        var dict = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            if (dict.ContainsKey(complement))
            {
                return new int[] { dict[complement], i };
            }
            dict[nums[i]] = i;
        }
        return Array.Empty<int>();
    }

    // Contains Duplicate
    public static bool ContainsDuplicate(int[] nums)
    {
        var set = new HashSet<int>();
        foreach (int n in nums)
        {
            if (!set.Add(n))
            {
                return true;
            }
        }
        return false;
    }


    // Group strings that are anagrams of each other
    public static List<List<string>> GroupAnagrams(string[] strs)
    {
        var map = new Dictionary<string, List<string>>();
        foreach(string str in strs)
        {
            var chars = str.ToCharArray();
            Array.Sort(chars);
            var word = new string(chars);
            if (!map.ContainsKey(word))
            {
                map[word] = new List<string>(); 
            }
            map[word].Add(str);
        }
        return map.Values.ToList();


    }


    // Valid Anagram 
    public static bool IsAnagram(string word1, string word2)
    {
        if (word1.Length != word2.Length)
        {
            return false;
        }
        var count = new int[26];
        for (int i = 0; i < word1.Length; i++)
        {
            count[word1[i] - 'a']++;
            count[word2[i] - 'a']--;
        }
        return count.All(c => c == 0);

    }


    //  Longest Substring Without Repeating Characters
    public static int LengthOfLongestSubstring(string s)
    {
        var set = new HashSet<char>();
        int left = 0;
        int maxLength = 0;
        for (int right = 0; right < s.Length; right++)
        {
            while (!set.Add(s[right]))
            {
                set.Remove(s[left++]);
            }
            maxLength = Math.Max(maxLength, right - left + 1);
        }
        return maxLength;
    }

    // Encode and Decode Strings 
    public class Codec
    {
        public string Encode(IList<string> strs)
        {
            return string.Concat(strs.Select(s => s.Length + "#" + s));
        }

        public IList<string> Decode(string s)
        {
            var result = new List<string>();
            int i = 0;
            while (i < s.Length)
            {
                int j = s.IndexOf('#', i);
                int len = int.Parse(s.Substring(i, j - i));
                i = j + 1;
                result.Add(s.Substring(i, len));
                i += len;
            }
            return result;
        }
    }



    // Minimum Size Subarray Sum
    public static int MinSubArrayLen(int target, int[] nums)
    {
        int left = 0, sum = 0, minLen = int.MaxValue;
        for (int right = 0; right < nums.Length; right++)
        {
            sum += nums[right];
            while (sum >= target)
            {
                minLen = Math.Min(minLen, right - left + 1);
                sum -= nums[left++];
            }
        }
        return minLen == int.MaxValue ? 0 : minLen;
    }

    // Min Stack 
    public class MinStack
    {
        private Stack<int> stack = new Stack<int>();
        private Stack<int> minStack = new Stack<int>();

        public void Push(int val)
        {
            stack.Push(val);
            if (minStack.Count == 0 || val <= minStack.Peek())
                minStack.Push(val);
        }

        public void Pop()
        {
            if (stack.Pop() == minStack.Peek())
                minStack.Pop();
        }

        public int Top() => stack.Peek();
        public int GetMin() => minStack.Peek();
    }

    // Climbing Stairs
    public static int ClimbStairs(int n)
    {
        // ways(n) = ways(n-1) + ways(n-2)
        if (n <= 2) return n;
        int one = 1; // ways(n-2)
        int two = 2; // ways(n-1)
        for (int i = 3; i <= n; i++)
        {
            int tmp = one + two;
            one = two;
            two = tmp;
        }
        return two;
    }

    // Water trapped
    public static long TrapRainWater(long[] arr)
    {
        int n = arr.Length;
        if (n == 0) return 0;

        long[] leftMax = new long[n];
        long[] rightMax = new long[n];

        leftMax[0] = arr[0];
        for (int i = 1; i < n; i++)
        {
            leftMax[i] = Math.Max(leftMax[i - 1], arr[i]);
        }

        rightMax[n - 1] = arr[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            rightMax[i] = Math.Max(rightMax[i + 1], arr[i]);
        }

        long trappedWater = 0;
        for (int i = 0; i < n; i++)
        {
            trappedWater += Math.Min(leftMax[i], rightMax[i]) - arr[i];
        }

        return trappedWater;
    }



    // Weather class
    public class WeatherResponse
    {
        public CurrentWeather current_weather { get; set; }
    }

    public class CurrentWeather
    {
        public double temperature { get; set; }
        public double windspeed { get; set; }
        public string time { get; set; }
    }




}
