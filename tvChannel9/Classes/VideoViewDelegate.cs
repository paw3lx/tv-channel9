using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace tvChannel9.Classes
{
	public class VideoViewDelegate : UICollectionViewDelegateFlowLayout
	{
		#region Application Access
		public static AppDelegate App
		{
			get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
		}
		#endregion

		#region Constructors
		public VideoViewDelegate()
		{
		}
		#endregion

		#region Override Methods
		public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
		{
			return new CGSize(361, 256);
		}

		public override bool CanFocusItem(UICollectionView collectionView, NSIndexPath indexPath)
		{
            return indexPath != null;
		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
            var controller = collectionView as VideoCollectionView;
            var selectedVideo =  controller.Source.Videos[indexPath.Row];
            controller.StartPlayback(selectedVideo);
		}

		#endregion
	}
}
