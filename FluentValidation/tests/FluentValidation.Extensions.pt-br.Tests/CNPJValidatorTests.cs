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
    public class CNPJValidatorTests
    {
        private readonly DocumentsFixture _documentsFixture;

        public CNPJValidatorTests(DocumentsFixture documentsFixture)
        {
            _documentsFixture = documentsFixture;
        }

        [Fact(DisplayName = "Should Fail When All Digits Are Equals")]
        [Trait("Validator", "CNPJ Validator")]
        public void CNPJValidator_Validate_ShouldFailWhenAllDigitsEquals()
        {
            // Arrange
            var validator = new PersonValidator();

            // Act 
            var cnpjList = _documentsFixture.GetCNPJDigitsEquals();

            // Assert
            cnpjList.ToList().ForEach(cnpj => validator.Validate(new Person(cnpj: cnpj)).IsValid.Should().BeFalse());
        }

        [Fact(DisplayName = "Should Fail When All Digits Are Equals (Async)")]
        [Trait("Validator", "CNPJ Validator")]
        public void CNPJValidator_Validate_ShouldFailWhenAllDigitsEqualsAsync()
        {
            // Arrange
            var validator = new PersonValidator();

            // Act 
            var cnpjList = _documentsFixture.GetCNPJDigitsEquals();

            // Assert
            cnpjList.ToList().ForEach(async cnpj => (await validator.ValidateAsync(new Person(cnpj: cnpj))).IsValid.Should().BeFalse());
        }

        [Fact(DisplayName = "Should Fail When Is Null Or Empty")]
        [Trait("Validator", "CNPJ Validator")]
        public void CNPJValidator_Validate_ShouldFailWhenIsNullOrEmpty()
        {
            // Arrange
            var validator = new PersonValidator();
            var personCnpjEmpty = new Person(cnpj: "");
            var personCnpjNull = new Person(cnpj: null);

            // Act & Assert
            validator.Validate(personCnpjEmpty).IsValid.Should().BeFalse();
            validator.Validate(personCnpjNull).IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Fail When Is Null Or Empty (Async)")]
        [Trait("Validator", "CNPJ Validator")]
        public async Task CNPJValidator_Validate_ShouldFailWhenIsNullOrEmptyAsync()
        {
            // Arrange
            var validator = new PersonValidator();
            var personCnpjEmpty = new Person(cnpj: "");
            var personCnpjNull = new Person(cnpj: null);

            // Act
            var resultIsEmpty = await validator.ValidateAsync(personCnpjEmpty);
            var resultIsNull = await validator.ValidateAsync(personCnpjNull);

            // Assert
            resultIsEmpty.IsValid.Should().BeFalse();
            resultIsNull.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Should Fail When Invalid Length")]
        [Trait("Validator", "CNPJ Validator")]
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
        [InlineData("12345678901")]
        [InlineData("123456789012")]
        [InlineData("1234567890123")]
        [InlineData("123456789012345")]
        public void CNPJValidator_Validate_ShouldFailWhenInvalidLength(string cnpj)
        {
            // Arrange
            var validator = new PersonValidator();
            var person = new Person(cnpj: cnpj);

            // Act & Asset
            validator.Validate(person).IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Should Fail When Invalid Length (Async)")]
        [Trait("Validator", "CNPJ Validator")]
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
        [InlineData("12345678901")]
        [InlineData("123456789012")]
        [InlineData("1234567890123")]
        [InlineData("123456789012345")]
        public async Task CNPJValidator_Validate_ShouldFailWhenInvalidLengthAsync(string cnpj)
        {
            // Arrange
            var validator = new PersonValidator();
            var person = new Person(cnpj: cnpj);

            // Act
            var result = await validator.ValidateAsync(person);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Should Fail When Invalid Value")]
        [Trait("Validator", "CNPJ Validator")]
        [InlineData("51673426000148")]
        [InlineData("516734260B0147")]
        [InlineData("51.673.426/0001.48")]
        [InlineData("51.673.426/0B01-47")]
        public void CNPJValidator_Validate_ShouldFailWhenInvalidValue(string cnpj)
        {
            // Arrange
            var validator = new PersonValidator();
            var person = new Person(cnpj: cnpj);

            // Act & Asset
            validator.Validate(person).IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Should Fail When Invalid Value (Async)")]
        [Trait("Validator", "CNPJ Validator")]
        [InlineData("51673426000148")]
        [InlineData("516734260B0147")]
        [InlineData("51.673.426/0001.48")]
        [InlineData("51.673.426/0B01-47")]
        public async Task CNPJValidator_Validate_ShouldFailWhenInvalidValueAsync(string cnpj)
        {
            // Arrange
            var validator = new PersonValidator();
            var person = new Person(cnpj: cnpj);

            // Act
            var result = await validator.ValidateAsync(person);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "CNPJ Is Valid")]
        [Trait("Validator", "CNPJ Validator")]
        public void CNPJValidator_Validate_CNPJIsValid()
        {
            // Arrange
            var validator = new PersonValidator();

            // Act
            var cnpjList = _documentsFixture.GetValidCNPJ(10, true);
            cnpjList.AddRange(_documentsFixture.GetValidCNPJ(10, false));

            // Assert
            cnpjList.ToList().ForEach(cnpj => validator.Validate(new Person(cnpj: cnpj)).IsValid.Should().BeTrue());
        }

        [Fact(DisplayName = "CNPJ Is Valid (Async)")]
        [Trait("Validator", "CNPJ Validator")]
        public void CNPJValidator_Validate_CNPJIsValidAsync()
        {
            // Arrange
            var validator = new PersonValidator();

            // Act
            var cnpjList = _documentsFixture.GetValidCNPJ(10, true);
            cnpjList.AddRange(_documentsFixture.GetValidCNPJ(10, false));

            // Assert
            cnpjList.ToList().ForEach(async cnpj => (await validator.ValidateAsync(new Person(cnpj: cnpj))).IsValid.Should().BeTrue());
        }

        [Fact(DisplayName = "Should Fail With Custom Message")]
        [Trait("Validator", "CNPJ Validator")]
        public void CNPJValidator_Validate_ShouldFailWhenCustomMessage()
        {
            // Arrange
            var validator = new PersonValidator("{PropertyName} invalid!", "{PropertyName} invalid!");
            var person = new Person("516734260B0147");

            // Act 
            var result = validator.Validate(person);

            // Assert
            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Equals("CNPJ invalid!");
        }

        [Fact(DisplayName = "Should Fail With Custom Message (Async)")]
        [Trait("Validator", "CNPJ Validator")]
        public async Task CNPJValidator_Validate_ShouldFailWhenCustomMessageAsync()
        {
            // Arrange
            var validator = new PersonValidator("{PropertyName} invalid!", "{PropertyName} invalid!");
            var person = new Person("516734260B0147");

            // Act
            var result = await validator.ValidateAsync(person);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Equals("CNPJ invalid!");
        }
    }
}