Feature: Authority list response contract
  In order to present users with a list of local authorities
  As a client of the Food Ratings API
  I want the GET /api endpoint to return a valid, non-empty collection of authorities
  So that each authority can be identified by a unique positive integer id and a non-empty name

  Background:
    Given the following authorities exist:
      | id | name        |
      | 1  | Authority 1 |
      | 2  | Authority 2 |
    And the API endpoint is "https://localhost:5001/api"

  @contract
  Scenario: Authority list returns a non-empty collection
    When the API is called
    Then the response should contain at least 1 authority

  @contract
  Scenario: Each authority has a positive integer id
    When the API is called
    Then every authority id should be a positive integer

  @contract
  Scenario: Each authority has a non-empty name
    When the API is called
    Then every authority name should be a non-empty string

  @contract
  Scenario: Response content type is application/json
    When the API is called
    Then the response content type should be "application/json"
