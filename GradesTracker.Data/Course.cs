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
        public double CourseMarksTotal { get; set; } = 0.0;
        public double WeightTotal { get; set; } = 0.0;
        public double PercentTotal { get; set; } = 0.0;
    }

    [DataContract]
    public class Evaluation
    {
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int OutOf { get; set; } = 0;
        [DataMember]
        public double Weight { get; set; } = 0.0;
        [DataMember]
        public double? EarnedMarks { get; set; } = 0.0;
        public double Percent { get; set; } = 0.0;
        public double CourseMarks { get; set; } = 0.0;
    }
}
