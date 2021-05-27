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
            DataContractJsonSerializer js = new DataContractJsonSerializer(courses.GetType());
            FileStream stream = File.OpenWrite(jsonFile);
            js.WriteObject(stream, courses);
            stream.Close();
        }
    }
}
