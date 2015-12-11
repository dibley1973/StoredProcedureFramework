using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Extensions;
using System;
using System.Data;
using System.Reflection;
using Dibware.StoredProcedureFramework.Resources;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Maps the fields of a data reader record into
    /// </summary>
    public class DateReaderRecordToObjectMapper
    {
        #region Fields

        private readonly IDataReader _dataReader;
        private readonly Type _targetType;
        private PropertyInfo[] _targetObjectProperties;
        private ConstructorInfo _constructorInfo;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DateReaderRecordToObjectMapper"/> class.
        /// </summary>
        /// <param name="dataReader">
        /// The data reader which is to be used to populate the target.
        /// </param>
        /// <param name="targetType">
        /// The Type of the tapped target object.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// dataReader
        /// or
        /// targetType
        /// </exception>
        public DateReaderRecordToObjectMapper(IDataReader dataReader, Type targetType)
        {
            if (dataReader == null) throw new ArgumentNullException("dataReader");
            if (targetType == null) throw new ArgumentNullException("targetType");

            _dataReader = dataReader;
            _targetType = targetType;

            BuildTargetProperties();
            BuildTargetConstructor();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Creates an object of the target type using the current record of the 
        /// DataReader and populates the <see cref="MappedTarget"/> property with it.
        /// </summary>
        public void PopulateMappedTargetFromReaderRecord()
        {
            ClearMappedTarget();
            CreateNewTargetObject();
            ReadRecordFieldValuesIntoCurrentTargetProperties();
        }

        /// <summary>
        /// Gets the mapped target which will be populated after calling 
        /// <see cref="PopulateMappedTargetFromReaderRecord"/>.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public Object MappedTarget { get; private set; }

        #endregion

        #region Private Properties

        private PropertyInfo CurrentProperty { get; set; }
        private string CurrentFieldName { get; set; }
        private object CurrentFieldValue { get; set; }

        #endregion

        #region Private Members

        private void BuildTargetConstructor()
        {
            _constructorInfo = _targetType.GetConstructor(Type.EmptyTypes);

            EnsureTargetContructrorExists();
        }

        private void BuildTargetProperties()
        {
            _targetObjectProperties = _targetType.GetMappedProperties();
        }

        private void ClearMappedTarget()
        {
            MappedTarget = null;
        }

        private void CreateNewTargetObject()
        {
            MappedTarget = Activator.CreateInstance(_targetType);
        }

        private void EnsureTargetContructrorExists()
        {
            if (_constructorInfo == null)
                throw new MissingMethodException(
                    _targetType.Name, "Constructor");
        }

        private void HandleCastingExceptions(Exception ex)
        {
            if (!(ex is ArgumentException)) return;

            string returnTypeName = _targetType.Name;
            string message = string.Format(
                ExceptionMessages.IncorrectReturnType, CurrentFieldName, returnTypeName);

            throw new InvalidCastException(message, ex);
        }

        private void HandleMissingFieldExceptions(Exception ex)
        {
            if (!(ex is IndexOutOfRangeException)) return;

            string returnTypeName = _targetType.Name;
            string message = string.Format(ExceptionMessages.FieldNotFoundForName,
                CurrentFieldName, returnTypeName);

            throw new MissingFieldException(message, ex);
        }

        private void HandleOtherExceptions(Exception ex)
        {
            string returnTypeName = _targetType.Name;
            string message = string.Format(
                ExceptionMessages.ProcessingReturnColumnError, CurrentFieldName, returnTypeName);

            throw (Exception)Activator.CreateInstance(ex.GetType(), message, ex);
        }

        private void ReadRecordFieldValuesIntoCurrentTargetProperties()
        {
            foreach (PropertyInfo property in _targetObjectProperties)
            {
                SetCurrentProperty(property);
                SetCurrentFieldNameFromCurrentProperty();
                SetCurrentFieldValueFromReader();
                SetTargetPropertyFromCurrentFieldValue();
            }
        }

        private void SetCurrentFieldNameFromCurrentProperty()
        {
            CurrentFieldName = CurrentProperty.Name;
        }

        private void SetCurrentFieldValueFromReader()
        {
            try
            {
                CurrentFieldValue = _dataReader[CurrentFieldName];
            }
            catch (Exception ex)
            {
                HandleMissingFieldExceptions(ex);
                throw;
            }
        }

        private void SetCurrentProperty(PropertyInfo property)
        {
            CurrentProperty = property;
        }

        private void SetTargetPropertyFromCurrentFieldValue()
        {
            try
            {
                if (CurrentFieldValue is DBNull)
                {
                    TrySetTargetPropertyNullValue();
                }
                else
                {
                    CurrentProperty.SetValue(MappedTarget, CurrentFieldValue, null);
                }
            }
            catch (Exception ex)
            {
                HandleCastingExceptions(ex);
                HandleOtherExceptions(ex);
            }
        }

        private void TrySetTargetPropertyNullValue()
        {
            var isNullableType = GetIfCurrentPropertyNullableType();
            if (isNullableType)
            {
                CurrentProperty.SetValue(MappedTarget, null, null);
            }
            else
            {
                Type targetObjectType = _dataReader.GetFieldType(CurrentFieldName);
                throw new NullableFieldTypeException(CurrentFieldName, GetCurrentPropertyType(), targetObjectType);
            }
        }

        private Type GetCurrentPropertyType()
        {
            return CurrentProperty.PropertyType;
        }

        private bool GetIfCurrentPropertyNullableType()
        {
            var currentPropertyType = GetCurrentPropertyType();
            return (currentPropertyType == typeof(string) ||
                    (Nullable.GetUnderlyingType(currentPropertyType) != null));
        }

        #endregion
    }
}