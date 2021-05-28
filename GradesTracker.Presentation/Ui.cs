using System;
using System.Collections.Generic;
using GradesTracker.Data;
using GradesTracker.Logic;

namespace GradesTracker.Presentation
{
    public class Ui
    {
        private enum Footer
        {
            COURSE_SUMMARY,
            EVALUATION_SUMMARY,
            EVALUATION_SPECIFIC
        }

        public Ui()
        {
        }

        public void DrawSummary(List<Course> courses)
        {
            /*
            DrawTitle("Grades Summary");
            DrawBody(courses);
            DrawFooter(Footer.Main);
            */
        }

        /*
        public void DrawSpecific(Course course)
        {
            DrawTitle($"{course.Code} Evaluations");
            DrawBody(course);
            DrawFooter(Footer.Evaluation);
        }

        private void DrawTitle(string title)
        {
            //Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);

            Console.WriteLine(
                    "                       ~ GRADES TRACKING SYSTEM ~\n"
                    +  "+-----------------------------------------------------------------+\n"
                    + $"|                        {title}                           |\n"
                    +  "+-----------------------------------------------------------------+\n"
                );
        }

        private void DrawBody(List<Course> courses)
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("There are currently no saved courses.");
            }
            else
            {
                Console.WriteLine("#. Course\tMarks Earned\tOut Of\tPercent\n");

                int courseIndex = 0;

                foreach (Course course in courses)
                {
                    ++courseIndex;

                    Console.Write(
                            $"{courseIndex}  {course.Code}"
                    );

                    if (course.Evaluations.Count != 0)
                    {
                        foreach (Evaluation eval in course.Evaluations)
                        {
                            Console.WriteLine($"\t{eval.EarnedMarks}\t{eval.OutOf}\t{eval.Percent}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\t{0}\t{0.0}\t{0.0}");
                    }
                }
            }
        }

        private void DrawBody(Course course)
        {
            bool courseHasEval = CourseManagement.CourseHasEvaluation(course);
            if (!courseHasEval)
            {
                Console.WriteLine($"There are currently no evaluations for {course.Code}.");
            }
            else
            {
                Console.WriteLine("#. Evaluation\tMarks Earned\tOut Of\tPercent\tCourse Marks\tWeight/100\n");

                int evaluationIndex = 0;
                foreach (Evaluation eval in course.Evaluations)
                {
                    ++evaluationIndex;

                    //double percent = 100 * eval.EarnedMarks / eval.OutOf;
                    //double courseMarks = percent * eval.Weight / 100;

                    Console.WriteLine(
                            $"{evaluationIndex} "
                            + $"{eval.Description}"
                            + $"\t{eval.EarnedMarks}"
                            + $"\t{eval.OutOf}"
                            + $"\t{eval.Percent}"
                            + $"\t{eval.CourseMarks}"
                            + $"\t{eval.Weight / 100}"
                    );
                }
            }
        }

        private void DrawFooter(Footer footer)
        {
            if (footer == Footer.COURSE_SUMMARY)
            {
                Console.WriteLine(
                          "\n---------------------------------------------------------------------------\n"
                        + " Press # from the above list to view/edit/delete a specific course.\n"
                        + " Press A to add a new course.\n"
                        + " Press X to quit.\n"
                        + "---------------------------------------------------------------------------\n"
                );

                Console.Write("Enter a command: ");
            }
            else if (footer == Footer.EVALUATION_SUMMARY)
            {
                Console.WriteLine(
                          "\n---------------------------------------------------------------------------\n"
                        + " Press D to delete this course.\n"
                        + " Press A to add an evaluation\n"
                        + " Press # from the above list to edit/delete a specific evaluation.\n"
                        + " Press X to return to the main menu.\n"
                        + "---------------------------------------------------------------------------\n"
                );

                Console.Write("Enter a command: ");
            }
            else if (footer == Footer.EVALUATION_SPECIFIC)
            {
                Console.WriteLine(
                          "\n---------------------------------------------------------------------------\n"
                        + " Press D to delete this evaluation.\n"
                        + " Press E to edit this evaluation\n"
                        + " Press X to return to the main menu.\n"
                        + "---------------------------------------------------------------------------\n"
                );

                Console.Write("Enter a command: ");
            }
        }
        */
    }
}
