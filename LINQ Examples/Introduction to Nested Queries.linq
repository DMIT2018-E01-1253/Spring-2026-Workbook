<Query Kind="Statements">
  <Connection>
    <ID>6b821400-0acf-4186-ba5f-2174b2f7f2fe</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(local)</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <EncryptTraffic>true</EncryptTraffic>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook-2025</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

//Artists
//	.OrderBy(x => x.Name)
//	.Select(x => new
//	{
//		Artist = x.Name,
//		Albums = Albums
//					.Where(a => a.ArtistId == x.ArtistId)
//					.OrderBy(a => a.Title)
//					.Select(a => new
//					{
//						Album = a.Title,
//						Label = a.ReleaseLabel,
//						Year = a.ReleaseYear
//					})
//					.ToList()
//	})
//	.ToList()
//	.Dump();
	
Artists
	.OrderBy(x => x.Name)
	.Select(x => new
	{
		Artist = x.Name,
		Albums = x.Albums
					.OrderBy(a => a.Title)
					.Select(a => new
					{
						Album = a.Title,
						Label = a.ReleaseLabel,
						Year = a.ReleaseYear
					})
					.ToList()
	})
	.ToList()
	.Dump();
	
Artists
	.OrderBy(x => x.Name)
	.Select(x => new ArtistView
	{
		Name = x.Name,
		Albums = x.Albums
					.OrderBy(a => a.Title)
					.Select(a => new AlbumView
					{
						Album = a.Title,
						Label = a.ReleaseLabel,
						Year = a.ReleaseYear,
						Tracks = a.Tracks
									.Select(t => new TrackView
									{
										TrackID = t.TrackId,
										Name = t.Name,
										Length = t.Milliseconds / 1000
									})
									.ToList()
					})
					.ToList()
	})
	.ToList()
	.Dump();
	
public class ArtistView
{
	public string Name { get; set; }
	public List<AlbumView> Albums { get; set; }
}
	
public class AlbumView
{
	public string Album { get; set; }
	public string Label { get; set; }
	public int Year { get; set; }
	public List<TrackView> Tracks { get; set; }
}
	
public class TrackView
{
	public int TrackID { get; set; }
	public string Name { get; set; }
	public int Length { get; set; }
}
