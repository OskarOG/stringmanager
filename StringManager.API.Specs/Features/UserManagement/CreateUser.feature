Feature: UserManagement/CreateUser
Note: Improve the SuperUser handling
Maybe it could be divided up into a role or tag functionallity where the first user created is an Administrator
After that it is possible to select the role for the user
- Administrator
- UserAdmin
- FolderAdmin
- User

   Background:
      Given that the following users exists
        | UserId                               | Email                 | UserRole      | Password  |
        | 616aba5c-e933-4b22-a383-93aea94379b4 | adminuser@email.com   | Administrator | Password1 |
        | c0a1b5bd-d48d-44c3-bf25-0500ebbd239d | useradmin@email.com   | UserAdmin     | Password2 |
        | 910b77cb-ee2a-4673-bc34-78c0992b0f05 | folderadmin@email.com | FolderAdmin   | Password3 |
        | 11a8f678-b1b4-4d95-8191-0ea27f72407e | user@email.com        | User          | Password4 |
      And that the users have the following roles
        | UserId                               | RoleType      |
        | 616aba5c-e933-4b22-a383-93aea94379b4 | Administrator |
        | c0a1b5bd-d48d-44c3-bf25-0500ebbd239d | UserAdmin     |
        | 910b77cb-ee2a-4673-bc34-78c0992b0f05 | FolderAdmin   |
      And that the following access groups exists
        | AccessGroupId                        | AccessGroupName |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 | Marketing       |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 | Sales           |

   @ResourceCreation
   Scenario Template: User with user management role or administrator creates new standard user
      Given that the user "<userId>" is signed in
      And want to create a new user with the following information
        | Email              | Password | RoleType |
        | testuser@email.com | abc123   | User     |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 |
      When the new user request is sent
      Then the new user is created with the expected information
      And the new users id is returned
      And the http status code "201 Created" is returned

   ## The difference between these two scenarios is the role of the user calling the API, both should be able to create the user.
      Scenarios:
        | userId                               |
        | 616aba5c-e933-4b22-a383-93aea94379b4 |
        | c0a1b5bd-d48d-44c3-bf25-0500ebbd239d |

   @ResourceCreation
   Scenario: Administrator tries to create new user with Administrator role
      Given that the user "616aba5c-e933-4b22-a383-93aea94379b4" is signed in
      And want to create a new user with the following information
        | Email              | Password | RoleType      |
        | testuser@email.com | abc123   | Administrator |
      When the new user request is sent
      Then the new user is created with the expected information
      And the new users id is returned
      And the http status code "201 Created" is returned

   @ResourceCreation
   Scenario: Administrator tries to create new user with UserAdmin role
      Given that the user "616aba5c-e933-4b22-a383-93aea94379b4" is signed in
      And want to create a new user with the following information
        | Email              | Password | RoleType  |
        | testuser@email.com | abc123   | UserAdmin |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 |
      When the new user request is sent
      Then the new user is created with the expected information
      And the new users id is returned
      And the http status code "201 Created" is returned

   @ResourceCreation
   Scenario: User admin tries to create new user with UserAdmin role
      Given that the user "c0a1b5bd-d48d-44c3-bf25-0500ebbd239d" is signed in
      And want to create a new user with the following information
        | Email              | Password | RoleType  |
        | testuser@email.com | abc123   | UserAdmin |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 |
      When the new user request is sent
      Then the new user is created with the expected information
      And the new users id is returned
      And the http status code "201 Created" is returned

   @ResourceCreation
   Scenario: User admin tries to create new user with FolderAdmin role
      Given that the user "c0a1b5bd-d48d-44c3-bf25-0500ebbd239d" is signed in
      And want to create a new user with the following information
        | Email              | Password | RoleType    |
        | testuser@email.com | abc123   | FolderAdmin |
      When the new user request is sent
      Then the new user is created with the expected information
      And the new users id is returned
      And the http status code "201 Created" is returned

   @BusinessRuleError
   Scenario: User admin tries to create new administrator
      Given that the user "c0a1b5bd-d48d-44c3-bf25-0500ebbd239d" is signed in
      And want to create a new user with the following information
        | Email              | Password | RoleType      |
        | testuser@email.com | abc123   | Administrator |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 |
      When the new user request is sent
      Then a new user is not created
      And the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType                | Title                    | Detail                                                         | HttpStatus |
        | UnauthorizedRoleAssignment | Insufficient permissions | You can not create a new user with the role that you selected. | 400        |

   @BusinessRuleError
   Scenario: Folder admin tries to create new user
      Given that the user "910b77cb-ee2a-4673-bc34-78c0992b0f05" is signed in
      And want to create a new user with the following information
        | Email              | Password | RoleType |
        | testuser@email.com | abc123   | User     |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 |
      When the new user request is sent
      Then a new user is not created
      And the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType             | Title                    | Detail                         | HttpStatus |
        | InsufficientPermissions | Insufficient permissions | You can not create a new user. | 400        |

   @BusinessRuleError
   Scenario: Administrator tries to create a new user with an email that is already registered
      Given that the user "616aba5c-e933-4b22-a383-93aea94379b4" is signed in
      And want to create a new user with the following information
        | Email                 | Password | RoleType |
        | folderadmin@email.com | abc123   | User     |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 |
      When the new user request is sent
      Then a new user is not created
      And the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType   | Title          | Detail                                                    | HttpStatus |
        | ExistingEmail | Existing email | The email entered for the new user is already registered. | 400        |

   @BusinessRuleError
   Scenario Template: Administrator tries to create a new user with an invalid email
      Given that the user "616aba5c-e933-4b22-a383-93aea94379b4" is signed in
      And want to create a new user with the following information
        | Email          | Password | RoleType |
        | <invalidEmail> | abc123   | User     |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 |
      When the new user request is sent
      Then a new user is not created
      And the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType  | Title         | Detail                                         | HttpStatus |
        | InvalidEmail | Invalid email | The email entered for the new user is invalid. | 400        |

      Scenarios:
        | invalidEmail |
        | hello        |
        | 123          |
        | <NULL />     |
        | <EMPTY />    |
        | email@com    |
        | email@.com   |
        | @server.com  |

   @BusinessRuleError
   Scenario Template: Administrator tries to create a new user with invalid password
      Given that the user "616aba5c-e933-4b22-a383-93aea94379b4" is signed in
      And want to create a new user with the following information
        | Email          | Password          | RoleType |
        | test@email.com | <invalidPassword> | User     |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | ec77e8bf-f31b-411b-8942-6cfcce1266e7 |
      When the new user request is sent
      Then a new user is not created
      And the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType     | Title            | Detail                                            | HttpStatus |
        | InvalidPassword | Invalid password | The password entered for the new user is invalid. | 400        |

      Scenarios:
        | invalidPassword |
        | hello           |
        | 123             |
        | <NULL />        |
        | <EMPTY />       |
        | qwerty1         |
        | qwerty123!      |
        | Qwerty123       |

   @BusinessRuleError
   Scenario: Administrator tries to create a new user with initial access group that does not exist
      Given that the user "616aba5c-e933-4b22-a383-93aea94379b4" is signed in
      And want to create a new user with the following information
        | Email          | Password          | RoleType |
        | test@email.com | <invalidPassword> | User     |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | 8c9e47e5-b2ec-4818-8fe8-35241e3d26fb |
      When the new user request is sent
      Then a new user is not created
      And the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType        | Title                | Detail                                                             | HttpStatus |
        | InvalidAccessGroup | Invalid access group | One of the initial access groups added to the user does not exist. | 400        |

   @AuthenticationError
   Scenario: User that is not signed in tries to create a new user
      Given that no user is signed in
      And want to create a new user with the following information
        | Email          | Password          | RoleType |
        | test@email.com | <invalidPassword> | User     |
      And that the new user should be included in the following access groups
        | AccessGroupId                        |
        | 4605a27b-6f86-47c3-ac1a-547aa1b01fe3 |
        | 8c9e47e5-b2ec-4818-8fe8-35241e3d26fb |
      When the new user request is sent
      Then a new user is not created
      And the http status code "401 Unauthorized" is returned
      And the following problem detail is returned
        | ProblemType        | Title          | Detail                                         | HttpStatus |
        | UnauthorizedAction | Not authorized | You are not authorized to perform this action. | 401        |