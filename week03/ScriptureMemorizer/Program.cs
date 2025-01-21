using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureHider
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a scripture object with reference and text
            Scripture scripture = new Scripture(new Reference("Proverbs", 3, 5, 6), 
                "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");

            // Main loop to hide words and display scripture
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
