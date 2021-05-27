using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using GradesTracker.Data;

namespace GradesTracker.Logic
{
    public class JsonReader
    {
        public static List<Course> ReadJsonFile(string jsonFile)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Course>));
            FileStream stream = File.OpenRead(jsonFile);
            List<Course> courses = (List<Course>)js.ReadObject(stream);
            stream.Close();

            return courses;
        }
    }
}
