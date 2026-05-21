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

Albums
	.Select(x => new
	{
		RowCount = x.Tracks.Count(),
		Sum1 = x.Tracks.Sum(x => x.Milliseconds) / 1000,
		Sum2 = x.Tracks.Select(x => x.Milliseconds).Sum() / 1000,
		SumFormula1 = x.Tracks.Sum(x => x.Milliseconds * x.UnitPrice) / 1000,
		SumFormula2 = x.Tracks
					.Select(x => x.Milliseconds * x.UnitPrice)
					.Sum() / 1000,
		MinVal1 = x.Tracks.Min(x => x.Milliseconds) / 1000,
		MinVal2 = x.Tracks.Select(x => x.Milliseconds).Min() / 1000,
		MaxVal1 = x.Tracks.Max(x => x.Milliseconds) / 1000,
		MaxVal2 = x.Tracks.Select(x => x.Milliseconds).Max() / 1000,
		AvgVal1 = x.Tracks.Average(x => x.Milliseconds) / 1000,
		AvgVal2 = x.Tracks.Select(x => x.Milliseconds).Average() / 1000,
	})
	.Dump();