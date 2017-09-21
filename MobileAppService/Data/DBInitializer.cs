using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SafeHaven.MobileAppService.Models;
using System.Threading.Tasks;
using SafeHaven.MobileAppService.Data;

namespace SafeHaven.MobileAppService.Data
{
	// Class to seed our database with data for testing purposes.
	public static class DbInitializer
	{
		// Method runs on startup to initialize dummy data.
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new SafeHavenContext(serviceProvider.GetRequiredService<DbContextOptions<SafeHavenContext>>()))
			{
				// Look for any Customers.
				if (context.User.Any())
				{
					return;   // DB has been seeded, the rest of this method doesn't need to run.
				}
				// Creating new instances of User
				var users = new User[]
				{
					new User {
						FirstName = "Jill",
						LastName = "Smith",
						Email = "a@a.com",
                        Password = "aaaaaa",
						Street = "111 Bob St.",
						ZIP = 38101,
						State = "TN"
					},
					new User {
						FirstName = "Nigel",
						LastName = "Thornberry",
						Email = "b@b.com",
                        Password = "bbbbbb",
						Street = "222 Saint St.",
						ZIP = 39881,
						State = "CA"
					},
					new User {
						FirstName = "Mark",
						LastName = "Jones",
						Email = "c@c.com",
                        Password = "cccccc",
						Street = "333 Thomas St.",
						ZIP = 12938,
						State = "NY"
					}
				};
				// Adds each new user into the context
				foreach (User i in users)
				{
					context.User.Add(i);
				}
				// Saves to DB
				context.SaveChanges();

				var documentTypes = new DocumentType[]
				{
					new DocumentType {
						Title = "Insurance"
					},
					new DocumentType {
						Title = "Estate/Will"
					},
					new DocumentType {
						Title = "Medical"
					},
					new DocumentType {
						Title = "Taxes"
					},
					new DocumentType {
						Title = "Birth/Death"
					},
					new DocumentType {
						Title = "Marriage/Divorce"
					},
					new DocumentType {
						Title = "Military"
					},
					new DocumentType {
						Title = "Pension"
					},
					new DocumentType {
						Title = "Real Estate/Mortgage"
					},
					new DocumentType {
						Title = "Deed/Title"
					},
					new DocumentType {
						Title = "Education"
					},
					new DocumentType {
						Title = "Checking/Savings Accounts"
					},
					new DocumentType {
						Title = "Investment Accounts"
					},
					new DocumentType {
						Title = "Retirement Accounts"
					},
					new DocumentType {
						Title = "Credit/Loans"
					},
					new DocumentType {
						Title = "Identification"
					},
					new DocumentType {
						Title = "Other"
					}
				};
				foreach (DocumentType p in documentTypes)
				{
					context.DocumentType.Add(p);
				}
				context.SaveChanges();

				var documents = new Document[]
				{
					new Document{
						Title = "Jill's Passport",
						DateCreated = new DateTime(2017, 04, 28),
						DocumentTypeID = context.DocumentType.Single(x => x.Title == "Identification").DocumentTypeID,
						PhysicalLocation = "Basement Safe",
						Notes = "Expires 2019",
						UserID = context.User.Single(x => x.FirstName == "Jill").UserID

					},
					new Document{
						Title = "Health Insurance 2017",
						DateCreated = new DateTime(2017, 03, 28),
						DocumentTypeID = context.DocumentType.Single(x => x.Title == "Insurance").DocumentTypeID,
						PhysicalLocation = "Bedroom Closet",
						Notes = "Cigna",
						UserID = context.User.Single(x => x.FirstName == "Jill").UserID
					},
					new Document{
						Title = "Estate Plan",
						DateCreated = new DateTime(2017, 06, 28),
						DocumentTypeID = context.DocumentType.Single(x => x.Title == "Estate/Will").DocumentTypeID,
						PhysicalLocation = "Bank of America Safe Deposit Box",
						Notes = "Code is 201938",
						UserID = context.User.Single(x => x.FirstName == "Jill").UserID
					},
				};
				foreach (Document t in documents)
				{
					context.Document.Add(t);
				}
				context.SaveChanges();

				var images = new DocumentImage[]
				{
					new DocumentImage{
						DateCreated = new DateTime(2017, 06, 28),
						DocumentID = context.Document.Single(x => x.Title == "Jill's Passport").DocumentID,
						FilePath = "1.jpg",
						PageNumber = 1
					},
					new DocumentImage{
						DateCreated = new DateTime(2017, 05, 28),
						DocumentID = context.Document.Single(x => x.Title == "Health Insurance 2017").DocumentID,
						FilePath = "1.jpg",
						PageNumber = 1
					},
					new DocumentImage{
						DateCreated = new DateTime(2017, 04, 28),
						DocumentID = context.Document.Single(x => x.Title == "Estate Plan").DocumentID,
						FilePath = "1.jpg",
						PageNumber = 1
					}
				};
				foreach (DocumentImage p in images)
				{
					context.DocumentImage.Add(p);
				}
				context.SaveChanges();

                var accessrights = new AccessRight[]
                {
                    new AccessRight {
                        GrantorID = context.User.Single(x => x.FirstName == "Jill").UserID,
                        AccessorID = context.User.Single(x => x.FirstName == "Mark").UserID
                    },
					new AccessRight {
						GrantorID = context.User.Single(x => x.FirstName == "Jill").UserID,
						AccessorID = context.User.Single(x => x.FirstName == "Nigel").UserID
					},
					new AccessRight {
						GrantorID = context.User.Single(x => x.FirstName == "Nigel").UserID,
						AccessorID = context.User.Single(x => x.FirstName == "Mark").UserID
					},
					new AccessRight {
						GrantorID = context.User.Single(x => x.FirstName == "Nigel").UserID,
						AccessorID = context.User.Single(x => x.FirstName == "Jill").UserID
					},
					new AccessRight {
						GrantorID = context.User.Single(x => x.FirstName == "Mark").UserID,
						AccessorID = context.User.Single(x => x.FirstName == "Nigel").UserID
					},
					new AccessRight {
						GrantorID = context.User.Single(x => x.FirstName == "Mark").UserID,
						AccessorID = context.User.Single(x => x.FirstName == "Jill").UserID
					}
                };
			}
		}
	}
}