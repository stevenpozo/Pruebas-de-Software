Feature: Delete Product

A short summary of the feature

@tag1
Scenario: Eliminar Producto
    Given el producto con los siguientes datos:
    | ProductName | Category  | Price  | StockQuantity |
    | ExampleProd | Category2 | 245.00 | 100           |
    When el producto es eliminado de la BD
    | ProductName |
    | ExampleProd |
    Then el producto ya no debe existir en la BD
    | ProductName |
    | ExampleProd |
