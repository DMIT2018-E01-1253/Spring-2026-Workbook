using BYSResults;
using HogWildSystemV2.DAL;
using HogWildSystemV2.Entities;
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

		public Result<CustomerView> GetCustomerByID(int customerID)
		{
			// Create a Result container that will hold either a
			//	CustomerView objects on success or any accumulated errors on failure
			var result = new Result<CustomerView>();

			#region Business Rules
			//	These are processing rules that need to be satisfied for valid data		
			//		rule:	customerID must be valid (greater than zero) 
			// 		rule:	RemoveFromViewFlag must be false (soft delete)

			if (customerID == 0)
			{
				result.AddError(new BYSResults.Error("Missing Information",
					"Please provide a valid customer ID"));
				//  need to exit because we have no customer record
				return result;
			}
			#endregion

			var customer = _oltpDMIT2018Context.Customers
								.Where(c => (c.CustomerID == customerID
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
								}).FirstOrDefault();

			//  if no customer were found with the customer ID
			if (customer == null)
			{
				result.AddError(new BYSResults.Error("No Customer", "No customer were found"));
				//  need to exit because we did not find any customer
				return result;
			}

			//  return the result
			return result.WithValue(customer);
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

		public Result<CustomerView> AddEditCustomer(CustomerView editCustomer)
		{
			// Create a Result container that will hold either a
			//	CustomerView objects on success or any accumulated errors on failure
			var result = new Result<CustomerView>();

			#region Business Rules
			//	These are processing rules that need to be satisfied for valid data	
			//    rule:    customer cannot be null
			if (editCustomer == null)
			{
				result.AddError(new Error("Missing Customer",
					"No customer was supply"));
				//  need to exit because we have no customer view model to add/edit
				return result;
			}
			//	rule: first name, last name, phone number 
			//			and email are required (not empty)
			if (string.IsNullOrEmpty(editCustomer.FirstName))
			{
				result.AddError(new Error("Missing Information", "First name is required"));
			}

			if (string.IsNullOrEmpty(editCustomer.LastName))
			{
				result.AddError(new Error("Missing Information", "Last name is required"));
			}

			if (string.IsNullOrEmpty(editCustomer.Phone))
			{
				result.AddError(new Error("Missing Information", "Phone number is required"));
			}

			if (string.IsNullOrEmpty(editCustomer.Email))
			{
				result.AddError(new Error("Missing Information", "Email is required"));
			}

			//		rule: 	first name, last name and phone number cannot be duplicated (found more than once)
			if (editCustomer.CustomerID == 0)
			{
				bool customerExist = _oltpDMIT2018Context.Customers.Any(x =>
											  x.FirstName.ToUpper() == editCustomer.FirstName.ToUpper() &&
											  x.LastName.ToUpper() == editCustomer.LastName.ToUpper() &&
											  x.Phone.ToUpper() == editCustomer.Phone.ToUpper()
											);

				if (customerExist)
				{
					result.AddError(new Error("Existing Customer Data", "Customer already exist in the " +
																			  "database and cannot be enter again"));
				}
			}

			//  exit if we have any outstanding errors
			if (result.IsFailure)
			{
				return result;
			}
			#endregion


			Customer customer = _oltpDMIT2018Context.Customers
									.Where(x => x.CustomerID == editCustomer.CustomerID)
									.Select(x => x).FirstOrDefault();

			//  if the customer was not found (CustomerID == 0)
			//		the we are dealing with a new customer
			if (customer == null)
			{
				customer = new Customer();
			}

			//	NOTE:	You do not have to update the primary key "CustomerID".
			//				This is try for all privary keys for any view models.
			//			- If is is a new customer, the CustomerID will be "0"
			//			- If is is na existing customer, there is no need to update it.

			customer.FirstName = editCustomer.FirstName;
			customer.LastName = editCustomer.LastName;
			customer.Address1 = editCustomer.Address1;
			customer.Address2 = editCustomer.Address2;
			customer.City = editCustomer.City;
			customer.ProvStateID = editCustomer.ProvStateID;
			customer.CountryID = editCustomer.CountryID;
			customer.PostalCode = editCustomer.PostalCode;
			customer.Email = editCustomer.Email;
			customer.Phone = editCustomer.Phone;
			customer.StatusID = editCustomer.StatusID;
			customer.RemoveFromViewFlag = editCustomer.RemoveFromViewFlag;

			//  new customer
			if (customer.CustomerID == 0)
				_oltpDMIT2018Context.Customers.Add(customer);
			else
				//	existing customer
				_oltpDMIT2018Context.Customers.Update(customer);

			try
			{
				// NOTE:  YOU CAN ONLY HAVE ONE SAVE CHANGES IN A METHOD  
				_oltpDMIT2018Context.SaveChanges();
			}
			catch (Exception ex)
			{
				// Clear changes to maintain data integrity.
				_oltpDMIT2018Context.ChangeTracker.Clear();
				// we do not have to throw an exception, just need to log the error message
				result.AddError(new Error(
					"Error Saving Changes", ex.InnerException.Message));
				//  need to return the result
				return result;
			}
			//  need to refresh the customer information
			return GetCustomerByID(customer.CustomerID);
		}
	}
}
