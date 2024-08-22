Feature: Product Management

A short summary of the feature

@tag1
Scenario: Insert Product
    Given I fill in the product details
    | ProductName | Category  | Price  | StockQuantity |
    | ExampleProd | Category1 | 245.00 | 100           |
    When the product record is inserted into the database
    | ProductName | Category  | Price  | StockQuantity |
    | ExampleProd | Category1 | 245.00 | 100           |
    Then The result is stored in the database
    | ProductName | Category  | Price  | StockQuantity |
    | ExampleProd | Category1 | 245.00 | 100           |
