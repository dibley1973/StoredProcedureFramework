using Dibware.StoredProcedureFramework.Extensions;
using System;
using System.Data;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class DateReaderRecordToObjectMapper
    {
        #region Fields

        private readonly IDataReader _dataReader;
        private readonly Type _targetType;
        private PropertyInfo[] _targetObjectProperties;
        private ConstructorInfo _constructorInfo;
        #endregion

        #region Constructor

        public DateReaderRecordToObjectMapper(IDataReader dataReader, Type targetType)
        {
            if (_dataReader == null) throw new ArgumentNullException("dataReader");
            if (targetType == null) throw new ArgumentNullException("targetType");

            _dataReader = dataReader;
            _targetType = targetType;

            BuildTargetProperties();
            BuildTargetConstructor();
        }

        public void ReadRecordIntoTarget()
        {
        }

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

        private void EnsureTargetContructrorExists()
        {
            if (_constructorInfo == null)
                throw new MissingMethodException(
                    _targetType.Name, "Constructor");
        }

        #endregion
    }
}
