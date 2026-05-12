<Query Kind="Program">
  <Connection>
    <ID>2837cd29-a98c-4fc4-b5d3-5efbababb91f</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(local)</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <DisplayName>Contoso</DisplayName>
    <EncryptTraffic>true</EncryptTraffic>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Contoso</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	string phone = "7";
	string lName = "a";
	
	GetEmployeesByLastNameOrPhone(phone, lName).Dump();
}

// You can define other methods, fields, classes and namespaces here
public List<Employee> GetEmployeesByLastNameOrPhone(string phone, string lName, string fName = "")
{
	//List<Employee> employees = new List<Employee>();
	List<Employee> employees = new();
	
	if (!string.IsNullOrWhiteSpace(phone) && string.IsNullOrWhiteSpace(lName))
	{
		employees = Employees
				.Where(x => x.Phone.Contains(phone))
				.Select(x => x)
				.ToList();	
	}
	else if (string.IsNullOrWhiteSpace(phone) && !string.IsNullOrWhiteSpace(lName))
	{
		employees = Employees
				.Where(x => x.LastName.Contains(lName))
				.Select(x => x)
				.ToList();
	}
	else
	{
		employees = Employees
				.Where(x => x.Phone.Contains(phone) && x.LastName.Contains(lName))
				.Select(x => x)
				.ToList();
	}

	return employees;	
}
