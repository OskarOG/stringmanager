Feature: Get_StringCategories

    Background:
        Given that we have the following categories
          | AccessGroup  | Id | Name        | Description    | AmountOfItemsInCategory |
          | basic-list   | 1  | BasicList   | A basic list   | 100                     |
          | special-list | 2  | SpecialList | A special list | 5                       |
        And that the categories have the following tags
          | CategoryId | Tag       |
          | 1          | Basic     |
          | 1          | CustomTag |
          | 2          | Special   |
        And that the users have the following access groups
          | Username  | AccessGroup     |
          | basicUser | basic-list-r    |
          | adminUser | basic-list-crud |
          | newUser   | new-list-r      |

    Scenario Outline: Get all available categories for a user with read access to the category
        Given that the user "<currentUser>" want to get all available categories
        When the api gets the request
        Then all information about categories with acceess group "<accessGroup>" is returned
        And the reponse has the http status code "200 Ok"

        Examples:
          | currentUser | accessGroup     |
          | basicUser   | basic-list-r    |
          | adminUser   | basic-list-crud |

    Scenario: Get all categories when there is no categories available to the user
        Given that the user "newUser" want to get all available categories
        When the api gets the request
        Then no categories are returned
        And the response has the http status code "200 Ok"
        
    Scenario: Get all categories for invalid user
        Given that the user "invalidUsername" want to get all available categories
        When the api gets the request
        Then the following problem detail is returned
          | ProblemType     | Title        | Detail                                       |
          | InvalidUserName | Invalid user | The user specified in the request is invalid |
        And the response has the http status code "400 Bad Request"