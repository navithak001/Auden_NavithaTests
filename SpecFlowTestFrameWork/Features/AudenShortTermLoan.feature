Feature: ShortTermLoan
	 Validate Short Term Loan page

@Auden
Scenario: Validation of STL Functions
	Given I am on the Short Term Loan homepage

	#give only weekend date here
	When i select weekend as Repayment day 
		 | Day |
		 | 26  |
	Then Verify Valid First Repayment Day is displayed
	When I slide to set Loan Amount
		| Amount |
		|  420   |
	Then Verify the amount displayed
	And i Close Browser