using System;
using System.Collections.Generic;
using GradesTracker.Data;
using GradesTracker.Logic;

namespace GradesTracker.Logic
{
    public class GradeManagement
    {
        public static void ParseCourses(ref List<Course> courses)
        {
            int courseId = 0;

            foreach (Course c in courses)
                c.Id = ++courseId;
        }

        public static void ParseEvaluations(ref List<Evaluation> evaluations)
        {
            int evaluationId = 0;

            foreach (Evaluation e in evaluations)
                e.Id = ++evaluationId;
        }

        public static Course GetCourse(List<Course> courses, int id)
        {
            foreach (Course c in courses)
            {
                if (c.Id == id)
                    return c;
            }

            return null;
        }

        public static Evaluation GetEvaluation(List<Evaluation> evaluations, int id)
        {
            foreach (Evaluation e in evaluations)
            {
                if (e.Id == id)
                    return e;
            }

            return null;
        }

        public static void AddCourse(ref List<Course> courses, Course course)
        {
            courses.Add(course);
            ParseCourses(ref courses);
        }

        public static void DeleteCourse(ref List<Course> courses, Course course)
        {
            foreach (Course c in courses)
            {
                if (c.Id == course.Id)
                {
                    courses.Remove(c);
                    break;
                }
            }

            if (courses.Count > 0)
                ParseCourses(ref courses);
        }

        public static void AddEvaluation(ref Course course, Evaluation eval)
        {
            course.Evaluations.Add(eval);

            ParseEvaluations(ref course.Evaluations);
        }

        public static void DeleteEvaluation(ref Course course, Evaluation eval)
        {
            List<Evaluation> evaluations = course.Evaluations;

            foreach (Evaluation e in evaluations)
            {
                if (e.Id == eval.Id)
                {
                    evaluations.Remove(e);
                    break;
                }
            }

            if (evaluations.Count > 0)
                ParseEvaluations(ref evaluations);
        }

        public static void CalculateEvaluations(ref Course course)
        {
            List<Evaluation> evaluations = course.Evaluations;

            foreach (Evaluation eval in evaluations)
            {
                double? checkNullable = eval.EarnedMarks;
                double percent = 0.0;

                if (checkNullable.HasValue)
                {
                    double earnedMarks = checkNullable.Value;
                    percent = GradeManagement.CalculatePercent(eval.OutOf, earnedMarks);
                }

                double courseMarks = GradeManagement.CalculateCourseMarks(percent, eval.Weight);

                eval.Percent = percent;
                eval.CourseMarks = courseMarks;
            }
        }

        public static void RecalculateCourseTotal(ref Course course)
        {
            double courseMarksTotal = 0.0;
            double weightTotal = 0.0;

            foreach (Evaluation e in course.Evaluations)
            {
                courseMarksTotal += e.CourseMarks;
                weightTotal += e.Weight;
            }

            double percentTotal = 00;
            if (weightTotal > 0)
                percentTotal = 100 * courseMarksTotal / weightTotal;

            course.CourseMarksTotal = courseMarksTotal;
            course.WeightTotal = weightTotal;
            course.PercentTotal = percentTotal;
        }

        public static double CalculatePercent(int outOf, double earnedMarks)
        {
            if (outOf > 0)
                return 100 * earnedMarks / outOf;

            return 0;
        }

        public static double CalculateCourseMarks(double percent, double weight)
        {
            return percent * weight / 100;
        }

        public static bool CourseExists(List<Course> courses, string code)
        {
            foreach (Course c in courses)
            {
                if (string.Equals(c.Code.ToLower(), code.ToLower()))
                    return true;
            }

            return false;
        }
    }
}
