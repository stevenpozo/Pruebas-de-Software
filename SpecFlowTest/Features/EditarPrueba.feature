Feature: Actualizar Producto

Como administrador
Quiero actualizar la información de un producto
Para reflejar cambios en precios o en stock

@tag1
Scenario: Editar el registro de un producto
    Given el producto con el nombre "ExampleProd" existe 
    When edito los campos del producto existente
      | ProductName | Category  | Price  | StockQuantity |
      | ExampleProd | Category1 | 900.00 | 40            |
    Then los cambios se almacenan en la base de datos existente
    And los nuevos datos del producto existente
      | ProductName | Category  | Price  | StockQuantity |
      | ExampleProd | Category1 | 900.00 | 40            |
