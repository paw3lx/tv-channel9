using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace tvChannel9.Classes
{
	public class VideoDataSource : UICollectionViewDataSource
	{
		#region Application Access
		public static AppDelegate App
		{
			get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
		}
		#endregion

		#region Static Constants
		public static NSString CardCellId = new NSString("VideoItem");
		#endregion

		#region Computed Properties
        public List<VideoItem> Videos { get; set; } = new List<VideoItem>();
        public VideoCollectionView ViewController { get; set; }
		#endregion

		#region Constructors
		public VideoDataSource(VideoCollectionView controller)
		{
			// Initialize
			this.ViewController = controller;
			PopulateVideos();
		}
		#endregion

		#region Public Methods
        public async void PopulateVideos()
		{

			// Clear existing videos
			Videos.Clear();

            // Add new videos
            var task = tvChannel9.Data.RssReader.ParseVideoRss("https://s.ch9.ms/Events/dotnetConf/2017/rss/mp4");
            task.GetAwaiter().OnCompleted(ViewController.ReloadData);
            var videos = await task;
            Videos = videos.Select(s => new VideoItem(s)).ToList();
		}

        //public void PopulateVideos()
        //{
        //    Videos.Clear();

        //    Videos = new List<VideoItem>() 
        //    {
        //        new VideoItem{
        //            Id = 0,
        //            Name = "Keynote",
        //            //ThumbnailUrl = "https://sec.ch9.ms/ch9/e410/7a54effc-8b6f-4f8d-b4e4-d0928498e410/AzFrCosmosDBMetricsAndHotPartitionsGavrylyuk_512.jpg"
        //        }
        //    };
        //}
		#endregion

		#region Override Methods
		public override nint NumberOfSections(UICollectionView collectionView)
		{
			return 1;
		}

		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
            return Videos.Count;
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
            var videoCell = (VideoCollectionViewCell)collectionView.DequeueReusableCell(CardCellId, indexPath);
            var video = Videos[indexPath.Row];

			// Initialize video
            videoCell.Video = video;

			return videoCell;
		}

		#endregion
	}
}
