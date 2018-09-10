# crusha

## Description
This library is written as middleware so you can easily add an upload server to your project. This is not intended for production uses, but is more of a local dubugging tool

The middleware will create an `uploadedFiles ` directory on your root path and copy any files there.

The middleware will also prepend the ` DateTime.UtcNow.Ticks ` to the file name. 

## Development requirments
* [dotnet sdk 2.1.101](https://www.microsoft.com/net/download)

## Use
Add ` app.UseUploadServer(); ` to your Configure method in the startup.cs file.

For Example:

```
// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMvc();
    app.UseUploadServer();
}
```

When you run your server, post your files to this endpoint ` /uploadFiles `
## ToDo

- [ ] Create Test Files / Project
- [ ] Create nuget package
- [x] Add CI/CD server
- [ ] Add error handling
- [ ] Publish to nuget.org
