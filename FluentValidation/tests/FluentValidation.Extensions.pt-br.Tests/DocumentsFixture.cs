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

using Bogus;
using Xunit;
using System;
using System.Threading.Tasks;
using Bogus.Extensions.Brazil;
using System.Collections.Generic;

namespace FluentValidation.Tests
{
    [CollectionDefinition(nameof(DocumentsCollection))]
    public class DocumentsCollection : ICollectionFixture<DocumentsFixture>
    {
    }

    public class DocumentsFixture : IDisposable
    {
        private readonly byte CPF_SIZE = 11;
        private readonly byte CNPJ_SIZE = 14;

        private List<string> GetDocumentsDigitsEquals(byte size)
        {
            var documentList = new List<string>();

            Parallel.For(0, 10, (i) =>
            {
                documentList.Add("".PadRight(size, Convert.ToChar(i.ToString())));
            });

            return documentList;
        }

        public List<string> GetCPFDigitsEquals()
        {
            return GetDocumentsDigitsEquals(CPF_SIZE);
        }

        public List<string> GetCNPJDigitsEquals()
        {
            return GetDocumentsDigitsEquals(CNPJ_SIZE);
        }

        public List<string> GetValidCPF(int size, bool withMask)
        {
            var cpf = new Faker<string>()
                .CustomInstantiator(f => new string(f.Person.Cpf(withMask)));

            return cpf.Generate(size);
        }

        public List<string> GetValidCNPJ(int size, bool withMask)
        {
            var cnpj = new Faker<string>()
                .CustomInstantiator(f => new string(f.Company.Cnpj(withMask)));

            return cnpj.Generate(size);
        }

        public void Dispose()
        {
        }
    }
}
