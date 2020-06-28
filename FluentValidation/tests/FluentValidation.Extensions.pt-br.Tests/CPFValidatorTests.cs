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
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;

namespace FluentValidation.Tests
{
    [Collection(nameof(DocumentsCollection))]
    public class CPFValidatorTests
    {
        private readonly DocumentsFixture _documentsFixture;

        public CPFValidatorTests(DocumentsFixture documentsFixture)
        {
            _documentsFixture = documentsFixture;
        }

        [Fact(DisplayName = "Should Fail When All Digits Are Equals")]
        [Trait("Validator", "CPF Validator")]
        public void CPFValidator_Validate_ShouldFailWhenAllDigitsEquals()
        {
            // Arrange
            var validator = new PersonValidator();

            // Act 
            var cpfList = _documentsFixture.GetCPFDigitsEquals();

            // Assert
            cpfList.ToList().ForEach(cpf => validator.Validate(new Person(cpf)).IsValid.Should().BeFalse());
        }

        [Fact(DisplayName = "Should Fail When All Digits Are Equals (Async)")]
        [Trait("Validator", "CPF Validator")]
        public void CPFValidator_Validate_ShouldFailWhenAllDigitsEqualsAsync()
        {
            // Arrange
            var validator = new PersonValidator();

            // Act 
            var cpfList = _documentsFixture.GetCPFDigitsEquals();

            // Assert
            cpfList.ToList().ForEach(async cpf => (await validator.ValidateAsync(new Person(cpf))).IsValid.Should().BeFalse());
        }

        [Fact(DisplayName = "Should Fail When Is Null Or Empty")]
        [Trait("Validator", "CPF Validator")]
        public void CPFValidator_Validate_ShouldFailWhenIsNullOrEmpty()
        {
            // Arrange
            var validator = new PersonValidator();
            var personCpfEmpty = new Person("");
            var personCpfNull = new Person(null);

            // Act & Assert
            validator.Validate(personCpfEmpty).IsValid.Should().BeFalse();
            validator.Validate(personCpfNull).IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Fail When Is Null Or Empty (Async)")]
        [Trait("Validator", "CPF Validator")]
        public async Task CPFValidator_Validate_ShouldFailWhenIsNullOrEmptyAsync()
        {
            // Arrange
            var validator = new PersonValidator();
            var personCpfEmpty = new Person("");
            var personCpfNull = new Person(null);

            // Act
            var resultIsEmpty = await validator.ValidateAsync(personCpfEmpty);
            var resultIsNull = await validator.ValidateAsync(personCpfNull);

            // Assert
            resultIsEmpty.IsValid.Should().BeFalse();
            resultIsNull.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Should Fail When Invalid Length")]
        [Trait("Validator", "CPF Validator")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        [InlineData("1234567")]
        [InlineData("12345678")]
        [InlineData("123456789")]
        [InlineData("1234567890")]
        [InlineData("123456789011")]
        public void CPFValidator_Validate_ShouldFailWhenInvalidLength(string cpf)
        {
            // Arrange
            var validator = new PersonValidator();
            var person = new Person(cpf);

            // Act & Asset
            validator.Validate(person).IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Should Fail When Invalid Length (Async)")]
        [Trait("Validator", "CPF Validator")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        [InlineData("1234567")]
        [InlineData("12345678")]
        [InlineData("123456789")]
        [InlineData("1234567890")]
        [InlineData("123456789011")]
        public async Task CPFValidator_Validate_ShouldFailWhenInvalidLengthAsync(string cpf)
        {
            // Arrange
            var validator = new PersonValidator();
            var person = new Person(cpf);

            // Act
            var result = await validator.ValidateAsync(person);
            
            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Should Fail When Invalid Value")]
        [Trait("Validator", "CPF Validator")]
        [InlineData("12312312E87")]
        [InlineData("123123123B7")]
        [InlineData("123.123.123..87")]
        [InlineData("72186126500")]
        public void CPFValidator_Validate_ShouldFailWhenInvalidValue(string cpf)
        {
            // Arrange
            var validator = new PersonValidator();
            var person = new Person(cpf);

            // Act & Asset
            validator.Validate(person).IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Should Fail When Invalid Value (Async)")]
        [Trait("Validator", "CPF Validator")]
        [InlineData("12312312E87")]
        [InlineData("123123123B7")]
        [InlineData("123.123.123..87")]
        [InlineData("72186126500")]
        public async Task CPFValidator_Validate_ShouldFailWhenInvalidValueAsync(string cpf)
        {
            // Arrange
            var validator = new PersonValidator();
            var person = new Person(cpf);

            // Act
            var result = await validator.ValidateAsync(person);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "CPF Is Valid")]
        [Trait("Validator", "CPF Validator")]
        public void CPFValidator_Validate_CPFIsValid()
        {
            // Arrange
            var validator = new PersonValidator();

            // Act
            var cpfList = _documentsFixture.GetValidCPF(10, true);
            cpfList.AddRange(_documentsFixture.GetValidCPF(10, false));

            // Assert
            cpfList.ToList().ForEach(cpf => validator.Validate(new Person(cpf)).IsValid.Should().BeTrue());
        }

        [Fact(DisplayName = "CPF Is Valid (Async)")]
        [Trait("Validator", "CPF Validator")]
        public void CPFValidator_Validate_CPFIsValidAsync()
        {
            // Arrange
            var validator = new PersonValidator();

            // Act
            var cpfList = _documentsFixture.GetValidCPF(10, true);
            cpfList.AddRange(_documentsFixture.GetValidCPF(10, false));

            // Assert
            cpfList.ToList().ForEach(async cpf => (await validator.ValidateAsync(new Person(cpf))).IsValid.Should().BeTrue());
        }

        [Fact(DisplayName = "Should Fail With Custom Message")]
        [Trait("Validator", "CPF Validator")]
        public void CPFValidator_Validate_ShouldFailWhenCustomMessage()
        {
            // Arrange
            var validator = new PersonValidator("{PropertyName} invalid!", "{PropertyName} invalid!");
            var person = new Person("1231231C387");

            // Act 
            var result = validator.Validate(person);

            // Assert
            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Equals("CPF invalid!");
        }

        [Fact(DisplayName = "Should Fail With Custom Message (Async)")]
        [Trait("Validator", "CPF Validator")]
        public async Task CPFValidator_Validate_ShouldFailWhenCustomMessageAsync()
        {
            // Arrange
            var validator = new PersonValidator("{PropertyName} invalid!", "{PropertyName} invalid!");
            var person = new Person("1231231C387");

            // Act
            var result = await validator.ValidateAsync(person);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Equals("CPF invalid!");
        }
    }
}