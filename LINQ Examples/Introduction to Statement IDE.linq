<Query Kind="Statements">
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
</Query>

int paramYear = 2000;

var selectM = Albums
				.Where(x => x.ReleaseYear == paramYear)
				.Select(x => x); //.Dump();

selectM.Dump(); //Console.WriteLine(selectM)