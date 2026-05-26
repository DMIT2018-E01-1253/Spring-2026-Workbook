<Query Kind="Statements">
  <Connection>
    <ID>ef7068c3-55c1-4b0e-9cca-69e1be9fd4ab</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(local)</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <DisplayName>WestWind-2024</DisplayName>
    <EncryptTraffic>true</EncryptTraffic>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WestWind-2024</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

Customers
	.GroupBy(x => x.Region)
	.Dump("Groups")
	.Select(g => new
	{
		Region = g.Key == null ? "UnKnown" : g.Key,
		OrderCount = g.Sum(c => c.Orders.Count())
	})
	.Dump("Groups with items");