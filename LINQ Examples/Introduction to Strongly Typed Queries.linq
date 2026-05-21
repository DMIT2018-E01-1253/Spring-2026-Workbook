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

Tracks
	.Where(x => x.Name.Contains("Dance"))
	.Select(x => new Song
	{
		AlbumTitle = x.Album.Title,
		SongTitle = x.Name,
		Artist = x.Album.Artist.Name
	})
	.ToList()
	.Dump();
	
public class Song
{
	public string AlbumTitle { get; set; }
	public string SongTitle { get; set; }
	public string Artist { get; set; }
}