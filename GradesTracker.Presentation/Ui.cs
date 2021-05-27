using System;
using System.Collections.Generic;
using GradesTracker.Data;

namespace GradesTracker.Presentation
{
    public enum Footer
    {
        Main,
        Evaluation
    }

    public class Ui
    {
        public Ui()
        {
        }

        public void DrawSummary(List<Course> courses)
        {
            DrawTitle("Grades Summary");
            DrawBody(courses);
            DrawFooter(Footer.Main);
        }

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

        private void DrawBody(List<Course> courses =  null)
        {
            if (courses == null)
            {
                Console.WriteLine("There are currently no saved courses.\n");
            }
            else
            {
                Console.WriteLine("#. Course\tMarks Earned\tOut Of\tPercent\n");

                int courseIndex = 0;

                foreach (Course course in courses)
                {
                    ++courseIndex;

                    Console.Write(
                            $"{courseIndex}. {course.Code}"
                    );

                    foreach (Evaluation eval in course.Evaluations)
                    {
                        Console.WriteLine($"\t{eval.EarnedMarks}\t{eval.OutOf}\t{100 * eval.EarnedMarks / eval.Weight}");
                    }
                }
            }
        }

        private void DrawBody(Course course = null)
        {
            if (course == null)
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

                    double percent = 100 * eval.EarnedMarks / eval.OutOf;
                    double courseMarks = percent * eval.Weight / 100;

                    Console.WriteLine(
                            $"{evaluationIndex} "
                            + $"{eval.Description}"
                            + $"\t{eval.EarnedMarks}"
                            + $"\t{eval.OutOf}"
                            + $"\t{percent}"
                            + $"{courseMarks}"
                            + $"{eval.Weight / 100}"
                    );
                }
            }
        }

        private void DrawFooter(Footer footer)
        {
            if (footer == Footer.Main)
            {
                Console.WriteLine(
                          "---------------------------------------------------------------------------\n"
                        + " Press # from the above list to view/edit/delete a specific course.\n"
                        + " Press A to add a new course.\n"
                        + " Press X to quit.\n"
                        + "---------------------------------------------------------------------------\n"
                );

                Console.Write("Enter a command: ");
            }
            else if (footer == Footer.Evaluation)
            {
                Console.WriteLine(
                          "---------------------------------------------------------------------------\n"
                        + " Press D to delete this course.\n"
                        + " Press A to add an evaluation\n"
                        + " Press # from the above list to edit/delete a specific evaluation.\n"
                        + " Press X to return to the main menu.\n"
                        + "---------------------------------------------------------------------------\n"
                );

                Console.Write("Enter a command: ");

            }
        }
    }
}
