This is the simplest possible approach for queries. The query definitions query the database and return a dynamic ViewModel built on the 
fly for each query. Since the queries are idempotent, they will not change the data no matter how many times you run a query. Therefore, 
you do not need to be restricted by any DDD pattern used in the transactional side, like aggregates and other patterns, and that is why 
queries are separated from the transactional area. You simply query the database for the data that the UI needs and return a dynamic 
ViewModel that does not need to be statically defined anywhere (no classes for the ViewModels) except in the SQL statements themselves.