using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Aranslow.Objects
{
    internal class Sprite
    {
		BitmapImage _spriteSheet;

		int TotalFramesInSheet =>  _spriteSheet.PixelWidth / _frameWidth;

		ImageBrush _currentFrame;
		List<ImageSource> _spriteFrames;
		int _frameWidth;
		int _frameHeight;

		public int FrameWidth => _frameWidth;

		DispatcherTimer _animTimer;
		int _frameDuration = 100;
		int _animIndex = 0;


		/// <summary>
		/// Create new sprite
		/// </summary>
		/// <param name="spriteSheet">BitmapImage of spritesheet</param>
		public Sprite(BitmapImage spriteSheet)
		{
			this._spriteSheet = spriteSheet;
			this._frameWidth = Assets.SpritesheetInfoLoader.GetFrameWidthForSprite(System.IO.Path.GetFileNameWithoutExtension(spriteSheet.UriSource.AbsoluteUri));
			this._frameHeight = spriteSheet.PixelHeight;

			this._spriteFrames = CutSheet();

			this._currentFrame = new ImageBrush(this._spriteFrames[0]);

			this._animTimer = new DispatcherTimer();
			this._animTimer.Tick += (sender, e) => nextFrame();
		}

		public ImageBrush Brush
		{
			get { return this._currentFrame; }
		}

		public System.Windows.Controls.Image Image => new System.Windows.Controls.Image() { Source = Brush.ImageSource, Stretch = Stretch.None };

		public DispatcherTimer Timer
		{
			get { return this._animTimer; }
		}

		public List<ImageSource> Frames
		{
			get { return this._spriteFrames; }
		}

		public int FrameDuration
		{
			get { return this._frameDuration; }
			set { this._frameDuration = value; }
		}

		/// <summary>
		/// Manually set the frame index. This will not stop the running animation nor change the animation index.
		/// </summary>
		/// <param name="index">Frame index</param>
		public void SetFrame(int index)
		{
			this._currentFrame.ImageSource = this._spriteFrames[index];
		}

		/// <summary>
		/// Start sprite animation.
		/// </summary>
		public void StartAnimation()
		{
			this._animTimer.Interval = TimeSpan.FromMilliseconds(this._frameDuration);
			this._animTimer.Start();
		}

		/// <summary>
		/// Pause running sprite animation
		/// </summary>
		public void PauseAnimation()
		{
			if (this._animTimer != null)
			{
				this._animTimer.Stop();
			}
		}

		/// <summary>
		/// Increase animation index by one or reset to zero if at end of list
		/// </summary>
		private void nextFrame()
		{
			this._animIndex++;
			this._animIndex = this._animIndex % this._spriteFrames.Count;
			this._currentFrame.ImageSource = this._spriteFrames[this._animIndex];
		}

		/// <summary>
		/// Cut spritesheet into bitmap animation frames
		/// </summary>
		/// <returns>Returns a list of animation frames</returns>
		private List<ImageSource> CutSheet()
		{
			List<ImageSource> frames = new List<ImageSource>();

			Int32Rect cropRect = new Int32Rect(0, 0, this._frameWidth, this._frameHeight);
			BitmapImage sheet = this._spriteSheet;
			ImageSource frame;

			if ((sheet.PixelHeight % this._frameHeight) == 0 && (sheet.PixelWidth % this._frameWidth) == 0)
			{
				int rows = (int)sheet.PixelHeight / (int)this._frameHeight;
				int columns = (int)sheet.PixelWidth / (int)this._frameWidth;
				int frameCount = 0;
				for (int row = 0; row < rows; row++)
				{
					if (frameCount < this.TotalFramesInSheet)
					{
						for (int col = 0; col < columns; col++)
						{
							int currentY = row * this._frameHeight;
							int currentX = col * this._frameWidth;
							cropRect.X = currentX;
							cropRect.Y = currentY;

							frame = new CroppedBitmap(sheet, cropRect);
							frames.Add(frame);

							frameCount++;

							if (frameCount == this.TotalFramesInSheet) { break; }
						}
					}
					else { break; }
				}
			}
			return frames;
		}
	}
}
