# NSwaggerMerge
Merge OpenApi 3.0 documents from different services under the microservice architecture.

#### Motivation

In a Microservices architecture, using an API Gateway, the various services are hidden behind the gateway and each individual swagger doc is not exposed.
Front-end devs using an OpenApi codegen tool, need a single unified OpenApi doc for all services behind the gateway.

#### Example appsettings.json configuration

```json
{
  ...
  "SwaggerOutputConfig-v1": {
    "Info": {
      "Title": "Admin Portal API",
      "Version": "1.0"
    },
    "Schemes": [
      "https"
    ],
    "Security": [ {} ]
  },
  "SwaggerInputConfig-v1": [
    {
      "File": "https://localhost:5001/swagger/v1/swagger.json"
    },
    {
      "File": "https://localhost:5002/swagger/v1/swagger.json"
    },
    {
      "File": "https://localhost:5003/swagger/v1/swagger.json"
    },
    {
      "File": "https://localhost:5004/swagger/v1/swagger.json"
    },
    {
      "File": "https://localhost:5005/swagger/v1/swagger.json"
    }
  ]
  ...
}
```

#### Registations

```cs
...
builder.Services.RegisterCommonServices(configuration);

var swaggerInputConfig = builder.Configuration
    .GetSection("SwaggerInputConfig-v1")
    .Get<SwaggerInputConfiguration[]>();

builder.Services.AddSingleton(swaggerInputConfig);

builder.Services.AddOptions<SwaggerOutputConfiguration>()
    .BindConfiguration("SwaggerOutputConfig-v1")
    .ValidateDataAnnotations()
    .ValidateOnStart();
...
```

#### How To Use

```cs
...
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Admin Portal API";
        options.SwaggerEndpoint("/admin-portal-api/v1/swagger.json", "Admin Portal API v1");

        options.RoutePrefix = string.Empty;
        options.EnableTryItOutByDefault();
        options.DisplayRequestDuration();
        options.ShowCommonExtensions();
        options.ShowExtensions();
    });

    app.MapGet("/admin-portal-api/v1/swagger.json", async (SwaggerInputConfiguration[] swaggerInputConfig, IOptions<SwaggerOutputConfiguration> swaggerOutputOptions) =>
    {
        SwaggerOutputConfiguration swaggerOutputConfig = swaggerOutputOptions.Value;

        SwaggerMergeConfiguration config = new()
        {
            Inputs = swaggerInputConfig,
            Output = swaggerOutputConfig
        };

        SwaggerMerger.ValidateConfiguration(config);

        return await SwaggerMerger.MergeAsync(config);
    }).AllowAnonymous().Produces<ActionResult>(contentType: System.Net.Mime.MediaTypeNames.Application.Json);
}
...
```

The end result looks like this:

![Merge Swagger Endpoint](https://github.com/npapadioxos/NSwaggerMerge/raw/master/assets/img/readme-result-endpoint.png)

Upon sending a request to this endpoint, NSwaggerMerge will merge into a single doc all OpenApi docs from services configured in appsettings (services must be running).

#### Credits

Building upon the following 2 tools:

* [swagger-merge](https://github.com/jamesmcroft/swagger-merge)
* [Noise.SwaggerMerge](https://github.com/vtbmusic/Noise.SwaggerMerge)
