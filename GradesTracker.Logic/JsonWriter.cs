using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using GradesTracker.Data;

namespace GradesTracker.Logic
{
    public class JsonWriter
    {
        public static void WriteLibToJsonFile(List<Course> courses, string jsonFile)
        {
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(courses.GetType());
                File.WriteAllText(jsonFile, string.Empty);
                FileStream stream = File.OpenWrite(jsonFile);
                js.WriteObject(stream, courses);
                stream.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("ERROR: Can't write to the JSON file.");
            }
            catch(InvalidDataContractException)
            {
                Console.WriteLine("ERROR: Can't serialize the object to JSON.");
            }
        }

    }
}
