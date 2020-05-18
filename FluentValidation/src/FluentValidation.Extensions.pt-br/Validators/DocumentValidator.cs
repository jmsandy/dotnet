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

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FluentValidation.Validators
{
    /// <summary>
    /// Base class for document validation (CPF / CNPJ).
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>2020-05-17 10:21:54</Date>
    public abstract class DocumentValidator : PropertyValidator
    {
        /// <summary>
        /// Default mask of the document to be validated.
        /// </summary>
        private readonly string _mask;

        /// <summary>
        /// Maximum length of the document to be validated.
        /// </summary>
        private readonly int _documentLength;

        /// <summary>
        /// First multiplication vector for calculating the module.
        /// </summary>
        private readonly int[] _firstCollection;

        /// <summary>
        /// Second multiplication vector for calculating the module.
        /// </summary>
        private readonly int[] _secondCollection;

        /// <summary>
        /// Document validator.
        /// </summary>
        /// <param name="mask">default mask of the document to be validated.</param>
        /// <param name="documentLength">maximum length of the document to be validated.</param>
        /// <param name="errorMessage">error message used for cases in which validation fails.</param>
        /// <param name="firstCollection">first multiplication vector for calculating the module.</param>
        /// <param name="secondCollection">second multiplication vector for calculating the module.</param>
        protected DocumentValidator(string mask,
                                    int documentLength,
                                    string errorMessage,
                                    int[] firstCollection,
                                    int[] secondCollection)
            : base(errorMessage)
        {
            _mask = mask;
            _documentLength = documentLength;
            _firstCollection = firstCollection;
            _secondCollection = secondCollection;
        }

        /// <summary>
        /// Verify that the received property represents a valid document.
        /// </summary>
        /// <param name="context">rules context.</param>
        /// <returns>true if valid, false otherwise.</returns>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string ?? "";

            if (IsValidFormat(value))
            {
                value = OnlyDigits(value);
                var document = value.Select(c => (int)char.GetNumericValue(c)).ToArray();
                var digits = GetDigits(document);

                return value.EndsWith(digits);
            }

            return false;
        }

        /// <summary>
        /// Verify that the received property represents a valid document.
        /// </summary>
        /// <param name="context">rules context.</param>
        /// <param name="cancellation">cancelation task.</param>
        /// <returns>execution task.</returns>
        protected override Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            return new Task<bool>(() => { return IsValid(context); }, cancellation);
        }

        /// <summary>
        /// Checks if the received document meets all the rules necessary for its validation.
        /// </summary>
        /// <param name="value">document.</param>
        /// <returns>true if valid, false otherwise.</returns>
        protected virtual bool IsValidFormat(string value)
        {
            if (IsOnlyDigits(value) || IsValidMask(value))
            {
                value = OnlyDigits(value);
                return IsValidLength(value) && !AllDigitsAreEquals(value);
            }

            return false;
        }

        /// <summary>
        /// Gets only the digits presents in the received document.
        /// </summary>
        /// <param name="value">document.</param>
        /// <returns></returns>
        protected string OnlyDigits(string value)
        {
            return Regex.Replace(value, @"[^\d]", "");
        }

        /// <summary>
        /// Checks if the received document has only digits.
        /// </summary>
        /// <param name="value">document.</param>
        /// <returns>true if valid, false otherwise.</returns>
        protected bool IsOnlyDigits(string value)
        {
            return Regex.IsMatch(value, @"^[\d]*$");
        }

        /// <summary>
        /// Checks if the mask associated with the received document is valid.
        /// </summary>
        /// <param name="value">document.</param>
        /// <returns>true if valid, false otherwise.</returns>
        protected bool IsValidMask(string value)
        {
            return Regex.IsMatch(value, _mask);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>true if valid, false otherwise.</returns>
        protected bool IsValidLength(string value)
        {
            return (value ?? "").Trim().Length == _documentLength;
        }

        /// <summary>
        /// Checks if the length of the received document is valid.
        /// </summary>
        /// <param name="value">document.</param>
        /// <returns>true if valid, false otherwise.</returns>
        protected bool AllDigitsAreEquals(string value)
        {
            return value.All(c => c == value.FirstOrDefault());
        }

        /// <summary>
        /// Gets the check digits of the received document based on the multiplication vectors.
        /// </summary>
        /// <param name="document">document.</param>
        /// <returns>check digits.</returns>
        private string GetDigits(int[] document)
        {
            var firstDigit = Sum(_firstCollection, document);
            var secondDigit = Sum(_secondCollection, document);

            return $"{Mod11(firstDigit)}{Mod11(secondDigit)}";
        }

        /// <summary>
        /// Performs the sum of the product of the multiplication vector (weights) with the received document.
        /// </summary>
        /// <param name="weight">multiplication vectors.</param>
        /// <param name="document">document.</param>
        /// <returns>sum.</returns>
        private int Sum(int[] weight, int[] document)
        {
            var i = 0;
            return weight.Sum(w => w * document[i++]);
        }

        /// <summary>
        /// Applying module 11 to calculate the check digit.
        /// </summary>
        /// <param name="sum">sum (weights * document)</param>
        /// <returns>check digit</returns>
        private int Mod11(int sum)
        {
            var result = (sum % 11);
            return result < 2 ? 0 : 11 - result;
        }
    }
}