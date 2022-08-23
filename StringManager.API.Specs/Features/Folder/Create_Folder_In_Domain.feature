Feature: Create_Folder_In_Domain
Note:
    Include folder tags so that folders can be tagged with SystemFolder and such in order to keep system specific texts in separate folders
  
  
    Background:
        Given that the following users exists
          | UserId                               | Username      |
          | 1407c9e0-1bc9-4e46-b576-984cf7b7038f | SuperUser     |
          | 9cda3499-8de4-4841-9dfd-ec20872ede6f | SalesReader   |
          | 306c64f1-49f1-4e4c-ad80-875d753b7192 | SalesUser     |
          | d9696b2e-806c-4ce4-9e9a-51e50b732f1a | ProductReader |
          | 1f9b3c51-a657-4eb7-aab1-1dcb459345b3 | ProductUser   |
          | f38ed952-a262-47cf-8c49-46f78c0b36ca | SuperReader   |
        And that the following access groups exists
          | AccessGroupId                        | AccessGroup             | ParentGroupId                        |
          | ec425f3b-ee49-40c1-b8fc-d5c7cc4a8910 | sales-domain-r          | 51848b79-eba9-499e-946c-a71598e560ed |
          | 51848b79-eba9-499e-946c-a71598e560ed | sales-domain-crud       | fca72704-1d47-4cca-9502-f7c8d2052d40 |
          | fca72704-1d47-4cca-9502-f7c8d2052d40 | super-crud              | <null />                             |
          | 5b0f5e44-c20c-43eb-a57e-f81a468116f2 | super-r                 | fca72704-1d47-4cca-9502-f7c8d2052d40 |
          | 28325e81-d6ea-4c52-8df4-ca068f343809 | product-domain-r        | 3a1d8302-b333-47c7-a3ae-eb4516b23c8e |
          | 3a1d8302-b333-47c7-a3ae-eb4516b23c8e | product-domain-crud     | fca72704-1d47-4cca-9502-f7c8d2052d40 |
          | 4c9d7e88-35b6-4855-aabe-69edb1d3ea5c | sales-users-folder-r    | ec425f3b-ee49-40c1-b8fc-d5c7cc4a8910 |
          | 459b5518-f423-47f8-8cc8-5b2eb7e4dd8a | sales-users-folder-crud | 51848b79-eba9-499e-946c-a71598e560ed |
          | 6aa91965-3831-425a-94cd-f22516270559 | new-users-folder-crud   | fca72704-1d47-4cca-9502-f7c8d2052d40 |
        And that the users have the following access groups
          | UserId                               | AccessGroupId                        |
          | 1407c9e0-1bc9-4e46-b576-984cf7b7038f | fca72704-1d47-4cca-9502-f7c8d2052d40 |
          | 9cda3499-8de4-4841-9dfd-ec20872ede6f | ec425f3b-ee49-40c1-b8fc-d5c7cc4a8910 |
          | 306c64f1-49f1-4e4c-ad80-875d753b7192 | 51848b79-eba9-499e-946c-a71598e560ed |
          | 306c64f1-49f1-4e4c-ad80-875d753b7192 | 459b5518-f423-47f8-8cc8-5b2eb7e4dd8a |
          | d9696b2e-806c-4ce4-9e9a-51e50b732f1a | 28325e81-d6ea-4c52-8df4-ca068f343809 |
          | 1f9b3c51-a657-4eb7-aab1-1dcb459345b3 | 3a1d8302-b333-47c7-a3ae-eb4516b23c8e |
          | f38ed952-a262-47cf-8c49-46f78c0b36ca | 5b0f5e44-c20c-43eb-a57e-f81a468116f2 |
        And that the following folders exists
          | FolderId                             | FolderName  | FolderDescription                   | ParentId                             | OwnerId                              |
          | cb889bb1-96ae-434e-ab07-bdcb853bccc5 | SalesArea   | <null />                            | <null />                             | 1407c9e0-1bc9-4e46-b576-984cf7b7038f |
          | 77842885-c2e0-4bdd-9936-0ab7b8eb0a28 | ProductArea | <null />                            | <null />                             | 1407c9e0-1bc9-4e46-b576-984cf7b7038f |
          | 5c00286f-b936-4784-9795-0b331d43baf4 | SalesItems  | A folder that holds items for sales | cb889bb1-96ae-434e-ab07-bdcb853bccc5 | 306c64f1-49f1-4e4c-ad80-875d753b7192 |
        And that the following access groups can access the folders
          | FolderId                             | AccessGroupId                        | AccessRight |
          | cb889bb1-96ae-434e-ab07-bdcb853bccc5 | ec425f3b-ee49-40c1-b8fc-d5c7cc4a8910 | -R--        |
          | cb889bb1-96ae-434e-ab07-bdcb853bccc5 | 51848b79-eba9-499e-946c-a71598e560ed | CRUD        |
          | 77842885-c2e0-4bdd-9936-0ab7b8eb0a28 | 28325e81-d6ea-4c52-8df4-ca068f343809 | -R--        |
          | 77842885-c2e0-4bdd-9936-0ab7b8eb0a28 | 3a1d8302-b333-47c7-a3ae-eb4516b23c8e | CRUD        |

    Scenario Template: Create a new folder in one of the users available domains
        Given that the user "<userid>" wants to add a new folder to the domain "<domainid>"
        And that the user enters the following information about the folder
          | Name       | Description                    |
          | New folder | A description for this folder. |
        When the user sends the request
        Then the new folder is created with expected information
        And the user with id "<userid>" is set as the folders owner
        And the user gets the http status code "201 Created"
        And the following information about the folder is returned
          | Id       | Name      | Description                    |
          | <GUID /> | NewFolder | A description for this folder. |

        Scenarios:
          | userid | domainid |
          | 1      | 1        |
          | 1      | 2        |
          | 3      | 1        |
          | 5      | 2        |

    Scenario: Create a new folder with a specific access group
        Given that the user "1" wants to add a new folder to the domain "1"
        And that the user enters the following information about the folder
          | Name             | Description                               |
          | SalesUsersFolder | A description for the sales users folder. |
        And that the user adds the following access groups
          | AccessGroupId | AccessType |
          | 7             | -R--       |
          | 8             | CRUD       |
        When the user sends the request
        Then the new folder is created with the expected information
        And the user with id "<userid>" is set as the folders owner
        And the folder has the following access groups
          | AccessGroupId | AccessType |
          | 7             | -R--       |
          | 8             | CRUD       |
        And the user gets the http status code "201 Created"
        And the following information about the folder is returned
          | Id       | Name             | Description                               |
          | <GUID /> | SalesUsersFolder | A description for the sales users folder. |

    Scenario: Create a new folder in domain the user has access to without any access group
        Given that the user "3" wants to add a new folder to the domain "1"
        And that the user enters the following information about the folder
          | Name             | Description                               |
          | SalesUsersFolder | A description for the sales users folder. |
        When the user sends the request
        Then the new folder is created with the expected information
        And the user with id "3" is set as the folders owner
        And the folder has no access groups
        And the user gets the http status code "201 Created"
        And the following information about the folder is returned
          | Id       | Name             | Description                               |
          | <GUID /> | SalesUsersFolder | A description for the sales users folder. |

    Scenario: Create a new folder with a child access group when the user has the parent access group
        Given that the user "1" wants to add a new folder to the domain "1"
        And that the user enters the following information about the folder
          | Name             | Description                               |
          | SalesUsersFolder | A description for the sales users folder. |
        When the user sends the request
        Then the new folder is created with the expected information
        And the user with id "3" is set as the folders owner
        And the folder has no access groups
        And the user gets the http status code "201 Created"
        And the following information about the folder is returned
          | Id       | Name             | Description                               |
          | <GUID /> | SalesUsersFolder | A description for the sales users folder. |

    Scenario Template: Create a new folder with a user that does not have create access
        Given that the user "<userid>" wants to add a new folder to the domain "<domainid>"
        And that the user enters the following information about the folder
          | Name      | Description                    |
          | NewFolder | A description for this folder. |
        When the user sends the request
        Then no new folder is created
        And the user gets the http status code "401 Unauthorized"
        And the following problem detail is returned
          | ProblemType                     | Title             | Description                                                                           |
          | Problem.Folder.UnauthorizedUser | Unauthorized user | The user does not have the permissions to create a new folder in the selected domain. |

        Scenarios:
          | userid | domainid |
          | 2      | 1        |
          | 2      | 2        |
          | 3      | 2        |
          | 4      | 2        |
          | 4      | 1        |
          | 5      | 1        |
          | 6      | 1        |
          | 6      | 2        |

    Scenario: Create a new folder with a specific access group that the user does not have access to
        Given that the user "3" wants to add a new folder to the domain "1"
        And that the user enters the following information about the folder
          | Name      | Description                    |
          | NewFolder | A description for this folder. |
        And that the user adds the following access groups
          | AccessGroupId | AccessType |
          | 9             | CRUD       |
        When the user sends the request
        Then no new folder is created
        And the user gets the http status code "401 Unauthorized"
        And the following problem detail is returned
          | ProblemType                                   | Title             | Description                                                                        |
          | Problem.Folder.UnauthorizedUserForAccessGroup | Unauthorized user | The user does not have the permissions create a folder within the selected domain. |

    Scenario: Create a new folder with the same name as an existing folder
        Given that the user "1" wants to add a new folder to the domain "1"
        And that the user enters the following information about the folder
          | Name       | Description                            |
          | SalesItems | A folder that holds sales information. |
        And that the user adds the following access groups
          | AccessGroupId | AccessType |
          | 9             | CRUD       |
        When the user sends the request
        Then no new folder is created
        And the user gets the http status code "400 Bad Request"
        And the following problem detail is returned
          | ProblemType                       | Title              | Description                                      |
          | Problem.Folder.ExistingFolderName | Folder name exists | A folder with the requested name already exists. |

    Scenario Template: Create a new folder with an invalid folder name
        Given that the user "1" wants to add a new folder to the domain "1"
        And that the user enters the following information about the folder
          | Name                | Description                    |
          | <invalidFolderName> | A description for this folder. |
        And that the user adds the following access groups
          | AccessGroupId | AccessType |
          | 9             | CRUD       |
        When the user sends the request
        Then no new folder is created
        And the user gets the http status code "401 Unauthorized"
        And the following problem detail is returned
          | ProblemType                      | Title               | Description                                 |
          | Problem.Folder.InvalidFolderName | Invalid folder name | The name entered for the folder is invalid. |

        Scenarios:
          | invalidFolderName    |
          | name\this            |
          | <empty />            |
          | "a name contains "   |
          | 'thenamewith'        |
          | `namesurroundedwith` |

    @mvp2
    Scenario: Create sub folder to existing folder