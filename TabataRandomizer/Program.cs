using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TabataRandomizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (File.Exists("tabata.txt"))
            {
                var text = "";

                using(var sr = new StreamReader("tabata.txt"))
                {
                    text = await sr.ReadToEndAsync();
                }

                var textSplit = text.Split("Legs\n");
                var legsOnly = textSplit[1].Split("Core\n");
                var legExercises = legsOnly[0]
                    .Split('•')
                    .Where(s => !string.IsNullOrWhiteSpace(s.Trim()))
                    .Select(s => s.Trim())
                    .Select(ExerciseMapper)
                    .ToList();

                var coreExercises = textSplit[1].Split("Core\n")[1]
                    .Split("Cardio\n")[0]
                    .Split('•')
                    .Where(s => !string.IsNullOrWhiteSpace(s.Trim()))
                    .Select(s => s.Trim())
                    .Select(ExerciseMapper)
                    .ToList();

                var cardioExercises = textSplit[1].Split("Cardio\n")[1]
                    .Split("Upper body\n")[0]
                    .Split('•')
                    .Where(s => !string.IsNullOrWhiteSpace(s.Trim()))
                    .Select(s => s.Trim())
                    .Select(ExerciseMapper)
                    .ToList();

                var upperBodyExercises = textSplit[1].Split("Upper body\n")[1]
                    .Split('•')
                    .Where(s => !string.IsNullOrWhiteSpace(s.Trim()))
                    .Select(s => s.Trim())
                    .Select(ExerciseMapper)
                    .ToList();

                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine($"ROUND {i + 1}");
                    
                    Console.WriteLine("******************************");
                    var rand = new Random();

                    var randomIndex = rand.Next(legExercises.Count());
                    var randomCoreIndex = rand.Next(coreExercises.Count());
                    var randomCardioIndex = rand.Next(cardioExercises.Count());
                    var randomUpperbodyIndex = rand.Next(upperBodyExercises.Count());

                    var randomLegExercise = legExercises[randomIndex];
                    legExercises.RemoveAt(randomIndex);

                    var randomCoreExercise = coreExercises[randomCoreIndex];
                    coreExercises.RemoveAt(randomCoreIndex);

                    var randomCardioExercise = cardioExercises[randomCardioIndex];
                    cardioExercises.RemoveAt(randomCardioIndex);

                    var randomUpperBodyExercise = upperBodyExercises[randomUpperbodyIndex];
                    upperBodyExercises.RemoveAt(randomUpperbodyIndex);

                    Console.WriteLine($"LEGS: {randomLegExercise}");
                    Console.WriteLine($"CORE: {randomCoreExercise}");
                    Console.WriteLine($"CARDIO: {randomCardioExercise}");
                    Console.WriteLine($"UPPER BODY: {randomUpperBodyExercise}");

                    Console.WriteLine();
                }
            }
        }

        public static Exercise ExerciseMapper(string s)
        {
            return new Exercise
            {
                Name = s
            };
        }
    }

    class Exercise
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}