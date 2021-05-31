using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace GradesTracker.Logic
{
    public static class Util
    {
        public static T? GetValueOrNull<T>(string value) where T : struct
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else
                return (T)Convert.ChangeType(value, typeof(T));
        }

        public static bool ValidateEvaluation(int num, int lBound = int.MinValue, int uBound = int.MaxValue)
        {
            if (num < lBound)
            {
                Console.WriteLine($"ERROR: Invalid evaluation. Integer {num} is less than "
                        + $"minimun value of {lBound}.");
                return false;
            }
            else if (num > uBound)
            {
                Console.WriteLine($"ERROR: Invalid evaluation. Integer {num} is greater than "
                        + $"maximun value of {uBound}.");
                return false;
            }

            return true;
        }

        public static bool ValidateEvaluation(double num, double lBound = double.MinValue, double uBound = double.MaxValue)
        {
            if (num < lBound)
            {
                Console.WriteLine($"ERROR: Invalid evaluation. Double {num} is less than "
                        + $"minimun value of {lBound}.");
                return false;
            }
            else if (num > uBound)
            {
                Console.WriteLine($"ERROR: Invalid evaluation. Double {num} is greater than "
                        + $"maximun value of {uBound}.");
                return false;
            }

            return true;
        }

        public static bool ValidateJsonObj(string jsonString, string jsonSchema)
        {
            IList<string> validationEvents = new List<string>();
            try
            {
                JsonSchema schema = JsonSchema.Parse(System.IO.File.ReadAllText(jsonSchema));
                JObject jsonObject = JObject.Parse(jsonString);

                if (jsonObject.IsValid(schema, out validationEvents))
                {
                    return true;
                }
                else
                {
                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;

        }
    }
}
