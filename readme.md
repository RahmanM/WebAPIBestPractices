# Web API Best Practices

- Async conrollers with Async queries
- Lazyloading in DBContext #lazyloading
- DI of the context to Controllers per request #injectingcontext
- Global Exception handlers instead of repeating same code more and more #globalexceptionhandler
- Model validation action filters to validate models on each request automatically #ModelValidationActionFilter
- Content negociation e.g for XML #ContentNegociation
- API versioning #ApiVersioning
  https://dev.to/htissink/versioning-asp-net-core-apis-with-swashbuckle-making-space-potatoes-v-x-x-x-3po7
- API Automatic Documentaion using Swagger #SwaggerDocs
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