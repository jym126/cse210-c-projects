using System;

public class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    // Constructor
    public JournalEntry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    // Display the journal entry
    public void Display()
    {
        Console.WriteLine($"Date: {Date}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");
        Console.WriteLine();
    }

    // Convert entry to a CSV-safe format
    public string ToCsv()
    {
        string escape(string text) => $"\"{text.Replace("\"", "\"\"")}\"";
        return $"{escape(Date)},{escape(Prompt)},{escape(Response)}";
    }

    // Parse entry from CSV format
    public static JournalEntry FromCsv(string csvLine)
    {
        var parts = csvLine.Split(',');
        return new JournalEntry(parts[1].Trim('\"').Replace("\"\"", "\""), parts[2].Trim('\"').Replace("\"\"", "\""), parts[0].Trim('\"').Replace("\"\"", "\""));
    }
}
