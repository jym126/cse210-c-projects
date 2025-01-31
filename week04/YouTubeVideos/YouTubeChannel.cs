using System;
using System.Collections.Generic;

class YouTubeChannel
{
    public string ChannelName { get; set; }
    private List<Video> _videos; // Naming convention updated
    
    public YouTubeChannel(string channelName)
    {
        ChannelName = channelName;
        _videos = new List<Video>();
    }
    
    public void AddVideo(Video video)
    {
        _videos.Add(video);
    }
    
    public void DisplayChannelInfo()
    {
        Console.WriteLine($"Channel: {ChannelName}");
        Console.WriteLine("Videos:");
        foreach (var video in _videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
