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

Scenario: Admin goes to the edit page for an admin account
	Given the following admin accounts exist in the database
	| Id                                   | FirstName | LastName | Username |
	| 1567DDA0-8FC1-45C5-B0D3-F9396DD9BDB8 | Howard    | Roark    | hroark   |
	When the administrator visits the Admin Account edit page for '1567DDA0-8FC1-45C5-B0D3-F9396DD9BDB8'
	Then he should see the Admin Account edit page
	And he should see an admin account edit form with the following values
	| Field     | Value                                |
	| Id        | 1567dda0-8fc1-45c5-b0d3-f9396dd9bdb8 |
	| FirstName | Howard                               |
	| LastName  | Roark                                |
	| Username  | hroark                               |
	| Password  |                                      |

Scenario: Admin goes to the create page for an admin account
	When the administrator visits the Admin Account edit page for ''
	Then he should see the Admin Account edit page
	And he should see an admin account edit form with the following values
	| Field     | Value |
	| FirstName |       |
	| LastName  |       |
	| Username  |       |
	| Password  |       |

Scenario: Admin edits an admin account
	Given the following admin accounts exist in the database
	| Id                                   | FirstName | LastName | Username |
	| 20c492e8-b610-43f8-b97a-bdd50c9c864e | E     | W    | sdf |
	When the administrator submits the following Admin Account edit page
	| Field     | Value                                |
	| Id        | 20c492e8-b610-43f8-b97a-bdd50c9c864e |
	| FirstName | Ellis                                |
	| LastName  | Wyatt                                |
	| Username  | wyattoil                             |
	| Password  | elliswyattoil                        |
	Then he should see the Admin Account edit page
	And the following admin accounts should exist in the database
	| Id                                   | FirstName | LastName | Username | Password                                     |
	| 20C492E8-B610-43F8-B97A-BDD50C9C864E | Ellis     | Wyatt    | wyattoil | upsrXq/NBgWdbsiDjl9dto6Dtu1Oba3wjYghQjOrGM0= |

Scenario: Admin creates an admin account
	Given the following admin accounts exist in the database
	| Id                                   | FirstName | LastName | Username |
	When the administrator submits the following Admin Account edit page
	| Field     | Value                                |
	| Id        | 73977dae-10fa-4311-95b1-9b3ecca0023d |
	| FirstName | Ellis                                |
	| LastName  | Wyatt                                |
	| Username  | wyattoil                             |
	| Password  | elliswyattoil                        |
	Then he should see the Admin Account edit page
	And the following admin accounts should exist in the database
	| Id                                   | FirstName | LastName | Username | Password                                     |
	| 73977DAE-10FA-4311-95B1-9B3ECCA0023D | Ellis     | Wyatt    | wyattoil | upsrXq/NBgWdbsiDjl9dto6Dtu1Oba3wjYghQjOrGM0= |

Scenario: Admin does not set a password when saving it
	Given the following admin accounts exist in the database
	| Id                                   | FirstName | LastName | Username | Password                                     |
	| 20c492e8-b610-43f8-b97a-bdd50c9c864e | E         | W        | sdf      | upsrXq/NBgWdbsiDjl9dto6Dtu1Oba3wjYghQjOrGM0= |
	When the administrator submits the following Admin Account edit page
	| Field     | Value                                |
	| Id        | 20c492e8-b610-43f8-b97a-bdd50c9c864e |
	| FirstName | Ellis                                |
	| LastName  | Wyatt                                |
	| Username  | wyattoil                             |
	| Password  |                                      |
	Then he should see the Admin Account edit page
	And the following admin accounts should exist in the database
	| Id                                   | FirstName | LastName | Username | Password                                     |
	| 20C492E8-B610-43F8-B97A-BDD50C9C864E | Ellis     | Wyatt    | wyattoil | upsrXq/NBgWdbsiDjl9dto6Dtu1Oba3wjYghQjOrGM0= |

Scenario: Delete an account
	Given the following admin accounts exist in the database
	| Id                                   | FirstName | LastName | Username | Password                                     |
	| 20c492e8-b610-43f8-b97a-bdd50c9c864e | A         | D        | usera    | upsrXq/NBgWdbsiDjl9dto6Dtu1Oba3wjYghQjOrGM0= |
	| A8886A73-90FC-4675-A943-59A1A73CF19B | B         | E        | userb    | upsrXq/NBgWdbsiDjl9dto6Dtu1Oba3wjYghQjOrGM1= |
	| 2F44420B-4D33-405E-9599-54D4FFFE9F0A | C         | F        | userc    | upsrXq/NBgWdbsiDjl9dto6Dtu1Oba3wjYghQjOrGM2= |
	When the administrator deletes the admin account '20c492e8-b610-43f8-b97a-bdd50c9c864e'
	Then he should be sent to the admin account list page