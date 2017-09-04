    using System;
using System.Windows.Markup;

namespace timely.common.Infrastructure
{
    public class EnumerateExtension : MarkupExtension
    {
        public EnumerateExtension()
        {
        }

        public EnumerateExtension(Type enumType)
        {
            EnumType = enumType;
        }

        public Type EnumType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (EnumType == null)
            {
                throw new InvalidOperationException("The enum type must be specified.");
            }

            var actualEnumType = Nullable.GetUnderlyingType(EnumType) ?? EnumType;
            if (!actualEnumType.IsEnum)
            {
                throw new ArgumentException("Type must be for an Enum.");
            }
            
            var enumValues = Enum.GetValues(actualEnumType);

            return enumValues;
        }
    }
}
