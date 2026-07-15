using Microsoft.AspNetCore.Components;
using HogWildSystem.BLL;
using HogWildSystem.ViewModels;
using HogWildWeb.Components;
using HogWildSystem.Entities;
using BYSResults;

namespace HogWildWeb.Components.Pages.SamplePages
{
    public partial class CustomerList2
    {
        #region Fields

        // The last name
        private string lastName = string.Empty;

        // The phone number
        private string phoneNumber = string.Empty;

        private int customerId;

        // Tells us if the search has been performed
        private bool noRecords;

        // The feedback message
        private string feedbackMessage = string.Empty;

        // The error message
        private string errorMessage = string.Empty;

        // has feedback
        private bool hasFeedback => !string.IsNullOrWhiteSpace(feedbackMessage);

        // has error
        private bool hasError => !string.IsNullOrWhiteSpace(errorMessage);

        // error details
        private List<string> errorDetails = new();
		#endregion

		#region Properties
		// Injects the CustomerService dependency.
		[Inject]
		protected CustomerService CustomerService { get; set; } = default!;

		[Inject]
		protected HogWildSystemV2.BLL.CustomerService CustomerServiceV2 { get; set; } = default!;

		// Injects the NavigationManager dependency.
		[Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

		// Gets or sets the customers search view.
		protected List<CustomerSearchView> Customers { get; set; } = new();
		protected List<HogWildSystemV2.ViewModels.CustomerView> CustomersV2 { get; set; } = new();
		protected CustomerView Customer2 { get; set; } = new();
		#endregion

		#region Methods
		//  search for an existing customer
		private void Search()
        {
            try
            {
                // reset the no records flag
                noRecords = false;

				// clear previous error details and messages
				errorDetails.Clear();
				errorMessage = string.Empty;
				feedbackMessage = String.Empty;

				// wrap the service call in a try/catch to handle unexpected exceptions
                try
				{
					var result = CustomerService.GetCustomerByID(customerId);
					if (result.IsSuccess)
					{
						Customer2 = result.Value;

                        if (Customer2 != null)
                        {
                            feedbackMessage = $"Search for customer {Customer2.FirstName} {Customer2.LastName} was successful";
                        }
                    }
					else
					{
                        errorMessage = "Missing Information";
                        errorDetails = GetErrorMessages(result.Errors.ToList());
					}
				}
				catch (Exception ex)
				{
					// capture any exception message for display
					errorMessage = ex.Message;
				}

				//if (Customers.Count > 0)
				//{
				//    feedbackMessage = "Search for customer(s) was successful";
				//}
				//else
				//{
				//    feedbackMessage = "No customers were found for your search criteria";
				//    noRecords = true;
				//}
			}
            catch (ArgumentNullException ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (ArgumentException ex)
            {
                errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
            }
            catch (AggregateException ex)
            {
                //  have a collection of errors
                //  each error should be place into a separate line
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage = $"{errorMessage}{Environment.NewLine}";
                }
                errorMessage = $"{errorMessage}Unable to search for customer";
                foreach (var error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }
            }
        }

		private void SearchByLastNameOrPhone()
		{
			try
			{
				// reset the no records flag
				noRecords = false;

				// clear previous error details and messages
				errorDetails.Clear();
				errorMessage = string.Empty;
				feedbackMessage = String.Empty;

				// wrap the service call in a try/catch to handle unexpected exceptions
				try
				{
					var result = CustomerServiceV2.GetCustomerByLastNameOrPhone(lastName, phoneNumber);
					if (result.IsSuccess)
					{
						CustomersV2 = result.Value;

						if (CustomersV2 != null)
						{
							feedbackMessage = $"Search for customer that contains last name {lastName} or phone {phoneNumber} was successful";
						}
					}
					else
					{
						errorMessage = "Missing Information";
						errorDetails = GetErrorMessages(result.Errors.ToList());
					}
				}
				catch (Exception ex)
				{
					// capture any exception message for display
					errorMessage = ex.Message;
				}

				//if (Customers.Count > 0)
				//{
				//    feedbackMessage = "Search for customer(s) was successful";
				//}
				//else
				//{
				//    feedbackMessage = "No customers were found for your search criteria";
				//    noRecords = true;
				//}
			}
			catch (ArgumentNullException ex)
			{
				errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
			}
			catch (ArgumentException ex)
			{
				errorMessage = BlazorHelperClass.GetInnerException(ex).Message;
			}
			catch (AggregateException ex)
			{
				//  have a collection of errors
				//  each error should be place into a separate line
				if (!string.IsNullOrWhiteSpace(errorMessage))
				{
					errorMessage = $"{errorMessage}{Environment.NewLine}";
				}
				errorMessage = $"{errorMessage}Unable to search for customer";
				foreach (var error in ex.InnerExceptions)
				{
					errorDetails.Add(error.Message);
				}
			}
		}

		//  new customer
		private void New()
        {
            NavigationManager.NavigateTo("/SamplePages/CustomerAddEdit/0");
        }

        //  edit selected customer
        private void EditCustomer(int customerID)
        {
            NavigationManager.NavigateTo($"/SamplePages/CustomerAddEdit/{customerID}");
        }

        //  new invoice for selected customer
        private void NewInvoice(int customerID)
        {

        }

		public static List<string> GetErrorMessages(List<BYSResults.Error> errorMessage)
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
	}
}
