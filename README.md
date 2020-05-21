# dotnet
Project with extensions to simplify the development of .NET applications.

## FluentValidation.Extensions.pt-br

FluentValidation.Extensions.pt-br extension to validate rules applied exclusively in Brazilian scenarios.

### Get Started
FluentValidation.Extensions.pt-br can be installed using the Nuget package manager or the `dotnet` CLI.

```
dotnet add package Polimorfismo.FluentValidation.Extensions.pt-br
```
---
[![Build Status](https://dev.azure.com/jmsandy/FluentValidation.Extensions.pt-br/_apis/build/status/jmsandy.dotnet?branchName=master)](https://dev.azure.com/jmsandy/FluentValidation.Extensions.pt-br/_build/latest?definitionId=1&branchName=master)

|         |       |       |
| ------- | ----- | ----- |
| `FluentValidation.Extensions.pt-br` | [![NuGet](https://img.shields.io/nuget/v/Polimorfismo.FluentValidation.Extensions.pt-br.svg)](https://www.nuget.org/packages/Polimorfismo.FluentValidation.Extensions.pt-br) | [![Nuget](https://img.shields.io/nuget/dt/Polimorfismo.FluentValidation.Extensions.pt-br.svg)](https://nuget.org/packages/Polimorfismo.FluentValidation.Extensions.pt-br) 

### Example
```csharp
using FluentValidation;

public class PersonValidator : AbstractValidator<Person> 
{
    public PersonValidator()
    {
        RuleFor(p => p.CPF).IsValidCPF();
        RuleFor(p => p.CNPJ).IsValidCNPJ();
    }

    public PersonValidator(string messageCpf = null, string messageCnpj = null)
    {
        RuleFor(p => p.CPF).IsValidCPF().WithMessage(messageCpf);
        RuleFor(p => p.CNPJ).IsValidCNPJ().WithMessage(messageCnpj);
    }
}

var validator = new PersonValidator();
var results = validator.Validate(new Person("123.123.123-87"));

var success = results.IsValid;
var failures = results.Errors;
```
### License, Copyright etc

FluentValidation is copyright &copy; 2020 Polimorfismo - José Mauro da Silva Sandy and other contributors and is licensed under the [Apache2 license](https://github.com/jmsandy/dotnet/blob/master/LICENSE). 
