using System;
using System.IO;
using UnityEngine.Video;

namespace LilaMachinimaMod
{
    internal class VideoManager
    {
        private const string videoConfigPath = "mods/video.txt"; // TODO: Change hardcoded path

        readonly VideoPlayer _videoPlayer;

        public VideoManager(VideoPlayer videoPlayer)
        {
            _videoPlayer = videoPlayer;
        }
        
        public void PlayVideo()
        {
            _videoPlayer.url = GetVideoUrlFromFile(videoConfigPath);
            _videoPlayer.enabled = true;
            VideoPlayerManager.PlayVideo(""); // We don't need to provide the video name, the video player will play the video from the url
        }

        private string GetVideoUrlFromFile(string videoConfigPath)
        {
           var path = Path.Combine(Directory.GetCurrentDirectory(), videoConfigPath);
           return File.ReadAllText(path);
        }
    }
}
