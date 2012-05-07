
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace monoBooks
{
	public partial class DetailsViewController : UIViewController
	{
		public Book Book {get;set;}
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public DetailsViewController (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public DetailsViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public DetailsViewController () : base("DetailsViewController", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = Book.Title;
			bookTitle.Text = Book.Title;
			author.Text = Book.Author;
			desc.Text = Book.Desc;
			price.Text = Book.Price.ToString();

		}

		
		
	}
}
