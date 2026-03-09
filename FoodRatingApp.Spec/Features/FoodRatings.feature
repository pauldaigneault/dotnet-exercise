Feature: Food ratings API integration scenarios
  In order to surface food hygiene ratings to consumers
  As a client of the Food Ratings API
  I want the API to return well-structured rating and authority data
  So that downstream systems can reliably consume and display it

@integration
Scenario Outline: Get authority ratings for authority id
    Given the API endpoint is "<endpoint>"
    When the API is called
    Then the response status code should be 200
    And the response should contain "<star_rating>"
    And the response should contain "<value>"
    And the response should contain JSON field "name"
    And the response should contain JSON field "value"

Examples:
    | endpoint                       | star_rating | value |
    | https://localhost:5001/api/1   | 5-star      | 22.41 |
    | https://localhost:5001/api/999 | 5-star      | 50    |

@integration
Scenario Outline: Get authority list contract
    Given the API endpoint is "<endpoint>"
    When the API is called
    Then the response status code should be 200
    And the response should contain JSON field "id"
    And the response should contain JSON field "name"

Examples:
    | endpoint                     |
    | https://localhost:5001/api   |
