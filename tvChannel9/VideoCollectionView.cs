using System;
using Foundation;
using tvChannel9.Classes;
using UIKit;

namespace tvChannel9
{
	public partial class VideoCollectionView : UICollectionView
	{
		#region Application Access
		public static AppDelegate App {
			get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
		}
		#endregion

		#region Computed Properties
		public VideoDataSource Source {
			get { return DataSource as VideoDataSource;}
		}

		public VideoCollectionViewController ParentController { get; set;}
		#endregion

		#region Constructors
		public VideoCollectionView (IntPtr handle) : base (handle)
		{
			// Initialize
			RegisterClassForCell (typeof(VideoCollectionViewCell), VideoDataSource.CardCellId);
			DataSource = new VideoDataSource (this);
			Delegate = new VideoViewDelegate ();
		}
		#endregion

		#region Override Methods
		public override nint NumberOfSections ()
		{
			return 1;
		}

		public override void DidUpdateFocus (UIFocusUpdateContext context, UIFocusAnimationCoordinator coordinator)
		{
			var previousItem = context.PreviouslyFocusedView as VideoCollectionViewCell;
			if (previousItem != null) {
				Animate (0.2, () => {
					previousItem.VideoTitle.Alpha = 0.0f;
				});
			}

			var nextItem = context.NextFocusedView as VideoCollectionViewCell;
			if (nextItem != null) {
				Animate (0.2, () => {
					nextItem.VideoTitle.Alpha = 1.0f;
				});
			}
		}

		#endregion

        public void StartPlayback(VideoItem item)
        {
            ParentController.PlayStream(item);
        }
	}
}
