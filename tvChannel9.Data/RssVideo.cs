using System;
namespace tvChannel9.Data
{

    public class RssVideo
    {
        public RssVideo(RssVideo item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            VideoUrl = item.VideoUrl;
            ThumbnailUrl = item.ThumbnailUrl;
        }

        public RssVideo()
        {
            
        }

		public long Id { get; set; }
		public string Name { get; set; }

		public string Description { get; set; }

		public string VideoUrl { get; set; }

		public string ThumbnailUrl { get; set; }
    }
}
