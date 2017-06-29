# ScanlessDotNet

A c# port of scanless by @vesche, a python script for port scanning using existing online services, you can see the original here https://github.com/vesche/scanless. 

The output of this project is in a library format that can be used in new or existing .NET projects. 

This .dll can also be used to create a simple online port scanning API using [azure functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference-csharp#referencing-custom-assemblies). 

## Usage

Import scanless lib
``` c#
using Scanless;
using Scanless.Scanners;
```

Then pass the target to be scanned to one of the scanner classes. See the folder "scanners" for more.
```c#
static void Main(string[] args)
{
    string target = "http://scanme.nmap.org";
    var scanner = new hackertarget(target);
    scanner.Run();
    Console.WriteLine(scanner.Content);//TODO: Parse request response
    Console.WriteLine("--------");
    Console.Read();
}
```


## Build / Dependencies

The project was built against the .NET framework 4.6 using VS Studio 2017. It depends on the .NET framework and will require the relevant version to be installed in order to be used.

## TODO

[ ] Complete design for output parsing to pretty print open / closed ports

_Please note the library is not practical to use as-is without the above functionality completed. Until then, you can implement your own parsing logic. See below._

## Contribute and extend

If you want to contribute or add, you can find the core functionality to build a new scanner abstracted into the classes `Request` and `ScanScript`. 

To add your own HTML / response parsing logic the method `SetResponse` implemented by all scanners is the perfect place to do this.
```c#

public override void SetResponse(string result)
{
    base.SetResponse(result);
    //Process result
    var cleaned = Clean(result);
    Console.WriteLine(cleaned);
    //Store as a class variable
    this.Result = cleaned;
}

public string Clean(string toclean)
{
  //magical parsing happens here
}
```

