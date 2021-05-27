using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GradesTracker.Data
{
    [DataContract]
    public class Course
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public List<Evaluation> Evaluations = new List<Evaluation>();

        /*
        public void print()
        {
            Console.WriteLine(Code);
            foreach (Evaluation eval in Evaluations)
            {
                Console.WriteLine(
                        $"Description: {eval.Description}\n"
                        + $"OutOf: {eval.OutOf}\n"
                        + $"Weight: {eval.Weight}\n"
                        + $"EarnedMarks: {eval.EarnedMarks}"
                        );
            }
        }
        */
    }

    [DataContract]
    public class Evaluation
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int OutOf { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public double EarnedMarks { get; set; }
    }
}
