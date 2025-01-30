using System;
using System.Collections.Generic;

// Class to represent a Comment
class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }
    
    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

// Class to represent a Video
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> comments;
    
    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        comments = new List<Comment>();
    }
    
    public void AddComment(string name, string text)
    {
        comments.Add(new Comment(name, text));
    }
    
    public int GetCommentCount()
    {
        return comments.Count;
    }
    
    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.Name}: {comment.Text}");
        }
        Console.WriteLine("--------------------------");
    }
}

// Class to represent a YouTube Channel
class YouTubeChannel
{
    public string ChannelName { get; set; }
    private List<Video> videos;
    
    public YouTubeChannel(string channelName)
    {
        ChannelName = channelName;
        videos = new List<Video>();
    }
    
    public void AddVideo(Video video)
    {
        videos.Add(video);
    }
    
    public void DisplayChannelInfo()
    {
        Console.WriteLine($"Channel: {ChannelName}");
        Console.WriteLine("Videos:");
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}

class Program
{
    static void Main()
    {
        // Create a YouTube Channel
        YouTubeChannel channel = new YouTubeChannel("Tech Tutorials");
        
        // Create and add videos
        Video video1 = new Video("C# Basics", "John Doe", 300);
        video1.AddComment("Alice", "Great explanation!");
        video1.AddComment("Bob", "Very helpful, thanks!");
        video1.AddComment("Charlie", "Nice tutorial!");
        channel.AddVideo(video1);
        
        Video video2 = new Video("OOP in C#", "Jane Smith", 450);
        video2.AddComment("Dave", "This was exactly what I needed!");
        video2.AddComment("Eva", "Clear and concise.");
        video2.AddComment("Frank", "Awesome content!");
        channel.AddVideo(video2);
        
        Video video3 = new Video("LINQ in C#", "Michael Brown", 600);
        video3.AddComment("Grace", "LINQ makes life easier!");
        video3.AddComment("Hank", "Thanks for the detailed examples.");
        video3.AddComment("Ivy", "This helped me a lot!");
        channel.AddVideo(video3);
        
        Video video4 = new Video("Design Patterns in C#", "Sarah Johnson", 500);
        video4.AddComment("Jack", "Well explained, thanks!");
        video4.AddComment("Kara", "Great examples and explanations.");
        video4.AddComment("Liam", "This really helped me understand design patterns.");
        channel.AddVideo(video4);
        
        // Display channel info
        channel.DisplayChannelInfo();
    }
}
