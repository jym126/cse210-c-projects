using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureHider
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load scriptures from a file
            List<Scripture> scriptures = LoadScripturesFromFile("scriptures.txt");
            
            // Ensure there are scriptures available in the list
            if (scriptures.Count == 0)
            {
                Console.WriteLine("No scriptures found in the file. Please add scriptures to the file and try again.");
                return;
            }

            // Display the list of available scriptures to the user
            Console.WriteLine("Select scriptures to memorize:");
            for (int i = 0; i < scriptures.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {scriptures[i].ToString()}");
            }

            // Get the user's selection of scriptures
            Console.WriteLine("\nEnter the numbers of the scriptures you want to memorize, separated by commas (e.g., 1,3,5):");
            string input = Console.ReadLine()?.Trim();

            List<int> selectedIndexes = input?.Split(',')
                                            .Select(i => int.TryParse(i.Trim(), out int index) ? index - 1 : -1)
                                            .Where(index => index >= 0 && index < scriptures.Count)
                                            .ToList();

            // If no valid scriptures were selected
            if (selectedIndexes == null || selectedIndexes.Count == 0)
            {
                Console.WriteLine("No valid scriptures selected. Exiting...");
                return;
            }

            // Let the user memorize each selected scripture
            foreach (int index in selectedIndexes)
            {
                Scripture selectedScripture = scriptures[index];
                MemorizeScripture(selectedScripture);
            }
        }

        // Method to load scriptures from a text file
        public static List<Scripture> LoadScripturesFromFile(string filePath)
        {
            List<Scripture> scriptures = new List<Scripture>();

            try
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    var parts = line.Split(';');
                    if (parts.Length == 2)
                    {
                        var referenceParts = parts[0].Split(' ');
                        var book = referenceParts[0];
                        var chapterAndVerse = referenceParts[1].Split(':');
                        var chapter = int.Parse(chapterAndVerse[0]);
                        var verseRange = chapterAndVerse[1].Split('-');
                        var startVerse = int.Parse(verseRange[0]);
                        var endVerse = verseRange.Length > 1 ? int.Parse(verseRange[1]) : startVerse;

                        scriptures.Add(new Scripture(new Reference(book, chapter, startVerse, endVerse), parts[1]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading scriptures from file: " + ex.Message);
            }

            return scriptures;
        }

        // Method to guide the user through memorizing a scripture
        public static void MemorizeScripture(Scripture scripture)
        {
            while (!scripture.IsFullyHidden())
            {
                Console.Clear();
                scripture.Display();

                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "quit")
                {
                    Environment.Exit(0);
                }

                scripture.HideRandomWords();
            }

            // Final display when all words are hidden
            Console.Clear();
            scripture.Display();
            Console.WriteLine("\nAll words are hidden. Press any key to exit.");
            Console.ReadKey();
        }
    }

    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        public void Display()
        {
            Console.WriteLine(_reference.ToString());
            Console.WriteLine(string.Join(' ', _words.Select(word => word.GetDisplayText())));
        }

        public void HideRandomWords()
        {
            Random random = new Random();
            List<Word> visibleWords = _words.Where(word => !word.IsHidden()).ToList();

            // Ensure we only hide words that are not already hidden
            if (visibleWords.Count > 0)
            {
                int wordsToHide = Math.Min(3, visibleWords.Count); // Hide up to 3 words at a time
                for (int i = 0; i < wordsToHide; i++)
                {
                    int index = random.Next(visibleWords.Count);
                    visibleWords[index].Hide();
                    visibleWords.RemoveAt(index);
                }
            }
        }

        public bool IsFullyHidden()
        {
            return _words.All(word => word.IsHidden());
        }

        public override string ToString()
        {
            return _reference.ToString();
        }
    }

    class Reference
    {
        public string Book { get; private set; }
        public int Chapter { get; private set; }
        public int StartVerse { get; private set; }
        public int EndVerse { get; private set; }

        // Constructor for a single verse
        public Reference(string book, int chapter, int verse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = verse;
            EndVerse = verse;
        }

        // Constructor for a range of verses
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public override string ToString()
        {
            if (StartVerse == EndVerse)
            {
                return $"{Book} {Chapter}:{StartVerse}";
            }
            else
            {
                return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
            }
        }
    }

    class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public void Hide()
        {
            _isHidden = true;
        }

        public bool IsHidden()
        {
            return _isHidden;
        }

        public string GetDisplayText()
        {
            return _isHidden ? new string('_', _text.Length) : _text;
        }
    }
}
