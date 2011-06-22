Feature: Admin Accounts
	In order to manage administrator accounts
	As an administrator
	I want to be add, edit, delete admin accounts using a nice interface

Scenario: Admin visits the admin account list page
	Given the following admin accounts exist in the database
	| Id                                   | FirstName | LastName |
	| 20C492E8-B610-43F8-B97A-BDD50C9C864E | John      | Galt     |
	When the administrator visits the Admin Account list page
	Then he should see the Admin Account list page
	And he should see the following accounts on the list page
	| FirstName | LastName | Id                                   |
	| John      | Galt     | 20c492e8-b610-43f8-b97a-bdd50c9c864e |