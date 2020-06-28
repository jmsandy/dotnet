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

namespace FluentValidation.Validators
{
    /// <summary>
    /// CNPJ validator.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>2020-05-17 11:15:23</Date>
    public class CNPJValidator : DocumentValidator
    {
        /// <summary>
        /// Default error message.
        /// </summary>
        private const string DefaultMessage = "O '{PropertyValue}' é um CNPJ inválido";

        /// <summary>
        /// CNPJ validator.
        /// </summary>
        public CNPJValidator()
            : base(@"[\d]{2}\.[\d]{3}\.[\d]{3}\/[\d]{4}-[\d]{2}",
                  14,
                  DefaultMessage,
                  new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 },
                  new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 })
        {
        }
    }
}