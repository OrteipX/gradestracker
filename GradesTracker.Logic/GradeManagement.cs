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

        public static void DeleteEvaluation(ref List<Evaluation> evaluations, Evaluation eval)
        {
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

        public static bool CourseHasEvaluation(Course course)
        {
            foreach (Evaluation eval in course.Evaluations)
            {
                if (!string.IsNullOrEmpty(eval.Description))
                    return true;
            }

            return false;
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
