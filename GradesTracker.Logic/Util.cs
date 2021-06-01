using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using GradesTracker.Data;

namespace GradesTracker.Logic
{
    public static class Util
    {
        public static T? GetValueOrNull<T>(string value) where T : struct
        { if (string.IsNullOrEmpty(value))
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

        public static bool ValidateJsonObj(Course course, string jsonSchema)
        {
            IList<string> validationEvents = new List<string>();

            try
            {
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText(jsonSchema));
                JObject c = JObject.FromObject(course);

                if (!c.IsValid(schema, out validationEvents))
                {
                    foreach(string msg in validationEvents)
                        Console.WriteLine(msg);

                    System.Threading.Thread.Sleep(Constants.TIMEOUT_2);

                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return true;
        }
    }
}
