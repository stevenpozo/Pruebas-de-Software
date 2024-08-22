Feature: ReadPrueba

Consultar todos los productos o buscar un producto específico por ID.

@tag1
Scenario: Consultar todos los productos
    Given que hay productos en la base de datos
    When consulto todos los productos
    Then todos los productos deberían ser devueltos

@tag1
Scenario: Buscar un producto específico por ID
    Given que un producto con el ID "1" existe en la base de datos
    When busco el producto con el ID "1"
    Then el producto con el ID "1" debería ser devuelto
    And el producto debería tener los siguientes datos:
      | ProductId | ProductName     | Category    | Price  | StockQuantity |
      | 1         | Apple iPhone 14 | Electronics | 999.99 | 50            |
