using System;
using System.Collections.Generic;
using GradesTracker.Data;
using GradesTracker.Logic;

namespace GradesTracker.Presentation
{
    public class Ui
    {
        private static readonly int TITLE_DASH = 82;
        private static readonly int FOOTER_DASH = 84;

        private enum Footer
        {
            COURSE_SUMMARY,
            EVALUATION_SUMMARY,
            EVALUATION_SPECIFIC
        }

        public Ui()
        {
        }

        public void PrintCourseSummary(List<Course> courses)
        {
            PrintTitle("Grades Summary");
            PrintBody(courses);
            PrintFooter(Footer.COURSE_SUMMARY);
        }

        public void PrintEvaluationSummary(Course course)
        {
            PrintTitle($"{course.Code} Evaluations");
            PrintBody(course);
            PrintFooter(Footer.EVALUATION_SUMMARY);
        }

        public void PrintEvaluationSpecific(Course course, Evaluation eval)
        {
            PrintTitle($"{course.Code} {eval.Description}");
            PrintBody(eval);
            PrintFooter(Footer.EVALUATION_SPECIFIC);
        }

        private void PrintTitle(string title)
        {
            int titleLength = title.Length;

            // title
            Console.Write("".PadRight((TITLE_DASH - 24) / 2, ' '));
            Console.Write("~ GRADES TRACKING SYSTEM ~");
            Console.WriteLine("".PadLeft((TITLE_DASH - titleLength) / 2, ' '));

            Console.WriteLine(
                    "+"
                    + "".PadRight(TITLE_DASH, '-')
                    + "+"
            );
            Console.Write("|");
            Console.Write("".PadRight((TITLE_DASH + 1 - titleLength) / 2));
            Console.Write(title);
            Console.Write("".PadLeft((TITLE_DASH - titleLength) / 2));
            Console.WriteLine('|');
            Console.WriteLine(
                    "+"
                    + "".PadRight(TITLE_DASH, '-')
                    + "+\n"
            );
        }

        private void PrintBody(List<Course> courses)
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("There are currently no saved courses.");
            }
            else
            {
                Console.WriteLine(
                        $"{"#.", -3}"
                        + $"{"Course", -12}"
                        + $"{"Marks Earned", 15}"
                        + $"{"Out Of", 10}"
                        + $"{"Percent", 11}"
                        + '\n'
                    );

                foreach (Course course in courses)
                {
                    Course c = course;
                    GradeManagement.CalculateEvaluations(ref c);
                    GradeManagement.RecalculateCourseTotal(ref c);

                    Console.Write(
                            $"{course.Id, -3}"
                            + $"{course.Code, -12}"
                            + $"{c.CourseMarksTotal, 15:f2}"
                            + $"{c.WeightTotal, 10:f2}"
                            + $"{c.PercentTotal, 11:f2}\n"
                        );
                }
            }
        }

        private void PrintBody(Course course)
        {
            if (course.Evaluations.Count == 0)
            {
                Console.WriteLine($"There are currently no evaluations for {course.Code}.");
            }
            else
            {
                GradeManagement.CalculateEvaluations(ref course);

                Console.WriteLine(
                    $"{"#.", -3}"
                    + $"{"Evaluation", -16}"
                    + $"{"Marks Earned", 15}"
                    + $"{"Out Of", 10}"
                    + $"{"Percent", 11}"
                    + $"{"Course Marks", 16}"
                    + $"{"Weight/100", 13}\n"
                    );

                foreach (Evaluation eval in course.Evaluations)
                {
                    Console.Write($"{eval.Id + ".", -3}");
                    Console.Write($"{eval.Description, -16}");
                    Console.Write($"{eval.EarnedMarks, 15:f2}");
                    Console.Write($"{eval.OutOf, 10:f2}");
                    Console.Write($"{eval.Percent, 11:f2}");
                    Console.Write($"{eval.CourseMarks, 16:f2}");
                    Console.Write($"{eval.Weight, 13:f2}\n");
                }
            }
        }

        private void PrintBody(Evaluation eval)
        {
            Console.WriteLine(
                    $"{"Marks Earned", -12}"
                    + $"{"Out Of", 10}"
                    + $"{"Percent", 11}"
                    + $"{"Course Marks", 16}"
                    + $"{"Weight/100", 13}"
            );

            Console.Write($"{eval.EarnedMarks, 12:f2}");
            Console.Write($"{eval.OutOf, 10:f2}");
            Console.Write($"{eval.Percent, 11:f2}");
            Console.Write($"{eval.CourseMarks, 16:f2}");
            Console.Write($"{eval.Weight, 13:f2}\n");
        }

        private void PrintFooter(Footer footer)
        {
            if (footer == Footer.COURSE_SUMMARY)
            {
                Console.WriteLine(
                        "\n"
                        + "".PadRight(FOOTER_DASH, '-')
                        + "\n Press # from the above list to view/edit/delete a specific course.\n"
                        + " Press A to add a new course.\n"
                        + " Press X to quit.\n"
                        + "".PadRight(FOOTER_DASH, '-')
                        + "\n"
                );

                Console.Write("Enter a command: ");
            }
            else if (footer == Footer.EVALUATION_SUMMARY)
            {
                Console.WriteLine(
                        "\n"
                        + "".PadRight(FOOTER_DASH, '-')
                        + "\n Press D to delete this course.\n"
                        + " Press A to add an evaluation\n"
                        + " Press # from the above list to edit/delete a specific evaluation.\n"
                        + " Press X to return to the main menu.\n"
                        + "".PadRight(FOOTER_DASH, '-')
                        + "\n"
                );

                Console.Write("Enter a command: ");
            }
            else if (footer == Footer.EVALUATION_SPECIFIC)
            {
                Console.WriteLine(
                        "\n"
                        + "".PadRight(FOOTER_DASH, '-')
                        + "\n Press D to delete this evaluation.\n"
                        + " Press E to edit this evaluation\n"
                        + " Press X to return to the main menu.\n"
                        + "".PadRight(FOOTER_DASH, '-')
                        + "\n"
                );

                Console.Write("Enter a command: ");
            }
        }
    }
}
