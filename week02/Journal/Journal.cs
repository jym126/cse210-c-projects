using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<JournalEntry> _entries = new List<JournalEntry>();
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is one lesson I learned today?",
        "What am I most grateful for today?"
    };

    // Add a new entry
    public void AddEntry()
    {
        Random random = new Random();
        string prompt = _prompts[random.Next(_prompts.Count)];

        Console.WriteLine("Prompt:");
        Console.WriteLine(prompt);
        Console.WriteLine("Your response:");
        string response = Console.ReadLine();

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        _entries.Add(new JournalEntry(prompt, response, date));

        Console.WriteLine("Entry added successfully!\n");
    }

    // Display all entries
    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries to display.\n");
            return;
        }

        Console.WriteLine("Journal Entries:");
        foreach (var entry in _entries)
        {
            entry.Display();
        }
    }

    // Save entries to a text file
    public void SaveToFile()
    {
        Console.Write("Enter the filename to save (e.g., journal.txt): ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in _entries)
            {
                writer.WriteLine("Date: " + entry.Date);
                writer.WriteLine("Prompt: " + entry.Prompt);
                writer.WriteLine("Response: " + entry.Response);
                writer.WriteLine(); // Blank line between entries
            }
        }

        Console.WriteLine("Journal saved successfully to a text file!\n");
    }

    // Load entries from a text file
    public void LoadFromFile()
    {
        Console.Write("Enter the filename to load (e.g., journal.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.\n");
            return;
        }

        _entries.Clear();

        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            string date = "", prompt = "", response = "";
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("Date: "))
                {
                    date = line.Substring(6);
                }
                else if (line.StartsWith("Prompt: "))
                {
                    prompt = line.Substring(8);
                }
                else if (line.StartsWith("Response: "))
                {
                    response = line.Substring(10);
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    // Blank line indicates end of an entry
                    _entries.Add(new JournalEntry(prompt, response, date));
                }
            }
        }

        Console.WriteLine("Journal loaded successfully from a text file!\n");
    }
}
