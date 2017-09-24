using Foundation;
using System;
using UIKit;
using tvChannel9.Classes;
using System.Threading.Tasks;
using System.Net.Http;
using CoreGraphics;

namespace tvChannel9
{
	public partial class VideoCollectionViewCell : UICollectionViewCell
	{
		#region Private Variables
		private VideoItem _video;
		#endregion

		#region Computed Properties
		public UIImageView VideoView { get; set; }
		public UILabel VideoTitle { get; set; }

		public async Task<UIImage> LoadImage(string imageUrl)
		{
			var httpClient = new HttpClient();

			Task<byte[]> contentsTask = httpClient.GetByteArrayAsync(imageUrl);

			// await! control returns to the caller and the task continues to run on another thread
			var contents = await contentsTask;

			// load from bytes
			return UIImage.LoadFromData(NSData.FromArray(contents));
		}

		public VideoItem Video
		{
			get { return _video; }
			set
			{
				_video = value;
				VideoView.Alpha = 1.0f;
				VideoTitle.Text = Video.Name;		
				#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				SetImage(Video.ThumbnailUrl);
				#pragma warning restore CS4014 // Because this call is not awaiśted, execution of the current method continues before the call is completed

			}
		}

		public async Task<int> SetImage(string url)
		{
			VideoView.Image = await LoadImage(url);
			return 0;
		}
		#endregion

		#region Constructors
		public VideoCollectionViewCell(IntPtr handle) : base(handle)
		{
			// Initialize
			VideoView = new UIImageView(new CGRect(22, 19, 320, 171));
			VideoView.AdjustsImageWhenAncestorFocused = true;

			AddSubview(VideoView);

			VideoTitle = new UILabel(new CGRect(22, 209, 320, 21))
			{
				TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White,
				Alpha = 1.0f,
                Font = UIFont.FromName("Helvetica-Bold", 20f)
			};
			AddSubview(VideoTitle);

		}

		#endregion


	}
}