Feature: Food ratings API integration scenarios
  In order to surface food hygiene ratings to consumers
  As a client of the Food Ratings API
  I want the API to return well-structured rating and authority data
  So that downstream systems can reliably consume and display it

@integration
Scenario Outline: Ratings response returns the correct star rating and percentage value
  Given the API endpoint is "<endpoint>"
  When the API is called
  Then the response HTTP status code should be 200 OK
  And the response should contain "<star_rating>"
  And the response should contain "<percentage>"

Examples:
  | endpoint                       | star_rating | percentage |
  | https://localhost:5001/api/1   | 5-star      | 22.41      |
  | https://localhost:5001/api/999 | 5-star      | 50         |

@integration
Scenario: Authority list response includes an id and name for each authority
  Given the API endpoint is "https://localhost:5001/api"
  When the API is called
  Then the response HTTP status code should be 200 OK
  And the response should contain JSON field "id"
  And the response should contain JSON field "name"

@integration
Scenario: Request with a non-integer authority id returns 400 Bad Request
  Given the API endpoint is "https://localhost:5001/api/invalid"
  When the API is called
  Then the response HTTP status code should be 400 Bad Request
