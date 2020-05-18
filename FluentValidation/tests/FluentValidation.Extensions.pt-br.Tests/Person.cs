// Copyright 2020 Polimorfismo - José Mauro da Silva Sandy
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace FluentValidation.Tests
{
    internal class Person
    {
        public string CPF { get; }

        public string CNPJ { get; set; }

        public Person(string cpf = "12312312387", string cnpj = "51673426000147")
        {
            CPF = cpf;
            CNPJ = cnpj;
        }
    }

    internal class PersonValidator : AbstractValidator<Person>
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
}