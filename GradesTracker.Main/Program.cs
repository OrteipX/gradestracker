using System;
using System.Collections.Generic;
using System.IO;
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

            List<Course> courses = new List<Course>();

            if (!File.Exists(gradesFile))
            {
                char ans;
                bool validInput;

                do
                {
                    Console.Write("Grades data file grade.json not found. Create new file? (y/n): ");

                    ans = Console.ReadKey().KeyChar;

                    validInput = (ans == 'Y' || ans == 'y' || ans == 'N' || ans == 'n');

                    if (!validInput)
                        Console.WriteLine("\nWrong input!");

                } while (!validInput);


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

            courses = JsonReader.ReadJsonFile(gradesFile);

            Course c1 = new Course{
                Code = "INFO-9876"
            };


            //JsonWriter.WriteLibToJsonFile(courses, gradesFile);

            Ui ui = new Ui();
            ui.DrawSummary(courses);

            string opt = Console.ReadLine();
            int num;

            if (int.TryParse(opt, out num))
            {
                ui.DrawSpecific(courses[num -1]);
            }

            Console.WriteLine("\nME MAMA HARRY POTTER SEU IMUNDOOOOO!!!!!");
        }
    }
}
