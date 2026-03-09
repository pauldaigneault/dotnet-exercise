Feature: Authority list response contract

Validate the structure and content of the /api authority list endpoint.

@contract
Scenario: Authority list returns a non-empty collection
    Given the API endpoint is "https://localhost:5001/api"
    When the API is called
    Then the response should contain at least 1 authority

@contract
Scenario: Each authority has a positive integer id
    Given the API endpoint is "https://localhost:5001/api"
    When the API is called
    Then every authority id should be a positive integer

@contract
Scenario: Each authority has a non-empty name
    Given the API endpoint is "https://localhost:5001/api"
    When the API is called
    Then every authority name should be a non-empty string

@contract
Scenario: Response content type is application/json
    Given the API endpoint is "https://localhost:5001/api"
    When the API is called
    Then the response content type should be "application/json"
