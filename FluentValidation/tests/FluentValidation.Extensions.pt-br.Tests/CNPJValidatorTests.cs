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

using Xunit;
using System;
using System.Threading.Tasks;

namespace FluentValidation.Tests
{
    public class CNPJValidatorTests
    {
        [Fact]
        public void Validation_cnpj_should_fail_when_all_digits_equals()
        {
            var validator = new PersonValidator();
            Parallel.For(0, 10, (i) =>
            {
                validator.Validate(new Person(cnpj: "".PadRight(14, Convert.ToChar(i)))).IsValid.ShouldBeFalse();
            });
        }

        [Fact]
        public void Validation_cnpj_should_fail_when_async_all_digits_equals()
        {
            var validator = new PersonValidator();
            Parallel.For(0, 10, async (i) =>
            {
                var result = await validator.ValidateAsync(new Person(cnpj: "".PadRight(14, Convert.ToChar(i))));
                result.IsValid.ShouldBeFalse();
            });
        }

        [Fact]
        public void Validation_cnpj_should_fail_when_is_null_or_empty()
        {
            var validator = new PersonValidator();
            validator.Validate(new Person(cnpj: "")).IsValid.ShouldBeFalse();
            validator.Validate(new Person(cnpj: null)).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Validation_cnpj_should_fail_when_async_is_null_or_empty()
        {
            var validator = new PersonValidator();
            var result = await validator.ValidateAsync(new Person(cnpj: ""));
            result.IsValid.ShouldBeFalse();

            result = await validator.ValidateAsync(new Person(cnpj: null));
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validation_cnpj_should_fail_when_invalid_length()
        {
            var validator = new PersonValidator();
            validator.Validate(new Person(cnpj: "5167342600014")).IsValid.ShouldBeFalse();
            validator.Validate(new Person(cnpj: "516734260001474")).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Validation_cnpj_should_fail_when_async_invalid_length()
        {
            var validator = new PersonValidator();
            var result = await validator.ValidateAsync(new Person(cnpj: "5167342600014"));
            result.IsValid.ShouldBeFalse();

            result = await validator.ValidateAsync(new Person(cnpj: "516734260001474"));
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validation_cnpj_should_fail_when_invalid_value()
        {
            var validator = new PersonValidator();
            validator.Validate(new Person(cnpj: "51.673.426/0001.47")).IsValid.ShouldBeFalse();
            validator.Validate(new Person(cnpj: "51.673.426/0B01-47")).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Validation_cnpj_should_fail_when_async_invalid_value()
        {
            var validator = new PersonValidator();
            var result = await validator.ValidateAsync(new Person(cnpj: "51.673.426/0001.47"));
            result.IsValid.ShouldBeFalse();

            result = await validator.ValidateAsync(new Person(cnpj: "51.673.426/0B01-47"));
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validation_cnpj_should_succeed()
        {
            var validator = new PersonValidator();
            validator.Validate(new Person(cnpj: "51673426000147")).IsValid.ShouldBeTrue();
            validator.Validate(new Person(cnpj: "61.920.291/0001-20")).IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task Validation_cnpj_should_succeed_async()
        {
            var validator = new PersonValidator();
            var result = await validator.ValidateAsync(new Person(cnpj: "51.673.426/0001-47"));
            result.IsValid.ShouldBeTrue();

            result = await validator.ValidateAsync(new Person(cnpj: "61920291000120"));
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validation_cnpj_should_fail_when_custom_message()
        {
            var validator = new PersonValidator("{PropertyName} invalid!", "{PropertyName} invalid!");
            validator.Validate(new Person(cnpj: "6!920291000120")).Errors[0].ErrorMessage.ShouldEqual("CNPJ invalid!");
        }

        [Fact]
        public async Task Validation_cnpj_should_fail_when_async_custom_message()
        {
            var validator = new PersonValidator("{PropertyName} invalid!", "{PropertyName} invalid!");
            var result = await validator.ValidateAsync(new Person(cnpj: "6!920291000120"));
            result.Errors[0].ErrorMessage.ShouldEqual("CNPJ invalid!");
        }
    }
}