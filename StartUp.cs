using System;
using System.Text.RegularExpressions;


namespace FindPeaks 
{
    
    internal class StartUp
    {
        static void Main(string[] args)
        {   
        //
        // Summary:  
        //     Takes input from the console. Accepts three data formats:
        //     1. "[int1, int2, ... intN]" 
        //     2. "passing_tests" loads and runs calucalations for predefined successfully test. 
        //     3. "falling_tests" loads and runs calucalations for predefined falling test. 
        //     Depending the input runs appropriate FindPeaksHandler function
        //
        // Returns:
        //     None
        //
        // Exceptions:
        //     T:System.OverflowException:
        //       The string to be parsed represent a num greater than 9223372036854775807

            while(true)
            {
                Console.WriteLine(
                    @"Enter data in format: [int1, int2 , ... , intN] or 
                    ""passing_tests"" to run passing tests or
                    ""falling_tests"" to run falling tests"
                );          
                var inputData = Console.ReadLine();
                if(inputData == "passing_tests" || inputData == "falling_tests")
                {
                    var tests = inputData == "falling_tests" ? FALLING_TESTS : PASSING_TESTS;
                    foreach (var line in tests)
                    {
                        try
                        {
                            FindPeaksHandler(line);                            
                        }
                        catch (OverflowException ex)
                        {                            
                            Console.WriteLine($"Error info: {ex.Message}");
                        }
                    }
                    break;
                }  
                else
                {
                    FindPeaksHandler(inputData);
                    break;
                }
            }
        }
        public static void FindPeaksHandler(string? inputData)
        {
        //
        // Summary:
        //     Parse string of type "[int1, int2, ... intN]" to array of int64 numbers.
        //     Then finds all peaks and coresponding indices. The edge cases of the first and the last number
        //     are included in possible solutions as they are compared to just one available neighbour.
        //     This approach is taken because, for example to find the linear function's single maximum.
        //     The result is then print on the console in format "<index>, <value>"
        //
        // Returns:
        //     None
        //
      
            inputData = inputData != null ? inputData : string.Empty;
            var rx = new Regex(@"\[\s*?(?<nums>[0-9,\s+-]+)\s*?\]", RegexOptions.Compiled);
            Match match = rx.Match(inputData);
            if(match.Success)            
            {                                   
                var nums =  Array.ConvertAll(match.Groups["nums"].Value.Trim().Split(','),Convert.ToInt64);                
                var upperLimit = nums.Count() - 1;
                var result = new List<string>();
                long left = long.MinValue;
                long right = long.MinValue;                
                for (int i = 0; i < nums.Count(); i++)
                {                    
                    try {left = nums[i - 1];}
                    catch (IndexOutOfRangeException){}

                    try{right = nums[i + 1];}                    
                    catch (IndexOutOfRangeException){right = long.MinValue;}

                    if(left < nums[i] && nums[i] > right)
                    {
                        result.Add($"{i}, {nums[i]}");
                    }  
                }
                Console.WriteLine(string.Join("; ", result));                
            }
            else
            {
                Console.WriteLine($"Wrong input data \"{inputData}\"");
            }
        }
        public static string[] PASSING_TESTS = 
        {
        //
        // Summary: 
        //  Passing tests
        // Returns:
        //  Array of strings representing single test

            "nums = [4, 3, 2, 1]",
            "nums = [1, 2, 3, 4]",
            "nums = [1, 2, 1, 3, 5, 8, 4]",
            "nums = [-1, -2, -1, -3, -5, -8, -4]",
            "nums = [1, 2, 3, 1]",
            "nums = [1, 2, -3, 1]",
            "nums = [1, 9223372036854775807, 3, 1]"
        };
        public static string[] FALLING_TESTS = 
        {
        //
        // Summary: 
        //  Falling tests
        // Returns:
        //  Array of strings representing single test
            "nums = [4, 3, 2, 1.1]",
            "nums = [1, 2, 3, YYTY]",            
            "nums = [1, 9223372036854775809, 3, 1]",
            "nums = []",
        };
        
    }
}
