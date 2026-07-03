using BYSResults;
using HogWildSystemV2.DAL;
using HogWildSystemV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HogWildSystemV2.BLL
{
    public class CustomerService
    {
		private readonly OLTPDMIT2018Context _oltpDMIT2018Context;

		internal CustomerService(OLTPDMIT2018Context oltpDMIT2018Context)
		{
			_oltpDMIT2018Context = oltpDMIT2018Context;
		}

		public Result<List<CustomerView>> GetCustomerByLastNameOrPhone(string lastName, string phone)
		{
			// Create a Result container that will hold either a
			//	CustomerView objects on success or any accumulated errors on failure
			var result = new Result<List<CustomerView>>();

			#region Business Rules			
			if (string.IsNullOrWhiteSpace(lastName) && string.IsNullOrWhiteSpace(phone))
			{
				result.AddError(new Error("Missing Information",
					"Please provide a valid customer last name and/or phone number"));
				//  need to exit because we have no customer record
				return result;
			}
			#endregion

			var customers = _oltpDMIT2018Context.Customers
								.Where(c => ((!string.IsNullOrWhiteSpace(lastName) && string.IsNullOrWhiteSpace(phone) && c.LastName.Contains(lastName)) ||
											 (!string.IsNullOrWhiteSpace(phone) && string.IsNullOrWhiteSpace(lastName) && c.Phone.Contains(phone)) ||
											 (!string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(phone) && c.LastName.Contains(lastName) && c.Phone.Contains(phone))
											 && c.RemoveFromViewFlag == false))
								.Select(c => new CustomerView
								{
									CustomerID = c.CustomerID,
									FirstName = c.FirstName,
									LastName = c.LastName,
									Address1 = c.Address1,
									Address2 = c.Address2,
									City = c.City,
									ProvStateID = c.ProvStateID,
									CountryID = c.CountryID,
									PostalCode = c.PostalCode,
									Phone = c.Phone,
									Email = c.Email,
									StatusID = c.StatusID,
									RemoveFromViewFlag = c.RemoveFromViewFlag
								}).ToList();

			if (customers.Count < 1)
			{
				result.AddError(new Error("No Customer", "No customer were found"));
				//  need to exit because we did not find any customer
				return result;
			}

			//  return the result
			return result.WithValue(customers);
		}
	}
}
