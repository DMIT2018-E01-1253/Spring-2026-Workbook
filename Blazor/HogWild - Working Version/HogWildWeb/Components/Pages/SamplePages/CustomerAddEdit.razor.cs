using BYSResults;
using HogWildSystem.BLL;
using HogWildSystem.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HogWildWeb.Components.Pages.SamplePages
{
	partial class CustomerAddEdit
	{
		#region Fields
		[Parameter]
		public int CustomerID { get; set; } = 0;

		private HogWildSystemV2.ViewModels.CustomerView customer = new();

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

		private MudForm customerForm = new();

		private List<LookupView> countries = new();
		private List<LookupView> provinces = new();
		private List<LookupView> statuslookups = new();
		#endregion

		#region Properties
		[Inject]
		protected HogWildSystemV2.BLL.CustomerService CustomerServiceV2 { get; set; } = default!;
		[Inject]
		protected CategoryLookupService CategoryLookupService { get; set; } = default!;
		#endregion

		#region Validation
		// flag to if the form is valid.
		private bool isFormValid;
		//  flag if data has change
		private bool hasDataChanged;
		#endregion

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			try
			{
				//  reset the error detail list
				errorDetails.Clear();

				//  reset the error message to an empty string
				errorMessage = string.Empty;

				//  reset feedback message to an empty string
				feedbackMessage = String.Empty;
				//  check to see if we are navigating using a valid customer CustomerID.
				//      or are we going to create a new customer.
				if (CustomerID > 0)
				{
					var result = CustomerServiceV2.GetCustomerByID(CustomerID);
					if (result.IsSuccess)
					{
						customer = result.Value;
					}
					else
					{
						errorMessage = "Missing Information";
						errorDetails = GetErrorMessages(result.Errors.ToList());
					}
				}

				// lookups
				provinces = CategoryLookupService.GetLookups("Province");
				countries = CategoryLookupService.GetLookups("Country");
				statuslookups = CategoryLookupService.GetLookups("Customer Status");

				StateHasChanged();
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

		private void Save()
		{
			// clear previous error details and messages
			errorDetails.Clear();
			errorMessage = string.Empty;
			feedbackMessage = String.Empty;

			// wrap the service call in a try/catch to handle unexpected exceptions
			try
			{
				var result = CustomerServiceV2.AddEditCustomer(customer);
				if (result.IsSuccess)
				{
					customer = result.Value;
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
	}
}
