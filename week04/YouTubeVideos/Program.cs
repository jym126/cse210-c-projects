using System;

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
