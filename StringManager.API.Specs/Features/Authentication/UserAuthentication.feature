Feature: Authentication/UserAuthentication

   Background:
      Given that the following users exists
        | UserId                               | Email                 | UserRole      | Password  |
        | 616aba5c-e933-4b22-a383-93aea94379b4 | adminuser@email.com   | Administrator | Password1 |
        | c0a1b5bd-d48d-44c3-bf25-0500ebbd239d | useradmin@email.com   | UserAdmin     | Password2 |
        | 910b77cb-ee2a-4673-bc34-78c0992b0f05 | folderadmin@email.com | FolderAdmin   | Password3 |
        | 11a8f678-b1b4-4d95-8191-0ea27f72407e | user@email.com        | User          | Password4 |
      And that the current date and time is "2022-08-02 12:00"
      
   @UserAuthenticationToken
   Scenario Template: User can authenticate and get a token that is valid for the expected time
      Given that the user with the following information wants to sign in
        | Email       | Password       |
        | <userEmail> | <userPassword> |
      When the create token request is sent
      Then the http status code "201 Created" is returned
      And a valid token is returned
      And the token has a valid length of 30 minutes
      And the token has the "<userRole>" as a claim

      Scenarios:
        | userEmail             | userPassword | userRole      |
        | adminuser@email.com   | Password1    | Administrator |
        | useradmin@email.com   | Password2    | UserAdmin     |
        | folderadmin@email.com | Password3    | FolderAdmin   |
        | user@email.com        | Password4    | User          |

   @BusinessRuleError
   Scenario Template: User wants to authenticate with invalid email
      Given that the user with the following information wants to sign in
        | Email       | Password       |
        | <userEmail> | ValidPassword1 |
      When the create token request is sent
      Then the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType  | Title         | Detail                                         |
        | InvalidEmail | Invalid email | The email entered for the new user is invalid. |

      Scenarios:
        | invalidEmail |
        | hello        |
        | 123          |
        | <NULL />     |
        | <EMPTY />    |
        | email@com    |
        | email@.com   |
        | @server.com  |

   @UserError
   Scenario: User wants to authenticate with non existing email
      Given that the user with the following information wants to sign in
        | Email                     | Password       |
        | nonexistinguser@email.com | ValidPassword1 |
      When the create token request is sent
      Then the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType          | Title                   | Detail                                   |
        | WrongUserInformation | Faulty user information | The user information entered is invalid. |

   @UserError
   Scenario: User wants to authenticate with the wrong password
      Given that the user with the following information wants to sign in
        | Email          | Password       |
        | user@email.com | WrongPassword1 |
      When the create token request is sent
      Then the http status code "400 Bad Request" is returned
      And the following problem detail is returned
        | ProblemType          | Title                   | Detail                                   |
        | WrongUserInformation | Faulty user information | The user information entered is invalid. |