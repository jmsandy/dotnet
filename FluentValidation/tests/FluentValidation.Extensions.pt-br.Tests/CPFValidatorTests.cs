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
    public class CPFValidatorTests
    {
        [Fact]
        public void Validation_cpf_should_fail_when_all_digits_equals()
        {
            var validator = new PersonValidator();
            Parallel.For(0, 10, (i) =>
            {
                validator.Validate(new Person("".PadRight(11, Convert.ToChar(i)))).IsValid.ShouldBeFalse();
            });
        }

        [Fact]
        public void Validation_cpf_should_fail_when_async_all_digits_equals()
        {
            var validator = new PersonValidator();
            Parallel.For(0, 10, async (i) =>
            {
                var result = await validator.ValidateAsync(new Person("".PadRight(11, Convert.ToChar(i))));
                result.IsValid.ShouldBeFalse();
            });
        }

        [Fact]
        public void Validation_cpf_should_fail_when_is_null_or_empty()
        {
            var validator = new PersonValidator();
            validator.Validate(new Person("")).IsValid.ShouldBeFalse();
            validator.Validate(new Person(null)).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Validation_cpf_should_fail_when_async_is_null_or_empty()
        {
            var validator = new PersonValidator();
            var result = await validator.ValidateAsync(new Person(""));
            result.IsValid.ShouldBeFalse();

            result = await validator.ValidateAsync(new Person(null));
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validation_cpf_should_fail_when_invalid_length()
        {
            var validator = new PersonValidator();
            validator.Validate(new Person("123123")).IsValid.ShouldBeFalse();
            validator.Validate(new Person("123123123871")).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Validation_cpf_should_fail_when_async_invalid_length()
        {
            var validator = new PersonValidator();
            var result = await validator.ValidateAsync(new Person("123123"));
            result.IsValid.ShouldBeFalse();

            result = await validator.ValidateAsync(new Person("123123123871"));
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validation_cpf_should_fail_when_invalid_value()
        {
            var validator = new PersonValidator();
            validator.Validate(new Person("12312312E87")).IsValid.ShouldBeFalse();
            validator.Validate(new Person("123123123B7")).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Validation_cpf_should_fail_when_async_invalid_value()
        {
            var validator = new PersonValidator();
            var result = await validator.ValidateAsync(new Person("123.123.123..87"));
            result.IsValid.ShouldBeFalse();

            result = await validator.ValidateAsync(new Person("123123123B7"));
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validation_cpf_should_succeed()
        {
            var validator = new PersonValidator();
            validator.Validate(new Person("123.123.123-87")).IsValid.ShouldBeTrue();
            validator.Validate(new Person("66087661069")).IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task Validation_cpf_should_succeed_async()
        {
            var validator = new PersonValidator();
            var result = await validator.ValidateAsync(new Person("12312312387"));
            result.IsValid.ShouldBeTrue();

            result = await validator.ValidateAsync(new Person("660.876.610-69"));
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validation_cpf_should_fail_when_custom_message()
        {
            var validator = new PersonValidator("{PropertyName} invalid!", "{PropertyName} invalid!");
            validator.Validate(new Person("1231231C387")).Errors[0].ErrorMessage.ShouldEqual("CPF invalid!");
        }

        [Fact]
        public async Task Validation_cpf_should_fail_when_async_custom_message()
        {
            var validator = new PersonValidator("{PropertyName} invalid!", "{PropertyName} invalid!");
            var result = await validator.ValidateAsync(new Person("1231231C387"));
            result.Errors[0].ErrorMessage.ShouldEqual("CPF invalid!");
        }
    }
}