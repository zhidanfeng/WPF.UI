using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Threading;

namespace ZdfFlatUI
{
    public class GifImage : System.Windows.Controls.Image
    {
        public static readonly DependencyProperty GIFSourceProperty = DependencyProperty.Register(
            "GIFSource", typeof(string), typeof(GifImage), new PropertyMetadata(OnSourcePropertyChanged));

        /// <summary>
        /// GIF图片源，支持相对路径、绝对路径
        /// </summary>
        public string GIFSource
        {
            get { return (string)GetValue(GIFSourceProperty); }
            set { SetValue(GIFSourceProperty, value); }
        }

        internal Bitmap Bitmap; // Local bitmap member to cache image resource
        internal BitmapSource BitmapSource;
        public delegate void FrameUpdatedEventHandler();

        /// <summary>
        /// Delete local bitmap resource
        /// Reference: http://msdn.microsoft.com/en-us/library/dd183539(VS.85).aspx
        /// </summary>
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool DeleteObject(IntPtr hObject);

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Loaded += GifImage_Loaded;
            this.Unloaded += GifImage_Unloaded;
        }

        void GifImage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.StopAnimate();
        }

        void GifImage_Loaded(object sender, RoutedEventArgs e)
        {
            BindSource(this);
        }

        /// <summary>
        /// Start animation
        /// </summary>
        public void StartAnimate()
        {
            ImageAnimator.Animate(Bitmap, OnFrameChanged);
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public void StopAnimate()
        {
            ImageAnimator.StopAnimate(Bitmap, OnFrameChanged);
        }

        /// <summary>
        /// Event handler for the frame changed
        /// </summary>
        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FrameUpdatedEventHandler(FrameUpdatedCallback));
        }

        private void FrameUpdatedCallback()
        {
            ImageAnimator.UpdateFrames();

            if (BitmapSource != null)
                BitmapSource.Freeze();

            // Convert the bitmap to BitmapSource that can be display in WPF Visual Tree
            BitmapSource = GetBitmapSource(this.Bitmap, this.BitmapSource);
            Source = BitmapSource;
            InvalidateVisual();
        }

        /// <summary>
        /// 属性更改处理事件
        /// </summary>
        private static void OnSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            GifImage gif = sender as GifImage;
            if (gif == null) return;
            if (!gif.IsLoaded) return;
            BindSource(gif);
        }

        private static void BindSource(GifImage gif)
        {
            gif.StopAnimate();
            if (gif.Bitmap != null)
            {
                gif.Bitmap.Dispose();
            }
            var path = gif.GIFSource;
            
            gif.Bitmap = new Bitmap(GetStreamFromSource(path));
            gif.BitmapSource = GetBitmapSource(gif.Bitmap, gif.BitmapSource);
            gif.StartAnimate();
        }

        public static Stream GetStreamFromSource(string source)
        {
            Uri uri;
            Stream stream = null;

            try
            {
                uri = new Uri(source, UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                return null;
            }

            if (source.Trim().ToUpper().EndsWith(".GIF"))
            {
                if (!uri.IsAbsoluteUri)
                {
                    stream = GetGifStreamFromPack(uri);
                }
                else
                {
                    string leftPart = uri.GetLeftPart(UriPartial.Scheme);

                    if (leftPart == "http://" || leftPart == "ftp://" || leftPart == "file://")
                    {
                        //GetGifStreamFromHttp(uri);
                    }
                    else if (leftPart == "pack://")
                    {
                        GetGifStreamFromPack(uri);
                    }
                    else
                    {
                        //CreateNonGifAnimationImage();
                    }
                }
            }
            else
            {
                //CreateNonGifAnimationImage();
            }
            return stream;
        }

        private static Stream GetGifStreamFromPack(Uri uri)
        {
            StreamResourceInfo streamInfo = new StreamResourceInfo();
            try
            {
                if (!uri.IsAbsoluteUri)
                {
                    streamInfo = Application.GetContentStream(uri);
                    if (streamInfo == null)
                    {
                        streamInfo = Application.GetResourceStream(uri);
                    }
                }
                else
                {
                    if (uri.GetLeftPart(UriPartial.Authority).Contains("siteoforigin"))
                    {
                        streamInfo = Application.GetRemoteStream(uri);
                    }
                    else
                    {
                        streamInfo = Application.GetContentStream(uri);
                        if (streamInfo == null)
                        {
                            streamInfo = Application.GetResourceStream(uri);
                        }
                    }
                }
                if (streamInfo == null)
                {
                    throw new FileNotFoundException("Resource not found.", uri.ToString());
                }
            }
            catch (Exception ex)
            {
                
            }
            return streamInfo.Stream;
        }

        //private void GetGifStreamFromHttp(Uri uri)
        //{
        //    try
        //    {
        //        WebReadState webReadState = new WebReadState();
        //        webReadState.memoryStream = new MemoryStream();
        //        webReadState.webRequest = WebRequest.Create(uri);
        //        webReadState.webRequest.Timeout = 10000;

        //        webReadState.webRequest.BeginGetResponse(new AsyncCallback(WebResponseCallback), webReadState);
        //    }
        //    catch (SecurityException)
        //    {
        //        CreateNonGifAnimationImage();
        //    }
        //}

        //private void CreateNonGifAnimationImage()
        //{
        //    image = new Image();
        //    image.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(image_ImageFailed);
        //    ImageSource src = (ImageSource)(new ImageSourceConverter().ConvertFromString(Source));
        //    image.Source = src;
        //    image.Stretch = Stretch;
        //    image.StretchDirection = StretchDirection;
        //    this.AddChild(image);
        //}

        private static BitmapSource GetBitmapSource(Bitmap bmap, BitmapSource bimg)
        {
            IntPtr handle = IntPtr.Zero;

            try
            {
                handle = bmap.GetHbitmap();
                bimg = Imaging.CreateBitmapSourceFromHBitmap(
                    handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                if (handle != IntPtr.Zero)
                    DeleteObject(handle);
            }

            return bimg;
        }
    }
}
