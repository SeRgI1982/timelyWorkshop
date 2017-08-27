using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace timely.Infrastructure
{
    public class ValidatableObject : NotifiableObject, INotifyDataErrorInfo
    {
        private bool _susspend;
        private readonly List<ValidationResult> _errors = new List<ValidationResult>();
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return _errors?.Any() ?? false; }
        }

        public void SusspendValidation()
        {
            _susspend = true;
        }

        public void ResumeValidation()
        {
            _susspend = false;
        }

        public override bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null || !base.SetProperty(ref field, value, propertyName) || _susspend)
            {
                return false;
            }

            if (_values.ContainsKey(propertyName))
            {
                _values[propertyName] = value;
            }
            else
            {
                _values.Add(propertyName, value);
            }

            Validate();

            return true;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            var propertyErrors = _errors?.Where(error => error.MemberNames.Any(memberName => memberName == propertyName));
            return propertyErrors;
        }

        public void Validate()
        {
            var validationContext = new ValidationContext(this, null, null);
            _errors.Clear(); 

            Validator.TryValidateObject(this, validationContext, _errors, true);

            var memberNames = new HashSet<string>(_errors.SelectMany(error => error.MemberNames).Distinct().Concat(_values.Keys));
            RaiseErrorChanged(memberNames);    
        }
        
        public void Validate(string propertyName)
        {
            var validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };
            var crossPropertiesToClear = _errors.FirstOrDefault(error => error.MemberNames.Contains(propertyName))?.MemberNames.ToArray() ?? new[] { propertyName };
            _errors.Clear();

            foreach (var crossProperty in crossPropertiesToClear)
            {
                var localErrors = new List<ValidationResult>();
                object value;
                _values.TryGetValue(crossProperty, out value);
                Validator.TryValidateProperty(value, validationContext, localErrors);
                _errors.AddRange(localErrors);
            }
            
            RaiseErrorChanged(HasErrors ? _errors.SelectMany(error =>error.MemberNames) : crossPropertiesToClear);
        }

        private void RaiseErrorChanged(IEnumerable<string> memberNames)
        {
            foreach (var memberName in memberNames)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(memberName));
            }
        }
    }
}