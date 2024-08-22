Feature: Ingreso

A short summary of the feature

@tag1
Scenario: Insert Client
	Given llenar los campos de la bd
	| Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                    | Telefono   | Direccion | Estado | Saldo  |
	| 1753368099 | Pozo      | Steven  | 2024-08-21      | stevenpozo099@gmail.com | 0981515127 | Tumbaco   | 1      | 345.34 |
	When el registro de ingresa en la BD
	| Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                    | Telefono   | Direccion | Estado | Saldo  |
	| 1753368099 | Pozo      | Steven  | 2024-08-21      | stevenpozo099@gmail.com | 0981515127 | Tumbaco   | 1      | 345.34 |
	Then El resultado se almacena en la BD
    | Cedula     | Apellidos | Nombres | FechaNacimiento | Mail                    | Telefono   | Direccion | Estado | Saldo  |
	| 1753368099 | Pozo      | Steven  | 2024-08-21      | stevenpozo099@gmail.com | 0981515127 | Tumbaco   | 1      | 345.34 |

