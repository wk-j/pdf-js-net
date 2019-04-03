## PDF .NET

[![NuGet](https://img.shields.io/nuget/v/wk.PdfJsNet.svg)](https://www.nuget.org/packages/wk.PdfJsNet)

Embed pdf.js inside .NET DLL

## Installation

```bash
dotnet add package wk.PdfJsNet
```

## Usage

Load pdf.js with EmbeddedFileProvider

```csharp
var asm = typeof(Viewer).Assembly;
var asmName = asm.GetName().Name;
var defaultOptions = new DefaultFilesOptions();
defaultOptions.DefaultFileNames.Clear();
defaultOptions.DefaultFileNames.Add("index.html");

var wwwroot = new PhysicalFileProvider(env.WebRootPath);
var compositeProvider = new CompositeFileProvider(wwwroot, new EmbeddedFileProvider(asm, $"{asmName}.viewer"));

app
  .UseDefaultFiles(defaultOptions)
  .UseStaticFiles(new StaticFileOptions {
      FileProvider = compositeProvider
  });
```

Test

```bash
open http://localhost:5000/web/viewer.html?file=/001.pdf
```
