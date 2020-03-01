# REST API Key Constraints

- Client-Server 
The client-server constraint operates on the concept that the client and the 
server should be separate from each other and allowed to evolve 
individually.

- Stateless 
REST APIs are stateless, meaning that calls can be made independently of 
one another, and each call contains all of the data necessary to complete 
itself successfully. A REST API should not rely on data being stored on the 
server or sessions to determine what to do with a call, but rather solely rely 
on the data that is provided in that call itself. 

- Cache 
Because a stateless API can increase request overhead by handling large 
loads of incoming and outbound calls, a REST API should be designed to 
encourage the storage of cacheable data. This means that when data is 
cacheable, the response should indicate that the data can be stored up to a 
certain time (expires-at), or in cases where data needs to be real-time, that 
the response should not be cached by the client. 


- Uniform Interface 
The key to the decoupling client from server is having a uniform interface 
that allows independent evolution of the application without having the 
application’s services, or models and actions, tightly coupled to the API 
layer itself. The uniform interface lets the client talk to the server in a single 
language, independent of the architectural backend of either. This interface 
should provide an unchanging, standardized means of communicating 
between the client and the server, such as using HTTP with URI resources, 
CRUD (Create, Read, Update, Delete) and JSON. 

- Layered System 
As the name implies, a layered system is a system comprised of layers, 
with each layer having a specific functionality and responsibility. If we think 
of a Model View Controller framework, each layer has its own 
responsibilities, with the models comprising how the data should be formed, 
the controller focusing on the incoming actions and the view focusing on 
the output. Each layer is separate but also interacts with the other. 




# Web API Best Practices

- Async conrollers with Async queries
- Eagor loading instead of #lazyloading
- DI of the context to Controllers per request #injectingcontext
- Global Exception handlers instead of repeating same code more and more #globalexceptionhandler
- Model validation action filters to validate models on each request automatically #ModelValidationActionFilter
- Content negociation e.g for XML #ContentNegociation
- API versioning #ApiVersioning
  https://dev.to/htissink/versioning-asp-net-core-apis-with-swashbuckle-making-space-potatoes-v-x-x-x-3po7
- Automatic Documentaion using Swagger #SwaggerDocs
- Caching -> CachingDecorator and reading todos from cache. Just replacing the DI to caching decorator
- Paging https://github.com/Biarity/Sieve/blob/master/README.md#send-a-request
  e.g api/v2/Todo/GetWithFilerAndSortV2?Page=1&PageSize=3
- Filtering https://github.com/Biarity/Sieve/blob/master/README.md#send-a-request
  e.g. api/v2/Todo/GetWithFilerAndSortV2?Filters=CategoryId==2
- AutoMapper to map values of data to view models -  #AutoMapper
  https://code-maze.com/automapper-net-core/#configuration
- #FluentValidaton to applying validation to model and all properties 
https://medium.com/@sergiobarriel/how-to-automatically-validate-a-model-with-mvc-filter-and-fluent-validation-package-ae51098bcf5b

- Hypermedia (HATAEOS)

- Logging: #SerilogConfiguration   Using Serilog to replace default Microsoft Logging. Serilog has so many sinks that can be added dynamically e.g. send the logs to UDP port and using Log2Console see the logs dynamically.