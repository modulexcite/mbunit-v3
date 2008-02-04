// Copyright 2008 MbUnit Project - http://www.mbunit.com/
// Portions Copyright 2000-2004 Jonathan De Halleux, Jamie Cansdale
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

using System;

namespace Gallio.Data.Conversions
{
    /// <summary>
    /// Converts non-nullable values to <see cref="Nullable{T}" /> objects.
    /// </summary>
    public sealed class ValueToNullableConversionRule : IConversionRule
    {
        /// <inheritdoc />
        public ConversionCost GetConversionCost(Type sourceType, Type targetType, IConverter elementConverter)
        {
            if (typeof(Nullable).IsAssignableFrom(targetType)
                && Nullable.GetUnderlyingType(targetType).IsAssignableFrom(sourceType))
                return ConversionCost.Typical;

            return ConversionCost.Invalid;
        }

        /// <inheritdoc />
        public object Convert(object sourceValue, Type targetType, IConverter elementConverter)
        {
            return targetType.GetConstructor(new Type[] { Nullable.GetUnderlyingType(targetType) }).Invoke(new object[] { sourceValue });
        }
    }
}