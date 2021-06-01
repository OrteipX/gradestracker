using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using GradesTracker.Data;
using GradesTracker.Logic;
using GradesTracker.Presentation;

namespace GradesTracker.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = System.IO.Directory.GetCurrentDirectory();
            string gradesFile = Path.GetFullPath(Path.Combine(
                        projectDirectory,
                        @"../GradesTracker.Data/",
                        Constants.JSON_FILE));
            string schemaFile = Path.GetFullPath(Path.Combine(
                        projectDirectory,
                        @"../GradesTracker.Data/",
                        Constants.JSON_SCHEMA));

            char ans;
            bool valid;
            bool validJson = false;

            if (!File.Exists(gradesFile))
            {
                do
                {
                    Console.Write("Grades data file grade.json not found. Create new file? (y/n): ");

                    ans = Console.ReadKey().KeyChar;

                    valid = (ans == 'Y' || ans == 'y' || ans == 'N' || ans == 'n');

                    if (!valid)
                        Console.WriteLine("\nWrong input!");

                } while (!valid);

                if (ans == 'Y' || ans == 'y')
                {
                    JsonWriter.WriteLibToJsonFile(new List<Course>(), gradesFile);

                    Console.WriteLine("\nNew data set created. Press any key to continue...");
                    char dummy = Console.ReadKey().KeyChar;
                }
                else
                {
                    Console.WriteLine("\nNew data set NOT created."
                            + "\nAborting...");
                    return;
                }
            }

            List<Course> courses = new List<Course>();
            courses = JsonReader.ReadJsonFile(gradesFile);
            GradeManagement.ParseCourses(ref courses);

            Ui ui = new Ui();

            // first loop
            while (true)
            {
                Console.Clear();
                ui.PrintCourseSummary(courses);

                string option = Console.ReadLine();
                int courseId;

                if (option == "X")
                {
                    Console.WriteLine("Bye...");
                    break;
                }
                else if (option == "A")
                {
                    Console.Write("\nEnter a course code: ");
                    string code = Console.ReadLine();


                    if (GradeManagement.CourseExists(courses, code))
                    {
                        Console.WriteLine("ERROR: Course already exists.");
                        Thread.Sleep(Constants.TIMEOUT);
                        continue;
                    }

                    Course c = new Course { Code = code };

                    validJson = Util.ValidateJsonObj(c, schemaFile);

                    if (!validJson)
                    {
                        c = null;
                        continue;
                    }
                    else
                    {
                        validJson = true;
                    }

                    GradeManagement.AddCourse(ref courses, c);
                }
                else if (int.TryParse(option, out courseId))
                {
                    if (courseId <= 0 || courseId > courses.Count)
                    {
                        Console.WriteLine("ERROR: Course Id out not found.");
                        Thread.Sleep(Constants.TIMEOUT);
                        continue;
                    }

                    Course selectedCourse = GradeManagement.GetCourse(courses, courseId);
                    GradeManagement.ParseEvaluations(ref selectedCourse.Evaluations);

                   // second loop
                    while (true)
                    {
                        Console.Clear();
                        ui.PrintEvaluationSummary(selectedCourse);

                        option = Console.ReadLine();

                        int evaluationId;

                        if (option == "X")
                        {
                            break;
                        }
                        else if (option == "D")
                        {
                            validJson = true;

                            do
                            {
                                Console.Write($"\nDelete {selectedCourse.Code}? (Y/N): ");
                                ans = Console.ReadKey().KeyChar;
                                valid = (ans == 'Y' || ans == 'N');

                                if (valid)
                                {
                                    if (ans == 'Y')
                                    {
                                        GradeManagement.DeleteCourse(ref courses, selectedCourse);
                                        Console.WriteLine($"\nCourse \'{selectedCourse.Code}\' removed.");
                                        Thread.Sleep(Constants.TIMEOUT);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nERROR: Invalid option.");
                                }
                            } while (!valid);

                            if (ans == 'Y')
                                break;
                        }
                        else if (option == "A")
                        {
                            Evaluation eval = new Evaluation();

                            Console.Write("Enter description: ");
                            eval.Description = Console.ReadLine();

                            do
                            {
                                Console.Write("Enter the \'out of\' mark: ");
                                int outOf;
                                string data = Console.ReadLine();
                                valid = int.TryParse(data, out outOf);

                                if (valid)
                                {
                                    if (Util.ValidateEvaluation(outOf, 0))
                                    {
                                        eval.OutOf = outOf;
                                    }
                                    else
                                    {
                                        valid = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Invalid number.");
                                }

                            } while (!valid);

                            do
                            {
                                Console.Write("Enter the % weight: ");
                                double weight;
                                string data = Console.ReadLine();
                                valid = double.TryParse(data, out weight);

                                if (valid)
                                {
                                    if (Util.ValidateEvaluation(weight, 0, 100))
                                    {
                                        eval.Weight = weight;
                                    }
                                    else
                                    {
                                        valid = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Invalid number.");
                                }

                            } while (!valid);

                            do
                            {
                                Console.Write("Enter marks earned or press ENTER to skip: ");
                                double earnedMarks;
                                string data = Console.ReadLine();
                                valid = double.TryParse(data, out earnedMarks) || data == "";


                                if (valid)
                                {
                                    if (Util.ValidateEvaluation(earnedMarks, 0))
                                    {
                                        eval.EarnedMarks = Util.GetValueOrNull<double>(data);
                                    }
                                    else
                                    {
                                        valid = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Invalid number.");
                                }

                            } while (!valid);

                            validJson = Util.ValidateJsonObj(selectedCourse, schemaFile);

                            if (!validJson)
                            {
                                continue;
                            }
                            else
                            {
                                validJson = true;
                            }

                            GradeManagement.AddEvaluation(ref selectedCourse, eval);
                        }
                        else if (int.TryParse(option, out evaluationId))
                        {
                            if (evaluationId <= 0 || evaluationId > selectedCourse.Evaluations.Count)
                            {
                                Console.WriteLine("ERROR: Evaluation Id out not found.");
                                Thread.Sleep(Constants.TIMEOUT);
                                continue;
                            }

                            Evaluation selectedEvaluation = GradeManagement.GetEvaluation(selectedCourse.Evaluations, evaluationId);

                            // third loop
                            while (true)
                            {
                                Console.Clear();
                                ui.PrintEvaluationSpecific(selectedCourse, selectedEvaluation);

                                option = Console.ReadLine();

                                if (option == "X")
                                {
                                    Console.WriteLine("BACK SCREEN");
                                    break;
                                }
                                else if (option == "D")
                                {
                                    validJson = true;

                                    do
                                    {
                                        Console.Write($"\nDelete {selectedEvaluation.Description}? (Y/N): ");
                                        ans = Console.ReadKey().KeyChar;
                                        valid = (ans == 'Y' || ans == 'N');

                                        if (valid)
                                        {
                                            if (ans == 'Y')
                                            {
                                                GradeManagement.DeleteEvaluation(ref selectedCourse, selectedEvaluation);
                                                Console.WriteLine($"\nEvaluation \'{selectedEvaluation.Description}\' removed.");
                                                Thread.Sleep(Constants.TIMEOUT);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nERROR: Invalid option.");
                                        }
                                    } while (!valid);

                                    break;
                                }
                                else if (option == "E")
                                {
                                    int index;
                                    do
                                    {
                                        Console.Write("\nChoose the index of what you would like to edit or \"X\" to cancel."
                                                + "\n(Marks Earned [1], Out Of [2], Weight [3]): ");
                                        option = Console.ReadLine();

                                        if (option == "X")
                                        {
                                            index = 0;
                                            break;
                                        }

                                        valid = int.TryParse(option, out index);

                                        if (!valid)
                                        {
                                            Console.WriteLine("ERROR: Invalid option.");
                                        }
                                        else
                                        {
                                            if (index <= 0  || index > 3)
                                            {
                                                valid = false;
                                                Console.WriteLine("ERROR: Invalid index.");
                                            }
                                        }

                                    } while (!valid);

                                    if (index == 1)
                                    {
                                        do
                                        {
                                            string marks = selectedEvaluation.EarnedMarks == null
                                                ? "null"
                                                : selectedEvaluation.EarnedMarks.ToString();

                                            Console.Write($"Marks Earned: {marks}."
                                                    + $" Enter new value or press ENTER: ");

                                            string data = Console.ReadLine();

                                            double earnedMarks;
                                            valid = double.TryParse(data, out earnedMarks) || data == "";

                                            if (valid)
                                            {
                                                if (Util.ValidateEvaluation(earnedMarks, 0))
                                                {
                                                    selectedEvaluation.EarnedMarks = Util.GetValueOrNull<double>(data);
                                                }
                                                else
                                                {
                                                    valid = false;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: Invalid number.");
                                            }

                                        } while (!valid);

                                        break;
                                    }
                                    else if (index == 2)
                                    {
                                        do
                                        {
                                            Console.Write($"Out Of: {selectedEvaluation.OutOf}."
                                                    + $" Enter new value or press ENTER: ");

                                            string data = Console.ReadLine();

                                            if (string.IsNullOrEmpty(data))
                                                break;

                                            int outOf;
                                            valid = int.TryParse(data, out outOf);

                                            if (valid)
                                            {
                                                if (Util.ValidateEvaluation(outOf, 0))
                                                {
                                                    selectedEvaluation.OutOf = outOf;
                                                }
                                                else
                                                {
                                                    valid = false;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: Invalid number.");
                                            }

                                        } while (!valid);

                                        break;
                                    }
                                    else if (index == 3)
                                    {
                                        do
                                        {
                                            Console.Write($"Weight: {selectedEvaluation.Weight}."
                                                    + $" Enter new value or press ENTER: ");

                                            string data = Console.ReadLine();

                                            if (string.IsNullOrEmpty(data))
                                                break;

                                            double weight;
                                            valid = double.TryParse(data, out weight);

                                            if (valid)
                                            {
                                                if (Util.ValidateEvaluation(weight, 0))
                                                {
                                                    selectedEvaluation.Weight = weight;
                                                }
                                                else
                                                {
                                                    valid = false;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: Invalid number.");
                                            }

                                        } while (!valid);

                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Option not available.");
                                    Thread.Sleep(Constants.TIMEOUT);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Option not available.");
                            Thread.Sleep(Constants.TIMEOUT);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: Option not available.");
                    Thread.Sleep(Constants.TIMEOUT);
                }
            }

            if (validJson)
                JsonWriter.WriteLibToJsonFile(courses, gradesFile);
        }
    }
}
