# Domain-Model Layer

Responsible for representing concepts of the business, information about the business situation, 
and business rules. State that reflects the business situation is controlled and used here, 
even though the technical details of storing it are delegated to the infrastructure.

This layer contains the following:

- Domain Entity Model
- POCO entity classes (Clean C# code)
- Domain Entities with data + behaviour
- DDD Patterns:
	- Domain Entity, Aggregate
	- Aggregate-Root, Value-Object
	- Repository contracts/interfaces