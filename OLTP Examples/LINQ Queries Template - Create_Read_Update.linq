<Query Kind="Program">
  <Connection>
    <ID>48f41bb2-0f4a-46ba-b726-a5f2c4ec01ed</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>(local)</Server>
    <Database>Chinook-2025</Database>
    <DisplayName>Chinook-2025</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
  <NuGetReference>BYSResults</NuGetReference>
</Query>

// 	Lightweight result types for explicit success/failure 
//	 handling in .NET applications.
using BYSResults;

// —————— PART 1: Main → UI ——————
//	Driver is responsible for orchestrating the flow by calling 
//	various methods and classes that contain the actual business logic 
//	or data processing operations.
void Main()
{
	CodeBehind codeBehind = new CodeBehind(this); // “this” is LINQPad’s auto Context

	//#region GetAlbum
	//	//	Fail
	//	//	Rule:  album ID must be greater than zero
	//	codeBehind.GetAlbum(0);
	//	codeBehind.ErrorDetails.Dump("Album ID must be greater than zero");
	
	//	// Rule:  album ID must valid 
	//	codeBehind.GetAlbum(1000000);
	//	codeBehind.ErrorDetails.Dump("Album was not found for ID 1000000");
	
	//	// Pass:  valid album ID
	//	codeBehind.GetAlbum(1);
	//	codeBehind.Album.Dump("Pass - Valid album ID");
	//#endregion
	
	#region AddEditAlbum
		//	Fail
		//	Rule:  album cannot be null	
		//codeBehind.AddEditAlbum(null);
		//codeBehind.ErrorDetails.Dump("Album is null");
	
		// need to create an album object
	AlbumView album = new();
	
	//	//	Rule:  missing title, artists id, and release year are required (not empty)
	//	codeBehind.AddEditAlbum(album);
	//	codeBehind.ErrorDetails.Dump("Missing required Fields");

	//// Rule: Title and artists id cannot be duplicated (found more than once in the database).

	//// get an existing customer from the database
	//codeBehind.GetAlbum(1);
	//album = codeBehind.Album;

	//// reset the album Id to zero so that it is consider a new album.
	//album.AlbumId = 0;
	//codeBehind.AddEditAlbum(album);
	//codeBehind.ErrorDetails.Dump("Duplicated album");

	//#region new album
	//// Pass:  valid new customer
	////  minimum data require to create a new album.
	//album = new()
	//{
	//	Title = "Test Title2",
	//	ArtistId = 1,
	//	ReleaseYear = 2026
	//};

	////  get the last two album records to use as a 
	////		comparison after we added the new record
	//Albums.OrderByDescending(a => a.AlbumId).Take(2).Dump();

	////  add the new album to the database
	//codeBehind.AddEditAlbum(album);
	//codeBehind.Album.Dump("New album");

	////  get the last two customer records to see if the customer 
	////	  has been added
	//Albums.OrderByDescending(a => a.AlbumId).Take(2).Dump();
	//#endregion
	
	#region edit album
	// get previous album
	codeBehind.GetAlbum(349);
	album = codeBehind.Album;
	album.Title = "Modified Title";

	//  get the last two customer records to use as a 
	//		comparison after we edit the previous record
	Albums.OrderByDescending(a => a.AlbumId).Take(2).Dump("Before Editing");

	//  update the database with the edited album
	codeBehind.AddEditAlbum(album);
	codeBehind.Album.Dump("Edit album");

	//  get the last two album records to see if the album 
	//	  has been edited
	Albums.OrderByDescending(a => a.AlbumId).Take(2).Dump("After Editing");
	#endregion
	
	#endregion	
}

// ———— PART 2: Code Behind → Code Behind Method ————
// This region contains methods used to test the functionality
// of the application's business logic and ensure correctness.
// NOTE: This class functions as the code-behind for your Blazor pages
#region Code Behind Methods
public class CodeBehind(TypedDataContext context)
{
	#region Supporting Members (Do not modify)
	// exposes the collected error details
	public List<string> ErrorDetails => errorDetails;

	// Mock injection of the service into our code-behind.
	// You will need to refactor this for proper dependency injection.
	// NOTE: The TypedDataContext must be passed in.
	private readonly Library YourService = new Library(context);
	#endregion

	#region Fields from Blazor Page Code-Behind
	// feedback message to display to the user.
	private string feedbackMessage = string.Empty;
	// collected error details.
	private List<string> errorDetails = new();
	// general error message.
	private string errorMessage = string.Empty;
	#endregion

	//  album view returned by the service
	//	using the GetAlbum()
	public AlbumView Album = default!;
	
	public void GetAlbum(int albumId)
	{
		// clear previous error details and messages
		errorDetails.Clear();
		errorMessage = string.Empty;
		feedbackMessage = String.Empty;

		// wrap the service call in a try/catch to handle unexpected exceptions
		try
		{
			var result = YourService.GetAlbum(albumId);
			if (result.IsSuccess)
			{
				Album = result.Value;
			}
			else
			{
				errorDetails = GetErrorMessages(result.Errors.ToList());
			}
		}
		catch (Exception ex)
		{
			// capture any exception message for display
			errorMessage = ex.Message;
		}
	}
	
	public void AddEditAlbum(AlbumView album)
	{
		// clear previous error details and messages
		errorDetails.Clear();
		errorMessage = string.Empty;
		feedbackMessage = String.Empty;

		// wrap the service call in a try/catch to handle unexpected exceptions
		try
		{
			var result = YourService.AddEditAlbum(album);
			if (result.IsSuccess)
			{
				Album = result.Value;
			}
			else
			{
				errorDetails = GetErrorMessages(result.Errors.ToList());
			}
		}
		catch (Exception ex)
		{
			// capture any exception message for display
			errorMessage = ex.Message;
		}
	}
}
#endregion

// ———— PART 3: Database Interaction Method → Service Library Method ————
//	This region contains support methods for testing
#region Methods
public class Library
{
	#region Data Context Setup
	// The LINQPad auto-generated TypedDataContext instance used to query and manipulate data.
	private readonly TypedDataContext _chinook2025Context;

	// The TypedDataContext provided by LINQPad for database access.
	// Store the injected context for use in library methods
	// NOTE:  This constructor is simular to the constuctor in your service
	public Library(TypedDataContext context)
	{
		_chinook2025Context = context
					?? throw new ArgumentNullException(nameof(context));
	}
	#endregion

	public Result<AlbumView> GetAlbum(int albumId)
	{
		// Create a Result container that will hold either a
		//	AlbumView objects on success or any accumulated errors on failure
		var result = new Result<AlbumView>();

		#region Business Rules
		//	These are processing rules that need to be satisfied for valid data		
		//		rule:	albumId must be valid (greater than zero) 
		// 		rule:	RemoveFromViewFlag must be false (soft delete)

		if (albumId <= 0)
		{
			result.AddError(new Error("Missing Information",
				"Please provide a valid Album ID"));
			//  need to exit because we have no album record
			return result;
		}
		#endregion

		var album = _chinook2025Context.Albums
							.Where(a => (a.AlbumId == albumId))
							.Select(a => new AlbumView
							{
								AlbumId = a.AlbumId,
								Title = a.Title,
								ArtistId = a.ArtistId,
								ReleaseYear = a.ReleaseYear,
								ReleaseLabel = a.ReleaseLabel
							}).FirstOrDefault();

		//  if no album were found with the album ID
		if (album == null)
		{
			result.AddError(new Error("No Album", "No album were found"));
			//  need to exit because we did not find any album
			return result;
		}

		//  return the result
		return result.WithValue(album);
	}
	
	public Result<AlbumView> AddEditAlbum(AlbumView editAlbum)
	{
		// Create a Result container that will hold either an
		// AlbumView objects on success or any accumulated errors on failure
		var result = new Result<AlbumView>();
		
		#region Business Rules
		//	These are processing rules that need to be satisfied for valid data	
		//    rule:    album cannot be null
		if (editAlbum == null)
		{
			result.AddError(new Error("Missing Album",
				"No album was supply"));
			//  need to exit because we have no album view model to add/edit
			return result;
		}
		////	rule: title, artist id, and release year are required (not empty)
		if (string.IsNullOrEmpty(editAlbum.Title))
		{
			result.AddError(new Error("Missing Information", "Album title is required"));
		}

		if (editAlbum.ArtistId <= 0)
		{
			result.AddError(new Error("Missing Information", "Artist id is required"));
		}

		if (editAlbum.ReleaseYear < 1000)
		{
			result.AddError(new Error("Missing Information", "Release year is required"));
		}

		//		rule: 	title and artist id cannot be duplicated (found more than once)
		if (editAlbum.AlbumId == 0)
		{
			bool albumExist = _chinook2025Context.Albums.Any(x =>
										  x.Title.ToUpper() == editAlbum.Title.ToUpper() &&
										  x.ArtistId == editAlbum.ArtistId
										);

			if (albumExist)
			{
				result.AddError(new Error("Existing Album Data", "Album already exist in the " +
												"database and cannot be enter again"));
			}
		}

		//  exit if we have any outstanding errors
		if (result.IsFailure)
		{
			return result;
		}
		#endregion


		Album album = _chinook2025Context.Albums
								.Where(x => x.AlbumId == editAlbum.AlbumId)
								.Select(x => x).FirstOrDefault();

		//  if the album was not found (AlbumId == 0)
		//		then we are dealing with a new album
		if (album == null)
		{
			album = new();
		}

		//	NOTE:	You do not have to update the primary key "AlbumID".
		//				This is try for all primary keys for any view models.
		//			- If is is a new album, the AlbumId will be "0"
		//			- If is is an existing album, there is no need to update it.

		album.Title = editAlbum.Title;
		album.ArtistId = editAlbum.ArtistId;
		album.ReleaseYear = editAlbum.ReleaseYear;
		album.ReleaseLabel = editAlbum.ReleaseLabel;

		//  new album
		if (album.AlbumId == 0)
			_chinook2025Context.Albums.Add(album);
		else
			//	existing album
			_chinook2025Context.Albums.Update(album);

		try
		{
			// NOTE:  YOU CAN ONLY HAVE ONE SAVE CHANGES IN A METHOD  
			_chinook2025Context.SaveChanges();
		}
		catch (Exception ex)
		{
			// Clear changes to maintain data integrity.
			_chinook2025Context.ChangeTracker.Clear();
			// we do not have to throw an exception, just need to log the error message
			result.AddError(new Error(
				"Error Saving Changes", ex.InnerException.Message));
			//  need to return the result
			return result;
		}
		//  need to refresh the album information
		return GetAlbum(album.AlbumId);
	}
}
#endregion

// ———— PART 4: View Models → Service Library View Model ————
//	This region includes the view models used to 
//	represent and structure data for the UI.
#region View Models
public class AlbumView
{
	public int AlbumId { get; set; }
	public string Title { get; set; }
	public int ArtistId { get; set; }
	public int ReleaseYear { get; set; }
	public string ReleaseLabel { get; set; }
}
#endregion

//	This region includes support methods
#region Support Method
// Converts a list of error objects into their string representations.
public static List<string> GetErrorMessages(List<Error> errorMessage)
{
	// Initialize a new list to hold the extracted error messages
	List<string> errorList = new();

	// Iterate over each Error object in the incoming list
	foreach (var error in errorMessage)
	{
		// Convert the current Error to its string form and add it to errorList
		errorList.Add(error.ToString());
	}

	// Return the populated list of error message strings
	return errorList;
}
#endregion