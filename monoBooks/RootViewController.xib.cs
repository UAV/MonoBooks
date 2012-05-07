
using MonoTouch.UIKit;
using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.IO;
using System.Drawing;

namespace monoBooks
{
	partial class RootViewController : UITableViewController
	{
		private Book[] books;
		
		public RootViewController (IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad ()
		{
			this.Title = "Books";
			var bookFile = NSBundle.MainBundle.PathForResource("books", "xml");
			
			XDocument xmlDoc = XDocument.Load(bookFile);
			
		    var bookList = new List<Book>();
			
			var theBooks = 	from theBook in xmlDoc.Descendants("book")
				            select new
				            {
								title = theBook.Element("title").Value,
				              	author = theBook.Element("author").Value,
				                image = theBook.Element("image").Value,
								price = decimal.Parse(theBook.Element("price").Value),
								description = theBook.Element("description").Value
				            };
		
			foreach (var item in theBooks) 
			{
				Book book = new Book();			
				book.Title = item.title;
				book.Author = item.author;
				book.Image = item.image;
				book.Desc = item.description;
				book.Price = item.price;
				bookList.Add(book);
			}
			    
 			books = bookList.ToArray();

			base.ViewDidLoad ();
			//Show an edit button
			//NavigationItem.RightBarButtonItem = EditButtonItem;
			
			this.TableView.Source = new DataSource (this);
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidUnload ()
		{
			// Release anything that can be recreated in viewDidLoad or on demand.
			// e.g. this.myOutlet = null;
			
			base.ViewDidUnload ();
		}

		class DataSource : UITableViewSource
		{
			RootViewController controller;

			public DataSource (RootViewController controller)
			{
				this.controller = controller;
			}

			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			// Customize the number of rows in the table view
			public override int RowsInSection (UITableView tableview, int section)
			{
				return controller.books.Length;
			}
			
			
			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, 
			                                         MonoTouch.Foundation.NSIndexPath indexPath)
			{
				string cellIdentifier = "Cell";
				var cell = tableView.DequeueReusableCell (cellIdentifier);
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, cellIdentifier);
				}
				
				// Configure the cell.
				Book book = controller.books[indexPath.Row];
				cell.TextLabel.Text = book.Author;
				cell.DetailTextLabel.Text = book.Title;			
				cell.ImageView.Image = UIImage.FromFile(book.Image);						

				return cell;
			}


			// Override to support row selection in the table view.
			public override void RowSelected (UITableView tableView,
			                                  MonoTouch.Foundation.NSIndexPath indexPath)
			{
				DetailsViewController detailsViewController = new DetailsViewController();
				detailsViewController.Book =  controller.books[indexPath.Row];
				controller.NavigationController.PushViewController(detailsViewController, true);				
			}
		}
	}
	
}
