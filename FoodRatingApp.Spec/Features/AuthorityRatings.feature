Feature: Authority ratings response contract

Validate the structure and content of the /api/{authorityId} ratings endpoint.

@contract
Scenario Outline: Ratings response contains exactly 6 categories
    Given the API endpoint is "https://localhost:5001/api/<authorityId>"
    When the API is called
    Then the response should contain 6 rating items

Examples:
    | authorityId |
    | 1           |
    | 999         |

@contract
Scenario Outline: All rating values sum to 100
    Given the API endpoint is "https://localhost:5001/api/<authorityId>"
    When the API is called
    Then the sum of all rating values should equal 100.0

Examples:
    | authorityId |
    | 1           |
    | 999         |

@contract
Scenario: Ratings include all expected star categories
    Given the API endpoint is "https://localhost:5001/api/1"
    When the API is called
    Then the ratings should include the following categories:
      | category |
      | 5-star   |
      | 4-star   |
      | 3-star   |
      | 2-star   |
      | 1-star   |
      | Exempt   |

@contract
Scenario: Response content type is application/json
    Given the API endpoint is "https://localhost:5001/api/1"
    When the API is called
    Then the response content type should be "application/json"
