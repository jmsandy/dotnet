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

using FluentValidation.Validators;

namespace FluentValidation
{
    /// <summary>
    /// Extension class to apply the CPF and CNPJ validation rules in FluentValidation.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>2020-05-17 11:30:01</Date>
    public static class DefaultValidatorExtensions
    {
        /// <summary>
        /// Applies the CPF validator in the current rules.
        /// Validation will fail if the property is: 
        ///     i) empty; 
        ///     ii) void; 
        ///     iii) invalid length; 
        ///     iv) invalid mask; 
        ///     v) invalid characters; and 
        ///     vi) invalid CPF. 
        /// In general, only CPFs with valid number values ​​or with mask 
        /// and valid value are accepted.
        /// </summary>
        /// <typeparam name="T">type of object being validated</typeparam>
        /// <param name="ruleBuilder">rule builder for adding CPF validation.</param>
        /// <returns>rule builder with CPF validation included.</returns>
        public static IRuleBuilderOptions<T, string> IsValidCPF<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CPFValidator());
        }

        /// <summary>
        /// Applies the CNPJ validator in the current rules.
        /// Validation will fail if the property is: 
        ///     i) empty; 
        ///     ii) void; 
        ///     iii) invalid length; 
        ///     iv) invalid mask; 
        ///     v) invalid characters; and 
        ///     vi) invalid CNPJ. 
        /// In general, only CNPJs with valid number values ​​or with mask 
        /// and valid value are accepted.
        /// </summary>
        /// <typeparam name="T">type of object being validated</typeparam>
        /// <param name="ruleBuilder">rule builder for adding CNPJ validation.</param>
        /// <returns>rule builder with CNPJ validation included.</returns>
        public static IRuleBuilderOptions<T, string> IsValidCNPJ<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CNPJValidator());
        }
    }
}
