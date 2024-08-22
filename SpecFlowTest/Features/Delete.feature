Feature: Delete

A short summary of the feature

@tag1
Scenario: Eliminar Cliente
    Given el cliente con los siguientes datos:
    | Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                    | Telefono   | Direccion | Estado | Saldo  |
    | 1753368099 | Pozo      | Steven  | 2024-08-21      | stevenpozo099@gmail.com | 0981515127 | Quito     | 1      | 500.00 |
    When el cliente es eliminado de la BD
    | Cedula     |
    | 1753368099 |
    Then el cliente ya no debe existir en la BD
    | Cedula     |
    | 1753368099 |
