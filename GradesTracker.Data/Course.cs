using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GradesTracker.Data
{
    [DataContract]
    public class Course
    {
        public int Id { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public List<Evaluation> Evaluations = new List<Evaluation>();
    }

    [DataContract]
    public class Evaluation
    {
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int OutOf { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public double? EarnedMarks { get; set; }
        public double Percent { get; set; }
        public double CourseMarks { get; set; }
    }
}
