Feature: Editar

  Este feature permite editar un registro existente en la base de datos.

  @tag1
  Scenario: Editar el registro de un cliente existente
    Given que el cliente con la cédula "1753368099" existe en la base de datos
    When edito los campos del cliente con los siguientes datos:
      | Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                     | Telefono   | Direccion | Estado | Saldo  |
      | 1753368099 | Pozo      | Steven  | 2024-08-21      | stevenpozo0999@gmail.com | 0981515127 | Quito     | 1      | 500.00 |
    Then los cambios se almacenan en la base de datos correctamente
    And los nuevos datos del cliente deberían ser los siguientes:
      | Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                     | Telefono   | Direccion | Estado | Saldo  |
      | 1753368099 | Pozo      | Steven  | 2024-08-21      | stevenpozo0999@gmail.com | 0981515127 | Quito     | 1      | 500.00 |
